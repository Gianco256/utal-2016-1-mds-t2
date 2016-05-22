using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ajedrez.Models {
	public class Jugada {
		private Coordenada origen, destino;

		public Coordenada Origen {
			get; set;
		}
		public Coordenada Destino {
			get; set;
		}

		public Jugada() {
		}
	}
}
