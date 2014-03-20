using Collabrify_v2.CollabrifyProtocolBuffer;
using IMLC.IMLCProtocolBuffer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Collabrify
{

    /**
     * This is an app-specific subclass of HttpRequest_BasicPostRequest. It must be
     * further subclassed for request-specific actions.
     */
    public abstract class HttpRequest_BasicCollabrifyPostRequest<AbstractMessageLite, AbstractMessageLite>
    : HttpRequest_BasicPostRequest<AbstractMessageLite, AbstractMessageLite, Exception_PB, CollabrifyExceptionType_PB>
    {
      public static sealed String mainServletExtension = new ServletUrlExtension_PB().url_for_all_requests;

      // This is used when writing the request wrapper.
      private CollabrifyRequestType_PB requestType;

      // This class can automatically handle callbacks to the client code on behalf
      // of the derived class.
      private TrailingDataReadCallback trailingDataReadCallback;
      private TrailingDataWriteCallback trailingDataWriteCallback;

      // Note: CollabrifyRequest_PB does not have an internal field to hold the
      // actual request. So, if this flag is true, the actual delimited request will
      // follow the delimited CollabrifyRequest_PB in the stream.
      private bool useUnifiedServlet = true;

      private RESPONSE responseDefaultInstance;
      private CollabrifyResponse_PB responseWrapper;
      private String clientVersion;

      // ---------------------------------------------------------------------------
      // ---------------------------------------------------------------------------

      /**
       * This is called after writing the CollabrifyRequest_PB (if
       * useUnifiedServlet==true) and Request_XXXX_PB to the output stream. Derived
       * classes can override this method to write any trailing data. If the
       * TrailingDataWriteCallback provided in the constructor is not null, then the
       * default (non-overridden) version of this method will call it automatically.
       * If the callback is null, then this a no-op.
       * 
       * @param outputStream
       *          The stream after the request has already been written.
       * 
       * @param requestWrapper
       *          Provided for convenience. The CollabrifyRequest_PB if
       *          useUnifiedServlet is true; null, otherwise.
       * 
       * @param requestObject
       *          Provided for convenience. If there is trailing data to be written,
       *          this should ALREADY contain a field specifying how many bytes will
       *          follow this object.
       * 
       * @param connection
       *          The actual connection object. Use this to set headers, etc. Do NOT
       *          call connection.getOutputStream().
       * 
       * @throws Exception
       *           All exceptions are passed up through the stack and can be caught
       *           at the call to execute().
       */
      public void doTrailingWriteCallback(OutputStream outputStream,
          CollabrifyRequest_PB requestWrapper, REQUEST requestObject,
          HttpURLConnection connection) throws Exception
      {
        if( trailingDataWriteCallback != null )
        {
          trailingDataWriteCallback.write(outputStream);
        }
      }// doTrailingWriteCallback

      // ---------------------------------------------------------------------------

      /**
       * This is called after reading the CollabrifyResponse_PB and Response_XXXX_PB
       * from the input stream. Derived classes can override this method to read
       * trailing data. If the TrailingDataReadCallback provided in the constructor
       * is not null, then the default (non-overridden) version of this method will
       * call it automatically. If the callback is null, then this a no-op.
       * 
       * @param inputStream
       *          The stream from the connection after the CollabrifyResponse_PB and
       *          response object (which either followed or was contained by the
       *          CollabrifyResponse_PB) have been parsed out. If there is any
       *          trailing data, it should be read here. If the responseObject was
       *          contained inside the responseWrapper, then this field will be
       *          null.
       * 
       * @param responseWrapper
       *          Provided for convenience
       * 
       * @param responseObject
       *          Provided for convenience. In the event of trailing data, this
       *          should indicate the number of valid bytes to read from the stream.
       * 
       * @param connection
       *          The actual connection object. Use this to examine the response
       *          code, etc. Do NOT call connection.getInputStream().
       * 
       * @throws Exception
       *           All exceptions are passed up through the stack and can be caught
       *           at the call to execute().
       */
      public void doTrailingDataReadCallback(InputStream inputStream,
          CollabrifyResponse_PB responseWrapper, RESPONSE responseObject,
          HttpURLConnection connection) throws Exception
      {
        if( trailingDataReadCallback != null )
        {
          trailingDataReadCallback.read(inputStream);
        }
      }// doTrailingDataReadCallback

      // ---------------------------------------------------------------------------
      // ---------------------------------------------------------------------------

      /**
       * @param baseURL_
       * @param servletExtension_
       *          If this is non-null, it will be appended to the baseURL.
       *          Otherwise, the baseURL will be used alone.
       * @param request_pb
       *          The protocol buffer message.
       * 
       * @param requestType_
       *          The type of request this is.
       * 
       * @param defaultInstance
       *          This has to do with Java Generics. Just use
       *          RESPONSE_TYPE.getDefaultInstance() for this parameter. If this
       *          value is null, the specific response will not be read.
       * 
       * @param useUnifiedServlet_
       *          If true, a delimited CollabrifyRequest_PB will be written before
       *          the (delimited) request object. If false, the delimited request
       *          object will be sent alone.
       * 
       * @param writeCallback_
       *          This can be called when it is time to write trailing data. The
       *          value can be null. If the writeCallback() method is not overridden
       *          and this value is not null, then it will be called automatically.
       * 
       * @param readCallback_
       *          This can be called when it is time to read trailing data. The
       *          value can be null. If the readCallback() method is not overridden
       *          and this value is not null, then it will be called automatically.
       */
      public HttpRequest_BasicCollabrifyPostRequest(String baseURL_,
          String servletExtension_, REQUEST request_pb,
          CollabrifyRequestType_PB requestType_,
          RESPONSE responseDefaultInstance_pb, bool useUnifiedServlet_,
          TrailingDataWriteCallback writeCallback_,
          TrailingDataReadCallback readCallback_)
        : base(baseURL_, servletExtension_)
      {
        setRequestObject(request_pb);
        responseDefaultInstance = responseDefaultInstance_pb;
        requestType = requestType_;
        useUnifiedServlet = useUnifiedServlet_;
        trailingDataWriteCallback = writeCallback_;
        trailingDataReadCallback = readCallback_;

        if( responseDefaultInstance == null )
        {
          throw new IllegalArgumentException(
              "responseDefaultInstance cannot be null");
        }
      }// ctor

      // ---------------------------------------------------------------------------

      protected override sealed void writeRequest(OutputStream outputStream,
          HttpURLConnection connection) throws Exception
      {
        //Util_IMLC.printMethodName(this);

        CollabrifyRequest_PB.Builder builder = CollabrifyRequest_PB.newBuilder();

        builder.setRequestType(requestType);

        if( clientVersion != null )
        {
          builder.setClientVersion(clientVersion);
        }

        CollabrifyRequest_PB requestWrapper = builder.build();

        if( useUnifiedServlet )
        {
          requestWrapper.writeDelimitedTo(outputStream);
        }

        getRequestObject().writeDelimitedTo(outputStream);

        // Now allow trailing data to be written
        doTrailingWriteCallback(outputStream, requestWrapper, getRequestObject(),
            connection);
      }// writeRequest

      // ---------------------------------------------------------------------------

      //@SuppressWarnings({ "deprecation", "unchecked" })
      protected override sealed int readResponse(InputStream inputStream,
          HttpURLConnection connection) throws Exception
      {
        Util_IMLC.printMethodName(this);

        // Read wrapper
        responseWrapper = CollabrifyResponse_PB.parseDelimitedFrom(inputStream);

        // Make sure there is a success flag
        if( !responseWrapper.hasSuccessFlag() )
        {
          throw new IllegalStateException("CollabrifyResponse_PB did not have successFlag set");
        }

        // Handle successFlag==false
        if( !responseWrapper.getSuccessFlag() )
        {

          // Make sure we have an exception
          if( !responseWrapper.hasException() )
          {
            throw new IllegalStateException("CollabrifyResponse_PB: Success flag was false but Exception_PB was not set");
          }

          // Make sure we have the exception type
          if( !responseWrapper.hasExceptionType() )
          {
            throw new IllegalStateException("CollabrifyResponse_PB: Success flag was false but ExceptionType_PB was not set");
          }

          setExceptionType(responseWrapper.getExceptionType());
          setExceptionParameter(responseWrapper.getException());
          return RESULT_FAILURE;
        }// if

        // At this point we proceed with handling successFlag==true

        boolean responseContainedInsideWrapper = responseWrapper
            .hasResponsePbBytes();
        if( responseDefaultInstance != null && responseContainedInsideWrapper )
        {
          // The response is old-style (i.e. wrapped inside).
          setResponseObject((RESPONSE) responseDefaultInstance.getParserForType()
              .parseFrom(responseWrapper.getResponsePbBytes()));
        }// if
        else if( responseDefaultInstance != null && !responseContainedInsideWrapper )
        {
          // The response is unified-style (i.e. follows the wrapper).
          setResponseObject((RESPONSE) responseDefaultInstance.getParserForType()
              .parseDelimitedFrom(inputStream));
        }// else if

        // Let the derived class take further action.
        // Note: If this was an old-style response, we don't even expose the input
        // stream, to be on the safe side.
        doTrailingDataReadCallback((responseContainedInsideWrapper ? null
            : inputStream), responseWrapper, getResponseObject(), connection);

        return RESULT_NORMAL;
      }// readResponse

      // ---------------------------------------------------------------------------

      public bool isUnifiedRequest()
      {
        return useUnifiedServlet;
      }

      // ---------------------------------------------------------------------------

      public String getClientVersion()
      {
        return clientVersion;
      }

      // ---------------------------------------------------------------------------

      public void setClientVersion(String clientVersion_)
      {
        clientVersion = clientVersion_;
      }

      // ---------------------------------------------------------------------------

      public override RuntimeException getException()
      {
        if( getExceptionParameter() == null )
        {
          throw new IllegalStateException(
              "Cannot call getNativeException() if setException() has not been called.");
        }

        return ExceptionTranslator.translate(getExceptionParameter());
      }// getException

      // ---------------------------------------------------------------------------

      public override bool getSuccessFlag()
      {
        // Make sure there is a success flag
        if( !responseWrapper.hasSuccessFlag() )
        {
          throw new IllegalStateException(
              "CollabrifyResponse_PB did not have successFlag set");
        }

        return responseWrapper.getSuccessFlag();
      }// getSuccessFlag

      // ---------------------------------------------------------------------------

    }// class


}
