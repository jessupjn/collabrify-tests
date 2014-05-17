using System;
using System.Collections.Generic;
using System.Diagnostics;
using Microsoft.Phone.Controls;
using System.IO;
using System.Windows;
using System.Reflection;
using System.Runtime.InteropServices;

namespace Collabrify_wp8.Collabrify
{
  class ChannelAPI
  {
    private string LOG_TAG = "CHANNEL-API";

    // How long the token is valid for.
    // Reference:
    // https://developers.google.com/appengine/docs/java/channel/?csw=1#Java_Tokens_and_security
    private static readonly long TOKEN_VALID_LENGTH_MILLIS = 86400000; // 24 hours

    // Runs the javascript to connect to the ChannelAPI.
    private WebBrowser browser;
    
    // Listens for events back from the javascript running in the browser.
    private ChannelAPIEventListener listener;

    // When the object received the token. Every 2 hours te token must be refreshed and this is
    // what's used to reference if the token needs to be refreshed or not.
    private long mTimeTokenWasReceivedMillis;

    // If channel closes, this is set to true.
    private bool mChannelClosed;

    // The token we use to create the channel. Anyone with this token can listen in on the
    // channel so it should be treated as a secret.
    private string mToken;


    #region Constructor
    
    // CONSTRUCTOR
    public ChannelAPI(string token)
    {
      this.mToken = token;

      listener = new ChannelAPIEventListener();

      // Builds the browser object
      browser = new WebBrowser();
      browser.IsScriptEnabled = true;
      browser.IsGeolocationEnabled = true;
      browser.ScriptNotify += ScriptCallback;
      browser.LoadCompleted += delegate
      {
        connect();
      };

      string html = null;
      using (var s = Assembly.GetExecutingAssembly().GetManifestResourceStream("Collabrify_wp8.Resources.home.html"))
      using (var r = new StreamReader(s))
      html = r.ReadToEnd();

      browser.NavigateToString(html);
    } // CONSTRUCTOR
    #endregion

    #region Public Functions

    // ScriptCallback
    private void ScriptCallback(object sender, NotifyEventArgs e)
    {
      if(e.Value.Length < 8)
      {
        Debug.WriteLine(LOG_TAG + ":\nScriptNotify: " + e.Value);
        return;
      }
      switch( e.Value.Substring(0,8) )
      {
        case "onLoad::":
          pageLoaded();
          break;
        case "onOpen::":
          channelOpen();
          break;
        case "onClose:":
          channelClosed();
          break;
        case "errors::":
          channelError("","");
          break;
        case "message:":
          channelMessageReceived(e.Value.Substring(8));
          break;
        default:
          Debug.WriteLine(LOG_TAG + ":\nScriptNotify: " + e.Value);
          break;
      }
    }// ScriptCallback

    // connect
    public bool connect()
    {
      if (!isTokenValid()) return false;

      Debug.WriteLine(LOG_TAG + ": connecting using token: " + this.mToken);

      browser.InvokeScript("connectToServer", mToken);

      return true;
    } // connect

    // TODO: isTokenValid
    private bool isTokenValid()
    {
      return true;
    } // isTokenValid

    #endregion

    #region Private Functions
    // pageLoaded
    private void pageLoaded()
    {
      Debug.WriteLine(LOG_TAG + ": pageLoaded");
    }// pageLoaded

    // channelOpen
    private void channelOpen()
    {
      Debug.WriteLine(LOG_TAG + ": channelOpen");
      listener.onChannelAPIOpen();
    }// channelOpen

    // channelClosed
    private void channelClosed()
    {
      Debug.WriteLine(LOG_TAG + ": channelClosed");
      listener.onChannelAPIClosed();
    }// channelClosed

    // channelMessageReceived
    private void channelMessageReceived(string message)
    {
      Debug.WriteLine(LOG_TAG + ": channelMessageReceived");
      listener.onChannelAPIUpdateEvent();
    }// channelMessageReceived

    // channelError
    private void channelError(string code, string description)
    {
      Debug.WriteLine(LOG_TAG + ": channelError");
      listener.onChannelAPIError();
    } // channelError
    #endregion
  }

  // ChannelAPIEventListener
  class ChannelAPIEventListener
  {
    public ChannelAPIEventListener()
    {
    }

    public void onChannelAPIOpen()
    {

    }

    public void onChannelAPIClosed()
    {

    }

    public void onChannelAPIError()
    {

    }

    public void onChannelAPIUpdateEvent()
    {

    }
  } // ChannelAPIEventListener
}
