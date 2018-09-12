using System;
using System.IO;
using System.Collections.Generic;
using System.Text;

public class MovieDB {
    Graph<int> g;
    Dictionary<string,int> keys;
    Dictionary<int,string> labels;
    List<int> pairs;
    public MovieDB() {
        pairs = new List<int>();
        keys = new Dictionary<string,int>();
        labels = new Dictionary<int,string>();
        var symbolid=0;
        var movieid=0;
        var actorid=0;
        var linecount=0;
        using (var f = new StreamReader("movies.txt")) {
            var movie = f.ReadLine();
            while (!f.EndOfStream) {
                var toks = movie.Split(new[]{'/'});
                movieid = MakeSymbol(toks[0],ref symbolid);
                //$"{movieid} {toks[0]} Actors:".Dump();
                for (int i=1;i<toks.Length;i++) {
                    actorid = MakeSymbol(toks[i],ref symbolid);
                    pairs.Add(movieid);
                    pairs.Add(actorid);
                    //$"-> {symbolid} {toks[i]}".Dump();
                }
                linecount++;
                //if (linecount>640)
                //    break;
                movie = f.ReadLine();
            }
        }
        g = new Graph<int>(pairs);
        $"{linecount} Movies loaded.".Dump();
        $"LastID {symbolid}".Dump();
        if (Count != symbolid) {
            $"Vertice count doesn't match - possibly corrupted movies.txt file.".Dump();
        }
    }

    int MakeSymbol(string s,ref int symbolid) {
        int key = 0;
        if (keys.ContainsKey(s)) {
            key = keys[s];
        } else {
            key = symbolid++;
            keys[s] = key;
            labels[key] = s;
        }
        return key;
    }

    public string MovieWithMostActors() {
        int maxActors=0;
        int movieId=0;
        Graph.GetVerticesWithColor(false).ForEach(m=>{
            var d = g.Degree(m);
            if (d>maxActors) {
                maxActors = d;
                movieId = m; 
            }
        });
        return $"[{movieId}] {labels[movieId]} has {maxActors} actors.";
    }

    public string ActorWithMostMovies() {
        int max=0;
        int actorid=0;
        Graph.GetVerticesWithColor(true).ForEach(a=>{
            var degree = Graph.Degree(a); 
            if (degree>max) {
                max = degree;
                actorid = a;
            }
        });
        return $"[{actorid}] {labels[actorid]} has the most movies: {max}.";
        
    }

    public string KevinBaconReport() {
        var sb = new StringBuilder();
        int k = keys["Bacon, Kevin"];
        var mlist = new List<string>();
        var alist = new List<string>();
        Graph.GetVerticesWithColor(false).ForEach(m=>{
            if (Graph.Adjacent(m).Contains(k)) {
                mlist.Add($"->{labels[m]}");
            }
        });
        sb.Append($"================ KEVIN BACON REPORT ================\n");
        sb.Append($"All Movies With Kevin Bacon ({mlist.Count}):\n");
        mlist.ForEach(labels=>{
            sb.Append($"{labels}\n");
        });
        Graph.FindComponents();
        Graph.GetVerticesWithColor(true).ForEach(a=>{
            if (Graph.SameComponent(a,k)) {
                alist.Add($"->{labels[a]}");
            }
        });
        sb.Append("\n");
        sb.Append($"Some Actors Connected to Kevin Bacon Somehow ({alist.Count}):\n");
        var actors=0;
        alist.ForEach(labels=>{
            if (actors<25)
                sb.Append($"{labels}\n");
            actors++;
        });
        sb.Append($"...\n\n");
        sb.Append($"More to come!\n");
        return sb.ToString();
    }

    public Graph<int> Graph => g;
    public int Count => g.VerticeCount;
}