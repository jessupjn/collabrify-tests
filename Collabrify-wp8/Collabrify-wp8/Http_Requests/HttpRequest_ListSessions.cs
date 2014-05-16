using Collabrify_v2.CollabrifyProtocolBuffer;
using Collabrify_wp8.Collabrify;
using System;
using System.Collections.Generic;
using System.Net;

namespace Collabrify_wp8.Http_Requests
{

  public class HttpRequest_ListSessions : HttpRequest__Object
  {

    // -------------------------------------------------------------------------

    /// <summary>
    /// makes a warmup request to the server and returns a response object in the form of RESPONSE_
    /// </summary> 
    public static void make_request(CollabrifyClient c, HttpRequest__Object obj, List<string> tags)
    {
      CollabrifyRequest_PB req_pb = new CollabrifyRequest_PB();
      req_pb.request_type = CollabrifyRequestType_PB.LIST_SESSIONS_REQUEST;

      Request_ListSessions_PB cs_pb = new Request_ListSessions_PB();
      cs_pb.account_gmail = c.getAccountGmail();
      cs_pb.access_token = c.getAccessToken();
      foreach (string s in tags) cs_pb.session_tag.Add(s);
      cs_pb.limit_query_to_organization_account = true;
      cs_pb.use_subset_query = true;
      cs_pb.account_gmail = c.getAccountGmail();
      cs_pb.access_token = c.getAccessToken();
      foreach (string s in tags)
      {
        cs_pb.session_tag.Add(s);
      }

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
