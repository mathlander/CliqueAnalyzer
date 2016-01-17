using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CliqueAnalyzer.SocialNetwork
{
    /// <summary>
    /// Represents the network of nodes as a matrix.
    /// </summary>
    public interface INetworkMatrix
    {
        /// <summary>
        /// Number of nodes in the network.
        /// </summary>
        int NodeCount { get; }

        /// <summary>
        /// The collection of network nodes.
        /// </summary>
        IEnumerable<INetworkNode> Nodes { get; }

        /// <summary>
        /// Indicates whether or not a ~ b.
        /// </summary>
        /// <param name="a">The first node in the comparison.</param>
        /// <param name="b">The second node in the comparison.</param>
        /// <returns>True when a ~ b, and by symmetry when b ~ a.</returns>
        bool NodesRelate(int a, int b);

        /// <summary>
        /// Get the network node with the specified ID.
        /// </summary>
        /// <param name="id">The desired node's ID.</param>
        /// <returns>An INetworkNode instance with the given ID.</returns>
        INetworkNode GetNodeById(int id);

        /// <summary>
        /// Converts the INetworkNode collection into a series of IList instances
        /// where the list is a bit mask representing node relationships.
        /// </summary>
        /// <returns></returns>
        IEnumerable<IList<long>> AsBitMask();

        /// <summary>
        /// Tests whether or not the given ICliqueMatrix is present as a subnetwork
        /// of the INetworkMatrix.
        /// </summary>
        /// <param name="cliqueMatrix">The proposed subnetwork.</param>
        /// <returns>True when the ICliqueMatrix represents a subnetwork of the INetworkMatrix.</returns>
        bool ContainsClique(ICliqueMatrix cliqueMatrix);
    }
}
