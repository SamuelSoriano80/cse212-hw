using Microsoft.VisualStudio.TestTools.UnitTesting;

// TODO Problem 2 - Write and run test cases and fix the code to match requirements.

[TestClass]
public class PriorityQueueTests
{
    [TestMethod]
    // Scenario: Enqueue three items and verify that ToString matches expected order (back of queue).
    // Expected Result: All items are added in the order they were enqueued.
    // Defect(s) Found: None.
    public void TestPriorityQueue_EnqueueAddsToBack()
    {
        var priorityQueue = new PriorityQueue();
        priorityQueue.Enqueue("Low", 1);
        priorityQueue.Enqueue("Medium", 5);
        priorityQueue.Enqueue("High", 10);

        var expected = "[Low (Pri:1), Medium (Pri:5), High (Pri:10)]";
        Assert.AreEqual(expected, priorityQueue.ToString());
    }

    [TestMethod]
    // Scenario: Enqueue items with different priorities and call Dequeue.
    // Expected Result: Item with highest priority is returned.
    // Defect(s) Found: None
    public void TestPriorityQueue_DequeueHighestPriority()
    {
        var priorityQueue = new PriorityQueue();
        priorityQueue.Enqueue("Low", 1);
        priorityQueue.Enqueue("High", 10);
        priorityQueue.Enqueue("Medium", 5);

        string result = priorityQueue.Dequeue();
        Assert.AreEqual("High", result);
    }

    [TestMethod]
    // Scenario: Enqueue items with same highest priority, expect front-most one to be returned.
    // Expected Result: "First" is returned since it was enqueued before "Second".
    // Defect(s) Found: Same as above, loop must include the last index.
    public void TestPriorityQueue_DequeuesFrontOfTiedPriorities()
    {
        var priorityQueue = new PriorityQueue();
        priorityQueue.Enqueue("First", 10);
        priorityQueue.Enqueue("Second", 10);
        priorityQueue.Enqueue("Third", 5);

        string result = priorityQueue.Dequeue();
        Assert.AreEqual("First", result);
    }

    [TestMethod]
    // Scenario: Dequeue from an empty queue.
    // Expected Result: InvalidOperationException with message "The queue is empty."
    // Defect(s) Found: None.
    public void TestPriorityQueue_EmptyDequeueThrows()
    {
        var priorityQueue = new PriorityQueue();

        var ex = Assert.ThrowsException<InvalidOperationException>(() => priorityQueue.Dequeue());
        Assert.AreEqual("The queue is empty.", ex.Message);
    }

    [TestMethod]
    // Scenario: Enqueue out of priority order and Dequeue.
    // Expected Result: Highest priority ("Urgent") is returned even though enqueued last.
    // Defect(s) Found: Same loop logic bug in Dequeue.
    public void TestPriorityQueue_EnqueueOrderDoesNotAffectDequeue()
    {
        var priorityQueue = new PriorityQueue();
        priorityQueue.Enqueue("Normal", 3);
        priorityQueue.Enqueue("Low", 1);
        priorityQueue.Enqueue("Urgent", 99);

        string result = priorityQueue.Dequeue();
        Assert.AreEqual("Urgent", result);
    }
}