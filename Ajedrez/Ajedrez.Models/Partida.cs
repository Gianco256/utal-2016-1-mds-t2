using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Xml;
using System.Xml.Linq;

namespace Ajedrez.Models {
	public class Partida {
		private Pieza[,] Tablero;
		private List<Jugada> Jugadas;
		public Jugador Blancas {
			get; set;
		}
		public Jugador Negras {
			get; set;
		}
		public DateTime Inicio {
			get; set;
		}
		public DateTime UltimaJugada {
			get; set;
		}
		public Color Turno {
			get; set;
		}

		public Partida() {

		}

		public void Iniciar() {
			throw new NotImplementedException();
		}

		public bool Jugar(Jugada jugada) {
			if (this.ValidarJugada(jugada)) {
				this.Mover(jugada);
				this.GuardarJugada(jugada);
				this.ValidarTablero();
				return true;
			}
			this.Retroceder();
			this.ValidarTablero(); // No sé si será necesario revalidar el tablero en caso de un movimiento no válido
			return false;
		}

		private bool ValidarJugada(Jugada jugada) {
			throw new NotImplementedException();
		}

		private void Mover(Jugada jugada) {
			Pieza temp = this.Tablero[jugada.Origen.Traducir()[0], jugada.Origen.Traducir()[1]];
			this.Tablero[jugada.Origen.Traducir()[0], jugada.Origen.Traducir()[1]] = null;
			this.Tablero[jugada.Destino.Traducir()[0], jugada.Destino.Traducir()[1]] = temp;
		}

		private void GuardarJugada(Jugada jugada) {
			System.IO.Directory.CreateDirectory(@"C:\utal-2016-1-mds-t2");
			XDocument xDoc;
			if (!System.IO.File.Exists(@"C:\utal-2016-1-mds-t2\jugadas[ID].xml")) {
				xDoc = new XDocument(
					new XDeclaration("1.0", "utf-8", "yes"),
					new XComment("Lista de jugadas de la partida [ID]"),
					new XElement("Jugadas")
					);
			} else {
				xDoc = XDocument.Load(@"C:\utal-2016-1-mds-t2\jugadas[ID].xml");
			}
			xDoc.Element("Jugadas").Add(new XElement("Jugada", jugada.ToString()));
			xDoc.Save(@"C:\utal-2016-1-mds-t2\jugadas[ID].xml");
		}

		private bool ValidarTablero() {
			throw new NotImplementedException();
		}

		private void Retroceder() {
			throw new NotImplementedException();
		}

		private void CambiarTurno() {
			if (this.Turno == Color.BLANCO) {
				this.Turno = Color.NEGRO;
			} else {
				this.Turno = Color.NEGRO;
			}
		}
	}
}
