using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CliqueAnalyzer.SocialNetwork
{
    public class PredicatedCliqueMementoException : Exception { public PredicatedCliqueMementoException(string message) : base(message) { } }

    internal class PredicatedCliqueMemento : ICliqueMemento
    {
        private const string FirstCliqueMustNotBeNull = "The first clique must not be null.";
        private readonly ICliqueMatrix _firstClique;
        private readonly IEnumerator<ICliqueMatrix> _solutions;
        private readonly Predicate<ICliqueMatrix> _predicate;

        public PredicatedCliqueMemento(ICliqueMatrix firstClique, IEnumerator<ICliqueMatrix> solutions, Predicate<ICliqueMatrix> predicate)
        {
            if (firstClique == null)
                throw new PredicatedCliqueMementoException(FirstCliqueMustNotBeNull);

            _firstClique = firstClique;
            _solutions = solutions;
            _predicate = predicate;
        }

        public IEnumerable<ICliqueMatrix> Solutions
        {
            get
            {
                yield return _firstClique;

                while (_solutions.MoveNext())
                {
                    if (_predicate(_solutions.Current))
                        yield return _solutions.Current;
                }
            }
        }
    }
}
