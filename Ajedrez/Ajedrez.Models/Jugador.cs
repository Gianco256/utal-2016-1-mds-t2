using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ajedrez.Models {
	public class Jugador {
		public long Id {
			get; set;
		}
		public String Nick {
			get; set;
		}
		public Sexo Sexo {
			get; set;
		}
		public DateTime FechaNacimiento {
			get; set;
		}
		private List<Partida> partidas;

		public Jugador() {
			this.partidas = new List<Partida>();
		}

		public List<Partida> Partidas() {
			return this.partidas;
		}

		public void Desafiar(Partida partida) {
			this.partidas.Add(partida);
		}

		public bool Eliminar(Partida partida) {
			return this.partidas.Remove(partida);
		}
	}
}
