// See https://aka.ms/new-console-template for more information

using TravellingSalesmanDynamicSolution;

TravellingSalesmanDynamic travellingSalesman = new TravellingSalesmanDynamic();
Graph graph;

graph = new Graph(4);
graph.SetEdge(0,1,10);
graph.SetEdge(0,2,15);
graph.SetEdge(0,3,20);
graph.SetEdge(1,2,35);
graph.SetEdge(1,3,25);
graph.SetEdge(2,3,30);

Console.WriteLine(travellingSalesman.DynamicTravellingSalesman(0,graph.GetAdjacencyMatrix()));