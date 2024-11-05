
using System.Collections.Generic;
using System.Data.OleDb;

namespace ASPNETMVCCheckboxDemo.Models
{
    public class Cliente
    {
        public int Id { get; set; }
        public string Compania { get; set; }
        public string Nombre { get; set; }
        public string Apellidos { get; set; }
        public string Correo { get; set; }
        public string Tel { get; set; }
        public string Cargo { get; set; }
    }
}