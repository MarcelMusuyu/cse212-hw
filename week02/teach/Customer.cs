// using System;
// using System.Collections.Generic;
// using Microsoft.VisualStudio.TestTools.UnitTesting;

// // Define a class to represent customer information
// public class Customer
// {
//     public string Name { get; set; }
//     public int AccountId { get; set; }
//     public string Problem { get; set; }

//     public Customer(string name, int accountId, string problem)
//     {
//         Name = name;
//         AccountId = accountId;
//         Problem = problem;
//     }

//     public override string ToString()
//     {
//         return $"Name: {Name}, Account ID: {AccountId}, Problem: {Problem}";
//     }
// }

// // Define the CustomerService class
// public class CustomerService
// {
//     private Queue<Customer> customerQueue;
//     private int maxSize;

//     // Constructor for CustomerService
//     public CustomerService(int size)
//     {
//         // If the size is invalid, default to 10
//         maxSize = (size <= 0) ? 10 : size;
//         customerQueue = new Queue<Customer>(maxSize);
//     }

//     // Method to add a new customer to the queue
//     public void AddNewCustomer(Customer customer)
//     {
//         // Check if the queue is full
//         if (customerQueue.Count >= maxSize)
//         {
//             Console.WriteLine("Error: Customer queue is full. Cannot add new customer.");
//         }
//         else
//         {
//             customerQueue.Enqueue(customer);
//             Console.WriteLine($"Customer {customer.Name} added to the queue.");
//         }
//     }

//     // Method to serve the next customer in the queue
//     public Customer ServeCustomer()
//     {
//         // Check if the queue is empty
//         if (customerQueue.Count == 0)
//         {
//             Console.WriteLine("Error: Customer queue is empty. No customers to serve.");
//             return null;
//         }
//         else
//         {
//             Customer customer = customerQueue.Dequeue();
//             Console.WriteLine($"Serving customer: {customer}");
//             return customer;
//         }
//     }
// }

// // Test class for CustomerService using MSTest
// [TestClass]
// public class CustomerServiceTests
// {
//     [TestMethod]
//     // Test case for constructor with invalid size
//     public void Constructor_InvalidSize_DefaultsTo10()
//     {
//         CustomerService service = new CustomerService(-5);
//         // Assert that the queue is initialized with the default size of 10.  Use reflection to access private field.
//         System.Reflection.FieldInfo field = service.GetType().GetField("maxSize", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
//         int actualSize = (int)field.GetValue(service);
//         Assert.AreEqual(10, actualSize, "Constructor should default to 10 when size is invalid.");
//     }

//     [TestMethod]
//     // Test case for constructor with valid size
//     public void Constructor_ValidSize_SetsCorrectSize()
//     {
//         CustomerService service = new CustomerService(20);
//         // Assert that the queue is initialized with the specified size.
//         System.Reflection.FieldInfo field = service.GetType().GetField("maxSize", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
//         int actualSize = (int)field.GetValue(service);
//         Assert.AreEqual(20, actualSize, "Constructor should set the correct size when size is valid.");
//     }

//     [TestMethod]
//     // Test case for AddNewCustomer when the queue is not full
//     public void AddNewCustomer_QueueNotFull_AddsCustomer()
//     {
//         CustomerService service = new CustomerService(3);
//         Customer customer1 = new Customer("Alice", 1, "Problem A");
//         service.AddNewCustomer(customer1);
//         // Assert that the customer is added to the queue
//         System.Reflection.FieldInfo field = service.GetType().GetField("customerQueue", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
//         Queue<Customer> queue = (Queue<Customer>)field.GetValue(service);
//         Assert.AreEqual(1, queue.Count, "AddNewCustomer should add customer to queue when queue is not full.");
//     }

//     [TestMethod]
//     // Test case for AddNewCustomer when the queue is full
//     public void AddNewCustomer_QueueFull_DisplaysErrorMessage()
//     {
//         // Redirect console output to a StringWriter to capture the error message
//         using (var consoleOutput = new System.IO.StringWriter())
//         {
//             Console.SetOut(consoleOutput); // Set the console output to the StringWriter

//             CustomerService service = new CustomerService(2);
//             Customer customer1 = new Customer("Alice", 1, "Problem A");
//             Customer customer2 = new Customer("Bob", 2, "Problem B");
//             Customer customer3 = new Customer("Charlie", 3, "Problem C");
//             service.AddNewCustomer(customer1);
//             service.AddNewCustomer(customer2);
//             service.AddNewCustomer(customer3); // This should trigger the error message

//             // Assert that the error message is displayed
//             string expectedErrorMessage = "Error: Customer queue is full. Cannot add new customer.";
//             Assert.IsTrue(consoleOutput.ToString().Contains(expectedErrorMessage), "AddNewCustomer should display error message when queue is full.");
//             Console.SetOut(Console.Out); // Reset console output
//         }
//     }

//     [TestMethod]
//     // Test case for ServeCustomer when the queue is not empty
//     public void ServeCustomer_QueueNotEmpty_ServesCustomer()
//     {
//         CustomerService service = new CustomerService(3);
//         Customer customer1 = new Customer("Alice", 1, "Problem A");
//         Customer customer2 = new Customer("Bob", 2, "Problem B");
//         service.AddNewCustomer(customer1);
//         service.AddNewCustomer(customer2);
//         Customer servedCustomer = service.ServeCustomer();
//         // Assert that the correct customer is served and removed from the queue
//         Assert.AreEqual(customer1, servedCustomer, "ServeCustomer should serve the correct customer.");
//         System.Reflection.FieldInfo field = service.GetType().GetField("customerQueue", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
//         Queue<Customer> queue = (Queue<Customer>)field.GetValue(service);
//         Assert.AreEqual(1, queue.Count, "ServeCustomer should remove customer from queue.");
//     }

//     [TestMethod]
//     // Test case for ServeCustomer when the queue is empty
//     public void ServeCustomer_QueueEmpty_DisplaysErrorMessage()
//     {
//         // Redirect console output to a StringWriter to capture the error message
//         using (var consoleOutput = new System.IO.StringWriter())
//         {
//             Console.SetOut(consoleOutput); // Set the console output to the StringWriter

//             CustomerService service = new CustomerService(1);
//             service.ServeCustomer(); // This should trigger the error message

//             // Assert that the error message is displayed
//             string expectedErrorMessage = "Error: Customer queue is empty. No customers to serve.";
//             Assert.IsTrue(consoleOutput.ToString().Contains(expectedErrorMessage), "ServeCustomer should display error message when queue is empty.");
//             Console.SetOut(Console.Out); // Reset console output
//             Assert.IsNull(service.ServeCustomer(), "ServeCustomer should return null when queue is empty");
//         }
//     }
// }

