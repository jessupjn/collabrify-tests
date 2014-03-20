using Collabrify_v2.CollabrifyProtocolBuffer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Collabrify
{
    public class HttpRequest_AddEvent : HttpRequest_BasicCollabrifyPostRequest<Request_AddEvent_PB, Response_AddEvent_PB>
    {
        public static sealed String servletExtension = new ServletUrlExtension_PB().url_for_add_event;

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
         */
        public HttpRequest_AddEvent(String baseURL, Request_AddEvent_PB request_pb, bool useUnifiedServlet, TrailingDataWriteCallback writeCallback)
            : base(baseURL, baseURL,
              (useUnifiedServlet ? mainServletExtension : servletExtension),
              request_pb, CollabrifyRequestType_PB.ADD_EVENT_REQUEST,
              new Response_AddEvent_PB(), useUnifiedServlet,
              writeCallback, null)
        { }// ctor

        // ---------------------------------------------------------------------------

    }// class
}
