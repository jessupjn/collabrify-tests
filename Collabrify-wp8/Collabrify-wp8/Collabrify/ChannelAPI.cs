using Microsoft.Phone.Controls;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Windows;
using System.Threading.Tasks;

namespace Collabrify_wp8.Collabrify
{
  class ChannelAPI
  {
    public static readonly string CHANNEL_API_JAVASCRIPT_START = "<html><head></head><body><script type=\"text/javascript\" src=\"";
    public static readonly string CHANNEL_API_JAVASCRIPT_END = "\"></script><script language=\"javascript\">function closeConnection(){if(socket){socket.close()}}function connectToServer(a){channel=new goog.appengine.Channel(a);socket=channel.open();socket.onopen=onOpen;socket.onmessage=onMessage;socket.onerror=onError;socket.onClose=onClose}function onError(a){window.channelAPI.channelError(a.code,a.description)}function onMessage(a){window.channelAPI.channelMessageReceived(a.data)}function onClose(){window.channelAPI.channelClosed()}function onOpen(){window.channelAPI.channelOpen()}var channel;var socket;window.onload=function(){window.channelAPI.pageLoaded()}</script></body></html>";
    public static readonly string CHANNEL_API_JAVASCRIPT_SOURCE = "https://talkgadget.google.com/talkgadget/channel.js";

    WebBrowser browser;
    
    public ChannelAPI()
    {
      browser = new WebBrowser();
      browser.IsScriptEnabled = true;
      //browser.DocumentText = CHANNEL_API_JAVASCRIPT_START + CHANNEL_API_JAVASCRIPT_SOURCE + CHANNEL_API_JAVASCRIPT_END;
    }
  }
}
