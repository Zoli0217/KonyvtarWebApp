using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using KonyvtarWebApp.Models;
using KonyvtarWebApp.Data;
using KonyvtarWebApp.DTOs;

namespace KonyvtarWebApp.Pages.SzerzoPages;

public class EditModel : PageModel
{
    private readonly KonyvtarDbContext _context;

    public EditModel(KonyvtarDbContext context)
    {
        _context = context;
    }

    [BindProperty]
    public SzerzoEditDTO Szerzo { get; set; } = default!;

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
        Szerzo = new SzerzoEditDTO
        {
            Id = szerzo.Id,
            Nev = szerzo.Nev,
            SzuletesiEv = szerzo.SzuletesiEv
        };

        return Page();
    }

    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see https://aka.ms/RazorPagesCRUD.
    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid)
        {
            return Page();
        }

        Szerzo modositandoSzerzo = await _context.Szerzok.FindAsync(Szerzo.Id);

        modositandoSzerzo.Nev = Szerzo.Nev;
        modositandoSzerzo.SzuletesiEv = Szerzo.SzuletesiEv;

        _context.Attach(modositandoSzerzo).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!SzerzoExists(Szerzo.Id))
            {
                return NotFound();
            }
            else
            {
                throw;
            }
        }

        return RedirectToPage("./Index");
    }

    private bool SzerzoExists(int id)
    {
        return _context.Szerzok.Any(e => e.Id == id);
    }
}
