using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using RostrosFelices.Data;
using RostrosFelices.Modelos;

namespace RostrosFelices.Pages.Clientess
{
    [Authorize]
    public class IndexModel : PageModel
    {
        private readonly RostrosFelicesContext _Context;

        public IndexModel(RostrosFelicesContext context)
        {
            _Context = context;
        }

        public IList<Cliente> clientes { get; set; } = default!;
        public async Task OnGetAsync()
        {

            if (_Context.Clientes != null)
            {
                clientes = await _Context.Clientes.ToListAsync();
            }

        }
    }
}
