using Collabrify_v2.CollabrifyProtocolBuffer;
using Collabrify_wp8.Collabrify;
using ProtoBuf;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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

        public object response_object_pb;
        public object response_specific_pb;
        public CollabrifyRequestType_PB response_type;
        public event ChangedEventHander Changed;
        protected virtual void OnChanged(EventArgs e)
        {
          try { Changed(this, EventArgs.Empty); }
          catch { Debug.WriteLine("OnChanged exception..."); }
        }

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
            }
            else if (collabrify_req_pb.request_type == CollabrifyRequestType_PB.ADD_PARTICIPANT_REQUEST)
            {
                Serializer.SerializeWithLengthPrefix<Request_AddParticipant_PB>(ms2, (Request_AddParticipant_PB)secondary_pb, PrefixStyle.Base128, 0);
            }
            else if (collabrify_req_pb.request_type == CollabrifyRequestType_PB.ADD_TO_BASE_FILE_REQUEST)
            {
                Serializer.SerializeWithLengthPrefix<Request_AddToBaseFile_PB>(ms2, (Request_AddToBaseFile_PB)secondary_pb, PrefixStyle.Base128, 0);
            }
            else if (collabrify_req_pb.request_type == CollabrifyRequestType_PB.CREATE_OR_GET_USER)
            {
                Serializer.SerializeWithLengthPrefix<Request_CreateOrGetUser_PB>(ms2, (Request_CreateOrGetUser_PB)secondary_pb, PrefixStyle.Base128, 0);
            }
            else if (collabrify_req_pb.request_type == CollabrifyRequestType_PB.CREATE_SESSION_REQUEST)
            {
              Serializer.SerializeWithLengthPrefix<Request_CreateSession_PB>(ms2, (Request_CreateSession_PB)secondary_pb, PrefixStyle.Base128, 0);
            }
            else if (collabrify_req_pb.request_type == CollabrifyRequestType_PB.CREATE_SESSION_WITH_BASE_FILE_REQUEST)
            {
                Serializer.SerializeWithLengthPrefix<Request_CreateSessionWithBaseFile_PB>(ms2, (Request_CreateSessionWithBaseFile_PB)secondary_pb, PrefixStyle.Base128, 0);
            }
            else if (collabrify_req_pb.request_type == CollabrifyRequestType_PB.DELETE_ALL_SESSIONS_REQUEST)
            {
                Serializer.SerializeWithLengthPrefix<Request_DeleteAllSessions_PB>(ms2, (Request_DeleteAllSessions_PB)secondary_pb, PrefixStyle.Base128, 0);
            }
            else if (collabrify_req_pb.request_type == CollabrifyRequestType_PB.DELETE_OLD_SESSIONS_REQUEST)
            {
                // TODO: NO DELETE USER PB
                // Serializer.SerializeWithLengthPrefix<Request_DeleteAllSessions_PB>(ms2, (Request_DeleteAllSessions_PB)secondary_pb, PrefixStyle.Base128, 0);
            }
            else if (collabrify_req_pb.request_type == CollabrifyRequestType_PB.DELETE_SESSION_REQUEST)
            {
                Serializer.SerializeWithLengthPrefix<Request_DeleteSession_PB>(ms2, (Request_DeleteSession_PB)secondary_pb, PrefixStyle.Base128, 0);
            }
            else if (collabrify_req_pb.request_type == CollabrifyRequestType_PB.DELETE_USER)
            {
                // TODO: NO DELETE USER PB
                // Serializer.SerializeWithLengthPrefix<Request_DeleteSession_PB>(ms2, (Request_DeleteSession_PB)secondary_pb, PrefixStyle.Base128, 0);
            }
            else if (collabrify_req_pb.request_type == CollabrifyRequestType_PB.END_SESSION_REQUEST)
            {
                Serializer.SerializeWithLengthPrefix<Request_EndSession_PB>(ms2, (Request_EndSession_PB)secondary_pb, PrefixStyle.Base128, 0);
            }
            else if (collabrify_req_pb.request_type == CollabrifyRequestType_PB.GET_BASE_FILE_REQUEST)
            {
                Serializer.SerializeWithLengthPrefix<Request_GetBaseFile_PB>(ms2, (Request_GetBaseFile_PB)secondary_pb, PrefixStyle.Base128, 0);
            }
            else if (collabrify_req_pb.request_type == CollabrifyRequestType_PB.GET_CURRENT_ORDER_ID_REQUEST)
            {
                Serializer.SerializeWithLengthPrefix<Request_GetCurrentOrderID_PB>(ms2, (Request_GetCurrentOrderID_PB)secondary_pb, PrefixStyle.Base128, 0);
            }
            else if (collabrify_req_pb.request_type == CollabrifyRequestType_PB.GET_EVENT_BATCH_REQUEST)
            {
                Serializer.SerializeWithLengthPrefix<Request_GetEventBatch_PB>(ms2, (Request_GetEventBatch_PB)secondary_pb, PrefixStyle.Base128, 0);
            }
            else if (collabrify_req_pb.request_type == CollabrifyRequestType_PB.GET_EVENT_REQUEST)
            {
                Serializer.SerializeWithLengthPrefix<Request_GetEvent_PB>(ms2, (Request_GetEvent_PB)secondary_pb, PrefixStyle.Base128, 0);
            }
            else if (collabrify_req_pb.request_type == CollabrifyRequestType_PB.GET_FROM_BASE_FILE_REQUEST)
            {
                Serializer.SerializeWithLengthPrefix<Request_GetFromBaseFile_PB>(ms2, (Request_GetFromBaseFile_PB)secondary_pb, PrefixStyle.Base128, 0);
            }
            else if (collabrify_req_pb.request_type == CollabrifyRequestType_PB.GET_LAST_EVENT_BY_PARTICIPANT_REQUEST)
            {
                Serializer.SerializeWithLengthPrefix<Request_GetLastEventByParticipant_PB>(ms2, (Request_GetLastEventByParticipant_PB)secondary_pb, PrefixStyle.Base128, 0);
            }
            else if (collabrify_req_pb.request_type == CollabrifyRequestType_PB.GET_NOTIFICATION_ID_REQUEST)
            {
                Serializer.SerializeWithLengthPrefix<Request_GetNotificationID_PB>(ms2, (Request_GetNotificationID_PB)secondary_pb, PrefixStyle.Base128, 0);
            }
            else if (collabrify_req_pb.request_type == CollabrifyRequestType_PB.GET_PARTICIPANT_REQUEST)
            {
                Serializer.SerializeWithLengthPrefix<Request_GetParticipant_PB>(ms2, (Request_GetParticipant_PB)secondary_pb, PrefixStyle.Base128, 0);
            }
            else if (collabrify_req_pb.request_type == CollabrifyRequestType_PB.GET_SESSION_REQUEST)
            {
                Serializer.SerializeWithLengthPrefix<Request_GetSession_PB>(ms2, (Request_GetSession_PB)secondary_pb, PrefixStyle.Base128, 0);
            }
            else if (collabrify_req_pb.request_type == CollabrifyRequestType_PB.LIST_ACCOUNTS_REQUEST)
            {
                Serializer.SerializeWithLengthPrefix<Request_ListAccounts_PB>(ms2, (Request_ListAccounts_PB)secondary_pb, PrefixStyle.Base128, 0);
            }
            else if (collabrify_req_pb.request_type == CollabrifyRequestType_PB.LIST_SESSIONS_REQUEST)
            {
                Serializer.SerializeWithLengthPrefix<Request_ListSessions_PB>(ms2, (Request_ListSessions_PB)secondary_pb, PrefixStyle.Base128, 0);
            }
            else if (collabrify_req_pb.request_type == CollabrifyRequestType_PB.PREVENT_FURTHER_JOINS_REQUEST)
            {
                Serializer.SerializeWithLengthPrefix<Request_PreventFurtherJoins_PB>(ms2, (Request_PreventFurtherJoins_PB)secondary_pb, PrefixStyle.Base128, 0);
            }
            else if (collabrify_req_pb.request_type == CollabrifyRequestType_PB.REMOVE_PARTICIPANT_REQUEST)
            {
                Serializer.SerializeWithLengthPrefix<Request_RemoveParticipant_PB>(ms2, (Request_RemoveParticipant_PB)secondary_pb, PrefixStyle.Base128, 0);
            }
            else if (collabrify_req_pb.request_type == CollabrifyRequestType_PB.REQUEST_TYPE_NOT_SET)
            { }
            else if (collabrify_req_pb.request_type == CollabrifyRequestType_PB.UPDATE_NOTIFICATION_ID_REQUEST)
            {
                Serializer.SerializeWithLengthPrefix<Request_UpdateNotificationID_PB>(ms2, (Request_UpdateNotificationID_PB)secondary_pb, PrefixStyle.Base128, 0);
            }
            else if (collabrify_req_pb.request_type == CollabrifyRequestType_PB.UPDATE_USER)
            {
                Serializer.SerializeWithLengthPrefix<Request_UpdateUser>(ms2, (Request_UpdateUser)secondary_pb, PrefixStyle.Base128, 0);
            }
            else if (collabrify_req_pb.request_type == CollabrifyRequestType_PB.WARMUP_REQUEST)
            {
                Serializer.SerializeWithLengthPrefix<Request_Warmup_PB>(ms2, (Request_Warmup_PB)secondary_pb, PrefixStyle.Base128, 0);
            }

            // if the second memory stream has data in it - add it to the request.
            if (ms2.Length > 0) writeSecondObject(ms2, postStream);

            // close the request stream.
            postStream.Close();

            // get our response.
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

                string responseString = "FAIL";

                //to read server response 
                Stream streamResponse = response.GetResponseStream();
                try
                {
                  collabrify_resp_pb = Serializer.DeserializeWithLengthPrefix<CollabrifyResponse_PB>(streamResponse, PrefixStyle.Base128, 0);

                  responseString = collabrify_resp_pb.success_flag.ToString();
                  returned_secondary_pb = null;
                  if (collabrify_req_pb.request_type == CollabrifyRequestType_PB.ADD_EVENT_REQUEST)
                  {
                      returned_secondary_pb = Serializer.DeserializeWithLengthPrefix<Response_AddEvent_PB>(streamResponse, PrefixStyle.Base128, 0);
                  }
                  else if (collabrify_req_pb.request_type == CollabrifyRequestType_PB.ADD_PARTICIPANT_REQUEST)
                  {
                      returned_secondary_pb = Serializer.DeserializeWithLengthPrefix<Response_AddParticipant_PB>(streamResponse, PrefixStyle.Base128, 0);
                  }
                  else if (collabrify_req_pb.request_type == CollabrifyRequestType_PB.ADD_TO_BASE_FILE_REQUEST)
                  {
                      returned_secondary_pb = Serializer.DeserializeWithLengthPrefix<Response_AddToBaseFile_PB>(streamResponse, PrefixStyle.Base128, 0);
                  }
                  else if (collabrify_req_pb.request_type == CollabrifyRequestType_PB.CREATE_OR_GET_USER)
                  {
                    returned_secondary_pb = Serializer.DeserializeWithLengthPrefix<Response_CreateOrGetUser_PB>(streamResponse, PrefixStyle.Base128, 0);
                  }
                  else if (collabrify_req_pb.request_type == CollabrifyRequestType_PB.CREATE_SESSION_REQUEST)
                  {
                    returned_secondary_pb = Serializer.DeserializeWithLengthPrefix<Response_CreateSession_PB>(streamResponse, PrefixStyle.Base128, 0);
                  }
                  else if (collabrify_req_pb.request_type == CollabrifyRequestType_PB.CREATE_SESSION_WITH_BASE_FILE_REQUEST)
                  {
                      returned_secondary_pb = Serializer.DeserializeWithLengthPrefix<Response_CreateSessionWithBaseFile_PB>(streamResponse, PrefixStyle.Base128, 0);
                  }
                  else if (collabrify_req_pb.request_type == CollabrifyRequestType_PB.DELETE_ALL_SESSIONS_REQUEST)
                  {
                      returned_secondary_pb = Serializer.DeserializeWithLengthPrefix<Response_DeleteAllSessions_PB>(streamResponse, PrefixStyle.Base128, 0);
                  }
                  else if (collabrify_req_pb.request_type == CollabrifyRequestType_PB.DELETE_OLD_SESSIONS_REQUEST)
                  {
                      // TODO: DELETE OLD SESSIONS??
                      // returned_secondary_pb = Serializer.DeserializeWithLengthPrefix<Response_DeleteAllSessions_PB>(streamResponse, PrefixStyle.Base128, 0);
                  }
                  else if (collabrify_req_pb.request_type == CollabrifyRequestType_PB.DELETE_SESSION_REQUEST)
                  {
                      returned_secondary_pb = Serializer.DeserializeWithLengthPrefix<Response_DeleteSession_PB>(streamResponse, PrefixStyle.Base128, 0);
                  }
                  else if (collabrify_req_pb.request_type == CollabrifyRequestType_PB.DELETE_USER)
                  {
                      // TODO: DELETE USER??
                      // returned_secondary_pb = Serializer.DeserializeWithLengthPrefix<Response_DeleteSession_PB>(streamResponse, PrefixStyle.Base128, 0);
                  }
                  else if (collabrify_req_pb.request_type == CollabrifyRequestType_PB.END_SESSION_REQUEST)
                  {
                      returned_secondary_pb = Serializer.DeserializeWithLengthPrefix<Response_EndSession_PB>(streamResponse, PrefixStyle.Base128, 0);
                  }
                  else if (collabrify_req_pb.request_type == CollabrifyRequestType_PB.GET_BASE_FILE_REQUEST)
                  {
                      returned_secondary_pb = Serializer.DeserializeWithLengthPrefix<Response_GetBaseFile_PB>(streamResponse, PrefixStyle.Base128, 0);
                  }
                  else if (collabrify_req_pb.request_type == CollabrifyRequestType_PB.GET_CURRENT_ORDER_ID_REQUEST)
                  {
                      returned_secondary_pb = Serializer.DeserializeWithLengthPrefix<Response_GetCurrentOrderID_PB>(streamResponse, PrefixStyle.Base128, 0);
                  }
                  else if (collabrify_req_pb.request_type == CollabrifyRequestType_PB.GET_EVENT_BATCH_REQUEST)
                  {
                      returned_secondary_pb = Serializer.DeserializeWithLengthPrefix<Response_GetEventBatch_PB>(streamResponse, PrefixStyle.Base128, 0);
                  }
                  else if (collabrify_req_pb.request_type == CollabrifyRequestType_PB.GET_EVENT_REQUEST)
                  {
                      returned_secondary_pb = Serializer.DeserializeWithLengthPrefix<Response_GetEvent_PB>(streamResponse, PrefixStyle.Base128, 0);
                  }
                  else if (collabrify_req_pb.request_type == CollabrifyRequestType_PB.GET_FROM_BASE_FILE_REQUEST)
                  {
                      returned_secondary_pb = Serializer.DeserializeWithLengthPrefix<Response_GetFromBaseFile_PB>(streamResponse, PrefixStyle.Base128, 0);
                  }
                  else if (collabrify_req_pb.request_type == CollabrifyRequestType_PB.GET_LAST_EVENT_BY_PARTICIPANT_REQUEST)
                  {
                      returned_secondary_pb = Serializer.DeserializeWithLengthPrefix<Response_GetLastEventByParticipant_PB>(streamResponse, PrefixStyle.Base128, 0);
                  }
                  else if (collabrify_req_pb.request_type == CollabrifyRequestType_PB.GET_NOTIFICATION_ID_REQUEST)
                  {
                      returned_secondary_pb = Serializer.DeserializeWithLengthPrefix<Response_GetNotificationID_PB>(streamResponse, PrefixStyle.Base128, 0);
                  }
                  else if (collabrify_req_pb.request_type == CollabrifyRequestType_PB.GET_PARTICIPANT_REQUEST)
                  {
                      returned_secondary_pb = Serializer.DeserializeWithLengthPrefix<Response_GetParticipant_PB>(streamResponse, PrefixStyle.Base128, 0);
                  }
                  else if (collabrify_req_pb.request_type == CollabrifyRequestType_PB.GET_SESSION_REQUEST)
                  {
                      returned_secondary_pb = Serializer.DeserializeWithLengthPrefix<Response_GetSession_PB>(streamResponse, PrefixStyle.Base128, 0);
                  }
                  else if (collabrify_req_pb.request_type == CollabrifyRequestType_PB.LIST_ACCOUNTS_REQUEST)
                  {
                      returned_secondary_pb = Serializer.DeserializeWithLengthPrefix<Response_ListAccounts_PB>(streamResponse, PrefixStyle.Base128, 0);
                  }
                  else if (collabrify_req_pb.request_type == CollabrifyRequestType_PB.LIST_SESSIONS_REQUEST)
                  {
                    returned_secondary_pb = Serializer.DeserializeWithLengthPrefix<Response_ListSessions_PB>(streamResponse, PrefixStyle.Base128, 0);
                  }
                  else if (collabrify_req_pb.request_type == CollabrifyRequestType_PB.PREVENT_FURTHER_JOINS_REQUEST)
                  {
                      returned_secondary_pb = Serializer.DeserializeWithLengthPrefix<Response_PreventFurtherJoins_PB>(streamResponse, PrefixStyle.Base128, 0);
                  }
                  else if (collabrify_req_pb.request_type == CollabrifyRequestType_PB.REMOVE_PARTICIPANT_REQUEST)
                  {
                      returned_secondary_pb = Serializer.DeserializeWithLengthPrefix<Response_RemoveParticipant_PB>(streamResponse, PrefixStyle.Base128, 0);
                  }
                  else if (collabrify_req_pb.request_type == CollabrifyRequestType_PB.REQUEST_TYPE_NOT_SET)
                  { }
                  else if (collabrify_req_pb.request_type == CollabrifyRequestType_PB.UPDATE_NOTIFICATION_ID_REQUEST)
                  {
                      returned_secondary_pb = Serializer.DeserializeWithLengthPrefix<Response_UpdateNotificationID_PB>(streamResponse, PrefixStyle.Base128, 0);
                  }
                  else if (collabrify_req_pb.request_type == CollabrifyRequestType_PB.UPDATE_USER)
                  {
                      returned_secondary_pb = Serializer.DeserializeWithLengthPrefix<Response_UpdateUser>(streamResponse, PrefixStyle.Base128, 0);
                  }
                  else if (collabrify_req_pb.request_type == CollabrifyRequestType_PB.WARMUP_REQUEST)
                  {
                      returned_secondary_pb = Serializer.DeserializeWithLengthPrefix<Response_Warmup_PB>(streamResponse, PrefixStyle.Base128, 0);
                  }

                  System.Diagnostics.Debug.WriteLine("\t~ Before Assignments\n");

                  // ========================================================
                  // sets information that is used by the client upon return
                  response_type = collabrify_req_pb.request_type;
                  response_object_pb = collabrify_resp_pb;
                  response_specific_pb = returned_secondary_pb;
                  System.Diagnostics.Debug.WriteLine("\t~ Before On Changed\n");

                  //OnChanged(EventArgs.Empty);
                  // ========================================================
                  System.Diagnostics.Debug.WriteLine("\t~ After On Changed\n");

                }
                catch (Exception e)
                {
                  System.Diagnostics.Debug.WriteLine("EXCEPTION string\nMessage: " + e.Message.ToString() + "\ndata: " + e.Data.ToString() + "\n -- \n" + e.StackTrace.ToString());
                }

                // Close the stream object
                streamResponse.Close();

                // Release the HttpWebResponse
                response.Close();

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
