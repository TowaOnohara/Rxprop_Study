using Reactive.Bindings;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Rxprop_Study.ViewModels
{
    public class ValidationWindowViewModel: ViewModelBase
    {
        [Required(ErrorMessage = "Required property")]  // 値が格納されている必要がある、という属性(※)
        public ReactiveProperty<string> WithDataAnnotations { get; }

    }

    // ※属性
    // C#の機能。クラスや変数に対して負荷するもので、コンパイラやユーザー(プログラマ)に
    // 振る舞いを伝えることができる。
    // publicやprivateなども一種の属性。こういったものを自作できる機能。
    //https://ufcpp.net/study/csharp/sp_attribute.html

}
