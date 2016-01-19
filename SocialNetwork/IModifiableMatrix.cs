using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CliqueAnalyzer.SocialNetwork
{
    /// <summary>
    /// An extension of INetworkMatrix which allows for the matrix to be modified programatically.
    /// </summary>
    public interface IModifiableMatrix : INetworkMatrix
    {
        /// <summary>
        /// Indicates that the first node relates to the second node.
        /// </summary>
        /// <param name="firstId">The first node ID in the symmetric relationship.</param>
        /// <param name="secondId">The second node ID in the symmetric relationship.</param>
        void RelateNodes(int firstId, int secondId);
    }
}
