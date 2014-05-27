using Collabrify_v2.CollabrifyProtocolBuffer;
using Collabrify_wp8.Http_Requests;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Windows;

namespace Collabrify_wp8.Collabrify
{

  // QUESTIONS FOR GROUP:
  // 1: CHANNEL ERROR OF -1
  // 2: WHAT TO DO IN CASE OF CHANNEL EVENTS
  // 3: HOW TO MAKE SOME CLASSES ONLY VISIBLE WITHIN DLL FILE.
  //
  //
  //
  //
  //


  public class CollabrifyClient
  {

    #region Variables 
    private readonly string LOG_TAG = "COLLABRIFY-CLIENT";

    // Session and participant objects.
    private CollabrifySession session = null;
    public CollabrifyParticipant participant = null;
    private bool getLatest;
    private bool eventsPaused = false;

    // Information pertinant to the developers credentials.
    private readonly String accountGmail;
    private readonly String accessToken;

    // An object that helps with making the individual http requests.
    private HttpRequest__Object http_object;

    // event that is invoked upon completion of some requests
    private event CompletionHandler mCompletionHandler;
    private event ListSessionsCompletionHandler mListSessionsCompletionHandler;

    private ChannelAPI channelAPI;
    private string notificationID;

    public event ReceivedEvent receivedEvent;
    public event ReceivedBaseFileChunk receivedBaseFileChunk;
    public event UploadedBaseFileWithSize uploadedBaseFileWithSize;
    public event ParticipantJoined participantJoined;
    public event ParticipantLeft participantLeft;
    public event SessionEnded sessionEnded;
    public event ClientDidEnterBackground clientDidEnterBackground;

    #endregion 

    #region Constructor
 
    // CONSTRUCTOR
    public CollabrifyClient(string _gmail, string _displayName, string _accountGmail, string _access_token, bool _get_latest)
    {
      participant = new CollabrifyParticipant(0, _displayName, _gmail, 0);
      http_object = new HttpRequest__Object();
      channelAPI = new ChannelAPI(this);

      accountGmail = _accountGmail;
      accessToken = _access_token;
      getLatest = _get_latest;

      http_object.HttpRequestDone += new CollabrifyEventListener(httpReturned);
      channelAPI.channelEvent += new ChannelEventListener(channelNotification);
      //channelAPI.neg1error += new ChannelEventListener(channelNeg1Error);

      HttpRequest_Warmup.make_request(this, http_object);

      eventsPaused = false;
    } // CONSTRUCTOR
    #endregion

    // ------------------------------------------------------------------------------
    // ------------------------------------------------------------------------------

    #region HttpRequestReturned
    private void channelNeg1Error(ChannelEventArgs e)
    {
      Debug.WriteLine(LOG_TAG + ": Channel API Neg 1 error.");
      channelAPI = new ChannelAPI(this, delegate
      {
        //channelAPI.connect(this.notificationID);
      });
      channelAPI.channelEvent += new ChannelEventListener(channelNotification);
      channelAPI.neg1error += new ChannelEventListener(channelNeg1Error);
    }

