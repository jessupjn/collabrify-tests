using Collabrify_v2.CollabrifyProtocolBuffer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Collabrify
{
  public class HttpRequest_GetNotificationID : 
    HttpRequest_BasicCollabrifyPostRequest<Request_GetNotificationID_PB, 
    Response_GetNotificationID_PB>
  {
    // ---------------------------------------------------------------------------
    // ---------------------------------------------------------------------------

    public HttpRequest_GetNotificationID(String baseURL, String servletExtension,
        Request_GetNotificationID_PB request_pb)
      : base(baseURL, servletExtension, request_pb,
          CollabrifyRequestType_PB.GET_NOTIFICATION_ID_REQUEST,
          new Response_GetNotificationID_PB(), true, null, null)
    { }// ctor

    // ---------------------------------------------------------------------------

  }// class 
}
