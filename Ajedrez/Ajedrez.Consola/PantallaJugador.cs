using System;
using System.Collections.Generic;
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
			throw new NotImplementedException();
		}

		public static void Eliminar(Models.Cuenta c) {
			throw new NotImplementedException();
		}


	}
}
