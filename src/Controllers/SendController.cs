using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using Swashbuckle.AspNetCore.SwaggerGen;
using System;
using System.Collections.Generic;
using System.Text;
using TianCheng.BaseService;
using TianCheng.Model;
using TianCheng.Sms;
using TianCheng.Sms.Tencent.Services;

namespace TianCheng.SystemCommon
{
    /// <summary>
    /// 短信管理
    /// </summary>
    [Produces("application/json")]
    [Route("api/System/Sms")]
    public class SendController : DataController
    {
        #region 构造方法
        private readonly TencentSmsService _Service;

        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="service"></param>
        public SendController(TencentSmsService service)
        {
            _Service = service;
        }
        #endregion

        /// <summary>
        /// 发送单条短信
        /// </summary>
        /// <param name="view">发送信息</param>
        /// <power>发送短信</power>
        [Microsoft.AspNetCore.Authorization.Authorize(Policy = "SystemManage.Sms.Send")]
        [SwaggerOperation(Tags = new[] { "系统管理-短信服务" })]
        [HttpPost("Send")]
        public ResultView Create([FromBody]SingleSendData view)
        {
            return _Service.Send(view);
        }
    }
}
