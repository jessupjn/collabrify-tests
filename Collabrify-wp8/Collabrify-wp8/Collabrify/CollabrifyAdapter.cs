using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Collabrify_wp8.Collabrify
{
  public class CollabrifyAdapter : Collabrify.CollabrifyListener.CollabrifyErrorListener
  {
    public override void onError(CollabrifyException e)
    {

    }

    public override void onBaseFileUploadComplete(long baseFileSize)
    {

    }

    public override void onBaseFileReceived(Stream baseFile)
    {

    }

    public override void onReceiveEvent(long orderId, int submissionRegistrationId,
        String eventType, byte[] data, long elapsed)
    {

    }

    public override void onParticipantJoined(CollabrifyParticipant p)
    {

    }

    public override void onParticipantLeft(CollabrifyParticipant p)
    {

    }

    public override void onSessionEnd(long id)
    {

    }

    public override void onFurtherJoinsPrevented()
    {

    }
  }
}
