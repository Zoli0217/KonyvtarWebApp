using KonyvtarWebApp.Data;
using KonyvtarWebApp.DTOs;
using KonyvtarWebApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace KonyvtarWebApp.Pages.KonyvPages;

public class EditModel : PageModel
{
    private readonly KonyvtarDbContext _context;

    public EditModel(KonyvtarDbContext context)
    {
        _context = context;
    }

    [BindProperty]
    public KonyvEditDTO Konyv { get; set; } = default!;

    public IEnumerable<SelectListItem> SzerzoSelectList { get; set; }

    public async Task<IActionResult> OnGetAsync(int? id)
    {
        if (id is null)
        {
            return NotFound();
        }
        var Szerzok = _context.Szerzok.ToList();
        SzerzoSelectList = Szerzok.Select(s => new SelectListItem
        {
            Value = s.Id.ToString(),
            Text = s.Nev
        });

        var konyv = await _context.Konyvek.FirstOrDefaultAsync(m => m.Id == id);
        if (konyv is null)
        {
            return NotFound();
        }
        Konyv = new KonyvEditDTO
        { 
            Id = konyv.Id,
            Cim = konyv.Cim,
            KiadasEve = konyv.KiadasEve,
            SzerzoId = konyv.SzerzoId
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
        Konyv modositandoKonyv = new Konyv
        {
            Id = Konyv.Id,
            Cim = Konyv.Cim,
            KiadasEve = Konyv.KiadasEve,
            SzerzoId = Konyv.SzerzoId
        };

        _context.Attach(modositandoKonyv).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!KonyvExists(Konyv.Id))
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

    private bool KonyvExists(int id)
    {
        return _context.Konyvek.Any(e => e.Id == id);
    }
}
