using System;
using static System.Math;

public class MergeSortIter<T> where T : IComparable
{
    T[] aux;

    void Merge(T[] a,int lo,int mid, int hi) {
        int i = lo;
        int j = mid+1;
        for (int k=lo;k<=hi;k++)
            aux[k] = a[k];
        for (int k=lo;k<=hi;k++) {
            if (i>mid)
                a[k]=aux[j++];
            else if (j>hi) 
                a[k]=aux[i++];
            else if (aux[i].CompareTo(aux[j]) < 0)
                a[k]=aux[i++];
            else
                a[k]=aux[j++];
        }
    }

    public void Sort(T[] a) {
        aux = new T[a.Length];
        var n = a.Length;
        for (var step = 1; step < n-1; step=(2*step))
            for (var lo=0; lo<n-step; lo+=(2*step))
                Merge(a,lo,lo+step-1,Min(lo+(2*step)-1,n-1));
    }

}