using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CliqueAnalyzer.Combinatorics;
using CliqueAnalyzer.SocialNetwork;

namespace CliqueAnalyzer
{
    /// <summary>
    /// A static class which provides a method to find all cliques of a given size and another method to find
    /// all cliques of maximal size in a given INetworkMatrix.
    /// </summary>
    public static class CliqueSolver
    {
        /// <summary>
        /// Takes a network matrix and evaluates all possible cliques of size 'q', returning the INetworkMatrix
        /// representation of all cliques which are subnetworks of socialNetworkMatrix.
        /// </summary>
        /// <param name="socialNetworkMatrix">The social network to be searched.</param>
        /// <param name="sizeOfClique">The desired size of each clique.</param>
        /// <returns>The collection of INetworkMatrix instances which have at sizeOfClique members.</returns>
        public static IEnumerable<ICliqueMatrix> FindCliquesBySize(INetworkMatrix socialNetworkMatrix, int sizeOfClique)
        {
            var generator = new CombinationGenerator(socialNetworkMatrix.NodeCount, sizeOfClique);
            var pump = generator.CreateCombinationPump();

            foreach (var combo in pump)
            {
                var clique = NetworkMatrixFactory.CreateCliqueMatrix(combo, socialNetworkMatrix.NodeCount);

                if (socialNetworkMatrix.ContainsClique(clique))
                    yield return clique;
            }
        }

        /// <summary>
        /// Recursively searches for the largest clique size using a binary search.
        /// </summary>
        /// <param name="socialNetworkMatrix">The INetworkMatrix instance being searched.</param>
        /// <param name="rangeMin">The minimum clique size yet to be considered.</param>
        /// <param name="rangeMax">The maximum clique size yet to be considered.</param>
        /// <returns></returns>
        private static ICliqueMemento BinarySearch(INetworkMatrix socialNetworkMatrix, int rangeMin, int rangeMax)
        {
            int midpoint = (int)((rangeMin + rangeMax) / 2);

            var solutionEnumerator = FindCliquesBySize(socialNetworkMatrix, midpoint).GetEnumerator();
            var solutionsPresent = solutionEnumerator.MoveNext();

            ICliqueMatrix firstClique = null;

            if (solutionsPresent)
            {
                firstClique = solutionEnumerator.Current;
                var memento = new CliqueMemento(firstClique, solutionEnumerator);
                ICliqueMemento largerMemento = null;

                if (rangeMin < rangeMax)
                    largerMemento = BinarySearch(socialNetworkMatrix, (midpoint + 1), rangeMax);

                return largerMemento ?? memento;
            }
            else if (rangeMin < rangeMax)
                return BinarySearch(socialNetworkMatrix, rangeMin, (midpoint - 1));

            return null;
        }

        /// <summary>
        /// Implements a binary search to find the larget clique, returns each instance of maximum size.
        /// </summary>
        /// <param name="socialNetworkMatrix">The social network to be searched.</param>
        /// <returns>The collection of INetworkMatrix instances which are of the maximum size.</returns>
        public static IEnumerable<ICliqueMatrix> FindLargestClique(INetworkMatrix socialNetworkMatrix)
        {
            return BinarySearch(socialNetworkMatrix, 1, socialNetworkMatrix.NodeCount).Solutions;
        }
    }
}
