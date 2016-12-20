using System;
using System.Collections.Generic;
using System.Text;

namespace Common
{
    public class MyDog
    {        
        private static GrandDog dog;

        public static GrandDog Dog
        {
            get
            {
                if (dog == null)
                    dog = new GrandDog();
                return dog;
            }
        }
    }
}