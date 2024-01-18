namespace WebApplication1.Models
{
    public class ExpertsSMLinks
    {
        public int Id { get; set; }
        public int SMLinksId { get; set; }
        public SMLinks? SMLinks { get; set; }
        public int ExpertsId { get; set; }
        public Experts? Experts { get; set; }

    }
}
