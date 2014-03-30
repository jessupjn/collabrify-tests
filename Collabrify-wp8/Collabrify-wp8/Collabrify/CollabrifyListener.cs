using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Collabrify_v2.CollabrifyProtocolBuffer;
using Collabrify_wp8.Http_Requests;

namespace Collabrify_wp8.Collabrify
{
    public delegate void CollabrifyEventListener(object sender, CollabrifyEventArgs e);
    public delegate void CreateSessionListener(CreateSession_Args e);

    public class CollabrifyEventArgs : EventArgs
    {
      public CollabrifyRequestType_PB type;
      public CollabrifyResponse_PB response;
      public object specificResponsePB;

      public CollabrifyEventArgs(CollabrifyResponse_PB response_pb, object specific_response_pb, CollabrifyRequestType_PB request_type)
      {
        type = request_type;
        specificResponsePB = specific_response_pb;
        response = response_pb;
      }

    }

    public class CreateSession_Args : EventArgs
    {
      private Response_CreateSession_PB returned_data;
      public CreateSession_Args(object _returned_data)
      {
        returned_data = _returned_data as Response_CreateSession_PB;
      }
      public Response_CreateSession_PB getReturnedData() { return returned_data; }
    }








    public class CollabrifyListener
    {
      public event CollabrifyEventListener HttpReturned;
      public event CollabrifyEventListener ReturnHandled;



      public CollabrifyListener()
      {
        
      }
    }
}
