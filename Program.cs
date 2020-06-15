using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using static Functions;
using static Sequences;
using static System.Math;

interface ICovariant<out T> {
    // Allows more derived T to be a less derived T
    // i.e. we can assign a string reference to an object reference.
    // The left side type can be more general (up casting)
}
interface IContravariant<in T> {
    // Allows less derived T to be a more derived T
    // i.e. we can assign an object reference to a string reference.
    // The left side type can be more specific (down casting)
}

class Animal : ICovariant<Animal>, IContravariant<Animal> {
}

class Giraffe : Animal, ICovariant<Giraffe>, IContravariant<Giraffe> {
}

class Monkey : Animal, ICovariant<Monkey>, IContravariant<Monkey> {
}


class Program
{   

    static void Main(string[] args)
    {
        // TestGetNestedItem();
        // TestSumOfSubSets();
        // TestMaxSumPath();
        // TestNQueens();
        // TestTreeDfs();
        // TestTreeBfsChildren();
        // CovarianceExample();
        // TestEncoder();
        // TestLinked();
        // TestMovies();
        // TestIsBipartite();
        // TestHasCycle();
        // TestFindConnectedComponents();
        // TestBFSPaths();
        // TestPaths();
        // TestGetConnectedDFS();
        // TestGraph();
        // TestUnionFind();
        // TestHuffman();
        // TestTreeFromExpr();   
        // TestMergeSortIter();
        // TestMergeSort();
        // TestTreeTraversalThreaded();
        // TestThreadedInOrderSuccessor();
        // TreeTraversalStackIter();
        // TreeTraversalsRecurse();
        // TestCircularLists();
        // TestDeque();
        // RunTopologicalSort();
        // TestGcd();
        // TestGetPrimes();
        // TestPermutations();
        // TestMaxProduct3();
        // TestChooseMinMissing();
        // TestTraverseChoices();
        // TestMinAvg();
        // TestHowManyDivisible();   
        // TestFindMinImpact();
        // TestCountTransitions();
        RunMushroomChamp();
        // TestMinHeap();
        // TestPrefixSums();
        // TestUpdateCounters();            
        // TestWhen1ThruXFound();
        // TestCanSwapToEqual();
        // TestItemCounts();
        // TestMinSum();
        // TestAsserts();
        // TestPermsOfR();
        // TestNChooseR();
        // MissingItems();
        // TestRBacktrack();
        // TestPermPaths();
        // var d = new DefaultDictionary<string, List<int>>(() => new List<int>());

        // var list = d["NewList"];
        // Console.WriteLine(list.Count());

    }

    private static void TestGetNestedItem()
    {
        var a = new object[] {"1","2","3",new[]{"4","5"},"6",new[]{"7","8"},"9"};
        var t = getNestedItem(a,3,0,null);
        AssertAreEqual(t.Item2,"4");
        t = getNestedItem(a,2,0,null);
        AssertAreEqual(t.Item2,"3");
        t = getNestedItem(a,7,0,null);
        AssertAreEqual(t.Item2,"8");
    }

    private static void TestMaxSumPath()
    {
        // See my Gist: https://gist.githubusercontent.com/oshea00/7487f41c49aaf1fd133c92861c4fa352/raw/6efd660446d139895c7aac94b2504493e2c0a3e1/maxpathsums.py

        // 1. What are the sub-problems?
        //    - The largest path sums for each parent row's children
        //    -
        // 2. What are the problem variables/decisions - what changes?
        //    - current row level
        //
        // 3. Is there a recurrence relationship?
        //    largestSum = max(largestSums(rowlevel))
        //    
        //    - largestSums(level) = 
        //          foreach child of row[level] node
        //             node.sum = max(node.child1.sum , node.child2.sum) 
        //      largest sum is the largest sum of the children
        //      
        // 4. What are the base/smallest cases we can decide on?
        //    - parent with no children -> answer is parent
        //    - no nodes - answer is zero
        // 5. Memoization
        // 6. Analyses
        //
        //...                        0
        //     1                   3[3*]
        //                           3
        //     2            7[10*]         4[7*]
        //                    3            3
        //                    7            4
        //     3       2[12*]    4[14*,11]     6[13*]   
        //                3         3   3         3
        //                7         7   4         4
        //                2         4   4         6
        //     4   18[30*] 5[17,19*,16] 9[22,20,23*]  3[16*]
        //             3     3   3  3     3  3  3      3 
        //             7     7   7  4     4  4  7      4
        //             2     2   4  4     6  4  4      6
        //             8     5   5  5     9  9  9      3
        //            20    17  19 16    22 20 23     16
        //
        //       1. child adds largest of two parent path sums to itself
        //       2. on the last row - choose the largest sum.
        //       3. The path to the larget sum is the largest child sum
        //          
        // 75
        // 95 64
        // 17 47 82
        // 18 35 87 10
        // 20 04 82 47 65
        // 19 01 23 75 03 34
        // 88 02 77 73 07 63 67
        // 99 65 04 28 06 16 70 92
        // 41 41 26 56 83 40 80 70 33
        // 41 48 72 33 47 32 37 16 94 29
        // 53 71 44 65 25 43 91 52 97 51 14
        // 70 11 33 28 77 73 17 78 39 68 17 57
        // 91 71 52 38 17 14 91 43 58 50 27 29 48
        // 63 66 04 68 89 53 67 30 73 16 69 87 40 31
        // 04 62 98 27 23 09 70 98 73 93 38 53 60 04 23
        // Answer: 1074

    }

