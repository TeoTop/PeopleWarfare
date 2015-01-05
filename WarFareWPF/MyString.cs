using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WarFareWPF
{
    public class MyString : Notifier
    {
        private string _value;
        public string value
        {
            get
            {
                return _value;
            }
            set
            {
                _value = value;
            }
        }
        public MyString() { }
        public MyString(String value)
        {
            this.value = value;
        }
    }
}
