using System.ComponentModel.DataAnnotations;

namespace RostrosFelices.Modelos
{
    public class Empleado
    {

        public int Id { get; set; }//Sera llave primaria

        public string? Nombre { get; set; }

        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

    }
}
