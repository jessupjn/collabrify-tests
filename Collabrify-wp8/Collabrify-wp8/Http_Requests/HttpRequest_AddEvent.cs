using Collabrify_v2.CollabrifyProtocolBuffer;
using Collabrify_wp8;
using Collabrify_wp8.Collabrify;
using Collabrify_wp8.Http_Requests;
using ProtoBuf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;

namespace Collabrify_wp8.Http_Requests
{
  public class HttpRequest_AddEvent : HttpRequest__Object
  {

    /// <summary>
    /// baseURL
    ///
    /// makes a warmup request to the server and returns a response object in the form of RESPONSE_
    /// </summary> 
    public static void make_request(CollabrifyClient c, HttpRequest__Object obj, byte[] data, string eventType)
    {
      CollabrifyRequest_PB req_pb = new CollabrifyRequest_PB();
      req_pb.request_type = CollabrifyRequestType_PB.ADD_EVENT_REQUEST;

      Request_AddEvent_PB cs_pb = new Request_AddEvent_PB();
      cs_pb.account_gmail = c.getAccountGmail();
      cs_pb.access_token = c.getAccessToken();
      cs_pb.event_data = data;
      cs_pb.event_type = eventType;
      cs_pb.session_id = c.currentSessionID();

      HttpWebRequest request = obj.BuildRequest( req_pb, cs_pb );

      try { request.BeginGetRequestStream(new AsyncCallback(obj.getReqStream), request); }
      catch (WebException e) { System.Diagnostics.Debug.WriteLine("  -- EXCEPTION THROWN \n" + e.Message); }
    
    }


  }
}
