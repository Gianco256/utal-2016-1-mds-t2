using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Xml;
using System.Xml.Linq;

namespace Ajedrez.Models {
	public class Partida {
		private const string RutaXML = @"C:\Ajedrez";
		private string RutaXMLPartida;

		private Pieza[,] Tablero;
		private List<Jugada> Jugadas;
		public long Id {
			get; set;
		}
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
			this.Id = (new Random()).Next();
			this.RutaXMLPartida = RutaXML + @"\Partida" + Convert.ToString(this.Id);
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
			//valida que el espacio de llegada este disponible o ocupado por una pieza rival
			var cordLlegada = jugada.Destino.Traducir();
			if (this.Tablero[cordLlegada[0], cordLlegada[1]] != null &&
				this.Tablero[cordLlegada[0], cordLlegada[1]].Color == this.Turno)
				return false;

			//valida que la posicion este ocupada por una pieza del turno actual
			var cordInicio = jugada.Origen.Traducir();
			var piezaEnMano = this.Tablero[cordInicio[0], cordInicio[1]];
			if (piezaEnMano == null || piezaEnMano.Color != this.Turno)
				return false;

			//Valida que el movimiento pueda ser efectuado por la pieza
			var dX = cordInicio[0] - cordLlegada[0];
			var dY = cordInicio[1] - cordLlegada[1];
			switch (piezaEnMano.Tipo) {
				case Tipo.PEON:
					//comprueba que el movimiento sea hacia adelante
					//TODO: ratificar comprobaciones con el llenado del tablero
					if (piezaEnMano.Color == Color.BLANCO) {
						if (dX <= 0)
							return false;
					} else {
						if (dX >= 0)
							return false;
					}

					if (Math.Abs(dY) == 1) {
						//posiblemente come
						if (this.Tablero[cordLlegada[0], cordLlegada[1]] == null)
							return false;
					} else if (Math.Abs(dY) != 0)
						return false;

					if (Math.Abs(dX) == 2) {
						if (cordInicio[0] != 1 || cordInicio[0] != 6)
							return false;
					} else if (Math.Abs(dX) != 1)
						return false;
					break;
				case Tipo.TORRE:
					if (dX != 0 && dY != 0)
						return false;
					break;
				case Tipo.CABALLO:
					// el unico que se comprueba diferente
					if (Math.Abs(dY) == 1 && Math.Abs(dX) == 3)
						return true;
					if (Math.Abs(dX) == 1 && Math.Abs(dY) == 3)
						return true;
					return false;
				case Tipo.ALFIL:
					if (Math.Abs(dX) != Math.Abs(dY))
						return false;
					break;
				case Tipo.REY:
					if (Math.Abs(dX) > 1 || Math.Abs(dY) > 1)
						return false;
					break;
				case Tipo.REINA:
					if ((dX != 0 && dY != 0) || Math.Abs(dX) != Math.Abs(dY))
						return false;
					break;
				default:
					return false;
			}
			cordInicio[0] += dX;
			cordInicio[1] += dY;
			//comprueba que el camino este libre
			for (; cordInicio[0] != cordLlegada[0] || cordInicio[0] != cordLlegada[0];
					cordInicio[0] += dX, cordInicio[1] += dY) {
				if (this.Tablero[cordInicio[0], cordInicio[1]] != null)
					return false;
			}
			return true;
		}

		private void Mover(Jugada jugada) {
			Pieza temp = this.Tablero[jugada.Origen.Traducir()[0], jugada.Origen.Traducir()[1]];
			this.Tablero[jugada.Origen.Traducir()[0], jugada.Origen.Traducir()[1]] = null;
			this.Tablero[jugada.Destino.Traducir()[0], jugada.Destino.Traducir()[1]] = temp;
		}

		private void GuardarJugada(Jugada jugada) {
			System.IO.Directory.CreateDirectory(RutaXML);
			XmlDocument xmlDoc = new XmlDocument();
			if (!System.IO.File.Exists(this.RutaXMLPartida)) {
				xmlDoc.LoadXml("<Jugadas></Jugadas>");
			} else {
				xmlDoc.Load(this.RutaXMLPartida);
			}
			var xmldf = xmlDoc.CreateDocumentFragment();
			xmldf.InnerXml =
@"<Jugada>
	<Origen>
		<Fila>" + jugada.Origen.Traducir()[0] + @"</Fila>
		<Columna>" + jugada.Origen.Traducir()[1] + @"</Columna>
	</Origen>
	<Destino>
		<Fila>" + jugada.Destino.Traducir()[0] + @"</Fila>
		<Columna>" + jugada.Destino.Traducir()[1] + @"</Columna>
	</Destino>
</Jugada>";
			xmlDoc.FirstChild.AppendChild(xmldf);
			xmlDoc.Save(RutaXMLPartida);

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
