using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Xml;
using System.Xml.Linq;

namespace Ajedrez.Models {
	public class Partida {

        private string RutaXML = ConfigurationManager.AppSettings["RutaXML"];

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
			this.Turno = Color.BLANCO;
		}
        public Partida(long id){
            this.Seleccionar(id);
        }

		public void Iniciar() {
			Inicio = new DateTime();
			Tablero = new Pieza[8, 8];
			/* Jugador blanco */
			Tablero[7, 0] = new Pieza(Color.BLANCO, Tipo.TORRE);
			Tablero[7, 1] = new Pieza(Color.BLANCO, Tipo.CABALLO);
			Tablero[7, 2] = new Pieza(Color.BLANCO, Tipo.ALFIL);
			Tablero[7, 3] = new Pieza(Color.BLANCO, Tipo.REINA);
			Tablero[7, 4] = new Pieza(Color.BLANCO, Tipo.REY);
			Tablero[7, 5] = new Pieza(Color.BLANCO, Tipo.ALFIL);
			Tablero[7, 6] = new Pieza(Color.BLANCO, Tipo.CABALLO);
			Tablero[7, 7] = new Pieza(Color.BLANCO, Tipo.TORRE);
			for (int j = 0; j < 8; j++) {
				Tablero[6, j] = new Pieza(Color.BLANCO, Tipo.PEON);
			}
			/* Jugador negro */
			Tablero[0, 0] = new Pieza(Color.NEGRO, Tipo.TORRE);
			Tablero[0, 1] = new Pieza(Color.NEGRO, Tipo.CABALLO);
			Tablero[0, 2] = new Pieza(Color.NEGRO, Tipo.ALFIL);
			Tablero[0, 3] = new Pieza(Color.NEGRO, Tipo.REINA);
			Tablero[0, 4] = new Pieza(Color.NEGRO, Tipo.REY);
			Tablero[0, 5] = new Pieza(Color.NEGRO, Tipo.ALFIL);
			Tablero[0, 6] = new Pieza(Color.NEGRO, Tipo.CABALLO);
			Tablero[0, 7] = new Pieza(Color.NEGRO, Tipo.TORRE);
			for (int j = 0; j < 8; j++) {
				Tablero[1, j] = new Pieza(Color.NEGRO, Tipo.PEON);
			}
		}

		public bool Jugar(Jugada jugada) {
			if (!(this.ValidarJugada(jugada)))
				return false;
			this.Mover(jugada);
            if (!this.ValidarTablero()){
                this.Seleccionar(this.Id);
                return false;
            }
            this.GuardarJugada(jugada);
            this.Jugadas.Add(jugada);
			return true;
		}

        private void Seleccionar(long id)
        {
            ///a partir del xml que representa esta partida se deben extraer todas las propiedades que permitan levantar una partida ya existente
            throw new NotImplementedException();
        }

        private void Guardar()
        {
            ///Debe guardar en XML todas las propiedades de este objeto con el fin de poder levantarlas mas adelante desde el documento XML que la representa
            throw new NotImplementedException();
        }

        private bool ValidarJugada(Jugada jugada) {
			//valida que el espacio de llegada este disponible o ocupado por una pieza rival
			if (this.Tablero[jugada.Destino.Fila, jugada.Destino.Columna] != null &&
				this.Tablero[jugada.Destino.Fila, jugada.Destino.Columna].Color == this.Turno)
				return false;

			//valida que la posicion este ocupada por una pieza del turno actual
			var piezaEnMano = this.Tablero[jugada.Origen.Fila, jugada.Origen.Columna];
			if (piezaEnMano == null || piezaEnMano.Color != this.Turno)
				return false;

			//Valida que el movimiento pueda ser efectuado por la pieza
			var dX = jugada.Origen.Fila - jugada.Destino.Fila;
			var dY = jugada.Origen.Columna - jugada.Destino.Columna;
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
						if (this.Tablero[jugada.Destino.Fila, jugada.Destino.Columna] == null)
							return false;
					} else if (Math.Abs(dY) != 0)
						return false;

