using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ants
{
    public static class Helper
    {
        public static Color GetRandomColor(Random rand)
        {
            return Color.FromArgb(50, rand.Next(0, 255), rand.Next(0, 255), rand.Next(0, 255));
        }
    }

    public class ChemTrail : IEquatable<ChemTrail>
    {
        public int X { get; set; }
        public int Y { get; set; }
        public int Level { get; set; }

        public ChemTrail(int x, int y, int level = 10)
        {
            this.X = x;
            this.Y = y;
            this.Level = level;
        }

        public override bool Equals(object obj)
        {
            if (!(obj is ChemTrail))
                return false;

            return this.Equals((ChemTrail)obj);
        }

        public bool Equals(ChemTrail trail)
        {
            return (this.X == trail.X && this.Y == trail.Y);
        }

        public Color Color
        {
            get
            {
                return Color.FromArgb(this.Level, 0, 0, 255);
            }
        }
    }
}
