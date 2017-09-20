using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Ants.Sim
{
    public class Ant
    {
        public int X { get; set; }
        public int Y { get; set; }
        public int Speed { get; set; }
        public int Wildness { get; set; }
        public Random Rand { get; set; }
        public Label InfoLabel { get; set; }
        public Color Color { get; set; }
        public World World { get; set; }


        public Ant(int x, int y, int speed, int seed, World world, ref Label label, int wild = 10)
        {
            this.X = x;
            this.Y = y;
            this.Speed = speed;
            this.InfoLabel = label;
            this.Rand = new Random(seed);
            //this.Rand = new Random(seed+DateTime.Now.Millisecond);
            this.Color = Helper.GetRandomColor(this.Rand);
            this.InfoLabel.ForeColor = this.Color;
            this.World = world;
            this.Wildness = wild;
        }

        public bool WildCheck()
        {
            return Rand.Next(21) < Wildness;
        }

        public void Move()
        {
            if (WildCheck())
            {
                MoveRandomly();
            }
            else
            {
                FollowTrails();
            }

            //InfoLabel.Text = String.Format("X: {0} | Y: {1}", this.X, this.Y);
        }

        public void FollowTrails()
        {
            //MoveRandomly();
            //this.Y -= this.Speed;

            // 1. Get all ChemTrail spots within Speed of current location
            List<ChemTrail> closeTrails = World.Trails.Where(t => Math.Abs(X - t.X) <= this.Speed && Math.Abs(Y - t.Y) <= this.Speed).OrderByDescending(t => t.Level).ToList();

            if (closeTrails.Count() > 0)
            {
                // 2. Roll D20, if higher than Wildness, go with 1st ChemTrail, otherwise go with random
                if (WildCheck())
                {
                    int randTrail = Rand.Next(closeTrails.Count());
                    this.X = closeTrails[randTrail].X;
                    this.Y = closeTrails[randTrail].Y;
                }
                else
                {
                    this.X = closeTrails.First().X;
                    this.Y = closeTrails.First().Y;
                }
            }
            else
                MoveRandomly();
        }

        public void MoveRandomly()
        {
            this.X += Rand.Next((this.Speed * 2) + 1) - (this.Speed);
            this.Y += Rand.Next((this.Speed * 2) + 1) - (this.Speed);
        }

    }
}
