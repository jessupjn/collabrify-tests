using Collabrify_v2.CollabrifyProtocolBuffer;
using Collabrify_wp8.Collabrify;
using Collabrify_wp8.Http_Requests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;

namespace Collabrify_wp8.Http_Requests
{

  public class HttpRequest_CreateSessionWithBaseFile : HttpRequest__Object
  {

    /// <summary>
    /// baseURL
    ///
    /// makes a warmup request to the server and returns a response object in the form of RESPONSE_
    /// </summary> 
    public static void make_request(CollabrifyClient c, HttpRequest__Object obj)
    {
      CollabrifyRequest_PB req_pb = new CollabrifyRequest_PB();
      req_pb.request_type = CollabrifyRequestType_PB.CREATE_SESSION_WITH_BASE_FILE_REQUEST;

      Random rd = new Random();
      Request_CreateSession_PB cs_pb = new Request_CreateSession_PB();
      cs_pb.account_gmail = c.getAccountGmail();
      cs_pb.access_token = c.getAccessToken();
      cs_pb.owner_display_name = "Jack";
      cs_pb.session_name = rd.Next(1, 2000000000).ToString();
      cs_pb.session_tag.Add("[none]");
      cs_pb.owner_notification_id = "123123123";
      cs_pb.owner_display_name = "Jack";
      cs_pb.owner_gmail = "wp8-collabrify@umich.edu";
      cs_pb.owner_notification_type = NotificationMediumType_PB.COLLABRIFY_CLOUD_CHANNEL;

      HttpWebRequest request = obj.BuildRequest(req_pb, cs_pb);

      try { request.BeginGetRequestStream(new AsyncCallback(obj.getReqStream), request); }
      catch (WebException e) { System.Diagnostics.Debug.WriteLine("  -- EXCEPTION THROWN \n" + e.Message); }

    }


  }

}
