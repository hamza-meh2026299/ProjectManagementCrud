using ProjectHub.Data;
using ProjectHub.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ProjectHub.Pages.Projects
{
    public class DeleteModel : PageModel
    {
        private readonly AppDbContext _db;
        public DeleteModel(AppDbContext db) => _db = db;

        [BindProperty]
        public Project Project { get; set; } = null!;

        public async Task<IActionResult> OnGetAsync(int id)
        {
            var project = await _db.Projects.FindAsync(id);
            if (project is null) return NotFound();
            Project = project;
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var project = await _db.Projects.FindAsync(Project.Id);
            if (project is not null)
            {
                var name = project.Name;
                _db.Projects.Remove(project);
                await _db.SaveChangesAsync();
                TempData["Success"] = $"Project \"{name}\" was deleted.";
            }
            return RedirectToPage("Index");
        }
    }
}
