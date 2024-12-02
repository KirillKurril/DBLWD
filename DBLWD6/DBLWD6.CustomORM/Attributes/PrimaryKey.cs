namespace DBLWD6.CustomORM.Attributes
{
    [AttributeUsage(AttributeTargets.Property)]
    public class PrimaryKey : Attribute, ICustomORMAttribute
    {
    }
}
