using System.Collections.Generic;

internal class Paths<T>
{
    Graph<T> g;
    T s;
    Dictionary<T,T> edgeTo = new Dictionary<T,T>();

    public Paths(Graph<T> g, T s)
    {
        this.g = g;
        this.s = s;
        Search(g,s);
    }

    private void Search(Graph<T> g, T v) {
        g.Mark(v);
        foreach (var w in g.Adjacent(v)) {
            if (!g.IsMarked(w)) {
                edgeTo[w] = v;
                Search(g,w);
            }
        }
    }

    public bool HasPathTo(T w) {
        return g.IsMarked(w);
    }

    public List<T> PathTo(T w) {
        var path = new List<T>();
        if (HasPathTo(w)) {
            for (T x=w; !x.Equals(s); x=edgeTo[x]) {
                path.Add(x);
            }
            path.Add(s);
            path.Reverse();
        }
        return path;
    }
}