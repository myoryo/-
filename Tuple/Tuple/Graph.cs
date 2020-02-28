﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tuple
{
    class Graph
    {
        const int INF = 99999999;
        private HashSet<(string, string, int)> tuples;
        private ArrayList cities;
        private int count;
        public Graph(HashSet<(string, string, int)> tuples)
        {
            this.tuples = tuples;
            cities = new ArrayList();
            CitiesToList();
            count = cities.Count;
        }

        private void CitiesToList()
        {
            foreach ((string, string, int) i in this.tuples)
            {
                if (!cities.Contains(i.Item1))
                {
                    cities.Add(i.Item1);
                }
                if (!cities.Contains(i.Item2))
                {
                    cities.Add(i.Item2);
                }
            }
        }

        // Возвращает список городов
        public ArrayList ListOfCities()
        {
            return cities;
        }
        //Выводит список городов
        public void writeOutCities()
        {
            foreach (string s in cities)
            {
                Console.WriteLine(s);
            }
        }
        //Ближайшие города
        public ArrayList NearestCitiesToList()
        {

            int minRoad = this.tuples.First().Item3;
            ArrayList cities = new ArrayList();
            foreach ((string, string, int) i in this.tuples)
            {
                if (i.Item3 < minRoad)
                {
                    minRoad = i.Item3;
                }
            }
            foreach ((string, string, int) i in this.tuples)
            {
                if (i.Item3 == minRoad)
                {
                    cities.Add(i.Item1 + " - " + i.Item2);
                }
            }
            return cities;
        }

        //Самые далекие города
        public ArrayList FurthestCitiesToList()
        {
            int maxRoad = this.tuples.First().Item3;
            ArrayList cities = new ArrayList();
            foreach ((string, string, int) i in this.tuples)
            {
                if (i.Item3 > maxRoad)
                {
                    maxRoad = i.Item3;
                }
            }
            foreach ((string, string, int) i in this.tuples)
            {
                if (i.Item3 == maxRoad)
                {
                    cities.Add(i.Item1 + " - " + i.Item2);
                }
            }
            return cities;
        }

        //Сумма дорог
        public string SumOfRoads()
        {
            string result;
            int sum = 0;
            foreach ((string, string, int) i in this.tuples)
            {
                sum += i.Item3;
            }
            result = "Сумма всех дорог " + sum;
            return result;
        }


        
        //Из одного города в другой
        public void ShortestDistance(string cityFrom, string cityTo)
        {
            if (cityTo == cityFrom || !cities.Contains(cityTo) || !cities.Contains(cityFrom))
                throw new Exception("Wrong input");
            int[,] table = fillTable(cityFrom);
            
            int[] min = ShortestDistance(cityFrom);
            string[] visited = new string[count];
            int end = cities.IndexOf(cityTo);
            visited[0] = cities[end].ToString();
            int next = 1;
            int weight = min[cities.IndexOf(cityTo)]; // вес конечной вершины
            int tempEnd=end;
            while (end != cities.IndexOf(cityFrom)) // пока не дошли до начальной вершины
            {
                for (int i = 0; i < count; i++)
                {// просматриваем все вершины
                    if (table[i, end] != 0)   // если связь есть
                    {
                        tempEnd = i;
                        int temp = weight - table[i, end]; // определяем вес пути из предыдущей вершины
                        if (temp == min[i]) // если вес совпал с рассчитанным
                        {                 // значит из этой вершины и был переход
                            weight = temp; // сохраняем новый вес
                            end = i;       // сохраняем предыдущую вершину
                            visited[next] = cities[i].ToString(); // и записываем ее в массив
                            next++;


                        }
                    }
                }
                if (tempEnd != end)
                {
                    Console.WriteLine("Такого пути нет");
                    return;
                }


            }
            for (int i = next - 1; i >= 0; i--)
                Console.WriteLine(visited[i]);
        }

        public int[] ShortestDistance(string cityFrom)
        {
           
            
            if (!cities.Contains(cityFrom))
                throw new Exception("Wrong input");
            int[] min = new int[count];
            bool[] visited = new bool[count];
            int[,] table = fillTable(cityFrom);
            for (int i = 0; i < count; i++)
            {
                min[i] = 9999999;
                visited[i] = false;
            }
            int minPath = 0;
            int minIndex = 0;
            min[0] = 0;
            do
            {
                minIndex = 9999999;
                minPath = 9999999;
                for (int i = 0; i < count; i++)
                {
                    if ((visited[i] == false) && (min[i] < minPath))
                    {
                        minPath = min[i];
                        minIndex = i;
                    }
                }
                if (minIndex != 9999999)
                {
                    for (int i = 0; i < count; i++)
                    {
                        if (table[minIndex, i] > 0)
                        {
                            int temp = minPath + table[minIndex, i];
                            if (temp < min[i])
                            {
                                min[i] = temp;
                            }
                        }
                    }
                    visited[minIndex] = true;
                }
            } while (minIndex < 9999999);

            return min;

        }

        private int[,] fillTable(string cityFrom)
        {
            if (cities.IndexOf(cityFrom) != 0)
            {
                cities[cities.IndexOf(cityFrom)] = cities[0].ToString();
                cities[0] = cityFrom;
            }
            int[,] table = new int[count, count];
            for (int i = 0; i < count; i++)
            {
                for (int j = 0; j < count; j++)
                {
                    foreach ((string, string, int) t in tuples)
                    {
                        table[i, j] = 0;
                        if (cities[i].Equals(t.Item1) && cities[j].Equals(t.Item2))
                        {
                            table[i, j] = t.Item3;
                            break;
                        }

                    }

                }

            }

            return table;
        }
    }
}
