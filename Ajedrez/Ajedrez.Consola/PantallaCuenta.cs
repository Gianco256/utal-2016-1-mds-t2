using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ajedrez.Consola {
	public class PantallaCuenta {
		public static void MostarJugadores(Models.Cuenta c) {
			Console.WriteLine("---------- Tus Jugadores ---------------");
			var jugadores = c.Jugadores();
			for (int i = 0; i < jugadores.Count; i++) {
				var linea = string.Format("{0}.- {1} [{2}]", jugadores[i].Id, jugadores[i].Nick, (jugadores[i].Sexo == Models.Sexo.MASCULINO ? "M" : "S"));
				Console.WriteLine(linea);
			}
			Console.WriteLine("Ingresa el numero del jugador con el cual quieres jugar");
			var opcion = Console.ReadLine();
			if (!string.IsNullOrEmpty(opcion)) {
				var opcionSeleccionada = Convert.ToInt64(opcion);
				c.CambiarJugadorActivo(c.Jugadores().FirstOrDefault(m => m.Id == opcionSeleccionada));
				Console.WriteLine("El jugador seleccionado fue:" + c.JugadorActual.Nick);
			}
		}

		public static void Login(Models.Cuenta c) {
			throw new NotImplementedException();
		}

		public static void Registrar(Models.Cuenta c) {
			throw new NotImplementedException();
		}
	}
}
