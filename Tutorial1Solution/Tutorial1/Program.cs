using System;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Tutorial1
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var websiteUrl = args.Length > 0 ? args[0] : throw new ArgumentNullException();


            if (websiteUrl == null)
            {
                throw new ArgumentException();
            }
            var httpClient = new HttpClient();

            try
            {
                var response = await httpClient.GetAsync(websiteUrl);
                httpClient.Dispose();
                if (response.IsSuccessStatusCode)
                {
                    var htmlContent = await response.Content.ReadAsStringAsync();
                    var regex = new Regex("[a-z]+[a-z0-9]*@[a-z0-9]+\\.[a-z]+", RegexOptions.IgnoreCase);
                    var emailAddresses = regex.Matches(htmlContent);

                    if (emailAddresses.Count > 0)
                    {
                        foreach (var address in emailAddresses)
                        {
                            Console.WriteLine(address.ToString());
                        }
                    }
                    else
                    {
                        Console.Write("No addresses found!");
                    }

                }
            }
            catch (Exception)
            {
                Console.WriteLine("Wrror while downloading the page!");
            }
        }
    }
}
