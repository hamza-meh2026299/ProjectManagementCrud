using ProjectHub.Data;
using ProjectHub.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace ProjectHub.Pages.Projects
{
    public class IndexModel : PageModel
    {
        private readonly AppDbContext _db;
        public IndexModel(AppDbContext db) => _db = db;

        public List<Project> Projects { get; set; } = new();
        public string? SearchQuery { get; set; }
        public string? StatusFilter { get; set; }

        public async Task OnGetAsync(string? search, string? status)
        {
            SearchQuery  = search?.Trim();
            StatusFilter = status?.Trim();

            var query = _db.Projects.AsQueryable();

            if (!string.IsNullOrEmpty(SearchQuery))
            {
                var lower = SearchQuery.ToLower();
                query = query.Where(p =>
                    p.Name.ToLower().Contains(lower) ||
                    p.Client.ToLower().Contains(lower) ||
                    (p.TeamLead != null && p.TeamLead.ToLower().Contains(lower)));
            }

            if (!string.IsNullOrEmpty(StatusFilter) && Enum.TryParse<ProjectStatus>(StatusFilter, out var s))
            {
                query = query.Where(p => p.Status == s);
            }

            Projects = await query.OrderByDescending(p => p.CreatedAt).ToListAsync();
        }
    }
}
