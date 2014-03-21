using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Collabrify_v2.CollabrifyProtocolBuffer;

namespace Collabrify_wp8.Collabrify
{
    public class CollabrifyListener
    {
        public interface CollabrifyErrorListener
        {
         /**
          * Called when an error has occured
          * 
           * @param e
            *          Details on the error
           */
          
            //void onError(readonly CollabrifyException e);
        }

        public interface CollabrifySessionListener : CollabrifyErrorListener
        {


            /**
             * Called when the client has successfully uploaded the base file
             * 
             * @param baseFileSize
             *          The size of the base file
             */
            //void onBaseFileUploadComplete(readonly long baseFileSize);
            void onBaseFileUploadComplete(long baseFileSize);


            /**
             * Called when the basefile has been received. The client will not use the
             * base file after this method returns, and will attempt to delete it when
             * it leaves the session.
             * 
             * @param baseFile
             *          The base file
             */

            //void onBaseFileReceived(readonly File baseFile);
            void onBaseFileReceived(Stream baseFile);


            /**
             * Called when the client has received an event.
             * 
             * @param orderId
             *          The order id of the received event
             * @param submissionRegistrationId
             *          The submission registration id for the event
             * @param eventType
             *          The event type string passed in by the event broadcaster
             * @param data
             *          The event
             * @param elapsed
             *          Approximately how much time has passed since this event was
             *          originally received on the server in milliseconds
             */

           // void onReceiveEvent(readonly long orderId, int submissionRegistrationId,
           //     String eventType, readonly byte[] data, long elapsed);
            void onReceiveEvent( long orderId, int submissionRegistrationId,
                String eventType, byte[] data, long elapsed);

            /**
             * Called when a participant has joined the session
             * 
             * @param p
             *          the participant
             */
            void onParticipantJoined(CollabrifyParticipant p);

            /**
             * Called when a participant has left the session
             * 
             * @param p
             *          the participant
             */
            void onParticipantLeft(CollabrifyParticipant p);


            /**
             * Called when the current session has been ended by it's owner.
             * Participants can still retrieve events and leave the session, but they
             * can't broadcast or rejoin the session once they left
             * 
             * @param id
             *          Id of session that ended
             */

            //void onSessionEnd(readonly long id);
            void onSessionEnd(long id);


            /**
             * Called when the owner of the session has prevented other people from
             * joining this session
             */
            void onFurtherJoinsPrevented();
          }

          public interface CollabrifyCreateSessionListener : CollabrifyErrorListener
          {
            /**
             * called when a session is successfully created
             * 
             * @param session
             *          the session object
             */

            //void onSessionCreated(readonly CollabrifySession session);
            void onSessionCreated(CollabrifySession session);
          }

          public interface CollabrifyJoinSessionListener : CollabrifyErrorListener
          {
            /**
             * Called when the client has successfully joined a session
             * 
             * @param maxOrderId
             *          The order id of the latest event in this session
             * @param baseFileSize
             *          The size of the session's basefile, or 0 if there is no basefile
             */

            //void onSessionJoined(readonly long maxOrderId, readonly long baseFileSize);
            void onSessionJoined(long maxOrderId, long baseFileSize);
          }

          public interface CollabrifyListSessionsListener : CollabrifyErrorListener
          {
            /**
             * Called when the client has retrieved a session list
             * 
             * @param sessionList
             *          A list of sessions
             */

            //void onReceiveSessionList(readonly List<CollabrifySession> sessionList);
            void onReceiveSessionList(List<CollabrifySession> sessionList);
          }

          public interface CollabrifyBroadcastListener : CollabrifyErrorListener
          {
            /**
             * Called when the client has successfully broadcasted an event
             * 
             * @param event_var
             *          the event that is broadcasted
             * @param orderId
             *          the order id of the event
             * @param srid
             *          the submission registration id of the event
             */
            void onBroadcastDone(byte[] event_var, long orderId, long srid);
          }

          public interface CollabrifyLeaveSessionListener :  CollabrifyErrorListener
          {
            /**
             * Called when the client has successfully left the session
             */
            void onDisconnect();
          }
    }
}
