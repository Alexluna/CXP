using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.OleDb;
using System.Data;
using System.Windows;

namespace IndicadoresV1._001.SDK_Admipaq.Modelo
{
    class Modelo_SDKAdmipaq
    {
        Nombre_Archivos archivosAdmi;//para tener acceso a los archivos admipaq
        string path;//alamcenara la ruta de la empresa
        string connStr;//para realizar conexion con la empresa
        OleDbConnection conn;//COnexion para .DBF
        /// <summary>
        /// Constructor
        /// </summary>
        public Modelo_SDKAdmipaq()
        {
            archivosAdmi = new Nombre_Archivos();
            path = "";
            connStr = "";
        }

        #region INICIALIZA PATH DE LA EMPRESA
        /// <summary>
        /// Inicializa la PAth de donde se encuentra la emrpesa
        /// </summary>
        /// <param name="Path"></param>
        public void InicializaPath(string Path)
        {
            this.path = Path;
            PropiedadesAccesoRuta();
        }
        /// <summary>
        /// Inicializa la conexión con la empresa
        /// </summary>
        private void PropiedadesAccesoRuta()
        {
            connStr = @"Provider=VFPOLEDB.1;Data Source=" + path + ";Collating Sequence=MACHINE;";
            conn = new OleDbConnection(connStr);
        }
        #endregion

        #region GET LISTA ARCHIOS ADMIPAQ
        /// <summary>
        /// Regresa la lista con los archivos que son necesarios para la comunicacion con admipaq
        /// </summary>
        /// <returns>Regresa la lista</returns>
        public List<string> getListArchivosAdmi()
        {
            return archivosAdmi.Getlista();
        }

        #endregion


        #region CONSULTA CRU
        /// <summary>
        /// consigue los documentos entre fechas de CRU
        /// </summary>
        /// <returns>regresa table el cual tiene los datos de la tabla a la cual se apunto</returns>
        public DataTable get_DocumentosCRU(string fechainicial, string fechafinal)
        {
            try
            {
                conn.Open();//abre la conexion  ************************ CIDDOCUM02=4 and CIDCONCE01=3007 esto es para  que agarre solo facturas cfdi  conforme al archivo  MGW10006********************************
                string cmd_string = " select CIDDOCUM01,CSERIEDO01,CFOLIO,CIDAGENTE,CRAZONSO01,CFECHAVE01,CRFC,CFECHA,CNETO,CTOTAL,CPENDIENTE,CTEXTOEX01,CTEXTOEX02,CTEXTOEX03,CCANCELADO,CIMPRESO,CAFECTADO,CIDCLIEN01,CIDCONCE01,CUNIDADE01 from " + archivosAdmi.Documentos + " where CIDDOCUM02=4 and CIDCONCE01=3007 and between( CFECHA, ctod( \"" + fechainicial + "\" ), ctod( \"" + fechafinal + "\" ))";
                OleDbDataAdapter da = new OleDbDataAdapter(cmd_string, conn);
                DataSet ds = new DataSet();
                da.Fill(ds);
                DataTable dtt = ds.Tables[0];
                conn.Close();//cierra la conexion
                return dtt;
            }
            catch (Exception g)
            {
                conn.Close();//siempre cierra la conexion
                MessageBox.Show(g.Message);
                return null;
            }
            
        }

        /// <summary>
        /// Obtener el nobre del agente y el codigo del agente por medio de su id
        /// </summary>
        /// <param name="idCodigo">id del agente el cual se kiere saber el nombre</param>
        /// <returns>regresa el nombre dle agente</returns>
        public DataRow GETNombreAgente(string idCodigo)
        {
            try
            {
                conn.Open();//abre la conexion
                string cmd_string = " select CCODIGOA01,CNOMBREa01 from " + archivosAdmi.Agentes + " where CIDAGENTE=" + Convert.ToInt32(idCodigo);
                OleDbDataAdapter da = new OleDbDataAdapter(cmd_string, conn);
                DataSet ds = new DataSet();
                da.Fill(ds);
                DataTable dtt = ds.Tables[0];
                conn.Close();//cierra la conexion
                DataRow row = dtt.Rows[0];
                return row;
            }
            catch (Exception g)
            {
                conn.Close();//siempre cierra la conexion
                MessageBox.Show(g.Message);
                return null;
            }
        }


