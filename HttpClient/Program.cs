using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Http;
using System.Threading.Tasks;

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
    }
}
