using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Collabrify_wp8.Collabrify;
using Collabrify_v2.CollabrifyProtocolBuffer;
using System.IO;
using System.Threading;
using Collabrify_wp8.Http_Requests;
using System.Collections.ObjectModel;
using System.Diagnostics;
using Microsoft.Phone.Controls;
using System.Windows;
using System.Windows.Media;

namespace Collabrify_wp8.Collabrify
{

  public class CollabrifyClient
  {

    #region Variables 
    private readonly string LOG_TAG = "COLLABRIFY-CLIENT";

    // Session and participant objects.
    public CollabrifySession session = null;
    public CollabrifyParticipant participant = null;
    private bool getLatest;
    private bool eventsPaused = false;

    // Information pertinant to the developers credentials.
    private readonly String accountGmail;
    private readonly String accessToken;

    // An object that helps with making the individual http requests.
    private HttpRequest__Object http_object = new HttpRequest__Object();

    // events that are invoked on the return of certain request types.
    private event AddEventListener addEventListener;
    private event AddParticipantListener addParticipantListener;
    private event AddToBaseFileListener addToBaseFileListener;
    private event CreateOrGetUserListener createOrGetUserListener;
    private event CreateSessionListener createSessionListener;
    private event CreateSessionWithBaseFileListener createSessionWithBaseFileListener;
    private event DeleteSessionListener deleteSessionListener;
    private event DeleteAllSessionsListener deleteAllSessionsListener;
    private event EndSessionListener endSessionListener;
    private event GetBaseFileListener getBaseFileListener;
    private event GetCurrentOrderIDListener getCurrentOrderIDListener;
    private event GetEventListener getEventListener;
    private event GetEventBatchListener getEventBatchListener;
    private event GetFromBaseFileListener getFromBaseFileListener;
    private event GetNotificationIDListener getNotificationIDListener;
    private event GetParticipantListener getParticipantListener;
    private event GetSessionListener getSessionListener;
    private event ListAccountsListener listAccountsListener;
    private event ListSessionsListener listSessionsListener;
    private event PreventFurtherJoinsListener preventFurtherJoinsListener;
    private event RemoveParticipantListener removeParticipantListener;
    private event UpdateNotificationIDListener updateNotificationIDListener;
    private event UpdateUserListener updateUserListener;
    private event WarmupListener warmupListener;

    ChannelAPI channelAPI;
    //private event receivedEvent receivedEvent;
    //private event receivedBaseFileChunk receivedEvent;
    //private event uploadedBaseFileWithSize receivedEvent;
    //private event participantJoined receivedEvent;
    //private event participantLeft receivedEvent;
    //private event sessionEnded receivedEvent;
    //private event clientDidEnterBackground participantLeftSession;

    #endregion 

    #region Constructor
 
    // CONSTRUCTOR
    public CollabrifyClient(string _gmail, string _displayName, string _accountGmail, string _access_token, bool _get_latest)
    {
      participant = new CollabrifyParticipant(0, _displayName, _gmail, 0);
      accountGmail = _accountGmail;
      accessToken = _access_token;
      getLatest = _get_latest;

      http_object.HttpRequestDone += new CollabrifyEventListener(httpReturned);
      HttpRequest_Warmup.make_request(this, http_object);

      channelAPI = new ChannelAPI(_access_token);
    } // CONSTRUCTOR
    #endregion

    // ------------------------------------------------------------------------------
    // ------------------------------------------------------------------------------

    #region HttpRequestReturned

