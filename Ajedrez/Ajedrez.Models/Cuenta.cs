using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Xml;
using System.Xml.Linq;

namespace Ajedrez.Models {
	public class Cuenta {
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
			System.IO.Directory.CreateDirectory(@"C:\utal-2016-1-mds-t2");
			XDocument xDoc;
			if (!System.IO.File.Exists(@"C:\utal-2016-1-mds-t2\cuentas.xml")) {
				xDoc = new XDocument(
				new XDeclaration("1.0", "utf-8", "yes"),
				new XComment("Lista de cuentas"),
				new XElement("Cuentas")
				);
			} else {
				xDoc = XDocument.Load(@"C:\utal-2016-1-mds-t2\cuentas.xml");
			}
			if (!this.Seleccionar(email)) {
				xDoc.Element("Cuentas").Add(new XElement("Cuenta",
													new XElement("Email", email),
													new XElement("Password", password),
													new XElement("UltimoAcceso", DateTime.Now),
													new XElement("JugadorActual")
							));
				xDoc.Save(@"C:\utal-2016-1-mds-t2\cuentas.xml");
				return true;
			}
			return false;
		}

		public bool Seleccionar(string email) {
			try {
				XmlDocument xDoc = new XmlDocument();
				xDoc.Load(@"C:\utal-2016-1-mds-t2\cuentas.xml");
				var xCuenta = xDoc.SelectSingleNode("/Cuentas/Cuenta[Email = '" + email + "']");
				if (xCuenta == null) {
					return false;
				} else {
					return true;
				}
			} catch (Exception ex) {
				return false;
			}
		}

		public bool IniciarSesion() {
			XmlDocument xDoc = new XmlDocument();
			xDoc.Load(@"C:\utal-2016-1-mds-t2\cuentas.xml");
			var xCuenta = xDoc.SelectSingleNode("/Cuentas/Cuenta[Email = '" + this.Email + "' and Password = '" + this.Password + "']");
			if (xCuenta == null) {
				return false;
			} else {
				this.UltimoAcceso = DateTime.Now;
				xDoc.Save(@"C:\utal-2016-1-mds-t2\cuentas.xml");
				return true;
			}
		}

		public void CerrarSesion() {
			throw new NotImplementedException();
		}

		public List<Jugador> Jugadores() {
            XDocument xDoc;
            List<Jugador> acum = new List<Jugador>();
            if (!System.IO.File.Exists(@"C:\utal-2016-1-mds-t2\jugadores.xml")) return acum;
            else xDoc = XDocument.Load(@"C:\utal-2016-1-mds-t2\jugadores.xml");
            

            List<XElement> entrada= (List<XElement>)xDoc.Elements("/Jugadores/Jugador['Id-Cuenta=" + Email + "]");

            foreach(XElement th in entrada){
                acum.Add(new Jugador() {
                    Id = Int64.Parse(th.Attribute("['Id']").Value),
                    Nick = th.Attribute("['Nick']").Value,
                    Sexo = (Sexo)Int32.Parse(th.Attribute("['Sexo']").Value),
                    FechaNacimiento = new DateTime(Int64.Parse(th.Attribute("['FechaNacimiento']").Value)),
                });
            }
            return acum;
        }

		public void CambiarJugadorActivo(Jugador jugador) {
            XDocument xDoc;
            if (!System.IO.File.Exists(@"C:\utal-2016-1-mds-t2\jugadores.xml")) return;
            else xDoc = XDocument.Load(@"C:\utal-2016-1-mds-t2\jugadores.xml");

            if (xDoc.Element("/Jugadores/Jugador['Id=" + jugador.Id + " Id-Cuenta=" + this.Email + "]") != null)
            {
                this.JugadorActual = jugador;
            }
        }

		public bool CrearJugador(Jugador jugador) {
            XDocument xDoc;
            if (!System.IO.File.Exists(@"C:\utal-2016-1-mds-t2\jugadores.xml"))
            {
                xDoc = new XDocument(
                new XDeclaration("1.0", "utf-8", "yes"),
                new XComment("Lista de jugadores"),
                new XElement("Jugadores")
                );
            }else xDoc = XDocument.Load(@"C:\utal-2016-1-mds-t2\jugadores.xml");

            if(xDoc.Element("/Jugadores/Jugador['Id=" + jugador.Id + "]")==null){
                xDoc.Element("Jugadores").Add(new XElement("Jugador",
                                                    new XElement("Id-Cuenta", this.Email),
                                                    new XElement("Id", jugador.Id),
                                                    new XElement("Nick", jugador.Nick),
                                                    new XElement("Sexo", jugador.Sexo),
                                                    new XElement("FechaNacimiento", jugador.FechaNacimiento.Ticks)
                            ));
                xDoc.Save(@"C:\utal-2016-1-mds-t2\jugadores.xml");
                return true;
            }
            return false;
        }

		public bool EliminarJugador(Jugador jugador) {
			throw new NotImplementedException();
		}

		public bool Desactivar() {
			try {
				XmlDocument xDoc = new XmlDocument();
				xDoc.Load(@"C:\utal-2016-1-mds-t2\cuentas.xml");

				var xCuenta = xDoc.SelectSingleNode("/Cuentas/Cuenta[Email = '" + this.Email + "' and Password = '" + this.Password + "']");
				if (xCuenta == null) {
					return false;
				} else {
					xCuenta.ParentNode.RemoveChild(xCuenta);
					xDoc.Save(@"C:\utal-2016-1-mds-t2\cuentas.xml");
				}
				return true;
			} catch (Exception ex) {
				return false;
			}
		}
	}
}
