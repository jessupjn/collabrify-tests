using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Collabrify_v2.CollabrifyProtocolBuffer;

namespace Collabrify
{
  public class HttpRequest_RemoveParticipant : 
    HttpRequest_BasicCollabrifyPostRequest<Request_RemoveParticipant_PB, 
    Response_RemoveParticipant_PB>
  {

    /// <summary>
    /// baseURL
    ///
    /// request_pb
    ///
    /// useUnifiedServlet
    ///   If true, the request object will be prefixed by
    ///   CollabrifyRequest_PB, and will be sent to the unified servlet.
    ///   Otherwise, the request object will be sent alone to the old
    ///   specific servlet. 
    /// </summary> 

    public HttpRequest_RemoveParticipant(String baseURL,
      Request_RemoveParticipant_PB request_pb, bool useUnifiedServlet)
      : base(baseURL,
          (useUnifiedServlet ? mainServletExtension : servletExtension),
          request_pb, CollabrifyRequestType_PB.REMOVE_PARTICIPANT_REQUEST,
          new Response_RemoveParticipant_PB(), useUnifiedServlet,
          null, null)
    { }// ctor

    // ---------------------------------------------------------------------------

  }
}
