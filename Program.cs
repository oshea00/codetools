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
            TestMinHeap();
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


