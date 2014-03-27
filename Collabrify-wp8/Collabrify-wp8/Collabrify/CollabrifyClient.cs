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
  public delegate void ChangedEventHander(object sender, EventArgs e);

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
    public bool success_flag;

    private HttpRequest__Object http_object = new HttpRequest__Object();
    private CollabrifyParticipant participant = null;
    private CollabrifySession session = null;
    private bool eventsPaused = false;

    private event ChangedEventHander ListSessionsHandler;



    // USED TO UPDATE MAINPAGE INFORMATION
    public event ChangedEventHander ReturnInformation;
    private string ret;
    public long id;

    private void BasicClientInitialize()
    {
      http_object.Changed += new ChangedEventHander(httpReturned);

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

    private void httpReturned(object sender, EventArgs e)
    {
      /* Handles the response from the server within the CollabrifyClient object */
      
      CollabrifyResponse_PB res_pb = http_object.response_object_pb as CollabrifyResponse_PB;
      ret = "SUCCESS FLAG:   " + res_pb.success_flag.ToString();
      ret += "\nTYPE:   " + http_object.response_type.ToString();
      Debug.WriteLine(ret);

      if(ReturnInformation != null) ReturnInformation.Invoke(ret, EventArgs.Empty);


      switch (http_object.response_type)
      {
        case CollabrifyRequestType_PB.CREATE_SESSION_REQUEST:
          Debug.WriteLine("Adding Session: " + (http_object.response_specific_pb as Response_CreateSession_PB).session.session_id.ToString());
          session_list.Add( (http_object.response_specific_pb as Response_CreateSession_PB).session);
          id = (http_object.response_specific_pb as Response_CreateSession_PB).owner.participant_id;
          break;
        case CollabrifyRequestType_PB.DELETE_SESSION_REQUEST:
          session_list.RemoveAt(0);
          break;
        case CollabrifyRequestType_PB.LIST_SESSIONS_REQUEST:
          ListSessionsHandler.Invoke(this, EventArgs.Empty);
          break;
      }
    }

    public void makeWarmup() { HttpRequest_Warmup.make_request(this, http_object); }
    public void makeCreateSession() { HttpRequest_CreateSession.make_request(this, http_object); }
    public void makeListSession() { HttpRequest_ListSessions.make_request(this, http_object); }
    public void makeDeleteSession() { HttpRequest_DeleteSession.make_request(this, http_object); }










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