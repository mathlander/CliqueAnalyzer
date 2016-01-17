using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CliqueAnalyzer.Combinatorics
{
    internal interface ITroupe
    {
        /// <summary>
        /// Number of troupe members.
        /// </summary>
        int TroupeSize { get; }

        /// <summary>
        /// Members of the troupe.
        /// </summary>
        IEnumerable<ITraveler> Travelers { get; }

        /// <summary>
        /// Sets CurrentValue = OriginalValue for all troupe members.
        /// </summary>
        void ResetTroupe();

        /// <summary>
        /// Sets the CurrentValue = (indexTraveler.CurrentValue + offset).
        /// </summary>
        /// <param name="indexTraveler">Traveler by which all travelers to the right are aligned.</param>
        /// <param name="offset">Distance from the indexTraveler.</param>
        void AlignToTraveler(ITraveler indexTraveler, int offset);

        /// <summary>
        /// Increments the CurrentValue for every member of the troupe by one.
        /// </summary>
        /// <returns>True if no troupe members have exceeded MaxValue.  False otherwise.</returns>
        bool ShiftTroupe();

        /// <summary>
        /// Visits each combination for the set of travelers.
        /// </summary>
        /// <returns>A collection of sets, each of size TroupeSize</returns>
        IEnumerable<IEnumerable<int>> VisitPositions();
    }
}
