using Collabrify_v2.CollabrifyProtocolBuffer;
using ProtoBuf;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;

namespace C
{
  public class HttpRequest_Warmup : httprequest__Object
  {
    public static readonly string servletExtension = new ServletUrlExtension_PB().url_for_warmup;

    // ---------------------------------------------------------------------------
    // ---------------------------------------------------------------------------

    /// <summary>
    /// baseURL
    ///
    /// makes a warmup request to the server and returns a response object in the form of RESPONSE_
    /// </summary> 
    public void make_request()
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
            System.Diagnostics.Debug.WriteLine("EXCEPTION THROWN \n" + ex.Message);
        }

    }

    public void getReqStream(IAsyncResult result)
    {
        try
        {
            HttpWebRequest request = (HttpWebRequest)result.AsyncState;
            Stream postStream = request.EndGetRequestStream(result);

            CollabrifyRequest_PB req_pb = new CollabrifyRequest_PB();
            req_pb.request_type = CollabrifyRequestType_PB.WARMUP_REQUEST;
            MemoryStream ms = new MemoryStream();
            Serializer.SerializeWithLengthPrefix<CollabrifyRequest_PB>(ms, req_pb, PrefixStyle.Base128, 0);

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
            catch (Exception e)
            {
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
            HttpWebRequest request = (HttpWebRequest)asynchronousResult.AsyncState;

            System.Diagnostics.Debug.WriteLine("responses suck");

        }
    }

    // ---------------------------------------------------------------------------

  }
}
