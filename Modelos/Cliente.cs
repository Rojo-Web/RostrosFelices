using System.ComponentModel.DataAnnotations;

namespace RostrosFelices.Modelos
{
    public class Cliente
    {
        public int id { get; set; } //LLave primaria

        [Required]
        [DataType(DataType.EmailAddress)]
        public string Nombre { get; set; }
        public string celular { get; set; }
        
    }
}
