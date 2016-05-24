using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ajedrez.Models
{
    class Partida {
        private int[,] tablero = new int[8, 8];
        private Jugador blancas;
        private Jugador negras;
        private DateTime inicio;
        private DateTime ultimaJugada;
        private bool turno;
        private List<Jugada> jugadas= new List<Jugada>();

        Partida(){
            throw new NotImplementedException();
        }

        public void iniciar(){
            throw new NotImplementedException();
        }

        public bool jugar(Jugada jugada){
            throw new NotImplementedException();
        }

        private bool validarJugada(Jugada jugada){
            throw new NotImplementedException();
        }

        private bool mover(Jugada jugada){
            throw new NotImplementedException();
        }

        private void guardarJugada(Jugada jugada){
            throw new NotImplementedException();
        }

        private bool validarTablero(){
            throw new NotImplementedException();
        }

        private void retroceder(){
            throw new NotImplementedException();
        }

        private void cambiarTurno(){
            throw new NotImplementedException();
        }
    }
}
