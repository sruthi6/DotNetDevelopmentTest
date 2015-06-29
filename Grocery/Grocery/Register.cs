using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grocery
{
    public class Register
    {
        public List<Customer> Customers { get; set; }
        public int TimeTakenToProcessEachItem { get; set; }

        //find current customer count for register by time
        public int CurrentCustomerCountForGivenMinute(int currentMinute)
        {
            int customerCount = 0;
            if (Customers != null && Customers.Any())
            {
                customerCount = Customers.Where(x => x.ElapsedTimeToCompleteThisCustomer >= currentMinute).Count();
                if (customerCount < 0)
                {
                    customerCount = 0;
                }
            }
            return customerCount;
        }

        //get current register processed time
        public int RegisterCompletionTime { get { return Customers.OrderByDescending(x => x.ElapsedTimeToCompleteThisCustomer).FirstOrDefault().ElapsedTimeToCompleteThisCustomer; } }

        //find last customer items for register 
        public int LastCustomerItemsForGivenMinute(int currentMinute)
        {
            int items = 0;
            if (Customers != null && Customers.Any() && Customers.Any(x => x.ElapsedTimeToCompleteThisCustomer >= currentMinute))
            {
                items = Customers.Last().Items;
            }
            return items;
        }

        //assign a customer to this register
        public void AssignCustomerToThisRegister(Customer customer)
        {
            // if we have customer already in this register we are calculating the total time taken for this register to process this customer
            if (Customers != null && Customers.Any())
            {
                if (Customers.Last().ElapsedTimeToCompleteThisCustomer > customer.ArrivalTime)
                {
                    customer.ElapsedTimeToCompleteThisCustomer = Customers.Last().ElapsedTimeToCompleteThisCustomer + (TimeTakenToProcessEachItem * customer.Items);
                }
                else
                {
                    customer.ElapsedTimeToCompleteThisCustomer = customer.ArrivalTime + (TimeTakenToProcessEachItem * customer.Items);
                }
            }
            else
            {
                customer.ElapsedTimeToCompleteThisCustomer = customer.ArrivalTime + (customer.Items * TimeTakenToProcessEachItem) - 1;
            }
            Customers.Add(customer);
        }
    }
}
