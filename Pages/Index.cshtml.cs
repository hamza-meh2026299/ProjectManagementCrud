using ProjectHub.Data;
using ProjectHub.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace ProjectHub.Pages
{
    public class IndexModel : PageModel
    {
        private readonly AppDbContext _db;
        public IndexModel(AppDbContext db) => _db = db;

        public int Total { get; set; }
        public int InProgress { get; set; }
        public int Completed { get; set; }
        public int Overdue { get; set; }
        public int OnHold { get; set; }

        public async Task OnGetAsync()
        {
            var now = DateTime.UtcNow.Date;
            Total      = await _db.Projects.CountAsync();
            InProgress = await _db.Projects.CountAsync(p => p.Status == ProjectStatus.InProgress);
            Completed  = await _db.Projects.CountAsync(p => p.Status == ProjectStatus.Completed);
            OnHold     = await _db.Projects.CountAsync(p => p.Status == ProjectStatus.OnHold);
            Overdue    = await _db.Projects.CountAsync(p =>
                p.DueDate.HasValue &&
                p.DueDate.Value.Date < now &&
                p.Status != ProjectStatus.Completed &&
                p.Status != ProjectStatus.Cancelled);
        }
    }
}
