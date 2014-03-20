using Collabrify_v2.CollabrifyProtocolBuffer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Collabrify.http
{

    /**
     * HttpRequest_GetEvent and HttpRequest_GetEventBatch are the only two classes
     * that need to expose additional getter methods. The unified servlet places the
     * CollabrifyEvent_DS(s) after the response whereas the classic servlet does
     * not. To handle this difference, the event(s) is automatically pulled from the
     * stream and made available via a getter.
     * 
     */
    public class HttpRequest_GetEvent : HttpRequest_BasicCollabrifyPostRequest<Request_GetEvent_PB, Response_GetEvent_PB>
    {
      public static sealed String servletExtension = new ServletUrlExtension_PB().url_for_get_event;

      // Holder for event parsed from stream after unified request.
      private CollabrifyEvent_PB _event;

      // ---------------------------------------------------------------------------
      // ---------------------------------------------------------------------------

      /**
       * 
       * @param baseURL
       * 
       * @param request_pb
       * 
       * @param useUnifiedServlet
       *          If true, the request object will be prefixed by
       *          CollabrifyRequest_PB, and will be sent to the unified servlet.
       *          Otherwise, the request object will be sent alone to the old
       *          specific servlet.
       * 
       * @param readCallback
       *          This will be called when it is time to read trailing data. This
       *          can be null.
       */
      public HttpRequest_GetEvent(String baseURL, Request_GetEvent_PB request_pb,
          bool useUnifiedServlet)
        :base(baseURL,
            (useUnifiedServlet ? mainServletExtension : servletExtension),
            request_pb, CollabrifyRequestType_PB.GET_EVENT_REQUEST,
            Response_GetEvent_PB.getDefaultInstance(), useUnifiedServlet, null,
            null)
      { }// ctor

      // ---------------------------------------------------------------------------

      
      public override void doTrailingDataReadCallback(InputStream inputStream,
          CollabrifyResponse_PB responseWrapper,
          Response_GetEvent_PB responseObject, HttpURLConnection connection)
          throws Exception
      {
        if( isUnifiedRequest() )
        {
          _event = CollabrifyEvent_PB.parseDelimitedFrom(inputStream);
        }
        else
        {
          _event = getResponseObject().getEvent();
        }
      }// doTrailingDataReadCallback

      // ---------------------------------------------------------------------------

      public CollabrifyEvent_PB getEvent()
      {
        return _event;
      }

      // ---------------------------------------------------------------------------

    }// class

}
