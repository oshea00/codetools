using System;
using System.Collections.Generic;

public class TreeDfs<T> {
    Tree<T> tree;
    Stack<Tree<T>> s;
    Func<Tree<T>,bool> visitor;

    public TreeDfs(Tree<T> tree, Func<Tree<T>,bool> visitor) {
        this.tree = tree;
        this.visitor = visitor;
        s = new Stack<Tree<T>>();
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
        s.Push(t);
        while (s.Count > 0) {
            var p = s.Pop();
            level = p.Level;
            if (!visit(p))
                break;
            level++;
            foreach (var c in Children(p)) {
                c.Level = level;
                s.Push(c);
            }
        }
    }

    public void Traverse() {
        traverse(tree);
    }

}
