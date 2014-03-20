using Collabrify_v2.CollabrifyProtocolBuffer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Collabrify.http
{

    public class HttpRequest_GetBaseFile : HttpRequest_BasicCollabrifyPostRequest<Request_GetBaseFile_PB, Response_GetBaseFile_PB>
    {
      public static sealed String servletExtension = new ServletUrlExtension_PB().url_for_get_base_file;

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
       * @param readCallback
       *          This will be called when it is time to read trailing data. This
       *          can be null.
       */
      public HttpRequest_GetBaseFile(String baseURL,
          Request_GetBaseFile_PB request_pb, bool useUnifiedServlet,
          TrailingDataReadCallback readCallback)
          : base(baseURL,
            (useUnifiedServlet ? mainServletExtension : servletExtension),
            request_pb, CollabrifyRequestType_PB.GET_BASE_FILE_REQUEST,
            new Response_GetBaseFile_PB(), useUnifiedServlet, null,
            readCallback)
      { }// ctor

      // ---------------------------------------------------------------------------

    }// class

}
