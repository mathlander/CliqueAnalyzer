using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CliqueAnalyzer.Combinatorics
{
    public class SingletonTroupeException : Exception { public SingletonTroupeException(string message) : base(message) { } }

    internal class SingletonTroupe : ITroupe
    {
        private const string MaxPositionExceeded = "The singleton traveler has exceeded the maximum value.";
        private ITraveler _traveler;

        public SingletonTroupe(ITraveler loneTraveler)
        {
            _traveler = loneTraveler;
        }

        public int TroupeSize
        {
            get;
            private set;
        }

        public IEnumerable<ITraveler> Travelers
        {
            get { yield return _traveler; }
        }

        public void ResetTroupe()
        {
            _traveler.TroupeReset();
        }

        public void AlignToTraveler(ITraveler indexTraveler, int offset)
        {
            _traveler.AlignToTraveler(indexTraveler, offset);
        }

        public bool ShiftTroupe()
        {
            return _traveler.TroupeShift();
        }

        public IEnumerable<IEnumerable<int>> VisitPositions()
        {
            if (_traveler.CurrentValue > _traveler.MaxValue)
                throw new SingletonTroupeException(MaxPositionExceeded);

            do
            {
                yield return Enumerable.Repeat<int>(_traveler.CurrentValue, 1);
            } while (_traveler.Shift());
        }
    }
}
