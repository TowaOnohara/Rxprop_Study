using System;
using System.Collections.Generic;
using System.Text;

namespace Rxprop_Study.Models
{
    public class POCO : BindableBase
    {
        private string _firstName;
        public string FirstName
        {
            get { return _firstName; }
            set { SetProperty(ref _firstName, value); }
        }

        private string _lastName;
        public string LastName
        {
            get { return _lastName; }
            set { SetProperty(ref _lastName, value); }
        }

    }
    //public class POCO: BindableBase
    //{
    //    private string _FirstName;
    //    public string FirstName 
    //    {
    //        get { return _FirstName; }
    //        set { SetProperty(ref _FirstName, value); }
    //    }

    //    private string _LastName;
    //    public string LastName 
    //    {
    //        get { return _LastName; }
    //        set { SetProperty(ref _LastName, value); }
    //    }
    //}
}
