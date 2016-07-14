using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Ajedrez.Models {
	public class Jugador {
		private static string RutaXMLJugadores = ConfigurationManager.AppSettings["RutaXMLJugadores"];

		public long Id {
			get; set;
		}
		public String Nick {
			get; set;
		}
		public Sexo Sexo {
			get; set;
		}
		public DateTime FechaNacimiento {
			get; set;
		}
		private List<Partida> partidas;

		public Jugador() {
			this.partidas = new List<Partida>();
		}

        public static long CantidadJugadores() {
            XmlDocument xDoc = new XmlDocument();
            xDoc.Load(RutaXMLJugadores);
            long tmp= xDoc.SelectNodes("//jugador").Count;
            return tmp;
        }

		public List<Partida> Partidas() {
			XmlDocument xmlDoc = new XmlDocument();
			if (!System.IO.File.Exists(RutaXMLJugadores))
				return this.partidas;
			else
				xmlDoc.Load(RutaXMLJugadores);

			var xPartidas = xmlDoc.SelectNodes("//Jugadores/Jugador[Id='" + this.Id + "']/Partidas/Partida");

			foreach (XmlNode xPartida in xPartidas) {
				Partida p = new Partida();
				//p.Seleccionar(Int64.Parse(th["Id"].InnerText));
				this.partidas.Add(p);
			}
			return this.partidas;
		}

		public bool Desafiar(Partida partida) {
            this.partidas.Add(partida);
            XmlDocument xmlDoc = new XmlDocument();
            if (!System.IO.File.Exists(RutaXMLJugadores))
                return false;
            else
                xmlDoc.Load(RutaXMLJugadores);


            var xmldf = xmlDoc.CreateDocumentFragment();
            xmldf.InnerXml =
            @"<Partida>
	           <ID>" + partida.Id + @"</ID>
               <Estado>Pendiente</Estado>
              </Partida>";
            xmlDoc.SelectSingleNode("//Jugadores/Jugador[Id='" + this.Id + "']/Partidas").AppendChild(xmldf);
            xmlDoc.Save(RutaXMLJugadores);
            ////Las partidas deberian guardarse de la siguiente manera en el documento XML que guarda los jugadores.
            /*
            
            <Jugador>
		        <Email>abc</Email>
		        <Id>2</Id>
		        <Nick>Castor</Nick>
		        <Sexo>0</Sexo>
		        <FechaNacimiento>628108128000000000</FechaNacimiento>
		        <Partidas>
			        <Partida>
				        <ID>2134</ID>
				        <Estado>Pendiente</Estado>
			        </Partida>
			        <Partida>
				        <ID>2135</ID>
				        <Estado>Pendiente</Estado>
			        </Partida>
			        <Partida>
				        <ID>1</ID>
				        <Estado>Finalizado</Estado>
			        </Partida>
			        <Partida>
				        <ID>2</ID>
				        <Estado>Finalizado</Estado>
			        </Partida>
			        <Partida>
				        <ID>3</ID>
				        <Estado>Finalizado</Estado>
			        </Partida>
			        <Partida>
				        <ID>4</ID>
				        <Estado>Finalizado</Estado>
			        </Partida>
		        </Partidas>
	        </Jugador>
            
            */
            return true;
		}

        public bool Eliminar(Partida partida)
        {
            XmlDocument xmlDoc = new XmlDocument();
            if (!System.IO.File.Exists(RutaXMLJugadores))
                return false;
            else
                xmlDoc.Load(RutaXMLJugadores);

            var elemPartida = xmlDoc.SelectSingleNode("//Jugadores/Jugador[Id='" + this.Id + "']/Partidas/Partida['ID=" + partida.Id + "']");
            if (elemPartida != null)
            {
                elemPartida.ParentNode.RemoveChild(elemPartida);
                xmlDoc.Save(RutaXMLJugadores);
                return true;
            }
            return false;
        }
    }
}
