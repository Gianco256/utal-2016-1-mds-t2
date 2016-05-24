using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ajedrez.Models
{
    class Cuenta
    {
        private string email;
        private string password;
        private DateTime ultimoAcceso;
        private Jugador jugadorActual;

        Cuenta(){
            throw new NotImplementedException();
        }

        public bool registrar(string email, string password){
            throw new NotImplementedException();
        }

        public bool seleccionar(string email){
            throw new NotImplementedException();
        }

        public void iniciarSesion(){
            throw new NotImplementedException();
        }

        public void cerrarSesion(){
            throw new NotImplementedException();
        }

        public List<Jugador> jugadores(){
            throw new NotImplementedException();
        }

        public void cambiarJugadorActivo(Jugador jugador){
            throw new NotImplementedException();
        }

        public bool crearJugador(jugador){
            throw new NotImplementedException();
        }

        public bool eliminarJugador(jugador){
            throw new NotImplementedException();
        }

        public void desactivar(){
            throw new NotImplementedException();
        }
    }
}
