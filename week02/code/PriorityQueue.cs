public class PriorityQueue
{
    private List<PriorityItem> _queue = new();

    /// <summary>
    /// Add a new value to the queue with an associated priority.  The
    /// node is always added to the back of the queue regardless of 
    /// the priority.
    /// </summary>
    /// <param name="value">The value</param>
    /// <param name="priority">The priority</param>
    public void Enqueue(string value, int priority)
    {
        var newNode = new PriorityItem(value, priority);
        _queue.Add(newNode);
    }

    public string Dequeue()
    {
        if (_queue.Count == 0) // Verify the queue is not empty
        {
            throw new InvalidOperationException("The queue is empty.");
        }

         // Find the index of the item with the highest priority to remove
        var highPriorityIndex = 0;

        for (int index = 1; index < _queue.Count; index++)
        {
            // The condition should be strictly greater (>) to ensure FIFO for ties.
            // If it's '>=', and a later item has the same priority, highPriorityIndex would update,
            // violating the "closest to the front" rule.
            if (_queue[index].Priority > _queue[highPriorityIndex].Priority)
            {
                highPriorityIndex = index;
            }
        }

        // Store the value of the item with the highest priority
        var value = _queue[highPriorityIndex].Value;

        // Remove the item from the queue
        _queue.RemoveAt(highPriorityIndex);
        return value;
    }

/// <summary>
    /// Gets the current number of items in the queue.
    /// </summary>
    public int Count => _queue.Count;

    /// <summary>
    /// Checks if the queue is empty.
    /// </summary>
    /// <returns>True if the queue is empty, false otherwise.</returns>
    public bool IsEmpty()
    {
        return _queue.Count == 0;
    }

    public override string ToString()
    {
        return $"[{string.Join(", ", _queue)}]";
    }
}

internal class PriorityItem
{
    internal string Value { get; set; }
    internal int Priority { get; set; }

    internal PriorityItem(string value, int priority)
    {
        Value = value;
        Priority = priority;
    }

    public override string ToString()
    {
        return $"{Value} (Pri:{Priority})";
    }
}