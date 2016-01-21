using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CliqueAnalyzer.SocialNetwork
{
    internal class NetworkNode : INetworkNode, IModifiableNode
    {
        private const long Zero = 0;

        private readonly int _nodeId;
        private readonly int _networkSize;
        private readonly SortedSet<int> _relatedNodes;
        private readonly int _sizeOfInt = sizeof(int);

        public NetworkNode(int id, int networkSize, IEnumerable<int> relatedNodes)
        {
            _nodeId = id;
            _networkSize = networkSize;
            _relatedNodes = new SortedSet<int>(relatedNodes);

            // always ensure that the reflexive property is satisfied
            _relatedNodes.Add(_nodeId);
        }

        public int NodeId
        {
            get { return _nodeId; }
        }

        public int NetworkSize
        {
            get { return _networkSize; }
        }

        public IEnumerable<int> RelatedNodes
        {
            get { return _relatedNodes; }
        }

        public void RelateToNode(int nodeId)
        {
            _relatedNodes.Add(nodeId);
        }

        public bool IsRelatedToNode(int comparisonNode)
        {
            return _relatedNodes.Contains(comparisonNode);
        }

        public IList<long> AsBitMask()
        {
            var numberOflongs = (int)Math.Ceiling((double)_networkSize / 8);
            var bitMask = new List<long>(numberOflongs);
            int longIdx;
            int shiftIdx;
            long flag = 0;

            foreach(var num in Enumerable.Range(0, NetworkSize - 1))
            {
                longIdx = (int) (num / 8);
                shiftIdx = num % 8;
                flag = IsRelatedToNode(num + 1)
                    ? (long) (1 << shiftIdx)
                    : Zero;

                if (longIdx < bitMask.Count)
                    bitMask[longIdx] |= flag;
                else
                    bitMask.Add(flag);
            }

            return bitMask;
        }

        public override string ToString()
        {
            return String.Join(",", Enumerable.Range(1, NetworkSize).Select(nodeId => IsRelatedToNode(nodeId) ? 1 : 0));
        }
    }
}
