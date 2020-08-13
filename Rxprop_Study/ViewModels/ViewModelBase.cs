using Rxprop_Study.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reactive.Disposables;
using System.Text;

namespace Rxprop_Study.ViewModels
{
    public class ViewModelBase : BindableBase, IDisposable
    {
        // まとめてDisposeするための仕組み
        protected CompositeDisposable Disposables { get; } = new CompositeDisposable();
        public void Dispose() => Disposables.Dispose();
    }
}
