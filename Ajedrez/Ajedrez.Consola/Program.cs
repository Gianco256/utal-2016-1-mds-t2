using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ajedrez.Consola
{
    class Program
    {
        static void Main(string[] args)
        {
            var persona = new Models.Persona();
            Console.WriteLine(persona.Edad);
        }
    }
}
