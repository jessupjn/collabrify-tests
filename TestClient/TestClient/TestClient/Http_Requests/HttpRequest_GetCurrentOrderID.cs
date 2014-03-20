using Collabrify_v2.CollabrifyProtocolBuffer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Collabrify.http
{

    public class HttpRequest_GetCurrentOrderID : HttpRequest_BasicCollabrifyPostRequest<Request_GetCurrentOrderID_PB, Response_GetCurrentOrderID_PB>
    {
      public static sealed String servletExtension = new ServletUrlExtension_PB().url_for_get_current_order_id;

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
      public HttpRequest_GetCurrentOrderID(String baseURL,
          Request_GetCurrentOrderID_PB request_pb, bool useUnifiedServlet)
          : base(baseURL,
            (useUnifiedServlet ? mainServletExtension : servletExtension),
            request_pb, CollabrifyRequestType_PB.GET_CURRENT_ORDER_ID_REQUEST,
            new Response_GetCurrentOrderID_PB(), useUnifiedServlet,
            null, null)
      { }// ctor

      // ---------------------------------------------------------------------------

    }// class


}
