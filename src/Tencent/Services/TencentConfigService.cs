using System;
using System.Collections.Generic;
using System.Text;
using TianCheng.BaseService;
using TianCheng.Sms.Tencent.DAL;
using TianCheng.Sms.Tencent.Model;
using System.Linq;
using TianCheng.Model;

namespace TianCheng.Sms.Tencent.Services
{
    /// <summary>
    /// 腾讯云短信配置信息
    /// </summary>
    public class TencentConfigService : MongoBusinessService<TencentConfigInfo>
    {
        #region 构造方法
        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="dal"></param>
        public TencentConfigService(TencentConfigDAL dal) : base(dal)
        {
        }
        #endregion

        /// <summary>
        /// 初始化配置信息
        /// </summary>
        /// <param name="logonInfo"></param>
        public TencentConfigInfo InitConfig(TokenLogonInfo logonInfo)
        {
            TencentConfigInfo info = new TencentConfigInfo { AppId = 1400059775, AppKey = "eaa15e15da52a7892bf91277f66b3f63" };
            _Dal.Drop();
            this.Create(info, logonInfo);
            return info;
        }
        static public TencentConfigInfo Load()
        {
            TencentConfigService service = ServiceLoader.GetService<TencentConfigService>();
            return service.Load(new TokenLogonInfo { Id = "", Name = "系统自动" });
        }
        /// <summary>
        /// 加载配置信息
        /// </summary>
        /// <param name="logonInfo"></param>
        /// <returns></returns>
        public TencentConfigInfo Load(TokenLogonInfo logonInfo)
        {
            var info = _Dal.Queryable().FirstOrDefault();
            if (info == null || info.IsEmpty)
            {
                return InitConfig(logonInfo);
            }
            return info;
        }
        /// <summary>
        /// 设置配置信息
        /// </summary>
        /// <param name="info"></param>
        /// <param name="logonInfo"></param>
        public void Setting(TencentConfigInfo info, TokenLogonInfo logonInfo)
        {
            var old = Load(logonInfo);
            old.AppId = info.AppId;
            old.AppKey = info.AppKey;
            this.Update(info, logonInfo);
        }
    }
}