    private void httpReturned(CollabrifyEventArgs e)
    {
      /* Handles the response from the server within the CollabrifyClient object */
      if (e.response.success_flag)
      {
          Debug.WriteLine(LOG_TAG + ": " + e.type.ToString() + " was successful.\n");

          List<CollabrifySession> sessionList = new List<CollabrifySession>();

          switch (e.type)
          {
              case CollabrifyRequestType_PB.ADD_EVENT_REQUEST:
                  break;
              case CollabrifyRequestType_PB.ADD_PARTICIPANT_REQUEST:
                  this.session = new CollabrifySession((e.specificResponsePB as Response_AddParticipant_PB).session);
                  this.participant = new CollabrifyParticipant((e.specificResponsePB as Response_AddParticipant_PB).participant);
                  this.notificationID = (e.specificResponsePB as Response_AddParticipant_PB).participant.notification_id;
                  channelAPI.connect( notificationID );
                  break;
              case CollabrifyRequestType_PB.ADD_TO_BASE_FILE_REQUEST:
                  break;
              case CollabrifyRequestType_PB.CREATE_OR_GET_USER:
                  break;
              case CollabrifyRequestType_PB.CREATE_SESSION_REQUEST: 
                    this.session = new CollabrifySession((e.specificResponsePB as Response_CreateSession_PB).session);
                    this.participant = session.getOwner();
                    this.notificationID = (e.specificResponsePB as Response_CreateSession_PB).owner.notification_id;
                    channelAPI.connect(this.notificationID);
                  break;
              case CollabrifyRequestType_PB.CREATE_SESSION_WITH_BASE_FILE_REQUEST:
                  this.session = new CollabrifySession((e.specificResponsePB as Response_CreateSessionWithBaseFile_PB).session);
                  this.participant = session.getOwner();
                  this.notificationID = (e.specificResponsePB as Response_CreateSessionWithBaseFile_PB).owner.notification_id;
                  try { channelAPI.connect((e.specificResponsePB as Response_CreateSessionWithBaseFile_PB).owner.notification_id); }
                  catch (Exception ex) { Debug.WriteLine(LOG_TAG + ":\n" + ex.Message); }
                  break;
              case CollabrifyRequestType_PB.DELETE_SESSION_REQUEST:
                  break;
              case CollabrifyRequestType_PB.DELETE_ALL_SESSIONS_REQUEST:
                  break;
              case CollabrifyRequestType_PB.DELETE_OLD_SESSIONS_REQUEST:
                  break;
              case CollabrifyRequestType_PB.DELETE_USER:
                  break;
              case CollabrifyRequestType_PB.END_SESSION_REQUEST:
                  this.channelAPI.disconnect();
                  break;
              case CollabrifyRequestType_PB.GET_BASE_FILE_REQUEST:
                  break;
              case CollabrifyRequestType_PB.GET_CURRENT_ORDER_ID_REQUEST:
                  break;
              case CollabrifyRequestType_PB.GET_EVENT_BATCH_REQUEST:
                  break;
              case CollabrifyRequestType_PB.GET_EVENT_REQUEST:
                  break;
              case CollabrifyRequestType_PB.GET_FROM_BASE_FILE_REQUEST:
                  break;
              case CollabrifyRequestType_PB.GET_LAST_EVENT_BY_PARTICIPANT_REQUEST:
                  break;
              case CollabrifyRequestType_PB.GET_NOTIFICATION_ID_REQUEST:
                  break;
              case CollabrifyRequestType_PB.GET_PARTICIPANT_REQUEST:
                  break;
              case CollabrifyRequestType_PB.GET_SESSION_REQUEST:
                  break;
              case CollabrifyRequestType_PB.LIST_ACCOUNTS_REQUEST:
                  break;
              case CollabrifyRequestType_PB.LIST_SESSIONS_REQUEST:
                  foreach(Session_PB s in (e.specificResponsePB as Response_ListSessions_PB).session)
                  {
                    sessionList.Add( new CollabrifySession(s) );
                  }
                  break;
              case CollabrifyRequestType_PB.PREVENT_FURTHER_JOINS_REQUEST:
                  break;
              case CollabrifyRequestType_PB.REMOVE_PARTICIPANT_REQUEST:
                  channelAPI.disconnect();
                  break;
              case CollabrifyRequestType_PB.REQUEST_TYPE_NOT_SET:
                  Debug.WriteLine(LOG_TAG + ":\n\tError:\n\tRequest Type Was Not Set");
                  break;
              case CollabrifyRequestType_PB.UPDATE_NOTIFICATION_ID_REQUEST:
                  this.participant = new CollabrifyParticipant((e.specificResponsePB as Response_UpdateNotificationID_PB).participant);
                  this.session = new CollabrifySession((e.specificResponsePB as Response_UpdateNotificationID_PB).session);
                  break;
              case CollabrifyRequestType_PB.UPDATE_USER:
                  break;
              case CollabrifyRequestType_PB.WARMUP_REQUEST:
                  //HttpRequest_CreateOrGetUser.make_request(this, http_object);
                  break;
          } // switch

          if (mCompletionHandler != null)
          {
            Deployment.Current.Dispatcher.BeginInvoke(delegate {
              mListSessionsCompletionHandler += delegate { mListSessionsCompletionHandler = null; };
              mCompletionHandler.Invoke(this);
            });
          }
          if(mListSessionsCompletionHandler != null)
          {
            Deployment.Current.Dispatcher.BeginInvoke(delegate
            {
              mListSessionsCompletionHandler += delegate { mListSessionsCompletionHandler = null; };
              mListSessionsCompletionHandler.Invoke(sessionList);
            });
          }
      } // if
      else
      {
          Debug.WriteLine(LOG_TAG + ":\n\t" + e.type.ToString() + " Failed:");
          Debug.WriteLine("\tException Type: " + e.response.exception.exception_type.ToString());
          Debug.WriteLine("\tException Cause: " + e.response.exception.cause.ToString() + "\n");
      }

    } // httpReturned

