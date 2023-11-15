namespace TravellingSalesmanDynamicSolution;

public class TravellingSalesmanDynamic
{
    //Input: Int: start and end Vertex
    //       Int[,]  Adjacency matrix to represent graph
    //Output
    //Naive approach: Calculate all permutations of vertex paths
    //                Keep track of permutation with lowest cost
    
    //Dynamic: Compute the optimal solution for all the subpaths of length N
    //         while using information from the already known optimal partial tours of length N-1.
    //         Visited nodes as a bit field: 001001 if vertex 0 and 3 are visited
    // https://www.youtube.com/watch?v=cY4HiiFHO1o&ab_channel=WilliamFiset


    private int visited_all;
    private int[,] memo, matrix;
    private int n;

    public int DynamicTravellingSalesman(int startIndex, int[,] graph)
    {
        if (startIndex >= graph.GetLength(0) || startIndex < 0) { return int.MaxValue;}

        n = graph.GetLength(0);
        Console.WriteLine($"Vertices: {n}");
        visited_all = (1 << n) - 1;
        
        memo = new int[n, (int)Math.Pow(2,n)];
        matrix = graph;
        for (int i = 0; i < n; i++)
        {
            for (int j = 0; j < (int)Math.Pow(2, n); j++)
            {
                memo[i, j] = int.MaxValue;
            }
        }
        
        //for(int i = 1)

        return TSP((1 << startIndex), startIndex);
    }

    /// <summary>
    /// Cache the optimal solution from the start node to every other node
    /// </summary>
    /// <param name="matrix"></param>
    /// <param name="memo"></param>
    /// <param name="n"></param>
    /// <param name="startIndex"></param>
    private void Setup(int[,] matrix, int[,] memo, int n, int startIndex)
    {
        for (int i = 0; i < n; i++)
        {
            if(i == startIndex){continue;}

            memo[i, 1 << startIndex | 1 << i] = matrix[startIndex, i]; //set (bitwise logical of leftshift startindex & leftshift i) to edgeweight of startindex,i (optimal value)
        }
    }
    
    //NEW

    int TSP(int mask, int pos)
    {
        Console.WriteLine($"{visited_all}_{mask}");
        if (mask == visited_all)
        {
            return matrix[pos,0]; //PATH TO OG
        }

        int ans = int.MaxValue;
        
        Console.WriteLine($"{n}");
        for (int vertex = 0; vertex < n; vertex++) //For each Vertex
        {
            Console.WriteLine(vertex);
            if ((mask & (1 << vertex)) == 0) //IF Vertex not visited
            {
                int newAns = matrix[pos, vertex] + TSP(mask | (1 << vertex), vertex);
                ans = Math.Min(ans, newAns);
            }
        }

        return ans;
    }
    
    
    
    
    
}