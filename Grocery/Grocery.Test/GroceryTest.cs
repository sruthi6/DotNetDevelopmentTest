using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;

namespace Grocery.Test
{
    [TestClass]
    public class GroceryTest
    {        
        [TestMethod]
        public void ShouldReturn7MinutesForInputFile1()
        {

            //Change the URL
            StreamReader file = new StreamReader("D:\\Grocery\\Grocery.Test\\InputFiles\\input1.txt");

            Program.ProcessFile(file);
            Assert.AreEqual(7, Program.GetGroceryCustomerProcessingTime());
        }

        [TestMethod]
        public void ShouldReturn13MinutesForInputFile2()
        {
            //Change the URL
            StreamReader file = new StreamReader("D:\\Grocery\\Grocery.Test\\InputFiles\\input2.txt");

            Program.ProcessFile(file);
            Assert.AreEqual(13, Program.GetGroceryCustomerProcessingTime());
        }

        [TestMethod]
        public void ShouldReturn6MinutesForInputFile3()
        {
            //Change the URL
            StreamReader file = new StreamReader("D:\\Grocery\\Grocery.Test\\InputFiles\\input3.txt");

            Program.ProcessFile(file);
            Assert.AreEqual(6, Program.GetGroceryCustomerProcessingTime());
        }

        [TestMethod]
        public void ShouldReturn9MinutesForInputFile4()
        {
            //Change the URL
            StreamReader file = new StreamReader("D:\\Grocery\\Grocery.Test\\InputFiles\\input4.txt");

            Program.ProcessFile(file);
            Assert.AreEqual(9, Program.GetGroceryCustomerProcessingTime());
        }

        [TestMethod]
        public void ShouldReturn11MinutesForInputFile5()
        {
            //Change the URL
            StreamReader file = new StreamReader("D:\\Grocery\\Grocery.Test\\InputFiles\\input5.txt");

            Program.ProcessFile(file);
            Assert.AreEqual(11, Program.GetGroceryCustomerProcessingTime());
        }
    }
}
