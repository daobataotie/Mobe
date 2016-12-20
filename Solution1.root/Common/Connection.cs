using System;
using System.Collections.Generic;
using System.Text;

namespace Common
{
    public abstract class Connection
    {
        private string name;
    
        public string Name
        {
            get
            {
                return this.name;
            }
            set
            {
                this.name = value;
            }
        }

        public abstract string ToString(string note);

        public override string ToString()
        {
            return string.Format("{0}({1})", this.name, this.Type);
        }

        public abstract string Type
        {
            get;
        }

        public abstract bool Awailable
        {
            get;
        }
    }
}
