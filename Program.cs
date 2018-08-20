using System;
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
            TestAsserts();
            TestPermsOfR();
            TestNChooseR();
            MissingItems();
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

        static int[] LowestSum(int[] A, int[] B) 
        {
            long sumA = A.Sum();
            long sumB = B.Sum();
            long minSum = long.MaxValue;
            var combo = new int[]{};

            if (sumA < sumB) {
                minSum = sumA;
                combo = A;
            }
            else {
                minSum = sumB;
                combo = B;
            }

            for (int i=0;i<A.Length;i++)
            {
                for (int s=0;s<2;s++)
                {
                    int k=0;
                    var c = new int[A.Length];
                    for (int j=0;j<B.Length;j++)
                    {
                        if (s==0){
                            if (i==j)
                                c[k]=A[j];
                            else
                                c[k]=B[j];
                        }
                        else {
                            if (i==j)
                                c[k]=B[j];
                            else
                                c[k]=A[j];

                        }
                        k++;
                    }
                    var sumC = c.Sum();
                    if (sumC < minSum)
                    {
                        minSum = sumC;
                        combo = c;
                    }
                    c.Dump();
                }
            }
            return combo;
        }

        static void ArrayItemSwaps()
        {
            var stopWatch = new Stopwatch();
            var A = new int[] {5,2,3}; 
            var B = new int[] {4,3,1};
            stopWatch.Start();
            LowestSum(A,B).Dump();
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
                "Missing Item:".Dump();
                stopWatch.Start();
                FindMissingSequenceItem(A).Dump();
                stopWatch.Stop();
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
        }
    }
}


