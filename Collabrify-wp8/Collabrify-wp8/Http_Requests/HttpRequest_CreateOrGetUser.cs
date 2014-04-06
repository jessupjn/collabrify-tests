using Collabrify_v2.CollabrifyProtocolBuffer;
using Collabrify_wp8.Collabrify;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Collabrify_wp8.Http_Requests
{
  class HttpRequest_CreateOrGetUser : HttpRequest__Object
  {

    // -------------------------------------------------------------------------

    /// <summary>
    /// makes a warmup request to the server and returns a response object in the form of RESPONSE_
    /// </summary> 
    public static void make_request(CollabrifyClient c, HttpRequest__Object obj)
    {
      CollabrifyRequest_PB req_pb = new CollabrifyRequest_PB();
      req_pb.request_type = CollabrifyRequestType_PB.CREATE_OR_GET_USER;

      Request_CreateOrGetUser_PB cs_pb = new Request_CreateOrGetUser_PB();
      cs_pb.create_if_does_not_exist = true;

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

