using Microsoft.VisualStudio.TestTools.UnitTesting;
using Factura;
using System;
using Newtonsoft.Json;
using System.Text.RegularExpressions;

namespace FacturaTestProject
{
    [TestClass]
    public class FacturaTest
    {
        [TestMethod]
        public void TestMethod_GetNameUsuario()
        {
            string nombre = EmitirFactura.getNameUsuario();
            // Verifica que la variable nombre no sea null
            Assert.IsNotNull(nombre);
            // Verifica que la variable sea un string. recuerde quie "1" es un string
            Assert.IsInstanceOfType(nombre, typeof(string));
            Assert.IsTrue(esNombre(nombre));
            Assert.IsFalse(esNumerico(nombre));
        }
        /// <summary>
        /// MEtodo que recibe una variable de nombre y verifica que este no contenga
        /// numeros o caracteres especiales.
        /// </summary>
        /// <param name="nombre"></param>
        /// <returns></returns>
        public bool esNombre(string nombre) {
            bool coincide = Regex.IsMatch(nombre, @"^[\p{L} ]+$");
            return coincide;
        }

        public bool esNumerico(string nombre) { 
            bool coincide = Regex.IsMatch(nombre, @"^[0-9]+$");
            return coincide;
        }

        
    }
}
