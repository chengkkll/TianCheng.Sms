namespace TianCheng.Sms.Tencent.Model
{

    public interface IHTTPClient
    {
        HTTPResponse fetch(HTTPRequest request);

        void close();
    }
}