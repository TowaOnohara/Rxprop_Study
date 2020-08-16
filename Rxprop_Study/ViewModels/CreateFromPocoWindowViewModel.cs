using Reactive.Bindings;
using Reactive.Bindings.Extensions;
using Rxprop_Study.Models;
using System;
using System.Collections.Generic;
using System.Reactive.Disposables;
using System.Text;

namespace Rxprop_Study.ViewModels
{
    public class CreateFromPocoWindowViewModel : ViewModelBase
    {
        public POCO Poco { get; } = new POCO
        {
            FirstName = "yasuo",
            LastName = "okamoto",
        };
        public ReactiveProperty<string> FirstNameTwoWay{ get; }
        public ReactiveProperty<string> LastNameTwoWay { get; }
        public ReadOnlyReactiveProperty<string> FirstNameOneWay { get; }
        public ReadOnlyReactiveProperty<string> LastNameOneWay { get; }
        public ReactiveProperty<string> FirstNameToSource { get; }
        public ReactiveProperty<string> LastNameToSource { get; }

        public CreateFromPocoWindowViewModel()
        {

            // バインド方式:TwoWay 
            // TwoWay: ToReactivePropertyAsSynchronized()
            FirstNameTwoWay = Poco.ToReactivePropertyAsSynchronized(x => x.FirstName)
                .AddTo(Disposables);
            LastNameTwoWay = Poco.ToReactivePropertyAsSynchronized(x => x.LastName)
                .AddTo(Disposables);

            // バインド方式:OneWay
            // OneWay: ObserveProperty().ToReadOnlyReactiveProperty()
            FirstNameOneWay = Poco.ObserveProperty(x => x.FirstName)
                .ToReadOnlyReactiveProperty()
                .AddTo(Disposables);
            LastNameOneWay = Poco.ObserveProperty(x => x.LastName)
                .ToReadOnlyReactiveProperty()
                .AddTo(Disposables);

            // バインド方式:OneWayToSource
            // OneWayToSource: ReactiveProperty.FromObject()
            FirstNameToSource = ReactiveProperty.FromObject(Poco, x => x.FirstName);
            //.AddTo(Disposables);  // TODO:不要？
            LastNameToSource = ReactiveProperty.FromObject(Poco, x => x.LastName);
            //.AddTo(Disposables);  // TODO:不要？

        }
    }
}


