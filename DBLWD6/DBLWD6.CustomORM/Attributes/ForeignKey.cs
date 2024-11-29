namespace DBLWD6.CustomORM.Attributes
{
    [AttributeUsage(AttributeTargets.Property)]
    public class ForeignKey : Attribute
    {
        public Type TargetType { get; }
        public string TargetProperty { get; }
        public ForeignKey(Type targetType, string targetProperty)
        {
            TargetType = targetType;
            TargetProperty = targetProperty;
        }
    }
}
