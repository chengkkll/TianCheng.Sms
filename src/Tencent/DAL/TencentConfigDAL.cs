using System;
using System.Collections.Generic;
using System.Text;
using TianCheng.DAL;
using TianCheng.DAL.MongoDB;
using TianCheng.Sms.Tencent.Model;

namespace TianCheng.Sms.Tencent.DAL
{
    /// <summary>
    /// 展会项目企业信息
    /// </summary>
    [DBMapping("sms_tencent_config")]
    public class TencentConfigDAL : MongoOperation<TencentConfigInfo>
    {
    }
}