    // ------------------------------------------------------------------------------

    private void channelNotification(ChannelEventArgs e)
    {
      if (e.type == NotificationMessageType_PB.ADD_EVENT_NOTIFICATION)
      {
        
      }
      else if (e.type == NotificationMessageType_PB.ADD_PARTICIPANT_NOTIFICATION)
      {

      }
      else if (e.type == NotificationMessageType_PB.END_SESSION_NOTIFICATION)
      {

      }
      else if (e.type == NotificationMessageType_PB.NOTIFICATION_MESSAGE_TYPE_NOT_SET)
      {
      
      }
      else if (e.type == NotificationMessageType_PB.ON_CHANNEL_CONNECTED_NOTIFICATION)
      {

      }
      else if (e.type == NotificationMessageType_PB.PREVENT_FURTHER_JOINS_NOTIFICATION)
      {

      }
      else if (e.type == NotificationMessageType_PB.REMOVE_PARTICIPANT_NOTIFICATION)
      {

      }
      else if (e.type == NotificationMessageType_PB.TRANSIENT_MESSAGE_NOTIFICATION)
      {

      }

    } // channelNotification
#endregion 

    // ------------------------------------------------------------------------------
    // ------------------------------------------------------------------------------

    #region Public Functions

    // PUBLICALLY AVAILABLE FUNCTIONS AND METHODS TO THE DEVELOPER.
    // ------------------------------------------------------------------------------

    public void pauseEvents() 
    {
      eventsPaused = true;
    } // pauseEvents

    // ------------------------------------------------------------------------------

    public void resumeEvents() 
    { 
      eventsPaused = false;
    } // resumeEvents

    // ------------------------------------------------------------------------------

    // TODO: currentNetworkStatus
    public void currentNetworkStatus() 
    {
      return; 
    } // currentNetworkStatus

    // ------------------------------------------------------------------------------

    public void createSession(string name, List<string> tags, string password, bool startPaused, CompletionHandler completionHandler)
    {
      createSession(name, tags, password, 0, startPaused, completionHandler);
    } // createSession

    // ------------------------------------------------------------------------------

    public void createSession(string name, List<string> tags, string password, int participantLimit, bool startPaused, CompletionHandler completionHandler)
    {
      mCompletionHandler = completionHandler;
      HttpRequest_CreateSession.make_request(this, http_object, name, tags, password, participantLimit);
      if (startPaused) pauseEvents();
    } // createSession

    // ------------------------------------------------------------------------------

    public void createSessionWithBaseFile(string name, List<string> tags, string password, bool startPaused, CompletionHandler completionHandler)
    {
      createSessionWithBaseFile(name, tags, password, 0, startPaused, completionHandler);
    } // createSessionWithBaseFile

    // ------------------------------------------------------------------------------

    // TODO: createSessionWithBaseFile
    public void createSessionWithBaseFile(string name, List<string> tags, string password, int participantLimit, bool startPaused, CompletionHandler completionHandler)
    {
      mCompletionHandler = completionHandler;
      if (startPaused) pauseEvents();
      HttpRequest_CreateSessionWithBaseFile.make_request(this, http_object, name, tags, password, participantLimit);
    } // createSessionWithBaseFile

    // ------------------------------------------------------------------------------

    public void joinSession(long id, string password, CompletionHandler completionHandler)
    {
      mCompletionHandler = completionHandler;
      HttpRequest_AddParticipant.make_request(this, http_object, id, password);
    } // joinSession

