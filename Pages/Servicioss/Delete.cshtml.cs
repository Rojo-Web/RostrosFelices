using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using RostrosFelices.Data;
using RostrosFelices.Modelos;

namespace RostrosFelices.Pages.Servicioss
{
    [Authorize]
    public class DeleteModel : PageModel
    {
        
        private readonly RostrosFelicesContext _context;

        public DeleteModel(RostrosFelicesContext context)
        {
            _context = context;
        }
        [BindProperty]
        public Servicio Servicio { get; set; } = default;

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
            else
            {
                Servicio = servicio;

            }
            return Page();

        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null || _context.Servicios == null)
            {
                return NotFound();
            }
            var servicio = await _context.Servicios.FindAsync(id);

            if (servicio != null)
            {
                Servicio = servicio;
                _context.Servicios.Remove(servicio);
                await _context.SaveChangesAsync();
            }
            return RedirectToPage("./Index");
        }
    }
}
