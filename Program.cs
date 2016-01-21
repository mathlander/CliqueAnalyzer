using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CliqueAnalyzer.Combinatorics;
using CliqueAnalyzer.SocialNetwork;

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

        private static void TestNodeOfInterest()
        {
            Console.WriteLine("Testing search for maximal clique involving node ID [6].");
            var socialNetwork = NetworkMatrixFactory.CreateModifiableMatrix(12);

            foreach (var num in Enumerable.Range(1, 12))
            {
                // 1 is related to all other nodes
                socialNetwork.RelateNodes(1, num);

                // 2 is related to all even nodes
                if (num % 2 == 0)
                    socialNetwork.RelateNodes(2, num);

                // 3 is related to all multiples of three
                if (num % 3 == 0)
                    socialNetwork.RelateNodes(3, num);

                // 5 is related to all multiples of 5
                if (num % 5 == 0)
                    socialNetwork.RelateNodes(5, num);

                // 7 is related to all multiples of 7
                if (num % 7 == 0)
                    socialNetwork.RelateNodes(7, num);

                if (num % 11 == 0)
                    socialNetwork.RelateNodes(11, num);
            }

            // 5 is related to all odd numbers
            socialNetwork.RelateNodes(5, 1);
            socialNetwork.RelateNodes(5, 3);
            socialNetwork.RelateNodes(5, 5);
            socialNetwork.RelateNodes(5, 7);
            socialNetwork.RelateNodes(5, 9);
            socialNetwork.RelateNodes(5, 11);

            // 7 is related to multiples of 3
            socialNetwork.RelateNodes(7, 3);
            socialNetwork.RelateNodes(7, 6);
            socialNetwork.RelateNodes(7, 9);
            socialNetwork.RelateNodes(7, 12);

            Console.WriteLine(socialNetwork.ToString());

            // find the maximal clique
            Console.WriteLine("The maximal cliques are:");

            foreach (var clique in CliqueSolver.FindLargestClique(socialNetwork))
            {
                Console.WriteLine(Environment.NewLine);
                Console.WriteLine(clique.ToString());
            }

            Console.WriteLine("The maximal cliques involving [12] are:");
            foreach (var clique in CliqueSolver.FindLargestCliqueInvolvingNode(socialNetwork, 12))
            {
                Console.WriteLine(Environment.NewLine);
                Console.WriteLine(clique.ToString());
            }
        }

        private static void TestSubnetworkCondition()
        {
            var memberNodeIds = new List<int> { 1, 3, 5 };
            var cliqueToExpand = NetworkMatrixFactory.CreateCliqueMatrix(memberNodeIds, 12);
            var socialNetwork = NetworkMatrixFactory.CreateModifiableMatrix(12);

            foreach (var num in Enumerable.Range(1, 12))
            {
                // 1 is related to all other nodes
                socialNetwork.RelateNodes(1, num);

                // 2 is related to all even nodes
                if (num % 2 == 0)
                    socialNetwork.RelateNodes(2, num);

                // 3 is related to all multiples of three
                if (num % 3 == 0)
                    socialNetwork.RelateNodes(3, num);

                // 5 is related to all multiples of 5
                if (num % 5 == 0)
                    socialNetwork.RelateNodes(5, num);

                // 7 is related to all multiples of 7
                if (num % 7 == 0)
                    socialNetwork.RelateNodes(7, num);

                if (num % 11 == 0)
                    socialNetwork.RelateNodes(11, num);
            }

            // 5 is related to all odd numbers
            socialNetwork.RelateNodes(5, 1);
            socialNetwork.RelateNodes(5, 3);
            socialNetwork.RelateNodes(5, 5);
            socialNetwork.RelateNodes(5, 7);
            socialNetwork.RelateNodes(5, 9);
            socialNetwork.RelateNodes(5, 11);

            // 7 is related to multiples of 3
            socialNetwork.RelateNodes(7, 3);
            socialNetwork.RelateNodes(7, 6);
            socialNetwork.RelateNodes(7, 9);
            socialNetwork.RelateNodes(7, 12);

            Console.WriteLine("The clique to be expanded: {0}", Environment.NewLine);
            Console.WriteLine(cliqueToExpand.ToString());

            Console.WriteLine("The maximal cliques involving [{0}] are:", String.Join(",", memberNodeIds));
            foreach (var clique in CliqueSolver.FindLargestCliqueInvolvingSubnetwork(socialNetwork, cliqueToExpand))
            {
                Console.WriteLine(Environment.NewLine);
                Console.WriteLine(clique.ToString());
            }
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
            //TestNodeOfInterest();
            TestSubnetworkCondition();

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
