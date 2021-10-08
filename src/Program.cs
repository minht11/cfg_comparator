using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using System.Net.Http.Headers;
using System;

namespace Cfg
{
    class Program
    {
        static void Main(string[] args)
        {
            // ConsoleConfigUI.Input.ListenForUserInput((parsedInput) => {
            //     ConfigUI.Runner.Display(parsedInput, new ConsoleConfigUI.Output());
            // });

            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("http://localhost:11129/");
            // Add an Accept header for JSON format.
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            // List all Names.
            HttpResponseMessage response = client.GetAsync("api/Values").Result;  // Blocking call!
            if (response.IsSuccessStatusCode)
            {
                var products = response.Content.ReadAsStringAsync().Result;
            }
            else
            {
                Console.WriteLine("{0} ({1})", (int)response.StatusCode, response.ReasonPhrase);
            }

        }
    }
}
