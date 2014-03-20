using Collabrify_v2.CollabrifyProtocolBuffer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Collabrify.http
{

    public class HttpRequest_CreateSession : HttpRequest_BasicCollabrifyPostRequest<Request_CreateSession_PB, Response_CreateSession_PB>
    {
      public static sealed String servletExtension = new ServletUrlExtension_PB().url_for_create_session;

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
      public HttpRequest_CreateSession(String baseURL,
          Request_CreateSession_PB request_pb, bool useUnifiedServlet)
          : base(baseURL,
            (useUnifiedServlet ? mainServletExtension : servletExtension),
            request_pb, CollabrifyRequestType_PB.CREATE_SESSION_REQUEST,
            new Response_CreateSession_PB(), useUnifiedServlet,
            null, null)
      { }// ctor

      // ---------------------------------------------------------------------------

    }// class

}
