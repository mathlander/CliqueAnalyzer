using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CliqueAnalyzer.SocialNetwork
{
    /// <summary>
    /// Represents a special type of network matrix where a transitive property is obeyed.  That is,
    /// if node 'a' relates to node 'b', and 'b' relates to 'c', the 'a' relates to 'c'.
    /// </summary>
    public interface ICliqueMatrix : INetworkMatrix
    {
        /// <summary>
        /// The number of members included in the clique.
        /// </summary>
        int SizeOfClique { get; }

        /// <summary>
        /// The first member of the clique.
        /// 
        /// Note that the test foo.IsRelatedToNode(bar) returns true if and only if foo and bar are
        /// both included in the clique.  In particular, foo may be any member of the clique and may
        /// be used to test inclusion against any other network node.
        /// </summary>
        INetworkNode RepresentativeNode { get; }

        /// <summary>
        /// The collection of node ID's which define the current clique.
        /// </summary>
        IEnumerable<int> MemberNodeIds { get; }
    }
}
