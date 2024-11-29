using DBLWD6.CustomORM.Entities;

namespace DBLWD6.CustomORM.Attributes
{
    [AttributeUsage(AttributeTargets.Property)]
    public class ForeignKey : Attribute
    {
        public Type TargetType { get; }
        public string TargetProperty { get; }
        public ForeignKey(Type targetType, string targetProperty)
        {
            if (!typeof(DbEntity).IsAssignableFrom(targetType))
                throw new ArgumentException("TargetType must inherit from DbEntity.");

            TargetType = targetType;
            TargetProperty = targetProperty;
        }
    }
}
