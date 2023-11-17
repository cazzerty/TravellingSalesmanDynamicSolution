// See https://aka.ms/new-console-template for more information

using System.Diagnostics;
using TravellingSalesmanDynamicSolution;

TravellingSalesmanDynamic travellingSalesman = new TravellingSalesmanDynamic();

Graph graph;

#region Testing functions

bool CorrectnessTester(List<int> result, List<int> expectedValue)
{
    if (result.SequenceEqual(expectedValue)) { return true;}
    result.Reverse();
    if (result.SequenceEqual(expectedValue)) { return true;}
    return false;
}

void RunTest(Graph g, List<int> expectedTour)
{
    Console.WriteLine();
    Console.WriteLine("----------TEST----------");
    graph.GraphToStringOutput();
    var watch = new System.Diagnostics.Stopwatch();
    watch.Start();
    int output = travellingSalesman.DynamicTravellingSalesman(0, g.GetAdjacencyMatrix());
    watch.Stop();
    Console.WriteLine($"MinimumCostTour: {output}");
    
    Console.WriteLine($"Execution Time: {watch.ElapsedMilliseconds} ms");
    
    if (output == int.MaxValue)
    {
        Console.WriteLine("INVALID RESULT");
        return;
    }
    
    Console.WriteLine($"Recursive Steps:{travellingSalesman.GetStepChecker()}");
    
    //Evaluate tour
    List<int> tour = travellingSalesman.GetTour();
    String tourString = "";
    foreach (int index in tour) {    tourString = tourString + ($"{index}, "); }

    Console.WriteLine(expectedTour.Count < 3
        ? $"Tour: {tourString}"
        : $"Tour: {tourString} is correct?: {CorrectnessTester(tour, expectedTour)}");
}

#endregion

#region Designed Tests

List<int> expectedIntList = new List<int>();

graph = new Graph(3);

graph.SetEdge(0,1,40);
graph.SetEdge(1,2,30);
graph.SetEdge(0,2,20);

expectedIntList.Add(0);
expectedIntList.Add(1);
expectedIntList.Add(2);
expectedIntList.Add(0);

RunTest(graph,expectedIntList);

graph = new Graph(4);

graph.SetEdge(0,1,10);
graph.SetEdge(0,2,15);
graph.SetEdge(0,3,20);
graph.SetEdge(1,2,35);
graph.SetEdge(1,3,25);
graph.SetEdge(2,3,30);

expectedIntList.Clear();
expectedIntList.Add(0);
expectedIntList.Add(1);
expectedIntList.Add(3);
expectedIntList.Add(2);
expectedIntList.Add(0);

RunTest(graph,expectedIntList);

graph = new Graph(10);
graph.SetEdge(0,5,10);
graph.SetEdge(5,1,10);
graph.SetEdge(1,3,10);
graph.SetEdge(3,7,10);
graph.SetEdge(7,4,10);
graph.SetEdge(4,2,10);
graph.SetEdge(2,9,10);
graph.SetEdge(9,6,10);
graph.SetEdge(6,8,10);
graph.SetEdge(8,0,10);

expectedIntList.Clear();
expectedIntList.Add(0);
expectedIntList.Add(5);
expectedIntList.Add(1);
expectedIntList.Add(3);
expectedIntList.Add(7);
expectedIntList.Add(4);
expectedIntList.Add(2);
expectedIntList.Add(9);
expectedIntList.Add(6);
expectedIntList.Add(8);
expectedIntList.Add(0);

RunTest(graph,expectedIntList);

expectedIntList.Clear();
graph = new Graph(2);
RunTest(graph,expectedIntList); //Expecting invalid

graph = new Graph(5);
graph.SetEdge(0,1,55);
graph.SetEdge(0,2,35);
graph.SetEdge(0,3,15);
graph.SetEdge(0,4,25);
graph.SetEdge(1,2,15);
graph.SetEdge(1,3,75);
graph.SetEdge(1,4,45);
graph.SetEdge(2,3,35);
graph.SetEdge(2,4,10);
graph.SetEdge(3,4,40);

expectedIntList.Clear();
expectedIntList.Add(0);
expectedIntList.Add(1);
expectedIntList.Add(2);
expectedIntList.Add(4);
expectedIntList.Add(3);
expectedIntList.Add(0);
RunTest(graph,expectedIntList);


// graph.SetEdge(1,4,45);
// graph.SetEdge(2,4,30);
// graph.SetEdge(3,4,35);
//
// graph.SetEdge(1,5,45);
// graph.SetEdge(2,5,30);
// graph.SetEdge(3,5,35);
// graph.SetEdge(4,5,25);


//graph = new Graph(8);
//graph.RandomWeights();

#endregion

#region Procedural Testing
bool runNaive = false;
int repeat = 0;
int citiesMax;
Console.WriteLine("");
Console.WriteLine("----Procedural testing----");
Console.WriteLine("Input max cities: Input must be > 2 && < 32");
citiesMax = Convert.ToInt32(Console.ReadLine());
Console.WriteLine("Input the amount of times each test will repeat:");
repeat = Convert.ToInt32(Console.ReadLine());
Console.WriteLine("Input 'c' to test against naive:");
runNaive = (ConsoleKey.C ==Console.ReadKey().Key);



//Random input testing
for (int i = 3; i < citiesMax; i++)
{
    for (int j = 0; j < repeat; j++)
    {
        graph = new Graph(i);
        graph.RandomWeights();

        RunTest(graph, new List<int>()); //List Doesn't Matter
    }

    if (runNaive)
    {
        Console.WriteLine();
        Console.WriteLine("testing Naive");
        var watch2 = new System.Diagnostics.Stopwatch();
        watch2.Reset();
        watch2.Start();
        Console.WriteLine($"MinimumCostTour: {travellingSalesman.TravellingSalesman(0, graph.GetAdjacencyMatrix())}");
        watch2.Stop();
        Console.WriteLine($"NAIVE Execution Time: {watch2.ElapsedMilliseconds} ms");
        List<int> tour;
        tour = travellingSalesman.GetTour();
        String tourString = "";
        foreach (int index in tour) { tourString = tourString + ($"{index}, "); }

        Console.WriteLine($"Recursive Steps: {travellingSalesman.GetStepChecker()}");
        Console.WriteLine(tourString);
    }
}

Console.WriteLine("Testing Finished");
Console.WriteLine("Press any key to close");
Console.ReadKey();
#endregion