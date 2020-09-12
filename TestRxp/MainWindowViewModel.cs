using Reactive.Bindings;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reactive.Disposables;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Windows.Threading;

namespace TestRxp.ViewModel
{
    public class MainWindowViewModel : ViewModelBase
    {
        public ReactivePropertySlim<string> CurrentTime1 { get; }
        public ReactivePropertySlim<string> CurrentTime2 { get; }
        private System.Timers.Timer timer1 = new System.Timers.Timer(); // 他スレッド
        private DispatcherTimer timer2 = new DispatcherTimer();         // UIスレッド

        private string _CurrentTime3;
        public string CurrentTime3 
        {
            get { return _CurrentTime3; }
            set { SetProperty(ref _CurrentTime3, value); }
        }

        public MainWindowViewModel() 
        {

            CurrentTime1 = new ReactivePropertySlim<string>();
            CurrentTime2 = new ReactivePropertySlim<string>();

            timer1.Interval = 500;
            timer1.Elapsed += (s, e) =>
            {
                CurrentTime1.Value = DateTime.Now.ToString();
                CurrentTime3 = DateTime.Now.ToString();
            };
            timer1.Start();


            timer2.Interval = TimeSpan.FromMilliseconds(500);
            timer2.Tick += (s, e) => 
            {
                CurrentTime2.Value = DateTime.Now.ToString();
            };
            timer2.Start();

        }



        ~MainWindowViewModel() 
        {
            //thLoop.Abort();
            timer1.Stop();
            timer2.Start();
        }
    }

    public class ViewModelBase : BindableBase, IDisposable
    {
        protected CompositeDisposable Disposables { get; }
        public void Dispose()
        {
            Disposables.Dispose();
        }
    }

    public class BindableBase : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected bool SetProperty<T>(ref T field, T value, [CallerMemberName] string propName = null)
        {
            if (EqualityComparer<T>.Default.Equals(field, value))
            {
                return false;
            }

            field = value;

            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));

            return true;
        }
    }
}
