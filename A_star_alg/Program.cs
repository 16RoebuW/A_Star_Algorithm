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
            Node A = new Node();
            Node B = new Node();
            Node C = new Node();
            Node D = new Node();
            Node E = new Node();
            Node F = new Node();
            Node G = new Node();
            Node H = new Node();
            Node I = new Node();

            A.Join(C, 1);
            A.Join(F, 2);
            A.Join(H, 3);
            A.Join(G, 4);
            A.Join(E, 5);
            B.Join(E, 6);
            B.Join(F, 7);
            B.Join(H, 8);
            C.Join(E, 5);
            C.Join(I, 3);
            C.Join(D, 2);
            E.Join(G, 8);
            E.Join(F, 7);
            G.Join(H, 4);
            H.Join(I, 2);




        }
    }

    class Node
    {
        public List<Node> connections = new List<Node>();
        public List<int> weights = new List<int>();

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
    }
}
