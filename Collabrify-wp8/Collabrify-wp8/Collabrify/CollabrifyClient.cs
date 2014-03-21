﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Collabrify_wp8.Collabrify;
using Collabrify_v2.CollabrifyProtocolBuffer;
using System.IO;
using System.Threading;
using Collabrify_wp8.Http_Requests;

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
    private CollabrifyListener.CollabrifySessionListener sessionListener = null;

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

    public CollabrifyClient(string _gmail, string _accountGmail, string _access_token, bool _get_latest)
    {
      gmail = _gmail;
      accountGmail = _accountGmail;
      accessToken = _access_token;
      getLatest = _get_latest;

      HttpRequest_Warmup.make_request();
    }






    // TODO: listSessions
    public List<CollabrifySession> listSessions(List<string> tags) { return new List<CollabrifySession>(); }

    // TODO: isInSession
    public bool isInSession() { return false; }

    // TODO: currentSessionHasEnded
    public bool currentSessionHasEnded() { return false; }

    // TODO: currentSessionHasBaseFile
    public bool currentSessionHasBaseFile() { return false; }

    // TODO: currentSessionID
    public long currentSessionID() { return 0; }

    // TODO: currentSessionIsPasswordProtected
    public bool currentSessionIsPasswordProtected() { return false; }

    // TODO: currentSessionName
    public string currentSessionName() { return ""; }

    // TODO: currentSessionOrderID
    public long currentSessionOrderID() { return 0; }

    // TODO: current_session_owner
    public CollabrifyParticipant currentSessionOwner() { return null; }

    // TODO: current_session_participants
    public int currentSessionParticipantCount() { return 0; }

    // TODO: current_session_participants
    public List<CollabrifyParticipant> currentSessionParticipants() { return new List<CollabrifyParticipant>(); }

    // TODO: current_session_tags
    public List<string> currentSessionTags() { return new List<string>(); }

    // TODO: has network connection
    public bool hasNetworkConnection() { return true; }
  }
}