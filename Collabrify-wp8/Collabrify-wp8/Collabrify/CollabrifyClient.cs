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
    private CollabrifySession session = null;
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

          switch (e.type)
          {
              case CollabrifyRequestType_PB.ADD_EVENT_REQUEST:
                  break;
              case CollabrifyRequestType_PB.ADD_PARTICIPANT_REQUEST:
                  break;
              case CollabrifyRequestType_PB.ADD_TO_BASE_FILE_REQUEST:
                  break;
              case CollabrifyRequestType_PB.CREATE_OR_GET_USER:
                  break;
              case CollabrifyRequestType_PB.CREATE_SESSION_REQUEST:
                  CreateSession_Args c = new CreateSession_Args(e.specificResponsePB);
                  this.session = new CollabrifySession((e.specificResponsePB as Response_CreateSession_PB).session);
                  if (createSessionListener != null) createSessionListener.Invoke(c);
                  break;
              case CollabrifyRequestType_PB.DELETE_SESSION_REQUEST:
                  break;
              case CollabrifyRequestType_PB.LIST_SESSIONS_REQUEST:
                  //ListSessionsHandler.Invoke(this, EventArgs.Empty);
                  break;
          }
      }
      else
      {
          Debug.WriteLine("Request Failed:");
          Debug.WriteLine("Exception Type: " + e.response.exception.exception_type.ToString());
          Debug.WriteLine("Exception Cause: " + e.response.exception.cause.ToString());
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

      createSessionListener = completionHandler;
      HttpRequest_CreateSession.make_request(this, http_object, name, tags, password);

      if (startPaused) pauseEvents();
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
      createSessionWithBaseFileListener = completionHandler;
      HttpRequest_CreateSessionWithBaseFile.make_request(this, http_object, name, tags, password);

      if (startPaused) pauseEvents();
    }

    // TODO: createSessionWithBaseFile
    public void createSessionWithBaseFile(string name, List<string> tags, string password, int participantLimit, bool startPaused, CreateSessionWithBaseFileListener completionHandler)
    {
      createSessionWithBaseFileListener = completionHandler;
      HttpRequest_CreateSessionWithBaseFile.make_request(this, http_object, name, tags, password, participantLimit);

      if (startPaused) pauseEvents();
    }

    // TODO: joinSession
    public void joinSession(long id, string password, AddParticipantListener completionHandler)
    {

    }

    // TODO: joinSession
    public void joinSession(long id, string password, bool startPaused, AddParticipantListener completionHandler)
    {
      
      if (startPaused) pauseEvents();
    }

    // TODO: leaveAndDeleteSession
    public void leaveAndDeleteSession(DeleteSessionListener completionHandler)
    {
    }

    // TODO: broadcast
    public void broadcast(object data, string eventType)
    {

    }

    // TODO: listSessions
    public List<CollabrifySession> listSessions(List<string> tags) { return new List<CollabrifySession>(); }

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