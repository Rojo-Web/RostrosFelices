using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;
using RostrosFelices.Data;
using RostrosFelices.Modelos;

namespace RostrosFelices.Pages.Servicioss
{
    [Authorize]
    public class CreateModel : PageModel
    {
        private readonly RostrosFelicesContext _context;
        public List<string> Empleados = new List<string>();


        public CreateModel(RostrosFelicesContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            string connectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=RostrosFelicesBD;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                // Consultar las categorías desde la base de datos
                using (SqlCommand command = new SqlCommand("select concat(id,'-',Nombre) from Empleados", connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {


                        // Leer las categorías y agregarlas a la lista
                        while (reader.Read())
                        {
                            string empleado = reader.GetString(0);

                            Empleados.Add(empleado);
                        }

                        // Pasar la lista de categorías a la vista
                        //cat = categorias;
                    }
                }
            }
            return Page();
        }
        [BindProperty]
        public Servicio servicios { get; set; } = default;
        //public Servicio servicios_F { get; set; } = default;
        //public Cliente cliente { get; set; } = default;

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid || _context.Servicios == null || servicios == null)
            {
                return Page();
            }

            Servicio servicios_F = new Servicio
            {
                Id = servicios.Id,
                servicio = servicios.servicio,
                fecha= servicios.fecha,
                cliente = servicios.cliente,
            };

            if (!string.IsNullOrEmpty(servicios.empleado))
            {
                int employeeSeparatorIndex = servicios.empleado.IndexOf('-');
                if (employeeSeparatorIndex != -1 && servicios.empleado.Length > employeeSeparatorIndex + 2)
                {
                    servicios_F.id_empleado = Convert.ToInt32(servicios.empleado.Substring(0, employeeSeparatorIndex));
                    servicios_F.empleado = servicios.empleado.Substring(employeeSeparatorIndex + 1);
                }
            }




            /*servicios_F.servicio = servicios.servicio;
            servicios_F.fecha = servicios.fecha;
            servicios_F.id_empleado = Convert.ToInt32(servicios.empleado.Substring(0,servicios.empleado.IndexOf('-')));
            servicios_F.servicio = servicios.empleado.Substring(servicios.empleado.IndexOf('-'), servicios.empleado.IndexOf('.'));
            servicios_F.cliente = servicios.cliente;*/
            _context.Servicios.Add(servicios_F);


            Cliente cliente = new Cliente
            {
                Nombre = servicios.cliente
            };
            //Cliente cliente = new Cliente();
            //cliente.Nombre = servicios.cliente;
            _context.Clientes.Add(cliente);

            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
