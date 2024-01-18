namespace WebApplication1.Models
{
    public class SMLinks:BaseModel
    {
        public string SMLink { get; set; }
        public IEnumerable<ExpertsSMLinks>? ExpertsSMLinks { get; set; }
        public int SMIconId { get; set; }
        public SMIcon? SMIcon { get; set; }
    }
}
