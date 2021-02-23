using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alpha.Mock.Test.Data.Schema.DataGenerator
{
    public class AddressGenerator : Generator
    {
        #region Constructores

        public AddressGenerator() : base() { }

        public AddressGenerator(bool GenerarDatosInmediatamente) : base(GenerarDatosInmediatamente) { }

        #endregion Constructores

        /// <summary>
        /// Obtiene una direccion aleatorio. Ejemplo: "CALLE 125 # 86A	ESTE 43A-01"
        /// </summary>
        protected override void Generate()
        {
            string Calle = TipoDeCalle();
            this.Linea1 = string.Format("{0} {1} {2}-{3}",
                            Calle, NombreCalle(Calle), NumeroCalle(), DistanciaEnMetros());
            this.Linea2 = ObtenerLinea2();
            this.Barrio = Aleatorio(BaseData.Barrios);
            this.Departamento = Aleatorio(BaseData.Departamentos);
            this.Ciudad = Aleatorio(BaseData.Ciudades[this.Departamento]);
            this.Pais = "Colombia";
            this.CodigoPostal = AleatorioDigitos(6);
        }

        public string Linea1 { get; private set; }

        public string Linea2 { get; private set; }

        public string Barrio { get; private set; }

        public string Ciudad { get; private set; }

        public string Departamento { get; private set; }

        public string Pais { get; private set; }

        public string CodigoPostal { get; private set; }

        /// <summary>
        /// Para desplegar en una sola linea todos los datos generados
        /// </summary>
        public override string ToString()
        {
            return string.Format("{0}{1}\n{2}\n{3}, {4} {5}. {6}",
                            this.Linea1,
                            this.Linea2 == "" ? "" : "\n" + this.Linea2,
                            this.Barrio,
                            this.Ciudad, this.Departamento, this.Pais,
                            this.CodigoPostal == "" ? "" : "CP " + this.CodigoPostal
                            ).TrimEnd();
        }

        #region Miembros privados

        public static List<string> PrefijoAvenidas = new List<string> { "Avenida", "Av.", "Avda." };
        private static string[] Orientaciones = new string[] { "ESTE", "OESTE", "NORTE", "SUR" };
        private static string[] Numerales = new string[] { "#", "No.", "n." };

        private const int MAX_NUM_CALLE = 150;
        private const int MAX_DISTANCIA_METROS = 300;
        private const int MIN_SUFIJO_CALLE = 'A';
        private const int MAX_SUFIJO_CALLE = 'H';

        /// <summary>
        /// Obtiene el tipo de calle a crear (calle, carrera, avenida, etc)
        /// </summary>
        private string TipoDeCalle()
        {
            string[] seleccion = null;

            int i = Aleatorio(1, 20);
            switch (i)
            {
                // carrera
                case 1:
                case 2:
                case 3:
                case 4:
                case 5:
                    seleccion = new string[] { "Carrera", "Cra.", "Cr." };
                    break;

                // calle
                case 6:
                case 7:
                case 8:
                case 9:
                case 10:
                    seleccion = new string[] { "Calle", "Cll" }; break;
                case 11: seleccion = new string[] { "Transversal", "Tr.", "TR", "Trans.", "Transv." }; break;
                case 12: seleccion = new string[] { "Diagonal", "DG." }; break;
                case 13: seleccion = new string[] { "Autopista" }; break;
                case 14: seleccion = new string[] { "Circunvalar" }; break;
                case 15: seleccion = new string[] { "Manzana" }; break;
                case 16: seleccion = new string[] { "Circular" }; break;

                // avenida
                default:
                    seleccion = PrefijoAvenidas.ToArray();
                    break;
            }

            return Aleatorio(seleccion);
        }

        /// <summary>
        /// Construye el número o nombre de la calle
        /// </summary>
        private string NombreCalle(string calle)
        {
            // si se escogio una avenida, 50% chance que escoja un nombre en vez de un número
            bool UsarNombreDeAvenidas = (PrefijoAvenidas.Contains(calle)) ? CayoProbabilidad(porcentajeProbabilidad: 50) : false;

            // escoja un nombre o una calle entre 1 y 150
            string nombre;
            if (UsarNombreDeAvenidas)
            {
                nombre = BaseData.Avenidas[Aleatorio(BaseData.Avenidas.Length)];
            }
            else
            {
                nombre = NumeroCalle();

                // 1% de probabilidad para agregar si ESTE, OESTE, etc
                if (CayoProbabilidad(porcentajeProbabilidad: 1))
                {
                    nombre += " " + Orientaciones[Aleatorio(Orientaciones.Length)];
                }
            }

            // 10% que aparezca "#", "No." o similares
            if (CayoProbabilidad(porcentajeProbabilidad: 10))
            {
                nombre += " " + Numerales[Aleatorio(Numerales.Length)];
            }

            return nombre;
        }

        /// <summary>
        /// Genera número de calles válidos
        /// </summary>
        private string NumeroCalle()
        {
            string numeroCalle = (Aleatorio(MAX_NUM_CALLE) + 1).ToString();

            // el 1% de las calles tendrá una letra de sufijo (Ej: la 'D' en Calle 15D)
            if (CayoProbabilidad(porcentajeProbabilidad: 1))
            {
                char sufijo = Convert.ToChar(MIN_SUFIJO_CALLE + Aleatorio(MAX_SUFIJO_CALLE - MIN_SUFIJO_CALLE));
                numeroCalle += sufijo.ToString();
            }

            return numeroCalle;
        }

        /// <summary>
        /// devuelve la distancia de una calle hasta el final de la misma en metros
        /// </summary>
        private string DistanciaEnMetros()
        {
            int distancia = Aleatorio(MAX_DISTANCIA_METROS) + 1;
            return (distancia < 10) ? distancia.ToString().PadLeft(2, '0') : distancia.ToString();
        }

        /// <summary>
        /// Linea 2 en la direcion.
        /// Agregar solo al 50% de las direcciones generadas
        /// </summary>
        private string ObtenerLinea2()
        {
            if (!CayoProbabilidad(porcentajeProbabilidad: 50)) { return string.Empty; }

            string[] seleccion = null;
            int piso = Aleatorio(1, 10); // 10 pisos
            bool agregarSeparador = false; // true: se desea agregar separador entre piso y numero
            int numero = Aleatorio(1, 20); // 20 locales por piso

            int i = Aleatorio(1, 10);
            switch (i)
            {
                case 1: seleccion = new string[] { "Apartamento", "Apto", "Ap.", "AP" }; break;
                case 2: seleccion = new string[] { "Bloque", "BL", "Bl." }; break;
                case 3: seleccion = new string[] { "Casa" }; break;
                case 4: seleccion = new string[] { "Condominio" }; break;
                case 5: seleccion = new string[] { "Etapa" }; break;
                case 6: seleccion = new string[] { "Piso" }; break;
                case 7: seleccion = new string[] { "Local", "LOC", "L." }; break;
                case 8: seleccion = new string[] { "Oficina", "Of.", "OF" }; break;
                case 9:
                    seleccion = new string[] { "Interior", "INT." };
                    agregarSeparador = true;
                    piso = Aleatorio(1, 3); // 3 pisos
                    break;

                // piso
                default:
                    seleccion = new string[] { "Piso", "P" };
                    numero = 0; // sin numero
                    break;
            }

            return string.Format("{0} {1}{2}{3:00}", Aleatorio(seleccion), piso, agregarSeparador ? "-" : "", numero);
        }

        #endregion Miembros privados
    }
}
