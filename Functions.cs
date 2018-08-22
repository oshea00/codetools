using System;
using System.Collections.Generic;
using System.Text;
using static System.Math;

public static class Functions {
    static Random r = new Random(System.Environment.TickCount);

    public static long[] PrefixSums(int[] A)
    {   
        var n = A.Length + 1;
        var sums = new long[n];
        for (int i=1;i<n;i++) {
            sums[i]=sums[i-1]+A[i-1];
        }
        return sums;
    }

    // update counters 1-N using an input command list:
    // containing counter # to increment. If N+1, treat
    // as "set all to current max count" command.
    public static int[] UpdateCounters(int N, int[] A) {
        var counters = new int[N];
        var bonus = new int[N];
        int maxCmd = N+1;
        int lastMax=0;
        int maxCount=0;
        for (int i=0;i<A.Length;i++)
        {
            if (A[i] == maxCmd)
            {
                if (maxCount > lastMax)
                {
                    for (int j=0;j<bonus.Length;j++)
                    {
                        bonus[j] = (maxCount - counters[j]);
                    }
                    lastMax = maxCount;
                }
            }
            else
            {
                if (bonus[A[i]-1]>0)
                {
                    counters[A[i]-1] += (1 + bonus[A[i]-1]);
                    bonus[A[i]-1] = 0;
                }
                else
                {
                    counters[A[i]-1] += 1;
                }
                maxCount = Max(maxCount,counters[A[i]-1]);
            }
        }
        for (int j=0;j<bonus.Length;j++)
            counters[j] += bonus[j];
        return counters;
    }

    public static int WhenAllFound1ThruX(int[] A, int x) {
        var all = new HashSet<int>();
        if (A.Length<x)
            return -1;
        for (int i=0;i<A.Length;i++)
        {
            all.Add(A[i]);
            if (all.Count==x)
                return i;
        }
        return -1;
    }

    public static bool CanMakeSumsEqual(int[] A, int[] B, int m) {
        var suma = A.Sum();
        var sumb = B.Sum();
        var d = sumb - suma;
        if (d==0)
            return true;
        if (Abs(d % 2) == 1)
            return false;
        d /= 2; // net amount we have to evenly add/subtract 
        var countsA = CountItems(A,m);
        for (int i=0; i<A.Length; i++) {
            var v = B[i]-d;  // Element in b which gives d/2 diff
            if (v >=0 && v <= m && countsA[v] > 0) // is it in A?
                return true;
        }
        return false;
    }

    public static int[] CountItems(int[] A, int m) {
        var cnts = new int[m+1];
        foreach (int val in A) {
            cnts[val] += 1;
        }
        return cnts;
    }
    
    public static int[] MinSum(int[] A, int[] B) {
        // O(n)
        int[] s = new int[A.Length];
        for (int i=0;i<A.Length;i++)
        {
            s[i] = Min(A[i],B[i]);
        }
        return s;
    }

    // Returns first missing sequence element in given array
    // assuming duplicates.
    // if all elements are present (or none) return the next one.
    public static int FindMissingSequenceItem(int[] A,int step=1)
    {
        // O(N + log N) worst case missing element @ N-1
        // Average O(N/2 + log N)
        // Overall O(N) 
        var set = new SortedSet<int>(A);
        int j=0;
        foreach (var i in set)
        {
            j += step;
            if (i > j)
                return j;
        }
        j += step;
        return j; // return next element 
    }

    // Returns first missing sequence element in given array
    // assuming duplicates.
    // if all elements are present (or none) return the next one.
    public static int FindPositiveMissingSequenceItem(int[] A,int step=1)
    {
        int smallest = 1;
        var counts = new int[1000001];
        int min = int.MaxValue;
        int max = 1;
        for (int i=0;i<A.Length;i++)
        {
            if (A[i]>0)
            {
                counts[A[i]]+=1;
                min = Min(min,A[i]);
                max = Max(max,A[i]);
            }
        }
        if (min>1 || max < 0)
            return smallest;
        for (int i=1;i<counts.Length;i++)
        {
            if (counts[i] == 0)
            {
                return i;
            }
        }
        return max+1;
    }

    public static void Exit() {
        System.Environment.Exit(0);
    }

    public static void AssertIsTrue(bool t)
    {
        if (t == false)
        {
            Console.WriteLine("Expected True");
            Exit();
        }
    }

