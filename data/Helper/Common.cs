using System;
using System.Collections.Generic;
using System.Text;

namespace data.Helper
{
    public static class Common
    {
        #region Properties
        public class Response
        {
            public dynamic Result { get; set; }
            public string Message { get; set; }
            public int HttpStatus { get; set; }

        }
        #endregion
    }
}
