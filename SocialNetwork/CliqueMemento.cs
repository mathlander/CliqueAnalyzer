using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CliqueAnalyzer.SocialNetwork
{
    public class CliqueMementoException : Exception { public CliqueMementoException(string message) : base(message) { } }

    internal class CliqueMemento : ICliqueMemento
    {
        private const string FirstCliqueMustNotBeNull = "The first clique must not be null.";
        private readonly ICliqueMatrix _firstClique;
        private readonly IEnumerator<ICliqueMatrix> _solutions;

        public CliqueMemento(ICliqueMatrix firstClique, IEnumerator<ICliqueMatrix> solutions)
        {
            if (firstClique == null)
                throw new Exception(FirstCliqueMustNotBeNull);

            _firstClique = firstClique;
            _solutions = solutions;
        }

        public IEnumerable<ICliqueMatrix> Solutions
        {
            get
            {
                yield return _firstClique;

                while (_solutions.MoveNext())
                    yield return _solutions.Current;
            }
        }
    }
}
