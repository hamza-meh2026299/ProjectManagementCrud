using ProjectHub.Data;
using ProjectHub.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace ProjectHub.Pages.Projects
{
    public class EditModel : PageModel
    {
        private readonly AppDbContext _db;
        public EditModel(AppDbContext db) => _db = db;

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
            if (!ModelState.IsValid)
                return Page();

            Project.UpdatedAt = DateTime.UtcNow;

            _db.Attach(Project).State = EntityState.Modified;
            _db.Entry(Project).Property(x => x.CreatedAt).IsModified = false;

            try
            {
                await _db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await _db.Projects.AnyAsync(p => p.Id == Project.Id))
                    return NotFound();
                throw;
            }

            TempData["Success"] = $"Project \"{Project.Name}\" was updated successfully.";
            return RedirectToPage("Index");
        }
    }
}
