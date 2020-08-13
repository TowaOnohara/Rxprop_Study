using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;

namespace Rxprop_Study.Models
{
    public class BindableBase : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// 指定フィールドに値をセットします。値に変更があった場合は変更イベントを発行します。
        /// </summary>
        /// <typeparam name="T">フィールド型</typeparam>
        /// <param name="_field">セット対象フィールド</param>
        /// <param name="value">値</param>
        /// <param name="propName">変更通知するプロパティ名</param>
        /// <returns>値変更結果 true:変更した/false:変更なし</returns>
        public bool SetProperty<T>(ref T _field, T value, [CallerMemberName] string propName = null) 
        {
            // 値の比較
            if(!EqualityComparer<T>.Default.Equals(_field, value)) 
            {
                // [同じであった]: 変更イベントの発行無し
                return false;
            }

            // 値変更イベントの発行
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));

            // 値の格納
            _field = value;

            return true;
        }
    }
}
