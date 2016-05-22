using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ajedrez.Models {
	public class Pieza {
		private Color color;
		private Tipo tipo;

		public Color Color {
			get; set;
		}
		public Tipo Tipo {
			get; set;
		}

		public Pieza() {
		}
	}
}
