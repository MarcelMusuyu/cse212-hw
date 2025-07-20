using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

[TestClass]
public class PriorityQueue_Tests
{
    // Scenario: Enqueue multiple items with varying priorities and verify they are added to the back.
    // Expected Result: Queue should maintain insertion order for Enqueue: [(A,1), (B,5), (C,2)]
    // Defects Found: None. The Enqueue method correctly adds items to the back.
    [TestMethod]
    public void TestEnqueue_BasicFunctionality()
    {
        var queue = new PriorityQueue();
        queue.Enqueue("A", 1);
        queue.Enqueue("B", 5);
        queue.Enqueue("C", 2);

        // This check implicitly relies on ToString() or internal inspection,
        // but for a true test, we'd ideally have an internal peek or size check.
        // For now, let's rely on subsequent Dequeue tests to confirm.
        Assert.AreEqual("[A (Pri:1), B (Pri:5), C (Pri:2)]", queue.ToString());
    }

    // Scenario: Dequeue from an empty queue.
    // Expected Result: InvalidOperationException with message "The queue is empty."
    // Defects Found: None. The Dequeue method correctly throws the specified exception.
    [TestMethod]
    public void TestDequeue_EmptyQueueThrowsException()
    {
        var queue = new PriorityQueue();
        var caughtException = false;
        try
        {
            queue.Dequeue();
        }
        catch (InvalidOperationException ex)
        {
            Assert.AreEqual("The queue is empty.", ex.Message);
            caughtException = true;
        }
        Assert.IsTrue(caughtException, "Expected InvalidOperationException was not thrown.");
    }

    // Scenario: Dequeue with a single item in the queue.
    // Expected Result: The single item should be dequeued.
    // Defects Found: None. The Dequeue method handles a single item correctly.
    [TestMethod]
    public void TestDequeue_SingleItem()
    {
        var queue = new PriorityQueue();
        queue.Enqueue("Solo", 10);
        var dequeuedValue = queue.Dequeue();
        Assert.AreEqual("Solo", dequeuedValue);
        Assert.IsTrue(queue.IsEmpty(), "Queue should be empty after dequeuing the single item.");
    }

    // Scenario: Dequeue multiple items, ensuring the highest priority item is removed.
    // Expected Result: "B" (priority 5) should be dequeued first, then "C" (priority 2).
    // Defects Found:
    // 1. The `for` loop condition in `Dequeue` (`index < _queue.Count - 1`) was off by one.
    //    It would skip the very last element of the list when searching for the highest priority.
    // 2. The `Dequeue` method correctly identified the highest priority item but did not
    //    actually remove it from the `_queue` list, leading to repeated dequeues of the same item
    //    and the queue never emptying.
    [TestMethod]
    public void TestDequeue_HighestPriority()
    {
        var queue = new PriorityQueue();
        queue.Enqueue("A", 1);
        queue.Enqueue("B", 5);
        queue.Enqueue("C", 2);

        // First dequeue: B (priority 5)
        Assert.AreEqual("B", queue.Dequeue(), "Dequeue should return the item with the highest priority (B).");
        Assert.AreEqual("[A (Pri:1), C (Pri:2)]", queue.ToString(), "Queue state after B: [A (Pri:1), C (Pri:2)]"); // Verify state

        // Second dequeue: C (priority 2)
        Assert.AreEqual("C", queue.Dequeue(), "Dequeue should return the next highest priority item (C).");
        Assert.AreEqual("[A (Pri:1)]", queue.ToString(), "Queue state after C: [A (Pri:1)]"); // Verify state

        // Third dequeue: A (priority 1)
        Assert.AreEqual("A", queue.Dequeue(), "Dequeue should return the last item (A).");
        Assert.IsTrue(queue.IsEmpty(), "Queue should be empty after all items are dequeued.");
    }

    // Scenario: Dequeue when multiple items have the same highest priority.
    // Expected Result: The item closest to the front of the queue with the highest priority should be removed.
    // Defects Found:
    // 1. The comparison `_queue[index].Priority >= _queue[highPriorityIndex].Priority` in `Dequeue`
    //    was incorrect. For "first one (following the FIFO strategy) is removed first" with ties,
    //    it should be `_queue[index].Priority > _queue[highPriorityIndex].Priority` or iterate
    //    from the beginning and only update `highPriorityIndex` if the priority is *strictly* greater.
    //    If equal, the current `highPriorityIndex` (which is closer to the front) should be kept.
    //    The fix involved changing the condition to `>` to ensure the earlier item is prioritized in ties.
    [TestMethod]
    public void TestDequeue_HighestPriorityFIFO_TieBreaker()
    {
        var queue = new PriorityQueue();
        queue.Enqueue("FirstHigh", 5);  // Index 0
        queue.Enqueue("Medium", 3);     // Index 1
        queue.Enqueue("SecondHigh", 5); // Index 2
        queue.Enqueue("Low", 1);        // Index 3

        // Expected: FirstHigh (5) should be dequeued, as it came first among items with priority 5
        Assert.AreEqual("FirstHigh", queue.Dequeue(), "Dequeue should prioritize the item closest to the front for tied highest priorities.");
        Assert.AreEqual("[Medium (Pri:3), SecondHigh (Pri:5), Low (Pri:1)]", queue.ToString());

        // Expected: SecondHigh (5) should be dequeued next
        Assert.AreEqual("SecondHigh", queue.Dequeue(), "Dequeue should get the remaining highest priority item.");
        Assert.AreEqual("[Medium (Pri:3), Low (Pri:1)]", queue.ToString());

        // Expected: Medium (3)
        Assert.AreEqual("Medium", queue.Dequeue());
        Assert.AreEqual("[Low (Pri:1)]", queue.ToString());

        // Expected: Low (1)
        Assert.AreEqual("Low", queue.Dequeue());
        Assert.IsTrue(queue.IsEmpty());
    }
}