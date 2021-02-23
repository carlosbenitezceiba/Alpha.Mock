using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alpha.Mock.Test.Data.Schema.DataGenerator
{
    public class Generator
    {

        private Random rnd;
        public Generator()
        {
            int Semilla = (int)DateTime.Now.Ticks;
            this.rnd = new Random(Semilla);
        }

        /// <summary>
        ///  Constructor que permite decidir si generar conjunto de datos inmediatamente o esperar hasta que se llame al metodo Next()
        /// </summary>
        /// <param name="GenerarDatosInmediatamente"></param>
        public Generator(bool GenerarDatosInmediatamente)
            : this()
        {
            // al ser creado, generador un conjunto de datos aleatorios inmediatamente
            if (GenerarDatosInmediatamente) { this.Generate(); }
        }


        /// <summary>
        /// Metodo que debe ser sobreescrito en clase hija para generar los valores aleatorios deseados
        /// </summary>
        protected virtual void Generate() { }


        /// <summary>
        /// Llamar para genarar otro conjunto de datos aleatorios
        /// </summary>
        public void Next() { this.Generate(); }


        /// <summary>
        /// Genera un número aleatorio entre cero y MaximoValor.
        /// </summary>
        /// <param name="MaximoValor"></param>
        /// <returns></returns>
        public int Aleatorio(int MaximoValor)
        {
            return rnd.Next(0, MaximoValor);
        }

        /// <summary>
        /// Genera un número aleatorio entre <paramref name="MiniValor"/> y <paramref name="MaximoValor"/>
        /// </summary>
        /// <param name="MaximoValor"></param>
        /// <returns></returns>
        public int Aleatorio(int MinimoValor, int MaximoValor)
        {
            return rnd.Next(MinimoValor, MaximoValor);
        }

        /// <summary>
        /// Genera el NUmero de Verificación para un nit recibido
        /// </summary>
        /// <param name="unNit"></param>
        /// <returns>Digito de verificacion</returns>

        public string CalcularDigitoVerificacion(string unNit)
        {

            string miTemp;
            int miContador;
            int miResiduo;
            int miChequeo;
            int[] miArregloPA = new int[15] { 3, 7, 13, 17, 19, 23, 29, 37, 41, 43, 47, 53, 59, 67, 71 };

            miChequeo = 0;
            miResiduo = 0;
            for (miContador = 0; miContador < unNit.Length; miContador++)
            {
                miTemp = unNit[(unNit.Length - 1) - miContador].ToString();
                miChequeo = miChequeo + (Convert.ToInt32(miTemp) * miArregloPA[miContador]);
            }
            miResiduo = miChequeo % 11;
            if (miResiduo > 1)
                return Convert.ToString(11 - miResiduo);
            return miResiduo.ToString();

        }

        /// <summary>
        /// Obtiene un item aleatorio dentro de la lista de elementos
        /// </summary>
        /// <param name="MaximoValor"></param>
        /// <returns></returns>
        public string Aleatorio(string[] Conjunto)
        {
            return Conjunto[Aleatorio(Conjunto.Length)];
        }


        /// <summary>
        ///  Genera un codigo aleatorio de solo dígitos (0-9) con la longitud especificada
        /// </summary>
        /// <param name="p"></param>
        /// <returns></returns>
        public string AleatorioDigitos(int Longitud)
        {
            return AleatorioDigitosConPrimerDigito(Longitud, 0);
        }


        /// <summary>
        ///  Genera un codigo aleatorio de solo dígitos (0-9) con la longitud especificada.
        ///  Para el primer digito, permite especificar en <paramref name="MinValorPrimerDigito"/> el mínimo valor generado
        ///  lo cual es util para generar número de teléfonos, celulares, cédulas, NITs donde se requiere que el primer
        ///  dígito no sea cero o sea mayor a cierto valor
        /// </summary>
        public string AleatorioDigitosConPrimerDigito(int Longitud, int MinValorPrimerDigito)
        {
            string valor = "";
            for (int i = 0; i < Longitud; i++)
            {
                if (i == 0) { valor += Aleatorio(MinValorPrimerDigito, 9).ToString(); }
                else { valor += Aleatorio(0, 9).ToString(); }
            }

            return valor;
        }


        /// <summary>
        ///  Genera un codigo aleatorio de solo dígitos (0-9) con la longitud especificada
        /// </summary>
        /// <param name="p"></param>
        /// <returns></returns>
        public string AleatorioDigitos(int MinLongitud, int MaxLongitud)
        {
            return AleatorioDigitos(MinLongitud, MaxLongitud, 0);
        }


        /// <summary>
        ///  Genera un codigo aleatorio de solo dígitos (0-9) con la longitud especificada
        /// </summary>
        /// <param name="p"></param>
        /// <returns></returns>
        public string AleatorioDigitos(int MinLongitud, int MaxLongitud, int MinValorPrimerDigito)
        {
            int Longitud = Aleatorio(MinLongitud, MaxLongitud);
            return AleatorioDigitosConPrimerDigito(Longitud, MinValorPrimerDigito);
        }


        /// <summary>
        /// de cada 100 veces, devuelve 'true', el número de veces indicado
        /// </summary>
        /// <returns></returns>
        public bool CayoProbabilidad(int porcentajeProbabilidad)
        {
            return Aleatorio(1, 100) <= porcentajeProbabilidad;
        }


        /// <summary>
        /// Construye una palabra aleatoria para armar otros términos.
        /// Ejemplo:
        /// * Nombres de dominios para armar URLs o cuentas de email
        /// </summary>
        public string AleatorioPalabra()
        {
            string palabra = "";
            int seleccion = Aleatorio(BaseData.SilabasGrupos.Count);

            // arma una palabra aleatoria con los grupos predefinidos arriba
            foreach (string item in BaseData.SilabasGrupos.ElementAt(seleccion))
            { palabra += Aleatorio(BaseData.Silabas[item]); }

            return palabra;
        }


        /// <summary>
        ///  Genera un un codigo alfabetico de letras aleatorias
        /// </summary>
        /// <param name="p"></param>
        /// <returns></returns>
        public string AleatorioLetras(int Longitud)
        {
            string valor = "";
            for (int i = 0; i < Longitud; i++)
            { valor += Aleatorio(BaseData.Letras); }
            return valor;
        }


        /// <summary>
        ///  Genera un un codigo alfanumérico con valores aleatorias de la longitud especificada
        /// </summary>
        public string AleatorioAlfanumerico(int Longitud)
        {
            string valor = "";
            for (int i = 0; i < Longitud; i++)
            { valor += Aleatorio(BaseData.DigitosMasLetras); }
            return valor;
        }

        /// <summary>
        ///  Genera un un codigo alfanumérico con valores aleatorias bajo las restricciones de mínimo y máximo de longitud especificadas
        /// </summary>
        public string AleatorioAlfanumerico(int MinLongitud, int MaxLongitud)
        {
            int Longitud = Aleatorio(MinLongitud, MaxLongitud);
            return AleatorioAlfanumerico(Longitud);
        }

        /// <summary>
        ///  Genera un un codigo Hexadecimal con valores aleatorias de la longitud especificada
        /// </summary>
        public string AleatorioHexadecimal(int Longitud)
        {
            string valor = "";
            for (int i = 0; i < Longitud; i++)
            { valor += Aleatorio(BaseData.DigitosHexadecimales); }
            return valor;
        }

        /// <summary>
        ///  Genera un un codigo Hexadecimal con valores aleatorias bajo las restricciones de mínimo y máximo de longitud especificadas
        /// </summary>
        public string AleatorioHexadecimal(int MinLongitud, int MaxLongitud)
        {
            int Longitud = Aleatorio(MinLongitud, MaxLongitud);
            return AleatorioHexadecimal(Longitud);
        }

        /// <summary>
        /// Generador de comentarios siguiendo el estandar de caligrafia "lorem ipsum dolor sit amet"
        /// </summary>
        public string ObtenerComentario(int TotalPalabras)
        {
            StringBuilder sb = new StringBuilder();

            for (int i = 0; i <= TotalPalabras; i++)
            {
                sb.Append(" ");
                // normalmente siempre se empieza con estas palabras como prefijo al comentario
                if (i < BaseData.LatinPrefijos.Length) { sb.Append(BaseData.LatinPrefijos[i]); }
                else { sb.Append(Aleatorio(BaseData.LatinPalabras)); }
            }
            sb.Append(".");
            return sb.ToString();
        }

        /// <summary>
        /// Generador de comentarios siguiendo el estandar de caligrafia "lorem ipsum dolor sit amet"
        /// </summary>
        public string ObtenerComentario(int MinPalabras, int MaxPalabras)
        {
            int TotalPalabras = Aleatorio(MinPalabras, MinPalabras);
            return ObtenerComentario(TotalPalabras);
        }


        /// <summary>
        /// Generador de unidades de medida para usar en comprobantes
        /// </summary>
        public string ObtenerUnidadMedida() { return Aleatorio(BaseData.UnidadesMedida); }
    }
}
