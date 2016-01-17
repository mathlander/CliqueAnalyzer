using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CliqueAnalyzer.SocialNetwork
{
    internal class NetworkNodeComparer : IComparer<INetworkNode>
    {
        public int Compare(INetworkNode x, INetworkNode y)
        {
            return x.NodeId.CompareTo(y.NodeId);
        }
    }
}
