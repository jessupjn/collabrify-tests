using Collabrify_v2.CollabrifyProtocolBuffer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Collabrify
{
  public class HttpRequest_ListSessions :
     HttpRequest_BasicCollabrifyPostRequest<Request_ListSessions_PB, 
     Response_ListSessions_PB>
  {
    public static readonly string servletExtension = new ServletUrlExtension_PB().url_for_list_sessions;

  // ---------------------------------------------------------------------------
  // ---------------------------------------------------------------------------

  public HttpRequest_ListSessions(String baseURL,
      Request_ListSessions_PB request_pb, bool useUnifiedServlet)
    : base(baseURL,
        (useUnifiedServlet ? mainServletExtension : servletExtension),
        request_pb, CollabrifyRequestType_PB.LIST_SESSIONS_REQUEST,
        new Response_ListSessions_PB(), useUnifiedServlet, null,
        null)
  { }// ctor

  // ---------------------------------------------------------------------------

  }
}
