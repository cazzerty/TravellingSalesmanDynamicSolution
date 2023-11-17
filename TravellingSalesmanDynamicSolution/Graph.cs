namespace TravellingSalesmanDynamicSolution;

public class Graph
{
    private int vertexNum;
    private int[,] _adjacencyMatrix;


    /// <summary>
    /// Graph class containing functions to work with adjacency matrix
    /// </summary>
    /// <param name="vertexNum"></param>
    public Graph(int vertexNum)
    {
        this.vertexNum = vertexNum;
        _adjacencyMatrix = new int[vertexNum, vertexNum];
        ClearWeights();
    }

    public void RandomWeights()
    {
        Random rnd = new Random();
        for (int i = 0; i < vertexNum; i++)
        {
            for (int j = i + 1; j < vertexNum; j++)
            {
                if(i == j){continue;}
                SetEdge(i,j,rnd.Next(2,19) * 5);
            }
        }
    }

    public int GetVertexNum() { return vertexNum; }
    public int[,] GetAdjacencyMatrix() { return _adjacencyMatrix; }

    public void SetEdge(int index1, int index2, int weight)
    {
        if (index1 < vertexNum && index2 < vertexNum)
        {
            _adjacencyMatrix[index1, index2] = weight;
            _adjacencyMatrix[index2, index1] = weight;
        }
        else { Console.WriteLine("Invalid Index"); }
    }

    public void ClearWeights()
    {
        for (int i = 0; i < this.vertexNum; i++)
        {
            for (int j = 0; j < this.vertexNum; j++)
            {
                _adjacencyMatrix[i, j] = int.MaxValue;
            }
        }
    }

    public void GraphToStringOutput()
    {
        for (int i = 0; i < vertexNum; i++)
        {
            String output = "{";
            for (int j = 0; j < vertexNum; j++)
            {
                String toAdd = "";
                if (_adjacencyMatrix[i, j] == int.MaxValue) { toAdd = "__";}
                else { toAdd = _adjacencyMatrix[i, j].ToString(); } 
                output = output + toAdd;
                if(j == vertexNum - 1){continue;}

                output = output + ", ";

            }
            output = output + "}";
            Console.WriteLine(output);
        }
    }
}