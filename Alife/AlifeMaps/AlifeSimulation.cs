using Alife.Agents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alife.AlifeMaps
{
    public static class AlifeSimulation
    {
        /**
         * Static method to run a simulation on an Alife map populated with agents. Agents should be supplied beforehand.
         * <author>1upD</author>
         */
        public static void Simulate(ref AlifeMap map, int lifetime)
        {
            for(int i = 0; i < lifetime; i++)
            {
                int agentsCount = map.Agents.Count;

                // If all life is dead, end the simulation
                if(agentsCount < 1)
                {
                    return;
                }

                for(int j = 0; j < agentsCount; j++)
                {
                    Agent agent = map.Agents[j];
                    if(agent.GetTurnsLeft() > 0)
                    {
                        agent.Step(map);

                    } else
                    {
                        map.Agents.RemoveAt(j--);
                    }

                    agentsCount = map.Agents.Count;
                }
            }
        }
    }
}
