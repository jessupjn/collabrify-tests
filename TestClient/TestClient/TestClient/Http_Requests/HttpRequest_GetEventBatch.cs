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
public class HttpRequest_GetEventBatch : HttpRequest_BasicCollabrifyPostRequest<Request_GetEventBatch_PB, Response_GetEventBatch_PB>
  {
    public static sealed String servletExtension = new ServletUrlExtension_PB().url_for_get_event_batch;

    // Holder for events parsed from stream after unified request.
    private List<CollabrifyEvent_PB> events;

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
     * @param writeCallback
     *          This will be called when it is time to write trailing data. This
     *          can be null.
     * 
     * @param readCallback
     *          This will be called when it is time to read trailing data. This
     *          can be null.
     */
    public HttpRequest_GetEventBatch(String baseURL,
        Request_GetEventBatch_PB request_pb, bool useUnifiedServlet)
      : base(baseURL,
          (useUnifiedServlet ? mainServletExtension : servletExtension),
          request_pb, CollabrifyRequestType_PB.GET_EVENT_BATCH_REQUEST,
          new Response_GetEventBatch_PB(), useUnifiedServlet,
          null, null)
    { }// ctor

    // ---------------------------------------------------------------------------

    
    public override void doTrailingDataReadCallback(InputStream inputStream,
        CollabrifyResponse_PB responseWrapper,
        Response_GetEventBatch_PB responseObject, HttpURLConnection connection)
        throws Exception
    {
      if( isUnifiedRequest() )
      {
        events = new ArrayList<CollabrifyEvent_PB>();

        for( int i = 0; i < responseObject.getNumberOfEventsToFollow(); i++ )
        {
          events.add(CollabrifyEvent_PB.parseDelimitedFrom(inputStream));
        }
      }// if
      else
      {
        events = responseObject.getEventList();
      }// else

    }// doTrailingDataReadCallback

    // ---------------------------------------------------------------------------

    public List<CollabrifyEvent_PB> getEvents()
    {
      return events;
    }

    // ---------------------------------------------------------------------------

  }// class

}
