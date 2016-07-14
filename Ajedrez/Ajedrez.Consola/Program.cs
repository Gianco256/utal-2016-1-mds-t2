using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Ajedrez.Models;

namespace Ajedrez.Consola {
	public class Program {
        public static void Main(string[] args) {
            Cuenta cuenta = new Cuenta() { Email = "Gianco256@gmail.com", Password = "123", UltimoAcceso = DateTime.Now };
            cuenta.Registrar(cuenta.Email, cuenta.Password);
            Console.WriteLine(cuenta.IniciarSesion());
            var partida = new Partida() { IdBlancas = 0};
            cuenta.CambiarJugadorActivo(cuenta.Jugadores().FindLast(m=> m.Id==1));
            partida.IdNegras = cuenta.JugadorActual.Id;

            partida.Iniciar();
            long[] ids = { partida.IdBlancas, cuenta.JugadorActual.Id };
            for(int g= 0; g<6; g++)
            {
                PantallaPartida.Detalle(partida, ids[g%2]);
            }
            
            //cuenta.JugadorActual.Desafiar(partida);
            //cuenta.JugadorActual.Partidas();
            
			Console.Read();
		}
	}
}
