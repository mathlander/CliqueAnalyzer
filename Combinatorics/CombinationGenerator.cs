using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CliqueAnalyzer.Combinatorics
{
    public class CombinationGeneratorException : Exception { public CombinationGeneratorException(string message) : base(message) { } }

    public class CombinationGenerator : ICombinationGenerator
    {
        private const string MasterSetSizeMustBeLarger = "The selectionSize parameter must be no larger than the masterSetSize.";
        private const string MasterSetSizeMustBeGreaterThanZero = "The masterSetSize parameter must be greater than zero.";
        private const string SelectionSizeMustBeGreaterThanZero = "The selectionSize parameter must be greater than zero.";

        private readonly ITroupe _masterTroupe;

        public CombinationGenerator(int masterSetSize, int selectionSize)
        {
            if (selectionSize > masterSetSize)
                throw new CombinationGeneratorException(MasterSetSizeMustBeLarger);
            else if (masterSetSize < 1)
                throw new CombinationGeneratorException(MasterSetSizeMustBeGreaterThanZero);
            else if (selectionSize < 1)
                throw new CombinationGeneratorException(SelectionSizeMustBeGreaterThanZero);

            MasterSetSize = masterSetSize;
            SelectionCount = selectionSize;

            // generate the collection of travelers with values in the range [1, MasterSetSize]
            ITroupe currentTroupe = null;
            ITroupe previousTroupe = null;

            for (var i = SelectionCount; i > 0; i--)
            {
                if (i == SelectionCount)
                    currentTroupe = new SingletonTroupe(new Traveler(i, MasterSetSize));
                else
                    currentTroupe = new Troupe(new Traveler(i, MasterSetSize), previousTroupe, (SelectionCount-i)+1);

                previousTroupe = currentTroupe;
            }

            _masterTroupe = currentTroupe;
        }

        public int MasterSetSize
        {
            get;
            private set;
        }

        public int SelectionCount
        {
            get;
            private set;
        }

        public IEnumerable<IEnumerable<int>> CreateCombinationPump()
        {
            return _masterTroupe.VisitPositions();
        }
    }
}
