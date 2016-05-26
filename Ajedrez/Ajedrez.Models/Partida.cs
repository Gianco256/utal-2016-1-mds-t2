using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ajedrez.Models
{
    public class Partida {
        private int[,] Tablero = new int[8, 8];
        private Jugador Blancas;
        private Jugador Negras;
        private DateTime Inicio;
        private DateTime uUltimaJugada;
        private bool Turno;
        private List<Jugada> Jugadas= new List<Jugada>();

        Partida(){
            throw new NotImplementedException();
        }

        public void Iniciar(){
            throw new NotImplementedException();
        }

        public bool Jugar(Jugada jugada){
            throw new NotImplementedException();
        }

        private bool ValidarJugada(Jugada jugada){
            throw new NotImplementedException();
        }

        private bool Mover(Jugada jugada){
            throw new NotImplementedException();
        }

        private void GuardarJugada(Jugada jugada){
            throw new NotImplementedException();
        }

        private bool ValidarTablero(){
            throw new NotImplementedException();
        }

        private void Retroceder(){
            throw new NotImplementedException();
        }

        private void CambiarTurno(){
            throw new NotImplementedException();
        }
    }
}
