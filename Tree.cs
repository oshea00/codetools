public class Tree<T> {
    public int Level { get; set; }
    public T Value { get; set; }
    public Tree<T> Left { get; set; }
    public Tree<T> Right { get; set; }
    public Tree(int level, T value) {
        this.Level = level;
        this.Value = value;
    }
    public Tree(T value, Tree<T> left = null, Tree<T> right = null) {
        Value = value;
        Left = left;
        Right = right;
    }
}

