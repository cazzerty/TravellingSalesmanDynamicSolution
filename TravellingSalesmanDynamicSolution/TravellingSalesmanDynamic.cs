using System.Diagnostics;

namespace TravellingSalesmanDynamicSolution;

public class TravellingSalesmanDynamic
{
    // Implementation by Lachlan Garrity:
    //                   SID: 13561523
    //
    //Input: Int: start and end Vertex
    //       Int[,]  Adjacency matrix to represent graph
    //Output
    //Naive approach: Calculate all permutations of vertex paths
    //                Keep track of permutation with lowest cost
    //
    //Dynamic: Compute the optimal solution for all the subpaths of length N
    //         while using information from the already known optimal partial tours of length N-1.
    //         Visited nodes as a bit field: 001001 if vertex 0 and 3 are visited
    //
    // Resources I found useful:
    // https://www.geeksforgeeks.org/travelling-salesman-problem-using-dynamic-programming/?ref=header_search
    // https://www.youtube.com/watch?v=cY4HiiFHO1o&ab_channel=WilliamFiset
    // https://www.youtube.com/watch?v=JE0JE8ce1V0&ab_channel=CodingBlocks


    private int visited_all;
    private int[,] memo, matrix, next;
    private int n;
    private int startIndex;

    private int stepChecker = 0; //Used to compare performance of Naive and Dynamic TSP

    /// <summary>
    /// SETUP function for Naive tsp
    /// </summary>
    /// <param name="startIndex"></param>
    /// <param name="graph"></param>
    /// <returns>minimum weight cost of hamiltonian cycle</returns>
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
    
    /// <summary>
    /// SETUP function for Dynamic TSP. Calls the first recursive function
    /// THIS IS THE IMPORTANT IMPLEMENTATION: THE OTHER IS JUST FOR COMPARISON TESTING
    /// </summary>
    /// <param name="startIndex"></param>
    /// <param name="graph"></param>
    /// <returns>minimum weight cost of hamiltonian cycle</returns>
    public int DynamicTravellingSalesman(int startIndex, int[,] graph)
    {
        stepChecker = 0;
        
        //Check if input data is valid
        if (startIndex >= graph.GetLength(0) || startIndex < 0) { return -1;}

        if (graph.GetLength(0) <= 1) { return int.MaxValue;}

        if (graph.GetLength(0) >= 32)
        {
            Console.WriteLine("Too many cities");
            return int.MaxValue;
        }
        
        //SETUP
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
    
/// <summary>
/// Naive TSP recursive function
/// </summary>
/// <param name="mask"></param>
/// <param name="pos"></param>
/// <returns></returns>
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
    
    /// <summary>
    /// Recursive function for the Dynamic TSP. Called by respective SETUP function
    /// THIS IS THE IMPORTANT IMPLEMENTATION: THE OTHER IS JUST FOR COMPARISON TESTING
    /// </summary>
    /// <param name="mask"></param>
    /// <param name="pos"></param>
    /// <returns></returns>
    private int TSP_Dynamic(int mask, int pos)
    {
        stepChecker++; //Debug variable tracks # of recursion
        if (mask == visited_all) //At final node in tour?
        {
            return matrix[pos,startIndex]; //PATH TO OG
        }

        if (memo[mask,pos] != int.MaxValue) //Has the sub problem already been solved?
        {
            return memo[mask, pos]; //Stored solution
        }

        int ans = int.MaxValue;
        int index = -1;
        
        for (int vertex = 0; vertex < n; vertex++) //For each Vertex
        {
            if ((mask & (1 << vertex)) == 0) //IF Vertex not visited
            {
                int newAns = matrix[pos, vertex] + TSP_Dynamic(mask | (1 << vertex), vertex); //What ongoing weight is equal to (gets sub nodes first: Works out from bottom up)
                if (newAns < ans && newAns > 0)
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
    /// <returns>List<int> of indexes</returns>
    public List<int> GetTour()
    {
        List<int> tour = new List<int>();
        int index = startIndex;
        int mask = 1 << startIndex;
        //Follow path given by next index repeatedly to build complete tour
        while (true) {
            tour.Add(index);
            int nextIndex = next[mask,index];
            if (nextIndex == int.MaxValue) break;
            int nextMask = mask | (1 << nextIndex);
            mask = nextMask;
            index = nextIndex;
        }
        tour.Add(startIndex); //Add star node to complete hamiltonian cycle

        return tour;
    }

    public int GetStepChecker() { return stepChecker; } //GETTER
    
    
    
}