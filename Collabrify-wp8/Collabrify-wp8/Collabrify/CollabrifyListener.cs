using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Collabrify_v2.CollabrifyProtocolBuffer;
using Collabrify_wp8.Http_Requests;

namespace Collabrify_wp8.Collabrify
{
    // Delegates that correspond to the different types of responses that the server backend
    // will send to us.
    public delegate void CollabrifyEventListener(CollabrifyEventArgs e);
    public delegate void AddEventListener(AddEvent_Args e);
    public delegate void AddParticipantListener(AddParticipant_Args e);
    public delegate void AddToBaseFileListener(AddToBaseFile_Args e);
    public delegate void CreateSessionListener(CreateSession_Args e);
    public delegate void CreateOrGetUserListener(CreateOrGetUser_Args e);
    public delegate void CreateSessionWithBaseFileListener(CreateSessionWithBaseFile_Args e);
    public delegate void DeleteSessionListener(DeleteSession_Args e);
    public delegate void DeleteAllSessionsListener(DeleteAllSessions_Args e);
    public delegate void EndSessionListener(EndSession_Args e);
    public delegate void GetBaseFileListener(GetBaseFile_Args e);
    public delegate void GetCurrentOrderIDListener(GetCurrentOrderID_Args e);
    public delegate void GetEventListener(GetEvent_Args e);
    public delegate void GetEventBatchListener(GetEventBatch_Args e);
    public delegate void GetFromBaseFileListener(GetFromBaseFile_Args e);
    public delegate void GetNotificationIDListener(GetNotificationID_Args e);
    public delegate void GetParticipantListener(GetParticipant_Args e);
    public delegate void GetSessionListener(GetSession_Args e);
    public delegate void ListAccountsListener(ListAccounts_Args e);
    public delegate void ListSessionsListener(ListSessions_Args e);
    public delegate void PreventFurtherJoinsListener(PreventFurtherJoins_Args e);
    public delegate void RemoveParticipantListener(RemoveParticipant_Args e);
    public delegate void UpdateNotificationIDListener(UpdateNotificationID_Args e);
    public delegate void UpdateUserListener(UpdateUser_Args e);
    public delegate void WarmupListener(Warmup_Args e);

    // ------------------------------------------------------------------------------
    // ------------------------------------------------------------------------------

    public class CollabrifyEventArgs : EventArgs
    {
      public CollabrifyRequestType_PB type;
      public CollabrifyResponse_PB response;
      public object specificResponsePB;
      public object trailData;

      public CollabrifyEventArgs(CollabrifyResponse_PB response_pb, object specific_response_pb, CollabrifyRequestType_PB request_type)
      {
        type = request_type;
        specificResponsePB = specific_response_pb;
        response = response_pb;
      }

    } // CollabrifyEventArgs

    // ------------------------------------------------------------------------------
    // ------------------------------------------------------------------------------

    public class AddEvent_Args : EventArgs
    {
      public Response_AddEvent_PB returned_data;
      public AddEvent_Args(object _returned_data)
      {
        returned_data = _returned_data as Response_AddEvent_PB;
      }
    } // AddEvent_Args

    // ------------------------------------------------------------------------------
    // ------------------------------------------------------------------------------

    public class AddParticipant_Args : EventArgs
    {
      public Response_AddParticipant_PB returned_data;
      public AddParticipant_Args(object _returned_data)
      {
        returned_data = _returned_data as Response_AddParticipant_PB;
      }
    } // AddParticipant_Args

    // ------------------------------------------------------------------------------
    // ------------------------------------------------------------------------------

    public class AddToBaseFile_Args : EventArgs
    {
      public Response_AddToBaseFile_PB returned_data;
      public AddToBaseFile_Args(object _returned_data)
      {
        returned_data = _returned_data as Response_AddToBaseFile_PB;
      }
    } // AddToBaseFile_Args

    // ------------------------------------------------------------------------------
    // ------------------------------------------------------------------------------

