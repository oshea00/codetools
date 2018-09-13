using System.Collections.Generic;
using System.Text;

public class PhoneEncoder
{
    Dictionary<string,List<string>> index;
    public PhoneEncoder(List<string> words)
    {
        index = new Dictionary<string,List<string>>();
        foreach (var w in words) {
            var code = Charmap(w);
            if (index.ContainsKey(code)) {
                index[code].Add(w);
            } else {
                index[code] = new List<string>();
                index[code].Add(w);
            }
        }
    }

    public List<string> Encode(string code) {
        if (index.ContainsKey(code)) {
            return index[code];
        } else {
            return new List<string>();
        }
    }

    string Charmap(string word) {
        var sb = new StringBuilder();
        string map = "22233344455556667778889999";
        foreach (var c in word) {
            var e = c - 'a';
            sb.Append(map[e]);
        }
        return sb.ToString();
    }
}