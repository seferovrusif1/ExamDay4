namespace WebApplication1.Models
{
    public class Experts:BaseModel
    {
        public string ImagePath { get; set; }
        public int ProfessionId { get; set; }
        public Profession? Profession { get; set; }
        public IEnumerable<ExpertsSMLinks>? ExpertsSMLinks { get; set; }
    }
}
