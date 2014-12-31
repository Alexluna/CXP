using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IndicadoresV1._001.SDK_Admipaq.Modelo
{
    class Tipos_Datos_CRU
    {
        #region FACTURAS CRU
        public class FacturasCRU
        {
            public string IdDocumento { get; set; }
            public string Fecha { get; set; }
            public string Serie { get; set; }
            public string Folio { get; set; }
            public string NombreAgente { get; set; }
            public string CodigoAgente { get; set; }
            public string IDAgente { get; set; }
            public string RazonSocial { get; set; }
            public string FechaVencimiento { get; set; }
            public string RFC { get; set; }
            public float Subtotal { get; set; }
            public float IVA { get; set; }
            public float Total { get; set; }
            public float Pendiente { get; set; }
            public float TotalUnidades { get; set; }
            public string TextoExtra1 { get; set; }
            public string TextoExtra2 { get; set; }
            public string TextoExtra3 { get; set; }
            public string Afectado { get; set; }
            public string Impreso { get; set; }
            public string Cancelado { get; set; }
            public string Clasificacion1 { get; set; }
            public string Clasificacion2 { get; set; }
            public string Clasificacion3 { get; set; }
            public string NombreConcepto { get; set; }
            public string CodigoCliente { get; set; }
            public string IDCliente { get; set; }
            public string IDNombreConcepto { get; set; }
            public Cliente_Proveedor proveedor { get; set; }
            public List<Movimientos> Listmovimiento { get; set; }

        }
        public class Movimientos
        {
            public string IDProducto { get; set; }
            // public string NombreProducto { get; set; } //ver si es necesario si llegara hacer necesario se tiene que hacer una busqueda de producto y sacar solo el nombre
            public string PrecioProducto { get; set; }
            public string CantidadProducto { get; set; }
            public float Total { get; set; }
            public float Importe { get; set; }
            public float IVA { get; set; }
            public Producto producto { get; set; }
        }

        public class Producto
        {
            public string codigo { get; set; }
            public string Descripcion { get; set; }
            public string Clasifiacion1 { get; set; }
            public string ValorClasificación1 { get; set; }
            public string Clasificacion2 { get; set; }
            public string ValorClasificación2 { get; set; }
            public string Clasificacion3 { get; set; }
            public string ValorClasificación3 { get; set; }
        }
        public class Cliente_Proveedor
        {
            public string CodigoCliente { get; set; }
            public string RazonSocial { get; set; }
            public string Clasificación1 { get; set; }
            public string ValorClasificación1 { get; set; }
            public string Clasificación2 { get; set; }
            public string ValorClasificación2 { get; set; }
            public string Clasificación3 { get; set; }
            public string ValorClasificación3 { get; set; }
        }
        #endregion

        #region FILTRO POR AGENTES
        public class FiltroAgentes
        {
            public string IDAgente { get; set; }
            public string NombreAgente { get; set; }
            public List<Tipos_Datos_CRU.FacturasCRU> Listproductos { get; set; }
        }
        #endregion


        #region CXP

        public class Clasificacion1
        {
            public string id { get; set; }
            public string nombre_clasifiacion1 { get; set; }
        }

        public class Dataclient
        {
            public string id { get; set; }
            public string nombre_cliente { get; set; }
            public string clasificacion_1 { get; set; }
            public string clasificacion_2 { get; set; }
        }


        public class Producto_data
        {
            public string codigo { get; set; }
            public string id { get; set; }
            public string Descripcion { get; set; }
            public string Clasifiacion1 { get; set; }
            public string ValorClasificación1 { get; set; }
            public string Clasificacion2 { get; set; }
            public string ValorClasificación2 { get; set; }
            public string Clasificacion3 { get; set; }
            public string ValorClasificación3 { get; set; }
        }

        public class Movimientos_Cuentas// CUENTAS POR PAGAR 
        {

            public string fecha { get; set; }
            public string folio { get; set; }
            public string Proveedor { get; set; }
            public string Proveedor_codigo { get; set; }
            public string IDProducto { get; set; }
            // public string NombreProducto { get; set; } //ver si es necesario si llegara hacer necesario se tiene que hacer una busqueda de producto y sacar solo el nombre
            public string PrecioProducto { get; set; }//precio unitario
            public string CantidadProducto { get; set; }//cuantos productos
            public float Total { get; set; }
            public float Subtotal { get; set; }
            public float Importe { get; set; }
            public float IVA { get; set; }
            public string ID_doc { get; set; }
            public string producto_codigo { get; set; }
            public string producto_nombre { get; set; }
            public int semana { get; set; }
            public string Clasificacion_1_producto { get; set; }
            public string Valor_Clasificacion_1_producto { get; set; }
            public string Clasificacion_2_producto { get; set; }
            public string Valor_Clasificacion_2_producto { get; set; }
            public string Clasificacion_1_proveedor { get; set; }
            public string Valor_Clasificacion_1_proveedor { get; set; }
            public string Clasificacion_2_proveedor { get; set; }
            public string Valor_Clasificacion_2_proveedor { get; set; }
            public string pendiente { get; set; }
            public string IDCliente { get; set; }
            public Cliente_Proveedor proveedor { get; set; }
            public List<Movimientos1> Listmovimiento { get; set; }


        }


        public class Movimientos1
        {

            public string fecha { get; set; }
            public string IDProducto { get; set; }
            public string NombreProducto { get; set; } //ver si es necesario si llegara hacer necesario se tiene que hacer una busqueda de producto y sacar solo el nombre
            public string PrecioProducto { get; set; }//precio unitario
            public string CantidadProducto { get; set; }//cuantos productos
            public float Total { get; set; }
            public float Subtotal { get; set; }
            public float Importe { get; set; }
            public float IVA { get; set; }
            public string ID_doc { get; set; }


            public Producto1 producto { get; set; }


        }


        public class Producto1
        {
            public string codigo { get; set; }
            public string Descripcion { get; set; }
            public string Clasifiacion1 { get; set; }
            public string ValorClasificación1 { get; set; }
            public string Clasificacion2 { get; set; }
            public string ValorClasificación2 { get; set; }
            public string Clasificacion3 { get; set; }
            public string ValorClasificación3 { get; set; }
        }
        #region PARA CLASIFICACION 1 PROVEEDORES

        public class ComprasMensualesXClasificacionIMagenes
        {
            public string Anio { get; set; }
            public string Clasificacion1 { get; set; }
            public string nombreimagen { get; set; }
            public List<ComprasMensualesXClasificacion> compras { get; set; }
        }

        public class ComprasMensualesXClasificacion
        {
            public string CodigoClasificacion { get; set; }
            public string Clasificacion1 { get; set; }
            public string Mes { get; set; }
            public float total { get; set; }
            public string Dia { get; set; }
            public string Anio { get; set; }

        }
        #endregion

        #region PARA CLASIFICACION 2 PROVEEDORES

        public class ComprasMensualesXClasificacionIMagenes2
        {
            public string Anio { get; set; }
            public string Clasificacion2 { get; set; }
            public string nombreimagen { get; set; }
            public List<ComprasMensualesXClasificacion2> compras { get; set; }
        }

        public class ComprasMensualesXClasificacion2
        {
            public string CodigoClasificacion { get; set; }
            public string Clasificacion2 { get; set; }
            public string Mes { get; set; }
            public float total { get; set; }
            public string Dia { get; set; }
            public string Anio { get; set; }

        }
        #endregion

        #region PARA CLASIFICACION 1 PRODCUTOS
        public class ComprasMensualesXClasificacion1Productos
        {
            public string Anio { get; set; }
            public string Clasificacion1 { get; set; }
            public string nombreimagen { get; set; }
            public List<ComprasMensualesXClasificacion1> compras { get; set; }
        }

        public class ComprasMensualesXClasificacion1
        {
            public string CodigoClasificacion { get; set; }
            public string Clasificacion1 { get; set; }
            public string Mes { get; set; }
            public float total { get; set; }
            public string Dia { get; set; }
            public string Anio { get; set; }

        }
        #endregion


        #region PARA CLASIFICACION 2 PRODUCTOS
        public class ComprasMensualesXClasificacion2Productos
        {
            public string Anio { get; set; }
            public string Clasificacion2 { get; set; }
            public string nombreimagen { get; set; }
            public List<ComprasMensualesXClasificacion2> compras { get; set; }
        }
        #endregion

        #region PARA CLASIFICACION 1 PRODUCTOS POR MES

        public class ComprasMensualesXClasificacion1ProductosMes
        {
            public string Anio { get; set; }
            public string nombreimagen { get; set; }
            public string Mes { get; set; }
            public string Clasificacion1 { get; set; }
            public List<ComprasMensualesXClasificacion1> compras { get; set; }
        }
        #endregion


        #region PARA CLASIFICAION 2 PRODUCTO POR MES
        public class ComprasMensualesXClasificacion2ProductosMes
        {
            public string Anio { get; set; }
            public string nombreimagen { get; set; }
            public string Mes { get; set; }
            public string Clasificacion2 { get; set; }
            public List<ComprasMensualesXClasificacion2> compras { get; set; }
        }
        #endregion
        
        #endregion
    }
}
