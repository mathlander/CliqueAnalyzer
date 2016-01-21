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
        /// <returns>A memento which allows for the collection of solution cliques to be iterated.</returns>
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
            var searchResults = BinarySearch(socialNetworkMatrix, 1, socialNetworkMatrix.NodeCount);

            return searchResults != null
                ? searchResults.Solutions
                : Enumerable.Empty<ICliqueMatrix>();
        }

        /// <summary>
        /// Takes an INetworkMatrix instance and composes a new INetworkMatrix instance composed of only the collection of
        /// nodes given by memberNodeIds.  Cost of method is (n^2 + n), where n = memberNodesIds.Count.
        /// </summary>
        /// <param name="socialNetworkMatrix">The social network to be searched.</param>
        /// <param name="memberNodeIds">The collection of node IDs to be included in the subnetwork.</param>
        /// <returns>A tuple composed of the new subnetwork, a map of original node IDs to new node IDs, and an inverse map of
        /// new node IDs to original node IDs.</returns>
        private static Tuple<INetworkMatrix, IDictionary<int, int>, IDictionary<int, int>> CreateSubnetworkFromNodeIds(INetworkMatrix socialNetworkMatrix, ICollection<int> memberNodeIds)
        {
            var subnetwork = NetworkMatrixFactory.CreateModifiableMatrix(memberNodeIds.Count);
            var map = new Dictionary<int, int>(memberNodeIds.Count);
            var inverseMap = new Dictionary<int, int>(memberNodeIds.Count);
            var i = 1;

            foreach (var nodeId in memberNodeIds)
            {
                map.Add(nodeId, i);
                inverseMap.Add(i, nodeId);

                i++;
            }

            foreach (var nodeId in memberNodeIds)
            {
                var mappedNodeId = map[nodeId];

                foreach (var relatedNodeId in socialNetworkMatrix.GetNodeById(nodeId).RelatedNodes)
                {
                    // relationships to nodes not defined in the subnetwork are ignored
                    if (map.ContainsKey(relatedNodeId))
                        subnetwork.RelateNodes(mappedNodeId, map[relatedNodeId]);
                }
            }

            return new Tuple<INetworkMatrix, IDictionary<int, int>, IDictionary<int, int>>(subnetwork, map, inverseMap);
        }

        /// <summary>
        /// Finds the largest clique in which nodeId is a member and returns all cliques of that size which meet that criterion.
        /// </summary>
        /// <param name="socialNetworkMatrix">The INetworkMatrix instance being searched.</param>
        /// <param name="nodeId">The ID for the node of interest.</param>
        /// <returns>The collection of ICliqueMatrix instances of maximal size which include nodeId as a member.</returns>
        public static IEnumerable<ICliqueMatrix> FindLargestCliqueInvolvingNode(INetworkMatrix socialNetworkMatrix, int nodeId)
        {
            var relatedNodes = new List<int>(Enumerable.Range(1, socialNetworkMatrix.NodeCount).Where<int>( secondNodeId => socialNetworkMatrix.NodesRelate(nodeId, secondNodeId) ));
            var tuple = CreateSubnetworkFromNodeIds(socialNetworkMatrix, relatedNodes);
            var subnetwork = tuple.Item1;
            var map = tuple.Item2;
            var inverseMap = tuple.Item3;

            // uses the smaller subnetwork created from the related nodes of nodeId to search for a clique of maximal size,
            // of which we are guaranteed that nodeId is a member (since it is related to all nodes in this subnetwork)
            //
            // in turn, this collection of maximal cliques is mapped back to a collection of ICliqueMatrix instances where
            // the node IDs reflect the node IDs of the original socialNetworkMatrix
            return FindLargestClique(subnetwork)
                .Select<ICliqueMatrix, ICliqueMatrix>(
                    cliqueOfSubnet => NetworkMatrixFactory.CreateCliqueMatrix( cliqueOfSubnet.MemberNodeIds.Select<int, int>( subnetId => inverseMap[subnetId] ), socialNetworkMatrix.NodeCount) );
        }

        /// <summary>
        /// Uses a binary search to recursively seek the largest clique containing a subnetwork.
        /// </summary>
        /// <param name="socialNetworkMatrix">The INetworkMatrix instance being searched.</param>
        /// <param name="rangeMin">The minimum clique size yet to be considered.</param>
        /// <param name="rangeMax">The maximum clique size yet to be considered.</param>
        /// <param name="predicate">A condition which must be met in order for an ICliqueMatrix instance to be considered a solution.</param>
        /// <returns>A memento which allows for the collection of solution cliques to be iterated.</returns>
        private static ICliqueMemento PredicatedBinarySearch(INetworkMatrix socialNetworkMatrix, int rangeMin, int rangeMax, Predicate<ICliqueMatrix> predicate)
        {
            int midpoint = (int)((rangeMin + rangeMax) / 2);

            var solutionEnumerator = FindCliquesBySize(socialNetworkMatrix, midpoint).GetEnumerator();
            ICliqueMatrix firstClique = null;

            // see if any of the cliques meet the predicate condition
            while (solutionEnumerator.MoveNext())
            {
                if (predicate(solutionEnumerator.Current))
                {
                    firstClique = solutionEnumerator.Current;
                    break;
                }
            }

            // check to see if a valid solution was found
            if (firstClique != null)
            {
                var memento = new PredicatedCliqueMemento(firstClique, solutionEnumerator, predicate);
                ICliqueMemento largerMemento = null;

                if (rangeMin < rangeMax)
                    largerMemento = PredicatedBinarySearch(socialNetworkMatrix, (midpoint + 1), rangeMax, predicate);

                return largerMemento ?? memento;
            }
            else if (rangeMin < rangeMax)
                return PredicatedBinarySearch(socialNetworkMatrix, rangeMin, (midpoint - 1), predicate);

            return null;
        }

        /// <summary>
        /// Finds all cliques of maximal size of which the given subnetwork members are a subset.
        /// </summary>
        /// <param name="socialNetworkMatrix">The INetworkMatrix instance being searched.</param>
        /// <param name="subnetwork">An ICliqueMatrix composed of a set of nodes which should be included in any solution ICliqueMatrix instances.</param>
        /// <returns>The collection of ICliqueMatrix instances which are of maximal size and include every member of the ICliqueMatrix subnetwork instance.</returns>
        public static IEnumerable<ICliqueMatrix> FindLargestCliqueInvolvingSubnetwork(INetworkMatrix socialNetworkMatrix, ICliqueMatrix subnetwork)
        {
            var superNetworkCondition = new Predicate<ICliqueMatrix>( possibleSuperNetwork => possibleSuperNetwork.ContainsClique(subnetwork) );
            var searchResults = PredicatedBinarySearch(socialNetworkMatrix, 1, socialNetworkMatrix.NodeCount, superNetworkCondition);

            return searchResults != null
                ? searchResults.Solutions
                : Enumerable.Empty<ICliqueMatrix>();
        }
    }
}
