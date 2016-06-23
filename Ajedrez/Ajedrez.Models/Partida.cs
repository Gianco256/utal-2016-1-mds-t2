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
			get; private set;
		}
		public long IdBlancas {
			get; set;
		}
		public long IdNegras {
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
			this.Id = (new Random()).Next();//??
			this.RutaXMLPartida = RutaXML + @"\Partida" + this.Id.ToString() + ".xml";
			this.Turno = Color.BLANCO;
		}
		public Partida(long id) {
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
			this.CambiarTurno();
			if (!this.ValidarTablero()) {
				this.Seleccionar(this.Id);
				return false;
			}
			this.GuardarJugada(jugada);
			this.Jugadas.Add(jugada);
			return true;
		}

		private bool Seleccionar(long id) {
			///a partir del xml que representa esta partida se deben extraer todas las propiedades que permitan levantar una partida ya existente
			this.RutaXMLPartida = RutaXML + @"\Partida" + id.ToString() + ".xml";
			if (!System.IO.File.Exists(RutaXMLPartida))
				return false;
			this.Id = id;
			XmlDocument xDoc = new XmlDocument();
			xDoc.Load(this.RutaXMLPartida);

			this.Inicio = new DateTime(Convert.ToInt64(xDoc.SelectSingleNode("/Inicio").InnerText));
			this.UltimaJugada = new DateTime(Convert.ToInt64(xDoc.SelectSingleNode("/UltimaJugada").InnerText));
			this.Turno = (Color) Convert.ToInt32(xDoc.SelectSingleNode("/Turno").InnerText);
			this.IdBlancas = Convert.ToInt64(xDoc.SelectSingleNode("/IdBlancas").InnerText);
			this.IdNegras = Convert.ToInt64(xDoc.SelectSingleNode("/IdNegras").InnerText);
			//llenado del tablero
			this.Tablero = new Pieza[8, 8];
			for (int x = 0; x < 8; x++) {
				for (int y = 0; y < 8; y++) {
					var casilla = xDoc.SelectSingleNode("/Tablero/casilla[Fila='" + x.ToString() + "' and Columna='" + y.ToString() + "']");
					if (casilla["Tipo"].InnerText == "null")
						this.Tablero[x, y] = null;
					else
						this.Tablero[x, y] = new Pieza((Color) Convert.ToInt32(casilla["Color"].InnerText), (Tipo) Convert.ToInt32(casilla["Tipo"].InnerText));
				}
			}
			XmlNodeList jugs = xDoc.SelectNodes("/Jugadas/jugada");
			this.Jugadas = new List<Jugada>();
			foreach (XmlNode jug in jugs) {
				this.Jugadas.Add(new Jugada() {
					Origen = new Coordenada() {
						Fila = Convert.ToInt32(jug["Origen"]["Fila"].InnerText),
						Columna = Convert.ToInt32(jug["Origen"]["Columna"].InnerText),
					},
					Destino = new Coordenada() {
						Fila = Convert.ToInt32(jug["Destino"]["Fila"].InnerText),
						Columna = Convert.ToInt32(jug["Destino"]["Columna"].InnerText),
					}
				});
			}


			return true;
		}

		private bool Guardar() {
			///Debe guardar en XML todas las propiedades de este objeto con el fin de poder levantarlas mas adelante desde el documento XML que la representa
			XmlDocument xDoc= new XmlDocument();
			if (System.IO.File.Exists(RutaXMLPartida)){
				xDoc.Load(this.RutaXMLPartida);
				if(xDoc.SelectSingleNode("/UltimaJugada").InnerText != this.UltimaJugada.Ticks.ToString()) return false;
			}
			
			String xTableroFrag = "";
			for (int x = 0; x < 8; x++) {
				for (int y = 0; y < 8; y++) {
					xTableroFrag+= 
					@"<casilla>
						<Fila>" + x.ToString() + @"</Fila>
						<Columna>" + y.ToString() + @"</Columna>
					";
					if(this.tablero[x, y]==null){
						xTableroFrag+= 
							@"<Tipo>null</Tipo>
							<Color>null</Color>
						</casilla>
						";
					}else{
						xTableroFrag+=
							@"<Tipo>" + ((int)this.tablero[x, y].Tipo).ToString() + @"</Tipo>
							<Color>" + ((int)this.tablero[x, y].Color).ToString() + @"</Color>
						</casilla>
						";
					}
				}
			}
			String xJugadasFrag= "";
			foreach(var jug in this.Jugadas){
				xJugadasFrag+= 
				@"<jugada>
					<Origen>
						<Fila>" + jug.Origen.Fila.ToString() + @"</Fila>
						<Columna>" + jug.Origen.Columna.ToString() + @"</Columna>
					</Origen>
					<Destino>
						<Fila>" + jug.Destino.Fila.ToString() + @"</Fila>
						<Columna>" + jug.Destino.Columna.ToString() + @"</Columna>
					</Destino>
				</jugada>
				";
			}
			xPartida= 
			@"<Id>" + this.Id.ToString() + @"</Id>
			<Inicio>" + this.Inicio.Ticks.ToString() + @"</Inicio>
			<UltimaJugada>" + this.UltimaJugada.Ticks.ToString() + @"</UltimaJugada>
			<Turno>" + ((int)this.Turno).ToString() + @"</Turno>
			<IdBlancas>" + this.IdBlancas.ToString() + @"</IdBlancas>
			<IdNegras>" + this.IdNegras.ToString() + @"</IdNegras>
			<Jugadas>" + xJugadasFrag + @"</Jugadas>
			<Tablero>" + xTableroFrag + @"</Tablero>";
			xDoc.LoadXml(xPartida);
			xDoc.Save(this.RutaXMLPartida);
			return true;
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
						if (dX >= 0)
							return false;
					} else {
						if (dX <= 0)
							return false;
					}

					if (Math.Abs(dY) == 1) {
						//posiblemente come
						if (this.Tablero[jugada.Destino.Fila, jugada.Destino.Columna] == null)
							return false;
					} else if (Math.Abs(dY) != 0)
						return false;

					if (Math.Abs(dX) == 2) {
						if ((piezaEnMano.Color == Color.NEGRO && jugada.Origen.Fila != 1) || (piezaEnMano.Color==Color.BLANCO && jugada.Origen.Fila != 6)
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
			int[] cordInicio = { jugada.Origen.Fila, jugada.Origen.Columna };
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

		private bool ValidarTablero() {
			Jugada jug= new Jugada();
			for(int fila= 0; fila<8; fila++){
				for(int colum= 0; colum<8; colum++){
					if(this.tablero[fila, colum]!=null && 
						this.tablero[fila, colum].Tipo == Tipo.REY &&
						this.tablero[fila, colum].Color != this.Turno)
					{
						jug.Destino.Fila= fila;
						jug.Destino.Columna= colum;
						fila= 8;
						colum= 8;
					}
				}
			}
			for(int jug.Origen.Fila= 0; jug.Origen.Fila<8; jug.Origen.Fila++){
				for(int jug.Origen.Columna= 0; jug.Origen.Columna<8; jug.Origen.Columna++){
					if(this.ValidarJugada(jug)) return false;
				}
			}
			return true;
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
