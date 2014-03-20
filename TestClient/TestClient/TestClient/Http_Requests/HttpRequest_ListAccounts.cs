using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Collabrify_v2.CollabrifyProtocolBuffer;

namespace Collabrify
{
  public class HttpRequest_ListAccounts : 
    HttpRequest_BasicCollabrifyPostRequest<Request_ListAccounts_PB, 
    Response_ListAccounts_PB>
  {
    public static readonly String servletExtension = new ServletUrlExtension_PB().url_for_list_accounts; 
     
    // ---------------------------------------------------------------------------
    // ---------------------------------------------------------------------------

    public HttpRequest_ListAccounts(String baseURL,
        Request_ListAccounts_PB request_pb, bool useUnifiedServlet)
      : base(baseURL,
          (useUnifiedServlet ? mainServletExtension : servletExtension),
          request_pb, CollabrifyRequestType_PB.LIST_ACCOUNTS_REQUEST,
          new Response_ListAccounts_PB(), useUnifiedServlet, null, null)
    { }// ctor

    // ---------------------------------------------------------------------------

  }
}
