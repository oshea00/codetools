using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

public class Graph<T> {
    Dictionary<T,List<T>> vertices;

    public Graph()
    {
        vertices = new Dictionary<T, List<T>>();
    }

    public Graph(List<T> edges) : this() {
        if (edges.Count%2!=0)
            throw new Exception("Edge list must be an even count.");
        for (int i=0;i<edges.Count-1;i+=2) {
            if (!vertices.ContainsKey(edges[i])) {
                vertices.Add(edges[i],new List<T>());
            }
            if (!vertices.ContainsKey(edges[i+1])) {
                vertices.Add(edges[i+1],new List<T>());
            }
            vertices[edges[i]].Add(edges[i+1]);
            vertices[edges[i+1]].Add(edges[i]);
        } 
    }

    public int VerticeCount => vertices.Count;

    public int EdgeCount => vertices.SelectMany(v=>v.Value).Count() / 2;

    public List<T> Adjacent(T w) {
        var adj = new List<T>();
        foreach (var v in vertices.Keys) {
            if (vertices[v].Contains(w)) 
                adj.Add(v);
        }
        return adj;
    }

    public override string ToString(){
        var sb = new StringBuilder();
        var edges = new HashSet<string>();
        sb.Append($"Vertices {VerticeCount}, Edges {EdgeCount}\n");
        foreach (var k in vertices.Keys) {
            foreach (var e in Adjacent(k)) {
                var edge = $"{k}:{e}";
                if (!edges.Contains($"{e}:{k}")) {
                    sb.Append($"{edge}\n");
                    edges.Add(edge);
                }
            }
        }
        return sb.ToString();
    }
}