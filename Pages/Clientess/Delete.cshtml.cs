using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using RostrosFelices.Data;
using RostrosFelices.Modelos;

namespace RostrosFelices.Pages.Clientess
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
        public Cliente Cliente { get; set; } = default;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Clientes == null)
            {
                return NotFound();

            }
            var cliente = await _context.Clientes.FirstOrDefaultAsync(m => m.id == id);

            if (cliente == null)
            {
                return NotFound();
            }
            else
            {
                Cliente = cliente;

            }
            return Page();

        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null || _context.Clientes == null)
            {
                return NotFound();
            }
            var cliente = await _context.Clientes.FindAsync(id);

            if (cliente != null)
            {
                Cliente = cliente;
                _context.Clientes.Remove(cliente);
                await _context.SaveChangesAsync();
            }
            return RedirectToPage("./Index");
        }
    }
}