					if (Math.Abs(dX) == 2) {
						if (jugada.Origen.Fila != 1 || jugada.Origen.Fila != 6)
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
					if (Math.Abs(dY) == 1 && Math.Abs(dX) == 2)
						return true;
					if (Math.Abs(dX) == 1 && Math.Abs(dY) == 2)
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
            int[] cordInicio = { jugada.Origen.Fila, jugada.Origen.Columna};
			cordInicio[0] += dX;
			cordInicio[1] += dY;
			//comprueba que el camino este libre
			for (; cordInicio[0] != jugada.Destino.Fila || cordInicio[0] != jugada.Destino.Columna;
					cordInicio[0] += dX, cordInicio[1] += dY) {
				if (this.Tablero[cordInicio[0], cordInicio[1]] != null)
					return false;
			}
			return true;
		}

		private void Mover(Jugada jugada) {
			Pieza temp = this.Tablero[jugada.Origen.Fila, jugada.Origen.Columna];
			this.Tablero[jugada.Origen.Fila, jugada.Origen.Columna] = null;
			this.Tablero[jugada.Destino.Fila, jugada.Destino.Columna] = temp;
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
		<Fila>" + jugada.Origen.Fila + @"</Fila>
		<Columna>" + jugada.Origen.Columna + @"</Columna>
	</Origen>
	<Destino>
		<Fila>" + jugada.Destino.Fila + @"</Fila>
		<Columna>" + jugada.Destino.Columna + @"</Columna>
	</Destino>
  </Jugada>";
			xmlDoc.FirstChild.AppendChild(xmldf);
			xmlDoc.Save(RutaXMLPartida);

		}

		private bool ValidarTablero_Caballo(int posicionCaballoX, int posicionCaballoY, int posicionReyX, int posicionReyY) {
			if (posicionReyX + 1 == posicionCaballoX && posicionReyY + 2 == posicionCaballoY) {
				return true;
			}
			if (posicionReyX - 1 == posicionCaballoX && posicionReyY + 2 == posicionCaballoY) {
				return true;
			}
			if (posicionReyX + 1 == posicionCaballoX && posicionReyY - 2 == posicionCaballoY) {
				return true;
			}
			if (posicionReyX - 1 == posicionCaballoX && posicionReyY - 2 == posicionCaballoY) {
				return true;
			}
			if (posicionReyX + 2 == posicionCaballoX && posicionReyY + 1 == posicionCaballoY) {
				return true;
			}
			if (posicionReyX - 2 == posicionCaballoX && posicionReyY + 1 == posicionCaballoY) {
				return true;
			}
			if (posicionReyX + 2 == posicionCaballoX && posicionReyY - 1 == posicionCaballoY) {
				return true;
			}
			if (posicionReyX - 2 == posicionCaballoX && posicionReyY - 1 == posicionCaballoY) {
				return true;
			}
			return false;
		}

