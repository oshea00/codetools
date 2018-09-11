using System.Collections.Generic;

public class ConnectedVertices<T>
{
    Graph<T> g;
    int count;

    public ConnectedVertices(Graph<T> g, T v)
    {
        this.g = g;
        Search(g,v);
    }

    private void Search(Graph<T> g, T v) {
        g.Mark(v);
        count++;
        foreach (var w in g.Adjacent(v)) {
            if (!g.IsMarked(w))
                Search(g,w);
        }
    }
    public int Count => count;

    public List<T> GetConnected() {
        return g.GetMarked();
    }
}