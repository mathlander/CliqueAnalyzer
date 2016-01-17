using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CliqueAnalyzer.SocialNetwork
{
    /// <summary>
    /// Holds the first solution and the enumerator for cliques of a given size.
    /// </summary>
    internal interface ICliqueMemento
    {
        /// <summary>
        /// The collection of solution cliques of a given size.
        /// </summary>
        IEnumerable<ICliqueMatrix> Solutions { get; }
    }
}
