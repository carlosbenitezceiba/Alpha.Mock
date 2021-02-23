using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Alpha.Mock.Test.Data.Schema.DataGenerator
{
    public class NameGenerator : Generator 
    {
        public enum Genero { Hombre, Mujer }

        #region Constructores
        public NameGenerator() : base() { }
        public NameGenerator(bool GenerarDatosInmediatamente) : base(GenerarDatosInmediatamente) { }
        #endregion

        /// <summary>
        /// Indica el dominio por defecto a usar para la generacion de emails
        /// </summary>
        public const string DOMINIO_EMAIL = "yopmail.com";

        /// <summary>
        /// Obtiene datos aleatorios para personas y empresas
        /// </summary>
        protected override void Generate()
        {
            this.Nombre1 = PersonaObtenerNombre();
            this.Nombre2 = PersonaObtenerNombre2();
            this.Apellido1 = PersonaObtenerApellido();
            this.Apellido2 = PersonaObtenerApellido();
            this.Cedula = PersonaObtenerCedula();
            this.Telefono = PersonaObtenerTelefono();
            this.TelefonoExtension = AleatorioDigitos(1, 5, 1);
            this.Celular = PersonaObtenerCelular();
            this.Password = "abc123$";
            this.Password_Test = "test123$!";

            this.NIT = EmpresaObtenerNIT();
            this.NITDigitoVerificacion = CalcularDigitoVerificacion(this.NIT);
            this.RazonSocial = EmpresaObtenerNombre();
            this.ClaveControlTecnico = AleatorioHexadecimal(30, 40);

            this.CategoriaFiscalIndex = Aleatorio(1, 4);

            this.Email = ObtenerEmail();
            this.WebUrl = ObtenerWebUrl();
            this.ProductoNombre = ProductoObtenerNombre();
        }

        public string Nombre1 { get; private set; }
        public string Nombre2 { get; private set; }
        public string Apellido1 { get; private set; }
        public string Apellido2 { get; private set; }
        public string Cedula { get; private set; }
        public string Telefono { get; private set; }
        public string Celular { get; private set; }
        public string Email { get; private set; }
        public object Password { get; private set; }

        public object Password_Test { get; private set; }

        public object TelefonoExtension { get; private set; }

        public string RazonSocial { get; private set; }
        public string NIT { get; private set; }
        public string NITDigitoVerificacion { get; private set; }
        public string ClaveControlTecnico { get; set; }
        public int CategoriaFiscalIndex { get; set; }
        private string WebDominio { get; set; }   // privado por ahora, dependera que hacemos en Ceiba
        public string WebUrl { get; private set; }

        public string ProductoNombre { get; private set; }



        public override string ToString()
        {
            return string.Format("{0} {1}{2} {3} {4}\nTel: {5}\nCel: {6}\nEmail: {7}",
                        this.Cedula,
                        this.Nombre1,
                        this.Nombre2 == "" ? "" : " " + this.Nombre2,
                        this.Apellido1,
                        this.Apellido2,
                        this.Telefono,
                        this.Celular,
                        this.Email
                    ) +
                    string.Format("\n{0} {1}\n{2}",
                        this.NIT,
                        this.RazonSocial,
                        this.WebUrl
                        );
        }


        public string PersonaObtenerNombre(Genero PersonaGenero)
        {
            return PersonaGenero == Genero.Hombre ? Aleatorio(BaseData.Hombres) : Aleatorio(BaseData.Mujeres);
        }

        private string PersonaObtenerNombre()
        {
            return CayoProbabilidad(50) ? Aleatorio(BaseData.Hombres) : Aleatorio(BaseData.Mujeres);
        }

        private string PersonaObtenerNombre2()
        {
            // 50% probabilidad de tener segundo nombre
            return CayoProbabilidad(50) ? PersonaObtenerNombre() : "";
        }

        private string PersonaObtenerApellido()
        {
            return Aleatorio(BaseData.Apellidos);
        }

        public string PersonaObtenerCedula()
        {
            return AleatorioDigitos(7, 10, 1);
        }

        public string PersonaObtenerTelefono()
        {
            //return string.Format("({0}) {1}-{2}",
            return string.Format("{0}{1}{2}",
                        AleatorioDigitosConPrimerDigito(1, 1),
                        AleatorioDigitosConPrimerDigito(3, 1),
                        AleatorioDigitos(4));
        }

        public string PersonaObtenerCelular()
        {
            //return string.Format("(3{0:00}) {1}-{2}",
            return string.Format("3{0:00}{1}{2}",
                        Aleatorio(0, 20),
                        AleatorioDigitosConPrimerDigito(3, 1),
                        AleatorioDigitos(4));
        }

        private string ObtenerEmail()
        {
            string prefijoEmail = "";

            if (string.IsNullOrEmpty(this.Nombre1)) { this.Nombre1 = PersonaObtenerNombre(); }
            if (string.IsNullOrEmpty(this.Apellido1)) { this.Apellido1 = PersonaObtenerApellido(); }
            //if (string.IsNullOrEmpty(this.WebDominio)) { this.WebDominio = ObtenerDominioWeb(); }
            //Se utilizara para generar un email con un dominio fijo
            if (string.IsNullOrEmpty(this.WebDominio)) { this.WebDominio = ObtenerDominioWeb_Fijo(); }

            string nombre = this.Nombre1;
            string apellido = this.Apellido1;

            int caso = Aleatorio(1, 3);
            switch (caso)
            {
                // nombre.apellido@dominio.com
                case 1:
                    prefijoEmail = string.Format("{0}.{1}", nombre, apellido);
                    break;

                // [inicial-nombre]apellido@dominio.com
                case 2:
                    prefijoEmail = string.Format("{0}.{1}", nombre.Substring(0, 1), apellido);
                    break;

                // [inicial-apellido]nombre@dominio.com
                default:
                    prefijoEmail = string.Format("{0}.{1}", apellido.Substring(0, 1), nombre);
                    break;
            }

            return string.Format("{0}@{1}", prefijoEmail, this.WebDominio);
        }


        private string EmpresaObtenerNombre()
        {
            int palabras = Aleatorio(3, 6);
            string nombre = "";
            for (int i = 0; i < palabras; i++)
            {
                nombre += Aleatorio(BaseData.Empresas) + " ";
            }

            if (CayoProbabilidad(30)) { nombre += Aleatorio(BaseData.EmpresaSufijos); }

            return nombre.TrimEnd();
        }

        private string EmpresaObtenerNIT()
        {
            return AleatorioDigitos(9, 9, 8);
        }

        private string ObtenerDominioWeb()
        {
            return string.Format("{0}.{1}", AleatorioPalabra().ToLower(), Aleatorio(BaseData.ExtensionDominios));
        }

        //Servira para generar un dominio fijo
        private string ObtenerDominioWeb_Fijo()
        {
            return string.Format("{0}", DOMINIO_EMAIL);
        }


        public string ObtenerWebUrl()
        {
            if (string.IsNullOrEmpty(this.WebDominio)) { this.WebDominio = ObtenerDominioWeb(); }
            return string.Format("www.{0}", this.WebDominio);
        }

        #region Nombre_Producto 
        private string ProductoObtenerNombre()
        {
            return Aleatorio(BaseData.nombreProductos);
        }
        #endregion

    }
}
