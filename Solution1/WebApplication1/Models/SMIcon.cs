namespace WebApplication1.Models
{
    public class SMIcon:BaseModel
    {
        public string Name {  get; set; }
        public IEnumerable<SMLinks> SMLinks { get; set; }
    }
}
