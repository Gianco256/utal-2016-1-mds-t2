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
			Console.WriteLine("Id      : " + p.Id);
			Console.WriteLine("Blancas : " + p.IdBlancas);
			Console.WriteLine("Negras  : " + p.IdNegras);
			Console.WriteLine("Turno   : " + p.Turno);
			if (jugador == p.IdBlancas) {
				p.Pintar();
				//ganador?
				if (p.Turno == Models.Color.BLANCO) {
					PantallaPartida.HacerJugada(p);
				} else {
					Console.WriteLine("Aún no es su turno");
				}
			} else {
				p.Pintar(); // Invertir!
				if (p.Turno == Models.Color.NEGRO) {
					PantallaPartida.HacerJugada(p);
				} else {
					Console.WriteLine("Aún no es su turno");
				}
			}
			p.Pintar();
			Console.WriteLine(p.Turno);
		}

		private static void HacerJugada(Models.Partida p) {
			Console.Write("Origen  : "); var origen = Console.ReadLine();
			Console.Write("Destino : "); var destino = Console.ReadLine();
			var origenCoordenada = PantallaPartida.Traducir(origen);
			var destinoCoordenada = PantallaPartida.Traducir(destino);
			var jugada = new Models.Jugada(origenCoordenada, destinoCoordenada);
			if (p.Jugar(jugada)) {
				Console.WriteLine("Jugada hecha exitosamente");
			} else {
				Console.WriteLine("Jugada no válida. Ingrese nuevamente");
				PantallaPartida.HacerJugada(p);
			}
		}

		// TODO
		private static bool ValidarMovimiento(string coordenada) {
			return true;
		}

		// TODO
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
