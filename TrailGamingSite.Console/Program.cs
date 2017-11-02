using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using TrailGamingSite.Models.Model;

namespace TrailGamingSiteNewConsole
{
    class Program
    {
        static bool bNoRecords = true;

        /// <summary>
        /// Invokes the Menu items
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            DisplayHeading();
            GetCustomers().Wait();
            DisplayMenuOptions();
            Console.ReadKey();
        }

        static void DisplayHeading() {
            Console.WriteLine("-------------------------------------------------------------------");
            Console.WriteLine("-----------------Trail Gaming Site -------------------");
            Console.WriteLine("-------------------------------------------------------------------");
        }

        /// <summary>
        /// Displays Menu options for add/delete customers and funds to customer account
        /// </summary>
        /// <param name="oHttpClient"></param>
        static void DisplayMenuOptions()
        {
            try
            {
                Console.WriteLine("1. Add a Customer? [Press 1]");
                Console.WriteLine("2. Deposit Funds to a Customer? [Press 2]");
                Console.WriteLine("3. Delete to a Customer? [Press 3]");
                int iSelectedOption = Convert.ToInt32(Console.ReadLine());

                switch (iSelectedOption)
                {
                    case 1:
                        PostCustomer().Wait();
                        break;
                    case 2:
                        AddFund().Wait();
                        break;
                    case 3:
                        DeleteCustomer();
                        break;
                    default:
                        Console.Clear();
                        DisplayHeading();
                        GetCustomers().Wait();
                        break;
                }
            }
            catch (Exception oException)
            {
                Console.WriteLine("Something went wrong as type casting is not handled. Please rerun!");
                Console.Clear();
                DisplayHeading();
                GetCustomers().Wait();
            }           
        }

        /// <summary>
        /// Executes to display customer details //Todo/ Formatting to be worked on :)
        /// </summary>
        /// <param name="oHttpClient"></param>
        static async Task GetCustomers()
        {
            using (HttpClient oHttpClient = GetHTTPClientObject())
            {
                HttpResponseMessage oResponseMessage = await oHttpClient.GetAsync("api/getcustomers");
                oResponseMessage.EnsureSuccessStatusCode();
                if (oResponseMessage.IsSuccessStatusCode)
                {
                    var oCustomerDetails = await oResponseMessage.Content.ReadAsAsync<IEnumerable<CustomerItem>>();
                    int iCount = 0;
                    foreach (var item in oCustomerDetails)
                    {
                        iCount += 1;
                        if (iCount == 1)
                        {
                            bNoRecords = false;
                            Console.WriteLine("Customer Details");
                            Console.WriteLine("-----------------------------------------------------------------------------------------------------");
                            Console.WriteLine("CustomerId \t      Customer Name \t\t       Email \t\t\t Balance(in kr)");
                        }
                        decimal dcAmount = (from oTran in item.Transaction select oTran.Amount).Sum();
                        Console.WriteLine("" + item.Id + "\t\t\t" + item.Name + "\t\t\t" + item.Email + "    \t\t\t" + dcAmount);
                    }
                    if (iCount == 0)
                    {//TODO
                        if (bNoRecords)
                        {
                            Console.WriteLine("There are no customers. Please Register Customer(s)\r");
                            PostCustomer().Wait();
                        }
                    }
                    Console.WriteLine("-----------------------------------------------------------------------------------------------------");
                    DisplayMenuOptions();
                }
            }
        }

