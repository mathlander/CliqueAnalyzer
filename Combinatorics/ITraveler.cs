using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CliqueAnalyzer.Combinatorics
{
    internal interface ITraveler
    {
        /// <summary>
        /// The current position of the ITraveler in the range of [1, MaxValue].
        /// </summary>
        int CurrentValue { get; }

        /// <summary>
        /// The original position of the ITraveler in the range of [1, MaxValue].
        /// </summary>
        int OriginalValue { get; }

        /// <summary>
        /// The position of the ITraveler as of the most recent TroupeShift(), in the range of [OriginalValue, MaxValue].
        /// </summary>
        int TroupeValue { get; set; }

        /// <summary>
        /// The maximum position that any ITraveler may visit.  For "n choose q", this value represents 'n'.
        /// </summary>
        int MaxValue { get; }

        /// <summary>
        /// Increases the CurrentValue of the traveler by one.
        /// </summary>
        /// <returns>True when the traveler is able to increment without exceeding MaxValue.  False otherwise.</returns>
        bool Shift();

        /// <summary>
        /// Increases the TroupeValue of the traveler by one and assigns TroupeValue to CurrentValue.
        /// </summary>
        /// <returns>True when the traveler is able to increment without exceeding MaxValue.  False otherwise.</returns>
        bool TroupeShift();

        /// <summary>
        /// Sets CurrentValue = OriginalValue.
        /// </summary>
        void Reset();

        /// <summary>
        /// Sets CurrentValue = TroupeValue.
        /// </summary>
        void TroupeReset();

        /// <summary>
        /// Sets the CurrentValue = (indexTraveler.CurrentValue + offset).
        /// </summary>
        /// <param name="indexTraveler">Traveler by which all travelers to the right are aligned.</param>
        /// <param name="offset">Distance from the indexTraveler.</param>
        void AlignToTraveler(ITraveler indexTraveler, int offset);

        /// <summary>
        /// Visits each number from TroupeValue through MaxValue.
        /// </summary>
        /// <returns>A collection of integers ranging from TroupeValue through MaxValue.</returns>
        IEnumerable<int> VisitPositions();
    }
}
