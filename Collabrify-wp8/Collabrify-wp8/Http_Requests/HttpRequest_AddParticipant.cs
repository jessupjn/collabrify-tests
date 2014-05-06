using Collabrify_v2.CollabrifyProtocolBuffer;
using Collabrify_wp8.Collabrify;
using System;
using System.Net;

namespace Collabrify_wp8.Http_Requests
{

  public class HttpRequest_AddParticipant : HttpRequest__Object
  {

    // -------------------------------------------------------------------------

    /// <summary>
    /// makes a request to the server
    /// </summary> 
    public static void make_request(CollabrifyClient c, HttpRequest__Object obj, long id, string password)
    {
      CollabrifyRequest_PB req_pb = new CollabrifyRequest_PB();
      req_pb.request_type = CollabrifyRequestType_PB.ADD_PARTICIPANT_REQUEST;

      Request_AddParticipant_PB cs_pb = new Request_AddParticipant_PB();
      cs_pb.account_gmail = c.getAccountGmail();
      cs_pb.access_token = c.getAccessToken();
      cs_pb.participant_display_name = c.participant.getDisplayName();
      cs_pb.participant_user_id = c.participant.getUserID();
      cs_pb.participant_notification_id = c.participant.getId().ToString();
      cs_pb.participant_notification_type = NotificationMediumType_PB.COLLABRIFY_CLOUD_CHANNEL;

      cs_pb.session_id = id;
      cs_pb.session_password = password;

      HttpWebRequest request = obj.BuildRequest(req_pb, cs_pb);

      try 
      { 
        request.BeginGetRequestStream(new AsyncCallback(obj.getReqStream), request); 
      }
      catch (WebException e) 
      { 
        System.Diagnostics.Debug.WriteLine("  -- EXCEPTION THROWN \n" + e.Message); 
      }

    } // make_request

    // -------------------------------------------------------------------------

  }

}
