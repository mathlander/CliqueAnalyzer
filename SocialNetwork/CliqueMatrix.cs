using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CliqueAnalyzer.SocialNetwork
{
    internal class CliqueMatrix : NetworkMatrix, ICliqueMatrix
    {
        private readonly SortedSet<int> _members;
        private readonly INetworkNode _representativeNode;

        public CliqueMatrix(IEnumerable<INetworkNode> nodes, IEnumerable<int> members)
            : base(nodes)
        {
            _members = new SortedSet<int>(members);
            _representativeNode = base.GetNodeById(_members.First());
        }

        public int SizeOfClique
        {
            get { return _members.Count; }
        }

        public INetworkNode RepresentativeNode
        {
            get { return _representativeNode; }
        }

        public IEnumerable<int> MemberNodeIds
        {
            get { return _members; }
        }
    }
}
