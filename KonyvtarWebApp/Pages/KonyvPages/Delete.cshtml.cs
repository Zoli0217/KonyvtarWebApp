using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using KonyvtarWebApp.Models;
using KonyvtarWebApp.Data;

namespace KonyvtarWebApp.Pages.KonyvPages;

public class DeleteModel : PageModel
{
    private readonly KonyvtarDbContext _context;

    public DeleteModel(KonyvtarDbContext context)
    {
        _context = context;
    }

    [BindProperty]
    public Konyv Konyv { get; set; } = default!;

    public async Task<IActionResult> OnGetAsync(int? id)
    {
        if (id is null)
        {
            return NotFound();
        }

        var konyv = await _context.Konyvek.Include(k => k.Szerzo).FirstOrDefaultAsync(m => m.Id == id);
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

    public async Task<IActionResult> OnPostAsync(int? id)
    {
        if (id is null)
        {
            return NotFound();
        }

        var konyv = await _context.Konyvek.FindAsync(id);
        if (konyv != null)
        {
            Konyv = konyv;
            _context.Konyvek.Remove(Konyv);
            await _context.SaveChangesAsync();
        }

        return RedirectToPage("./Index");
    }
}
