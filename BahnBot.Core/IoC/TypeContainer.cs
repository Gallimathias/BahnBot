using System;
using System.Collections.Generic;
using System.Text;

namespace BahnBot.Core.IoC
{
    public static class TypeContainer
    {
        public static StandaloneTypeContainer Instance { get; }

        static TypeContainer()
        {
            Instance = new StandaloneTypeContainer();
        }

        public static void Register<TType>(bool singelton = false) 
            => Instance.Register<TType>(singelton);
        public static void Register<TType, TRegister>(bool singelton = false)
            => Instance.Register<TType, TRegister>(singelton);

        public static TType Get<TType>() 
            => Instance.Get<TType>();
    }
}
