﻿using Collabrify_v2.CollabrifyProtocolBuffer;
using Collabrify_wp8.Collabrify;
using System;
using System.Net;

namespace Collabrify_wp8.Http_Requests
{

  public class HttpRequest_DeleteSession : HttpRequest__Object
  {

    // -------------------------------------------------------------------------

    /// <summary>
    /// makes a warmup request to the server and returns a response object in the form of RESPONSE_
    /// </summary> 
    public static void make_request(CollabrifyClient c, HttpRequest__Object obj)
    {

      /* if user isnt owner of session, dont delete the session */
      if (c.currentSessionOwner().getId() != c.participant.getId())
      {
        return;
      }

      CollabrifyRequest_PB req_pb = new CollabrifyRequest_PB();
      req_pb.request_type = CollabrifyRequestType_PB.DELETE_SESSION_REQUEST;

      Request_DeleteSession_PB cs_pb = new Request_DeleteSession_PB();
      cs_pb.account_gmail = c.getAccountGmail();
      cs_pb.access_token = c.getAccessToken();
      cs_pb.participant_id = c.participant.getId();
      cs_pb.session_id = c.currentSessionID();

      CollabrifyParticipant owner = c.currentSessionOwner();
      HttpWebRequest request = obj.BuildRequest(req_pb, cs_pb);

      try
      {
        request.BeginGetRequestStream(new AsyncCallback(obj.getReqStream), request);
      }
      catch (WebException e)
      {
        System.Diagnostics.Debug.WriteLine("BeginGetRequestStream FAILED:\n" + e.Message);
      }

    } // make_request

    // -------------------------------------------------------------------------

  }

}
