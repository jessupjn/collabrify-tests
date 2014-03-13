using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using TestClient.Resources;
using System.Threading;
using System.Net;
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
            HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create("http://collabrify-cloud.appspot.com");

            NetworkCredential cred = new NetworkCredential();
            cred.UserName = "wp8-collabrify@umich.edu";
            cred.Password = "82763BDBCA";

            // unsure if we need credentials
            request.Credentials = cred;
            request.Method = "POST";
            request.ContentType = "application/x-www-form-urlencoded";


            
            request.BeginGetRequestStream(new AsyncCallback(GetCallback), request);

        }

        public void GetCallback(IAsyncResult result)
        {
            try
            {
                HttpWebRequest request = (HttpWebRequest)result.AsyncState;
                System.IO.Stream postStream = request.EndGetRequestStream(result);

                CollabrifyRequest_PB req_pb = new CollabrifyRequest_PB();
                Request_ListSessions_PB ls_pb = new Request_ListSessions_PB();
                req_pb.request_type = CollabrifyRequestType_PB.WARMUP_REQUEST;
                ls_pb.account_gmail = "wp8-collabrify@umich.edu";
                ls_pb.access_token = "82763BDBCA";

                var ms = new MemoryStream();
                var ms2 = new MemoryStream();
                //Serializer.Serialize<CollabrifyRequest_PB>(ms, req_pb);
                Serializer.SerializeWithLengthPrefix<CollabrifyRequest_PB>(ms, req_pb, PrefixStyle.None);
                //Serializer.Serialize<Request_ListSessions_PB>(ms2, ls_pb);


                byte[] byteArr = ms.ToArray();
                q1 = ms.Length;
                //byteArr.Concat( ms2.ToArray() );
                //q2 = ms2.Length;

                System.Diagnostics.Debug.WriteLine("size 1: " + q1.ToString());
                //System.Diagnostics.Debug.WriteLine("size 2: " + q2.ToString());

                //your binary stream to upload
                postStream.Write(byteArr, 0, byteArr.Length);
                postStream.Close();

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

                //to read server responce 
                Stream streamResponse = response.GetResponseStream();

                // ===============================
                // HERE IS WHERE WE WANT TO DESERIALIZE THE STREAM
                // AHHH
                // ===============================
                //CollabrifyResponse_PB resp_pb = Serializer.Deserialize<CollabrifyResponse_PB>(streamResponse);
                CollabrifyResponse_PB resp_pb = Serializer.DeserializeWithLengthPrefix<CollabrifyResponse_PB>(streamResponse, PrefixStyle.None);
                

                System.Diagnostics.Debug.WriteLine("RESP\n" + streamResponse);


                System.Diagnostics.Debug.WriteLine(streamResponse.Length);

                string responseString = "FAIL";
                if( resp_pb.success_flag ) responseString = "SUCCESS";

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