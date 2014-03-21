using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Collabrify_v2.CollabrifyProtocolBuffer;

namespace Collabrify_wp8.Collabrify
{
  public class CollabrifyParticipant
  {
    private readonly long id;
    private readonly string displayName;
    private readonly string email;
    private readonly long joinTime;

    // ---------------------------------------------------------------------------
    // ---------------------------------------------------------------------------

    public CollabrifyParticipant(Participant_PB participant)
    {
      id = participant.participant_id;
      displayName = participant.display_name;
      email = participant.g_mail;
      joinTime = participant.joined_timestamp;
    } // ctor

    // ---------------------------------------------------------------------------

    public CollabrifyParticipant(long id_, string displayName_, string email_, long joinTime_)
    {
      id = id_;
      displayName = displayName_;
      email = email_;
      joinTime = joinTime_;
    } // ctor

    // -------------------------------------------------------------------------

    public long getId()
    {
      return id;
    } // getId

    // -------------------------------------------------------------------------

    public string getDisplayName()
    {
      return displayName;
    } // getDisplayName

    // -------------------------------------------------------------------------

    public string getEmail()
    {
      return email;
    } // getEmail

    // -------------------------------------------------------------------------

    public long getJoinTime()
    {
      return joinTime;
    } // getJoinTime

    // -------------------------------------------------------------------------

  }
}
