using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ajedrez.Models
{
    /// <summary>
    /// Esta es una clase de ejemplo para que vean como se declaran las propiedades, metodos, atributos y clases
    /// </summary>
    public class Persona
    {
        private int edad = 0; //atributo de la clase

        /// <summary>
        /// constructor de esta clase
        /// </summary>
        public Persona()
        {
            this.FechaNacimiento = new DateTime(1985, 12, 1);
        }

        /// <summary>
        /// Propiedad automática
        /// </summary>
        public DateTime FechaNacimiento
        {
            get;
            set;
        }

        /// <summary>
        /// Propiedad con implementacion
        /// </summary>
        public int Edad
        {
            get
            {
                this.CalcularEdad();
                return this.edad;
            }
        }


        /// <summary>
        /// metodo
        /// </summary>
        public void CalcularEdad()
        {
            this.edad = Convert.ToInt32((DateTime.Now - this.FechaNacimiento).TotalDays / 365);
        }


    }
}
