using System;
using System.Text;
using System.Collections.Generic;

public class NodeLink<T> {
    public T Value { get; set; }
    public NodeLink<T> Next { get; set;}
    public NodeLink() {
        Value = default(T);
        Next = this;
    }
    public NodeLink(T val) {
        Value = val;
        Next = this;
    }
    public NodeLink(T val, NodeLink<T> next) {
        Value = val;
        Next = next;
    }
}

public class NodeDLink<T> {
    public T Value { get; set; }
    public NodeDLink<T> Next { get; set;}
    public NodeDLink<T> Prev { get; set;}
}

public class Linked {
    public static NodeLink<T> LList<T>(List<T> list) {
        var top = new NodeLink<T>(); 
        var p = top;
        foreach (var i in list) {
            p.Next = new NodeLink<T>(i);
            p = p.Next;
        }
        p.Next = null; // last item points nowhere
        return top;
    }

    public static bool IsCyclic<T>(NodeLink<T> list) {
        NodeLink<T> fast = list;
        NodeLink<T> slow = list;
        while (fast != null && fast.Next != null) {
            fast = fast.Next.Next;
            slow = slow.Next;
            if (fast == slow) {
                return true;
            }
        }
        return false;
    }

    public static NodeLink<T> CycleNode<T>(NodeLink<T> list) {
        if (Linked.IsCyclic(list)==false) {
            return default(NodeLink<T>);
        }
        NodeLink<T> fast = list;
        NodeLink<T> slow = list;
        NodeLink<T> prevfast = list;
        while (fast != null && fast.Next != null) {
            prevfast = fast;
            fast = fast.Next.Next;
            slow = slow.Next;
            if (fast == slow) {
                return prevfast.Next;
            }
        }
        return default(NodeLink<T>);
    }

    public static int Count<T>(NodeLink<T> list) {
        int counter=0;
        var p = list;
        if (!Linked.IsCyclic(list)) {
            while (p != null && p.Next != null) {
                counter ++;
                p = p.Next;
            }
        }
        else {
            throw new Exception("List is Cyclic.");
        }
        return counter;
    }

    public static NodeLink<T> Reverse<T>(NodeLink<T> list) {
        var p = list;
        NodeLink<T> prev = null;
        NodeLink<T> next = null;
        var last = list.Next;
        while (p != null && p.Next != null) {
            next = p.Next; 
            p.Next = prev;
            prev = p;
            p = next;
        }
        if (last != null) {
            list.Next = p;
            list.Next.Next = prev;
            last.Next = null;
        }
        return list;
    }

    public static string ToString<T>(NodeLink<T> list) {
        var p = list;
        if (!Linked.IsCyclic(p)) {
            var sb = new StringBuilder();
            sb.Append("[");
            while (p != null && p.Next != null) {
                if (p.Next.Value != null)
                    sb.Append($"{p.Next.Value.ToString()},");
                else
                    sb.Append($"(null)");
                p = p.Next;
            }
            if (sb[sb.Length-1] != '[')
                sb.Remove(sb.Length-1,1);
            sb.Append("]");
            return sb.ToString();
        }
        else {
            return "[cyclic]";
        }
    }
}



