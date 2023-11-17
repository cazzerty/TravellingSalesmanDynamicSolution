// See https://aka.ms/new-console-template for more information

using TravellingSalesmanDynamicSolution;

TravellingSalesmanDynamic travellingSalesman = new TravellingSalesmanDynamic();
Graph graph;

graph = new Graph(2);
 graph.SetEdge(0,1,10);
 //graph.SetEdge(0,2,15);
// graph.SetEdge(0,3,20);
 //graph.SetEdge(1,2,35);
// graph.SetEdge(1,3,25);
// graph.SetEdge(2,3,30);
//
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
graph.GraphToStringOutput();

var watch = new System.Diagnostics.Stopwatch();

watch.Reset();
watch.Start();
Console.WriteLine(travellingSalesman.DynamicTravellingSalesman(0,graph.GetAdjacencyMatrix()));
watch.Stop();
Console.WriteLine($"Execution Time: {watch.ElapsedMilliseconds} ms");

List<int> tour = travellingSalesman.GetTour();
String tourString = "";
foreach (int index in tour) {    tourString = tourString + ($"{index}, "); }
Console.WriteLine($"{travellingSalesman.GetStepChecker()}_STEPS");
Console.WriteLine(tourString);

bool runNaive = false;
int repeat = 1;

//Random input testing
for (int i = 3; i < 28; i++)
{
    for (int j = 0; j < repeat; j++)
    {
        graph = new Graph(i);
        graph.RandomWeights();

        watch.Reset();
        watch.Start();
        int output = travellingSalesman.DynamicTravellingSalesman(0, graph.GetAdjacencyMatrix());
        if (output == int.MaxValue)
        {
            Console.WriteLine("INVALID RESULT");
            break;
        }

        Console.WriteLine($"Minimum: {output}");
        watch.Stop();
        Console.WriteLine($"Execution Time: {watch.ElapsedMilliseconds} ms");

        tour = travellingSalesman.GetTour();
        tourString = "";
        foreach (int index in tour)
        {
            tourString = tourString + ($"{index}, ");
        }

        Console.WriteLine($"{travellingSalesman.GetStepChecker()}_STEPS");
        Console.WriteLine(tourString);
    }

    if (runNaive)
    {
        watch.Reset();
        watch.Start();
        Console.WriteLine(travellingSalesman.TravellingSalesman(0, graph.GetAdjacencyMatrix()));
        watch.Stop();
        Console.WriteLine($"NAIVE Execution Time: {watch.ElapsedMilliseconds} ms");
        tour = travellingSalesman.GetTour();
        tourString = "";
        foreach (int index in tour) { tourString = tourString + ($"{index}, "); }

        Console.WriteLine($"{travellingSalesman.GetStepChecker()}_STEPS");
        Console.WriteLine(tourString);
    }
}