using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using KonyvtarWebApp.Models;
using KonyvtarWebApp.Data;

namespace KonyvtarWebApp.Pages.SzerzoPages;

public class DeleteModel : PageModel
{
    private readonly KonyvtarDbContext _context;

    public DeleteModel(KonyvtarDbContext context)
    {
        _context = context;
    }

    [BindProperty]
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

    public async Task<IActionResult> OnPostAsync(int? id)
    {
        if (id is null)
        {
            return NotFound();
        }

        var szerzo = await _context.Szerzok.FindAsync(id);
        bool vanKonyve = await _context.Konyvek.AnyAsync(k => k.SzerzoId == id);
        if (szerzo != null && !vanKonyve)
        {
            Szerzo = szerzo;
            _context.Szerzok.Remove(Szerzo);
            await _context.SaveChangesAsync();
        }

        return RedirectToPage("./Index");
    }
}
