using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using KonyvtarWebApp.Models;
using KonyvtarWebApp.Data;
using Microsoft.AspNetCore.Mvc.Rendering;
using KonyvtarWebApp.DTOs;

namespace KonyvtarWebApp.Pages.KonyvPages;

public class CreateModel : PageModel
{
    private readonly KonyvtarDbContext _context;

    public CreateModel(KonyvtarDbContext context)
    {
        _context = context;
    }

    public List<Szerzo> Szerzok { get; set; }
    public IEnumerable<SelectListItem> SzerzoSelectList { get; set; }

    public IActionResult OnGet()
    {
        Szerzok = _context.Szerzok.ToList();
        SzerzoSelectList = Szerzok.Select(s => new SelectListItem
        {
            Value = s.Id.ToString(),
            Text = s.Nev
        });
        return Page();
    }    
    

    [BindProperty]
    public KonyvPostDTO Konyv { get; set; } = default!;

    // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD.
    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid)
        {
            return Page();
        }
        Konyv ujKonyv = new Konyv
        {
            Cim = Konyv.Cim,
            KiadasEve = Konyv.KiadasEve,
            SzerzoId = Konyv.SzerzoId
        };
        _context.Konyvek.Add(ujKonyv);
        await _context.SaveChangesAsync();

        return RedirectToPage("./Index");
    }
}
