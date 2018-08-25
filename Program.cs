﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using static Functions;
using static Sequences;
using static System.Math;

namespace codility
{
    class Program
    {   
        static void Main(string[] args)
        {
            TestTraverseChoices();
            //TestMinAvg();
            //TestHowManyDivisible();   
            //TestFindMinImpact();
            //TestCountTransitions();
            //RunMushroomChamp();
            //TestMinHeap();
            //TestPrefixSums();
            //TestUpdateCounters();            
            //TestWhen1ThruXFound();
            //TestCanSwapToEqual();
            //TestItemCounts();
            //TestMinSum();
            //TestAsserts();
            //TestPermsOfR();
            //TestNChooseR();
            //MissingItems();
        }

        private static void TestTraverseChoices()
        {
            var A = new int[] {1,2};
            var B = new int[] {5,6};
            int i=0;

            TraverseChoices(A,B,(C)=>{
                C.Dump<int>();
                i++;
                return true;
            });
            AssertAreEqual(Pow(2,A.Length),i);

            i=0;
            TraverseChoices(A,B,(C)=>{
                C.Dump<int>();
                i++;
                return false;
            });
            AssertAreEqual(1,i);
        }

        private static void TestMinAvg()
        {
            AssertAreEqual(1,MinAvgTable(new int[] {4,2,2,5,1,5,8}));
            AssertAreEqual(5,MinAvgTable(new int[] {-4,-2,-2,-5,-1,-5,-3}));
            AssertAreEqual(0,MinAvgTable(new int[] {-4,2,-2,5,-1,5,-3}));
            AssertAreEqual(5,MinAvgTable(new int[] {1,2,3,4,5,-8,-9}));
            AssertAreEqual(1,MinAvgTable(new int[] {8,-2,3,-4,5,8,-9}));
            AssertAreEqual(2,MinAvgTable(new int[] {-3, -5, -8, -4, -10}));
        }

        static void AvgTable(int[] A)
        {
            var sums = PrefixSums(A);
            //sums.Dump<long>();
            int N = A.Length;
            double minAvg = double.MaxValue;
            int minPos = N-1;
            int lastRowPos=minPos;
            DumpSlice(A,0,N-1,0);
            for (int s=2; s<=N; s++)
            {
                Console.Write($"{s}  ");
                for (int i=0; i<Min(lastRowPos,N-(s-1)); i++) {
//                for (int i=0; i<N-(s-1); i++) {
                    var x=i;
                    var y=i+s-1;
                    var avg = ((double) sums[y+1]-sums[x])/s;  
                    //Console.Write($"{avg.ToString("0.0")} ");
                    if (avg<minAvg)
                    {
                        minAvg = avg;
                        minPos = i;
                        Console.Write($" {minAvg.ToString("0.0")}");
                    }
                }
                lastRowPos = N-minPos+1;
                Console.WriteLine($" {lastRowPos}");
            }
        }

        static int MinAvgTable(int[] A)
        {
            AvgTable(A);
            double minAvg = double.MaxValue;
            var sums = PrefixSums(A);
            int N = A.Length;
            int minPos = N-1;
            int lastRowPos=minPos;
            for (int s=2; s<=N; s++)
            {
                //for (int i=0; i<Min(minPos+1, N-(s-1)); i++) {
                for (int i=0; i<Min(lastRowPos,N-(s-1)); i++) {
                    var x=i;
                    var y=i+s-1;
                    var avg = ((double) sums[y+1]-sums[x])/s;
                    if (avg<minAvg)
                    {
                        minAvg = avg;
                        minPos = i;
                    }
                }
                lastRowPos = Min(minPos,N-minPos-1);
            }
            minPos.Dump();
            return minPos;
        }

