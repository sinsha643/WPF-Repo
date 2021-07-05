using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Disposables;
using System.Text;
using System.Threading.Tasks;
using System.Reactive;

namespace Common
{
    public static class DisposableExtensions
    {
        public static void AddToDisposables(this IDisposable disposable, System.Reactive.Disposables.CompositeDisposable composite)
        {
            if (disposable != null)
            {
                composite.Add(disposable);
            }
        }
    }
}
