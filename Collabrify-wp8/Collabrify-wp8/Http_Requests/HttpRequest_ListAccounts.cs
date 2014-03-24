using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Collabrify_v2.CollabrifyProtocolBuffer;
using Collabrify_wp8.Http_Requests;
using System.Net;
using Collabrify_wp8.Collabrify;

namespace Collabrify_wp8.Http_Requests
{
  public class HttpRequest_ListAccounts : HttpRequest__Object
  {

    /// <summary>
    /// baseURL
    ///
    /// makes a warmup request to the server and returns a response object in the form of RESPONSE_
    /// </summary> 
    public static void make_request(CollabrifyClient c, HttpRequest__Object obj)
    {
      CollabrifyRequest_PB req_pb = new CollabrifyRequest_PB();
      req_pb.request_type = CollabrifyRequestType_PB.LIST_ACCOUNTS_REQUEST;

      Request_ListAccounts_PB cs_pb = new Request_ListAccounts_PB();

      obj = new HttpRequest__Object();
      HttpWebRequest request = obj.BuildRequest(req_pb, cs_pb);

      try { request.BeginGetRequestStream(new AsyncCallback(obj.getReqStream), request); }
      catch (WebException e) { System.Diagnostics.Debug.WriteLine("  -- EXCEPTION THROWN \n" + e.Message); }

    }


  }
}
