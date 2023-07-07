using Cs.CodeAutocompletion.Interfaces;
using System.Reflection;

namespace Cs.CodeAutocompletion.Services
{
    public class ReflectionService : IReflectionService
    {
        public void Invoke(object instance, MemberInfo member, string[] args)
        {
            var method = member as MethodInfo;

            if (method is null)
            {
                throw new InvalidOperationException("Provided member is not a method.");
            }

            method.Invoke(instance, args);
        }

        public MemberInfo GetMember(Type type, string memberName)
        {
            var member = type.GetMember(memberName).FirstOrDefault();
            if (member is null)
            {
                throw new Exception("No member was found.");
            }

            return member;
        }

        public Type SubstractType(Assembly assembly, string typeName)
        {
            var type = assembly.GetType(typeName);
            if (type == null)
            {
                throw new Exception("Unable to load type from assembly.");
            }

            return type;
        }

        public object CreateInstance(Assembly assembly, Type type)
        {
            var instance = assembly.CreateInstance(type.Name);
            if (instance is null)
            {
                throw new Exception("Unable to create instance.");
            }

            return instance;
        }
    }
}