    public class SumOfSubSets {
        int M;
        int n;
        int[] w;
        int[] x;
        List<List<int>> results;

        public SumOfSubSets(int M, int[] w) {
            this.M = M;
            this.n = w.Length;
            x = new int[n+1];
            this.w = new int[n+1];
            for (int i=1;i<=n;i++)
                this.w[i]=w[i-1];
            results = new List<List<int>>();
        }

        void StoreResult(int k) {
            // stash result
            var r = new List<int>();
            for (int i=1;i<=k;i++) {
                if (x[i]==1)
                    r.Add(w[i]);
            }
            results.Add(r);
        }

        public List<List<int>> Find() {
            int sum = 0;
            foreach (int n in w)
                sum = sum + n;
            SumOfSubsetsR(0,1,sum);
            return results;
        }

        private void SumOfSubsetsR(int s, int k, int r) {
            x[k] = 1;
            if (s + w[k] == M) { // subset found
                StoreResult(k); // store solution
            } 
            else {
                if (k<n) {
                    if (s + w[k] + w[k + 1] <= M)
                        SumOfSubsetsR(s + w[k], k + 1, r - w[k]);
                }
            }

            if (k<n) {
                if (s + r - w[k] >= M && s + w[k+1] <= M) {
                    x[k] = 0;
                    SumOfSubsetsR(s, k + 1, r - w[k]);
                }
            }
        }
    }
    
    private static void TestSumOfSubSets()
    {
        // We pass weights in sorted
        $"Subsets of [5,10,12,13,15,18] Summing to 30:".Dump();
        var s = new SumOfSubSets(30,new int[] {5,10,12,13,15,18});
        foreach (var r in s.Find()) {
            r.ListToString().Dump();
        }
        
        $"Subsets of [5] Summing to 5:".Dump();
        s = new SumOfSubSets(5,new int[] {5});
        foreach (var r in s.Find()) {
            r.ListToString().Dump();
        }
        
        $"Subsets of [-5,0,1,6] Summing to 1:".Dump();
        s = new SumOfSubSets(1,new int[] {-5,0,1,6});
        foreach (var r in s.Find()) {
            r.ListToString().Dump();
        }

        // odd result on this one...
        $"Subsets of [-1,-1,-1,-1] Summing to -3:".Dump();
        s = new SumOfSubSets(-3,new int[] {-1,-1,-1,-1});
        foreach (var r in s.Find()) {
            r.ListToString().Dump();
        }

        $"Subsets of [1,1,1,1] Summing to 2:".Dump();
        s = new SumOfSubSets(2,new int[] {1,1,1,1});
        foreach (var r in s.Find()) {
            r.ListToString().Dump();
        }
    }

    private static void TestNQueens()
    {
        NQueens(6);  // general backtracking pattern
    }

    private static void TestTreeDfs()
    {
        var tree = 
            new Tree<int>(1,
                new Tree<int>(2,
                    new Tree<int>(3),
                    new Tree<int>(4)),
                new Tree<int>(5,
                    new Tree<int>(6),
                    new Tree<int>(7))
                );

        int evaluated=0;
        var traversal = new TreeDfs<int>(tree,(t)=>{
            evaluated++;
            $"Visiting {t.Level} {t.Value}".Dump();
            return true;
        });
        $"Depth First:".Dump();
        traversal.Traverse();
        $"Looked at {evaluated} vertices.".Dump();

    }

    private static void TestTreeBfsChildren()
    {
        var tree = 
            new Tree<int>(1,
                new Tree<int>(2,
                    new Tree<int>(4),
                    new Tree<int>(5)),
                new Tree<int>(3,
                    new Tree<int>(6),
                    new Tree<int>(7))
                );

        int evaluated=0;
        var traversal = new TreeBfsChildren<int>(tree,(t)=>{
            evaluated++;
            $"Visiting {t.Level} {t.Value}".Dump();
            return true;
        });
        $"Breadth First:".Dump();
        traversal.Traverse();
        $"Looked at {evaluated} vertices.".Dump();

    }

    public static async Task<bool> DoItAsync() {
        Thread.Sleep(5000);
        return true;
    }

    public static bool JustDoIt() {
        return true;
    }

