using Collabrify_v2.CollabrifyProtocolBuffer;
using ProtoBuf;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Threading;

namespace Collabrify_wp8.Http_Requests
{
    public class HttpRequest__Object
    {
        public static readonly string BASE_URI = "http://collabrify-cloud.appspot.com/request";

        private CollabrifyRequest_PB collabrify_req_pb;
        private CollabrifyResponse_PB collabrify_resp_pb;
        private object secondary_pb;
        private object trail_info;
        private object returned_secondary_pb;
        private object returned_trail_info;

        public HttpWebRequest BuildRequest( CollabrifyRequest_PB req_pb, object _secondary_pb = null, object _trail_info = null )
        {
            HttpWebRequest request = HttpWebRequest.CreateHttp( BASE_URI );
            request.ContentType = "application/x-www-form-urlencoded";
            request.Method = "POST";
            request.Credentials = new NetworkCredential("wp8-collabrify@umich.edu", "82763BDBCA");

            collabrify_req_pb = req_pb;
            if (_secondary_pb != null) secondary_pb = _secondary_pb;
            if (_trail_info != null) trail_info = _trail_info;


            return request;
        }


        public void getReqStream(IAsyncResult result)
        {
          try
          {
            HttpWebRequest request = (HttpWebRequest)result.AsyncState;
            Stream postStream = request.EndGetRequestStream(result);

            MemoryStream ms = new MemoryStream();
            MemoryStream ms2 = new MemoryStream();
            Serializer.SerializeWithLengthPrefix<CollabrifyRequest_PB>(ms, collabrify_req_pb, PrefixStyle.Base128, 0);

            byte[] byteArr = ms.ToArray();
            postStream.Write(byteArr, 0, byteArr.Length);
            System.Diagnostics.Debug.WriteLine("writing ms to stream... size: " + byteArr.Length.ToString());

            if (collabrify_req_pb.request_type == CollabrifyRequestType_PB.ADD_EVENT_REQUEST)
            {
              Serializer.SerializeWithLengthPrefix<Request_AddEvent_PB>(ms2, (Request_AddEvent_PB)secondary_pb, PrefixStyle.Base128, 0);
              byteArr = ms2.ToArray();
              postStream.Write(byteArr, 0, byteArr.Length);
              System.Diagnostics.Debug.WriteLine("writing ms2 to stream... size: " + byteArr.Length.ToString());
            }
            else if (collabrify_req_pb.request_type == CollabrifyRequestType_PB.ADD_PARTICIPANT_REQUEST) ;
            else if (collabrify_req_pb.request_type == CollabrifyRequestType_PB.ADD_TO_BASE_FILE_REQUEST) ;
            else if (collabrify_req_pb.request_type == CollabrifyRequestType_PB.CREATE_OR_GET_USER) ;
            else if (collabrify_req_pb.request_type == CollabrifyRequestType_PB.CREATE_SESSION_REQUEST)
            {
              Serializer.SerializeWithLengthPrefix<Request_CreateSession_PB>(ms2, (Request_CreateSession_PB)secondary_pb, PrefixStyle.Base128, 0);
              writeSecondObject(ms2, postStream);
            }
            else if (collabrify_req_pb.request_type == CollabrifyRequestType_PB.CREATE_SESSION_WITH_BASE_FILE_REQUEST) ;
            else if (collabrify_req_pb.request_type == CollabrifyRequestType_PB.DELETE_ALL_SESSIONS_REQUEST) ;
            else if (collabrify_req_pb.request_type == CollabrifyRequestType_PB.DELETE_OLD_SESSIONS_REQUEST) ;
            else if (collabrify_req_pb.request_type == CollabrifyRequestType_PB.DELETE_SESSION_REQUEST) ;
            else if (collabrify_req_pb.request_type == CollabrifyRequestType_PB.DELETE_USER) ;
            else if (collabrify_req_pb.request_type == CollabrifyRequestType_PB.END_SESSION_REQUEST) ;
            else if (collabrify_req_pb.request_type == CollabrifyRequestType_PB.GET_BASE_FILE_REQUEST) ;
            else if (collabrify_req_pb.request_type == CollabrifyRequestType_PB.GET_CURRENT_ORDER_ID_REQUEST) ;
            else if (collabrify_req_pb.request_type == CollabrifyRequestType_PB.GET_EVENT_BATCH_REQUEST) ;
            else if (collabrify_req_pb.request_type == CollabrifyRequestType_PB.GET_EVENT_REQUEST) ;
            else if (collabrify_req_pb.request_type == CollabrifyRequestType_PB.GET_FROM_BASE_FILE_REQUEST) ;
            else if (collabrify_req_pb.request_type == CollabrifyRequestType_PB.GET_LAST_EVENT_BY_PARTICIPANT_REQUEST) ;
            else if (collabrify_req_pb.request_type == CollabrifyRequestType_PB.GET_NOTIFICATION_ID_REQUEST) ;
            else if (collabrify_req_pb.request_type == CollabrifyRequestType_PB.GET_PARTICIPANT_REQUEST) ;
            else if (collabrify_req_pb.request_type == CollabrifyRequestType_PB.GET_SESSION_REQUEST) ;
            else if (collabrify_req_pb.request_type == CollabrifyRequestType_PB.LIST_ACCOUNTS_REQUEST) ;
            else if (collabrify_req_pb.request_type == CollabrifyRequestType_PB.LIST_SESSIONS_REQUEST)
            {
              Serializer.SerializeWithLengthPrefix<Request_ListSessions_PB>(ms2, (Request_ListSessions_PB)secondary_pb, PrefixStyle.Base128, 0);
              writeSecondObject(ms2, postStream);
            }
            else if (collabrify_req_pb.request_type == CollabrifyRequestType_PB.PREVENT_FURTHER_JOINS_REQUEST) ;
            else if (collabrify_req_pb.request_type == CollabrifyRequestType_PB.REMOVE_PARTICIPANT_REQUEST) ;
            else if (collabrify_req_pb.request_type == CollabrifyRequestType_PB.REQUEST_TYPE_NOT_SET) ;
            else if (collabrify_req_pb.request_type == CollabrifyRequestType_PB.UPDATE_NOTIFICATION_ID_REQUEST) ;
            else if (collabrify_req_pb.request_type == CollabrifyRequestType_PB.UPDATE_USER) ;
            else if (collabrify_req_pb.request_type == CollabrifyRequestType_PB.WARMUP_REQUEST) ;

            postStream.Close();

            request.BeginGetResponse(new AsyncCallback(GetResponseCallback), request);
          }
          catch (WebException e) { System.Diagnostics.Debug.WriteLine("  -- A WEB EXCEPTION OCCURED\n" + e.Message); }

        }

