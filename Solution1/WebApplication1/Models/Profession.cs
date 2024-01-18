namespace WebApplication1.Models
{
    public class Profession:BaseModel
    {
        public string Name { get; set; }
        public IEnumerable<Experts>? Experts { get; set; }
    }
}
