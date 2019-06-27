using System;
using System.Collections.Generic;
using System.Text;

namespace TianCheng.Sms.Tencent.Model
{
    public class JSONException : Exception
    {
        public JSONException()
        { }

        public JSONException(string message) : base(message)
        { }
    }
}
