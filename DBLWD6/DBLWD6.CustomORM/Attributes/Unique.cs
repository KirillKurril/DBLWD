namespace DBLWD6.CustomORM.Attributes
{
    [AttributeUsage(AttributeTargets.Property)]
    public class Unique : Attribute, ICustomORMAttribute
    {
    }
}
