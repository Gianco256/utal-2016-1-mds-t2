using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ajedrez.Models
{
    public class Partida {
        private int[,] Tablero;
        private List<Jugada> Jugadas;
        public Jugador Blancas { get; set; }
        public Jugador Negras { get; set; }
        public DateTime Inicio { get; set; }
        public DateTime UltimaJugada { get; set; }
        public Color Turno { get; set; }

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
