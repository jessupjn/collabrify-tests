using Collabrify_v2.CollabrifyProtocolBuffer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Collabrify
{
  public class HttpRequest_GetSession : 
    HttpRequest_BasicCollabrifyPostRequest<Request_GetSession_PB, 
    Response_GetSession_PB>
  {
    public static sealed string servletExtension = new ServletUrlExtension_PB().url_for_get_session;

  // ---------------------------------------------------------------------------
  // ---------------------------------------------------------------------------

  public HttpRequest_GetSession(string baseURL,
      Request_GetSession_PB request_pb, bool useUnifiedServlet)
        : base(baseURL,
            (useUnifiedServlet ? mainServletExtension : servletExtension),
            request_pb, CollabrifyRequestType_PB.GET_SESSION_REQUEST,
            new Response_GetSession_PB(), useUnifiedServlet, null,
            null)
  { }// ctor

  // ---------------------------------------------------------------------------

  }
}
