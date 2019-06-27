using System;
using System.Collections.Generic;
using System.Text;
using TianCheng.Model;

namespace TianCheng.Sms
{
    /// <summary>
    /// 基础配置
    /// </summary>
    public class BaseConfigInfo : BusinessMongoModel
    {
        /// <summary>
        /// 短信服务商类型
        /// </summary>
        public SmsProviderEnum Type { get; set; }
        /// <summary>
        /// AppId
        /// </summary>
        public int AppId { get; set; }
        /// <summary>
        /// AppKey
        /// </summary>
        public string AppKey { get; set; }
        /// <summary>
        /// 短信签名
        /// </summary>
        public string SmsSign { get; set; }
    }
}
