using System;
using System.Collections.Generic;
using System.Text;

namespace Rxprop_Study.Models
{
    public class POCO: BindableBase
    {
        private string _FirstName;
        public string FirstName 
        {
            get { return _FirstName; }
            set { SetProperty(ref _FirstName, value); }
        }

        private string _LastName;
        public string LastName 
        {
            get { return _LastName; }
            set { SetProperty(ref _LastName, value); }
        }
    }
}
