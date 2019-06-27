using System;
using System.Collections.Generic;
using System.Text;
using TianCheng.BaseService;
using TianCheng.Model;
using TianCheng.Sms.Tencent.Model;

namespace TianCheng.Sms.Tencent.Services
{
    /// <summary>
    /// 腾讯短信服务
    /// </summary>
    public class TencentSmsService : IServiceRegister
    {
        protected int appid { get; }
        protected string appkey { get; }
        protected IHTTPClient httpclient { get; set; }
        protected string SmsSign { get; }
        private string url = "https://yun.tim.qq.com/v5/tlssmssvr/sendsms";

        public TencentSmsService()
        {
            var config = TencentConfigService.Load();
            appid = config.AppId;
            appkey = config.AppKey;
            SmsSign = config.SmsSign;
            this.httpclient = new DefaultHTTPClient();
        }

        public TencentSmsService(int appid, string appkey)
        {
            this.appid = appid;
            this.appkey = appkey;
            this.httpclient = new DefaultHTTPClient();
        }

        public TencentSmsService(int appid, string appkey, IHTTPClient httpclient)
        {
            this.appid = appid;
            this.appkey = appkey;
            this.httpclient = httpclient;
        }

        /**
         * Handle http status error
         *
         * @param response   raw http response
         * @return response  raw http response
         * @throws HTTPException  http status exception
         */
        public HTTPResponse handleError(HTTPResponse response)
        {
            if (response.statusCode < 200 || response.statusCode >= 300)
            {
                throw new HTTPException(response.statusCode, response.reason);
            }
            return response;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="phoneNumber"></param>
        /// <param name="msg"></param>
        /// <returns></returns>
        public ResultView Send(SingleSendData data)
        {
            SmsSingleSenderResult result = Send(0, "86", data.PhoneNumber, data.Message, "", "");
            if (result.result == 0)
            {
                return ResultView.Success();
            }
            throw ApiException.BadRequest(result.errMsg);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="phoneNumber"></param>
        /// <param name="msg"></param>
        /// <returns></returns>
        public SmsSingleSenderResult Send(string phoneNumber, string msg)
        {
            return Send(0, "86", phoneNumber, msg, "", "");
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="type"></param>
        /// <param name="nationCode"></param>
        /// <param name="phoneNumber"></param>
        /// <param name="msg"></param>
        /// <param name="extend"></param>
        /// <param name="ext"></param>
        /// <returns></returns>
        public SmsSingleSenderResult Send(int type, string nationCode, string phoneNumber,
            string msg, string extend, string ext)
        {
            long random = SmsSenderUtil.getRandom();
            long now = SmsSenderUtil.getCurrentTime();
            if (!string.IsNullOrWhiteSpace(SmsSign))
            {
                msg = SmsSign + msg;
            }
            JSONObjectBuilder body = new JSONObjectBuilder()
                .Put("tel", (new JSONObjectBuilder()).Put("nationcode", nationCode).Put("mobile", phoneNumber).Build())
                .Put("type", type)
                .Put("msg", msg)
                .Put("sig", SmsSenderUtil.calculateSignature(this.appkey, random, now, phoneNumber))
                .Put("time", now)
                .Put("extend", !String.IsNullOrEmpty(extend) ? extend : "")
                .Put("ext", !String.IsNullOrEmpty(ext) ? ext : "");

            HTTPRequest req = new HTTPRequest(HTTPMethod.POST, this.url)
                .addHeader("Conetent-Type", "application/json")
                .addQueryParameter("sdkappid", this.appid)
                .addQueryParameter("random", random)
                .setConnectionTimeout(60 * 1000)
                .setRequestTimeout(60 * 1000)
                .setBody(body.Build().ToString());

            // May throw HttpRequestException
            HTTPResponse res = httpclient.fetch(req);

            // May throw HTTPException
            handleError(res);

            SmsSingleSenderResult result = new SmsSingleSenderResult();
            // May throw JSONException
            result.parseFromHTTPResponse(res);

            return result;
        }
    }
}
