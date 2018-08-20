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
            ArrayItemSwaps();
            //TestAsserts();
            //TestPermsOfR();
            //TestNChooseR();
            //MissingItems();
        }

        static void ArrayItemSwaps()
        {
            var stopWatch = new Stopwatch();
            var A = LinearSequence(19); 
            var B = LinearSequence(19);
            B[B.Length-1]=0;
            stopWatch.Start();
            "Low Sum:".Dump();
            var minPath =  BuildTree(A,B);
            minPath.Dump<int>();
            minPath.Sum().Dump();
            stopWatch.Stop();
            Console.WriteLine($"Elapsed {stopWatch.ElapsedMilliseconds} milliseconds.");
        }

        class Tree<T> {
            public int Level { get; set; }
            public T Value { get; set; }
            public Tree<T> Left { get; set; }
            public Tree<T> Right { get; set; }
            public Tree(int level, T value) {
                this.Level = level;
                this.Value = value;
            }
        }

        static int[] BuildTree(int[] A, int[] B) 
        {
            var stack = new Stack<Tree<int>>((int)Math.Pow(2,A.Length));
            int level = 0;
            Tree<int> t = new Tree<int>(-1,-1);
            stack.Push(t);
            int[] path = new int[A.Length];
            long minSum=long.MaxValue;
            int[] minPath = new int[A.Length];
            while (stack.Count>0)
            {
                var top = stack.Pop();
                if (top.Level > -1)
                {
                    path[top.Level]=top.Value;
                }
                level = top.Level + 1;
                if (level<A.Length)
                {
                    top.Left = new Tree<int>(level,A[level]);
                    top.Right = new Tree<int>(level,B[level]);
                    stack.Push(top.Left);
                    stack.Push(top.Right);
                }
                else
                {
                    long currSum = path.Sum();
                    if (currSum < minSum)
                    {
                        minSum = currSum;
                        for (int i=0;i<path.Length;i++)
                            minPath[i]=path[i];
                    }
                }
            }
            return minPath;
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