    public class CreateOrGetUser_Args : EventArgs
    {
      public Response_CreateOrGetUser_PB returned_data;
      public CreateOrGetUser_Args(object _returned_data)
      {
        returned_data = _returned_data as Response_CreateOrGetUser_PB;
      }
    } // CreateOrGetUser_Args

    // ------------------------------------------------------------------------------
    // ------------------------------------------------------------------------------

    public class CreateSession_Args : EventArgs
    {
      public Response_CreateSession_PB returned_data;
      public CreateSession_Args(object _returned_data)
      {
        returned_data = _returned_data as Response_CreateSession_PB;
      }
    } // CreateSession_Args

    // ------------------------------------------------------------------------------
    // ------------------------------------------------------------------------------

    public class CreateSessionWithBaseFile_Args : EventArgs
    {
      public Response_CreateSessionWithBaseFile_PB returned_data;
      public CreateSessionWithBaseFile_Args(object _returned_data)
      {
        returned_data = _returned_data as Response_CreateSessionWithBaseFile_PB;
      }
    } // CreateSessionWithBaseFile_Args

    // ------------------------------------------------------------------------------
    // ------------------------------------------------------------------------------

    public class DeleteSession_Args : EventArgs
    {
      public Response_DeleteSession_PB returned_data;
      public DeleteSession_Args(object _returned_data)
      {
        returned_data = _returned_data as Response_DeleteSession_PB;
      }
    } // DeleteSession_Args

    // ------------------------------------------------------------------------------
    // ------------------------------------------------------------------------------

    public class DeleteAllSessions_Args : EventArgs
    {
      public Response_DeleteAllSessions_PB returned_data;
      public DeleteAllSessions_Args(object _returned_data)
      {
        returned_data = _returned_data as Response_DeleteAllSessions_PB;
      }
    } // DeleteAllSessions_Args

    // ------------------------------------------------------------------------------
    // ------------------------------------------------------------------------------

    public class EndSession_Args : EventArgs
    {
      public Response_EndSession_PB returned_data;
      public EndSession_Args(object _returned_data)
      {
        returned_data = _returned_data as Response_EndSession_PB;
      }
    } // EndSession_Args

    // ------------------------------------------------------------------------------
    // ------------------------------------------------------------------------------

    public class GetBaseFile_Args : EventArgs
    {
      public Response_GetBaseFile_PB returned_data;
      public GetBaseFile_Args(object _returned_data)
      {
        returned_data = _returned_data as Response_GetBaseFile_PB;
      }
    } // GetBaseFile_Args

    // ------------------------------------------------------------------------------
    // ------------------------------------------------------------------------------

    public class GetCurrentOrderID_Args : EventArgs
    {
      public Response_GetCurrentOrderID_PB returned_data;
      public GetCurrentOrderID_Args(object _returned_data)
      {
        returned_data = _returned_data as Response_GetCurrentOrderID_PB;
      }
    } // GetCurrentOrderID_Args

    // ------------------------------------------------------------------------------
    // ------------------------------------------------------------------------------

    public class GetEvent_Args : EventArgs
    {
      public Response_GetEvent_PB returned_data;
      public GetEvent_Args(object _returned_data)
      {
        returned_data = _returned_data as Response_GetEvent_PB;
      }
    } // GetEvent_Args

    // ------------------------------------------------------------------------------
    // ------------------------------------------------------------------------------

    public class GetEventBatch_Args : EventArgs
    {
      public Response_GetEventBatch_PB returned_data;
      public GetEventBatch_Args(object _returned_data)
      {
        returned_data = _returned_data as Response_GetEventBatch_PB;
      }
    } // GetEventBatch_Args

    // ------------------------------------------------------------------------------
    // ------------------------------------------------------------------------------

    public class GetFromBaseFile_Args : EventArgs
    {
      public Response_GetFromBaseFile_PB returned_data;
      public GetFromBaseFile_Args(object _returned_data)
      {
        returned_data = _returned_data as Response_GetFromBaseFile_PB;
      }
    } // GetFromBaseFile_Args

    // ------------------------------------------------------------------------------
    // ------------------------------------------------------------------------------

