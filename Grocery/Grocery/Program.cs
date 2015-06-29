using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Grocery
{
    public class Program
    {
        static List<Customer> customerList = new List<Customer>();
        static List<Register> registerList = new List<Register>();

        public static void Main(string[] args)
        {
            //Console.WriteLine(args[0]);
            // Read the file and display it line by line.
            StreamReader file = new StreamReader(Environment.CurrentDirectory + "" + args[0]);

            ProcessFile(file);            

            Console.WriteLine(string.Format("Finished at: t={0} minutes", GetGroceryCustomerProcessingTime()));
        }

        public static void ProcessFile(StreamReader file)
        {
            var noOfRegisters = 0;
            int counter = 0;
            string line;

            while ((line = file.ReadLine()) != null)
            {
                if (counter == 0)
                {
                    noOfRegisters = int.Parse(line);
                }
                else
                {
                    var customerInput = line.Split(' ');
                    var customer = new Customer();
                    customer.CustomerType = customerInput[0].ToString();
                    customer.ArrivalTime = Convert.ToInt32(customerInput[1]);
                    customer.Items = Convert.ToInt32(customerInput[2]);

                    customerList.Add(customer);
                }
                //Console.WriteLine(line);
                counter++;
            }
            file.Close();

            // building the registers
            for (int i = 1; i <= noOfRegisters; i++)
            {
                int timeTakenForItems = 1;
                if (i == noOfRegisters) // for last register we will set the time to two minutes
                {
                    timeTakenForItems = 2;
                }
                registerList.Add(new Register()
                {
                    Customers = new List<Customer>(),
                    TimeTakenToProcessEachItem = timeTakenForItems
                });
            }
        }

        public static int GetGroceryCustomerProcessingTime()
        {
            while (customerList.Count != 0)
            {
                //determine which customer to pick to assign id muliple customer came at same time
                var firstCustomer = customerList.First();
                var customersCameAtSameTime = customerList.Where(x => x.ArrivalTime == firstCustomer.ArrivalTime).ToList();
                // If two or more customers arrive at the same time, those with fewer items choose registers before those with more, 
                //    and if they have the same number of items then type A's choose before type B's.
                if (customersCameAtSameTime.Count() > 1)
                {
                    var customerGrouping = customersCameAtSameTime.GroupBy(x => x.Items).Select(x => new { itemsCount = x.Key, customerCount = x.Count(), customers = x.Select(y => y) });

                    customerGrouping.OrderBy(x => x.itemsCount).ToList().ForEach(x =>
                    {
                        if (x.customerCount > 1)
                        {
                            x.customers.Where(c => c.CustomerType == "A").ToList().ForEach(cl =>
                            {
                                ProcessGivenCustomer(cl);
                            });

                            x.customers.Where(c => c.CustomerType == "B").ToList().ForEach(cl =>
                            {
                                ProcessGivenCustomer(cl);
                            });
                        }
                        else
                        {
                            ProcessGivenCustomer(x.customers.FirstOrDefault());
                        }
                    });
                }
                else
                {
                    ProcessGivenCustomer(firstCustomer);
                }
            }

            return registerList.Max(x => x.RegisterCompletionTime) + 1;
        }

        private static void ProcessGivenCustomer(Customer customer)
        {
            if (customer.CustomerType == "A")
            {
                AssignATypeCustomerToRegister(customer);
            }
            else if (customer.CustomerType == "B")
            {
                //Customer Type B looks at the last customer in each line, and always chooses to be behind the customer with the fewest number 
                // of items left to check out, regardless of how many other customers are in the line or how many items they have. 
                // Customer Type B will always choose an empty line before a line with any customers in it.
                AssignBTypeCustomerToRegister(customer);
            }
        }

        private static void AssignATypeCustomerToRegister(Customer customer)
        {
            // find and assign the customer to the register
            registerList.OrderBy(x => x.CurrentCustomerCountForGivenMinute(customer.ArrivalTime)).FirstOrDefault().AssignCustomerToThisRegister(customer);
            // remove the customer for the customer list
            customerList.Remove(customer);
        }

        private static void AssignBTypeCustomerToRegister(Customer customer)
        {
            registerList.OrderBy(x => x.LastCustomerItemsForGivenMinute(customer.ArrivalTime)).FirstOrDefault().AssignCustomerToThisRegister(customer);
            customerList.Remove(customer);
        }
    }
}
