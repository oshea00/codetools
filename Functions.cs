using System;
using System.Collections.Generic;
using System.Text;
using static System.Math;

public class Job {
    public string Name { get; set; }
    public bool Done { get; set; }
    public bool Running { get; set; }
    public List<string> Dependencies { get; set; }
}

public static class Functions {
    static Random r = new Random(System.Environment.TickCount);

    public static long Gcd(long a, long b) {
        if (a%b == 0)
            return b;
        else
            return Gcd(b,a%b);
    }

    public static bool IsPrime(long p) {
        if (p==2 || p==3)
            return true;
        if (p<2 || p%2==0 || p%3==0)
            return false;
        var i = 5;
        var w = 2;
        while (i*i <= p) {
            if (p % i == 0)
                return false;
            i += w;
            w = 6 - w;
        }
        return true;
    }

    public static IEnumerable<long> GetPrimes() {
        yield return 2;
        yield return 3;
        long n=3;
        while (true) {
            n += 2;
            if (IsPrime(n))
                yield return n;
        }
    }

    public static void Permutations<T>(T[] A, Action<T[]> f) {
        Permutations(A,0,A.Length - 1,f);
    }

    public static void Permutations<T>(T[] A, int k, int m, Action<T[]> f) {

        if (k == m) {
            f(A);
        }
        else {
            for (int i=k; i<=m; i++) {
                Swap(ref A[k],ref A[i]);
                Permutations(A,k+1,m,f);
                Swap(ref A[k],ref A[i]);
            }
        }
    }

    public static T[] Insert<T>(this T[] me, T item, int pos) {
        int n = me.Length;
        Array.Resize(ref me,n+1);
        for (int i=n; i>pos; i--) {
            me[i]=me[i-1];
        }
        me[pos]=item;
        return me;
    }

    public static void CopyTo<T>(this T[] a,T [] b) {
        for (int i=0;i<a.Length;i++)
        {
            b[i] = a[i];
        }
    }

    public static int MaxProduct3(int[] A) {
        //A.Shuffle();
        Array.Sort(A);
        var N = A.Length;

        // only 3 or all nonpos xxx nnn
        if (N==3 || A[N-1] <=0)
            return A[N-3]*A[N-2]*A[N-1];

        // ppp or nnp ?
        if ((A[0] < 0 && A[1] < 0) && (A[0]*A[1]*A[N-1] > A[N-1]*A[N-2]*A[N-3])) {
                // nnp
                return A[0]*A[1]*A[N-1];
        }

        // ppp
        return A[N-3]*A[N-2]*A[N-1];
    }


    // Given 2 Arrays representing a list of binary choices (A or B),
    // call given function with each possible choice combination until
    // f returns false or we are done generating all the options.
    // Definitely brute force O(2^N) - but correct and at least allows
    // for an early stop if some condition is met before all options
    // are explored.
    public static void TraverseChoices<T>(T[] A, T[] B, Func<T[], bool> f) {
        var N = A.Length;
        var C = new T[2][] {A,B};
        var P = (long) Pow(2,N);
        var c = new T[N];
        for (long i=0;i<P;i++) {
            var n = i;
            var idx = 0;
            while (n>0) {
                var choice = n%2;
                c[idx] = C[choice][idx];
                n /= 2;
                idx++;
            }
            while (idx<N) {
                c[idx] = C[0][idx++];
            }
            if (f(c) == false)
                break;
        }
    } 

    public static long[] PrefixSums(int[] A)
    {   
        var n = A.Length + 1;
        var sums = new long[n];
        for (int i=1;i<n;i++) {
            sums[i]=sums[i-1]+A[i-1];
        }
        return sums;
    }

    public static double[] Differentials(double[] A)
    {
        var n = A.Length+1;
        var difs = new double[n];
        for (int i=1;i<n-1;i++) {
            difs[i]=A[i]-A[i-1];
        }
        return difs;
    }

    public static double[] PrefixAvgs(int[] A)
    {   
        var n = A.Length + 1;
        var sums = new double[n];
        for (int i=1;i<n;i++) {
            sums[i]=((sums[i-1]*(i-1))+A[i-1])/i;
        }
        return sums;
    }

    public static Tuple<int,int,long[]> PrefixSumsAndStats(int[] A) {
        var n = A.Length + 1;
        int min=int.MaxValue, max=0;
        var sums = new long[n];
        for (int i=1;i<n;i++) {
            sums[i]=sums[i-1]+A[i-1];
            min = Min(min,A[i-1]);
            max = Max(max,A[i-1]);
        }
        return new Tuple<int,int,long[]>(min,max,sums);
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

    // Find the only missing sequence number in given array
    // assume no dupes and only one missing item.
    // assume first item in sequence s/b step.
    // If all items present return next item
    public static int FindSinglePositiveMissingSequenceItem(int[] A, int step=1)
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

    // Returns first missing sequence element in given array
    // assuming duplicates.
    // if all elements are present (or none) return the next one.
    public static int FindPositiveMissingSequenceItem(int[] A)
    {
        int smallest = 1;
        var counts = new HashSet<int>();
        int min = int.MaxValue;
        int max = 1;
        for (int i=0;i<A.Length;i++)
        {
            if (A[i]>0)
            {
                counts.Add(A[i]);
                min = Min(min,A[i]);
                max = Max(max,A[i]);
            }
        }

        if (min > 1)
            return smallest;

        for (int i=1;i<max;i++)
        {
            if (!counts.Contains(i))
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
        if (r == N || r == 0)
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

