using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CliqueAnalyzer.Combinatorics
{
    public interface ICombinationGenerator
    {
        /// <summary>
        /// This represents the size of the master set.  For n choose q, this value is equal to n.
        /// </summary>
        int MasterSetSize { get; }

        /// <summary>
        /// This represents the number of items selected without replacement.  For n choose q, this value is equal to q.
        /// </summary>
        int SelectionCount { get; }
        
        /// <summary>
        /// Iterates over the distinct sets of size N.
        /// </summary>
        IEnumerable<IEnumerable<int>> CreateCombinationPump();
    }
}
