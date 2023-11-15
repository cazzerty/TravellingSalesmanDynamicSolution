namespace TravellingSalesmanDynamicSolution;

public class Graph
{
    private int vertexNum;
    private int[,] _adjacencyMatrix;


    public Graph(int vertexNum)
    {
        this.vertexNum = vertexNum;
        _adjacencyMatrix = new int[vertexNum, vertexNum];
        ClearWeights();
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
                _adjacencyMatrix[i, j] = 600;
            }
        }
    }
}