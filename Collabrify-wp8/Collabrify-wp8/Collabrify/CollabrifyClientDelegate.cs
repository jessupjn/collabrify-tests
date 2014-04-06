using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Collabrify_wp8.Collabrify
{
  public delegate void receivedEvent();
  public delegate void receivedBaseFileChunk();
  public delegate void uploadedBaseFileWithSize();
  public delegate void sessionEnded();
  public delegate void participantJoined();
  public delegate void participantLeft();
  public delegate void clientDidEnterBackground();

}
