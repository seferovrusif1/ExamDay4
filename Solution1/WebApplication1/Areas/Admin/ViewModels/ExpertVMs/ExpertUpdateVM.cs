namespace WebApplication1.Areas.Admin.ViewModels.ExpertVMs
{
    public class ExpertUpdateVM
    {
        public string ImagePath { get; set; }
        public string? ProfessionName { get; set; }
        public int ProfessionId { get; set; }
        public IEnumerable<int>? SMLinkIds { get; set; }
    }
}