    public class GetLastEventByParticipant_Args : EventArgs
    {
      public Response_GetLastEventByParticipant_PB returned_data;
      public GetLastEventByParticipant_Args(object _returned_data)
      {
        returned_data = _returned_data as Response_GetLastEventByParticipant_PB;
      }
    } // GetLastEventByParticipant_Args

    // ------------------------------------------------------------------------------
    // ------------------------------------------------------------------------------

    public class GetNotificationID_Args : EventArgs
    {
      public Response_GetNotificationID_PB returned_data;
      public GetNotificationID_Args(object _returned_data)
      {
        returned_data = _returned_data as Response_GetNotificationID_PB;
      }
    } // GetNotificationID_Args

    // ------------------------------------------------------------------------------
    // ------------------------------------------------------------------------------

    public class GetParticipant_Args : EventArgs
    {
      public Response_GetParticipant_PB returned_data;
      public GetParticipant_Args(object _returned_data)
      {
        returned_data = _returned_data as Response_GetParticipant_PB;
      }
    } // GetParticipant_Args

    // ------------------------------------------------------------------------------
    // ------------------------------------------------------------------------------

    public class GetSession_Args : EventArgs
    {
      public Response_GetSession_PB returned_data;
      public GetSession_Args(object _returned_data)
      {
        returned_data = _returned_data as Response_GetSession_PB;
      }
    } // GetSession_Args

    // ------------------------------------------------------------------------------
    // ------------------------------------------------------------------------------

    public class ListAccounts_Args : EventArgs
    {
      public Response_ListAccounts_PB returned_data;
      public ListAccounts_Args(object _returned_data)
      {
        returned_data = _returned_data as Response_ListAccounts_PB;
      }
    } // ListAccounts_Args

    // ------------------------------------------------------------------------------
    // ------------------------------------------------------------------------------

    public class ListSessions_Args : EventArgs
    {
      public Response_ListSessions_PB returned_data;
      public ListSessions_Args(object _returned_data)
      {
        returned_data = _returned_data as Response_ListSessions_PB;
      }
    } // ListSessions_Args

    // ------------------------------------------------------------------------------
    // ------------------------------------------------------------------------------

    public class PreventFurtherJoins_Args : EventArgs
    {
      public Response_PreventFurtherJoins_PB returned_data;
      public PreventFurtherJoins_Args(object _returned_data)
      {
        returned_data = _returned_data as Response_PreventFurtherJoins_PB;
      }
    } // PreventFurtherJoins_Args

    // ------------------------------------------------------------------------------
    // ------------------------------------------------------------------------------

    public class RemoveParticipant_Args : EventArgs
    {
      public Response_RemoveParticipant_PB returned_data;
      public RemoveParticipant_Args(object _returned_data)
      {
        returned_data = _returned_data as Response_RemoveParticipant_PB;
      }
    } // RemoveParticipant_Args

    // ------------------------------------------------------------------------------
    // ------------------------------------------------------------------------------

    public class UpdateNotificationID_Args : EventArgs
    {
      public Response_UpdateNotificationID_PB returned_data;
      public UpdateNotificationID_Args(object _returned_data)
      {
        returned_data = _returned_data as Response_UpdateNotificationID_PB;
      }
    } // UpdateNotificationID_Args

    // ------------------------------------------------------------------------------
    // ------------------------------------------------------------------------------

    public class UpdateUser_Args : EventArgs
    {
      public Response_UpdateUser returned_data;
      public UpdateUser_Args(object _returned_data)
      {
        returned_data = _returned_data as Response_UpdateUser;
      }
    } // UpdateUser_Args

    // ------------------------------------------------------------------------------
    // ------------------------------------------------------------------------------

    public class Warmup_Args : EventArgs
    {
      public Response_Warmup_PB returned_data;
      public Warmup_Args(object _returned_data)
      {
        returned_data = _returned_data as Response_Warmup_PB;
      }
    } // Warmup_Args

    // ------------------------------------------------------------------------------
    // ------------------------------------------------------------------------------

}
