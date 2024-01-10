using System;
using System.Collections.Generic;
using System.Text;

namespace AppBenavidesAnthonyCurdMovil
{
    internal class Restaurante
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public int Estrellas { get; set; }
        public string Especialidad { get; set; }
        public string Ciudad { get; set; }
        public string Direccion { get; set; }

        // Relación 1-Restaurante => *-Platos 
        public List<Plato> Platos { get; set; }
    }
}
