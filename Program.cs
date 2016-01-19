using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CliqueAnalyzer.Combinatorics;

namespace CliqueAnalyzer
{
    class Program
    {
        private const int MasterSetSize = 10;
        private const int SelectionCount = 5;
        private const string FilePath = @"D:\Math\Research\PvsNP\CliqueAnalyzer\Example_1.csv";

        private static void TestComboGen()
        {
            var generator = new CombinationGenerator(MasterSetSize, SelectionCount);
            var i = 1;

            foreach (var combination in generator.CreateCombinationPump())
            {
                Console.WriteLine("Combination #{0}", i);
                i++;

                foreach (var number in combination)
                {
                    Console.Write("{0}   ", number);
                }

                Console.WriteLine(String.Empty);
            }
        }

        private static void TestBinarySearch()
        {
            Console.WriteLine("Creating network matrix from CSV {0}", Environment.NewLine);
            var networkMatrix = NetworkMatrixFactory.CreateNetworkMatrix(FilePath);
            Console.WriteLine(networkMatrix.ToString());
            Console.WriteLine(String.Empty);

            Console.WriteLine("Attempting search");
            foreach (var clique in CliqueSolver.FindLargestClique(networkMatrix))
            {
                Console.WriteLine("Found another solution!");
                Console.WriteLine(clique.ToString());
                Console.WriteLine(String.Empty);
            }
            Console.WriteLine("Search complete");
        }

        static void Main(string[] args)
        {
            /*
            var testNetwork = new TestNetwork();
            var socialNetwork = testNetwork.Network;
            var valid3Clique = new Clique(6, new List<int> { 1, 2, 3 });
            var invalid3Clique = new Clique(6, new List<int> { 0, 2, 3 });

            if (socialNetwork.DoesNetworkContainClique(valid3Clique))
                Console.WriteLine("The network correctly identified the valid 3-clique.");
            else
                Console.WriteLine("The network incorrectly dismissed the valid 3-clique.");

            if (socialNetwork.DoesNetworkContainClique(invalid3Clique))
                Console.WriteLine("The network incorrectly identified the invalid 3-clique.");
            else
                Console.WriteLine("The network correctly dismissed the invalid 3-clique.");
            */

            //TestComboGen();
            //TestBinarySearch();
            Console.WriteLine("Testing modification...");
            var foo = NetworkMatrixFactory.CreateModifiableMatrix(10);
            foo.RelateNodes(1, 10);
            foo.RelateNodes(5, 7);
            Console.WriteLine(foo.ToString());
            /*
            Console.WriteLine("Creating network matrix from CSV {0}", Environment.NewLine);
            var networkMatrix = NetworkMatrixFactory.CreateNetworkMatrix(FilePath);
            Console.WriteLine(networkMatrix.ToString());
            Console.WriteLine(String.Empty);

            foreach (var foo in CliqueSolver.FindCliquesBySize(networkMatrix, 3))
            {
                // no-op
            }
            */

            Console.ReadKey();
        }
    }
}
