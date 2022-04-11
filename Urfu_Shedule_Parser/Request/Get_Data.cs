﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Threading;
using System.Windows;
using System.IO;

namespace Urfu_Shedule_Parser.Request
{
    public class Get_Data
    {
        
        public string get_data()
        {
            string returned_request_string = "";
            //MainWindow Form1 = new MainWindow();
            Shedule_Scheme Scheme1 = new Shedule_Scheme();

            var proxy = new WebProxy("127.0.0.1:8888");
            var postRequest = new PostRequest("https://urfu.ru/0c2dd46eb010c6689ce8a9cd86fdeb8b");/*/0c2dd46eb010c6689ce8a9cd86fdeb8b*/
            var Post_cookieContainer = new CookieContainer();
            var Get_cookieContainer = new CookieContainer();


            if (postRequest != null)
            {
                postRequest.Data = $"2537422532326576656e74253232253341253232757365725f636f6f726473253232253243253232646174612532322533412535422535423025324330253544253544253243253232686f7374253232253341253232757266752e7275253232253744";
                postRequest.Accept = "*/*";
                postRequest.UserAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/100.0.4896.75 Safari/537.36";
                postRequest.ContentType = "application/octet-stream";

                postRequest.Headers.Add("X-Ajax-Token", "1be01860c46ee308dab89280b85b7116ee1abefbb2cf280a27becc44989b9602"); /*1be01860c46ee308dab89280b85b7116ee1abefbb2cf280a27becc44989b9602*/ /*d54d9a00e680fc0170ba6316189bd681e079a1daa4786c81e6b33e88d8a3182e*/

                postRequest.Referer = $"https://urfu.ru/ru/students/study/schedule/";/*/ru/students/study/schedule/*/
                postRequest.Host = "urfu.ru";

                //postRequest.Proxy = proxy;

                postRequest.Headers.Add("Origin", "https://urfu.ru");
                postRequest.Headers.Add("sec-ch-ua", "\" Not A; Brand\";v=\"99\", \"Chromium\";v=\"100\", \"Google Chrome\";v=\"100\"");
                postRequest.Headers.Add("sec-ch-ua-mobile", "?0");
                postRequest.Headers.Add("sec-ch-ua-platform", "\"Windows\"");
                postRequest.Headers.Add("Sec-Fetch-Dest", "empty");
                postRequest.Headers.Add("Sec-Fetch-Mode", "cors");
                postRequest.Headers.Add("Sec-Fetch-Site", "same-origin");

                postRequest.Run(Post_cookieContainer);

                Thread.Sleep(50);

                var getRequest = new GetRequest(Properties.Resources.Shedule_url);


                if (getRequest != null)
                {
                    //postRequest.Proxy = proxy;

                    getRequest.Accept = "text/html, */*; q=0.01";
                    getRequest.UserAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/100.0.4896.75 Safari/537.36";

                    getRequest.Referer = "https://urfu.ru/ru/students/study/schedule/";

                    getRequest.Headers.Add("sec-ch-ua", "\" Not A; Brand\";v=\"99\", \"Chromium\";v=\"100\", \"Google Chrome\";v=\"100\"");
                    getRequest.Headers.Add("sec-ch-ua-mobile", "?0");
                    getRequest.Headers.Add("sec-ch-ua-platform", "\"Windows\"");
                    getRequest.Headers.Add("Sec-Fetch-Dest", "empty");
                    getRequest.Headers.Add("Sec-Fetch-Mode", "cors");
                    getRequest.Headers.Add("Sec-Fetch-Site", "same-origin");

                    getRequest.Host = "urfu.ru";

                    getRequest.Run(Get_cookieContainer);


                    //File.WriteAllText(@"D:\111.txt", returned_request_string);

                    //Form1.Institute_ComboBox.Items.Add("");

                    //var string test_string = getRequest.Response.IndexOf("div class=\"shedule-group-title"); 
                    returned_request_string = getRequest.Response;


                }
                //File.WriteAllText(@"D:\122.txt", returned_request_string);
                return returned_request_string = getRequest.Response;
            }
            else return string.Empty;
            
            //return returned_request_string;
        }

        //public string Return_Request_String()
        //{
        //    File.WriteAllText(@"D:\222.txt", returned_request_string);
        //    return returned_request_string;
        //}
    }
}
