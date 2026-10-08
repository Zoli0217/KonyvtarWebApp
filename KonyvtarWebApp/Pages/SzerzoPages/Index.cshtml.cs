using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using KonyvtarWebApp.Models;
using KonyvtarWebApp.Data;

namespace KonyvtarWebApp.Pages.SzerzoPages;

public class IndexModel : PageModel
{
    private readonly KonyvtarDbContext _context;

    public IndexModel(KonyvtarDbContext context)
    {
        _context = context;
    }

    public IList<Szerzo> Szerzo { get; set; } = default!;

    public async Task OnGetAsync()
    {
        Szerzo = await _context.Szerzok.ToListAsync();
    }
}
