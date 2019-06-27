using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Text;

namespace TianCheng.Sms.Tencent.Model
{
    public abstract class SmsResultBase
    {
        protected HTTPResponse response;

        /**
         * Parse result from HTTPResponse
         *
         * @param response  HTTP response from api return
         * @return SmsResultbase
         * @throws JSONException  json parse exception
         */
        public abstract void parseFromHTTPResponse(HTTPResponse response);

        /**
         * Parse HTTP response to JSONObject
         *
         * @param response  HTTP response
         * @return JSONObject
         * @throws JSONException  json parse exception
         */
        public JObject parseToJson(HTTPResponse response)
        {
            // Set raw response
            this.response = response;
            try
            {
                return JObject.Parse(response.body);
            }
            catch (JsonReaderException e)
            {
                throw new JSONException(e.Message);
            }
        }

        /**
         * Get raw response
         *
         * @return HTTPResponse
         */
        public HTTPResponse getResponse()
        {
            return response;
        }

        public override string ToString()
        {
            return response.body;
        }
    }
}
