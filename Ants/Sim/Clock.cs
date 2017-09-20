using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Ants.Sim
{
    public class Clock
    {
        public BackgroundWorker Worker { get; set; }
        public bool Playing { get; set; }
        public int Count { get; set; }
        public int Delay { get; set; }
        public Clock(int delay = 500)
        {
            this.Delay = delay;
            Worker = new BackgroundWorker();
            Worker.WorkerReportsProgress = true;

            // what to do in the background thread
            Worker.DoWork += DoWork;
        }

        public void DoWork(object sender, DoWorkEventArgs e)
        {
            BackgroundWorker b = sender as BackgroundWorker;

            // do some simple processing for 10 seconds
            while (Playing)
            {
                // report the progress in percent
                b.ReportProgress(Count);
                //Count++
                Thread.Sleep(this.Delay);
            }
        }
    }
}
