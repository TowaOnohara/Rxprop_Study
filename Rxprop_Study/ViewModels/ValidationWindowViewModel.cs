using Reactive.Bindings;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Rxprop_Study.ViewModels
{
    public class ValidationWindowViewModel: ViewModelBase
    {
        [Required(ErrorMessage = "Required property")]
        public ReactiveProperty<string> WithDataAnnotations { get; }

    }
}
