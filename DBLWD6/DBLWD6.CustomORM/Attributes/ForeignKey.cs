namespace DBLWD6.CustomORM.Attributes
{
    [AttributeUsage(AttributeTargets.Property)]
    public class ForeignKey : Attribute
    {
        Type _targetType { get; }
        public string TargetProperty { get; }
        public string OnDeleteAction { get; }
        public string TargetTable => _targetType.Name;
        public ForeignKey(Type targetType, string targetProperty, string onDeleteAction = "NO ACTION")
        {
            _targetType = targetType;
            TargetProperty = targetProperty;
            OnDeleteAction = onDeleteAction;
        }
    }
}
