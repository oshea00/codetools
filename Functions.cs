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

    public static void AssertIsNull<T>(T val)
    {
        if (val != null)
        {
            Console.WriteLine("Expected Null");
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

    public static string ListToString<T>(this List<T> list) {
        var sb = new StringBuilder();
        sb.Append("[");
        foreach (var l in list) {
            sb.Append($"{l.ToString()},");
        }
        sb.Remove(sb.Length-1,1);
        sb.Append("]");
        return sb.ToString();
    }

    public static string ListJoin<T>(this T[] list) {
        return "["+String.Join(",",list)+"]";
    }

    public static string ListJoin<T>(this List<T> list) {
        return "["+String.Join(",",list)+"]";
    }
    
    public static string ListToString<T>(this T[] list) {
        var sb = new StringBuilder();
        sb.Append("[");
        foreach (var l in list) {
            sb.Append($"{l.ToString()},");
        }
        sb.Remove(sb.Length-1,1);
        sb.Append("]");
        return sb.ToString();
    }

    public static string TreeInOrderToString(this Tree<string> t) {
        var visits = new List<string>();
        var s = new Stack<Tree<string>>();
        var p = t;
        while (true) {
            if (p == null)
            {
                if (s.Count==0) {
                    break;
                }
                else {
                    p = s.Pop();
                    visits.Add(p.Value); // Visit Then Right
                    p = p.Right;
                }
            }
            else {
                s.Push(p);
                p = p.Left;
            }
        }
        return visits.ListToString();
    }

    public static string TreePreOrderToString(this Tree<string> t) {
        var visits = new List<string>();
        var s = new Stack<Tree<string>>();
        var p = t;
        while (true) {
            if (p == null)
            {
                if (s.Count==0) {
                    break;
                }
                else {
                    p = s.Pop();
                    p = p.Right;
                }
            }
            else {
                s.Push(p);
                visits.Add(p.Value); // Visit Then Left 
                p = p.Left;
            }
        }
        return visits.ListToString();
    }

    public static string TreePostOrderToString(this Tree<string> t) {
        var visits = new List<string>();
        var s = new Stack<Tree<string>>();
        var p = t;
        var priorp = p;
        while (true) {
            if (p == null)
            {
                if (s.Count==0) {
                    break;
                }
                else {
                    p = s.Pop();
                    // Visit if right visited
                    if (p.Right == null || p.Right == priorp) {
                        visits.Add(p.Value);
                        priorp = p;
                        p = null;
                    } else {
                        s.Push(p);
                        p = p.Right;
                    }
                }
            }
            else {
                s.Push(p);
                p = p.Left;
            }
        }
        return visits.ListToString();
    }

    public static Tree<string> TreeFromExp(string e) {
        var expr = e.ToCharArray();
        var s = new Stack<Tree<string>>();
        Tree<string> t = new Tree<string>("root");
        s.Push(t);
        var q = t;
        var topTree = t;
        var i = 0;
        var level =0;
        while (i < expr.Length && s.Count != 0) {
            switch (expr[i]) {
                case '(' : 
                    level += 1;
                    topTree.Left = new Tree<string>(expr[i+1].ToString());
                    topTree = topTree.Left;
                    s.Push(topTree);
                    i += 1;
                    //$"toptree = {topTree.Value}".Dump();
                    break;
                case ')' :
                    topTree = s.Pop();
                    level -= 1;
                    //$"<={s.Peek().Value} {level}".Dump();
                    break;
                case ',' :
                    q = topTree;
                    while (q.Right != null)
                        q = q.Right;
                    q.Right = new Tree<string>(expr[i+1].ToString());
                    topTree = q.Right;
                    i += 1;
                    //$"To {s.Peek().Value} add sibling {topTree.Value}".Dump();
                    break;
                default : break;
            }
            i += 1;
        }
        return t;
    }
    public static void Huffman(int[] w) {
        // # A[m..2m-1] original weights as external node weights
        // # A[i] weight of node internal node i
        // # L[i] R[i] left and right children of node i
        var m = w.Length-1;
        var A = new int[2*m+1];
        //$"len A={2*m+1}".Dump();
        var L = new int[m+1];
        var R = new int[m+1];
        // # H1
        for (int z=1;z < m+1; z++)
            A[m-1+z]=w[z];
        A[2*m] = 9999;
        var x = m;
        var i = m + 1;
        var j = m - 1;
        var k = m;
        var y = 0;
        // # H2
        while (true) {
            if (j<k || A[i] <= A[j]) {
                y=i;
                i = i+1;
            } else {
                y=j;
                j=j-1;
            }
            // H3.
            k=k-1;
            L[k]=x;
            R[k]=y;
            A[k]=A[x]+A[y];
            // # H4
            if (k==1)
                break;
            // # H5
            if (A[y] < 0) {
                j=k;
                i=y+1;
                // A[i] s/b > A[j]
            }
            if (A[i] <= A[j]) {
                x=i;
                i=i+1;
            } else {
                x=j;
                j=j-1;
            }
        }
            
        A.ListToString<int>().Dump();
        L.ListToString<int>().Dump();
        R.ListToString<int>().Dump();
    }

}

class PolyTerm {
    public int Coef { get; set; }
    public int Exp { get; set; }
    public PolyTerm Link { get; set; }

    public PolyTerm()
    {
        Exp = -1;
        Link = this;
    }

    public PolyTerm(int coef, int exp, PolyTerm link = null) {
        Coef = coef;
        Exp = exp;
        Link = link;
    }

    public bool IsTail => Exp == -1;

    public override string ToString() {
        var sb = new StringBuilder();
        var q1 = this.Link;
        while (q1.Exp >= 0) {
            var x = q1.Exp / 100;
            var y = (q1.Exp - (x*100)) / 10;
            var z = (q1.Exp - (x*100) - (y*10));
            var xexp = (x>1) ? $"x^{x}" : (x==1)? "x" : "";
            var yexp = (y>1) ? $"y^{y}" : (y==1)? "y" : "";
            var zexp = (z>1) ? $"z^{z}" : (z==1)? "z" : "";
            var sgn = (q1.Coef >= 0) ? $"+" : $"-";
            var sgncoef = (Abs(q1.Coef) > 1) ? $"{sgn}{Abs(q1.Coef)}" : $"{sgn}";
            sb.Append($"{sgncoef}{xexp}{yexp}{zexp} ");
            q1 = q1.Link;
        }
        if (sb.Length>0) {
            if (sb[0]=='+')
                sb.Remove(0,1);
            sb.Remove(sb.Length-1,1);
        }
        return sb.ToString();
    }

}

