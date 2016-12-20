using System;
using System.Collections.Generic;
using System.Text;

namespace Book.UI.Invoices
{
    public class ChooseItem
    {
        private object _editValue;

        public object EditValue
        {
            get { return _editValue; }
            set { _editValue = value; }
        }
        private string buttonText;

        public string ButtonText
        {
            get { return buttonText; }
            set { buttonText = value; }
        }
        private string labelText;

        public string LabelText
        {
            get { return labelText; }
            set { labelText = value; }
        }
        private string errorMessage;

        public string ErrorMessage
        {
            get { return errorMessage; }
            set { errorMessage = value; }
        }

        public ChooseItem(object obj, string button, string lable) 
        {
            _editValue = obj;
            buttonText = button;
            labelText = lable;
        }

        public ChooseItem(object obj, string button, string lable, string errorMsg)
            : this(obj, button, lable)
        {
            errorMessage = errorMsg;
        }

        public ChooseItem(string button) 
        {
            buttonText = button;
        }

        public ChooseItem(object editValue)
        {
            _editValue = editValue;
        }

        public ChooseItem() { }
    }
}
