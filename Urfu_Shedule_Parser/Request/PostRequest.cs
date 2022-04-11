using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Net;

namespace Urfu_Shedule_Parser.Request
{
    public class PostRequest
    {
        HttpWebRequest _request;
        string _url;

        public Dictionary<string, string> Headers { get; set; }

        public string Response { get; set; }
        public string Accept { get; set; }
        public string Host { get; set; }
        public string Data { get; set; }
        public string ContentType { get; set; }
        public string Referer { get; set; }
        public string UserAgent { get; set; }
        public WebProxy Proxy { get; set; }

        public PostRequest(string url)
        {
            _url = url;
            Headers = new Dictionary<string, string>();
        }

        public PostRequest()
        {
        }

        public void Run(CookieContainer cookieContainer)
        {
            _request = (HttpWebRequest)WebRequest.Create(_url);
            _request.Method = "Post";
            _request.CookieContainer = cookieContainer;
            _request.Proxy = Proxy;
            _request.Accept = Accept;
            _request.ContentType = ContentType;
            _request.Host = Host;
            _request.Referer = Referer;
            _request.UserAgent = UserAgent;

            byte[] sentData = Encoding.UTF8.GetBytes(Data);
            _request.ContentLength = sentData.Length;
            Stream sendStream = _request.GetRequestStream();
            sendStream.Write(sentData, 0, sentData.Length);
            sendStream.Close();

            foreach (var item in Headers)
            {
                _request.Headers.Add(item.Key, item.Value);
            }

            try
            {
                HttpWebResponse response = (HttpWebResponse)_request.GetResponse();
                var stream = response.GetResponseStream();
                if (stream != null) Response = new StreamReader(stream).ReadToEnd();
            }
            catch (Exception)
            {
                Response = "Возникла ошибка";
                //throw;
            }
        }
    }
}
