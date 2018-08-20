using System;
using System.Text;
using static Functions;

public static class Sequences {
    static Random r = new Random(System.Environment.TickCount);

    public static int[] Empty() {
        return new int[]{};
    }

    public static int[] LinearSequence(int len,int step=1) {
        var A = new int[len];
        for (int i=0;i<len;i++)
        {
            A[i] = (i+1)*step;
        }
        return A;
    }

    public static int[] ShuffledLinearSequence(int len, int step=1) {
        var A = LinearSequence(len, step);
        for (int i=0;i<len/2;i++)
        {
            var a = (int) (r.NextDouble()*len);
            var b = (int) (r.NextDouble()*len);
            Swap(ref A[a], ref A[b]);
        }
        return A;
    }

    public static int[] MissingSequenceN(int len,int step=1)
    {
        var A = LinearSequence(len,step);
        A[len-1]+=step; // Bump last element by step
        return A;
    }

    public static int[] MissingSequenceNWithDupe(int len, int step=1)
    {
        var A = LinearSequence(len,step);
        var dup = (int)(r.NextDouble()*len); // 0 to len-1
        A[dup]=A[dup]+step;
        return A;
    }

}