    private void httpReturned(CollabrifyEventArgs e)
    {
      /* Handles the response from the server within the CollabrifyClient object */

      if (e.response.success_flag)
      {
          Debug.WriteLine(LOG_TAG + ": " + e.type.ToString() + " was successful.\n");
          switch (e.type)
          {
              case CollabrifyRequestType_PB.ADD_EVENT_REQUEST:
                  AddEvent_Args ae = new AddEvent_Args(e.specificResponsePB);
                  if (addEventListener != null) addEventListener.Invoke(ae);
                  break;
              case CollabrifyRequestType_PB.ADD_PARTICIPANT_REQUEST:
                  AddParticipant_Args ap = new AddParticipant_Args(e.specificResponsePB);
                  if (addParticipantListener != null) addParticipantListener.Invoke(ap);
                  break;
              case CollabrifyRequestType_PB.ADD_TO_BASE_FILE_REQUEST:
                  AddToBaseFile_Args atbf = new AddToBaseFile_Args(e.specificResponsePB);
                  if (addToBaseFileListener != null) addToBaseFileListener.Invoke(atbf);
                  break;
              case CollabrifyRequestType_PB.CREATE_OR_GET_USER:
                  CreateOrGetUser_Args cogu = new CreateOrGetUser_Args(e.specificResponsePB);
                  if (createOrGetUserListener != null) createOrGetUserListener.Invoke(cogu);
                  break;
              case CollabrifyRequestType_PB.CREATE_SESSION_REQUEST:
                  CreateSession_Args cs = new CreateSession_Args(e.specificResponsePB);
                  this.session = new CollabrifySession((e.specificResponsePB as Response_CreateSession_PB).session);
                  this.participant = session.getOwner();  
                  if (createSessionListener != null) createSessionListener.Invoke(cs);
                  break;
              case CollabrifyRequestType_PB.CREATE_SESSION_WITH_BASE_FILE_REQUEST:
                  CreateSessionWithBaseFile_Args cswbf = new CreateSessionWithBaseFile_Args(e.specificResponsePB);
                  if (createSessionWithBaseFileListener != null) createSessionWithBaseFileListener.Invoke(cswbf);
                  break;
              case CollabrifyRequestType_PB.DELETE_SESSION_REQUEST:
                  DeleteSession_Args ds = new DeleteSession_Args(e.specificResponsePB);
                  if (deleteSessionListener != null) deleteSessionListener.Invoke(ds);
                  break;
              case CollabrifyRequestType_PB.DELETE_ALL_SESSIONS_REQUEST:
                  DeleteAllSessions_Args das = new DeleteAllSessions_Args(e.specificResponsePB);
                  if (deleteAllSessionsListener != null) deleteAllSessionsListener.Invoke(das);
                  break;
              case CollabrifyRequestType_PB.DELETE_OLD_SESSIONS_REQUEST:
                  break;
              case CollabrifyRequestType_PB.DELETE_USER:
                  break;
              case CollabrifyRequestType_PB.END_SESSION_REQUEST:
                  RemoveParticipant_Args es = new RemoveParticipant_Args(e.specificResponsePB);
                  if (removeParticipantListener != null) removeParticipantListener.Invoke(es);
                  break;
              case CollabrifyRequestType_PB.GET_BASE_FILE_REQUEST:
                  GetBaseFile_Args gbf = new GetBaseFile_Args(e.specificResponsePB);
                  if (getBaseFileListener != null) getBaseFileListener.Invoke(gbf);
                  break;
              case CollabrifyRequestType_PB.GET_CURRENT_ORDER_ID_REQUEST:
                  GetCurrentOrderID_Args gcoi = new GetCurrentOrderID_Args(e.specificResponsePB);
                  if (getCurrentOrderIDListener != null) getCurrentOrderIDListener.Invoke(gcoi);
                  break;
              case CollabrifyRequestType_PB.GET_EVENT_BATCH_REQUEST:
                  GetEventBatch_Args geb = new GetEventBatch_Args(e.specificResponsePB);
                  if (getEventBatchListener != null) getEventBatchListener.Invoke(geb);
                  break;
              case CollabrifyRequestType_PB.GET_EVENT_REQUEST:
                  GetEvent_Args ge = new GetEvent_Args(e.specificResponsePB);
                  if (getEventListener != null) getEventListener.Invoke(ge);
                  break;
              case CollabrifyRequestType_PB.GET_FROM_BASE_FILE_REQUEST:
                  GetFromBaseFile_Args gfbf = new GetFromBaseFile_Args(e.specificResponsePB);
                  if (getFromBaseFileListener != null) getFromBaseFileListener.Invoke(gfbf);
                  break;
              case CollabrifyRequestType_PB.GET_LAST_EVENT_BY_PARTICIPANT_REQUEST:
                  GetLastEventByParticipant_Args glebp = new GetLastEventByParticipant_Args(e.specificResponsePB);
                  break;
              case CollabrifyRequestType_PB.GET_NOTIFICATION_ID_REQUEST:
                  GetCurrentOrderID_Args gni = new GetCurrentOrderID_Args(e.specificResponsePB);
                  if (getCurrentOrderIDListener != null) getCurrentOrderIDListener.Invoke(gni);
                  break;
              case CollabrifyRequestType_PB.GET_PARTICIPANT_REQUEST:
                  GetParticipant_Args gp = new GetParticipant_Args(e.specificResponsePB);
                  if (getParticipantListener != null) getParticipantListener.Invoke(gp);
                  break;
              case CollabrifyRequestType_PB.GET_SESSION_REQUEST:
                  GetSession_Args gs = new GetSession_Args(e.specificResponsePB);
                  if (getSessionListener != null) getSessionListener.Invoke(gs);
                  break;
              case CollabrifyRequestType_PB.LIST_ACCOUNTS_REQUEST:
                  ListAccounts_Args la = new ListAccounts_Args(e.specificResponsePB);
                  if (listAccountsListener != null) listAccountsListener.Invoke(la);
                  break;
              case CollabrifyRequestType_PB.LIST_SESSIONS_REQUEST:
                  ListSessions_Args ls = new ListSessions_Args(e.specificResponsePB);
                  if (listSessionsListener != null) listSessionsListener.Invoke(ls);
                  break;
              case CollabrifyRequestType_PB.PREVENT_FURTHER_JOINS_REQUEST:
                  PreventFurtherJoins_Args pfj = new PreventFurtherJoins_Args(e.specificResponsePB);
                  if (preventFurtherJoinsListener != null) preventFurtherJoinsListener.Invoke(pfj);
                  break;
              case CollabrifyRequestType_PB.REMOVE_PARTICIPANT_REQUEST:
                  RemoveParticipant_Args rp = new RemoveParticipant_Args(e.specificResponsePB);
                  if (removeParticipantListener != null) removeParticipantListener.Invoke(rp);
                  break;
              case CollabrifyRequestType_PB.REQUEST_TYPE_NOT_SET:
                  Debug.WriteLine(LOG_TAG + ":\n\tError:\n\tRequest Type Was Not Set");
                  break;
              case CollabrifyRequestType_PB.UPDATE_NOTIFICATION_ID_REQUEST:
                  UpdateNotificationID_Args uni = new UpdateNotificationID_Args(e.specificResponsePB);
                  if (updateNotificationIDListener != null) updateNotificationIDListener.Invoke(uni);
                  break;
              case CollabrifyRequestType_PB.UPDATE_USER:
                  UpdateUser_Args uu = new UpdateUser_Args(e.specificResponsePB);
                  if (updateUserListener != null) updateUserListener.Invoke(uu);
                  break;
              case CollabrifyRequestType_PB.WARMUP_REQUEST:
                  break;
          } // switch
      } // if
      else
      {
          Debug.WriteLine(LOG_TAG + ":\n\t" + e.type.ToString() + " Failed:");
          Debug.WriteLine("\tException Type: " + e.response.exception.exception_type.ToString());
          Debug.WriteLine("\tException Cause: " + e.response.exception.cause.ToString() + "\n");
      }

    } // httpReturned

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

