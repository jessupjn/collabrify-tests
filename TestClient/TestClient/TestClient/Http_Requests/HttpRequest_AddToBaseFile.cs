using Collabrify_v2.CollabrifyProtocolBuffer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Collabrify
{

    public class HttpRequest_AddToBaseFile : HttpRequest_BasicCollabrifyPostRequest<Request_AddToBaseFile_PB, Response_AddToBaseFile_PB>
    {
      public static sealed String servletExtension = new ServletUrlExtension_PB().url_for_add_to_base_file;

      // ---------------------------------------------------------------------------
      // ---------------------------------------------------------------------------

      /**
       * 
       * @param baseURL
       * 
       * @param request_pb
       * 
       * @param useUnifiedServlet
       *          If true, the request object will be prefixed by
       *          CollabrifyRequest_PB, and will be sent to the unified servlet.
       *          Otherwise, the request object will be sent alone to the old
       *          specific servlet.
       * 
       * @param writeCallback
       *          This will be called when it is time to write trailing data. This
       *          can be null.
       * 
       */
      public HttpRequest_AddToBaseFile(String baseURL,
          Request_AddToBaseFile_PB request_pb, bool useUnifiedServlet,
          TrailingDataWriteCallback writeCallback)
          : base(baseURL,
            (useUnifiedServlet ? mainServletExtension : servletExtension),
            request_pb, CollabrifyRequestType_PB.ADD_TO_BASE_FILE_REQUEST,
            new Response_AddToBaseFile_PB(), useUnifiedServlet,
            writeCallback, null)
      { }// ctor

      // ---------------------------------------------------------------------------

    }// class

}
