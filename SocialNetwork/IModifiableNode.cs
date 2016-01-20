using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CliqueAnalyzer.SocialNetwork
{
    /// <summary>
    /// An extension of INetworkNode which allows for the node to be modified programatically.
    /// </summary>
    internal interface IModifiableNode : INetworkNode
    {
        /// <summary>
        /// Defines a relationship with the given node ID.
        /// </summary>
        /// <param name="nodeId">The node ID to which the current node is related.</param>
        void RelateToNode(int nodeId);
    }
}
