using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CliqueAnalyzer.SocialNetwork
{
    internal class ModifiableMatrix : NetworkMatrix, IModifiableMatrix
    {
        public ModifiableMatrix(IEnumerable<IModifiableNode> nodes) : base(nodes)
        {
        }

        public void RelateNodes(int firstId, int secondId)
        {
            var firstNode = base.GetNodeById(firstId) as IModifiableNode;
            var secondNode = base.GetNodeById(secondId) as IModifiableNode;

            firstNode.RelateToNode(secondId);
            secondNode.RelateToNode(firstId);
        }
    }
}
