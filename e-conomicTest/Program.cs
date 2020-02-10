using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace e_conomicTest
{
    class Program
    {

        static HttpClient client = new HttpClient();

        static void ShowCustomer(Customer customer)
        {
            Console.WriteLine(customer.ToString());
        }

        static async Task<Customer> GetCustomerAsync(string path, int custNumber)
        {
            Customer customer = null;
            HttpResponseMessage response = await client.GetAsync(path + string.Format("{0}", custNumber));
            if (response.IsSuccessStatusCode)
            {
                customer = await response.Content.ReadAsAsync<Customer>();
            }
            return customer;
        }

        static async Task RunAsync()
        {
            Customer customer = new Customer();
            client.BaseAddress = new Uri("https://restapi.e-conomic.com");
            client.DefaultRequestHeaders.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            client.DefaultRequestHeaders.Add("X-AppSecretToken", Auth.APPSECRETTOKEN);
            client.DefaultRequestHeaders.Add("X-AgreementGrantToken", Auth.AGREEMENTGRANTTOKEN);
            try
            {
                customer = await GetCustomerAsync(client.BaseAddress + "/customers/", 1);
                ShowCustomer(customer);
            }catch(Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        static void Main(string[] args)
        {
            RunAsync().GetAwaiter().GetResult();
            Console.ReadLine();
        }
    }
}
