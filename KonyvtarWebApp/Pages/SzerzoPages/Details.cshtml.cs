using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using KonyvtarWebApp.Models;
using KonyvtarWebApp.Data;

namespace KonyvtarWebApp.Pages.SzerzoPages;

public class DetailsModel : PageModel
{
    private readonly KonyvtarDbContext _context;
    public DetailsModel(KonyvtarDbContext context)
    {
        _context = context;
    }

    public Szerzo Szerzo { get; set; } = default!;

    public async Task<IActionResult> OnGetAsync(int? id)
    {
        if (id is null)
        {
            return NotFound();
        }

        var szerzo = await _context.Szerzok.FirstOrDefaultAsync(m => m.Id == id);
        if (szerzo is null)
        {
            return NotFound();
        }
        else
        {
            Szerzo = szerzo;
        }

        return Page();
    }
}
