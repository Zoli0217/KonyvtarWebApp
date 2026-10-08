using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using KonyvtarWebApp.Models;
using KonyvtarWebApp.Data;
using KonyvtarWebApp.DTOs;

namespace KonyvtarWebApp.Pages.SzerzoPages;

public class CreateModel : PageModel
{
    private readonly KonyvtarDbContext _context;

    public CreateModel(KonyvtarDbContext context)
    {
        _context = context;
    }

    public IActionResult OnGet()
    {
        return Page();
    }

    [BindProperty]
    public SzerzoPostDTO Szerzo { get; set; } = default!;

    // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD.
    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid)
        {
            return Page();
        }
        var ujSzerzo = new Szerzo
        {
            Nev = Szerzo.Nev,
            SzuletesiEv = Szerzo.SzuletesiEv
        };

        _context.Szerzok.Add(ujSzerzo);
        await _context.SaveChangesAsync();

        return RedirectToPage("./Index");
    }
}
