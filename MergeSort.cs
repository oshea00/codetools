using System;

public class MergeSort<T> where T : IComparable
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

    void Sort(T[] a,int lo, int hi) {
        if (lo>=hi)
            return;
        var mid = lo+(hi-lo)/2;
        Sort(a,lo,mid);
        Sort(a,mid+1,hi);
        Merge(a,lo,mid,hi);
    }

    public void Sort(T[] a) {
        aux = new T[a.Length];
        Sort(a,0,a.Length-1);
    }
}