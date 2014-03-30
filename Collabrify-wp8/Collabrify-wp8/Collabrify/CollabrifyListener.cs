using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Collabrify_v2.CollabrifyProtocolBuffer;
using Collabrify_wp8.Http_Requests;

namespace Collabrify_wp8.Collabrify
{
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

    }

    public class AddEvent_Args : EventArgs
    {
      private Response_AddEvent_PB returned_data;
      public AddEvent_Args(object _returned_data)
      {
        returned_data = _returned_data as Response_AddEvent_PB;
      }
      public Response_AddEvent_PB getReturnedData() { return returned_data; }
    }

    public class AddParticipant_Args : EventArgs
    {
      private Response_AddParticipant_PB returned_data;
      public AddParticipant_Args(object _returned_data)
      {
        returned_data = _returned_data as Response_AddParticipant_PB;
      }
      public Response_AddParticipant_PB getReturnedData() { return returned_data; }
    }

    public class AddToBaseFile_Args : EventArgs
    {
      private Response_AddToBaseFile_PB returned_data;
      public AddToBaseFile_Args(object _returned_data)
      {
        returned_data = _returned_data as Response_AddToBaseFile_PB;
      }
      public Response_AddToBaseFile_PB getReturnedData() { return returned_data; }
    }

    public class CreateOrGetUser_Args : EventArgs
    {
      private Response_CreateOrGetUser_PB returned_data;
      public CreateOrGetUser_Args(object _returned_data)
      {
        returned_data = _returned_data as Response_CreateOrGetUser_PB;
      }
      public Response_CreateOrGetUser_PB getReturnedData() { return returned_data; }
    }

    public class CreateSession_Args : EventArgs
    {
      private Response_CreateSession_PB returned_data;
      public CreateSession_Args(object _returned_data)
      {
        returned_data = _returned_data as Response_CreateSession_PB;
      }
      public Response_CreateSession_PB getReturnedData() { return returned_data; }
    }

    public class CreateSessionWithBaseFile_Args : EventArgs
    {
      private Response_CreateSessionWithBaseFile_PB returned_data;
      public CreateSessionWithBaseFile_Args(object _returned_data)
      {
        returned_data = _returned_data as Response_CreateSessionWithBaseFile_PB;
      }
      public Response_CreateSessionWithBaseFile_PB getReturnedData() { return returned_data; }
    }

    public class DeleteSession_Args : EventArgs
    {
      private Response_DeleteSession_PB returned_data;
      public DeleteSession_Args(object _returned_data)
      {
        returned_data = _returned_data as Response_DeleteSession_PB;
      }
      public Response_DeleteSession_PB getReturnedData() { return returned_data; }
    }

    public class DeleteAllSessions_Args : EventArgs
    {
      private Response_DeleteAllSessions_PB returned_data;
      public DeleteAllSessions_Args(object _returned_data)
      {
        returned_data = _returned_data as Response_DeleteAllSessions_PB;
      }
      public Response_DeleteAllSessions_PB getReturnedData() { return returned_data; }
    }

    public class EndSession_Args : EventArgs
    {
      private Response_EndSession_PB returned_data;
      public EndSession_Args(object _returned_data)
      {
        returned_data = _returned_data as Response_EndSession_PB;
      }
      public Response_EndSession_PB getReturnedData() { return returned_data; }
    }

    public class GetBaseFile_Args : EventArgs
    {
      private Response_GetBaseFile_PB returned_data;
      public GetBaseFile_Args(object _returned_data)
      {
        returned_data = _returned_data as Response_GetBaseFile_PB;
      }
      public Response_GetBaseFile_PB getReturnedData() { return returned_data; }
    }

    public class GetCurrentOrderID_Args : EventArgs
    {
      private Response_GetCurrentOrderID_PB returned_data;
      public GetCurrentOrderID_Args(object _returned_data)
      {
        returned_data = _returned_data as Response_GetCurrentOrderID_PB;
      }
      public Response_GetCurrentOrderID_PB getReturnedData() { return returned_data; }
    }

    public class GetEvent_Args : EventArgs
    {
      private Response_GetEvent_PB returned_data;
      public GetEvent_Args(object _returned_data)
      {
        returned_data = _returned_data as Response_GetEvent_PB;
      }
      public Response_GetEvent_PB getReturnedData() { return returned_data; }
    }

    public class GetEventBatch_Args : EventArgs
    {
      private Response_GetEventBatch_PB returned_data;
      public GetEventBatch_Args(object _returned_data)
      {
        returned_data = _returned_data as Response_GetEventBatch_PB;
      }
      public Response_GetEventBatch_PB getReturnedData() { return returned_data; }
    }

    public class GetFromBaseFile_Args : EventArgs
    {
      private Response_GetFromBaseFile_PB returned_data;
      public GetFromBaseFile_Args(object _returned_data)
      {
        returned_data = _returned_data as Response_GetFromBaseFile_PB;
      }
      public Response_GetFromBaseFile_PB getReturnedData() { return returned_data; }
    }

    public class GetLastEventByParticipant_Args : EventArgs
    {
      private Response_GetLastEventByParticipant_PB returned_data;
      public GetLastEventByParticipant_Args(object _returned_data)
      {
        returned_data = _returned_data as Response_GetLastEventByParticipant_PB;
      }
      public Response_GetLastEventByParticipant_PB getReturnedData() { return returned_data; }
    }

    public class GetNotificationID_Args : EventArgs
    {
      private Response_GetNotificationID_PB returned_data;
      public GetNotificationID_Args(object _returned_data)
      {
        returned_data = _returned_data as Response_GetNotificationID_PB;
      }
      public Response_GetNotificationID_PB getReturnedData() { return returned_data; }
    }

    public class GetParticipant_Args : EventArgs
    {
      private Response_GetParticipant_PB returned_data;
      public GetParticipant_Args(object _returned_data)
      {
        returned_data = _returned_data as Response_GetParticipant_PB;
      }
      public Response_GetParticipant_PB getReturnedData() { return returned_data; }
    }

    public class GetSession_Args : EventArgs
    {
      private Response_GetSession_PB returned_data;
      public GetSession_Args(object _returned_data)
      {
        returned_data = _returned_data as Response_GetSession_PB;
      }
      public Response_GetSession_PB getReturnedData() { return returned_data; }
    }

    public class ListAccounts_Args : EventArgs
    {
      private Response_ListAccounts_PB returned_data;
      public ListAccounts_Args(object _returned_data)
      {
        returned_data = _returned_data as Response_ListAccounts_PB;
      }
      public Response_ListAccounts_PB getReturnedData() { return returned_data; }
    }

    public class ListSessions_Args : EventArgs
    {
      private Response_ListSessions_PB returned_data;
      public ListSessions_Args(object _returned_data)
      {
        returned_data = _returned_data as Response_ListSessions_PB;
      }
      public Response_ListSessions_PB getReturnedData() { return returned_data; }
    }

    public class PreventFurtherJoins_Args : EventArgs
    {
      private Response_PreventFurtherJoins_PB returned_data;
      public PreventFurtherJoins_Args(object _returned_data)
      {
        returned_data = _returned_data as Response_PreventFurtherJoins_PB;
      }
      public Response_PreventFurtherJoins_PB getReturnedData() { return returned_data; }
    }

    public class RemoveParticipant_Args : EventArgs
    {
      private Response_RemoveParticipant_PB returned_data;
      public RemoveParticipant_Args(object _returned_data)
      {
        returned_data = _returned_data as Response_RemoveParticipant_PB;
      }
      public Response_RemoveParticipant_PB getReturnedData() { return returned_data; }
    }

    public class UpdateNotificationID_Args : EventArgs
    {
      private Response_UpdateNotificationID_PB returned_data;
      public UpdateNotificationID_Args(object _returned_data)
      {
        returned_data = _returned_data as Response_UpdateNotificationID_PB;
      }
      public Response_UpdateNotificationID_PB getReturnedData() { return returned_data; }
    }

    public class UpdateUser_Args : EventArgs
    {
      private Response_UpdateUser returned_data;
      public UpdateUser_Args(object _returned_data)
      {
        returned_data = _returned_data as Response_UpdateUser;
      }
      public Response_UpdateUser getReturnedData() { return returned_data; }
    }

    public class Warmup_Args : EventArgs
    {
      private Response_Warmup_PB returned_data;
      public Warmup_Args(object _returned_data)
      {
        returned_data = _returned_data as Response_Warmup_PB;
      }
      public Response_Warmup_PB getReturnedData() { return returned_data; }
    }


}