        /// <summary>
        /// obtiene los datos del cliente proveedor
        /// </summary>
        /// <param name="idcliente">id cpor el cual se buscaran los datos dle cliente</param>
        /// <returns>datarow que regresara los dartos del cliente proveedor</returns>
        public DataRow GETCLientePRoveedor(string idcliente)
        {
            try
            {
                conn.Open();//abre la conexion
                string cmd_string = " select CCODIGOC01,CRAZONSO01,CIDVALOR07,CIDVALOR08,CIDVALOR09 from " + archivosAdmi.Clientes_Proveedores + " where CIDCLIEN01=" + Convert.ToInt32(idcliente);
                OleDbDataAdapter da = new OleDbDataAdapter(cmd_string, conn);
                DataSet ds = new DataSet();
                da.Fill(ds);
                DataTable dtt = ds.Tables[0];
                conn.Close();//cierra la conexion
                DataRow row = dtt.Rows[0];
                return row;
            }
            catch (Exception g)
            {
                conn.Close();//siempre cierra la conexion
                MessageBox.Show(g.Message);
                return null;
            }
        }

        /// <summary>
        /// metodo para obntener los valores de clasificacion
        /// </summary>
        /// <param name="idvalorClasificacion">id el cual esde un valor de clasificacion</param>
        /// <returns>regresa el valor de clasificacion que se requiere</returns>
        public string GetValoresClasificacionClientesPRoveedores(string idvalorClasificacion)
        {
            try
            {

                conn.Open();//abre la conexion
                string cmd_string = " select CVALORCL01 from " + archivosAdmi.ValoresClasificacion + " where CIDVALOR01=" + Convert.ToInt32(idvalorClasificacion);
                OleDbDataAdapter da = new OleDbDataAdapter(cmd_string, conn);
                DataSet ds = new DataSet();
                da.Fill(ds);
                DataTable dtt = ds.Tables[0];
                conn.Close();//cierra la conexion
                DataRow row = dtt.Rows[0];
                return row[0].ToString();
            }
            catch (Exception)
            {

                return "";
            }
            
        }
        /// <summary>
        /// obtiene el nombre de concepto que se requiere saber
        /// </summary>
        /// <param name="idnombreconcepto">id del nombre deconcepto que se requiere saber</param>
        /// <returns>regresa el nombre del concepto el cual se pidio</returns>
        public string GetNombreConcepto(string idnombreconcepto)
        {
            try
            {

                conn.Open();//abre la conexion
                string cmd_string = " select CNOMBREC01 from " + archivosAdmi.Conceptos + " where CIDCONCE01=" + Convert.ToInt32(idnombreconcepto);
                OleDbDataAdapter da = new OleDbDataAdapter(cmd_string, conn);
                DataSet ds = new DataSet();
                da.Fill(ds);
                DataTable dtt = ds.Tables[0];
                conn.Close();//cierra la conexion
                DataRow row = dtt.Rows[0];
                return row[0].ToString();
            }
            catch (Exception)
            {

                return "";
            }
            
        }

        /// <summary>
        /// Obtengo todos los movimientos que contiene un documento
        /// </summary>
        /// <param name="idDocumento">id del documento que se requieren los movimientos</param>
        /// <returns>regresa ek datatable que contiene los movientos de un documento</returns>
        public DataTable get_MovimientosCRU(string idDocumento)
        {
            try
            {
                conn.Open();//abre la conexion
                string cmd_string = " select CIDPRODU01,CUNIDADES,CPRECIO,CNETO,CTOTAL from " + archivosAdmi.Movimientos + " where CIDDOCUM01=" + Convert.ToInt32(idDocumento);
                OleDbDataAdapter da = new OleDbDataAdapter(cmd_string, conn);
                DataSet ds = new DataSet();
                da.Fill(ds);
                DataTable dtt = ds.Tables[0];
                conn.Close();//cierra la conexion
                return dtt;
            }
            catch (Exception g)
            {
                conn.Close();//siempre cierra la conexion
                MessageBox.Show(g.Message);
                return null;
            }

        }


        public DataRow getProductos(string idproducto)
        {
            try
            {
                conn.Open();//abre la conexion
                string cmd_string = " select CCODIGOP01,CNOMBREP01,CIDVALOR01,CIDVALOR02,CIDVALOR03 from " + archivosAdmi.Productos + " where CIDPRODU01=" + Convert.ToInt32(idproducto);
                OleDbDataAdapter da = new OleDbDataAdapter(cmd_string, conn);
                DataSet ds = new DataSet();
                da.Fill(ds);
                DataTable dtt = ds.Tables[0];
                conn.Close();//cierra la conexion
                DataRow row = dtt.Rows[0];
                return row;
            }
            catch (Exception g)
            {
                conn.Close();//siempre cierra la conexion
                MessageBox.Show(g.Message);
                return null;
            }
        }


        public void GetUnicaConsulta()
        {
 
        }
        #endregion


