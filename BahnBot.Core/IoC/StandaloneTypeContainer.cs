using System;
using System.Collections.Generic;
using System.Text;

namespace BahnBot.Core.IoC
{
    public class StandaloneTypeContainer
    {
        private readonly Dictionary<Type, int> register;
        private readonly Dictionary<Type, int> typeRegister;
        private readonly Dictionary<int, RegisterInformation> informationRegister;


        public StandaloneTypeContainer()
        {
            register = new Dictionary<Type, int>();
            typeRegister = new Dictionary<Type, int>();
            informationRegister = new Dictionary<int, RegisterInformation>();
        }

        public void Register<TType>(bool singelton = false)
        {
            InternalRegister(typeof(TType), typeof(TType), singelton);
        }
        public void Register<TType, TRegister>(bool singelton = false)
        {
            InternalRegister(typeof(TRegister), typeof(TType), singelton);
        }
              
        public TType Get<TType>()
        {
            if (register.TryGetValue(typeof(TType), out int id) ||
                typeRegister.TryGetValue(typeof(TType), out id))
            {
                return (TType)informationRegister[id].Object;
            }

            return default;
        }

        private void InternalRegister(Type registerType, Type instanceType, bool singelton)
        {
            RegisterInformation registerInformation;

            if (typeRegister.TryGetValue(instanceType, out int registerId))
            {
                registerInformation = informationRegister[registerId];
            }
            else
            {
                registerInformation = new RegisterInformation
                {
                    Singelton = singelton,
                    Type = instanceType
                };

                typeRegister.Add(instanceType, registerInformation.Id);
                informationRegister.Add(registerInformation.Id, registerInformation);
            }

            register.Add(registerType, registerInformation.Id);
        }

        private class RegisterInformation
        {
            public static int NextId => ++id;
            private static int id;

            public int Id { get; }
            public bool Singelton { get; set; }
            public Type Type { get; set; }
            public object Object => GetObject();

            private object singeltonObject;

            public RegisterInformation()
            {
                Id = NextId;
            }

            private object GetObject()
            {
                if (Singelton && singeltonObject != null)
                    return singeltonObject;

                var obj = Activator.CreateInstance(Type);

                if (Singelton)
                    singeltonObject = obj;

                return obj;
            }
        }
    }
}
