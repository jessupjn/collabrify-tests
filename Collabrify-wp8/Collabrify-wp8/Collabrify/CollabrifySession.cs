using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Collabrify_v2.CollabrifyProtocolBuffer;
using System.Collections.ObjectModel;
using Collabrify_wp8;

namespace Collabrify_wp8.Collabrify
{
  public class CollabrifySession
  {
    private static readonly string PARTICIPANT_UPDATE_STRING = "Updating...";
    private readonly long id;
    private readonly string name;
    private readonly bool passwordProtected;
    private readonly CollabrifyParticipant owner;
    private readonly bool hasBaseFile;
    private readonly int baseFileSize;
    private readonly int participantLimit;
    private readonly List<string> sessionTags;
    private readonly long creationTime;
    private Dictionary<long, CollabrifyParticipant> participants = new Dictionary<long, CollabrifyParticipant>();
    private int participantCount;
    private long currentOrderId;
    private bool sessionEnded;
    // ---------------------------------------------------------------------------
    // ---------------------------------------------------------------------------

    CollabrifySession(Session_PB sessionPB)
    {
      id = sessionPB.session_id;
      name = sessionPB.session_name;
      passwordProtected = sessionPB.password_protected;
      owner = new CollabrifyParticipant(sessionPB.owner);
      hasBaseFile = sessionPB.base_file_complete;
      baseFileSize = hasBaseFile ? sessionPB.base_file_size : 0;
      currentOrderId = sessionPB.current_order_id;
      participantCount = sessionPB.participant_id.Count;
      participantLimit = sessionPB.participant_limit;
      sessionEnded = false;
      sessionTags = new List<string>(sessionPB.session_tag);

      foreach( Participant_PB p in sessionPB.participant )
      {
        participants.Add(p.participant_id, new CollabrifyParticipant(p));
      }
      creationTime = sessionPB.creation_timestamp;
    }// ctor

    // ---------------------------------------------------------------------------
































    public int getBaseFileSize()
    {
      return baseFileSize;
    } // getbaseFileSize

    // ---------------------------------------------------------------------------

    public long getCurrentOrderId()
    {
      return currentOrderId;
    }// getCurrentOrderId

    // ---------------------------------------------------------------------------

    public bool getHasBaseFile()
    {
      return hasBaseFile;
    }// getHasBaseFile

    // ---------------------------------------------------------------------------

    public long getId()
    {
      return id;
    }// getId

    // ---------------------------------------------------------------------------

    public long getCreationTime()
    {
      return creationTime;
    }// getCreationTime

    // ---------------------------------------------------------------------------

    public bool getIsPasswordProtected()
    {
      return passwordProtected;
    }// getIsPasswordProtected

    // ---------------------------------------------------------------------------

    public String getName()
    {
      return name;
    }// getName

    // ---------------------------------------------------------------------------

    public bool getSessionEnded()
    {
      return sessionEnded;
    }// getSessionEnded

		protected List<long> onChannelConnectSynchronization(IEnumerable<long> participantUpdateList)
	  {
		  // TODO left out printMethodName(TAG)
      IEnumerable<long> S1 = participants.Keys;

      // remove nonexistant participants 
		  foreach( long i in S1.Except(participantUpdateList) )
		  {
        participants.Remove(i);
		  }

      // add in new participants 
		  List<long> toUpdate = new List<long>();
      foreach (long i in S1.Except(participantUpdateList))
      {
        participants.Add( i,
          new CollabrifyParticipant(i, PARTICIPANT_UPDATE_STRING, 
            PARTICIPANT_UPDATE_STRING, (long) DateTime.Now.Ticks));

			  toUpdate.Add(i);
		  }

      return toUpdate;
    }// onChannelConnectSynchronization

    // ---------------------------------------------------------------------------

    protected void updateParticipantInfo(List<Participant_PB> participantList)
    {
      // TODO left out printMethodName(TAG)
      foreach ( Participant_PB p in participantList )
      {
        if( participants.ContainsKey(p.participant_id))
        {
          participants.Add(p.participant_id, new CollabrifyParticipant(p));
        }
      }

      // remove any participant that is still incomplete
      foreach ( CollabrifyParticipant p in participants.Values )
      {
        if( p.getEmail().Equals(PARTICIPANT_UPDATE_STRING) )
        {
          participants.Remove(p.getId());
        }
      }
    }// updateParticipantInfo

    // ---------------------------------------------------------------------------

    public CollabrifyParticipant getOwner()
    {
      return owner;
    }// getOwner

    // ---------------------------------------------------------------------------

    public int getParticipantCount()
    {
      return participantCount;
    }// getParticipantCount

    // ---------------------------------------------------------------------------

    public int getParticipantLimit()
    {
      return participantLimit;
    }// getParticipantLimit

    // ---------------------------------------------------------------------------

    public List<String> getSessionTags()
    {
      return sessionTags;
    }// getSessionTags

    // ---------------------------------------------------------------------------

    public String convertToString()
    {
      return "(" + name + "," + id + ")";
    }// convertToString

    // ---------------------------------------------------------------------------

    protected void addParticipant(CollabrifyParticipant p_)
    {
      // TODO left out printMethodName(TAG)
      participants.Add(p_.getId(), p_);
      participantCount = participants.Count();
    }// addParticipant

    // ---------------------------------------------------------------------------

    protected void endSession()
    {
      // TODO left out printMethodName(TAG)
      sessionEnded = true;
    }// endSession

    // ---------------------------------------------------------------------------

    protected CollabrifyParticipant getParticipant(long id)
    {
      IEnumerable<KeyValuePair<long, CollabrifyParticipant> > result = participants.Where(x => x.Key == id);
      if (result.Count() > 0) { // return if found
        return result.First().Value;
      }
      else { // not found
        return null;
      }
    }

    // ---------------------------------------------------------------------------

    protected ReadOnlyObservableCollection< CollabrifyParticipant > getParticipants()
    {
      ObservableCollection<CollabrifyParticipant> result = new ObservableCollection<CollabrifyParticipant>();

      foreach( KeyValuePair<long, CollabrifyParticipant> k in participants)
      {
        result.Add(k.Value);
      }

      return new ReadOnlyObservableCollection< CollabrifyParticipant >(result);
    }// getParticipants

    // ---------------------------------------------------------------------------

    protected CollabrifyParticipant removeParticipant(long participantId_)
    {
      // TODO left out printMethodName(TAG)
      CollabrifyParticipant p = participants.FirstOrDefault(x => x.Key == participantId_).Value;
      participants.Remove(participantId_);
      participantCount = participants.Count();
      return p;
    }

    // ---------------------------------------------------------------------------

    protected void updateCurrentOrderId(long newOrderId_)
    {
      // TODO left out printMethodName(TAG)
      currentOrderId = newOrderId_;
    }// updateCurrentOrderId

    // ---------------------------------------------------------------------------

  }// class
}
