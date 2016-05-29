using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ajedrez.Models {
	public class Jugador {
        public long Id { get; set; }
        public String Nick { get; set; }
        public Sexo Sexo { get; set;}
        public DateTime FechaNacimiento { get; set; }

        public Jugador()
        {
            throw new NotImplementedException();
        }

        public List<Partida> Partidas()
        {
            throw new NotImplementedException();
        }

        public void Desafiar(Partida partida)
        {
            throw new NotImplementedException();
        }

        public bool Eliminar(Partida partida)
        {
            throw new NotImplementedException();
        }
	}
}
