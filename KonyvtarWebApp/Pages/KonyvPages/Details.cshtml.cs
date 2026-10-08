using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using KonyvtarWebApp.Models;
using KonyvtarWebApp.Data;

namespace KonyvtarWebApp.Pages.KonyvPages;

public class DetailsModel : PageModel
{
    private readonly KonyvtarDbContext _context;
    public DetailsModel(KonyvtarDbContext context)
    {
        _context = context;
    }

    public Konyv Konyv { get; set; } = default!;

    public async Task<IActionResult> OnGetAsync(int? id)
    {
        if (id is null)
        {
            return NotFound();
        }

        var konyv = await _context.Konyvek.FirstOrDefaultAsync(m => m.Id == id);
        if (konyv is null)
        {
            return NotFound();
        }
        else
        {
            Konyv = konyv;
        }

        return Page();
    }
}
