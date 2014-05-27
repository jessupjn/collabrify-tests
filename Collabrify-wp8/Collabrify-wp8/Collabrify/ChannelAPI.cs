using Collabrify_v2.CollabrifyProtocolBuffer;
using Microsoft.Phone.Controls;
using ProtoBuf;
using System;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Windows;
using System.Windows.Navigation;

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

    // When the object received the token. Every 2 hours te token must be refreshed and this is
    // what's used to reference if the token needs to be refreshed or not.
    private long mTimeTokenWasReceivedMillis;

    // If channel closes, this is set to true.
    private bool mChannelClosed;

    // The token we use to create the channel. Anyone with this token can listen in on the
    // channel so it should be treated as a secret.
    private string mToken;

    private CollabrifyClient client;

    public event ChannelEventListener channelEvent;
    public event ChannelEventListener neg1error;

    #region Constructor
    
    // CONSTRUCTOR
    public ChannelAPI(CollabrifyClient c, LoadCompletedEventHandler del = null)
    { 
      Debug.WriteLine(LOG_TAG + ": building ChannelAPI.");
      mChannelClosed = true;

      client = c;

      // Builds the browser object
      try
      {
        browser = new WebBrowser();
        browser.IsScriptEnabled = true;
        browser.ScriptNotify += ScriptCallback;
        browser.LoadCompleted += delegate { };
        if(del != null) browser.LoadCompleted += del;
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

    #region Public Functions

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

    public void disconnect()
    {
      try
      {
        Deployment.Current.Dispatcher.BeginInvoke(delegate
        {
          Debug.WriteLine(LOG_TAG + ": attempting to disconnect");
          this.browser.InvokeScript("closeConnection");
        });
      }
      catch (Exception e)
      {
        Debug.WriteLine(LOG_TAG + ": " + e.Source);
        Debug.WriteLine("\t" + e.Message);
      }
    } // disconnect

    #endregion

    #region Private Functions

    // ScriptCallback
    private void ScriptCallback(object sender, NotifyEventArgs e)
    {
      if(e.Value.Length < 8)
      {
        Debug.WriteLine(LOG_TAG + ": ScriptNotify: " + e.Value);
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
          Debug.WriteLine(LOG_TAG + ": ScriptNotify: " + e.Value);
          break;
      }
    } // ScriptCallback

    // ------------------------------------------------------------------------------

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
    } // setToken

    // ------------------------------------------------------------------------------

    private bool isTokenValid()
    {
      long currentTime = DateTime.Now.Ticks / TimeSpan.TicksPerMillisecond;
      return mToken != null && mToken != "" && currentTime < mTimeTokenWasReceivedMillis + TOKEN_VALID_LENGTH_MILLIS;
    } // isTokenValid

    // ------------------------------------------------------------------------------

    private void pageLoaded()
    {
      Debug.WriteLine(LOG_TAG + ": pageLoaded");
    } // pageLoaded

    // ------------------------------------------------------------------------------

    private void channelOpen()
    {
      Debug.WriteLine(LOG_TAG + ": channelOpen");

      mChannelClosed = false;

    }// channelOpen

    // ------------------------------------------------------------------------------

    private void channelClosed()
    {
      Debug.WriteLine(LOG_TAG + ": channelClosed");

      mChannelClosed = true;

    }// channelClosed

    // ------------------------------------------------------------------------------

    private void channelError(string message)
    {
      if (message.Substring(0, message.IndexOf('|')) == "-1")
      {
        Debug.WriteLine(LOG_TAG + ": channelError -1");
        Deployment.Current.Dispatcher.BeginInvoke(delegate
        {
          ChannelEventArgs args = new ChannelEventArgs(null, null);
          if (neg1error != null) neg1error.Invoke(args);
        });
      }
      else
      {
        Debug.WriteLine(LOG_TAG + ": channelError");
        Debug.WriteLine("\tCode: " + message.Substring(0, message.IndexOf('|')));
        Debug.WriteLine("\tDescription: " + message.Substring(message.IndexOf('|') + 1));
      }
    } // channelError

    // ------------------------------------------------------------------------------

    private void channelMessageReceived(string message)
    {
      Debug.WriteLine(LOG_TAG + ": update was received on channel");

      Debug.WriteLine("\tMessage: " + message);

      MemoryStream stream = new MemoryStream();
      StreamWriter writer = new StreamWriter(stream);
      writer.Write(message);
      writer.Flush();
      stream.Position = 0;

      CollabrifyNotification_PB response = Serializer.DeserializeWithLengthPrefix<CollabrifyNotification_PB>(stream, PrefixStyle.None);
      object specific_response = 0;

      if(response.notification_message_type == NotificationMessageType_PB.NOTIFICATION_MESSAGE_TYPE_NOT_SET)
      {

      }
      else if(response.notification_message_type == NotificationMessageType_PB.ON_CHANNEL_CONNECTED_NOTIFICATION)
      {
        specific_response = Serializer.DeserializeWithLengthPrefix<Notification_OnChannelConnected_PB>(stream, PrefixStyle.None);
      }
      else if(response.notification_message_type == NotificationMessageType_PB.ADD_EVENT_NOTIFICATION)
      {
        specific_response = Serializer.DeserializeWithLengthPrefix<Notification_AddEvent_PB>(stream, PrefixStyle.None);
      }
      else if(response.notification_message_type == NotificationMessageType_PB.ADD_PARTICIPANT_NOTIFICATION)
      {
        specific_response = Serializer.DeserializeWithLengthPrefix<Notification_AddParticipant_PB>(stream, PrefixStyle.None);
      }
      else if(response.notification_message_type == NotificationMessageType_PB.REMOVE_PARTICIPANT_NOTIFICATION)
      {
        specific_response = Serializer.DeserializeWithLengthPrefix<Notification_RemoveParticipant_PB>(stream, PrefixStyle.None);
      }
      else if(response.notification_message_type == NotificationMessageType_PB.END_SESSION_NOTIFICATION)
      {
        specific_response = Serializer.DeserializeWithLengthPrefix<Notification_EndSession_PB>(stream, PrefixStyle.None);
      }
      else if(response.notification_message_type == NotificationMessageType_PB.PREVENT_FURTHER_JOINS_NOTIFICATION)
      {
        specific_response = Serializer.DeserializeWithLengthPrefix<Notification_PreventFurtherJoins_PB>(stream, PrefixStyle.None);
      }
      else if(response.notification_message_type == NotificationMessageType_PB.TRANSIENT_MESSAGE_NOTIFICATION)
      {
        specific_response = Serializer.DeserializeWithLengthPrefix<Notification_TransientMessage_PB>(stream, PrefixStyle.None);
      }

      ChannelEventArgs args = new ChannelEventArgs(response, specific_response);

      if (channelEvent != null) channelEvent.Invoke(args); 
    }// channelMessageReceived

    // ------------------------------------------------------------------------------

    #endregion
  }

}
