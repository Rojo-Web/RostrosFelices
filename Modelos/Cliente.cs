using System.ComponentModel.DataAnnotations;

namespace RostrosFelices.Modelos
{
    public class Cliente
    {
        public int id { get; set; } //LLave primaria

        [Required]
        public string Nombre { get; set; }
        
        
    }
}
