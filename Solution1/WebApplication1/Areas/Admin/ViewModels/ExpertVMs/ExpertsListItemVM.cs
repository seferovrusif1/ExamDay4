namespace WebApplication1.Areas.Admin.ViewModels.ExpertVMs
{
    public class ExpertsListItemVM
    {
        public int Id { get; set; }
        public string ImagePath { get; set; }
        public string ProfessionName { get; set; }
        public string? smlink{ get; set; }
        public List<string>? SMLink { get; set; }


        public bool IsDeleted { get; set; }
    }
}
