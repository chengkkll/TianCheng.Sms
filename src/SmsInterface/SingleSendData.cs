using System;
using System.Collections.Generic;
using System.Text;

namespace TianCheng.Sms
{
    /// <summary>
    /// 单条短信发送对象
    /// </summary>
    public class SingleSendData
    {
        /// <summary>
        /// 收件人电话号码
        /// </summary>
        public string PhoneNumber { get; set; }
        /// <summary>
        /// 短信内容
        /// </summary>
        public string Message { get; set; }
    }
}
