using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Concurrency;
using System.Text;
using System.Threading.Tasks;
namespace Common
{
    public sealed class SchedulerProvider : ISchedulerProvider
    {
        private readonly DispatcherScheduler _dispatcherScheduler;

        public SchedulerProvider() => this._dispatcherScheduler = DispatcherScheduler.Current;

        public IScheduler GetUi() => (IScheduler)this._dispatcherScheduler;

        public IScheduler GetBackground() => (IScheduler)TaskPoolScheduler.Default;
    }
}
