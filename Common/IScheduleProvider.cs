using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Concurrency;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    public interface ISchedulerProvider
    {
        IScheduler GetUi();

        IScheduler GetBackground();
    }
}
