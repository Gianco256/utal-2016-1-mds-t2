using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Ajedrez.Models;

namespace Ajedrez.Consola {
	class Program {
		public static void Main(string[] args) {
			Cuenta cuenta = new Cuenta() { Email = "abc", Password = "123" };
			cuenta.Registrar("abc", "123");

			cuenta.CrearJugador(new Jugador() { Id = 1, FechaNacimiento = DateTime.Now, Nick = "Polux", Sexo = Sexo.MASCULINO });
			var jugadores = cuenta.Jugadores();

			cuenta.CambiarJugadorActivo(jugadores[0]);
			cuenta.EliminarJugador(jugadores[0]);
		}
	}
}
