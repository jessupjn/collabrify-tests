using System;
using System.Collections.Generic;
using System.Diagnostics;
using Microsoft.Phone.Controls;
using System.IO;
using System.Windows;
using System.Reflection;
using System.Runtime.InteropServices;
using Windows.System.Threading;
using System.Threading;

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

    // an event that fires whenever a notification arrives from the channel.
    public event CollabrifyEventListener channelNotification;

    Thread t;

    #region Constructor
    
    // CONSTRUCTOR
    public ChannelAPI()
    { 
      Debug.WriteLine(LOG_TAG + ": building ChannelAPI.");
      // set default variables and token.
      listener = new ChannelAPIEventListener();
      mChannelClosed = true;

      // Builds the browser object
      try
      {
        browser = new WebBrowser();
        browser.IsScriptEnabled = true;
        browser.ScriptNotify += ScriptCallback;
        browser.LoadCompleted += delegate
        {
        };
      }
      catch( Exception ex)
      {
        Debug.WriteLine(LOG_TAG + ": " + ex.Source);
        Debug.WriteLine("\t" + ex.Message);
      }

      string html = null;
      using (var s = Assembly.GetExecutingAssembly().GetManifestResourceStream("Collabrify_wp8.Resources.home.html"))
      using (var r = new StreamReader(s))
      html = r.ReadToEnd();

      browser.NavigateToString(html);

    } // CONSTRUCTOR

    #endregion

    #region Private Functions

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
          channelError(e.Value.Substring(8));
          break;
        case "message:":
          channelMessageReceived(e.Value.Substring(8));
          break;
        default:
          Debug.WriteLine(LOG_TAG + ":\nScriptNotify: " + e.Value);
          break;
      }
    }// ScriptCallback

    public bool connect(string token)
    {

      setToken(token);

      if (!isTokenValid())
      {
        Debug.WriteLine(LOG_TAG + ": Token is not valid.");
        return false;
      }

      try
      {
        Deployment.Current.Dispatcher.BeginInvoke(delegate
        {
          Debug.WriteLine(LOG_TAG + ": attempting to connect with token:" + token);
          this.browser.InvokeScript("connectToServer", mToken);
        });
      }
      catch (Exception e)
      {
        Debug.WriteLine(LOG_TAG + ": " + e.Source);
        Debug.WriteLine("\t" + e.Message);
      }
      

      return true;
    } // connect

    // setToken
    private bool setToken(string token)
    {
      if (token != null && !token.Equals(""))
      {
        this.mToken = token;
        this.mTimeTokenWasReceivedMillis = DateTime.Now.Ticks / TimeSpan.TicksPerMillisecond;
        return true;
      }
      Debug.WriteLine(LOG_TAG + ": setToken - token cannot be null or empty");
      return false;
    }// setToken

    // isTokenValid
    private bool isTokenValid()
    {
      long currentTime = DateTime.Now.Ticks / TimeSpan.TicksPerMillisecond;
      return mToken != null && mToken != "" && currentTime < mTimeTokenWasReceivedMillis + TOKEN_VALID_LENGTH_MILLIS;
    } // isTokenValid

    // pageLoaded
    private void pageLoaded()
    {
      Debug.WriteLine(LOG_TAG + ": pageLoaded");
    }// pageLoaded

    // channelOpen
    private void channelOpen()
    {
      Debug.WriteLine(LOG_TAG + ": channelOpen");

      mChannelClosed = false;

      listener.onChannelAPIOpen();

    }// channelOpen

    // channelClosed
    private void channelClosed()
    {
      Debug.WriteLine(LOG_TAG + ": channelClosed");

      mChannelClosed = true;

      listener.onChannelAPIClosed();
    }// channelClosed

    // channelMessageReceived
    private void channelMessageReceived(string message)
    {
      Debug.WriteLine(LOG_TAG + ": update was received on channel");

      listener.onChannelAPIUpdateEvent(message);
    }// channelMessageReceived

    // channelError
    private void channelError(string message)
    {
      string code = message.Substring(0, message.IndexOf('|'));
      string description = message.Substring(message.IndexOf('|') + 1);
      Debug.WriteLine(LOG_TAG + ": channelError");
      listener.onChannelAPIError(code, description);
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

    public void onChannelAPIError(string code, string description)
    {
      Debug.WriteLine("\tCode: " + code);
      Debug.WriteLine("\tDescription: " + description);
    }

    public void onChannelAPIUpdateEvent(string message)
    {

    }
  } // ChannelAPIEventListener
}