    // ------------------------------------------------------------------------------

    public void joinSession(long id, string password, bool startPaused, CompletionHandler completionHandler)
    {
      if (startPaused) pauseEvents();
      joinSession(id, password, completionHandler);
    } // joinSession

    // ------------------------------------------------------------------------------

    public void leaveSession(bool deleteSession, CompletionHandler completionHandler)
    {
      mCompletionHandler = completionHandler;
      if (session.getOwner().getId() == participant.getId() && deleteSession)
      {
        HttpRequest_EndSession.make_request(this, http_object);
      }
      else
      {
        HttpRequest_RemoveParticipant.make_request(this, http_object);
      }
    } // leaveAndDeleteSession

    // ------------------------------------------------------------------------------

    public void broadcast(byte[] data, string eventType)
    {
      if (this.isInSession() == false) Debug.WriteLine(LOG_TAG + ": Create/Join a session to broadcast.");
      else if (!eventsPaused) HttpRequest_AddEvent.make_request(this, http_object, data, eventType);
      else Debug.WriteLine(LOG_TAG + ": events are paused... cannot broadcast.");
    } // broadcast

    // ------------------------------------------------------------------------------

    // TODO: finish listSessions
    public void listSessions(List<string> tags, ListSessionsCompletionHandler completionHandler) 
    {
      mListSessionsCompletionHandler = completionHandler;
      mCompletionHandler = null;
      HttpRequest_ListSessions.make_request(this, http_object, tags); 
    } // listSessions

    // ------------------------------------------------------------------------------

    public bool isInSession() {
      if (session == null) return false;
      else if (currentSessionHasEnded()) return false;
      else return true;
    }

    // ------------------------------------------------------------------------------

    public bool currentSessionHasEnded() 
    { 
      return session.getSessionEnded();
    } // currentSessionHasEnded

    // ------------------------------------------------------------------------------

    public bool currentSessionHasBaseFile() 
    { 
      return session.getHasBaseFile();
    } // currentSessionHasBaseFile

    // ------------------------------------------------------------------------------

    public long currentSessionID() 
    {
      return session.getId(); 
    } // currentSessionID

    // ------------------------------------------------------------------------------

    public bool currentSessionIsPasswordProtected() 
    { 
      return session.getIsPasswordProtected();
    } // currentSessionIsPasswordProtected

    // ------------------------------------------------------------------------------

    public string currentSessionName() 
    {
      return session.getName(); 
    } // currentSessionName

    // ------------------------------------------------------------------------------

    public string getAccountGmail() 
    {
      return accountGmail; 
    } // getAccountGmail

    // ------------------------------------------------------------------------------

    public string getAccessToken() 
    {
      return accessToken;
    } // getAccessToken

    // ------------------------------------------------------------------------------

    public long currentSessionOrderID() 
    { 
      return session.getCurrentOrderId();
    } // currentSessionOrderID

    // ------------------------------------------------------------------------------

    public CollabrifyParticipant currentSessionOwner() 
    {
      return session.getOwner();
    } // currentSessionOwner

    // ------------------------------------------------------------------------------

    public int currentSessionParticipantCount() 
    {
      return session.getParticipantCount();  
    } // currentSessionParticipantCount

    // ------------------------------------------------------------------------------

    public List<CollabrifyParticipant> currentSessionParticipants() 
    {
      List<CollabrifyParticipant> list = new List<CollabrifyParticipant>();
      foreach (CollabrifyParticipant p in session.getParticipants())
      {
        list.Add(p);
      }
      return list;
    } // currentSessionParticipants

    // ------------------------------------------------------------------------------

    public List<string> currentSessionTags() 
    {
      return session.getSessionTags();
    } // currentSessionTags

    // ------------------------------------------------------------------------------

    // TODO: has network connection
    public bool hasNetworkConnection()
    {
      return true;
    } // hasNetworkConnection

    // ------------------------------------------------------------------------------

    public CollabrifySession getSession()
    { 
      return session;
    } // getSession

    // ------------------------------------------------------------------------------

    #endregion 
  }
}