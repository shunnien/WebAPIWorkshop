using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Http;
using System.Threading.Tasks;
using System.Net;

namespace HttpClient
{
    class Program
    {
        private static string requestbinUrl = "http://requestb.in/1k80fbq1";

        static void Main(string[] args)
        {
        }

        

        public async static Task ReUseHttpClient(System.Net.Http.HttpClient client)
        {
            // instance re-use
            var task1 = await client.GetStringAsync(requestbinUrl);
            var task2 = await client.GetStringAsync(requestbinUrl);

            Console.WriteLine(task1);
            Console.WriteLine(task2);
        }

        public async static Task GetResponse(System.Net.Http.HttpClient client)
        {
            var response = await client.GetAsync(requestbinUrl);
            response.EnsureSuccessStatusCode();
            string result = await response.Content.ReadAsStringAsync();
            Console.WriteLine(result);
        }

        public async static Task SendAsync(System.Net.Http.HttpClient client)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, requestbinUrl);
            var response = await client.SendAsync(request);
            response.EnsureSuccessStatusCode();
            string result = await response.Content.ReadAsStringAsync();
            Console.WriteLine(result);
        }

        public async static Task POSTData()
        {
            System.Net.Http.HttpClient client = new System.Net.Http.HttpClient(new HttpClientHandler { UseProxy = false });
            // POST方法
            var request = new HttpRequestMessage(HttpMethod.Post, requestbinUrl);
            // Content存放要上傳的內容
            request.Content = new StringContent("This is a test.");
            HttpResponseMessage response = await client.SendAsync(request);
            response.EnsureSuccessStatusCode();
            Console.WriteLine(await response.Content.ReadAsStringAsync());
        }

        public async static Task HttpClientForCredential()
        {
            // HttpClient 透過 HttpClientHandler提供屬性設置
            // http://msdn.microsoft.com/zh-tw/library/system.net.networkcredential(v=vs.110).aspx
            var handler = new HttpClientHandler
            {
                Credentials = new NetworkCredential("username", "1a2b3c4d"),
                PreAuthenticate = true
            };
            System.Net.Http.HttpClient client = new System.Net.Http.HttpClient(handler);

            HttpResponseMessage response = await client.GetAsync(requestbinUrl);
            response.EnsureSuccessStatusCode();
            string result = await response.Content.ReadAsStringAsync();
            Console.WriteLine(result);
        }
    }
}
