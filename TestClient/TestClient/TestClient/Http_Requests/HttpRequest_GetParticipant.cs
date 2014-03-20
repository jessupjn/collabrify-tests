using Collabrify_v2.CollabrifyProtocolBuffer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Collabrify
{
  public class HttpRequest_GetParticipant : HttpRequest_BasicCollabrifyPostRequest<Request_GetParticipant_PB, Response_GetParticipant_PB>
{
  public static sealed String servletExtension = ServletUrlExtension_PB.getDefaultInstance().getUrlForGetParticipant();

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
  public HttpRequest_GetParticipant(String baseURL,
      Request_GetParticipant_PB request_pb, bool useUnifiedServlet)
    : base(baseURL, (useUnifiedServlet ? mainServletExtension : servletExtension),
        request_pb, CollabrifyRequestType_PB.GET_PARTICIPANT_REQUEST,
        Response_GetParticipant_PB.getDefaultInstance(), useUnifiedServlet,
        null, null)
  { }// ctor

  // ---------------------------------------------------------------------------

}// class

}
