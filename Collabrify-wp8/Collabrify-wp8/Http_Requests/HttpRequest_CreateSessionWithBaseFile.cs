﻿using Collabrify_v2.CollabrifyProtocolBuffer;
using Collabrify_wp8.Collabrify;
using System;
using System.Collections.Generic;
using System.Net;

namespace Collabrify_wp8.Http_Requests
{

  public class HttpRequest_CreateSessionWithBaseFile : HttpRequest__Object
  {

    // -------------------------------------------------------------------------

    /// <summary>
    /// makes a warmup request to the server and returns a response object in the form of RESPONSE_
    /// </summary> 
    public static void make_request(CollabrifyClient c, HttpRequest__Object obj, string name, List<string> tags, string password, int participantLimit = 0)
    {
      CollabrifyRequest_PB req_pb = new CollabrifyRequest_PB();
      req_pb.request_type = CollabrifyRequestType_PB.CREATE_SESSION_WITH_BASE_FILE_REQUEST;

      Random rd = new Random();
      Request_CreateSession_PB cs_pb = new Request_CreateSession_PB();
      cs_pb.owner_display_name = c.participant.getDisplayName();
      cs_pb.session_name = name;
      if (password != "")
      {
        cs_pb.session_password = password;
      }

      foreach (string s in tags)
      {
        cs_pb.session_tag.Add(s);
      }
      cs_pb.account_gmail = c.getAccountGmail();
      cs_pb.access_token = c.getAccessToken();
      cs_pb.owner_display_name = c.participant.getDisplayName();
      cs_pb.owner_user_id = c.participant.getUserID();

      if (participantLimit > 0)
      {
        cs_pb.participant_limit = participantLimit;
      }

      cs_pb.owner_notification_id = "0";
      cs_pb.owner_notification_type = NotificationMediumType_PB.COLLABRIFY_CLOUD_CHANNEL;
      byte[] baseFile = null;
      HttpWebRequest request = obj.BuildRequest(req_pb, cs_pb, baseFile);

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

