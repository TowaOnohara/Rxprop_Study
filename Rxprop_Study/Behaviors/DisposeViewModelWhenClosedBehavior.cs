using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xaml.Behaviors;
using System.Windows;
using System.Windows.Forms;

namespace Rxprop_Study.Behaviors
{
    public class DisposeViewModelWhenClosedBehavior: Behavior<Window>
    {
        protected override void OnAttached()
        {
            base.OnAttached();
            AssociatedObject.Closed += AssociatedObject_Closed;
        }

        private void AssociatedObject_Closed(object sender, EventArgs e) => (AssociatedObject.DataContext as IDisposable)?.Dispose();

        protected override void OnDetaching()
        {
            base.OnDetaching();
            AssociatedObject.Closed -= AssociatedObject_Closed;
        }
    }
}
