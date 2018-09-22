using System;
using System.Collections.Generic;

public class TreeBfsChildren<T> {
    Tree<T> tree;
    Queue<Tree<T>> q;
    Func<Tree<T>,bool> visitor;

    public TreeBfsChildren(Tree<T> tree, Func<Tree<T>,bool> visitor) {
        this.tree = tree;
        this.visitor = visitor;
        q = new Queue<Tree<T>>();
    }
    
    bool visit(Tree<T> t) {
        return visitor(t);
    }

    IEnumerable<Tree<T>> Children(Tree<T> t) {
        if (t.Left != null)
            yield return t.Left;
        if (t.Right != null)
            yield return t.Right;
        yield break; 
    }

    void traverse(Tree<T> t) {
        var level = 0;
        t.Level = level;
        q.Enqueue(t);
        while (q.Count > 0) {
            var p = q.Dequeue();
            level = p.Level;
            if (!visit(p))
                break;
            level++;
            foreach (var c in Children(p)) {
                c.Level = level;
                q.Enqueue(c);
            }
        }
    }

    public void Traverse() {
        traverse(tree);
    }

}
