using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ajedrez.Models {
	public class Jugada {
		public Coordenada Origen {
			get; set;
		}
		public Coordenada Destino {
			get; set;
		}

		public Jugada() {
		}

		public Jugada(Coordenada origen, Coordenada destino) {
			this.Origen = origen;
			this.Destino = destino;
		}
	}
}
