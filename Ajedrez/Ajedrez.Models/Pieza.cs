using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ajedrez.Models {
	public class Pieza {
		public Color Color {
			get; set;
		}
		public Tipo Tipo {
			get; set;
		}

		public Pieza(Color color, Tipo tipo) {
			this.Color = color;
			this.Tipo = tipo;
		}
	}
}
