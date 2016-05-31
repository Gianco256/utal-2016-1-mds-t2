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
        private List<Partida> partidas;

        public Jugador()
        {
			this.partidas = new List<Partida>();
        }

        public List<Partida> Partidas()
        {
            return partidas;
        }

        public void Desafiar(Partida partida)
        {
            partidas.Add(partida);
        }

        public bool Eliminar(Partida partida)
        {
            return partidas.Remove(partida);
        }
	}
}
