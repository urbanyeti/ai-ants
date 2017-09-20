using Ants.Sim;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using TinyMessenger;

namespace Ants
{
    public partial class Form1 : Form
    {
        public Clock C { get; set; }
        public List<Ant> Ants { get; set; }
        public Random Rand { get; set; }
        public World W { get; set; }

        private System.Drawing.Graphics g;
        private System.Drawing.Pen pen1 = new System.Drawing.Pen(Color.Blue, 2F);
        private System.Drawing.Pen pen2 = new System.Drawing.Pen(Color.Red, 2F);

        TinyMessengerHub MessageHub { get; set; }
        public Form1()
        {
            InitializeComponent();
            W = new World();
            C = new Clock(100);
            C.Worker.ProgressChanged += ClockProgressChanged;
            C.Worker.RunWorkerCompleted += ClockCompleted;

            g = pictureBox1.CreateGraphics();
            MessageHub = new TinyMessengerHub();
            this.Rand = new Random();

            W.Trails.Add(new ChemTrail(200, 10, 800));

            this.Ants = new List<Ant>();
            Ants.Add(new Ant(200, 200, 4, 1, W, ref label2, 19));
            Ants.Add(new Ant(200, 200, 4, 2, W, ref label3, 18));
            Ants.Add(new Ant(200, 200, 4, 3, W, ref label4, 17));
            Ants.Add(new Ant(200, 200, 2, 4, W, ref label5));
            Ants.Add(new Ant(200, 200, 2, 5, W, ref label6));
            Ants.Add(new Ant(200, 200, 2, 6, W, ref label7));
            Ants.Add(new Ant(200, 200, 2, 7, W, ref label8));
            Ants.Add(new Ant(200, 200, 2, 8, W, ref label9));
            Ants.Add(new Ant(200, 200, 2, 9, W, ref label10));
            Ants.Add(new Ant(200, 200, 2, 10, W, ref label11));
            Ants.Add(new Ant(200, 200, 2, 11, W, ref label12));
            Ants.Add(new Ant(200, 200, 2, 12, W, ref label13));
            Ants.Add(new Ant(200, 200, 2, 13, W, ref label14));
            Ants.Add(new Ant(200, 200, 2, 14, W, ref label15));

            //MessageHub.Subscribe<MoveMsg>((m) =>
            //{
            //    //label1.Text = string.Format("Tick {0} - Playing", C.Count);
            //    pictureBox1.Refresh();
            //    //g.DrawLine(pen1, 190, 210, 210, 190);
            //    //g.DrawLine(pen1, 190, 190, 210, 210);

            //    //g.DrawLine(pen2, 190, 20, 210, 0);
            //    //g.DrawLine(pen2, 190, 0, 210, 20);
            //});

            foreach (Ant A in Ants)
            {
                MessageHub.Subscribe<MoveMsg>((m) =>
                {
                    A.Move();
                    g.FillRectangle(new System.Drawing.SolidBrush((A.Color)), A.X - 2, A.Y - 2, 5, 5);
                    ChemTrail t = W.AddTrail(new ChemTrail(A.X, A.Y, 50));
                });
            }


        }

        private void button1_Click(object sender, EventArgs e)
        {
            C.Playing = !C.Playing;
            if (C.Playing)
            {
                button1.Text = "Pause";
                C.Worker.RunWorkerAsync();
            }
            else
                button1.Text = "Play";
        }

        private void ClockProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            //label1.Text = string.Format("Tick {0} - Playing", e.ProgressPercentage);
            //pictureBox1.Refresh();
            g.DrawLine(pen1, 190, 210, 210, 190);
            g.DrawLine(pen1, 190, 190, 210, 210);

            g.DrawLine(pen2, 190, 20, 210, 0);
            g.DrawLine(pen2, 190, 0, 210, 20);
            MessageHub.Publish(new MoveMsg());
        }

        private void ClockCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            label1.Text = String.Format("Tick {0} - Paused!", C.Count);
        }

        public class MoveMsg : ITinyMessage
        {
            public object Sender { get; private set; }
        }
    }
}
