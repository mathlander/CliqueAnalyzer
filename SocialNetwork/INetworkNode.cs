using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CliqueAnalyzer.SocialNetwork
{
    /// <summary>
    /// Represents a node in a network and a row in an INetworkMatrix instance.
    /// </summary>
    public interface INetworkNode
    {
        /// <summary>
        /// The unique identifier for the network node.
        /// </summary>
        int NodeId { get; }

        /// <summary>
        /// Indicates whether or not the current node relates to comparisonNode.
        /// </summary>
        /// <param name="comparisonNode">The test node.</param>
        /// <returns>True if there is a relationship between the current node and comparisonNode.</returns>
        bool IsRelatedToNode(int comparisonNode);

        /// <summary>
        /// Converts the network relationships into a long array.
        /// </summary>
        /// <returns>A list of longs indicating the given node's relationship to other nodes in the network,
        /// where each set bit indicates a relationship to the node in that bit's position (plus 1), and
        /// each unset bit indicates that there is no relationship.</returns>
        IList<long> AsBitMask();
    }
}
