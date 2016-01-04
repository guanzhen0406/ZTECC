using System;
using System.Threading;

namespace Wicresoft.Solution.Utils
{
    public class TimeoutWatch<U>
    {
        AutoResetEvent evnt = new AutoResetEvent(false);

        private U u;
        public U Execute(Func<U> action, int millisecondsTimeout)
        {
            Thread th = new Thread(this.Run);
            evnt.Reset();
            th.Start(action);
            if (evnt.WaitOne(millisecondsTimeout, false))
            {
                return u;
            }
            else
            {
                th.Abort();
                throw new TimeoutException("method execute timeout!");
            }
        }

        void Run(object obj)
        {
            var action = obj as Func<U>;
            u = action();
            evnt.Set();
        }
    }
}
