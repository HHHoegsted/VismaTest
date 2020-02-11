using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace e_conomicTest
{
    class Program
    {

        static readonly HttpClient client = new HttpClient();

        //Metoder der har med eksempel 1 at gøre - hent en kunde fra API og vis stamdata + balance/forfaldent i console
        static async Task ShowCustomers()
        {
            Customer customer;
            try
            {
                customer = await GetCustomerAsync(client.BaseAddress + "/customers/", 1);
                ShowCustomer(customer);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

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

        //Slut eksempel 1

        //Eksempel 2 - hent en liste af forfaldne fakturaer
        //Kan filtreres på kundenummer og mange andre parametre - her sendes kundenummer med i kaldet af ShowOverdue, og dato og pagesize er hardcoded for eksemplets skyld
        //Kan sorteres på mange parametre, her på dato - igen hardcoded for eksemplets skyld
        //Jeg nøjes med at vise hvor mange ubetalte fakturaer der er i dette eksempel, for der er MANGE i sandbox data - dette eksempel returnerer en liste med 255 ubetalte fakturaer på 3 dage.

        static async Task ShowOverdue(int customerNumber)
        {
            List<Invoice> overdue;
            try
            {
                overdue = await GetOverdueInvoicesAsync(client.BaseAddress + "/invoices/overdue/?filter=customer.customerNumber$eq:" + customerNumber.ToString() + 
                    "$and:date$gt:2019-10-01$and:date$lt:2019-10-03&pagesize=1000&sort=date");
                Console.WriteLine("Total number of overdue invoices in date range is {0}",overdue.Count);
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        static async Task<List<Invoice>> GetOverdueInvoicesAsync(string path)
        { 
            List<Invoice> overdue = new List<Invoice>();
            HttpResponseMessage response = await client.GetAsync(path);
            string responseBody = await response.Content.ReadAsStringAsync();
            if (response.IsSuccessStatusCode)
            {
                var invoiceResponse = JsonConvert.DeserializeObject<InvoiceResponse>(responseBody);
                overdue = invoiceResponse.Collection;
            }
            return overdue;
        }

        //Slut eksempel 2
                
        
        //Main metode, sætter request headers til auth og mediatype og kører de enkelte requests
        static async Task RunAsync()
        {
            client.BaseAddress = new Uri("https://restapi.e-conomic.com");
            client.DefaultRequestHeaders.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            client.DefaultRequestHeaders.Add("X-AppSecretToken", Auth.APPSECRETTOKEN);
            client.DefaultRequestHeaders.Add("X-AgreementGrantToken", Auth.AGREEMENTGRANTTOKEN);

            //Eksempel 1; Vis en kunde med stamdata og balance/forfaldent
            //await ShowCustomers();
            //Eksempel 2; Vis en liste med forfaldne fakturaer på en bestemt kunde - kundenummer medsendes i metodekald
            //await ShowOverdue(1);
        }

        static void Main()
        {
            RunAsync().GetAwaiter().GetResult();
            Console.ReadLine();
        }
    }
}
