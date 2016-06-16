using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Ajedrez.Models;

namespace Ajedrez.Consola {
	class Program {
		public static void Main(string[] args) {
			Cuenta cuenta = new Cuenta() {Email = "abc", Password = "123"};
			Console.WriteLine(cuenta.IniciarSesion());

            cuenta.CambiarJugadorActivo(cuenta.Jugadores()[0]);


            var partida = new Partida() { Blancas = cuenta.Jugadores()[0], Negras = cuenta.Jugadores()[1] };
            partida.Iniciar();
            partida.Pintar();

            cuenta.JugadorActual.Desafiar(partida);
            cuenta.JugadorActual.Partidas();


            Console.Read();




		}
	}
}
