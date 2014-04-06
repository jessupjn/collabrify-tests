using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Collabrify_v2.CollabrifyProtocolBuffer;
using Collabrify_wp8.Http_Requests;
using System.Net;
using Collabrify_wp8.Collabrify;
using System.Diagnostics;

namespace Collabrify_wp8.Http_Requests
{
  public class HttpRequest_RemoveParticipant : HttpRequest__Object
  {

    /// <summary>
    /// baseURL
    ///
    /// makes a warmup request to the server and returns a response object in the form of RESPONSE_
    /// </summary> 
    public static void make_request(CollabrifyClient c, HttpRequest__Object obj)
    {
      CollabrifyRequest_PB req_pb = new CollabrifyRequest_PB();
      req_pb.request_type = CollabrifyRequestType_PB.REMOVE_PARTICIPANT_REQUEST;

      Request_RemoveParticipant_PB cs_pb = new Request_RemoveParticipant_PB();
      cs_pb.account_gmail = c.getAccountGmail();
      cs_pb.access_token = c.getAccessToken();
      cs_pb.session_id = c.currentSessionID();
      if(c.getSession().getIsPasswordProtected())
      {
        cs_pb.session_password = "";
      }
      cs_pb.accessing_participant_id = c.participant.getId();
      cs_pb.to_be_removed_participant_id = c.participant.getId();
      Debug.WriteLine("ID: " + c.participant.getId());

      HttpWebRequest request = obj.BuildRequest(req_pb, cs_pb);

      try { request.BeginGetRequestStream(new AsyncCallback(obj.getReqStream), request); }
      catch (WebException e) { System.Diagnostics.Debug.WriteLine("  -- EXCEPTION THROWN \n" + e.Message); }

    }


  }
}
