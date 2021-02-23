using Alpha.Mock.Test.Data.Schema.DataGenerator;
using System;

namespace Factura
{
    public class EmitirFactura

    {
        public EmitirFactura()
        {
            string usuario = getNameUsuario();
        }

        public static void Main(string[] args)
        {
            ImprimirCabecera();
            getProductos();
        }

        private static void ImprimirCabecera()
        {
            Console.WriteLine("        .: FACTURA VARIEDADES 15 LETRAS :. \n=====================================================");
            Console.WriteLine("Comprador: " + getNameUsuario());
            Console.WriteLine("correo: " + getCorreo());
            Console.WriteLine("Nit: " + getNit());
            Console.WriteLine("=====================================================");
            Console.WriteLine("================ P R O D U C T O S ==================");
            Console.WriteLine("=====================================================");
            Console.WriteLine("Item \t" + "Productos\t" + "Und.\t" + "Cant\t" + "Precio\t" + "Total");
        }

        private static NameGenerator mock = new NameGenerator(true);


        public static string getNameUsuario()
        {
            String nombre = mock.Nombre1 + " " + mock.Apellido1 + " " + mock.Apellido2;
            return nombre;
        }

        public static string getNit()
        {
            string nit = mock.NIT + "-" + mock.NITDigitoVerificacion;
            return nit;
        }
        public static string getCorreo()
        {
            string correo = mock.Email.ToLower();
            return correo;
        }


        public static void getProductos()
        {
            int cantidadProductos = mock.Aleatorio(1, 4);
            string[,] productos = new string[cantidadProductos, 5];
            int subtotal = 0;
            for (int i = 0; i < cantidadProductos; i++)
            {
                Producto p = new Producto(true);
                productos[i, 0] = p.ProductoNombre;
                productos[i, 1] = p.UnidadMedida;
                productos[i, 2] = p.Cantidad.ToString();
                productos[i, 3] = p.Valor.ToString();
                productos[i, 4] = (p.Cantidad * p.Valor).ToString();
                subtotal += (p.Cantidad * p.Valor);
                p = null;
            }
            imprimirProductos(productos);
            CalculoFinal(subtotal);

        }

        public static void imprimirProductos(string[,] productos)
        {
            for (int i = 0; i < productos.GetLongLength(0); i++)
            {
                Console.WriteLine(i+1 + " - " + " \t" + productos[i, 0] + "\t" + productos[i, 1] + "\t" + productos[i, 2] + "\t$" + productos[i, 3] + "\t$" + productos[i, 4]);
            }
        }

        public static void CalculoFinal(double subtotal)
        {
            double iva = mock.Aleatorio(10, 20);
            Console.WriteLine("Tipo de IVA: " + iva + "%");
            Console.WriteLine("=====================================================");
            double descuento = 0.0;
            
            if (subtotal<50)
            {
                descuento = Math.Round(.1 * subtotal,2);
                Console.WriteLine("Por su compra de: $" + subtotal  + ", descuento del 10%: $" + descuento);
            }else
            {
                descuento = Math.Round(.2 * subtotal, 2);
                Console.WriteLine("Por su compra de: $" + subtotal + ", descuento del 20%: $" + descuento);
            }

            iva = Math.Round((subtotal * iva/100), 2);
            double Ttotal = Math.Round(subtotal - descuento + iva,2);
            Console.WriteLine("Total:      $" + subtotal);
            Console.WriteLine("Descuento:  $" + descuento);
            Console.WriteLine("IVA:        $" + iva);
            Console.WriteLine("Total pagar $" + Ttotal + " Pesos Colombianos");
            Console.WriteLine("=====================================================");
            Console.WriteLine("============== GRACIAS POR SU COMPRA ================");

        }



    }
}



