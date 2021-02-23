using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Alpha.Mock.Test.Data.Schema.DataGenerator
{
    public class Producto : Generator 
    {

        #region Constructores
        public Producto() : base() { }
        public Producto(bool GenerarDatosInmediatamente) : base(GenerarDatosInmediatamente) { }
        #endregion

        
        protected override void Generate()
        {
            
            this.ProductoNombre = ProductoObtenerNombre();
            this.UnidadMedida = ProductoObtenerUnidad();
            this.Cantidad = Aleatorio(1, 10);
            this.Valor = Aleatorio(1, 10);
            this.TipoIva = ProductosIVA();
        }

        public int Cantidad { get; private set; }
        public int Valor { get; private set; }
        public string ProductoNombre { get; private set; }
        public string UnidadMedida { get; private set; }
        public string TipoIva { get; private set; }


        public override string ToString()
        {
            return string.Format("{0} {1} {2} {3}",
                        this.ProductoNombre,
                        this.UnidadMedida,
                        this.Cantidad,
                        this.Valor
                        
                    );
        }


        #region Nombre_Producto 
        private string ProductoObtenerNombre()
        {
            return Aleatorio(BaseData.nombreProductos);
        }

        private string ProductoObtenerUnidad()
        {
            return Aleatorio(BaseData.UnidadesMedida);
        }
        #endregion

        #region IVA
        private string ProductosIVA()
        {
            return Aleatorio(BaseData.IVA);
        }
        #endregion



    }
}
