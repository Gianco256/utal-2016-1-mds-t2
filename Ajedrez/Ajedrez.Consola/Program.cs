﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Ajedrez.Models;

namespace Ajedrez.Consola {
	class Program {
		static void Main(string[] args) {
			Cuenta cuenta = new Cuenta() { Email = "abc", Password = "123" };
			cuenta.Registrar("abc", "123");
			Console.ReadLine();
		}
	}
}
