using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using IndicadoresV1._001.SDK_Admipaq.Modelo;
using System.Windows;

namespace IndicadoresV1._001.SDK_Admipaq.Controlador
{
    class Controlador_Impresion
    {
        Modelo_Impresion modeloimpresion;//objeto para comunicarse con el modelo de impresion
        /// <summary>
        /// constructor para controlador de impresion
        /// </summary>
        public Controlador_Impresion()
        {
            modeloimpresion = new Modelo_Impresion();
        }

        /// <summary>
        /// Impresion para facturas
        /// </summary>
        /// <param name="ListFactrurasCRU"></param>
        /// <param name="fechas"></param>
        /// <param name="path"></param>
        /// <param name="ListFactrurasCRUFiltroRFCPublico"></param>
        /// <param name="ListFactrurasCRUFiltroRFCOL"></param>
        public void ImpresionCRUFacturas(List<Tipos_Datos_CRU.FacturasCRU> ListFactrurasCRU, string fechas, string path, List<Tipos_Datos_CRU.FacturasCRU> ListFactrurasCRUFiltroRFCPublico, List<Tipos_Datos_CRU.FacturasCRU> ListFactrurasCRUFiltroRFCOL)
        {
            modeloimpresion.ImpresionCRUFacturas(ListFactrurasCRU, fechas, path, ListFactrurasCRUFiltroRFCPublico, ListFactrurasCRUFiltroRFCOL);
                
        }

        /// <summary>
        /// impresio para abonos
        /// </summary>
        /// <param name="ListFactrurasCRU"></param>
        /// <param name="fechas"></param>
        /// <param name="path"></param>
        /// <param name="ListFactrurasCRUFiltroRFCPublico"></param>
        /// <param name="ListFactrurasCRUFiltroRFCOL"></param>
        public void ImpresionCRUAbonos(List<Tipos_Datos_CRU.FacturasCRU> ListFactrurasCRU, string fechas, string path, List<Tipos_Datos_CRU.FacturasCRU> ListFactrurasCRUFiltroRFCPublico, List<Tipos_Datos_CRU.FacturasCRU> ListFactrurasCRUFiltroRFCOL)
        {
            modeloimpresion.ImpresionCRUAbonos(ListFactrurasCRU, fechas, path, ListFactrurasCRUFiltroRFCPublico, ListFactrurasCRUFiltroRFCOL);
        }


        /// <summary>
        /// impresion de compras
        /// </summary>
        /// <param name="ListFactrurasCRU"></param>
        /// <param name="fechas"></param>
        /// <param name="path"></param>
        /// <param name="ListFactrurasCRUFiltroRFCPublico"></param>
        public void ImpresionCRUCompras(List<Tipos_Datos_CRU.FacturasCRU> ListFactrurasCRU, string fechas, string path, List<Tipos_Datos_CRU.FacturasCRU> ListFactrurasCRUFiltroRFCPublico)
        {
            modeloimpresion.ImpresionCRUCompras(ListFactrurasCRU, fechas, path, ListFactrurasCRUFiltroRFCPublico);
        }

        /// <summary>
        /// impresion de Pagos del proveedor
        /// </summary>
        /// <param name="ListFactrurasCRU"></param>
        /// <param name="fechas"></param>
        /// <param name="path"></param>
        /// <param name="ListFactrurasCRUFiltroRFCPublico"></param>
        public void ImpresionCRUPagosProveedor(List<Tipos_Datos_CRU.FacturasCRU> ListFactrurasCRU, string fechas, string path, List<Tipos_Datos_CRU.FacturasCRU> ListFactrurasCRUFiltroRFCPublico)
        {
            modeloimpresion.ImpresionCRUPAgosPRoveedor(ListFactrurasCRU, fechas, path, ListFactrurasCRUFiltroRFCPublico);
        }


        /// <summary>
        /// controla la impreadion de los moviemitos k tiene el prodcto de forma grafica
        /// </summary>
        /// <param name="lista"></param>
        /// <param name="fechas"></param>
        /// <param name="fecha_titulo"></param>
        /// <param name="path"></param>
        public void impresion_movimientos_productos(List<Tipos_Datos_CRU.Movimientos_Cuentas> lista, string fechas, string fecha_titulo, string path)
        {
            modeloimpresion.Reporte_Compras(lista, fechas, fecha_titulo, path);
        }
    }
}
