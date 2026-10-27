using ProjectHub.Data;
using ProjectHub.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ProjectHub.Pages.Projects
{
    public class DetailsModel : PageModel
    {
        private readonly AppDbContext _db;
        public DetailsModel(AppDbContext db) => _db = db;

        public Project Project { get; set; } = null!;

        public async Task<IActionResult> OnGetAsync(int id)
        {
            var project = await _db.Projects.FindAsync(id);
            if (project is null) return NotFound();
            Project = project;
            return Page();
        }
    }
}