        static void DumpSlice(int[] A,int i,int j,double avg)
        {
            var sb = new StringBuilder();
            sb.Append("  [");
            while (i<=j) {
                sb.Append(A[i]+"   ");
                i++;
            }
            sb.Remove(sb.Length-1,1);
            sb.Append("]");
            //sb.Append($" {avg.ToString("0.00")}");
            sb.ToString().Dump();
        }
        private static void TestHowManyDivisible()
        {
            AssertAreEqual(3,HowManyDivisible(6,11,2));
            AssertAreEqual(8,HowManyDivisible(0,14,2));
            AssertAreEqual(1,HowManyDivisible(10,10,5));
            AssertAreEqual(2,HowManyDivisible(11,14,2));
            AssertAreEqual(20,HowManyDivisible(11,345,17));
            AssertAreEqual(12345,HowManyDivisible(101, 123456789, 10000));
        }

        static int HowManyDivisible(int a, int b, int k)
        {
            // proved for a=b=0
            if (a == b && a == 0)
                return 1;

            // proved a=0, b>0 and a%k == 0
            if (a == 0 && b > 0 && a%k==0)
                return 1 + (b-a)/k;

            // proved a=b, a>0, a%k != 0
            if (a == b && a > 0 && a%k != 0)
                return (b-a)/k;

            // proved k>0, a>0, a<b, a%k == 0
            if (a % k == 0) {
                return 1 + (b-a)/k;
            }   
            else {
                // Make a%k==0 then above works a%k==0 a>0 a<b
                if (a+a%k>b)
                    return 0;
                else
                {
                    if (k<a)
                        a = a + a%k;
                    else
                        a = k;
                    return 1 + (b-a)/k;
                }
            }         
        }

        static void TestFindMinImpact() {
            string code = "CAGCCTA";
            var P = new int[] {2,5,0};
            var Q = new int[] {4,5,6}; 
            var a = minImpact(code,P,Q);
            a.Dump<int>();
            AssertAreEqual(new List<int>{2,4,1},a);
        }

        // 100% - Use prefix counts on binary marker array to
        // determin presence of events - in this case min value in string segments.
        static int[] minImpact(string code,int[] P, int[] Q) {
            var min = int.MaxValue;
            var max = 0;
            var impact = new Dictionary<char,int>() {
                {'A',1},
                {'C',2},
                {'G',3},
                {'T',4},
            };

            var codes = new int[code.Length];
            var codemins = new int[code.Length];

            for (int i=0;i<code.Length; i++) {
                var c = impact[code[i]];
                min = Min(min,c);
                max = Max(max,c);
                codes[i] = c;
            }

            for (int i=0;i<codemins.Length;i++) {
                codemins[i] = (codes[i] == min) ? 1 : 0;
            }

            var minsums = PrefixSums(codemins);

            var answers = new int[P.Length];
            for (int i=0;i<P.Length;i++) {
                if (min-max == 0) {
                    answers[i]=min; // If the whole string is same digit min/max are always equal.
                }
                else
                if (Q[i]-P[i]+1==codes.Length) {
                    answers[i]=min; // If querying the whole string - the min is the answer
                }
                else {
                    // We check for a min code in the P[i] Q[i] range
                    if (minsums[Q[i]+1]-minsums[P[i]] > 0)
                    {
                        answers[i]=min;
                    }
                    else {
                        // We have no min in the range - we'll need to check it.
                        var mincode=int.MaxValue;
                        for (int j=P[i];j<=Q[i];j++)
                        {
                            mincode = Min(mincode,codes[j]);
                        }
                        answers[i]=mincode;
                    }
                }
            }
            return answers;
        }

        static void TestCountTransitions() {
            var passing = CountTransitions(new int[]{0,1,0,1,1});
            AssertAreEqual(5,passing);
            $"Transitions = {passing} OK".Dump();
        }

        static long CountTransitions(int[] A) {
            var counts = PrefixSums(A);
            long total = 0;
            var n = A.Length;
            for (int i=0;i<n;i++) {
                if (A[i]==0) {
                    total += (counts[n]-counts[i]);
                    if (total > 1000000000)
                        return -1;
                }
            }
            return total;
        }

