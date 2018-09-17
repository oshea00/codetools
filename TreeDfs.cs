using System;
using System.Collections.Generic;

public class TreeDfs<T> {
    Tree<T> tree;
    List<T> visits;
    Action<Tree<T>> visitor;
    public TreeDfs(Tree<T> tree, Action<Tree<T>> visitor) {
        this.tree = tree;
        this.visitor = visitor;
        visits = new List<T>();
        this.visitor = visitor;
    }
    
    public List<T> TraceVisits => visits;

    public void visit(Tree<T> t) {
        visits.Add(t.Value);
        visitor(t);
    }

    void traverse(Tree<T> t) {
        if (t==null)
            return;
        visit(t);
        traverse(t.Left);
        traverse(t.Right);
    }

    public void Traverse() {
        traverse(tree);
    }

}
