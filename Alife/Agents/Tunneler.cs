using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using Alife.AlifeMaps;

namespace Alife.Agents
{
    /**
     * <summary>
     * Basic agent that digs tunnels.
     * <author>1upD</author>
     * </summary>
     */
    [Serializable]
    public class Tunneler : BaseAgent
    {
        public int X;
        public int Y;
        public int Z;
        public int Width;
        public int Height;
        public int Lifetime;
        public int MaxLifetime;
        public float ProbReproduce;
        public float ProbTurn;
        public float ProbAscend;
        public float ProbSpawnRoomer;
        public int MaxHeightDecayRate;
        public int MinHeightDecayRate;
        public int MaxWidthDecayRate;
        public int MinWidthDecayRate;
        public string Style;
        public AlifeDirection Direction;
        public bool SpawnRoomerOnDeath;
        public int LifetimeDecayRate;

        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public Tunneler()
        {
            this.X = 0;
            this.Y = 0;
            this.Z = 0;
            this.Width = 1;
            this.Height = 2;
            this.Lifetime = 1;
            this.MaxLifetime = 1;
            this.ProbReproduce = 0.0f;
            this.ProbAscend = 0.0f;
            this.ProbTurn = 0.0f;
            this.Direction = 0.0f;
            this.Style = "";
            this.MaxHeightDecayRate = 0;
            this.MinHeightDecayRate = 0;
            this.MaxWidthDecayRate = 0;
            this.MinWidthDecayRate = 0;
            this.ProbSpawnRoomer = 0.0f;
            this.SpawnRoomerOnDeath = true;
            this.LifetimeDecayRate = 1;
        }

        /**
         * <summary>
         * Constructor takes a lot of arguments.
         * <author>1upD</author>
         * </summary>
         */
        public Tunneler(string style = "", int x = 0, int y = 0, int z = 0,
            int width = 1, int height = 2,
            int lifetime = 1, int maxLifetime = 1,
            float probReproduce = 0.0f, float probTurn = 0.0f, float probAscend = 0.0f,
            AlifeDirection direction = AlifeDirection.East, int MaxHeighDecayRate = 0, int MinHeightDecayRate = 0, int MaxWidthDecayRate = 0, int MinWidthDecayRate = 0, float ProbSpawnRoomer = 0.0f, bool spawnRoomerOnDeath = true, int LifeTimeDecayRate = 1)
        {
            this.X = x;
            this.Y = y;
            this.Z = z;
            this.Width = width;
            this.Height = height;
            this.Lifetime = lifetime;
            this.MaxLifetime = maxLifetime;
            this.ProbReproduce = probReproduce;
            this.ProbAscend = probAscend;
            this.ProbTurn = probTurn;
            this.Direction = direction;
            this.Style = style;
            this.ProbSpawnRoomer = ProbSpawnRoomer;
            this.SpawnRoomerOnDeath = spawnRoomerOnDeath;
            this.LifetimeDecayRate = 1;

            log.Debug(string.Format("Tunneler spawned at {0}, {1}, {2}.", this.X, this.Y, this.Z));
        }

        public new void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            throw new NotImplementedException();
        }

        /**
         * <summary>
         * Gets the remaining number of turns. Useful if this agent needs to be culled.
         * <author>1upD</author>
         * </summary>
         */
        public override int GetTurnsLeft()
        {
            return this.Lifetime;
        }

        /**
         * <summary>
         * Step function for tunneler. Uses RNG to choose whether to turn or reproduce, then moves forward in a direction. Marks all occupied spaces in the provided map.
         * <author>1upD</author>
         * </summary>
         *
         */
        public override void Step(AlifeMap map)
        {
            try {
            // Check if agent is still alive
            if (this.Lifetime > 0)
            {
                int seed = this.X + this.Y + this.Z + (int)this.Direction + this.Height + this.Width + (int)System.DateTime.Now.Ticks;

                // Get random number
                Random random = new Random(seed);
                double sample = random.NextDouble();

                // Check turn probability. If turning, change direction 90 degrees
                if (sample < this.ProbTurn)
                {
                    sample = random.NextDouble();
                    int polarity = sample > 0.5 ? 1 : -1;
                    this.Direction = AlifeDirectionOperations.Add(this.Direction, polarity);
                }

                // Get new random seed
                sample = random.NextDouble();

                // Check reproduction probability
                if (sample < this.ProbReproduce)
                {
                    sample = random.NextDouble();
                    int polarity = sample > 0.5 ? 1 : -1;
                    AlifeDirection childDirection = AlifeDirectionOperations.Add(this.Direction, polarity);
                    int widthDecay = random.Next(this.MinWidthDecayRate, this.MaxWidthDecayRate);
                    int heightDecay = random.Next(this.MinHeightDecayRate, this.MaxHeightDecayRate);
                    Tunneler child = new Tunneler(this.Style, this.X, this.Y, this.Z, this.Width - widthDecay, this.Height - heightDecay, this.MaxLifetime - this.LifetimeDecayRate, this.MaxLifetime - this.LifetimeDecayRate, this.ProbReproduce, this.ProbTurn, this.ProbAscend, childDirection);
                    map.Agents.Add(child);
                }
                else
                {
                    sample = random.NextDouble();
                    if (sample < this.ProbSpawnRoomer)
                    {
                        Roomer child = new Roomer(x: this.X, y: this.Y, z: this.Z, style: this.Style, height: Math.Max(this.Height, 2), maxWidth: Math.Min(this.Width * 2, 3), mustDeploy: false);
                        map.Agents.Add(child);
                        }
                }

                // Get new random seed
                sample = random.NextDouble();

                // Check a s c e n d probability
                if (sample < this.ProbAscend)
                {
                    sample = random.NextDouble();
                    int polarity = sample > 0.5 ? this.Height : -this.Height;
                    this.Z += polarity;
                }
                else
                {
                    // Update location
                    switch (this.Direction)
                    {
                        case AlifeDirection.East:
                            this.X++;
                            break;
                        case AlifeDirection.North:
                            this.Y++;
                            break;
                        case AlifeDirection.West:
                            this.X--;
                            break;
                        case AlifeDirection.South:
                            this.Y--;
                            break;
                        case AlifeDirection.Up:
                            this.Z++;
                            break;
                        case AlifeDirection.Down:
                            this.Z--;
                            break;
                    }

                }


                // Mark location
                // Nasty nested four loop to handle the added spaces from the height and width
                bool vertical = this.Direction == AlifeDirection.North || this.Direction == AlifeDirection.South;
                for (int x = this.X; x <= (vertical ? this.X + this.Width : this.X); x++)
                {
                    for (int y = this.Y; y <= (vertical ? this.Y : this.Y + this.Width); y++)
                    {
                        for (int z = this.Z; z <= this.Z + this.Height; z++)
                        {
                            map.MarkLocation(this.Style, x, y, z);
                        }
                    }
                }

            }
            else if (this.SpawnRoomerOnDeath)
            {
                    log.Debug(string.Format("Tunneler died at {0}, {1}, {2}.", this.X, this.Y, this.Z));

                    // Add a roomer
                    Roomer child = new Roomer(x: this.X, y: this.Y, z: this.Z, style: this.Style, height: Math.Max(this.Height, 2), maxWidth: Math.Min(this.Width * 2, 3));
                    map.Agents.Add(child);
                

                }

            this.Lifetime--;
            }
            catch (Exception e){
                log.Error("Error in Tunneler Step function: ", e);
            }
        }
    }
}