    private static void CovarianceExample()
    {
        ICovariant<Animal> a = new Animal();
        ICovariant<Giraffe> g = new Giraffe();
        a = g; // Giraffes can be upcasted implicitly to Animals
        // Giraffes are Animals

        IContravariant<Animal> ca = new Animal();
        IContravariant<Giraffe> cg = new Giraffe();
        cg = ca; // Animals can be downcasted implictly to Giraffes  
                 // Animals can be Giraffes

        $"{("\"")}".Dump();
        $"I've {((true) ? "\"Made my Bed\"" : "")}".Dump();

        var act = new Func<Task<bool>>(async () => await DoItAsync());
        var result = act();
        JustDoIt().Dump();
        $"I've {((true) ? "\"Made my Bed\"" : "")}".Dump();
    }

    private static void TestEncoder()
    {
        var encoder = new PhoneEncoder(new List<string>{"cat","bat","dog","ca","food","go"});
        var words = encoder.Encode("228");
        words.ListToString().Dump();
    }

    private static void TestLinked()
    {
        var empty = new List<string>();
        var items = new List<string> {"A","B","C"};
        var emptylist = Linked.LList(empty);
        var itemslist = Linked.LList(items);

        AssertIsTrue(Linked.IsCyclic(emptylist)==false);
        var llist = Linked.LList(items);
        AssertIsTrue(Linked.IsCyclic(llist)==false);
        AssertAreEqual(0,Linked.Count(emptylist));
        AssertAreEqual(3,Linked.Count(itemslist));

        var a = new NodeLink<string>("A");
        var b = new NodeLink<string>("B");
        var c = new NodeLink<string>("C");
        a.Next = b;
        b.Next = c;
        c.Next = a;
        AssertIsTrue(Linked.IsCyclic(a));
        AssertAreEqual("C",Linked.CycleNode(a).Value);
        var top = new NodeLink<string>();
        AssertIsTrue(Linked.IsCyclic(top));
        AssertIsNull(Linked.CycleNode(top).Value);
        Linked.ToString(itemslist).Dump();
        AssertAreEqual("[A,B,C]",Linked.ToString(itemslist));
        Linked.ToString(emptylist).Dump();
        AssertAreEqual("[]",Linked.ToString(emptylist));
        Linked.ToString(new NodeLink<string>()).Dump();
        AssertAreEqual("[cyclic]",Linked.ToString(new NodeLink<string>()));
        var list = Linked.Reverse(itemslist);
        Linked.ToString(list).Dump();
        AssertAreEqual("[C,B,A]",Linked.ToString(list));
        var listr = Linked.Reverse(emptylist);
        Linked.ToString(listr).Dump();
        AssertAreEqual("[]",Linked.ToString(listr));

    }

    private static void TestMovies()
    {
        var db = new MovieDB();
        AssertAreEqual(119412,db.Count);
        $"IsBipartite {db.Graph.IsBipartite}".Dump();
        AssertIsTrue(db.Graph.IsBipartite);
        db.Graph.FindComponents();
        $"Vertices {db.Graph.VerticeCount}, Edges {db.Graph.EdgeCount}".Dump();
        $"Movie graph has {db.Graph.GetComponentIds().Count} components.".Dump();
        db.MovieWithMostActors().Dump();
        db.ActorWithMostMovies().Dump();
        File.WriteAllText("kevinbacon.txt",db.KevinBaconReport());
    }

    private static void TestIsBipartite()
    {
        var g = new Graph<int>(new List<int>
        {0,6, 6,7, 7,8, 0,5, 0,1, 0,2, 1,3, 6,4, 3,5, 
         5,4, 8,10, 9,10, 9,11, 10,12, 11,12});
         $"Is Bipartite = {g.IsBipartite}".Dump();
         AssertIsTrue(g.IsBipartite);
    }

    private static void TestHasCycle()
    {
        var g = new Graph<int>(new List<int>
        {0,5, 4,3, 0,1, 9,12, 6,4, 5,4, 0,2, 11,12, 9,10, 0,6,
         7,8, 9,11, 5,3});
         $"Has Cycle = {g.HasCycle}".Dump();
         AssertIsTrue(g.HasCycle);
    }

    private static void TestFindConnectedComponents()
    {
        var g = new Graph<int>(new List<int>
        {0,5, 4,3, 0,1, 9,12, 6,4, 5,4, 0,2, 11,12, 9,10, 0,6,
         7,8, 9,11, 5,3});
         g.ToString().Dump();
         g.FindComponents();
         var comps = g.GetComponentIds();
         $"Components {comps.Count} :".Dump();
         comps.ForEach((Action<int>)(c=>{
             Functions.ListToString<int>(g.GetVerticesInComponent((int)c)).Dump();
         }));
         AssertAreEqual(g.IsConnected(0,3),g.SameComponent(0,3));
    }

    private static void TestBFSPaths()
    {
        // Finds shortest path using BFS
        var g = new Graph<int>(new List<int>
        {0,5, 4,3, 0,1, 9,12, 6,4, 5,4, 0,2, 11,12, 9,10, 0,6,
         7,8, 9,11, 5,3});
        var paths = new BFSPaths<int>(g,0);
        AssertIsTrue(paths.HasPathTo(3));
        $"Shortest path 0 to 3".Dump();
        paths.PathTo(3).ListToString().Dump();
        AssertAreEqual("[0,5,3]",paths.PathTo(3).ListToString());
    }

    private static void TestPaths()
    {
        // Finds paths but path length is dependent
        // on edge order used to build graph - may not be
        // shortst path - for that see BFS.
        var g = new Graph<int>(new List<int>
        {0,5, 4,3, 0,1, 9,12, 6,4, 5,4, 0,2, 11,12, 9,10, 0,6,
         7,8, 9,11, 5,3});
        var paths = new Paths<int>(g,0);
        AssertIsTrue(paths.HasPathTo(3));
        $"A path 0 to 3".Dump();
        paths.PathTo(3).ListToString().Dump();
        AssertAreEqual("[0,5,4,3]",paths.PathTo(3).ListToString());

    }

    private static void TestGetConnectedDFS()
    {
        var g = new Graph<int>(new List<int>
        {0,5, 4,3, 0,1, 9,12, 6,4, 5,4, 0,2, 11,12, 9,10, 0,6,
         7,8, 9,11, 5,3});
         var s = new ConnectedVertices<int>(g,0);
         AssertAreEqual(7,s.Count);
         $"Vertices connected to 0".Dump();
         s.GetConnected().ListToString().Dump();
         AssertAreEqual("[0,5,4,3,1,6,2]",s.GetConnected().ListToString());
         AssertIsTrue(g.IsConnected(0,4));
         AssertIsTrue(g.IsConnected(0,3));
    }

    private static void TestUnionFind()
    {
        // 0 1 2 3 4 5 6 7 8 9 sites
        // 0 1 2 4 4 6 6 7 4 9 component ids
        //
        var pairs = new int[]{4,3, 3,8, 6,5, 9,4, 2,1, 5,0, 7,2, 6,1};
        var uf = new UnionFind(10);
        AssertAreEqual(10,uf.ComponentCount);
        AssertAreEqual(9,uf.FindComponent(9));
        for (int i=0;i<pairs.Length-1;i+=2) {
            uf.Union(pairs[i],pairs[i+1]);
            uf.ComponentIds.Dump();
        }
        AssertAreEqual(2,uf.ComponentCount);

    }

    private static void TestGraph()
    {
        //     C       F
        //     |       |
        // A - B - D - E
        //             |
        //             G
        var a = new Graph<string>(new List<string>{
            "A","B","B","C","B","D","D","E","E","F","E","G"});
        a.ToString().Dump();

        //     3       6
        //     |       |
        // 1 - 2 - 4 - 5
        //             |
        //             7
        var n = new Graph<int>(new List<int>() {1,2,2,3,2,4,4,5,5,6,5,7});
        n.ToString().Dump();

        // x + y + z
        var pTail = new PolyTerm(); 
        pTail.Link = new PolyTerm(1,100,new PolyTerm(1,10,new PolyTerm(1,1,pTail)));
        // x^2 - 2y - z
        var qTail = new PolyTerm();
        qTail.Link = new PolyTerm(1,200,new PolyTerm(-2,10,new PolyTerm(-1,1,qTail)));

        var p = new Graph<PolyTerm>(new List<PolyTerm> {pTail,qTail});
        p.ToString().Dump();
    }

    private static void TestHuffman()
    {
        Huffman(new int[] {0,1,4,9,16,25,36,49,64,81,100});
    }

    private static void TestTreeFromExpr()
    {
        var expr = "(A(B,C(K)),D(E(H),F(J),G))";
        var t = TreeFromExp(expr);

        $"Tree Expression: {expr}".Dump();
        // post-order for "trees" corr in-order for binary trees
        "In-Order binary = forest postorder):".Dump();
        t.TreeInOrderToString().Dump();  
        AssertAreEqual("[B,K,C,H,J,G,F,E,D,A,root]",t.TreeInOrderToString());   

        "Pre-Order (forests = binary):".Dump();
        t.TreePreOrderToString().Dump();
        AssertAreEqual("[root,A,B,C,K,D,E,H,F,J,G]",t.TreePreOrderToString());

        "Post-Order:".Dump();
        t.TreePostOrderToString().Dump();
        AssertAreEqual("[K,G,J,F,H,E,D,C,B,A,root]",t.TreePostOrderToString());

        $"Empty Expression {@""""""} ".Dump();
        t = TreeFromExp("");
        t.TreeInOrderToString().Dump();
        AssertAreEqual("[root]",t.TreeInOrderToString());

        $"Empty sub-tree () ".Dump();
        t = TreeFromExp("()");
        t.TreeInOrderToString().Dump();
        AssertAreEqual("[),root]",t.TreeInOrderToString());

        $"Empty nested sub-tree (()) ".Dump();
        t = TreeFromExp("(())");
        t.TreeInOrderToString().Dump();
        AssertAreEqual("[(,root]",t.TreeInOrderToString());

        $"Empty odd nested level sub-trees ((((())))) ".Dump();
        t = TreeFromExp("((((()))))");
        t.TreeInOrderToString().Dump();
        AssertAreEqual("[),(,(,root]",t.TreeInOrderToString());

        $"Empty insane odd nested level sub-trees ((((((())))))) ".Dump();
        t = TreeFromExp("((((((()))))))");
        t.TreeInOrderToString().Dump();
        AssertAreEqual("[),(,(,(,root]",t.TreeInOrderToString());

        $"Empty even nested sub-trees (((()))) ".Dump();
        t = TreeFromExp("(((())))");
        t.TreeInOrderToString().Dump();
        AssertAreEqual("[(,(,root]",t.TreeInOrderToString());

        $"Empty insane even nested sub-trees (((((((()))))))) ".Dump();
        t = TreeFromExp("(((((((())))))))");
        t.TreeInOrderToString().Dump();
        AssertAreEqual("[(,(,(,(,root]",t.TreeInOrderToString());

    }

    public static void ThreadedSuccessorInOrderHead(Tree<string> head, Action<string> visit) {
        Tree<string> p = head;
        Tree<string> q = null;
        while (true) {
            q = p.Left;
            while (p.LeftThread==false) {
                p = q;
                q = p.Left;
            }
            if (p==head)
                break;
            visit(p.Value);
            while (p.RightThread) {
                if (p==head)
                    break;
                visit(p.Value);
                p = p.Right;
            }
            q = p.Right;
            p = q;
        }
    }

    private static void TestMergeSortIter()
    {
        var s = new MergeSortIter<char>();
        var a = "MERGESORTEXAMPLE".ToCharArray();
        s.Sort(a);
        AssertAreEqual(new List<char>("AEEEEGLMMOPRRSTX".ToCharArray()),a);
        a.Dump();
    }

    private static void TestMergeSort()
    {
        var s = new MergeSort<char>();
        var a = "MERGESORTEXAMPLE".ToCharArray();
        s.Sort(a);
        AssertAreEqual(new List<char>("AEEEEGLMMOPRRSTX".ToCharArray()),a);
        a.Dump();
    }

    private static void TestTreeTraversalThreaded()
    {
        var a = new Tree<string>("A");
        var b = new Tree<string>("B");
        var c = new Tree<string>("C");
        var root = a;

        // A links
        a.Left = b;
        a.LeftThread = false;
        a.Right = c;
        a.RightThread = false;
        // B links
        b.Left = c;
        b.LeftThread = true;
        b.Right = a;
        b.RightThread = true;
        // C links
        c.Left = a;
        c.LeftThread = true;
        c.Right = b;
        c.RightThread = true;

        var inOrder = new Func<Tree<string>,List<string>>(t=>{
            var visits = new List<string>();
            var q = t;
            while (true) {
                q = q.ThreadedSuccessorInOrder();
                visits.Add(q.Value);      
                if (q==t)
                    break;
            }
            return visits;
        });
        var result = inOrder(a);
        $"In-Order Threaded:".Dump();
        AssertAreEqual(new string[] {"C","B","A"},result);
        result.Dump();
        result = inOrder(b);
        AssertAreEqual(new string[] {"A","C","B"},result);
        result.Dump();
        result = inOrder(c);
        AssertAreEqual(new string[] {"B","A","C"},result);
        result.Dump();
    }

    private static void TestThreadedInOrderSuccessor()
    {   
        //       A        $ = InOrder
        //     /   \
        //    B     C     B -> A -> C -> B  P$
        //                B -> C -> A -> B  $P
        //
        // in order    A l-> B -r-> A r-> C -l-> Head    l-> == hard left  r-> == hard right
        //             C -l-> A l-> B -l-> C              -l-> == soft left -r-> == soft right
        var a = new Tree<string>("A");
        var b = new Tree<string>("B");
        var c = new Tree<string>("C");
        var root = a;

        // A links
        a.Left = b;
        a.LeftThread = false;
        a.Right = c;
        a.RightThread = false;
        // B links
        b.Left = c;
        b.LeftThread = true;
        b.Right = a;
        b.RightThread = true;
        // C links
        c.Left = a;
        c.LeftThread = true;
        c.Right = b;
        c.RightThread = true;

        AssertAreEqual("A",b.ThreadedSuccessorInOrder().Value);
        AssertAreEqual("C",a.ThreadedSuccessorInOrder().Value);
        AssertAreEqual("B",c.ThreadedSuccessorInOrder().Value);
        AssertAreEqual("C",b.ThreadedPredeccessorInOrder().Value);
        AssertAreEqual("B",a.ThreadedPredeccessorInOrder().Value);
        AssertAreEqual("A",c.ThreadedPredeccessorInOrder().Value);
        AssertAreEqual("A",a.ThreadedSuccessorInOrder().ThreadedPredeccessorInOrder().Value);
        AssertAreEqual("B",b.ThreadedSuccessorInOrder().ThreadedPredeccessorInOrder().Value);
        AssertAreEqual("C",c.ThreadedSuccessorInOrder().ThreadedPredeccessorInOrder().Value);
        AssertAreEqual("A",a.ThreadedPredeccessorInOrder().ThreadedSuccessorInOrder().Value);
        AssertAreEqual("B",b.ThreadedPredeccessorInOrder().ThreadedSuccessorInOrder().Value);
        AssertAreEqual("C",c.ThreadedPredeccessorInOrder().ThreadedSuccessorInOrder().Value);
    }

    private static void TreeTraversalStackIter()
    {
        var tree = 
        new Tree<string>("A",
            new Tree<string>("B",                                                                                      
                new Tree<string>("D")),
            new Tree<string>("C",
                new Tree<string>("E",
                    null,
                    new Tree<string>("G")),
                new Tree<string>("F",
                    new Tree<string>("H"),
                    new Tree<string>("J"))));

        var visits = new List<string>();

        // In-Order  l v r  "Visit Then Right"
        var visit = new Func<Tree<string>,string>(t=>t.Value);
        var s = new Stack<Tree<string>>();
        var p = tree;
        while (true) {
            if (p == null)
            {
                if (s.Count==0) {
                    break;
                }
                else {
                    p = s.Pop();
                    visits.Add(visit(p)); // Visit Then Right
                    p = p.Right;
                }
            }
            else {
                s.Push(p);
                p = p.Left;
            }
        }
        "Iter In-Order:".Dump();
        visits.Dump();
        visits.Clear();

        // Pre-order  v l r "Visit Then Left"
        p = tree;
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
                visits.Add(visit(p)); // Visit Then Left 
                p = p.Left;
            }
        }
        "Iter Pre-Order:".Dump();
        visits.Dump();
        visits.Clear();

        // post-order  l r v "Visit If Right Visited"
        p = tree;
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
                        visits.Add(visit(p));
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
        "Iter Post-Order:".Dump();
        visits.Dump();
    }

    private static void TreeTraversalsRecurse()
    {
        var tree = 
        new Tree<string>("A",
            new Tree<string>("B"),
            new Tree<string>("C"));

        var visits = new List<string>();

        var visit = new Func<Tree<string>,string>(t=>t.Value);

        Action<Tree<string>> inOrder = null;
        inOrder = new Action<Tree<string>>(t=>{
            if (t==null) return;
            inOrder(t.Left);   
            visits.Add(visit(t));
            inOrder(t.Right);
        });
        inOrder(tree);
        "In-Order:".Dump();
        visits.Dump();
        visits.Clear();

        Action<Tree<string>> preOrder = null;
        preOrder = new Action<Tree<string>>(t=>{
            if (t==null) return;
            visits.Add(visit(t));
            preOrder(t.Left);
            preOrder(t.Right);
        });
        preOrder(tree);
        "Pre-Order:".Dump();
        visits.Dump();
        visits.Clear();

        Action<Tree<string>> postOrder = null;
        postOrder = new Action<Tree<string>>(t=>{
            if (t==null) return;
            postOrder(t.Left);
            postOrder(t.Right);
            visits.Add(visit(t)); 
        });
        postOrder(tree);
        "Post-Order:".Dump();
        visits.Dump();

    }

    private static void TestCircularLists()
    {
        // x + y + z
        var pTail = new PolyTerm(); 
        pTail.Link = new PolyTerm(1,100,new PolyTerm(1,10,new PolyTerm(1,1,pTail)));
        // x^2 - 2y - z
        var qTail = new PolyTerm();
        qTail.Link = new PolyTerm(1,200,new PolyTerm(-2,10,new PolyTerm(-1,1,qTail)));

        // Add Polynomials.
        $"({pTail}) + ({qTail}) = ".Dump();

        var p = pTail.Link;
        var q = qTail.Link;
        var q1 = q;  

        while (true) {
            if (p.Exp < q.Exp) {
                q1 = q;
                q = q.Link;  // next q term
                continue;
            }
            if (p.Exp == q.Exp) {
                if (p.IsTail)  // we are at the end so quit
                    break;
                // Add Coefficients of like terms 
                q.Coef = q.Coef + p.Coef;
                if (q.Coef != 0) {
                    // Get next p and q terms
                    p = p.Link;
                    q1 = q;
                    q = q.Link;
                    continue; 
                }
                // Delete zeroed term from q
                q = q.Link;
                q1.Link = q;
            } else {
            //  Insert new term from p into q
                q1.Link = new PolyTerm(p.Coef, p.Exp, q);
                q1 = q1.Link;
            }
            p = p.Link;   // next p term           
        }
        // Algo should return to original p and q positions
        AssertAreEqual(p,pTail); 
        AssertAreEqual(q,qTail);

        // Answer will be Q list - print terms
        $"({q})".Dump();

    }

    private static void TestDeque()
    {
        var d = new Deque<int>();
        d.Enqueue(4);
        d.Enqueue(5);
        AssertAreEqual(4,d.Dequeue());
        AssertAreEqual(false,d.IsEmpty);
        AssertAreEqual(1,d.Count);
        d.Enqueue(4);
        AssertAreEqual(4,d.Pop());
        d.Push(4);
        AssertAreEqual(4,d.Dequeue());
        AssertAreEqual(5,d.Dequeue());
        AssertAreEqual(0,d.Count);
        d.Push(5);
        AssertAreEqual(5,d.Pop());
        AssertAreEqual(0,d.Count);
    }

    private static void RunTopologicalSort()
    {
        var d = new Dictionary<string,Job>();
        var deps = new string[] {
            "9<2","3<7","7<5","5<8","8<6",
            "4<6","1<3","7<4","9<5","2<8",
        };

        // Load job dependencies
        foreach (var dep in deps) {
            var toks = dep.Split('<');
            var anode = new Job { Name = toks[1], Dependencies = new List<string>() };
            var bnode = new Job { Name = toks[0], Dependencies = new List<string>() };
            if (!d.ContainsKey(anode.Name)) {
                d.Add(anode.Name,anode);
            }
            if (!d.ContainsKey(bnode.Name)) {
                d.Add(bnode.Name,bnode);
            }
            d[anode.Name].Dependencies.Add(bnode.Name);
        }

        // Run jobs
        var isRunnable = new Func<string,bool>(job=>
                d[job].Done == false &&
            (d[job].Dependencies.Count == 0 || d[job].Dependencies.All(j=>d[j].Done))
        );

        var jobRuns = new List<string>();
        while (true) {
            var runnable = d.Keys.Where(job=>isRunnable(job)).FirstOrDefault();
            if (runnable == null)
                break;
            // run job
            d[runnable].Done = true;
            jobRuns.Add(runnable);
        }

        // Jobs are run as soon as all their dependencies (if any)
        // are satisfied.
        $"Jobs run in order:".Dump();
        jobRuns.Dump();
        $"Jobs not run due to cyclic dependency:".Dump();
        foreach (var job in d.Keys.Where(job=>d[job].Done==false)) {
            d[job].Name.Dump();
        }
    }

    private static void TestGcd()
    {
        AssertAreEqual(1,Gcd(5,7));
        AssertAreEqual(1,Gcd(7,5));
        AssertAreEqual(7,Gcd(35,14));
        AssertAreEqual(14,Gcd(0,14));
        AssertAreEqual(1,Gcd(15,14));
    }

    private static void TestGetPrimes()
    {
        var primes = GetPrimes().GetEnumerator();

        for (int i=0;i<10;i++) {
            primes.MoveNext();
            primes.Current.Dump();
        }
        //PrintPrimes(500);
    }

    private static void TestPermutations()
    {
        Permutations("CATS".ToArray(),p=>{
            p.Dump();
        });
    }

    private static void TestMaxProduct3()
    {
        AssertAreEqual(125,MaxProduct3(new int[] {-5, 5, -5, 4}));
        AssertAreEqual(60,MaxProduct3(new int[] {-3, 1, 2, -2, 5, 6}));
        AssertAreEqual(105,MaxProduct3(new int[] {-5, -3, 1, 2, 3, 4, 7}));
    }

    private static void TestChooseMinMissing()
    {
        var A = new int[] {1,6,2,3};
        var B = new int[] {1,2,-9,5};

        var minMissing = int.MaxValue;
        int[] minSequence = new int[A.Length];

        TraverseChoices(A,B,(C)=>{
            var missing = FindPositiveMissingSequenceItem(C);
            if (missing < minMissing) {
                minMissing = missing;
                C.CopyTo(minSequence);
            }
            if (missing  == 1 )
                return false;
            else
                return true;
        });

        minSequence.Dump();
        minMissing.Dump();
    }

    private static void TestTraverseChoices()
    {
        var A = new int[] {1,2};
        var B = new int[] {5,6};
        int i=0;

        TraverseChoices(A,B,(C)=>{
            C.Dump();
            i++;
            return true;
        });

        AssertAreEqual(Pow(2,A.Length),i);

        i=0;
        TraverseChoices(A,B,(C)=>{
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
        sums.Dump();
        int N = A.Length;
        double minAvg = double.MaxValue;
        int minPos = N-1;
        DumpSlice(A,0,N-1,0);
        for (int s=2; s<=3; s++)
        {
            Console.Write($"{s}  ");
            for (int i=0; i<N-(s-1); i++) {
                var x=i;
                var y=i+s-1;
                var avg = ((double) sums[y+1]-sums[x])/s;  
                Console.Write($"{avg.ToString("0.0")} ");
                if (avg<minAvg)
                {
                    minAvg = avg;
                    minPos = i;
                }
            }
            Console.Write($" [{minAvg.ToString("0.0")}]");
            Console.WriteLine($"");
        }
    }

    static int MinAvgTable(int[] A)
    {
        AvgTable(A);
        double minAvg = double.MaxValue;
        var sums = PrefixSums(A);
        int N = A.Length;
        int minPos = N-1;
        for (int s=2; s<=3; s++) //Guessing that min is always slice 2 or 3
//            for (int s=2; s<=3; s++)
        {
            for (int i=0; i<N-(s-1); i++) {
                var x=i;
                var y=i+s-1;
                var avg = ((double) sums[y+1]-sums[x])/s;
                if (avg<minAvg)
                {
                    minAvg = avg;
                    minPos = i;
                }
            }
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
        a.Dump();
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
        AssertAreEqual(MushroomChamp(trail:A, maxmoves:6, startPos:4), 25);
        AssertAreEqual(MushroomChamp(trail:A, maxmoves:15, startPos:4), 30);
        AssertAreEqual(MushroomChamp(trail:A, maxmoves:1, startPos:4), 6);
        AssertAreEqual(MushroomChamp(trail:A, maxmoves:4, startPos:0), 18);
        AssertAreEqual(MushroomChamp(trail:A, maxmoves:3, startPos:6), 18);
        AssertAreEqual(MushroomChamp(trail: new[]{2}, maxmoves:1, startPos: 0), 2);
        AssertAreEqual(MushroomChamp(trail: new[]{2}, maxmoves:1, startPos:-1), 2);
        AssertAreEqual(MushroomChamp(trail: new[]{2}, maxmoves:0, startPos: 7), 0);
        AssertAreEqual(MushroomChamp(trail: new[]{2}, maxmoves:0, startPos: 0), 0);
        AssertAreEqual(MushroomChamp(trail: new[]{2}, maxmoves:1, startPos: 1), 2);
        AssertAreEqual(MushroomChamp(trail: new int[0], maxmoves:1, startPos:0), 0);
    }

    static long MushroomChamp(int[] trail, int maxmoves, int startPos) {
        trail.Dump();
        $"Starting pos = {startPos}, Max moves = {maxmoves}".Dump();
        var sums = PrefixSums(trail);
        int n = trail.Length;
        int altSideSteps = ((maxmoves + 1) / 2) - 1;
        int maxLeftPos = 0, maxRightPos = 0;
        long maxSum = 0;

        while (altSideSteps >= 0 && maxmoves > 0) {
            int currLeft = 0, currRight = 0;
            for (int sides = 0; sides < 2; sides++) {
                if (sides % 2 == 0) {
                    currLeft = Max(startPos - altSideSteps, 0);
                    currRight = Min(startPos + (maxmoves - altSideSteps * 2), n - 1);
                } else {
                    currLeft = Max(startPos - (maxmoves - altSideSteps * 2), 0);
                    currRight = Min(startPos + altSideSteps, n - 1);
                }
                var sum = sums[currRight + 1] - sums[currLeft];
                if (sum > maxSum) {
                    maxLeftPos = currLeft;
                    maxRightPos = currRight;
                    maxSum = sum;
                }
            }
            altSideSteps -= 1;
        }
        
        $"Best picking range[{maxLeftPos}..{maxRightPos}] yields {maxSum}".Dump();
        return maxSum;
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
            h.Items.Dump();
            h.Pop().Dump();
        }

        var n = new MinHeap<int>(4);
        n.Push(100);
        n.Push(2);
        n.Push(11);
        n.Push(9);

        while (!n.IsEmpty)
        {
            n.Items.Dump();
            n.Pop().Dump();
        }
    }

    private static void TestPrefixSums() {
        var A = LinearSequence(10);
        A.Shuffle();
        A.Dump();
        var sums = PrefixSums(A);
        sums.Dump();
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
        seq.Dump();
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
        minPath.Dump();
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
            var A = new int[] {1,2,-9,5};
            A.Dump();
            var stopWatch = new Stopwatch();
            stopWatch.Start();
            var answer = FindPositiveMissingSequenceItem(A);
            stopWatch.Stop();
            Console.WriteLine($"Elapsed {stopWatch.ElapsedMilliseconds} milliseconds.");
            "Missing Item:".Dump();
            Console.WriteLine(answer);
            AssertAreEqual(2,FindSinglePositiveMissingSequenceItem(new int[]{1,3}));
    }

    // Interesting to try one of Knuth's examples as-is, gotos, all caps vars, etc...
    public static void PrintPrimes(int numPrimes) {
        //p1:
        var PRIME = new int[numPrimes+1];
        PRIME[1] = 2;
        var N = 3;  // next odd number candidate
        var J = 1;  // # primes found 
        p2: // N Is Prime
        J += 1;
        PRIME[J] = N;
        //p3:
        if (J==numPrimes)
            goto p9;
        p4: // Advance
        N += 2;
        //p5:
        var K = 2;
        p6:
        var Q = N / PRIME[K];
        var R = N % PRIME[K];
        if (R==0)
            goto p4;
        //p7:
        if (Q <= PRIME[K])
            goto p2;
        //p8:
        K += 1;
        goto p6;
        p9:
        $"FIRST {numPrimes} PRIMES".Dump();
        for (int j=0;j<(numPrimes/10);j++) {
            for (int i=1;i<numPrimes;i+=(numPrimes/10)) {
                Console.Write($"{PRIME[i+j].ToString("0000")} ");
            }
            "".Dump();
        }
    }

    // Another Knuth example to play with...
    public static Tuple<int,int> FindMax(int[] A) {
        //m1:
        var n = A.Length-1;
        var j = n; // max position
        var k = n - 1;
        var m = A[n]; // current max
        m2:
        if (k==0) {
            return new Tuple<int,int>(m,j);
        }
        //m3:
        if (A[k]<=m) {
            goto m5;
        }
        //m4:
        j = k;
        m = A[k];
        m5:
        k-=1;
        goto m2;
    }
}


