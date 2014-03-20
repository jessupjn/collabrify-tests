using Collabrify_v2.CollabrifyProtocolBuffer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Collabrify
{

    public class HttpRequest_AddParticipant : HttpRequest_BasicCollabrifyPostRequest<Request_AddParticipant_PB, Response_AddParticipant_PB>
    {
      public static sealed String servletExtension = new ServletUrlExtension_PB().url_for_add_participant;

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
      public HttpRequest_AddParticipant(String baseURL, Request_AddParticipant_PB request_pb, bool useUnifiedServlet)
          : base(baseURL,
            (useUnifiedServlet ? mainServletExtension : servletExtension),
            request_pb, CollabrifyRequestType_PB.ADD_PARTICIPANT_REQUEST,
            new Response_AddParticipant_PB(), useUnifiedServlet,
            null, null)
      { }// ctor

      // ---------------------------------------------------------------------------

    }// class

}
