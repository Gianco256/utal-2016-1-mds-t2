﻿using System;
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
				var linea = string.Format("{0}.- {1} [{2}]", jugadores[i].Id, jugadores[i].Nick, (jugadores[i].Sexo == Models.Sexo.MASCULINO) ? "M" : "S");
				Console.WriteLine(linea);
			}

            Interfaz.Title("{0}SALIR\n{1}Seleccionar jugador activo\n{2}Ver Partidas del jugador activo\n{3}Crear Jugador", true, false);
            var opcion = Console.ReadLine();
            switch (Convert.ToInt32(opcion)){
                case (0): return;
                case (1):
                    Console.WriteLine("Ingresa el numero del jugador con el cual quieres jugar");
                    opcion = Console.ReadLine();
                    if (!string.IsNullOrEmpty(opcion))
                    {
                        var opcionSeleccionada = Convert.ToInt64(opcion);
                        c.CambiarJugadorActivo(c.Jugadores().FirstOrDefault(m => m.Id == opcionSeleccionada));
                        Console.WriteLine("El jugador seleccionado fue:" + c.JugadorActual.Nick);
                    }
                    return;
                case (2):
                    PantallaJugador.MostarPartidas(c.JugadorActual);
                    break;
                case (3):
                    PantallaJugador.Crear(c);
                    break;
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
