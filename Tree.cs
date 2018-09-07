public class Tree<T> {
    public int Level { get; set; }
    public T Value { get; set; }
    public Tree<T> Left { get; set; }
    public Tree<T> Right { get; set; }
    public bool LeftThread { get; set; }
    public bool RightThread { get; set; }

    public Tree(int level, T value) {
        this.Level = level;
        this.Value = value;
    }

    public Tree(T value, Tree<T> left = null, Tree<T> right = null) {
        Value = value;
        Left = left;
        Right = right;
    }
    
    public Tree<T> ThreadedSuccessorInOrder() {
        Tree<T> q = this.Right;
        if (this.RightThread == true)
            return q;
        while (q.LeftThread == false)
            q = q.Left;
        return q;
    }

    public Tree<T> ThreadedPredeccessorInOrder() {
        Tree<T> q = this.Left;
        if (this.LeftThread)
            return q;
        while (q.RightThread == false)
            q = q.Right;
        return q;
    }
}

