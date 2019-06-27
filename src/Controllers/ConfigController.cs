using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using Swashbuckle.AspNetCore.SwaggerGen;
using System;
using System.Collections.Generic;
using System.Text;
using TianCheng.BaseService;
using TianCheng.Model;
using TianCheng.Sms.Tencent.Model;
using TianCheng.Sms.Tencent.Services;

namespace TianCheng.SystemCommon
{
    /// <summary>
    /// 短信管理
    /// </summary>
    [Produces("application/json")]
    [Route("api/System/SettingSms")]
    public class ConfigController : DataController
    {
        #region 构造方法
        private readonly TencentConfigService _Service;

        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="service"></param>
        public ConfigController(TencentConfigService service)
        {
            _Service = service;
        }
        #endregion

        #region 修改数据
        /// <summary>
        /// 修改 
        /// </summary>
        /// <param name="view">请求体中带入修改对象的信息</param>
        /// <power>保存</power>
        [Microsoft.AspNetCore.Authorization.Authorize(Policy = "SystemManage.SmsConfig.Save")]
        [SwaggerOperation(Tags = new[] { "系统管理-短信服务" })]
        [HttpPut("")]
        public ResultView Update([FromBody]TencentConfigInfo view)
        {
            //保存数据
            return _Service.Update(view, LogonInfo);
        }
        #endregion

        #region 数据查询
        /// <summary>
        /// 查看详情
        /// </summary>
        /// <returns></returns>
        /// <power>查看</power>
        [Microsoft.AspNetCore.Authorization.Authorize(Policy = "SystemManage.SmsConfig.Search")]
        [SwaggerOperation(Tags = new[] { "系统管理-短信服务" })]
        [HttpGet("")]
        public TencentConfigInfo Load()
        {
            return _Service.Load(LogonInfo);
        }
        #endregion
    }
}
