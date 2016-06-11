using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Xml;
using System.Xml.Linq;

namespace Ajedrez.Models {
	public class Cuenta {
		private const string RutaXML = @"C:\Ajedrez";
		private const string RutaXMLCuentas = @"C:\Ajedrez\Cuentas.xml";
		private const string RutaXMLJugadores = @"C:\Ajedrez\Jugadores.xml";

		public string Email {
			get; set;
		}
		public string Password {
			get; set;
		}
		public DateTime UltimoAcceso {
			get; set;
		}
		public Jugador JugadorActual {
			get; set;
		}

		public Cuenta() {
		}

		public Cuenta(string email, string password, DateTime ultimoAcceso, Jugador jugadorActual) {
			this.Email = email;
			this.Password = password;
			this.UltimoAcceso = ultimoAcceso;
			this.JugadorActual = jugadorActual;
		}

		public bool Registrar(string email, string password) {
			System.IO.Directory.CreateDirectory(RutaXML);
			XmlDocument xmlDoc = new XmlDocument();
			if (!System.IO.File.Exists(RutaXMLCuentas)) {
				xmlDoc.LoadXml("<Cuentas></Cuentas>");
			} else {
				xmlDoc.Load(RutaXMLCuentas);
			}
			if (!this.Seleccionar(email)) {
				var xmldf = xmlDoc.CreateDocumentFragment();
				xmldf.InnerXml = @"<Cuenta>
                        <Email>" + email + @"</Email>
                        <Password>" + password + @"</Password>
                        <UltimoAcceso>" + DateTime.Now.Ticks + @"</UltimoAcceso>
                        <JugadorActual></JugadorActual>
                    </Cuenta>";
				xmlDoc.FirstChild.AppendChild(xmldf);
				xmlDoc.Save(RutaXMLCuentas);
				return true;
			}
			return false;
		}

		public bool Seleccionar(string email) {
			try {
				XmlDocument xmlDoc = new XmlDocument();
				xmlDoc.Load(RutaXMLCuentas);
				var xmlCuenta = xmlDoc.SelectSingleNode("/Cuentas/Cuenta[Email = '" + email + "']");
				if (xmlCuenta == null) {
					return false;
				} else {
					return true;
				}
			} catch (Exception ex) {
				return false;
			}
		}

		public bool IniciarSesion() {
			XmlDocument xmlDoc = new XmlDocument();
			xmlDoc.Load(RutaXMLCuentas);
			var xmlCuenta = xmlDoc.SelectSingleNode("/Cuentas/Cuenta[Email = '" + this.Email + "' and Password = '" + this.Password + "']");
			if (xmlCuenta == null) {
				return false;
			} else {
				this.UltimoAcceso = DateTime.Now;
				xmlDoc.Save(RutaXMLCuentas);
				return true;
			}
		}

		public void CerrarSesion() {
			throw new NotImplementedException();
		}

		public List<Jugador> Jugadores() {
			XmlDocument xmlDoc = new XmlDocument();
			List<Jugador> acum = new List<Jugador>();
			if (!System.IO.File.Exists(RutaXMLJugadores))
				return acum;
			else
				xmlDoc.Load(RutaXMLJugadores);

			var entrada = xmlDoc.SelectNodes("/Jugadores/Jugador[Email='" + Email + "']");

			foreach (XmlNode th in entrada) {
				acum.Add(new Jugador() {
					Id = Int64.Parse(th["Id"].InnerText),
					Nick = th["Nick"].InnerText,
					Sexo = (Sexo) Convert.ToInt32(th["Sexo"].InnerText),
					FechaNacimiento = new DateTime(Int64.Parse(th["FechaNacimiento"].InnerText)),
				});
			}
			return acum;
		}

		public void CambiarJugadorActivo(Jugador jugador) {
			XmlDocument xmlDoc = new XmlDocument();
			if (!System.IO.File.Exists(RutaXMLJugadores))
				return;
			else
				xmlDoc.Load(RutaXMLJugadores);

			if (xmlDoc.SelectSingleNode("/Jugadores/Jugador['Id=" + jugador.Id + " Email=" + this.Email + "]") != null) {
				xmlDoc.Load(RutaXMLCuentas);
				var cuenta = xmlDoc.SelectSingleNode("//Cuenta[Email='" + this.Email + "']");
				if (cuenta != null) {
					cuenta["JugadorActual"].InnerText = jugador.Id.ToString();
					this.JugadorActual = jugador;
					xmlDoc.Save(RutaXMLCuentas);
				}
			}
		}

		public bool CrearJugador(Jugador jugador) {
			XmlDocument xmlDoc = new XmlDocument();
			if (!System.IO.File.Exists(RutaXMLJugadores)) {
				xmlDoc.LoadXml("<Jugadores></Jugadores>");
			} else
				xmlDoc.Load(RutaXMLJugadores);

			var xmlJugador = xmlDoc.SelectSingleNode("//Jugadores/Jugador[Email='" + this.Email + "']");
			if (xmlJugador == null) {
				var xmldf = xmlDoc.CreateDocumentFragment();
				xmldf.InnerXml = @"<Jugador>
                    <Email>" + this.Email + @"</Email>
                    <Id>" + jugador.Id + @"</Id>
                    <Nick>" + jugador.Nick + @"</Nick>
                    <Sexo>" + Convert.ToInt32(jugador.Sexo) + @"</Sexo>
                    <FechaNacimiento>" + jugador.FechaNacimiento.Ticks + @"</FechaNacimiento>
                </Jugador>";
				xmlDoc.SelectSingleNode("/Jugadores").AppendChild(xmldf);

				xmlDoc.Save(RutaXMLJugadores);
				return true;
			}
			return false;
		}

		public bool EliminarJugador(Jugador jugador) {
			if (!System.IO.File.Exists(RutaXMLJugadores) || jugador == null)
				return false;

			XmlDocument xmlDoc = new XmlDocument();
			xmlDoc.Load(RutaXMLJugadores);
			var elemjugador = xmlDoc.SelectSingleNode("/Jugadores/Jugador['Id=" + jugador.Id + " Id-Cuenta=" + this.Email + "]");
			if (elemjugador != null) {
				elemjugador.ParentNode.RemoveChild(elemjugador);
				xmlDoc.Save(RutaXMLJugadores);
				return true;
			}
			return false;
		}

		public bool Desactivar() {
			try {
				XmlDocument xmlDoc = new XmlDocument();
				xmlDoc.Load(RutaXMLCuentas);

				var xmlCuenta = xmlDoc.SelectSingleNode("/Cuentas/Cuenta[Email = '" + this.Email + "' and Password = '" + this.Password + "']");
				if (xmlCuenta == null) {
					return false;
				} else {
					xmlCuenta.ParentNode.RemoveChild(xmlCuenta);
					xmlDoc.Save(RutaXMLCuentas);
				}
				return true;
			} catch (Exception ex) {
				return false;
			}
		}
	}
}
