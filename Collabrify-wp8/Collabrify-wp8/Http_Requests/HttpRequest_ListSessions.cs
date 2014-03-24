﻿using Collabrify_v2.CollabrifyProtocolBuffer;
using Collabrify_wp8.Collabrify;
using Collabrify_wp8.Http_Requests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;

namespace Collabrify_wp8.Http_Requests
{
  public class HttpRequest_ListSessions : HttpRequest__Object
  {

    /// <summary>
    /// baseURL
    ///
    /// makes a warmup request to the server and returns a response object in the form of RESPONSE_
    /// </summary> 
    public static void make_request(CollabrifyClient c, HttpRequest__Object obj)
    {
      CollabrifyRequest_PB req_pb = new CollabrifyRequest_PB();
      req_pb.request_type = CollabrifyRequestType_PB.LIST_SESSIONS_REQUEST;

      Request_ListSessions_PB cs_pb = new Request_ListSessions_PB();
      cs_pb.account_gmail = c.getAccountGmail();
      cs_pb.access_token = c.getAccessToken();
      cs_pb.session_tag.Add("[none]");
      cs_pb.include_sessions_with_incomplete_base_files_flag = true;
      cs_pb.limit_query_to_organization_account = true;
      cs_pb.use_subset_query = false;
      cs_pb.session_type = "cool";

      obj = new HttpRequest__Object();
      HttpWebRequest request = obj.BuildRequest(req_pb, cs_pb);

      try { request.BeginGetRequestStream(new AsyncCallback(obj.getReqStream), request); }
      catch (WebException e) { System.Diagnostics.Debug.WriteLine("  -- EXCEPTION THROWN \n" + e.Message); }

    }


  }
}
