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
using TestClient.Resources;
using System.Threading;
using Collabrify_v2.CollabrifyProtocolBuffer;
using System.Runtime.Serialization;
using System.IO;
using ProtoBuf;

namespace TestClient
{
    public partial class MainPage : PhoneApplicationPage
    {
        private long q1, q2;

        // Constructor
        public MainPage()
        {
            InitializeComponent();
        }

        private void Button1_Click(object sender, RoutedEventArgs e)
        {
            HttpWebRequest request = HttpWebRequest.CreateHttp("http://collabrify-cloud.appspot.com/request");
            request.ContentType = "application/x-www-form-urlencoded";
            request.Method = "POST";
            request.Credentials = new NetworkCredential("wp8-collabrify@umich.edu", "82763BDBCA");

            try
            {
                request.BeginGetRequestStream(new AsyncCallback(getReqStream), request);
            }
            catch (WebException ex)
            {
                // generic error handling
                TextBlock1.Text = "EXCEPTION THROWN \n" + ex.Message;
            }

        }

        public void getReqStream(IAsyncResult result)
        {
            try
            {
                HttpWebRequest request = (HttpWebRequest)result.AsyncState;
                Stream postStream = request.EndGetRequestStream(result);
//ls_pb.account_gmail = "wp8-collabrify@umich.edu";
                //ls_pb.access_token = "82763BDBCA";


                CollabrifyRequest_PB req_pb = new CollabrifyRequest_PB();
                req_pb.request_type = CollabrifyRequestType_PB.WARMUP_REQUEST;
                MemoryStream ms = new MemoryStream();
                Serializer.SerializeWithLengthPrefix<CollabrifyRequest_PB>(ms, req_pb, PrefixStyle.Base128, 0);
                byte[] byteArr = ms.ToArray();
                postStream.Write(byteArr, 0, byteArr.Length);
                postStream.Close();


                System.Diagnostics.Debug.WriteLine("size 1: " + byteArr.Length);

                request.BeginGetResponse(new AsyncCallback(GetResponseCallback), request);
            }
            catch (WebException e)
            { 
            }
            
        }

        private void GetResponseCallback(IAsyncResult asynchronousResult)
        {
            try
            {
                HttpWebRequest request = (HttpWebRequest)asynchronousResult.AsyncState;
                HttpWebResponse response = (HttpWebResponse)request.EndGetResponse(asynchronousResult);

                System.Diagnostics.Debug.WriteLine("***RESPONSE\n Length: " + response.ContentLength.ToString());
                System.Diagnostics.Debug.WriteLine("\n\n***RESPONSE\n URI: " + response.ResponseUri.ToString());
                System.Diagnostics.Debug.WriteLine("\n\n***RESPONSE\n Status Description: " + response.StatusDescription.ToString());
                System.Diagnostics.Debug.WriteLine("\n");

                string responseString = "FAIL";

                CollabrifyResponse_PB resp_pb = new CollabrifyResponse_PB();

                //to read server responce 
                Stream streamResponse = response.GetResponseStream();
                try
                {
                    //resp_pb = Serializer.DeserializeItems<CollabrifyResponse_PB>(streamResponse, PrefixStyle.Fixed32, 0);
                    System.Diagnostics.Debug.WriteLine("Before deserializing\n");
                    resp_pb = Serializer.DeserializeWithLengthPrefix<CollabrifyResponse_PB>(streamResponse, PrefixStyle.Base128, 0);
                    System.Diagnostics.Debug.WriteLine("after deserializing\n");
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
                        TextBlock1.Text = responseString;

                    });
            }
            catch (WebException e)
            {
                //Handle non success exception here                
            }       
        }

        private void Button2_Click(object sender, RoutedEventArgs e)
        {
            TextBlock1.Text = "Press the button to send a request to the server.";
        }

    }
}