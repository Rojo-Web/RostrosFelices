using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using RostrosFelices.Data;
using RostrosFelices.Modelos;

namespace RostrosFelices.Pages.Empleadoss
{
    //[Authorize]
    public class DeleteModel : PageModel
    {
        private readonly RostrosFelicesContext _context;

        public DeleteModel(RostrosFelicesContext context)
        {
            _context = context;
        }
        [BindProperty]
        public Empleado Users { get; set; } = default;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Empleados == null)
            {
                return NotFound();

            }
            var user = await _context.Empleados.FirstOrDefaultAsync(m => m.Id == id);

            if (user == null)
            {
                return NotFound();
            }
            else
            {
                Users = user;

            }
            return Page();

        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null || _context.Empleados == null)
            {
                return NotFound();
            }
            var user = await _context.Empleados.FindAsync(id);

            if (user != null)
            {
                Users = user;
                _context.Empleados.Remove(user);
                await _context.SaveChangesAsync();
            }
            return RedirectToPage("./Index");
        }
    }
}
