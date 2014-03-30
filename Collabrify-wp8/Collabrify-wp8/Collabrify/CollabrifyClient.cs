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

    // Session specific stuff

    //private sealed AtomicBoolean joined = new AtomicBoolean(false);
    //private sealed AtomicBoolean pending = new AtomicBoolean(false);
    //private sealed long lastOrderId = new AtomicLong(-1);
    //private sealed int sridCounter = new AtomicInteger(1);
    //private sealed AtomicLong lastOrderId = new AtomicLong(-1);
    //private sealed AtomicInteger sridCounter = new AtomicInteger(1);

    /**
     * set of srid, used to track srid that has been sent by addEvent, but not
     * retrieved by getEvent
     */
    /*private sealed Set<int> sridSet = new HashSet<int>();
    private sealed Map<long, int> oidMap = new HashMap<long, int>();
    private sealed Semaphore pendingEventsSemaphore = new Semaphore(0);
    private volatile String sessionPassword;
    private volatile long participantId;
    private volatile String notificationId;
    private volatile CollabrifySession currentSession;
    private BufferedInputStream baseFileInputStream;
    private BufferedOutputStream baseFileOutputStream;
    private File tempBaseFile;
    private long timeAdjustmentValue = 0;
    private CollabrifySessionLogger sessionLogger;
    private bool log = false;*/

    public ObservableCollection<Session_PB> session_list = new ObservableCollection<Session_PB>();

    private HttpRequest__Object http_object = new HttpRequest__Object();
    public CollabrifyParticipant participant = null;
    private CollabrifySession session = null;
    private bool eventsPaused = false;

    private event CreateSessionListener createSessionListener;


    // USED TO UPDATE MAINPAGE INFORMATION
    private string ret;
    public long id;

    private void BasicClientInitialize()
    {
      http_object.HttpRequestDone += new CollabrifyEventListener(httpReturned);

      makeWarmup();
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

    public void makeWarmup() { HttpRequest_Warmup.make_request(this, http_object); }
    public void makeListSession() { HttpRequest_ListSessions.make_request(this, http_object); }
    public void makeDeleteSession() { HttpRequest_DeleteSession.make_request(this, http_object); }



    public void createSession(string name, List<string> tags, string password, bool startPaused, CreateSessionListener completionHandler)
    {
      createSessionListener += completionHandler;
      HttpRequest_CreateSession.make_request(this, http_object, name, tags, password);
      
      // TODO: STARTPAUSE NOT USED YET... CALL IN CLIENT WHEN CREATESESSION RETURNS;
    }



    public void pauseEvents() { eventsPaused = true; }

    public void resumeEvents() { eventsPaused = false; }

    // TODO: listSessions
    public List<CollabrifySession> listSessions(List<string> tags) { return new List<CollabrifySession>(); }

    public bool isInSession() {
      if (session == null) return false;
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