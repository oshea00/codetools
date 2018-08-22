using System;
using System.Collections.Generic;
using static Functions;
using static System.Math;

public class MinHeap<T> where T : IComparable
{
    T[] items;
    long N;

    public MinHeap(int size)
    {
        items = new T[size+1];
        N = 0;
    }

    public T Pop() {
        if (!IsEmpty) {
            T item = items[1];
            Exch(1,N--);
            items[N+1]=default(T);
            Sink(1);
            return item;
        }
        else {
            throw new Exception("Empty Heap");
        }
    }

    public void Push(T item) {
        if (!IsFull) {
            items[++N] = item;
            Swim(N);
        }
        else {
            throw new Exception("Heap Full.");
        }
    }

    public bool IsFull => (N == items.Length-1);
    public bool IsEmpty => (N == 0);
    public T[] Items => items;

    private bool Less(long i, long j) {
        return items[i].CompareTo(items[j]) < 0;    
    }

    private void Exch(long i, long j) {
        Swap(ref items[i], ref items[j]);
    }

    private void Sink(long pos) {
        while (2*pos <= N)
        {
            var lesspos = 2*pos;
            if (lesspos < N && Less(lesspos+1,lesspos))
                lesspos++;
            if (Less(pos,lesspos))
                break;
            Exch(pos,lesspos);
            pos = lesspos;
        }
    }

    private void Swim(long pos)
    {
        while (pos > 1 && Less(pos,pos/2))
        {
            Exch(pos/2,pos);
            pos = pos/2;
        }
    }
}
