using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CliqueAnalyzer.SocialNetwork
{
    public class NetworkMatrixException : Exception { public NetworkMatrixException(string message) : base(message) { } }

    internal class NetworkMatrix : INetworkMatrix
    {
        private const string IncompatibleMatrixMessage = "Matrices being compared must be of the same size.";
        private readonly SortedDictionary<int, INetworkNode> _nodes;

        public NetworkMatrix(IEnumerable<INetworkNode> nodes)
        {
            _nodes = new SortedDictionary<int, INetworkNode>();

            foreach (var node in nodes)
                _nodes.Add(node.NodeId, node);
        }

        public int NodeCount
        {
            get { return _nodes.Count; }
        }

        public IEnumerable<INetworkNode> Nodes
        {
            get { return _nodes.Values; }
        }

        public bool NodesRelate(int a, int b)
        {
            return _nodes[a].IsRelatedToNode(b);
        }

        public INetworkNode GetNodeById(int id)
        {
            return _nodes[id];
        }

        public IEnumerable<IList<long>> AsBitMask()
        {
            foreach (var node in Nodes)
                yield return node.AsBitMask();
        }

        private IEnumerable<INetworkNode> GetSubmatrix(IEnumerable<int> nodeIds)
        {
            foreach (var member in nodeIds)
                yield return _nodes[member];
        }

        public bool ContainsClique(ICliqueMatrix cliqueMatrix)
        {
            if (cliqueMatrix.NodeCount != NodeCount)
                throw new NetworkMatrixException(IncompatibleMatrixMessage);

            var submatrix = GetSubmatrix(cliqueMatrix.MemberNodeIds);
            var cliqueRepBitString = cliqueMatrix.RepresentativeNode.AsBitMask();

            foreach (var matrixRow in submatrix)
            {
                var cliqueEnumerator = cliqueRepBitString.GetEnumerator();
                cliqueEnumerator.MoveNext();
                long cliqueBitString;

                foreach (var rowBitString in matrixRow.AsBitMask())
                {
                    cliqueBitString = cliqueEnumerator.Current;

                    if ((cliqueBitString & rowBitString) != cliqueBitString)
                        return false;

                    cliqueEnumerator.MoveNext();
                }
            }

            return true;
        }

        public override string ToString()
        {
            return String.Join(Environment.NewLine, Nodes.Select(node => node.ToString()));
        }
    }
}