        static void RunMushroomChamp() {
            var A = new int[] {2, 3, 7, 5, 1, 3, 9};
            MushroomChamp(A,6,4);
        }

        static void MushroomChamp(int[] A, int maxmoves, int startPos) {
            var sums = PrefixSums(A);
            var maxX = 0;
            var maxY = 0;
            long maxSum = 0;
            A.Dump<int>();
            $"Starting pos = {startPos}, Max moves = {maxmoves}".Dump();
            var n = A.Length;
            int k = (startPos < n/2+1) ? startPos : n - 1 - startPos;
            for (int i=0; i < maxmoves && k - i >= 0; i++) {
                int x=0, y=0;
                if (i==0) {
                    if (startPos < n/2+1) 
                    {    x = k;     y = Min(k + (maxmoves-1),n-1); }
                    else
                    {    x = Max(n-1-k - (maxmoves-1),0);   y = n-1-k; }
                } else {
                    if (startPos < n/2+1)
                    {    x = k - i; y = Min(k + maxmoves-2*i ,n-1); }
                    else
                    {    x = Max(n-1-k - (maxmoves-2*i),0); y = n-1-k + i; }
                }
                var sum = sums[y+1] - sums[x];
                if (sum > maxSum) {
                    maxX = x;
                    maxY = y;
                    maxSum = sum;
                }
                //$"leftmost={x} rightmost={y}".Dump();
                //$"sum={sum}".Dump();
            }
            $"Best picking range[{maxX}..{maxY}] yields {maxSum}".Dump();
        }

        private static void TestMinHeap()
        {
            var h = new MinHeap<string>(10);
            h.Push("D");
            h.Push("C");
            h.Push("A");
            h.Push("Z");
            while (!h.IsEmpty)
            {
                h.Items.Dump<string>();
                h.Pop().Dump();
            }

            var n = new MinHeap<int>(4);
            n.Push(100);
            n.Push(2);
            n.Push(11);
            n.Push(9);

            while (!n.IsEmpty)
            {
                n.Items.Dump<int>();
                n.Pop().Dump();
            }
        }

        private static void TestPrefixSums() {
            var A = LinearSequence(10);
            A.Shuffle();
            A.Dump<int>();
            var sums = PrefixSums(A);
            sums.Dump<long>();
            var A0_9 = sums[10]-sums[0];
            $"Total A[0..9] = {A0_9}".Dump();
            AssertAreEqual(55,A0_9);
        }

        private static void TestUpdateCounters()
        {
            var stopWatch = new Stopwatch();
            stopWatch.Start();
            var s = UpdateCounters(100,new int[]{3,4,4,101,1,101,4});
            stopWatch.Stop();
            Console.WriteLine($"Elapsed {stopWatch.ElapsedMilliseconds} milliseconds.");
            AssertAreEqual(301,s.Sum());
        }

        public static void TestWhen1ThruXFound() {
            var stopWatch = new Stopwatch();
            var seq = LinearSequence(10);
            seq.Shuffle();
            seq.Dump<int>();
            stopWatch.Start();
            var canCross = WhenAllFound1ThruX(seq,5);
            stopWatch.Stop();
            Console.WriteLine($"Elapsed {stopWatch.ElapsedMilliseconds} milliseconds.");
            AssertAreEqual(999999,canCross);
        }

        public static void TestCanSwapToEqual() {
            var stopWatch = new Stopwatch();
            stopWatch.Start();         //              14               10
            var canSwap = CanMakeSumsEqual(new int[]{3,5,6},new int[]{1,3,6},9);
            stopWatch.Stop();
            Console.WriteLine($"Elapsed {stopWatch.ElapsedMilliseconds} milliseconds.");
            AssertIsTrue(canSwap);
        }

