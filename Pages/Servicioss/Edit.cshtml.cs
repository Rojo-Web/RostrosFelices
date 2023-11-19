using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using RostrosFelices.Data;
using RostrosFelices.Modelos;

namespace RostrosFelices.Pages.Servicioss
{
    [Authorize]
    public class EditModel : PageModel
    {
        private readonly RostrosFelicesContext _context;

        public EditModel(RostrosFelicesContext context)
        {
            _context = context;
        }

        [BindProperty]

        public Servicio Servicio { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Servicios == null)
            {
                return NotFound();
            }

            var servicio = await _context.Servicios.FirstOrDefaultAsync(m => m.Id == id);
            if (servicio == null)
            {
                return NotFound();
            }

            Servicio = servicio;
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(Servicio).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UsersExists(Servicio.Id))
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
        private bool UsersExists(int id)
        {
            return (_context.Servicios?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
