using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ajedrez.Consola {
	public class Interfaz {
		public static void Line(bool argIsThick) {
			if (argIsThick) {
				Console.WriteLine("========================================");
			} else {
				Console.WriteLine("----------------------------------------");
			}
		}

		public static void Title(string argCaption, bool argIsThick, bool argIsPartial) {
			Interfaz.Line(argIsThick);
			Console.WriteLine(argCaption);
			if (!argIsPartial) {
				Interfaz.Line(argIsThick);
			}
		}
	}
}
