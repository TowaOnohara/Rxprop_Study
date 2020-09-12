using Reactive.Bindings;
using Reactive.Bindings.Extensions;
using System;
using System.Collections.Generic;
using System.Reactive.Linq;
using System.Text;

namespace Rxprop_Study.ViewModels
{
    public class BasicUsagesWindowVewModel : ViewModelBase
    {
        public ReactiveProperty<string> Input { get; }  // TODO: setは不要？
        public ReadOnlyReactiveProperty<string> Output { get; }
     
        public ReactivePropertySlim<string> InputSlim { get; } 
        public ReadOnlyReactivePropertySlim<string> OutputSlim { get; }

        public BasicUsagesWindowVewModel()
        {
            Input = new ReactiveProperty<string>();
            Output = Input.Delay(TimeSpan.FromSeconds(1))
                .Select(x => x?.ToUpper())
                .ToReadOnlyReactiveProperty()
                .AddTo(Disposables);

            InputSlim = new ReactivePropertySlim<string>();
            OutputSlim = InputSlim.Delay(TimeSpan.FromSeconds(1))
                .Select(x => x?.ToUpper())
                .ObserveOnUIDispatcher()
                .ToReadOnlyReactivePropertySlim()   // TODO:???必要なのか？
                .AddTo(Disposables);
        }
    }
}