        private void writeSecondObject(MemoryStream ms, Stream postStream)
        {
          byte[] byteArr = ms.ToArray();
          postStream.Write(byteArr, 0, byteArr.Length);
          System.Diagnostics.Debug.WriteLine("writing ms2 to stream... size: " + byteArr.Length.ToString());
        }

        private void GetResponseCallback(IAsyncResult asynchronousResult)
        {
            try
            {
                HttpWebRequest request = (HttpWebRequest)asynchronousResult.AsyncState;
                System.Diagnostics.Debug.WriteLine("\t-- GOT REQUEST");

                HttpWebResponse response = (HttpWebResponse)request.EndGetResponse(asynchronousResult);
                System.Diagnostics.Debug.WriteLine("\t-- GOT RESPONSE\n");


                //System.Diagnostics.Debug.WriteLine("***RESPONSE\n Length: " + response.ContentLength.ToString());
                //System.Diagnostics.Debug.WriteLine("\n\n***RESPONSE\n URI: " + response.ResponseUri.ToString());
                //System.Diagnostics.Debug.WriteLine("\n\n***RESPONSE\n Status Description: " + response.StatusDescription.ToString());
                //System.Diagnostics.Debug.WriteLine("\n");

                string responseString = "FAIL";

                //to read server response 
                Stream streamResponse = response.GetResponseStream();
                try
                {
                  collabrify_resp_pb = Serializer.DeserializeWithLengthPrefix<CollabrifyResponse_PB>(streamResponse, PrefixStyle.Base128, 0);

                  responseString = collabrify_resp_pb.success_flag.ToString();

                  if (collabrify_req_pb.request_type == CollabrifyRequestType_PB.ADD_EVENT_REQUEST) ;
                  else if (collabrify_req_pb.request_type == CollabrifyRequestType_PB.ADD_PARTICIPANT_REQUEST) ;
                  else if (collabrify_req_pb.request_type == CollabrifyRequestType_PB.ADD_TO_BASE_FILE_REQUEST) ;
                  else if (collabrify_req_pb.request_type == CollabrifyRequestType_PB.CREATE_OR_GET_USER)
                  {
                    returned_secondary_pb = Serializer.DeserializeWithLengthPrefix<Response_CreateOrGetUser_PB>(streamResponse, PrefixStyle.Base128, 0);
                    responseString += "\nSession Name: " + ((Response_CreateOrGetUser_PB)returned_secondary_pb).user.first_name; 
                  }
                  else if (collabrify_req_pb.request_type == CollabrifyRequestType_PB.CREATE_SESSION_REQUEST)
                  {
                    returned_secondary_pb = Serializer.DeserializeWithLengthPrefix<Response_CreateSession_PB>(streamResponse, PrefixStyle.Base128, 0);
                    responseString += "\nSession Name: " + ((Response_CreateSession_PB)returned_secondary_pb).session.session_name;
                  }
                  else if (collabrify_req_pb.request_type == CollabrifyRequestType_PB.CREATE_SESSION_WITH_BASE_FILE_REQUEST) ;
                  else if (collabrify_req_pb.request_type == CollabrifyRequestType_PB.DELETE_ALL_SESSIONS_REQUEST) ;
                  else if (collabrify_req_pb.request_type == CollabrifyRequestType_PB.DELETE_OLD_SESSIONS_REQUEST) ;
                  else if (collabrify_req_pb.request_type == CollabrifyRequestType_PB.DELETE_SESSION_REQUEST) ;
                  else if (collabrify_req_pb.request_type == CollabrifyRequestType_PB.DELETE_USER) ;
                  else if (collabrify_req_pb.request_type == CollabrifyRequestType_PB.END_SESSION_REQUEST) ;
                  else if (collabrify_req_pb.request_type == CollabrifyRequestType_PB.GET_BASE_FILE_REQUEST) ;
                  else if (collabrify_req_pb.request_type == CollabrifyRequestType_PB.GET_CURRENT_ORDER_ID_REQUEST) ;
                  else if (collabrify_req_pb.request_type == CollabrifyRequestType_PB.GET_EVENT_BATCH_REQUEST) ;
                  else if (collabrify_req_pb.request_type == CollabrifyRequestType_PB.GET_EVENT_REQUEST) ;
                  else if (collabrify_req_pb.request_type == CollabrifyRequestType_PB.GET_FROM_BASE_FILE_REQUEST) ;
                  else if (collabrify_req_pb.request_type == CollabrifyRequestType_PB.GET_LAST_EVENT_BY_PARTICIPANT_REQUEST) ;
                  else if (collabrify_req_pb.request_type == CollabrifyRequestType_PB.GET_NOTIFICATION_ID_REQUEST) ;
                  else if (collabrify_req_pb.request_type == CollabrifyRequestType_PB.GET_PARTICIPANT_REQUEST) ;
                  else if (collabrify_req_pb.request_type == CollabrifyRequestType_PB.GET_SESSION_REQUEST) ;
                  else if (collabrify_req_pb.request_type == CollabrifyRequestType_PB.LIST_ACCOUNTS_REQUEST) ;
                  else if (collabrify_req_pb.request_type == CollabrifyRequestType_PB.LIST_SESSIONS_REQUEST)
                  {
                    returned_secondary_pb = Serializer.DeserializeWithLengthPrefix<Response_ListSessions_PB>(streamResponse, PrefixStyle.Base128, 0);
                    //responseString += "\nSession Name: " + ((Response_ListSessions_PB)returned_secondary_pb).session[0].session_name;
                  }
                  else if (collabrify_req_pb.request_type == CollabrifyRequestType_PB.PREVENT_FURTHER_JOINS_REQUEST) ;
                  else if (collabrify_req_pb.request_type == CollabrifyRequestType_PB.REMOVE_PARTICIPANT_REQUEST) ;
                  else if (collabrify_req_pb.request_type == CollabrifyRequestType_PB.REQUEST_TYPE_NOT_SET) ;
                  else if (collabrify_req_pb.request_type == CollabrifyRequestType_PB.UPDATE_NOTIFICATION_ID_REQUEST) ;
                  else if (collabrify_req_pb.request_type == CollabrifyRequestType_PB.UPDATE_USER) ;
                  else if (collabrify_req_pb.request_type == CollabrifyRequestType_PB.WARMUP_REQUEST) ;   
                }
                catch (Exception e)
                {
                    responseString = e.Message.ToString();
                    System.Diagnostics.Debug.WriteLine("EXCEPTION string\n" + e.Data + "\n -- \n" + e.StackTrace.ToString());
                }

                if (!collabrify_resp_pb.success_flag) responseString += "\nex type: " + collabrify_resp_pb.exception_type.ToString() + "\nmessage: " + collabrify_resp_pb.exception.message;
                System.Diagnostics.Debug.WriteLine("Success Flag: " + responseString);

                // Close the stream object
                streamResponse.Close();

                // Release the HttpWebResponse
                response.Close();

              //Dispatcher.BeginInvoke(() =>
                //{
                //    //Update UI here
                //  System.Diagnostics.Debug.WriteLine( responseString );
                //
                //});
            }
            catch (WebException e)
            {
                //Handle non success exception here  
                HttpWebRequest request = (HttpWebRequest)asynchronousResult.AsyncState;

                System.Diagnostics.Debug.WriteLine("responses suck");

            }
        }
    }
}
