using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IndicadoresV1._001.SDK_Admipaq.Modelo
{
    class Nombre_Archivos
    {
        List<string> ListaArchivos;
        /// <summary>
        /// Meotod para regresar la lista con los archivos necesarios para la empresa
        /// </summary>
        /// <returns>regresa la lista con los archivos necesarios para la empresa</returns>
        public List<string> Getlista()
        {
            ListaArchivos = new List<string>();
            //1
            ListaArchivos.Add(Clasificaciones);
            ListaArchivos.Add(ValoresClasificacion);
            ListaArchivos.Add(Productos_Servicios);
            ListaArchivos.Add(Almacenes);
            ListaArchivos.Add(Clientes_Proveedores);
            ListaArchivos.Add(Agentes);
            ListaArchivos.Add(Promociones);
            ListaArchivos.Add(Direcciones);
            ListaArchivos.Add(Monedas);
            ListaArchivos.Add(Parametros);
            //2
            ListaArchivos.Add(Valores_Caracteristicas);
            ListaArchivos.Add(Componentes_Paquetes);
            ListaArchivos.Add(Caracteristicas_Padre);
            ListaArchivos.Add(Conversion_Unidades);
            ListaArchivos.Add(Identificado_Productos_Detalles);
            ListaArchivos.Add(Servicios_Productos);
            ListaArchivos.Add(Unidades_Medida_Peso);
            ListaArchivos.Add(Ejercicios_Periodos);
            ListaArchivos.Add(Existencias_Maximas_Minimas);
            ListaArchivos.Add(Costo_Historicos);
            ListaArchivos.Add(Lista_Precios_Compras);
            ListaArchivos.Add(Existencias);
            //3
            ListaArchivos.Add(Movimiento_Prepoliza);
            ListaArchivos.Add(Prepolizas);
            ListaArchivos.Add(Documentos_Soportados);
            ListaArchivos.Add(Asientos_Contables);
            ListaArchivos.Add(Movimientos_Contables);
            ListaArchivos.Add(Movimientos);
            ListaArchivos.Add(Documentos);
            ListaArchivos.Add(Conceptos);
            ListaArchivos.Add(Abonos_cargos);
            ListaArchivos.Add(Tipos_Cambio);
            //4
            ListaArchivos.Add(Documentos_Capas);
            ListaArchivos.Add(Capas_Productos);
            ListaArchivos.Add(Apartados);
            ListaArchivos.Add(Costos_Historicos);
            ListaArchivos.Add(Identificador_Productos_Detalles);
            ListaArchivos.Add(Movimientos_Serie);
            ListaArchivos.Add(Numeros_Serie);
            //5
            ListaArchivos.Add(Tipos_Cambios);
            ListaArchivos.Add(Periodos_Ejercicios);
            ListaArchivos.Add(Acumulados);
            ListaArchivos.Add(Tipos_Acumulados);
            ListaArchivos.Add(Conceptos_Tipos_Acumulados);
            //6
            ListaArchivos.Add(Productos);
            ListaArchivos.Add(Movimientos_Inventarios_Fisicos);
            ListaArchivos.Add(Movimientos_Series_Capas_Inventario_Fisico);
            //7
            //ListaArchivos.Add(Apertura_Corte);
            //ListaArchivos.Add(Cajas);
            //ListaArchivos.Add(Perifericos);
            //ListaArchivos.Add(Sucursales);
            //8
            ListaArchivos.Add(Documentos_Pos);
            ListaArchivos.Add(Cargos_Abonos);
            //ListaArchivos.Add(Forma_Pago);
            ListaArchivos.Add(Cobro_Notas);
            ListaArchivos.Add(Folios_Digitales);
            ListaArchivos.Add(Datos_Adicionales_Addendas);
            return ListaArchivos;
        }
        //a cada variable se le asignara un archivo el cual debe de ser utilizado
        //DIAGRAMA DE RELACIÓN CATÁLOGOS    1
        public string Clasificaciones = "MGW10019";
        public string ValoresClasificacion = "MGW10020";
        public string Productos_Servicios = "MGW10005";
        public string Almacenes = "MGW10003";
        public string Clientes_Proveedores = "MGW10002";
        public string Agentes = "MGW10001";
        public string Promociones = "MGW10029";
        public string Direcciones = "MGW10011";
        public string Monedas = "MGW10034";
        public string Parametros = "MGW10000";
        //DIAGRAMA DE RELACIÓN SERVICIOS/PRODUCTOS      2
        public string Valores_Caracteristicas = "MGW10022";
        public string Componentes_Paquetes = "MGW10015";
        public string Caracteristicas_Padre = "MGW10021";
        public string Conversion_Unidades = "MGW10027";
        public string Identificado_Productos_Detalles = "MGW10004";
        public string Servicios_Productos = "MGW10005";
        public string Unidades_Medida_Peso = "MGW10026";
        public string Ejercicios_Periodos = "MGW10031";
        public string Existencias = "MGW10030";
        public string Existencias_Maximas_Minimas = "MGW10016";
        public string Costo_Historicos = "MGW10017";
        public string Lista_Precios_Compras = "MGW10014";
        //DIAGRAMA DE RELACIÓN DOCUMENTOS   3
        public string Movimiento_Prepoliza = "MGW10039";
        public string Prepolizas = "MGW10038";
        public string Documentos_Soportados = "MGW10007";
        public string Asientos_Contables = "MGW10023";
        public string Movimientos_Contables = "MGW10024";
        public string Movimientos = "MGW10010";
        public string Documentos = "MGW10008";
        public string Conceptos = "MGW10006";
        public string Abonos_cargos = "MGW10009";
        public string Tipos_Cambio = "MGW10035";
        //DIAGRAMA DE RELACIÓN MOVIMIENTOS  4
        public string Documentos_Capas = "MGW10028";
        public string Capas_Productos = "MGW10025";
        public string Apartados = "MGW10037";
        public string Costos_Historicos = "MGW10017";
        public string Identificador_Productos_Detalles = "MGW10004";
        public string Movimientos_Serie = "MGW10036";
        public string Numeros_Serie = "MGW10032";
        //DIAGRAMA DE RELACIÓN AFECTACIÓN DE DOCUMENTOS 5
        public string Tipos_Cambios = "MGW10035";
        public string Periodos_Ejercicios = "MGW10035";
        public string Acumulados = "MGW10018";
        public string Tipos_Acumulados = "MGW10012";
        public string Conceptos_Tipos_Acumulados = "MGW10013";
        //DIAGRAMA DE REALACIÓN INVENTARIO FÍSICO   6
        public string Productos = "MGW10005";
        public string Movimientos_Inventarios_Fisicos = "MGW10033";
        public string Movimientos_Series_Capas_Inventario_Fisico = "MGW10040";
        //DIAGRAMA DE RELACION GENERAL PUNTO DE VENTA   7
        //string Apertura_Corte = "MGW10042";
        //string Cajas = "MGW10041";
        //string Perifericos = "MGW10051";
        //string Sucursales = "MGW10063";
        //DIAGRAMA DE RELACION CAJAS Y NOTAS DE VENTA   8
        public string Documentos_Pos = "MGW10008";
        public string Cargos_Abonos = "MGW10009";
        //string Forma_Pago = "MGW10054";
        public string Cobro_Notas = "MGW10043";
        public string Folios_Digitales = "MGW10045";
        public string Datos_Adicionales_Addendas = "MGW10046";
        //TABLAS ADICIONALES    9
        string TAB1 = "MGW10047";
        string TAB2 = "MGW10048";
        string TAB3 = "MGW10049";
        string TAB4 = "MGW10050";
    }
}
