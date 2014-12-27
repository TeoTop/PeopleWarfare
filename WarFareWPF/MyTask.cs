using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Threading;

namespace WarFareWPF
{
    public class MyTask
    {
        public Task task { get; set; }
        public MyTask(Action action)
        {
            task = Task.Factory.StartNew(action);
        }

        public void WaitWithPumping()
        {
            var nestedFrame = new DispatcherFrame();
            task.ContinueWith(_ => nestedFrame.Continue = false);
            Dispatcher.PushFrame(nestedFrame);
            task.Wait();
        }
    }
}
