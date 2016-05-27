using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ajedrez.Models
{
    public class Cuenta
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public DateTime UltimoAcceso { get; set; }
        public Jugador JugadorActual { get; set; }

        public Cuenta(){
            throw new NotImplementedException();
        }

        public bool Registrar(string email, string password){
            throw new NotImplementedException();
        }

        public bool Seleccionar(string email){
            throw new NotImplementedException();
        }

        public void IniciarSesion(){
            throw new NotImplementedException();
        }

        public void CerrarSesion(){
            throw new NotImplementedException();
        }

        public List<Jugador> Jugadores(){
            throw new NotImplementedException();
        }

        public void CambiarJugadorActivo(Jugador jugador){
            throw new NotImplementedException();
        }

        public bool CrearJugador(Jugador jugador){
            throw new NotImplementedException();
        }

        public bool EliminarJugador(Jugador jugador){
            throw new NotImplementedException();
        }

        public void Desactivar(){
            throw new NotImplementedException();
        }
    }
}
