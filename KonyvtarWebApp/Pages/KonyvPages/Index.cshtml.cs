using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using KonyvtarWebApp.Models;
using KonyvtarWebApp.Data;

namespace KonyvtarWebApp.Pages.KonyvPages;

public class IndexModel : PageModel
{
    private readonly KonyvtarDbContext _context;

    public IndexModel(KonyvtarDbContext context)
    {
        _context = context;
    }

    public IList<Konyv> Konyv { get; set; } = default!;

    public async Task OnGetAsync()
    {
        Konyv = await _context.Konyvek.Include(k => k.Szerzo).ToListAsync();
    }
}