		private bool ValidarTablero() {
			// Turno contrario
			Color turnoOponente = (this.Turno == Color.BLANCO) ? Color.NEGRO : Color.BLANCO;
			// Buscar la posición del rey
			int i, j, posicionX = -1, posicionY = -1;
			// Si es blanco
			if (this.Turno == Color.BLANCO) {
				for (i = 0; i < 8; i++) {
					for (j = 0; j < 8; j++) {
						Pieza piezaRey = this.Tablero[i, j];
						if (piezaRey != null && piezaRey.Tipo == Tipo.REY && piezaRey.Color == Color.NEGRO) {
							posicionX = i;
							posicionY = j;
						}
					}
				}
				// Si es negro
			} else {
				for (i = 0; i < 8; i++) {
					for (j = 0; j < 8; j++) {
						Pieza piezaRey = this.Tablero[i, j];
						if (piezaRey != null && piezaRey.Tipo == Tipo.REY && piezaRey.Color == Color.BLANCO) {
							posicionX = i;
							posicionY = j;
						}
					}
				}
			}
			// Mirar horizontalmente (a la izquierda) si el rey está en jaque
			for (i = posicionX - 1; i >= 0; i--) {
				// Encontrar la primera pieza
				Pieza piezaEncontrada = this.Tablero[i, posicionY];
				if (piezaEncontrada != null) {
					// Revisar si la pieza corresponde a una del oponente
					if (piezaEncontrada.Color == turnoOponente) {
						// Se salvó
						break;
					} else {
						// Revisar si es una torre o una reina
						if (piezaEncontrada.Tipo == Tipo.TORRE || piezaEncontrada.Tipo == Tipo.REINA) {
							// Jaque
							return true;
						} else {
							// Se salvó
							break;
						}
					}
				}
			}
			// Mirar horizontalmente (a la derecha) si el rey está en jaque
			for (i = posicionX + 1; i < 8; i++) {
				// Encontrar la primera pieza
				Pieza piezaEncontrada = this.Tablero[i, posicionY];
				if (piezaEncontrada != null) {
					// Revisar si la pieza corresponde a una del oponente
					if (piezaEncontrada.Color == turnoOponente) {
						// Se salvó
						break;
					} else {
						// Revisar si es una torre o una reina
						if (piezaEncontrada.Tipo == Tipo.TORRE || piezaEncontrada.Tipo == Tipo.REINA) {
							// Jaque
							return true;
						} else {
							// Se salvó
							break;
						}
					}
				}
			}
			// Mirar verticalmente (arriba) si el rey está en jaque
			for (j = posicionY - 1; j >= 0; j--) {
				// Encontrar la primera pieza
				Pieza piezaEncontrada = this.Tablero[posicionX, j];
				if (piezaEncontrada != null) {
					// Revisar si la pieza corresponde a una del oponente
					if (piezaEncontrada.Color == turnoOponente) {
						// Se salvó
						break;
					} else {
						// Revisar si es una torre o una reina
						if (piezaEncontrada.Tipo == Tipo.TORRE || piezaEncontrada.Tipo == Tipo.REINA) {
							// Jaque
							return true;
						} else {
							// Se salvó
							break;
						}
					}
				}
			}
			// Mirar verticalmente (abajo) si el rey está en jaque
			for (j = posicionY + 1; j < 8; j++) {
				// Encontrar la primera pieza
				Pieza piezaEncontrada = this.Tablero[posicionX, j];
				if (piezaEncontrada != null) {
					// Revisar si la pieza corresponde a una del oponente
					if (piezaEncontrada.Color == turnoOponente) {
						// Se salvó
						break;
					} else {
						// Revisar si es una torre o una reina
						if (piezaEncontrada.Tipo == Tipo.TORRE || piezaEncontrada.Tipo == Tipo.REINA) {
							// Jaque
							return true;
						} else {
							// Se salvó
							break;
						}
					}
				}
			}
			// Mirar diagonamlente (Noroeste) si el rey está en jaque
			i = posicionX - 1;
			j = posicionY - 1;
			while (i >= 0 && j >= 0) {
				// Encontrar la primera pieza
				Pieza piezaEncontrada = this.Tablero[i, j];
				if (piezaEncontrada != null) {
					// Revisar si la pieza corresponde a una del oponente
					if (piezaEncontrada.Color == turnoOponente) {
						// Se salvó
						break;
					} else {
						// Revisar si es un alfil o una reina
						if (piezaEncontrada.Tipo == Tipo.ALFIL || piezaEncontrada.Tipo == Tipo.REINA) {
							// Jaque
							return true;
							// Revisar si es un peón
						} else if (piezaEncontrada.Tipo == Tipo.PEON && i == posicionX - 1 && piezaEncontrada.Color == Color.NEGRO) {
							// Jaque
							return true;
						} else {
							// Se salvó
							break;
						}
					}
				}
				i--;
				j--;
			}
			// Mirar diagonamlente (Noreste) si el rey está en jaque
			i = posicionX + 1;
			j = posicionY - 1;
			while (i < 8 && j >= 0) {
				// Encontrar la primera pieza
				Pieza piezaEncontrada = this.Tablero[i, j];
				if (piezaEncontrada != null) {
					// Revisar si la pieza corresponde a una del oponente
					if (piezaEncontrada.Color == turnoOponente) {
						// Se salvó
						break;
					} else {
						// Revisar si es un alfil o una reina
						if (piezaEncontrada.Tipo == Tipo.ALFIL || piezaEncontrada.Tipo == Tipo.REINA) {
							// Jaque
							return true;
							// Revisar si es un peón
						} else if (piezaEncontrada.Tipo == Tipo.PEON && i == posicionX + 1 && piezaEncontrada.Color == Color.NEGRO) {
							// Jaque
							return true;
						} else {
							// Se salvó
							break;
						}
					}
				}
				i++;
				j--;
			}
			// Mirar diagonamlente (Sureste) si el rey está en jaque
			i = posicionX + 1;
			j = posicionY + 1;
			while (i < 8 && j < 8) {
				// Encontrar la primera pieza
				Pieza piezaEncontrada = this.Tablero[i, j];
				if (piezaEncontrada != null) {
					// Revisar si la pieza corresponde a una del oponente
					if (piezaEncontrada.Color == turnoOponente) {
						// Se salvó
						break;
					} else {
						// Revisar si es un alfil o una reina
						if (piezaEncontrada.Tipo == Tipo.ALFIL || piezaEncontrada.Tipo == Tipo.REINA) {
							// Jaque
							return true;
							// Revisar si es un peón
						} else if (piezaEncontrada.Tipo == Tipo.PEON && i == posicionX + 1 && piezaEncontrada.Color == Color.BLANCO) {
							// Jaque
							return true;
						} else {
							// Se salvó
							break;
						}
					}
				}
				i++;
				j++;
			}
			// Mirar diagonamlente (Suroeste) si el rey está en jaque
			i = posicionX - 1;
			j = posicionY + 1;
			while (i >= 0 && j < 8) {
				// Encontrar la primera pieza
				Pieza piezaEncontrada = this.Tablero[i, j];
				if (piezaEncontrada != null) {
					// Revisar si la pieza corresponde a una del oponente
					if (piezaEncontrada.Color == turnoOponente) {
						// Se salvó
						break;
					} else {
						// Revisar si es un alfil o una reina
						if (piezaEncontrada.Tipo == Tipo.ALFIL || piezaEncontrada.Tipo == Tipo.REINA) {
							// Jaque
							return true;
							// Revisar si es un peón
						} else if (piezaEncontrada.Tipo == Tipo.PEON && i == posicionX - 1 && piezaEncontrada.Color == Color.BLANCO) {
							// Jaque
							return true;
						} else {
							// Se salvó
							break;
						}
					}
				}
				i--;
				j++;
			}
			// Buscar caballo
			for (i = 0; i < 8; i++) {
				for (j = 0; j < 8; j++) {
					Pieza caballo = this.Tablero[i, j];
					// Ver si efectivamente es un caballo del que está jugando (el computador no sabe si lo es ¬¬)
					if (caballo != null && caballo.Tipo == Tipo.CABALLO && caballo.Color == this.Turno) {
						if (this.ValidarTablero_Caballo(i, j, posicionX, posicionY)) {
							// Jaque
							return true;
						}
					}
				}
			}
			// Definitivamente se salvó...
			return false;
		}

