using DBLWD6.CustomORM.Model;

namespace DBLWD6.CustomORM.Services
{
    public static class Converter<T>
    {
        public static SQLEntity GetCreateTableQuery()
        {
            return new SQLEntity();
        }
        public static SQLEntity GetAddProcedure()
        {
            return new SQLEntity();
        }
        public static SQLEntity GetUpdateProcedure()
        {
            return new SQLEntity();
        }
        public static SQLEntity GetDeleteProcedure()
        {
            return new SQLEntity();
        }
        public static SQLEntity GetSelectByIdProcedure()
        {
            return new SQLEntity();
        }
        public static SQLEntity GetSelectByConditionsProcedure()
        {
            return new SQLEntity();
        }
    }
}
