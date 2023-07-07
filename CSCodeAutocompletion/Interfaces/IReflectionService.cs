using System.Reflection;

namespace Cs.CodeAutocompletion.Interfaces
{
    public interface IReflectionService
    {
        object CreateInstance(Assembly assembly, Type type);
        MemberInfo GetMember(Type type, string memberName);
        void Invoke(object instance, MemberInfo member, string[] args);
        Type SubstractType(Assembly assembly, string typeName);
    }
}