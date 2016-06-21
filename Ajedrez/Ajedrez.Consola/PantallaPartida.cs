using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ajedrez.Consola {
	public class PantallaPartida {
		public static void Desafiar(Models.Jugador j) {
			throw new NotImplementedException();
		}

		public static void Detalle(Models.Partida p, long jugador) {
			Interfaz.Title("Información de la Partida", true, false);
			Console.WriteLine("Id      : " + p.Id);
			Console.WriteLine("Blancas : " + p.IdBlancas);
			Console.WriteLine("Negras  : " + p.IdNegras);
			Console.WriteLine("Turno   : " + p.Turno);
			Interfaz.Title("Tablero", false, false);
			if (jugador == p.IdBlancas) {
				p.Pintar();
				Interfaz.Line(false);
				if (p.Turno == Models.Color.BLANCO) {
					PantallaPartida.HacerJugada(p);
				} else {
					Console.WriteLine("(!) Aún no es su turno (!)");
				}
			} else {
				p.Pintar();
				Interfaz.Line(false);
				if (p.Turno == Models.Color.NEGRO) {
					PantallaPartida.HacerJugada(p);
				} else {
					Console.WriteLine("(!) Aún no es su turno (!)");
				}
			}
		}

		private static void HacerJugada(Models.Partida p) {
			string origen = "-1";
			while (!PantallaPartida.ValidarCoordenada(origen) && origen != "Salir" && origen != "SALIR" && origen != "salir") {
				Console.Write("Origen  : ");
				origen = Console.ReadLine();
				if (!PantallaPartida.ValidarCoordenada(origen) && origen != "Salir" && origen != "SALIR" && origen != "salir") {
					Console.WriteLine("(!) Coordenada no válida. Ingrese nuevamente (!)");
				}
			}
			if (origen != "Salir" && origen != "SALIR" && origen != "salir") {
				string destino = "-1";
				while (!PantallaPartida.ValidarCoordenada(destino) && destino != "Salir" && destino != "SALIR" && destino != "salir" ) {
					Console.Write("Destino : ");
					destino = Console.ReadLine();
					if (!PantallaPartida.ValidarCoordenada(destino) && destino != "Salir" && destino != "SALIR" && destino != "salir") {
						Console.WriteLine("(!) Coordenada no válida. Ingrese nuevamente (!)");
					}
				}
				if (destino != "Salir" && destino != "SALIR" && destino != "salir") {
					var origenCoordenada = PantallaPartida.Traducir(origen);
					var destinoCoordenada = PantallaPartida.Traducir(destino);
					var jugada = new Models.Jugada(origenCoordenada, destinoCoordenada);
					if (p.Jugar(jugada)) {
						Console.WriteLine("(i) Jugada hecha exitosamente (i)");
					} else {
						Console.WriteLine("(!) Jugada no válida. Ingrese nuevamente (!)");
						PantallaPartida.HacerJugada(p);
					}
				} else {
					Console.WriteLine("(i) Ha salido de la partida con éxito (i)");
					return;
				}
			} else {
				Console.WriteLine("(i) Ha salido de la partida con éxito (i)");
				return;
			}

		}

		private static bool ValidarCoordenada(string argCoordenada) {
			if (!(argCoordenada[0] == 'A' || argCoordenada[0] == 'a'
				|| argCoordenada[0] == 'B' || argCoordenada[0] == 'b'
				|| argCoordenada[0] == 'C' || argCoordenada[0] == 'c'
				|| argCoordenada[0] == 'D' || argCoordenada[0] == 'd'
				|| argCoordenada[0] == 'E' || argCoordenada[0] == 'e'
				|| argCoordenada[0] == 'F' || argCoordenada[0] == 'f'
				|| argCoordenada[0] == 'G' || argCoordenada[0] == 'g'
				|| argCoordenada[0] == 'H' || argCoordenada[0] == 'h'
				)) {
				return false;
			}
			var fila = (int) Char.GetNumericValue(argCoordenada[1]);
			if (fila < 1 || fila > 8){
				return false;
			}
			return true;
		}

		private static Models.Coordenada Traducir(string argCoordenada) {
			Models.Coordenada coordenada = new Models.Coordenada();
			if (argCoordenada[0] == 'A' || argCoordenada[0] == 'a') {
				coordenada.Columna = 0;
			}
			if (argCoordenada[0] == 'B' || argCoordenada[0] == 'b') {
				coordenada.Columna = 1;
			}
			if (argCoordenada[0] == 'C' || argCoordenada[0] == 'c') {
				coordenada.Columna = 2;
			}
			if (argCoordenada[0] == 'D' || argCoordenada[0] == 'd') {
				coordenada.Columna = 3;
			}
			if (argCoordenada[0] == 'E' || argCoordenada[0] == 'e') {
				coordenada.Columna = 4;
			}
			if (argCoordenada[0] == 'F' || argCoordenada[0] == 'f') {
				coordenada.Columna = 5;
			}
			if (argCoordenada[0] == 'G' || argCoordenada[0] == 'g') {
				coordenada.Columna = 6;
			}
			if (argCoordenada[0] == 'H' || argCoordenada[0] == 'h') {
				coordenada.Columna = 7;
			}
			coordenada.Fila = Math.Abs((int) Char.GetNumericValue(argCoordenada[1]) - 8);
			return coordenada;
		}
	}
}
