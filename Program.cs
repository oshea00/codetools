using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using static Functions;
using static Sequences;
using static System.Math;

class Program
{   
    static void Main(string[] args)
    {
        TestMovies();
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
        // RunMushroomChamp();
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
         g.FindComponents();
         var comps = g.GetComponentIds();
         $"Components {comps.Count} :".Dump();
         comps.ForEach(c=>{
             g.GetVerticesInComponent(c).ListToString().Dump();
         });
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
            uf.Dump();
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
        a.Dump<char>();
    }

    private static void TestMergeSort()
    {
        var s = new MergeSort<char>();
        var a = "MERGESORTEXAMPLE".ToCharArray();
        s.Sort(a);
        AssertAreEqual(new List<char>("AEEEEGLMMOPRRSTX".ToCharArray()),a);
        a.Dump<char>();
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
        result.Dump<string>();
        result = inOrder(b);
        AssertAreEqual(new string[] {"A","C","B"},result);
        result.Dump<string>();
        result = inOrder(c);
        AssertAreEqual(new string[] {"B","A","C"},result);
        result.Dump<string>();
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
        visits.Dump<string>();
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
        visits.Dump<string>();
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
        visits.Dump<string>();
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
        visits.Dump<string>();
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
        visits.Dump<string>();
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
        visits.Dump<string>();

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
        jobRuns.Dump<string>();
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
            p.Dump<char>();
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

        minSequence.Dump<int>();
        minMissing.Dump();
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
        sums.Dump<long>();
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
            var A = new int[] {1,2,-9,5};
            A.Dump<int>();
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


