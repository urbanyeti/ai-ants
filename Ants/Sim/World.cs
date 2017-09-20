using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ants.Sim
{

    public class World
    {
        public List<ChemTrail> Trails { get; set; }

        public World()
        {
            Trails = new List<ChemTrail>();
        }

        public ChemTrail AddTrail(ChemTrail trail)
        {
            ChemTrail oldTrail = this.Trails.FirstOrDefault(t => t.X == trail.X && t.Y == trail.Y);
            if (oldTrail != null)
            {
                oldTrail.Level += Math.Min(trail.Level,255);
                return oldTrail;
            }
            else
            {
                Trails.Add(trail);
                return trail; 
            }
        }
    }
}
