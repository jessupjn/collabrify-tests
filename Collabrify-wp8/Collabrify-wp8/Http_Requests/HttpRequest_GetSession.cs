using Collabrify_v2.CollabrifyProtocolBuffer;
using Collabrify_wp8.Collabrify;
using System;
using System.Net;

namespace Collabrify_wp8.Http_Requests
{
  public class HttpRequest_GetSession : HttpRequest__Object
  {

    // -------------------------------------------------------------------------

    /// <summary>
    /// makes a warmup request to the server and returns a response object in the form of RESPONSE_
    /// </summary> 
    public static void make_request(CollabrifyClient c, HttpRequest__Object obj, long session_id, string session_password)
    {
      CollabrifyRequest_PB req_pb = new CollabrifyRequest_PB();
      req_pb.request_type = CollabrifyRequestType_PB.GET_SESSION_REQUEST;

      Request_GetSession_PB cs_pb = new Request_GetSession_PB();
      //cs_pb.account_gmail = c.getAccountGmail();
      //cs_pb.access_token = c.getAccessToken();
      cs_pb.session_id = session_id;
      cs_pb.session_password = session_password;

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
