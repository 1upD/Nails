using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using Alife.AlifeMaps;

namespace Alife.Agents
{
    [Serializable]
    [XmlInclude(typeof(Tunneler))]
    [XmlInclude(typeof(Roomer))]
    public abstract class BaseAgent : Agent
    {
        // For serializer
        public BaseAgent()
        {

        }

        public virtual void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            throw new NotImplementedException();
        }

        public abstract int GetTurnsLeft();

        public abstract void Step(AlifeMap map);
    }
}
