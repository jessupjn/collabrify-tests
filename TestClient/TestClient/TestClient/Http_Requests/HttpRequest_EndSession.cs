using Collabrify_v2.CollabrifyProtocolBuffer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Collabrify.http
{

    public class HttpRequest_EndSession : HttpRequest_BasicCollabrifyPostRequest<Request_EndSession_PB, Response_EndSession_PB>
    {
      public static sealed String servletExtension = new ServletUrlExtension_PB().url_for_end_session;

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
       */
      public HttpRequest_EndSession(String baseURL,
          Request_EndSession_PB request_pb, bool useUnifiedServlet)
          : base(baseURL,
            (useUnifiedServlet ? mainServletExtension : servletExtension),
            request_pb, CollabrifyRequestType_PB.END_SESSION_REQUEST,
            new Response_EndSession_PB(), useUnifiedServlet, null,
            null)
      { }// ctor

      // ---------------------------------------------------------------------------

    }// class

}
