using Microsoft.AspNetCore.Mvc;

namespace RostrosFelices.Modelos
{
    public class Servicio
    {

        public int Id { get; set; }
        public DateTime fecha { get; set; }

        public int id_empleado { get; set; }
        public string empleado { get; set; }

        //public int id_cliente { get; set; }
        public string cliente { get; set; }
        public string servicio { get; set; }


        public ICollection<Empleado>? empleados { get; set; } = default!; //Propiedad de navegacion
    }
}
