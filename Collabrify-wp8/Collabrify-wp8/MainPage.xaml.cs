using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Net;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using Collabrify_wp8.Resources;
using System.Threading;
using Collabrify_v2.CollabrifyProtocolBuffer;
using System.Runtime.Serialization;
using System.IO;
using ProtoBuf;
using Collabrify_wp8.Http_Requests;
using Collabrify_wp8.Collabrify;

namespace Collabrify_wp8
{
    public partial class MainPage : PhoneApplicationPage
    {
        private long q1, q2;

        private ListPickerItem RequestType = new ListPickerItem();

        // Constructor
        public MainPage()
        {
            InitializeComponent();
        }

        private void Button1_Click(object sender, RoutedEventArgs e)
        {
          //CollabrifyClient c = new CollabrifyClient("A", "A", "A", true);
            if (RequestType.Name == "Warmup")
            {
                HttpRequest_Warmup.make_request();
            }
            else if (RequestType.Name == "CreateSession")
            {
                HttpRequest_CreateSession.make_request();
            }
            else if (RequestType.Name == "ListSessions")
            {
                HttpRequest_ListSessions.make_request();
            }

        }

        public void getReqStream(IAsyncResult result)
        {
            try
            {
                HttpWebRequest request = (HttpWebRequest)result.AsyncState;
                Stream postStream = request.EndGetRequestStream(result);


                CollabrifyRequest_PB req_pb = new CollabrifyRequest_PB();
                req_pb.request_type = CollabrifyRequestType_PB.CREATE_SESSION_REQUEST;
                MemoryStream ms = new MemoryStream();
                Serializer.SerializeWithLengthPrefix<CollabrifyRequest_PB>(ms, req_pb, PrefixStyle.Base128, 0);

                Request_CreateSession_PB ls_pb = new Request_CreateSession_PB();
                ls_pb.account_gmail = "wp8-collabrify@umich.edu";
                ls_pb.access_token = "82763BDBCA";
                ls_pb.session_tag.Add("none");
                ls_pb.session_name = "this is a test session5";
                ls_pb.owner_display_name = "Jack";
                ls_pb.owner_gmail = "wp8-collabrify@umich.edu";
                ls_pb.owner_notification_id = "Jack";
                ls_pb.owner_notification_type = NotificationMediumType_PB.COLLABRIFY_CLOUD_CHANNEL;
                MemoryStream ms2 = new MemoryStream();
                Serializer.SerializeWithLengthPrefix<Request_CreateSession_PB>(ms2, ls_pb, PrefixStyle.Base128, 0);

                
                byte[] byteArr = ms.ToArray();
                postStream.Write(byteArr, 0, byteArr.Length);
                System.Diagnostics.Debug.WriteLine("writing ms... size: " + byteArr.Length.ToString());

                byteArr = ms2.ToArray();
                postStream.Write(byteArr, 0, byteArr.Length);
                System.Diagnostics.Debug.WriteLine("writing ms2... size: " + byteArr.Length.ToString());

                postStream.Close();

                request.BeginGetResponse(new AsyncCallback(GetResponseCallback), request);
            }
            catch (WebException e)
            {
                System.Diagnostics.Debug.WriteLine("web exception");
            }
            
        }

        private void GetResponseCallback(IAsyncResult asynchronousResult)
        {
            try
            {
                HttpWebRequest request = (HttpWebRequest)asynchronousResult.AsyncState;
                System.Diagnostics.Debug.WriteLine("\t-- GOT REQUEST");

                HttpWebResponse response = (HttpWebResponse)request.EndGetResponse(asynchronousResult);
                System.Diagnostics.Debug.WriteLine("\t-- GOT RESPONSE\n");


                System.Diagnostics.Debug.WriteLine("***RESPONSE\n Length: " + response.ContentLength.ToString());
                System.Diagnostics.Debug.WriteLine("\n\n***RESPONSE\n URI: " + response.ResponseUri.ToString());
                System.Diagnostics.Debug.WriteLine("\n\n***RESPONSE\n Status Description: " + response.StatusDescription.ToString());
                System.Diagnostics.Debug.WriteLine("\n");

                string responseString = "FAIL";

                CollabrifyResponse_PB resp_pb = new CollabrifyResponse_PB();
                Response_CreateSession_PB resp_list_pb = new Response_CreateSession_PB();

                //to read server response 
                Stream streamResponse = response.GetResponseStream();
                try
                {
                    resp_pb = Serializer.DeserializeWithLengthPrefix<CollabrifyResponse_PB>(streamResponse, PrefixStyle.Base128, 0);

                    responseString = resp_pb.success_flag.ToString();
                    responseString += "\n" + resp_pb.backend_version.ToString();

                    resp_list_pb = Serializer.DeserializeWithLengthPrefix<Response_CreateSession_PB>(streamResponse, PrefixStyle.Base128, 0);
                    if (resp_pb.success_flag) responseString += "\n" + resp_list_pb.session.session_name;
                    else
                    {
                      responseString += "\n" + resp_pb.exception.message;
                    }
                }
                catch(Exception e) {
                    responseString = e.Message.ToString();
                    System.Diagnostics.Debug.WriteLine("EXCEPTION string\n" + e.Data + "\n -- \n" + e.StackTrace.ToString());
                }

                System.Diagnostics.Debug.WriteLine("RESP string\n" + responseString);

                // Close the stream object
                streamResponse.Close();

                // Release the HttpWebResponse
                response.Close();

                Dispatcher.BeginInvoke(() =>
                    {
                        //Update UI here
                        ResponseTextBlock.Text = responseString;

                    });
            }
            catch (WebException e)
            {
                //Handle non success exception here  
                HttpWebRequest request = (HttpWebRequest)asynchronousResult.AsyncState;

                System.Diagnostics.Debug.WriteLine("responses suck");

            }       
        }

        private void Button2_Click(object sender, RoutedEventArgs e)
        {
            // clear response text
            ResponseTextBlock.Text = "";
        }

        private void OptionSelection_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ListPickerItem lpi = (sender as ListPicker).SelectedItem as ListPickerItem;
            RequestType = lpi;
        }

    }
}