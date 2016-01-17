using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CliqueAnalyzer.SocialNetwork;

namespace CliqueAnalyzer
{
    public class NetworkMatrixFactoryException : Exception { public NetworkMatrixFactoryException(string message) : base(message) { } }

    /// <summary>
    /// A static class to generate an INetworkMatrix instance from a CSV file.
    /// </summary>
    public static class NetworkMatrixFactory
    {
        private const string FileMustExistMessage = "There is no CSV file at the specified location '{0}'";

        /// <summary>
        /// Takes a file path to a CSV file and returns an instance of INetworkMatrix.  The first
        /// line of the CSV file should be an integer indicating the number of nodes in the network.
        /// Each line in lines 2 through (n+1) should be a series 'n' of comma separated 1's or 0's.
        /// </summary>
        /// <param name="filePath">Full file path to the CSV file.</param>
        /// <returns>An instance of INetworkMatrix.</returns>
        public static INetworkMatrix CreateNetworkMatrix(string filePath)
        {
            if (!File.Exists(filePath))
                throw new NetworkMatrixFactoryException(String.Format(FileMustExistMessage, filePath));

            var lines = new List<string>(File.ReadAllLines(filePath));
            var nodeCount = int.Parse(lines[0]);
            var nodes = new List<INetworkNode>(nodeCount);
            //var relationships = new List<IEnumerable<int>>(nodeCount);

            for (var nodeId = 1; nodeId <= nodeCount; nodeId++)
            {
                var line = lines[nodeId];
                nodes.Add(new NetworkNode(
                    nodeId,
                    nodeCount,
                    line.Split(',')
                    .Select<string, int>((item, index) => item == "0" ? 0 : (index+1))
                    .Where<int>(val => val != 0)));
            }

            return new NetworkMatrix(nodes);
        }

        internal static ICliqueMatrix CreateCliqueMatrix(IEnumerable<int> members, int networkSize)
        {
            var memberList = new HashSet<int>(members);
            var nodes = Enumerable.Range(1, networkSize)
                .Select<int, INetworkNode>(
                    nodeId =>
                        new NetworkNode(
                            nodeId,
                            networkSize,
                            memberList.Contains(nodeId) ? memberList : Enumerable.Empty<int>()));

            return new CliqueMatrix(nodes, memberList);
        }
    }
}
