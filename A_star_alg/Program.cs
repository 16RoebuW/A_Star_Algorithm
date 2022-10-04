using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace A_star_alg
{
    class Program
    {
        static void Main(string[] args)
        {
            // Initial graph
            Node A = new Node("Aberdeen");
            Node B = new Node("Birmingham");
            Node C = new Node("Cardiff");
            Node D = new Node("Derby");
            Node E = new Node("Edinburgh");
            Node F = new Node("Fulford");
            Node G = new Node("Glasgow");
            Node H = new Node("Harrogate");
            Node I = new Node("Islington");
            List<Node> graph = new List<Node> { A, B, C, D, E, F, G, H, I };

            A.Join(C, 1);
            A.Join(F, 2);
            A.Join(H, 3);
            A.Join(G, 4);
            A.Join(E, 8);
            B.Join(E, 6);
            B.Join(F, 7);
            B.Join(H, 8);
            C.Join(E, 5);
            C.Join(I, 3);
            C.Join(D, 2);
            E.Join(G, 8);
            E.Join(F, 5);
            G.Join(H, 4);
            H.Join(I, 2);

            // The graph can be seen here: https://imgur.com/CDRboIP

            List<Node> shortestPath = AStar(graph, I, B);
            for (int i = 0; i < shortestPath.Count; i++)
            {
                Console.WriteLine(shortestPath[i].name);
            }
            Console.ReadLine();

        }

        public static List<Node> AStar(List<Node> graph, Node start, Node end)
        {
            // For any node in the open set, cameFrom at the same index is the node before it on the shortest path 
            var cameFrom = new List<Node>();

            // gScore is the cost of the cheapest path from start to the node
            var gScore = new List<float>();

            // fScore is our current best guess at how cheap a path could be
            var fScore = new List<float>();

            List<bool> explored = new List<bool>();

            foreach (Node n in graph)
            {
                cameFrom.Add(null);
                gScore.Add(float.PositiveInfinity);
                fScore.Add(float.PositiveInfinity);
                explored.Add(false);
            }
            int startPos = graph.IndexOf(start);
            gScore[startPos] = 0f;
            fScore[startPos] = Heuristic(start, end);

            while (explored.Contains(false))
            {
                // The node with the lowest fScore
                int currentPos = -1;
                float min = float.PositiveInfinity;
                for (int i = 0; i < fScore.Count; i++)
                {
                    if (fScore[i] < min && explored[i] == false)
                    {
                        currentPos = i;
                        min = fScore[i];
                    }
                }
                Node currentNode = graph[currentPos];
                if (currentNode == end)
                {
                    List<Node> path = new List<Node>();
                    while (currentNode != null)
                    {
                        path.Add(currentNode);
                        currentNode = cameFrom[graph.IndexOf(currentNode)];
                    }
                    path.Reverse();
                    return path;
                }
                else
                {
                    explored[currentPos] = true;
                    
                    for (int i = 0; i < currentNode.connections.Count; i++)
                    {
                        int neighbourPos = graph.IndexOf(currentNode.connections[i]);
                        float temp_gScore = gScore[currentPos] + currentNode.weights[i];
                        if (temp_gScore < gScore[neighbourPos])
                        {
                            // This path is better than any previous one
                            cameFrom[neighbourPos] = currentNode;
                            gScore[neighbourPos] = temp_gScore;
                            fScore[neighbourPos] = temp_gScore + Heuristic(graph[neighbourPos], end);

                            if (explored[neighbourPos] == true)
                            {
                                explored[neighbourPos] = false;
                            }
                        }
                    }
                }
            }
            // Failure
            return null;
        }

        public static float Heuristic(Node current, Node goal)
        {
            // Good heuristic goes here.
            return 1f;
        }
    }

    class Node
    {
        public List<Node> connections = new List<Node>();
        public List<int> weights = new List<int>();
        public string name;

        public void Join(Node other, int weight)
        {
            if (connections.Contains(other))
            {
                Console.WriteLine("Connection already exists");
                return;
            }
            else
            {
                connections.Add(other);
                weights.Add(weight);

                other.connections.Add(this);
                other.weights.Add(weight);
            }
        }
        public Node(string name)
        {
            this.name = name;
        }
    }
}
