using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Xml;
using System.Xml.Linq;

namespace Ajedrez.Models
{
    public class Cuenta
    {

        private const string RutaXMLCuentas = @"C:\Users\polux\Desktop\Cuentas.xml";
        private const string RutaXMLJugadores = @"C:\Users\polux\Desktop\Jugadores.xml";


        public string Email
        {
            get; set;
        }
        public string Password
        {
            get; set;
        }
        public DateTime UltimoAcceso
        {
            get; set;
        }
        public Jugador JugadorActual
        {
            get; set;
        }

        public Cuenta()
        {
        }

        public Cuenta(string email, string password, DateTime ultimoAcceso, Jugador jugadorActual)
        {
            this.Email = email;
            this.Password = password;
            this.UltimoAcceso = ultimoAcceso;
            this.JugadorActual = jugadorActual;
        }

        public bool Registrar(string email, string password)
        {
            System.IO.Directory.CreateDirectory(@"C:\Users\polux\Desktop");
            XmlDocument xDoc = new XmlDocument();
            if (!System.IO.File.Exists(RutaXMLCuentas))
            {
                xDoc.LoadXml("<Cuentas></Cuentas>");
            }
            else {
                xDoc.Load(RutaXMLCuentas);
            }
            if (!this.Seleccionar(email))
            {
                var xmldf = xDoc.CreateDocumentFragment();
                xmldf.InnerXml = @"<Cuenta>
                        <Email>" + email + @"</Email>
                        <Password>" + password + @"</Password>
                        <UltimoAcceso>" + DateTime.Now.Ticks + @"</UltimoAcceso>
                        <JugadorActual></JugadorActual>
                    </Cuenta>";
                xDoc.FirstChild.AppendChild(xmldf);
                xDoc.Save(RutaXMLCuentas);
                return true;
            }
            return false;
        }

        public bool Seleccionar(string email)
        {
            try
            {
                XmlDocument xDoc = new XmlDocument();
                xDoc.Load(RutaXMLCuentas);
                var xCuenta = xDoc.SelectSingleNode("/Cuentas/Cuenta[Email = '" + email + "']");
                if (xCuenta == null)
                {
                    return false;
                }
                else {
                    return true;
                }
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public bool IniciarSesion()
        {
            XmlDocument xDoc = new XmlDocument();
            xDoc.Load(RutaXMLCuentas);
            var xCuenta = xDoc.SelectSingleNode("/Cuentas/Cuenta[Email = '" + this.Email + "' and Password = '" + this.Password + "']");
            if (xCuenta == null)
            {
                return false;
            }
            else {
                this.UltimoAcceso = DateTime.Now;
                xDoc.Save(RutaXMLCuentas);
                return true;
            }
        }

        public void CerrarSesion()
        {
            throw new NotImplementedException();
        }

        public List<Jugador> Jugadores()
        {
            XmlDocument xDoc = new XmlDocument();
            List<Jugador> acum = new List<Jugador>();
            if (!System.IO.File.Exists(RutaXMLJugadores)) return acum;
            else
                xDoc.Load(RutaXMLJugadores);

            var entrada = xDoc.SelectNodes("/Jugadores/Jugador[Email='" + Email + "']");

            foreach (XmlNode th in entrada)
            {
                acum.Add(new Jugador()
                {
                    Id = Int64.Parse(th["Id"].InnerText),
                    Nick = th["Nick"].InnerText,
                    Sexo = (Sexo)Convert.ToInt32(th["Sexo"].InnerText),
                    FechaNacimiento = new DateTime(Int64.Parse(th["FechaNacimiento"].InnerText)),
                });
            }
            return acum;
        }

        public void CambiarJugadorActivo(Jugador jugador)
        {
            XmlDocument xDoc = new XmlDocument();
            if (!System.IO.File.Exists(RutaXMLJugadores)) return;
            else xDoc.Load(RutaXMLJugadores);

            if (xDoc.SelectSingleNode("/Jugadores/Jugador['Id=" + jugador.Id + " Email=" + this.Email + "]") != null)
            {
                xDoc.Load(RutaXMLCuentas);
                var cuenta = xDoc.SelectSingleNode("//Cuenta[Email='" + this.Email + "']");
                if(cuenta != null)
                {
                    cuenta["JugadorActual"].InnerText = jugador.Id.ToString();
                    this.JugadorActual = jugador;
                    xDoc.Save(RutaXMLCuentas);
                }
            }
        }

        public bool CrearJugador(Jugador jugador)
        {
            XmlDocument xDoc = new XmlDocument();
            if (!System.IO.File.Exists(RutaXMLJugadores))
            {
                xDoc.LoadXml("<Jugadores></Jugadores>");
            }
            else
                xDoc.Load(RutaXMLJugadores);

            var xJugador = xDoc.SelectSingleNode("//Jugadores/Jugador[Email='" + this.Email + "']");
            if (xJugador == null)
            {
                var xmldf = xDoc.CreateDocumentFragment();
                xmldf.InnerXml = @"<Jugador>
                    <Email>" + this.Email + @"</Email>
                    <Id>" + jugador.Id + @"</Id>
                    <Nick>" + jugador.Nick + @"</Nick>
                    <Sexo>" + Convert.ToInt32(jugador.Sexo) + @"</Sexo>
                    <FechaNacimiento>" + jugador.FechaNacimiento.Ticks + @"</FechaNacimiento>
                </Jugador>";
                xDoc.SelectSingleNode("/Jugadores").AppendChild(xmldf);
                
                xDoc.Save(RutaXMLJugadores);
                return true;
            }
            return false;
        }

        public bool EliminarJugador(Jugador jugador)
        {
            if (!System.IO.File.Exists(RutaXMLJugadores) || jugador == null) return false;

            XmlDocument xDoc = new XmlDocument();
            xDoc.Load(RutaXMLJugadores);
            var elemjugador = xDoc.SelectSingleNode("/Jugadores/Jugador['Id=" + jugador.Id + " Id-Cuenta=" + this.Email + "]");
            if (elemjugador != null)
            {
                elemjugador.ParentNode.RemoveChild(elemjugador);
                xDoc.Save(RutaXMLJugadores);
                return true;
            }
            return false;
        }

        public bool Desactivar()
        {
            try
            {
                XmlDocument xDoc = new XmlDocument();
                xDoc.Load(RutaXMLCuentas);

                var xCuenta = xDoc.SelectSingleNode("/Cuentas/Cuenta[Email = '" + this.Email + "' and Password = '" + this.Password + "']");
                if (xCuenta == null)
                {
                    return false;
                }
                else {
                    xCuenta.ParentNode.RemoveChild(xCuenta);
                    xDoc.Save(RutaXMLCuentas);
                }
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
