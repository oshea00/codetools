public class Tree<T> {
    public int Level { get; set; }
    public T Value { get; set; }
    public Tree<T> Left { get; set; }
    public Tree<T> Right { get; set; }
    public bool LeftSoft { get; set; }
    public bool RightSoft { get; set; }

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
        if (this.RightSoft == true)
            return q;
        while (q.LeftSoft == false)
            q = q.Left;
        return q;
    }

    public Tree<T> ThreadedPredeccessorInOrder() {
        Tree<T> q = this.Left;
        if (this.LeftSoft)
            return q;
        while (q.RightSoft == false)
            q = q.Right;
        return q;
    }
}

