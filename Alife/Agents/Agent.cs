using Alife.AlifeMaps;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Alife.Agents
{
    public interface Agent : ISerializable
    {
        void Step(AlifeMap map);

        int GetTurnsLeft();

    }
}