        /// <summary>
        /// Abonos CRU  entre rangos de fechas
        /// </summary>
        /// <param name="fechainicial">fecha inical para el filtro</param>
        /// <param name="fechafinal">fecha final del filtro</param>
        /// <returns>regresa una tabla que cotiene los datos apuntados</returns>
        public DataTable get_AbonosCRU(string fechainicial, string fechafinal)
        {
            try
            {
                conn.Open();//abre la conexion  ************************ CIDDOCUM02=12 and CIDCONCE01=13esto es para  que agarre solo abonos de cliente  conforme al archivo  MGW10006********************************
                string cmd_string = " select CIDDOCUM01,CSERIEDO01,CFOLIO,CIDAGENTE,CRAZONSO01,CFECHAVE01,CRFC,CFECHA,CNETO,CTOTAL,CPENDIENTE,CTEXTOEX01,CTEXTOEX02,CTEXTOEX03,CCANCELADO,CIMPRESO,CAFECTADO,CIDCLIEN01,CIDCONCE01,CUNIDADE01 from " + archivosAdmi.Documentos + " where CIDDOCUM02=12 and CIDCONCE01=13 and between( CFECHA, ctod( \"" + fechainicial + "\" ), ctod( \"" + fechafinal + "\" ))";
                OleDbDataAdapter da = new OleDbDataAdapter(cmd_string, conn);
                DataSet ds = new DataSet();
                da.Fill(ds);
                DataTable dtt = ds.Tables[0];
                conn.Close();//cierra la conexion
                return dtt;
            }
            catch (Exception g)
            {
                conn.Close();//siempre cierra la conexion
                MessageBox.Show(g.Message);
                return null;
            }

        }



        /// <summary>
        /// compras CRU  entre rangos de fechas
        /// </summary>
        /// <param name="fechainicial">fecha inical para el filtro</param>
        /// <param name="fechafinal">fecha final del filtro</param>
        /// <returns>regresa una tabla que cotiene los datos apuntados</returns>
        public DataTable get_ComprasCRU(string fechainicial, string fechafinal)
        {
            try
            {
                conn.Open();//abre la conexion  ************************ CIDDOCUM02=12 and CIDCONCE01=13esto es para  que agarre solo abonos de cliente  conforme al archivo  MGW10006********************************
                string cmd_string = " select CIDDOCUM01,CSERIEDO01,CFOLIO,CIDAGENTE,CRAZONSO01,CFECHAVE01,CRFC,CFECHA,CNETO,CTOTAL,CPENDIENTE,CTEXTOEX01,CTEXTOEX02,CTEXTOEX03,CCANCELADO,CIMPRESO,CAFECTADO,CIDCLIEN01,CIDCONCE01,CUNIDADE01 from " + archivosAdmi.Documentos + " where CIDDOCUM02=19 and CIDCONCE01=21 and between( CFECHA, ctod( \"" + fechainicial + "\" ), ctod( \"" + fechafinal + "\" ))";
                OleDbDataAdapter da = new OleDbDataAdapter(cmd_string, conn);
                DataSet ds = new DataSet();
                da.Fill(ds);
                DataTable dtt = ds.Tables[0];
                conn.Close();//cierra la conexion
                return dtt;
            }
            catch (Exception g)
            {
                conn.Close();//siempre cierra la conexion
                MessageBox.Show(g.Message);
                return null;
            }

        }

        /// <summary>
        /// PAgos CRU  entre rangos de fechas
        /// </summary>
        /// <param name="fechainicial">fecha inical para el filtro</param>
        /// <param name="fechafinal">fecha final del filtro</param>
        /// <returns>regresa una tabla que cotiene los datos apuntados</returns>
        public DataTable get_PagosPRoveedorCRU(string fechainicial, string fechafinal)
        {
            try
            {
                conn.Open();//abre la conexion  ************************ CIDDOCUM02=12 and CIDCONCE01=13esto es para  que agarre solo abonos de cliente  conforme al archivo  MGW10006********************************
                string cmd_string = " select CIDDOCUM01,CSERIEDO01,CFOLIO,CIDAGENTE,CRAZONSO01,CFECHAVE01,CRFC,CFECHA,CNETO,CTOTAL,CPENDIENTE,CTEXTOEX01,CTEXTOEX02,CTEXTOEX03,CCANCELADO,CIMPRESO,CAFECTADO,CIDCLIEN01,CIDCONCE01,CUNIDADE01 from " + archivosAdmi.Documentos + " where CIDDOCUM02=23 and CIDCONCE01=25 and between( CFECHA, ctod( \"" + fechainicial + "\" ), ctod( \"" + fechafinal + "\" ))";
                OleDbDataAdapter da = new OleDbDataAdapter(cmd_string, conn);
                DataSet ds = new DataSet();
                da.Fill(ds);
                DataTable dtt = ds.Tables[0];
                conn.Close();//cierra la conexion
                return dtt;
            }
            catch (Exception g)
            {
                conn.Close();//siempre cierra la conexion
                MessageBox.Show(g.Message);
                return null;
            }

        }

        #region COMPRAS PARA GRAFICAS