    public static void AssertAreEqual<T>(T a, T b) {
        if (!a.Equals(b)) {
            Console.WriteLine($"Expected {a} got {b}");
            Exit();
        }
    }
    
    public static void AssertAreEqual<T>(IList<T> a, IList<T> b)
    {
        if (a.Count != b.Count) {
            Console.WriteLine($"Unequal array lengths");
            Exit();
        }
        for (int i=0;i<a.Count;i++)
        {
            if (!a[i].Equals(b[i]))
            {
                if (a.Count < 100)
                {
                    Dump(a);  
                    Dump(b);
                }
                Console.WriteLine($"Mismatched values at index {i}: {a[i]} <> {b[i]}");
                Exit();
            }
        }
    }

    // n "choose" r - or binomial numbers
    public static double nCr(int N, int r) {
        if (r == N)
            return 1;
        //  0 <= r < N
        double numerator = 1.0;
        double den = 1.0;
        // (N-r) terms will cancel to 1 in the numerator and demoninator
        for (int j=N-r+1;j<=N;j++) {
            numerator *= j;
        }
        // only terms < r+1 remain in the denominator
        for (int j=1;j<r+1;j++) {
            den *= j;
        }
        return Round(numerator/den,0);
    }

    // N permute r - positional permutations of r items from N items
    public static double nPr(int N, int r) {
        if (r==0)
            return 1;

        double numerator = 1.0;

        // terms <= N-r will cancel to 1 in numerator
        for (int i=N-r+1; i<=N; i++)
            numerator *= i;

        // denominator is 1.0 due to cancellation in numerator
        return Round(numerator / 1.0 ,0);
    }

    public static double Factorial(int N) {
        double f = 1;
        for (int i=1;i<=N;i++)
        {
            f *= i;
        }
        return f;
    }

    // Assuming duplicates present - return all missing items
    public static int[] FindAllMissingSequenceItem(int[] A,int step=1)
    {
        // O(N + log N) worst case missing element @ N-1
        // Average O(N/2 + log N)
        // Overall O(N) 
        var missing = new List<int>(A.Length);
        var set = new SortedSet<int>(A);
        int j=0;
        foreach (var i in set)
        {
            j += step;
            while (i > j)
            {
                missing.Add(j);
                j += step;
            }
        }
        if (missing.Count==0)
        {
            if (A.Length==0)
                missing.Add(step);
            else
                missing.Add(A[A.Length-1]+step);
        }
        return missing.ToArray();  
    }

    // Find the only missing sequence number in given array
    // assume no dupes and only one missing item.
    // assume first item in sequence s/b step.
    // If all items present return next item
    public static int FindMissingSequenceItemNoDupes(int[] A, int step=1)
    {
        // Use triangle number math to find the missing value
        long n = A.Length+1;
        long sum_sb = n*step*(n+1) / 2;
        long sum = A.Sum();
        int missing = (int)(sum_sb-sum);
        if (missing == 0)
        {
            return (int)(n * step) + step;
        }
        else
        {
            return missing;
        }
    }

    public static long Sum(this int[] a)
    {
        long sum=0;
        foreach(var i in a)
        {
            sum += i;
        }
        return sum;
    }

    public static decimal Sum(this double[] a)
    {
        decimal sum=0;
        foreach(var i in a)
        {
            sum = sum + (decimal) i;
        }
        return sum;
    }

    public static void Shuffle<T>(this T[] a)
    {
        int n = a.Length;
        for (int i=0; i < n/2; i++) {
            var k = (int)(r.NextDouble()*n);
            var j = (int)(r.NextDouble()*n);
            Swap(ref a[k],ref a[j]);
        }
    }

    public static void Swap<T>(ref T a, ref T b)
    {
        T tmp = a;
        a = b;
        b = tmp;
    }

    public static void Dump<T>(this T item) {
        Console.WriteLine(item.ToString());
    }

    public static void Dump<T>(this IList<T> A)
    {
        if (A.Count == 0)
        {
            Console.WriteLine("[]");
            return;
        }
        var sb = new StringBuilder();
        sb.Append("[");
        foreach(var a in A)
        {
            sb.Append($"{a},");
        }
        sb.Remove(sb.Length-1,1);
        sb.Append("]");
        Console.WriteLine(sb.ToString());
    }


}

