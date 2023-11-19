using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using RostrosFelices.Data;
using RostrosFelices.Modelos;

namespace RostrosFelices.Pages.Servicioss
{
    [Authorize]
    public class IndexModel : PageModel
    {
        private readonly RostrosFelicesContext _Context;

        public IndexModel(RostrosFelicesContext context)
        {
            _Context = context;
        }

        public IList<Servicio> Servicio { get; set; } = default!;
        public async Task OnGetAsync()
        {

            if (_Context.Empleados != null)
            {
                Servicio = await _Context.Servicios.ToListAsync();
            }

        }
    }
}
