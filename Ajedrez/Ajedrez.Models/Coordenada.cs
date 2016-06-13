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

		public int[] Traducir() {
			int[] translate = new int[2];
			switch (this.Fila) {
				case 'A':
					translate[0] = 0;
					break;
				case 'B':
					translate[0] = 1;
					break;
				case 'C':
					translate[0] = 2;
					break;
				case 'D':
					translate[0] = 3;
					break;
				case 'E':
					translate[0] = 4;
					break;
				case 'F':
					translate[0] = 5;
					break;
				case 'G':
					translate[0] = 6;
					break;
				case 'H':
					translate[0] = 7;
					break;
				default:
					translate[0] = -1;
					break;
			}
			translate[1] = this.Columna;
			return translate;
		}
	}
}