    public void createSession(string name, List<string> tags, string password, bool startPaused, CreateSessionListener completionHandler)
    {
      createSession(name, tags, password, 0, startPaused, completionHandler);
    } // createSession

    // ------------------------------------------------------------------------------

    public void createSession(string name, List<string> tags, string password, int participantLimit, bool startPaused, CreateSessionListener completionHandler)
    {
      createSessionListener = completionHandler;
      HttpRequest_CreateSession.make_request(this, http_object, name, tags, password, participantLimit);
      if (startPaused) pauseEvents();
    } // createSession

    // ------------------------------------------------------------------------------

    // TODO: createSessionWithBaseFile
    public void createSessionWithBaseFile(string name, List<string> tags, string password, bool startPaused, CreateSessionWithBaseFileListener completionHandler)
    {
      createSessionWithBaseFile(name, tags, password, 0, startPaused, completionHandler);
    } // createSessionWithBaseFile

    // ------------------------------------------------------------------------------

    // TODO: createSessionWithBaseFile
    public void createSessionWithBaseFile(string name, List<string> tags, string password, int participantLimit, bool startPaused, CreateSessionWithBaseFileListener completionHandler)
    {
      createSessionWithBaseFileListener = completionHandler;
      HttpRequest_CreateSessionWithBaseFile.make_request(this, http_object, name, tags, password, participantLimit);

      if (startPaused) pauseEvents();
    } // createSessionWithBaseFile

    // ------------------------------------------------------------------------------

    public void joinSession(long id, string password, AddParticipantListener completionHandler)
    {
      addParticipantListener = completionHandler;
      HttpRequest_AddParticipant.make_request(this, http_object, id, password);
    } // joinSession

    // ------------------------------------------------------------------------------

    public void joinSession(long id, string password, bool startPaused, AddParticipantListener completionHandler)
    {
      joinSession(id, password, completionHandler);
      if (startPaused) pauseEvents();
    } // joinSession

    // ------------------------------------------------------------------------------
    
    public void leaveSession(bool deleteSession, RemoveParticipantListener completionHandler)
    {

      removeParticipantListener = completionHandler;
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

    // TODO: broadcast
    public void broadcast(byte[] data, string eventType)
    {
      if( !eventsPaused ) HttpRequest_AddEvent.make_request(this, http_object, data, eventType);
    } // broadcast

    // ------------------------------------------------------------------------------

    // TODO: listSessions
    public void listSessions(List<string> tags) 
    {
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

    // TODO: current_session_participants
    public List<CollabrifyParticipant> currentSessionParticipants() 
    {
      return new List<CollabrifyParticipant>();
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