using System;
using System.Collections.Generic;

public class TreeBfsChildren<T> {
    Tree<T> tree;
    List<T> visits;
    Action<Tree<T>> visitor;

    public TreeBfsChildren(Tree<T> tree, Action<Tree<T>> visitor) {
        this.tree = tree;
        this.visitor = visitor;
        visits = new List<T>();
    }
    
    public List<T> TraceVisits => visits;

    void visit(Tree<T> t) {
        visits.Add(t.Value);
        visitor(t);
    }

    void visitChildren(Tree<T> t) {
        if (t.Left != null)
            visit(t.Left);
        if (t.Right != null)
            visit(t.Right);
    }

    void traverse(Tree<T> t) {
        if (t==null)
            return;
        visitChildren(t);
        traverse(t.Left);
        traverse(t.Right);
    }

    public void Traverse() {
        visit(tree);
        traverse(tree);
    }

}
