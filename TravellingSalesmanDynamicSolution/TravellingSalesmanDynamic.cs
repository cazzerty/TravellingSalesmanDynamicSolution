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
    // Resources:
    // https://www.geeksforgeeks.org/travelling-salesman-problem-using-dynamic-programming/?ref=header_search
    // https://www.youtube.com/watch?v=cY4HiiFHO1o&ab_channel=WilliamFiset
    // https://www.youtube.com/watch?v=JE0JE8ce1V0&ab_channel=CodingBlocks


    private int visited_all;
    private int[,] memo, matrix, next;
    private int n;
    private int startIndex;

    private int stepChecker = 0;

    public int TravellingSalesman(int startIndex, int[,] graph)
    {
        stepChecker = 0;
        if (startIndex >= graph.GetLength(0) || startIndex < 0) { return int.MaxValue;}

        this.startIndex = startIndex;
        n = graph.GetLength(0);
        Console.WriteLine($"Vertices: {n}");
        
        visited_all = (1 << n) - 1;
        memo = new int[(int)Math.Pow(2,n), n];
        next = new int[(int)Math.Pow(2,n), n];
        matrix = graph;
        
        //Initialise Memo
        for (int i = 0; i < n; i++)
        {
            for (int j = 0; j < (int)Math.Pow(2, n); j++)
            {
                memo[j, i] = int.MaxValue;
                next[j, i] = int.MaxValue;
            }
        }
        
        return TSP((1 << startIndex), startIndex);
    }
    public int DynamicTravellingSalesman(int startIndex, int[,] graph)
    {
        stepChecker = 0;
        if (startIndex >= graph.GetLength(0) || startIndex < 0) { return int.MaxValue;}

        this.startIndex = startIndex;
        n = graph.GetLength(0);
        Console.WriteLine($"Vertices: {n}");
        
        visited_all = (1 << n) - 1;
        memo = new int[(int)Math.Pow(2,n), n];
        next = new int[(int)Math.Pow(2,n), n];
        matrix = graph;
        
        //Initialise Memo
        for (int i = 0; i < n; i++)
        {
            for (int j = 0; j < (int)Math.Pow(2, n); j++)
            {
                memo[j, i] = int.MaxValue;
                next[j, i] = int.MaxValue;
            }
        }

        return TSP_Dynamic((1 << startIndex), startIndex);
    }
    
    //NEW

    private int TSP(int mask, int pos)
    {
        stepChecker++;
        if (mask == visited_all)
        {
            return matrix[pos,startIndex]; //PATH TO OG
        }

        int ans = int.MaxValue;
        int index = -1;
        //Console.WriteLine($"{n}");
        for (int vertex = 0; vertex < n; vertex++) //For each Vertex
        {
            if ((mask & (1 << vertex)) == 0) //IF Vertex not visited
            {
                int newAns = matrix[pos, vertex] + TSP(mask | (1 << vertex), vertex);
                
                if (newAns < ans)
                {
                    ans = newAns;
                    index = vertex; 
                }
            }
        }

        next[mask, pos] = index; //keep track of next index so path can be rebuilt
        return ans;
    }
    
    private int TSP_Dynamic(int mask, int pos)
    {
        stepChecker++; //
        if (mask == visited_all)
        {
            return matrix[pos,startIndex]; //PATH TO OG
        }

        if (memo[mask,pos] != int.MaxValue)
        {
            return memo[mask, pos];
        }

        int ans = int.MaxValue;
        int index = -1;
        
        for (int vertex = 0; vertex < n; vertex++) //For each Vertex
        {
            if ((mask & (1 << vertex)) == 0) //IF Vertex not visited
            {
                int newAns = matrix[pos, vertex] + TSP_Dynamic(mask | (1 << vertex), vertex);
                if (newAns < ans)
                {
                    ans = newAns;
                    index = vertex; 
                }
            }
        }
        
        next[mask, pos] = index; //keep track of next index so path can be rebuilt
        return memo[mask, pos] = ans;
    }

    /// <summary>
    /// Rebuild tour using saved next index
    /// </summary>
    /// <returns></returns>
    public List<int> GetTour()
    {
        List<int> tour = new List<int>();
        int index = startIndex;
        int mask = 1 << startIndex;
        while (true) {
            tour.Add(index);
            int nextIndex = next[mask,index];
            if (nextIndex == int.MaxValue) break;
            int nextMask = mask | (1 << nextIndex);
            mask = nextMask;
            index = nextIndex;
        }
        tour.Add(startIndex);

        return tour;
    }

    public int GetStepChecker() { return stepChecker; }
    
    
    
}