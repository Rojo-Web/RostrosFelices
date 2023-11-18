using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using RostrosFelices.Data;
using RostrosFelices.Modelos;

namespace RostrosFelices.Pages.Empleadoss
{
    public class IndexModel : PageModel
    {
        private readonly RostrosFelicesContext _Context;

        public IndexModel(RostrosFelicesContext context)
        {
            _Context = context;
        }

        public IList<Empleado> Empleados { get; set; } = default!;
        public async Task OnGetAsync()
        {

            if (_Context.Empleados != null)
            {
                Empleados = await _Context.Empleados.ToListAsync();
            }

        }
    }
}
