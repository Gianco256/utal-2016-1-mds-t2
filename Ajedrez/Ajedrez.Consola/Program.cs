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
			Console.WriteLine(cuenta.IniciarSesion());
			cuenta.CambiarJugadorActivo(new Jugador());
			var partida = new Partida() { IdBlancas = 0, IdNegras = 1 };
			partida.Iniciar();
			//cuenta.JugadorActual.Desafiar(partida);
			//cuenta.JugadorActual.Partidas();
			//PantallaPartida.Detalle(partida, 0);
			Console.Read();
		}
	}
}
