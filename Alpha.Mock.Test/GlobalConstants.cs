using System;

namespace Alpha.Mock.Test
{
    public class GlobalConstants
    {

        /// TODO: Constantes de formato de fecha: a futuro, esto deberia estar en la configuración del idioma
        /// <summary>
        /// Formato a utilizar en el sistema para mostrar fechas.
        /// </summary>
        public const string FORMATO_FECHA = "yyyy-MM-dd"; // NOTE: format string is case sensitive
        /// <summary>
        /// Formato a utilizar en el sistema para mostrar fechas con horas.
        /// TODO: a futuro esto deberia estar en la configuración del idioma
        /// </summary>
        public const string FORMATO_FECHA_HORA = "yyyy-MM-dd hh:mm:ss tt"; // NOTE: format string is case sensitive
        /// <summary>
        /// Fecha con hora en formato 24 horas
        /// </summary>
        public const string FORMATO_FECHA_HORA_24 = "yyyy-MM-dd HH:mm:ss";
        /// <summary>
        /// Cadena de formato para utilizar para convertir fechas a texto en paginas ASPX especialmente (Ejemplo: en columnas de Grids)
        /// </summary>
        public const string FORMATO_FECHA_ASPX = "{0:" + FORMATO_FECHA + "}"; // NOTE: format string is case sensitive
        /// <summary>
        /// Cadena de formato para utilizar para convertir fechas a texto en paginas ASPX especialmente (Ejemplo: en columnas de Grids)
        /// </summary>
        public const string FORMATO_FECHA_HORA_ASPX = "{0:" + FORMATO_FECHA_HORA + "}"; // NOTE: format string is case sensitive

        /// <summary>
        /// Mensaje a mostrar en un Grid cuando no se retornaron registros
        /// </summary>
        public const string MESSAGE_NO_ROWS_FOUND = "No se encontraron registros.";

        
        /// <summary>
        /// Delimitador de inicio que se utiliza para Funciones, Expresiones en la Factura Recurrente
        /// </summary>
        public const string DELIMITADOR_INICIO = "[%";

        /// <summary>
        /// Delimitador de Final que se utiliza para Funciones, Expresiones Alpha
        /// </summary>
        public const string DELIMITADOR_FINAL = "%]";

        public const string CODIGO_PAIS_COLOMBIA = "CO";

        #region plantillas de mensaje

        /// <summary>
        /// Nombre del token a reemplazar en las XSL master de plantillas de email
        /// Este token es reemplazado con la XSL de cada plantilla
        /// </summary>
        public const string CONTENT_TEMPLATE = "{CONTENT_TEMPLATE}";

        /// <summary>
        /// url de la pagina de preferencias de usuario
        /// </summary>
        public const string URL_PREFERENCIAS = "/path1/page.aspx";

        /// <summary>
        /// url del visor publico de documento
        /// </summary>
        public const string URL_VISOR_PUBLICO = "/path1/page.aspx";

        #endregion plantillas de mensaje

        #region Amazon Config

        /// <summary>
        /// Nombre del parametro del Bucket usado en amazonS3
        /// </summary>
        public const string AMAZON_BUCKET_NAME = "AMAZON_BUCKET_NAME";
        /// <summary>
        /// Nombre del parametro de la clave de accesso usado en amazonS3
        /// </summary>
        public const string AMAZON_ACCESS_KEY = "AMAZON_ACCESS_KEY";
        /// <summary>
        /// Nombre del parametro de la clave usado en amazonS3
        /// </summary>
        public const string AMAZON_SECRET_KEY = "AMAZON_SECRET_KEY";

        #endregion Amazon Config

        #region Session Config

        public const int MINUTOS_VERIFICAR_SESSION = 5;

        #endregion

        public const string URL_IMAGEN = @"~/Include/Images/img_status_{0}.png";

        // ubicacion de servidor publico
        public const string url_iceware = "172.16.1.21";

        #region CORREOS CONOCIDO EN EL SISTEMA
        public const string email_fakedomain = "carlos.benitez@yopmail.com";
        public const string email_ceiba = "carlos.benitez@ceiba.com.co";

        #endregion

        #region Email Fakedomain
        public const string hostname_fake = "mail.yopmail.com";
        public const string username_fake = "catchall";
        public const string password_fake = "abc123$";
        #endregion


    }
}
