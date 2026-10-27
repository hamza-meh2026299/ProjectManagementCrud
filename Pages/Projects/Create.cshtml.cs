using ProjectHub.Data;
using ProjectHub.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ProjectHub.Pages.Projects
{
    public class CreateModel : PageModel
    {
        private readonly AppDbContext _db;
        public CreateModel(AppDbContext db) => _db = db;

        [BindProperty]
        public Project Project { get; set; } = new();

        public void OnGet() { }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
                return Page();

            Project.CreatedAt = DateTime.UtcNow;
            Project.UpdatedAt = DateTime.UtcNow;

            _db.Projects.Add(Project);
            await _db.SaveChangesAsync();

            TempData["Success"] = $"Project \"{Project.Name}\" was created successfully.";
            return RedirectToPage("Index");
        }
    }
}
