public class UnionFind {
    int[] componentIds;
    int[] componentSize;
    int componentCount=0;

    public UnionFind(int N)
    {
        componentIds = new int[N];
        componentSize = new int[N];
        for (int i=0;i<N;i++) {
            componentIds[i]=i;
            componentSize[i]=1;
        }
        componentCount = N;
    }

    public void Union(int p, int q) {
        if (!Connected(p,q)) {
            var idP = FindComponent(p);
            var idQ = FindComponent(q);
            if (componentSize[idP] < componentSize[idQ]) {
                componentIds[idP] = idQ;
                componentSize[idQ] += componentSize[idP];
            }
            else {
                componentIds[idQ] = idP;
                componentSize[idP] += componentSize[idQ];
            }
            componentCount --;
        }
    }

    public int FindComponent(int p) {
        while (componentIds[p]!=p) {
            p = componentIds[p];
        }
        return p;
    }

    public bool Connected(int p, int q) {
        return FindComponent(p) == FindComponent(q);
    }

    public int ComponentCount => componentCount;
    public int[] ComponentIds => componentIds;
}