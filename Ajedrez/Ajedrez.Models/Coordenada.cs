using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ajedrez.Models {
	public class Coordenada {
		public char Fila {
			get; set;
		}
		public int Columna {
			get; set;
		}

		public Coordenada() {
		}

		public int[] traducir() {
			throw new NotImplementedException();
		}
	}
}
