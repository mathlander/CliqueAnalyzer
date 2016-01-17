using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CliqueAnalyzer.Combinatorics
{
    internal class Troupe : ITroupe
    {
        private readonly ITraveler _leftMostTraveler;
        private readonly ITroupe _childTroupe;
        private readonly int _travelerCount;

        public Troupe(ITraveler leftMostTraveler, ITroupe childTroupe, int travelerCount)
        {
            _leftMostTraveler = leftMostTraveler;
            _childTroupe = childTroupe;
            _travelerCount = travelerCount;
        }

        public IEnumerable<ITraveler> Travelers
        {
            get { return Enumerable.Repeat<ITraveler>(_leftMostTraveler, 1)
                            .Union<ITraveler>(_childTroupe.Travelers); }
        }

        public int TroupeSize
        {
            get { return _travelerCount; }
        }

        public bool ShiftTroupe()
        {
            _leftMostTraveler.TroupeShift();
            return _childTroupe.ShiftTroupe();
        }

        public void ResetTroupe()
        {
            _leftMostTraveler.TroupeReset();
            _childTroupe.ResetTroupe();
        }

        public void AlignToTraveler(ITraveler indexTraveler, int offset)
        {
            _leftMostTraveler.AlignToTraveler(indexTraveler, offset);
            _childTroupe.AlignToTraveler(indexTraveler, (offset + 1));
        }

        public IEnumerable<IEnumerable<int>> VisitPositions()
        {
            do
            {
                foreach (var troupeCombo in _childTroupe.VisitPositions())
                {
                    // union the two sequences into a single enumerable
                    // using enumerables, rather than lists, has the advantage of pipelined evaluation
                    IEnumerable<int> combo = Enumerable.Repeat<int>(_leftMostTraveler.CurrentValue, 1)
                                                        .Union<int>(troupeCombo);

                    yield return combo;
                }

                _childTroupe.AlignToTraveler(_leftMostTraveler, 1);
            } while (ShiftTroupe());
        }
    }
}
