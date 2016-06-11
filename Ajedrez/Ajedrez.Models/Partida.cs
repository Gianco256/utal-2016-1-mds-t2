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
			Inicio = new DateTime();
			for (int y = 0; y < 2; y++) {
				for (int x = 0; x < 4; x++) {
					switch (x) {
						case 0:
							Tablero[x, y] = new Pieza();
							Tablero[x, y].Tipo = Tipo.TORRE;
							Tablero[4 + x, y] = new Pieza();
							Tablero[4 + x, y].Tipo = Tipo.TORRE;
							break;
						case 1:
							Tablero[x, y] = new Pieza();
							Tablero[x, y].Tipo = Tipo.CABALLO;
							Tablero[4 + x, y] = new Pieza();
							Tablero[4 + x, y].Tipo = Tipo.CABALLO;
							break;
						case 2:
							Tablero[x, y] = new Pieza();
							Tablero[x, y].Tipo = Tipo.ALFIL;
							Tablero[4 + x, y] = new Pieza();
							Tablero[4 + x, y].Tipo = Tipo.ALFIL;
							break;
						case 3:
							Tablero[x, y] = new Pieza();
							Tablero[x, y].Tipo = Tipo.REY;
							Tablero[4 + x, y] = new Pieza();
							Tablero[4 + x, y].Tipo = Tipo.REINA;
							break;
					}
				}
			}
		}

		public bool Jugar(Jugada jugada) {
			if (!(this.ValidarJugada(jugada)))
				return false;
			var xy = jugada.Destino.Traducir();
			var tmp = this.Tablero[xy[0], xy[1]];

			this.Mover(jugada);
			this.GuardarJugada(jugada);
			if (!(this.ValidarTablero())) {
				this.Retroceder();
				this.Tablero[xy[0], xy[1]] = tmp;
				return false;
			}
			return true;
		}

		private bool ValidarJugada(Jugada jugada) {
			var cord = jugada.Origen.Traducir();
			if (this.Tablero[cord[0], cord[1]].Color != this.Turno)
				return false;
			cord = jugada.Destino.Traducir();
			if (this.Tablero[cord[0], cord[1]].Color == this.Turno)
				return false;
			return true;
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
			for (int x = 0; x < Tablero.GetLength(0); x++) {
				for (int y = 0; y < Tablero.GetLength(1); y++) {
					if (Tablero[x, y].Tipo.Equals(Tipo.REY)) {
						/*
                            Verificar si el rey puede moverse a su alrededor(JAQUE)
                            Si puede moverse retornamos TRUE, si no, el ciclo continua hasta terminar 
                            y arrojar FALSE.
                        */
					}
				}
			}
			return false;
		}

		private void Retroceder() {
			throw new NotImplementedException();
		}

		private void CambiarTurno() {
			if (this.Turno == Color.BLANCO) {
				this.Turno = Color.NEGRO;
			} else {
				this.Turno = Color.BLANCO;
			}
		}
	}
}
