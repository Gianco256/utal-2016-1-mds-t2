using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ajedrez.Consola {
	public class PantallaJugador {
		public static void MostarPartidas(Models.Jugador j) {
			Console.WriteLine("---------- Tus partidas ---------------");
			var partidas = j.Partidas();
			for (int i = 0; i < partidas.Count; i++) {
				var linea = string.Format("{0}.- {1} vs {2} [{3}]", i, partidas[i].IdBlancas, partidas[i].IdNegras, partidas[i].Turno);
				Console.WriteLine(linea);
			}
		}

		public static void Crear(Models.Cuenta c) {
			// Falta establecer el ID
			Models.Jugador jugador = new Models.Jugador();
			Interfaz.Title("Nuevo Jugador para la cuenta " + c.Email, true, false);
			Console.Write("Nick                : ");
			string nick = Console.ReadLine();
			jugador.Nick = nick;
			string sexoStr = "-1";
			while (sexoStr != "MASCULINO" && sexoStr != "FEMENINO") {
				Console.Write("Sexo                : ");
				sexoStr = Console.ReadLine().ToUpper();
				if (sexoStr != "MASCULINO" && sexoStr != "FEMENINO") {
					Console.WriteLine("(!) Sexo no válido. Intente nuevamente (!)");
				}
			}
			jugador.Sexo = (Models.Sexo) Enum.Parse(typeof(Models.Sexo), sexoStr);
			while (true){
				try{
					Console.Write("Fecha de Nacimiento : ");
					string fechaNacimiento = Console.ReadLine();
					jugador.FechaNacimiento = DateTime.ParseExact(fechaNacimiento, "yyyy-MM-dd", CultureInfo.InvariantCulture);
					break;
				}catch (Exception ex) {
					Console.WriteLine("(!) Fecha no válida. Intente nuevamente (!)");
				}
			}
            jugador.Id = Models.Jugador.CantidadJugadores();
			c.CrearJugador(jugador);
			Interfaz.Title("(i) Jugador " + nick + " creado con éxito (i)", true, true);
		}

		public static void Eliminar(Models.Cuenta c) {
			throw new NotImplementedException();
		}
	}
}
