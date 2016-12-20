using System;
using System.Collections.Generic;
using System.Text;
namespace Book.UI
{
    public class AA
    {
     
        public AA(string message, System.Windows.Forms.Control control)
        {       
            
            this.message = message;
            this.control = control;
        }

        private string message;

        public string Message
        {
            get { return message; }
            set { message = value; }
        }

        private System.Windows.Forms.Control  control;

        public System.Windows.Forms.Control  Control
        {        
          
            get { return control; }
            set { control = value; }
        }
    }
}
