namespace DBLWD6.Domain.Entities
{
    public class DbEntity
    {
        [PrimaryKey]
        [Identity(1,1)]
        public int Id {  get; set; }
    }
}
