using Collabrify_v2.CollabrifyProtocolBuffer;
using Collabrify_wp8.Http_Requests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;

namespace Collabrify_wp8.Http_Requests
{

  public class HttpRequest_GetFromBaseFile : HttpRequest__Object
  {

    /// <summary>
    /// baseURL
    ///
    /// makes a warmup request to the server and returns a response object in the form of RESPONSE_
    /// </summary> 
    public static void make_request()
    {
      CollabrifyRequest_PB req_pb = new CollabrifyRequest_PB();
      req_pb.request_type = CollabrifyRequestType_PB.GET_FROM_BASE_FILE_REQUEST;

      HttpRequest__Object obj = new HttpRequest__Object();
      HttpWebRequest request = obj.BuildRequest(req_pb);

      try { request.BeginGetRequestStream(new AsyncCallback(obj.getReqStream), request); }
      catch (WebException e) { System.Diagnostics.Debug.WriteLine("  -- EXCEPTION THROWN \n" + e.Message); }

    }


  }

}
