using System;
using System.Collections;
using System.Collections.Generic;
namespace Tuple
{
    class Program
    {
        static void Main(string[] args)
        {
            HashSet<(string, string, int)> roads = new HashSet<(string, string, int)>();
            roads.Add(("St.Petersburg","Novosibirsk",27));
            roads.Add(("Tomsk", "Novosibirsk",5));            
            roads.Add(("Novosibirsk", "Berdsk",1));
            roads.Add(("Kaluga", "Novosibirsk",13));
            roads.Add(("Kaluga", "St.Petersburg",2));
            roads.Add(("St.Petersburg", "Kaluga",14));
            roads.Add(("Tomsk", "Kaluga",3));
            roads.Add(("Berdsk", "Tomsk",4));
            roads.Add(("St.Petersburg", "Tomsk", 10));

            Graph graph = new Graph(roads);
            graph.writeOutCities();
            Console.WriteLine();
            Console.WriteLine("Самые далекие города с одной связью: ");
            ArrayList  furthest = graph.FurthestCitiesToList();
            for(int i = 0; i < furthest.Count; i++)
            {
                Console.WriteLine(furthest[i]);
            }
            Console.WriteLine();
            Console.WriteLine(graph.SumOfRoads());
            Console.WriteLine();
            Console.WriteLine("Самые близкие города с одной связью: ");
            ArrayList nearest = graph.NearestCitiesToList();
            for (int i = 0; i < nearest.Count; i++)
            {
                Console.WriteLine(nearest[i]);
            }
            Console.WriteLine();
            Console.WriteLine("Кратчайший путь из одного в другой город ");
            graph.ShortestDistance("St.Petersburg","Novosibirsk");
            Console.WriteLine();
            Console.WriteLine("Кратчайшие пути во все города ");
            int[] min = graph.ShortestDistance("St.Petersburg");
            for (int i = 0; i < graph.ListOfCities().Count; i++)
            {
                Console.WriteLine(graph.ListOfCities()[i]+ " - " +min[i]);
            }
            Console.ReadKey();

        }

    }
}
