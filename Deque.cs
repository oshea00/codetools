using System;
using System.Collections.Generic;

public interface IDeque<T> {
    bool IsEmpty { get; }
    int Count { get; }
    T Pop();
    T Dequeue();
    void Push(T item);
    void Enqueue(T item);
}

// Wraps double-linked list LinkedList<T> to get good O(1) insert/delete performance
public class Deque<T> : IDeque<T>
{
    LinkedList<T> items;
    public Deque()
    {
        items = new LinkedList<T>();
    }

    public bool IsEmpty => items.Count == 0;

    public int Count => items.Count;

    public T Dequeue()
    {
        if (!IsEmpty) {
            T item = items.First.Value;
            items.RemoveFirst();
            return item;
        }
        else {
            throw new Exception("Deque Empty");
        }
    }

    public void Enqueue(T item)
    {
        items.AddLast(item);
    }

    public T Pop()
    {
        if (!IsEmpty) {
            T item = items.Last.Value;
            items.RemoveLast();
            return item;
        }
        else {
            throw new Exception("Deque Empty");
        }
    }

    public void Push(T item)
    {
        items.AddFirst(item);
    }
}

// Simple Deque - depends on List<T> performance which is O(N)? for inserts/deletes.
// A LinkedList<T> (doubly linked btw) will likely do a better job and is O(1)? for 
// inserts/deletes.
public class DequeList<T> : IDeque<T> {
    List<T> _items;
    public bool IsEmpty => _items.Count == 0;
    public int Count => _items.Count;

    public DequeList()
    {
        _items = new List<T>(100);
    }

    public DequeList(int capacity) : this() {
        _items.Capacity = capacity;
    }

    public void Enqueue(T item) {
        _items.Add(item);
    }

    public T Dequeue() {
        if (!IsEmpty) {
            T item = _items[0];
            _items.Remove(item);
            return item;
        }
        else {
            throw new Exception("DequeList is empty.");
        }
    }

    public T Pop() {
        if (!IsEmpty) {
            T item = _items[_items.Count-1];
            _items.Remove(item);
            return item;
        }
        else {
            throw new Exception("DequeList is empty.");
        }
    }

    public void Push(T item) {
        _items.Insert(0,item);
    }
}