		private void CambiarTurno() {
			if (this.Turno == Color.BLANCO) {
				this.Turno = Color.NEGRO;
			} else {
				this.Turno = Color.BLANCO;
			}
		}

		public void Pintar() {
			int i, j;
			for (i = 0; i < 8; i++) {
				for (j = 0; j < 8; j++) {
					Pieza pieza = this.Tablero[i, j];
					if (pieza == null) {
						Console.Write("xx ");
					} else if (pieza.Tipo == Tipo.PEON && pieza.Color == Color.BLANCO) {
						Console.Write("Pb ");
					} else if (pieza.Tipo == Tipo.PEON && pieza.Color == Color.NEGRO) {
						Console.Write("Pn ");
					} else if (pieza.Tipo == Tipo.ALFIL && pieza.Color == Color.BLANCO) {
						Console.Write("Ab ");
					} else if (pieza.Tipo == Tipo.ALFIL && pieza.Color == Color.NEGRO) {
						Console.Write("An ");
					} else if (pieza.Tipo == Tipo.CABALLO && pieza.Color == Color.BLANCO) {
						Console.Write("Cb ");
					} else if (pieza.Tipo == Tipo.CABALLO && pieza.Color == Color.NEGRO) {
						Console.Write("Cn ");
					} else if (pieza.Tipo == Tipo.REINA && pieza.Color == Color.BLANCO) {
						Console.Write("Qb ");
					} else if (pieza.Tipo == Tipo.REINA && pieza.Color == Color.NEGRO) {
						Console.Write("Qn ");
					} else if (pieza.Tipo == Tipo.REY && pieza.Color == Color.BLANCO) {
						Console.Write("Kb ");
					} else if (pieza.Tipo == Tipo.REY && pieza.Color == Color.NEGRO) {
						Console.Write("Kn ");
					} else if (pieza.Tipo == Tipo.TORRE && pieza.Color == Color.BLANCO) {
						Console.Write("Tb ");
					} else if (pieza.Tipo == Tipo.TORRE && pieza.Color == Color.NEGRO) {
						Console.Write("Tn ");
					}
				}
				Console.WriteLine("");
			}
		}
	}
}