        /// <summary>
        /// Executes when Adding customer details
        /// </summary>
        /// <param name="oHttpClient"></param>
        /// <returns></returns>
        static async Task PostCustomer()
        {          
            using (HttpClient oHttpClient = GetHTTPClientObject())
            {
                Customer oCustomer = new Customer();
                Console.WriteLine("Please enter Customer Name:\r");
                oCustomer.Name = Console.ReadLine();
                Console.WriteLine("Please enter Email:\r");
                oCustomer.Email = Console.ReadLine();
                Console.WriteLine("Please enter Phone Number:\r");
                oCustomer.Phone = Console.ReadLine();
                Console.WriteLine("Please add a minimum deposit. else default of 10 kr will be deposited to your account:\r");
                var oAmount = Console.ReadLine();
                var oTransaction = new Transaction();
                decimal oResultAmount = 0;
                decimal.TryParse(oAmount, out oResultAmount);
                if (oResultAmount > 0)
                {
                    oCustomer.Transaction = new List<Transaction>();
                    oTransaction.Amount = Convert.ToDecimal(oAmount);
                    oCustomer.Transaction.Add(oTransaction);
                }
                else
                { 
                    oCustomer.Transaction = new List<Transaction>();                   
                    oTransaction.Amount = 10;
                    oCustomer.Transaction.Add(oTransaction);
                }
                               
                var oResponseMessage = await oHttpClient.PostAsJsonAsync("api/postcustomer", oCustomer);
                oResponseMessage.EnsureSuccessStatusCode();
                if (oResponseMessage.IsSuccessStatusCode)
                {
                    Console.WriteLine("******Customer Registered Successfully.******");

                    Console.WriteLine("Do you wish to Continue? Press Y.");
                    var oYes = Console.ReadLine();
                    if (oYes.ToLower() == "y")
                    {
                        Console.Clear();
                        DisplayHeading();
                        GetCustomers().Wait();
                    }
                }

            }
            Console.ReadLine();
        }        

        /// <summary>
        /// Executes to delete a customer
        /// </summary>
        static void DeleteCustomer()
        {
            HttpClient oHttpClient = GetHTTPClientObject();
            Console.WriteLine("Please enter the Customer ID you wish to delete:");
            int iCustomerId = Convert.ToInt32(Console.ReadLine());
            using (oHttpClient)
            {
                HttpResponseMessage oResponseMessage = oHttpClient.DeleteAsync("api/deletecustomer/"+ iCustomerId.ToString()).Result;
                oResponseMessage.EnsureSuccessStatusCode();
                if (oResponseMessage.IsSuccessStatusCode)
                {
                    Console.Clear();
                    DisplayHeading();
                    Console.Write("******Customer Delete successful.******\n");
                    GetCustomers().Wait();
                }
            }
        }

        /// <summary>
        /// Executes when adding the funds // TODO// can call the same while withdrawing funds but with MINUS values
        /// </summary>
        /// <param name="oHttpClient"></param>
        static async Task AddFund()
        {
            using (HttpClient oHttpClient = GetHTTPClientObject())
            {
                var oTransaction = new Transaction();
                Console.WriteLine("Please enter Customer Id to deposit the funds:\r");
                oTransaction.CustomerId = Convert.ToInt32(Console.ReadLine());
                Console.WriteLine("Please enter the amount to deposit for this customer:\r");                
                decimal dcFundAmount = Convert.ToDecimal(Console.ReadLine());    
                oTransaction.Amount = dcFundAmount;

                var oResponseMessage = await oHttpClient.PostAsJsonAsync("api/addfund", oTransaction);
                oResponseMessage.EnsureSuccessStatusCode();
                if (oResponseMessage.IsSuccessStatusCode)
                {
                    Console.WriteLine("******Funds Deposited Successfully.*******\n");
                    Console.WriteLine("Do you wish to Continue? Press Y.");
                    var oYes = Console.ReadLine();
                    if (oYes.ToLower() == "y")
                    {
                        Console.Clear();
                        DisplayHeading();
                        GetCustomers().Wait();
                    }
                }
            }
            Console.ReadLine();
        }

        /// <summary>
        /// Gets HttpClient Object
        /// </summary>
        static HttpClient GetHTTPClientObject()
        {
            HttpClient oHttpClient = new HttpClient();
            oHttpClient.BaseAddress = new Uri("http://localhost:8088/");
            oHttpClient.DefaultRequestHeaders.Accept.Clear();
            oHttpClient.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
            return oHttpClient;
        }
    }
}
