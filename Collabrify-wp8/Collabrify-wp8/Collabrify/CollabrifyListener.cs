using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Collabrify_v2.CollabrifyProtocolBuffer;
using Collabrify_wp8.Http_Requests;

namespace Collabrify_wp8.Collabrify
{
    // Delegates that correspond to the HTTP requests returning and the completion handlers that will be called after a function has
    // returned as well as events from the Channel API.
    public delegate void CollabrifyEventListener(CollabrifyEventArgs e);
    public delegate void ChannelEventListener(ChannelEventArgs e);
    public delegate void CompletionHandler(CollabrifyClient c);
    public delegate void ListSessionsCompletionHandler(List<CollabrifySession> response);
    public delegate void ReceivedEvent();
    public delegate void ReceivedBaseFileChunk();
    public delegate void UploadedBaseFileWithSize();
    public delegate void ParticipantJoined();
    public delegate void ParticipantLeft();
    public delegate void SessionEnded();
    public delegate void ClientDidEnterBackground();

    // ------------------------------------------------------------------------------
    // ------------------------------------------------------------------------------

    public class CollabrifyEventArgs : EventArgs
    {
      public CollabrifyRequestType_PB type;
      public CollabrifyResponse_PB response;
      public object specificResponsePB;
      public object trailData;

      public CollabrifyEventArgs(CollabrifyResponse_PB response_pb, object specific_response_pb, CollabrifyRequestType_PB request_type)
      {
        type = request_type;
        specificResponsePB = specific_response_pb;
        response = response_pb;
      } // CTOR

    } // CollabrifyEventArgs

    // ------------------------------------------------------------------------------
    // ------------------------------------------------------------------------------

    public class ChannelEventArgs : EventArgs
    {
      public NotificationMessageType_PB type;
      public CollabrifyNotification_PB response;
      public object specificResponsePB;

      public ChannelEventArgs(CollabrifyNotification_PB response_pb, object specific_response_pb)
      {
        if (response_pb != null)
        {
          response = response_pb;
          type = response_pb.notification_message_type;
          specificResponsePB = specific_response_pb;
        }
      } // CTOR

    } // ChannelEventArgs

    // ------------------------------------------------------------------------------
    // ------------------------------------------------------------------------------
}
