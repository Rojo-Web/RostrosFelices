using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;
using RostrosFelices.Modelos;
using System.Security.Claims;

namespace RostrosFelices.Pages.Account
{
    public class LoginModel : PageModel
    {
        [BindProperty]
        public Empleado User { get; set; }
        
        public void OnGet()
        {
        }
        //public List<User> Usuarios = new List<User>();
        //private User usuario;
        private string UsuarioEmail = "", UsuarioPassword = "";
        public async Task<IActionResult> OnPostAsync()
        {
            User.Nombre = "admin";
            if (!ModelState.IsValid) return Page();
            string connectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=RostrosFelicesBD;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                // Consultar las categorías desde la base de datos
                using (SqlCommand command = new SqlCommand("Select Email,[Password] from Empleados where Email = '" + User.Email + "'; ", connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {


                        // Leer las categorías y agregarlas a la lista
                        while (reader.Read())
                        {

                            UsuarioEmail = reader.GetString(0);
                            UsuarioPassword = reader.GetString(1);

                            //Usuarios.Add(usuario);
                        }
                    }
                }


                if (User.Email == UsuarioEmail && User.Password == UsuarioPassword)
                {
                    //Se crea los Claim, datos a almacenar en la Cookie
                    var claims = new List<Claim>
                    {
                        new Claim(ClaimTypes.Name,"admin"),
                        new Claim(ClaimTypes.Email,User.Email),
                    };
                    //Se asocia los claims creados a un nombre de una cookie
                    var identity = new ClaimsIdentity(claims, "MyCookieAuth");
                    //Se agrega la identidad creada al ClaimsPrincipal de la aplicacion
                    ClaimsPrincipal claimsPrincipal = new ClaimsPrincipal(identity);

                    //Se registra exitosamente la autenticacion y se crea la cookie en el navegador
                    await HttpContext.SignInAsync("MyCookieAuth", claimsPrincipal);

                    return RedirectToPage("/Index");
                }
                else
                {
                    ViewData["Mensaje"] = "Email o contraseña invalidos, porfavor intente nuevamente";
                    ViewData["liveToastBtn"] = true;
                    return Page();
                }
                return Page();
            }
        }
    }
}
