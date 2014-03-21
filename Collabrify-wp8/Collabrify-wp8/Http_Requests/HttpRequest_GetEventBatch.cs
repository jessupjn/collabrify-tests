using Collabrify_v2.CollabrifyProtocolBuffer;
using Collabrify_wp8.Http_Requests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;

namespace Collabrify_wp8.Http_Requests
{

/**
 * HttpRequest_GetEvent and HttpRequest_GetEventBatch are the only two classes
 * that need to expose additional getter methods. The unified servlet places the
 * CollabrifyEvent_DS(s) after the response whereas the classic servlet does
 * not. To handle this difference, the event(s) is automatically pulled from the
 * stream and made available via a getter.
 * 
 */
  public class HttpRequest_GetEventBatch : HttpRequest__Object
  {

    /// <summary>
    /// baseURL
    ///
    /// makes a warmup request to the server and returns a response object in the form of RESPONSE_
    /// </summary> 
    public static void make_request()
    {
      CollabrifyRequest_PB req_pb = new CollabrifyRequest_PB();
      req_pb.request_type = CollabrifyRequestType_PB.GET_EVENT_BATCH_REQUEST;

      HttpRequest__Object obj = new HttpRequest__Object();
      HttpWebRequest request = obj.BuildRequest(req_pb);

      try { request.BeginGetRequestStream(new AsyncCallback(obj.getReqStream), request); }
      catch (WebException e) { System.Diagnostics.Debug.WriteLine("  -- EXCEPTION THROWN \n" + e.Message); }

    }


  }

}
