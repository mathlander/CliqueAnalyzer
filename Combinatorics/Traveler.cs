using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CliqueAnalyzer.Combinatorics
{
    internal class Traveler : ITraveler
    {
        public Traveler(int originalValue, int maxValue)
        {
            OriginalValue = originalValue;
            CurrentValue = originalValue;
            TroupeValue = originalValue;
            MaxValue = maxValue;
        }

        public int CurrentValue
        {
            get;
            private set;
        }

        public int OriginalValue
        {
            get;
            private set;
        }

        public int TroupeValue
        {
            get;
            set;
        }

        public int MaxValue
        {
            get;
            private set;
        }

        public bool Shift()
        {
            CurrentValue++;

            return CurrentValue <= MaxValue;
        }

        public bool TroupeShift()
        {
            TroupeValue++;
            CurrentValue = TroupeValue;

            return CurrentValue <= MaxValue;
        }

        public void Reset()
        {
            CurrentValue = OriginalValue;
        }

        public void TroupeReset()
        {
            CurrentValue = TroupeValue;
        }

        public void AlignToTraveler(ITraveler indexTraveler, int offset)
        {
            CurrentValue = indexTraveler.CurrentValue + offset;
            TroupeValue = CurrentValue;
        }

        public IEnumerable<int> VisitPositions()
        {
            do
            {
                yield return CurrentValue;
                CurrentValue++;
            } while (CurrentValue <= MaxValue);
        }
    }
}
