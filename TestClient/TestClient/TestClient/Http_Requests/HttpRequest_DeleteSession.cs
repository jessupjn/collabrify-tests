using Collabrify_v2.CollabrifyProtocolBuffer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Collabrify.http
{

    public class HttpRequest_DeleteSession : HttpRequest_BasicCollabrifyPostRequest<Request_DeleteSession_PB, Response_DeleteSession_PB>
    {
      public static sealed String servletExtension = new ServletUrlExtension_PB().url_for_delete_session;

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
      public HttpRequest_DeleteSession(String baseURL,
          Request_DeleteSession_PB request_pb, bool useUnifiedServlet)
          : base(baseURL,
            (useUnifiedServlet ? mainServletExtension : servletExtension),
            request_pb, CollabrifyRequestType_PB.DELETE_SESSION_REQUEST,
            new Response_DeleteSession_PB(), useUnifiedServlet,
            null, null)
      { }// ctor

      // ---------------------------------------------------------------------------

    }// class

}
