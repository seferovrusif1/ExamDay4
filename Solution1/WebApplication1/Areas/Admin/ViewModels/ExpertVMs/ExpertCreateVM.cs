﻿using WebApplication1.Models;

namespace WebApplication1.Areas.Admin.ViewModels.ExpertVMs
{
    public class ExpertCreateVM
    {
        public string ImagePath { get; set; }
        public string? ProfessionName { get; set; }
        public int ProfessionId { get; set; }
        public IEnumerable<int>? SMLinkIds { get; set; }

    }
}
