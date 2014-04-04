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
    private readonly String accountGmail;
    private readonly String gmail;
    private readonly String displayName;
    private readonly String accessToken;
    private bool getLatest;
    private bool eventsPaused = false;

    private HttpRequest__Object http_object = new HttpRequest__Object();
    public CollabrifySession session = null;
    public CollabrifyParticipant participant = null;

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

    // USED TO UPDATE MAINPAGE INFORMATION
    private string ret;
    public long id;

    private void BasicClientInitialize()
    {
      http_object.HttpRequestDone += new CollabrifyEventListener(httpReturned);

      HttpRequest_Warmup.make_request(this, http_object);
    }

    public CollabrifyClient(string _gmail, string _accountGmail, string _access_token, bool _get_latest)
    {

      gmail = _gmail;
      accountGmail = _accountGmail;
      accessToken = _access_token;
      getLatest = _get_latest;

      BasicClientInitialize();
    }

    private void httpReturned(CollabrifyEventArgs e)
    {
      /* Handles the response from the server within the CollabrifyClient object */

      if (e.response.success_flag)
      {
          Debug.WriteLine(e.type.ToString() + " was successful.\n");
          switch (e.type)
          {
              case CollabrifyRequestType_PB.ADD_EVENT_REQUEST:
                  break;
              case CollabrifyRequestType_PB.ADD_PARTICIPANT_REQUEST:
                  AddParticipant_Args ap = new AddParticipant_Args(e.specificResponsePB);
                  this.participant = new CollabrifyParticipant(ap.getReturnedData().participant);
                  Debug.WriteLine("USER DISPLAY NAME: " + participant.getDisplayName());
                  Debug.WriteLine("USER ID: " + participant.getId());
                  break;
              case CollabrifyRequestType_PB.ADD_TO_BASE_FILE_REQUEST:
                  break;
              case CollabrifyRequestType_PB.CREATE_OR_GET_USER:
                  CreateOrGetUser_Args cogu = new CreateOrGetUser_Args(e.specificResponsePB);
                  break;
              case CollabrifyRequestType_PB.CREATE_SESSION_REQUEST:
                  CreateSession_Args cs = new CreateSession_Args(e.specificResponsePB);
                  this.session = new CollabrifySession((e.specificResponsePB as Response_CreateSession_PB).session);
                  Debug.WriteLine("OWNER DISPLAY NAME: " + session.getOwner().getDisplayName());
                  Debug.WriteLine("OWNER ID: " + session.getOwner().getId());
                  this.participant = session.getOwner();
                  if (createSessionListener != null) createSessionListener.Invoke(cs);
                  break;
              case CollabrifyRequestType_PB.CREATE_SESSION_WITH_BASE_FILE_REQUEST:
                  break;
              case CollabrifyRequestType_PB.DELETE_SESSION_REQUEST:
                  DeleteSession_Args ds = new DeleteSession_Args(e.specificResponsePB);
                  if (deleteSessionListener != null) deleteSessionListener.Invoke(ds);
                  break;
              case CollabrifyRequestType_PB.DELETE_ALL_SESSIONS_REQUEST:
                  break;
              case CollabrifyRequestType_PB.DELETE_OLD_SESSIONS_REQUEST:
                  break;
              case CollabrifyRequestType_PB.DELETE_USER:
                  break;
              case CollabrifyRequestType_PB.END_SESSION_REQUEST:
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
                  ListSessions_Args ls = new ListSessions_Args(e.specificResponsePB);
                  if (listSessionsListener != null) listSessionsListener.Invoke(ls);
                  break;
              case CollabrifyRequestType_PB.PREVENT_FURTHER_JOINS_REQUEST:
                  break;
              case CollabrifyRequestType_PB.REMOVE_PARTICIPANT_REQUEST:
                  RemoveParticipant_Args rp = new RemoveParticipant_Args(e.specificResponsePB);
                  if (removeParticipantListener != null) removeParticipantListener.Invoke(rp);
                  break;
              case CollabrifyRequestType_PB.REQUEST_TYPE_NOT_SET:
                  break;
              case CollabrifyRequestType_PB.UPDATE_NOTIFICATION_ID_REQUEST:
                  break;
              case CollabrifyRequestType_PB.UPDATE_USER:
                  break;
              case CollabrifyRequestType_PB.WARMUP_REQUEST:
                  break;
          }
      }
      else
      {
          Debug.WriteLine(e.type.ToString() + " Failed:");
          Debug.WriteLine("Exception Type: " + e.response.exception.exception_type.ToString());
          Debug.WriteLine("Exception Cause: " + e.response.exception.cause.ToString() + "\n");
      }

    }

    public void makeListSession() { HttpRequest_ListSessions.make_request(this, http_object); }
    public void makeDeleteSession() { HttpRequest_DeleteSession.make_request(this, http_object); }


    public void pauseEvents() { eventsPaused = true; }

    public void resumeEvents() { eventsPaused = false; }

    // TODO: currentNetworkStatus
    public void currentNetworkStatus() { return; }

    public void createSession(string name, List<string> tags, string password, bool startPaused, CreateSessionListener completionHandler)
    {
      createSession(name, tags, password, 0, startPaused, completionHandler);
    }

    public void createSession(string name, List<string> tags, string password, int participantLimit, bool startPaused, CreateSessionListener completionHandler)
    {
      createSessionListener = completionHandler;
      HttpRequest_CreateSession.make_request(this, http_object, name, tags, password, participantLimit);
      if (startPaused) pauseEvents();
    }

    // TODO: createSessionWithBaseFile
    public void createSessionWithBaseFile(string name, List<string> tags, string password, bool startPaused, CreateSessionWithBaseFileListener completionHandler)
    {
      createSessionWithBaseFile(name, tags, password, 0, startPaused, completionHandler);
    }

    // TODO: createSessionWithBaseFile
    public void createSessionWithBaseFile(string name, List<string> tags, string password, int participantLimit, bool startPaused, CreateSessionWithBaseFileListener completionHandler)
    {
      createSessionWithBaseFileListener = completionHandler;
      HttpRequest_CreateSessionWithBaseFile.make_request(this, http_object, name, tags, password, participantLimit);

      if (startPaused) pauseEvents();
    }

    public void joinSession(long id, string password, AddParticipantListener completionHandler)
    {
      addParticipantListener = completionHandler;
      HttpRequest_AddParticipant.make_request(this, http_object, id, password);
    }

    public void joinSession(long id, string password, bool startPaused, AddParticipantListener completionHandler)
    {
      joinSession(id, password, completionHandler);
      if (startPaused) pauseEvents();
    }

    // TODO: leaveAndDeleteSession
    public void leaveSession(bool deleteSession, RemoveParticipantListener completionHandler)
    {

      removeParticipantListener = completionHandler;
      if (session.getOwner().getId() == participant.getId() && deleteSession)
        removeParticipantListener += delegate
        {
          HttpRequest_DeleteSession.make_request(this, http_object);
        };

      HttpRequest_RemoveParticipant.make_request(this, http_object);
    }

    // TODO: broadcast
    public void broadcast(object data, string eventType)
    {

    }

    // TODO: listSessions
    public List<CollabrifySession> listSessions(List<string> tags) 
    { 
      return new List<CollabrifySession>(); 
    }

    public bool isInSession() {
      if (session == null) return false;
      else if (currentSessionHasEnded()) return false;
      else return true;
    }

    public bool currentSessionHasEnded() { return session.getSessionEnded(); }

    public bool currentSessionHasBaseFile() { return session.getHasBaseFile(); }

    public long currentSessionID() { return session.getId(); }

    public bool currentSessionIsPasswordProtected() { return session.getIsPasswordProtected(); }

    public string currentSessionName() { return session.getName(); }

    public string getAccountGmail() { return accountGmail; }

    public string getAccessToken() { return accessToken; }

    public long currentSessionOrderID() { return session.getCurrentOrderId(); }

    public CollabrifyParticipant currentSessionOwner() { return session.getOwner(); }

    public int currentSessionParticipantCount() { return session.getParticipantCount();  }

    // TODO: current_session_participants
    public List<CollabrifyParticipant> currentSessionParticipants() { return new List<CollabrifyParticipant>(); }

    public List<string> currentSessionTags() { return session.getSessionTags(); }

    // TODO: has network connection
    public bool hasNetworkConnection() { return true; }
  }
}