        /// <summary>
        /// OBTIENE LOS MOVIMIENTOS DE COMPRAS POR FECHA 
        /// </summary>
        /// <param name="fechainicial"></param>
        /// <param name="fechafinal"></param>
        /// <returns></returns>
        public DataTable get_documentos_Compras(string fechainicial, string fechafinal)
        {
            try
            {
                conn.Open();//abre la conexion  ************************ CIDDOCUM02=4 and CIDCONCE01=3007 esto es para  que agarre solo facturas cfdi  conforme al archivo  MGW10006********************************
                string cmd_string = " select CFECHA,CRAZONSO01,CUNIDADE01,CTOTALUN01,CNETO,CIMPUESTO1,CTOTAL,CIDDOCUM01,CIDCLIEN01,CPENDIENTE,CFOLIO from " + archivosAdmi.Documentos + " where CIDDOCUM02=19 and CIDCONCE01=21 and between( CFECHA, ctod( \"" + fechainicial + "\" ), ctod( \"" + fechafinal + "\" ))";
                OleDbDataAdapter da = new OleDbDataAdapter(cmd_string, conn);
                DataSet ds = new DataSet();
                da.Fill(ds);
                DataTable dtt = ds.Tables[0];
                conn.Close();//cierra la conexion
                return dtt;
            }
            catch (Exception)
            {
                conn.Close();//siempre cierra la conexion
                return null;
            }

        }

        /// <summary>
        /// obtiene los datos de los productos pra mostrar en movimientos compras 
        /// </summary>
        /// <param name="idDocumento"></param>
        /// <returns></returns>
        public DataRow get_Movimientos_Productos(string idDocumento)
        {
            try
            {
                #region ******* se hace la primera consulta para obtener el id del producto de movimientos
                conn.Open();//abre la conexion
                string cmd_string = " select CIDPRODU01 from " + archivosAdmi.Movimientos + " where CIDDOCUM01=" + Convert.ToInt32(idDocumento);
                OleDbDataAdapter da = new OleDbDataAdapter(cmd_string, conn);
                DataSet ds = new DataSet();
                da.Fill(ds);
                DataTable dtt = ds.Tables[0];
                conn.Close();//cierra la conexion
                DataRow row1 = dtt.Rows[0];
                string id_producto = Convert.ToString(row1[0]);
                #endregion

                #region *****segunda consuta para obtener los datos que se necesitan de productos****************
                conn.Open();//abre la conexion  ************************ 
                string cmd_string_ = "select CCODIGOP01,CNOMBREP01,CIDVALOR01,CIDVALOR02 from " + archivosAdmi.Productos + " where CIDPRODU01=" + Convert.ToInt32(id_producto);
                OleDbDataAdapter daf = new OleDbDataAdapter(cmd_string_, conn);
                DataSet dsf = new DataSet();
                daf.Fill(dsf);
                DataTable dtt2 = dsf.Tables[0];
                DataRow row2 = dtt2.Rows[0];
                conn.Close();//cierra la conexion
                return row2;// regreso los datos de los productos
                #endregion

            }
            catch (Exception)
            {
                conn.Close();//siempre cierra la conexion
                return null;
            }

        }

        public string getclasificacion(string valor1)
        {
            try
            {
                conn.Open();//abre la conexion
                string cmd_string = " select CCODIGOV01, CVALORCL01  from " + archivosAdmi.ValoresClasificacion + " where CIDVALOR01=" + valor1;
                OleDbDataAdapter da = new OleDbDataAdapter(cmd_string, conn);
                DataSet ds = new DataSet();
                da.Fill(ds);
                DataTable dtt = ds.Tables[0];
                conn.Close();//cierra la conexion
                DataRow row1 = dtt.Rows[0];

                string row = Convert.ToString(row1[0]) + "," + Convert.ToString(row1[1]);

                return row;
            }
            catch (Exception g)
            {
                conn.Close();//siempre cierra la conexion
                MessageBox.Show(g.Message);
                return null;
            }
        }

        public DataRow get_codigo_proveedor(string valor1)
        {
            try
            {
                conn.Open();//abre la conexion
                string cmd_string = " select CCODIGOC01,CIDVALOR07,CIDVALOR08 from " + archivosAdmi.Clientes_Proveedores + " where CIDCLIEN01=" + Convert.ToInt32(valor1);
                OleDbDataAdapter da = new OleDbDataAdapter(cmd_string, conn);
                DataSet ds = new DataSet();
                da.Fill(ds);
                DataTable dtt = ds.Tables[0];
                conn.Close();//cierra la conexion
                DataRow row1 = dtt.Rows[0];
                return row1;
            }
            catch (Exception)
            {
                conn.Close();//siempre cierra la conexion
                return null;
            }
        }
        #endregion

    }
}
