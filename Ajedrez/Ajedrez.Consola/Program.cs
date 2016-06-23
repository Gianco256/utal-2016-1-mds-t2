using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Ajedrez.Models;

namespace Ajedrez.Consola {
	public class Program {
		public static void Main(string[] args) {
			Cuenta cuenta = new Cuenta() { Email = "abc", Password = "123" };
            cuenta.Registrar(cuenta.Email, cuenta.Password);
			Console.WriteLine(cuenta.IniciarSesion());
			cuenta.CambiarJugadorActivo(new Jugador());
			var partida = new Partida() { IdBlancas = 0, IdNegras = 1 };
			partida.Iniciar();
            PantallaPartida.Detalle(partida, 0);
			//cuenta.JugadorActual.Desafiar(partida);
			//cuenta.JugadorActual.Partidas();
			PantallaJugador.Crear(cuenta);
			Console.Read();
		}
	}
}
