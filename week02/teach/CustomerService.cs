/// <summary>
/// Maintain a Customer Service Queue.  Allows new customers to be 
/// added and allows customers to be serviced.
/// </summary>
public class CustomerService
{
    public static void Run()
    {
        // Example code to see what's in the customer service queue:
        // var cs = new CustomerService(10);
        // Console.WriteLine(cs);

        // Test Cases

        // Test 1
        // Scenario: What happens if I initialize the queue with an invalid size
        // Expected Result: The queue should default to a size of 10
        Console.WriteLine("Test 1");
        var service = new CustomerService(0);
        Console.WriteLine(service);

        // Defect(s) Found: None

        Console.WriteLine("=================");

        // Test 2
        // Scenario: Add two customers and then serve the customers in the right order
        // Expected Result: This should display the customers in the same order that they were entered
        Console.WriteLine("Test 2");
        service = new CustomerService(4);
        service.AddNewCustomer(); // Input: Alice, 001, Internet not working
        service.AddNewCustomer(); // Input: Bob, 002, Billing issue
        Console.WriteLine("Before serving customers:");
        Console.WriteLine(service);
        service.ServeCustomer();  // Should print Alice
        service.ServeCustomer();  // Should print Bob
        Console.WriteLine("After serving customers:");
        Console.WriteLine(service);

        // Defect(s) Found: ServeCustomer() removes first, then tries to access it again
        // Fix: First var customer = _queue[0]; then _queue.RemoveAt(0);

        Console.WriteLine("=================");

        // Test 3
        // Scenario: Add a customer when the queue is full
        // Expected Result: An error message should be displayed
        Console.WriteLine("Test 3");
        service = new CustomerService(2);
        service.AddNewCustomer(); // Input: Charlie, 003, Login issue
        service.AddNewCustomer(); // Input: Dana, 004, Lost password
        service.AddNewCustomer(); // Input: Eve, 005, Account hacked (should show error)
        Console.WriteLine(service);

        // Defect(s) Found: Bug in AddNewCustomer condition (allows 1 extra)
        // Fix: if (_queue.Count >= _maxSize)

        Console.WriteLine("=================");

        // Test 4
        // Scenario: Serve a customer when the queue is empty
        // Expected Result: An error message should be displayed
        Console.WriteLine("Test 4");
        service = new CustomerService(3);
        service.ServeCustomer(); // Should print error message

        // Defect(s) Found: ServeCustomer crashes with empty queue
        // Fix: Add if (_queue.Count == 0) check

        Console.WriteLine("=================");

        // Test 5
        // Scenario: Can I add multiple customers up to the limit and still maintain correct order and size?
        // Expected Result: Customers should be served in correct order and max size respected
        Console.WriteLine("Test 5");
        service = new CustomerService(3);
        service.AddNewCustomer(); // Input: Frank, 006, Connection drop
        service.AddNewCustomer(); // Input: Grace, 007, Slow speed
        service.AddNewCustomer(); // Input: Heidi, 008, Plan upgrade
        Console.WriteLine("Before serving all customers:");
        Console.WriteLine(service);
        service.ServeCustomer(); // Frank
        service.ServeCustomer(); // Grace
        service.ServeCustomer(); // Heidi
        Console.WriteLine("After serving all customers:");
        Console.WriteLine(service);

        // Defect(s) Found: None

        Console.WriteLine("=================");
    }

    private readonly List<Customer> _queue = new();
    private readonly int _maxSize;

    public CustomerService(int maxSize)
    {
        if (maxSize <= 0)
            _maxSize = 10;
        else
            _maxSize = maxSize;
    }

    /// <summary>
    /// Defines a Customer record for the service queue.
    /// This is an inner class.  Its real name is CustomerService.Customer
    /// </summary>
    private class Customer
    {
        public Customer(string name, string accountId, string problem)
        {
            Name = name;
            AccountId = accountId;
            Problem = problem;
        }

        private string Name { get; }
        private string AccountId { get; }
        private string Problem { get; }

        public override string ToString()
        {
            return $"{Name} ({AccountId})  : {Problem}";
        }
    }

    /// <summary>
    /// Prompt the user for the customer and problem information.  Put the 
    /// new record into the queue.
    /// </summary>
    private void AddNewCustomer()
    {
        // Verify there is room in the service queue
        if (_queue.Count >= _maxSize)
        {
            Console.WriteLine("Maximum Number of Customers in Queue.");
            return;
        }

        Console.Write("Customer Name: ");
        var name = Console.ReadLine()!.Trim();
        Console.Write("Account Id: ");
        var accountId = Console.ReadLine()!.Trim();
        Console.Write("Problem: ");
        var problem = Console.ReadLine()!.Trim();

        // Create the customer object and add it to the queue
        var customer = new Customer(name, accountId, problem);
        _queue.Add(customer);
    }

    /// <summary>
    /// Dequeue the next customer and display the information.
    /// </summary>
    private void ServeCustomer()
    {
        if (_queue.Count == 0)
        {
            Console.WriteLine("No customers to serve.");
            return;
        }

        var customer = _queue[0];
        _queue.RemoveAt(0);
        Console.WriteLine(customer);
    }

    /// <summary>
    /// Support the WriteLine function to provide a string representation of the
    /// customer service queue object. This is useful for debugging. If you have a 
    /// CustomerService object called cs, then you run Console.WriteLine(cs) to
    /// see the contents.
    /// </summary>
    /// <returns>A string representation of the queue</returns>
    public override string ToString()
    {
        return $"[size={_queue.Count} max_size={_maxSize} => " + string.Join(", ", _queue) + "]";
    }
}