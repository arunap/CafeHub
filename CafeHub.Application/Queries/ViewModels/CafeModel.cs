namespace CafeHub.Application.Queries.ViewModels
{
    public class CafeModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Logo { get; set; }
        public string Location { get; set; }
        public int Employees { get; set; }
    }
}