        public static void TestItemCounts()
        {
            var stopWatch = new Stopwatch();
            stopWatch.Start();
            var cnts = CountItems(new int[]{1,4,5,2,2,7,7},9);
            stopWatch.Stop();
            Console.WriteLine($"Elapsed {stopWatch.ElapsedMilliseconds} milliseconds.");
            AssertAreEqual(new List<int>{0,1,2,0,1,1,0,2,0,0},cnts);
        }

        static void TestMinSum()
        {
            var stopWatch = new Stopwatch();
            var A = LinearSequence(11); 
            var B = LinearSequence(11);
            B[B.Length-1]=0;
            stopWatch.Start();
            "Low Sum:".Dump();
            //var minPath =  BuildTree(A,B);
            var minPath = MinSum(A,B);
            minPath.Dump<int>();
            minPath.Sum().Dump();
            stopWatch.Stop();
            Console.WriteLine($"Elapsed {stopWatch.ElapsedMilliseconds} milliseconds.");
        }

        static void TestAsserts() {
            AssertAreEqual(6,6);
            AssertAreEqual("Ted","Ted");
            AssertAreEqual(new int[] {1,2,3}, new List<int> {1,2,3});
            "Done!".Dump();
        }   

        static void TestPermsOfR() {
            var stopWatch = new Stopwatch();
            stopWatch.Start();
            AssertAreEqual(720,nPr(6,6));
            AssertAreEqual(720,nPr(6,5));
            AssertAreEqual(360,nPr(6,4));
            AssertAreEqual(120,nPr(6,3));
            AssertAreEqual(30,nPr(6,2));
            AssertAreEqual(6,nPr(6,1));
            AssertAreEqual(1,nPr(6,0));
            AssertAreEqual(1,nPr(0,0));
            stopWatch.Stop();
            Console.WriteLine($"Elapsed {stopWatch.ElapsedMilliseconds} milliseconds.");
        }

        static void TestNChooseR() {
            var stopWatch = new Stopwatch();
            stopWatch.Start();
            AssertAreEqual(1,nCr(6,0));
            AssertAreEqual(6,nCr(6,1));
            AssertAreEqual(15,nCr(6,2));
            AssertAreEqual(20,nCr(6,3));
            AssertAreEqual(15,nCr(6,4));
            AssertAreEqual(6,nCr(6,5));
            AssertAreEqual(1,nCr(6,6));
            AssertAreEqual(1,nCr(0,0));
            AssertAreEqual(6.93,Round(nCr(1600,60)/1e109,2));
            stopWatch.Stop();
            Console.WriteLine($"Elapsed {stopWatch.ElapsedMilliseconds} milliseconds.");
        }

        static void MissingItems() {
                var s = new SortedSet<int>(); // forcing dll load to make output nicer later.
                "Test functions...".Dump();
                var stopWatch = new Stopwatch();
                var A = LinearSequence(1000000);
                A.Shuffle();
                if (A.Length<55)
                    A.Dump();
                stopWatch.Start();
                var answer = FindPositiveMissingSequenceItem(A);
                stopWatch.Stop();
                Console.WriteLine($"Elapsed {stopWatch.ElapsedMilliseconds} milliseconds.");
                "Missing Item:".Dump();
                Console.WriteLine(answer);
                /* 
                Console.WriteLine($"Elapsed {stopWatch.ElapsedMilliseconds} milliseconds.");
                stopWatch.Reset();
                stopWatch.Start();
                FindMissingSequenceItemNoDupes(A).Dump();
                stopWatch.Stop();
                Console.WriteLine($"Elapsed {stopWatch.ElapsedMilliseconds} milliseconds.");
                stopWatch.Reset();
                A = new int[] {5,10};
                stopWatch.Start();
                FindAllMissingSequenceItem(A).Dump();
                stopWatch.Stop();
                Console.WriteLine($"Elapsed {stopWatch.ElapsedMilliseconds} milliseconds.");
                */
        }
    }
}


