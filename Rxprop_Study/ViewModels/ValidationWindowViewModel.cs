using Reactive.Bindings;
using Reactive.Bindings.Extensions;
using Rxprop_Study.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Linq;
using System.Reactive.Linq;
using System.Runtime.CompilerServices;
using System.Text;

namespace Rxprop_Study.ViewModels
{
    public class ValidationWindowViewModel: ViewModelBase
    {
        [Required(ErrorMessage = "Required property")]  // 値が格納されている必要がある、という属性(※)
        public ReactiveProperty<string> WithDataAnnotations { get; }
        public ReadOnlyReactivePropertySlim<string> WithDataAnnotationsErrMessage { get; }

        public ReactiveProperty<string> WithCustomValidationLogic { get; }
        public ReadOnlyReactivePropertySlim<string> WithCustomValidationLogicErrMessage { get; }

        public ReactiveProperty<bool> HasValidationErrors { get; }
        public ReactiveCommand SubmitCommand { get; }

        //[Required(ErrorMessage = "Required property2")]  // 値が格納されている必要がある、という属性(※)
        //public ReactiveProperty<string> WithDataAnnotations2 { get; }
        //public ReadOnlyReactivePropertySlim<string> WithDataAnnotationsErrMessage2 { get; }

        public POCO poco { get; } = new POCO { LastName = "test", FirstName = "TEST" };

        public ValidationWindowViewModel()
        {

            WithDataAnnotations = new ReactiveProperty<string>()    // 1) ReavtivePropertyを生成する
                .SetValidateAttribute(() => WithDataAnnotations)    // 2) WithDataAnnotationsに割り当たっている属性を1)で作ったインスタンスにセットする
                .AddTo(Disposables);                                // 3) まとめてDisposeできるように登録

            WithDataAnnotationsErrMessage = WithDataAnnotations     // 1) 情報ソースから
                .ObserveValidationErrorMessage()                    // 2) ValidationエラーメッセージをObserveする
                .ToReadOnlyReactivePropertySlim()                   // 3) ReactivePropertyに変換
                .AddTo(Disposables);                                // 4) まとめてDisposeできるように登録


            WithCustomValidationLogic = new ReactiveProperty<string>()
                .SetValidateNotifyError(x => !string.IsNullOrEmpty(x) && x.Contains("-") ? null : "Require '-'")
                .AddTo(Disposables);
            WithCustomValidationLogicErrMessage = WithCustomValidationLogic
                .ObserveValidationErrorMessage()
                .ToReadOnlyReactivePropertySlim()
                .AddTo(Disposables);

            HasValidationErrors = new[]
            {
                WithDataAnnotations.ObserveHasErrors,
                WithCustomValidationLogic.ObserveHasErrors,
            }.CombineLatest(x => x.Any(y => y))
            .ToReactiveProperty()
            .AddTo(Disposables);

            //HasValidationErrors = WithDataAnnotations.ObserveHasErrors
            //    .CombineLatest(WithCustomValidationLogic.ObserveHasErrors, (x, y) => x | y)
            //    .ToReactiveProperty()
            //    .AddTo(Disposables);
            ////https://blog.okazuki.jp/entry/20120219/1329663635


            SubmitCommand = new[]
            {
                WithDataAnnotations.ObserveHasErrors.Inverse(),         // WithDataAnnotationsにエラーがないこと
                WithCustomValidationLogic.ObserveHasErrors.Inverse(),   // WithCustomValidationLogicにエラーがないこと
            }.CombineLatestValuesAreAllTrue()                           // どちらかのイベントが発行されたときに、どちらもTrueであること
            .ToReactiveCommand()                                        // コマンドに変更
            .AddTo(Disposables);

            // コマンド処理処理の登録
            SubmitCommand.Subscribe(_ => Trace.WriteLine("SubmitCommand"));



            // TODO: ModelをSyncしているときはどのような挙動になるのか？
            //WithDataAnnotations2 = poco.ToReactivePropertyAsSynchronized(x => x.FirstName, ignoreValidationErrorValue: true)
            //    .SetValidateAttribute(() => WithDataAnnotations2)
            //    .AddTo(Disposables);
            //WithDataAnnotationsErrMessage2 = WithDataAnnotations2.ObserveValidationErrorMessage()
            //    .ToReadOnlyReactivePropertySlim()
            //    .AddTo(Disposables);

            //ignoreValidationErrorValue: エラー時にModelに値を反映させるかどうかのフラグ
        }

    }

    // ※属性
    // C#の機能。クラスや変数に対して負荷するもので、コンパイラやユーザー(プログラマ)に
    // 振る舞いを伝えることができる。
    // publicやprivateなども一種の属性。こういったものを自作できる機能。
    //https://ufcpp.net/study/csharp/sp_attribute.html

}
