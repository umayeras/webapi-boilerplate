namespace WebApp.Model.Entities
{
    public class Status
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
    
    public enum StatusType
    {
        Active = 1,
        Passive = 2,
        Deleted = 3,
        Draft = 4
    }
}