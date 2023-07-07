namespace CSTestContextBuilder.Core.Models
{
    public class ClassMember
    {
        public string ClassName { get; set; }

        public string MemberName { get; set; }

        public ClassMember(string className, string memberName)
        {
            ClassName = className;
            MemberName = memberName;
        }
    }
}
