using Collabrify_v2.CollabrifyProtocolBuffer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Collabrify
{
  public class HttpRequest_UpdateNotificationID : 
    HttpRequest_BasicCollabrifyPostRequest<Request_UpdateNotificationID_PB, 
    Response_UpdateNotificationID_PB>
  {
    public static readonly string servletExtension = new ServletUrlExtension_PB()
      .url_for_update_notification_id;

    // ---------------------------------------------------------------------------
    // ---------------------------------------------------------------------------

    /// <summary> 
    /// 
    /// baseURL
    ///
    /// request_pb
    ///
    /// useUnifiedServlet
    ///   If true, the request object will be prefixed by
    ///   CollabrifyRequest_PB, and will be sent to the unified servlet.
    ///   Otherwise, the request object will be sent alone to the old
    ///   specific servlet.
    ///
    /// </summary> 
    public HttpRequest_UpdateNotificationID(String baseURL,
        Request_UpdateNotificationID_PB request_pb, bool useUnifiedServlet)
      : base(baseURL,
            (useUnifiedServlet ? mainServletExtension : servletExtension),
            request_pb, CollabrifyRequestType_PB.UPDATE_NOTIFICATION_ID_REQUEST,
            new Response_UpdateNotificationID_PB(),
            useUnifiedServlet, null, null)
    { }// ctor

    // ---------------------------------------------------------------------------

  }// class
}
