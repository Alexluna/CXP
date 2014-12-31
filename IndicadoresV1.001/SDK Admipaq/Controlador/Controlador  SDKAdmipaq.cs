using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using IndicadoresV1._001.SDK_Admipaq.Modelo;
using System.Windows;
using System.IO;
using System.Data;
using System.Globalization;

namespace IndicadoresV1._001.SDK_Admipaq.Controlador
{
    class Controlador__SDKAdmipaq
    {
        Modelo_SDKAdmipaq ModeloSDK;//objeto para comunicarse con el modelos del sdk
        bool Conexion;//variable que me dira si tengo conexion con el sdk de admipaq
        /// <summary>
        /// Constructor 
        /// </summary>
        public Controlador__SDKAdmipaq()
        {
            ModeloSDK = new Modelo_SDKAdmipaq();
            Conexion = false;//inicializo sin conexion
         }


        #region CONEXIÓN
        /// <summary>
        /// Regresa la coexion si es que se ha realizado
        /// </summary>
        /// <returns>regresa bool si esta o no conectado</returns>
        public bool GetConexion()
        {
            return Conexion;
        }
        /// <summary>
        /// Checa si la path seleccionada esta correcta conforme los archiovs k necesita admipaq
        /// </summary>
        /// <param name="PATH"></param>
        public void ConexionAdmipaq(string PATH)
        {
            List<string> ListArchivosAdmipaq = new List<string>();//para obtener los archivos admipaq
            ListArchivosAdmipaq = ModeloSDK.getListArchivosAdmi();//obtengo los archivos admipaq
            string[] files = Directory.GetFiles(@PATH,"*.dbf*");//obtengo todos los archivos de la carpeta seleccionada con extensión .dbf
            //debo de checar si existen o no los archivos
            int ArchivoValidado = 0;
            for (int i = 0; i < ListArchivosAdmipaq.Count; i++)//recorro la lista de los archivos necesarios
            {
                ArchivoValidado = 0;
                for (int j = 0; j < files.Length; j++)//recorro la lista delos archivo k estan en la carpeta
                {
                    string compara = PATH + "\\" + ListArchivosAdmipaq[i]+".dbf";//genero la ruta con el nombre del archivo y su extension
                    if (compara.Equals(files[j]))
                    {
                        ArchivoValidado = 1;//si existe en la carpeta seleccionada solo me salgo del primero for y pongo mi bandera en 1
                        break;
                    }
                }
                if (ArchivoValidado == 0)//si mi bandera sale con cero del primer for significa que el archvo necesario no esta en la carpeta seleccionada
                { break; }
            }
            if (ArchivoValidado == 0)//si mi bandera sale con cero significa que no estan todos los archivos necesarios y pongo mi conexion como false y mando un mensaje
            {
                Conexion = false;
                MessageBox.Show("Favor de Verificar Su Conexión (Empresa no Válida)");
            }
            else//de lo contrartio pongo mi conexion como tru y mando un mensaje
            {
                Conexion = true;
                ModeloSDK.InicializaPath(PATH);
                MessageBox.Show("Conexión Exitosa");
            }

        }
        #endregion



        #region CONSULTAS CRU FACTURACION
        /// <summary>
        /// consigue los documentos CRU
        /// </summary>
        /// <returns>regresa lo documentos leidos</returns>
        public List<Tipos_Datos_CRU.FacturasCRU> get_Documentos(string fechainicial, string fechafinal)
        {
            List<Tipos_Datos_CRU.FacturasCRU> ListDocmuentos = new List<Tipos_Datos_CRU.FacturasCRU>();//Creo el objeto donde regresare los documentos
            DataTable dtD = ModeloSDK.get_DocumentosCRU(fechainicial, fechafinal);//obtengo los docuemtos en un datatable
            if (dtD == null)//si tiene null siginifica que sucedio algun error 
                return ListDocmuentos;//regresa la lista vacia
            foreach (DataRow row in dtD.Rows)//recorro el databel
            {//si estan entre el filtro de fechas almacenalas en la lista si no no realizar anda
                //if (Convert.ToDateTime(row[7]) >= Convert.ToDateTime(fechainicial) && Convert.ToDateTime(row[7]) <= Convert.ToDateTime(fechafinal))
                //{
                Tipos_Datos_CRU.FacturasCRU newDocument = new Tipos_Datos_CRU.FacturasCRU()//crea el objeto para la lista
                {
                    IdDocumento = Convert.ToString(row[0]),
                    Serie = Convert.ToString(row[1]),
                    Folio = Convert.ToString(row[2]),
                    IDAgente = Convert.ToString(row[3]),
                    RazonSocial = Convert.ToString(row[4]),
                    FechaVencimiento = Convert.ToString(row[5]),
                    RFC = Convert.ToString(row[6]),
                    Fecha = Convert.ToString(row[7]),
                    Subtotal = (float)(double)row[8],
                    Total = (float)(double)row[9],
                    IVA = float.Parse(Math.Round((float)(double)row[9] - (float)(double)row[8], 2).ToString()),//redondea a 2 valores 
                    Pendiente = (float)(double)row[10],
                    TextoExtra1 = Convert.ToString(row[11]),
                    TextoExtra2 = Convert.ToString(row[12]),
                    TextoExtra3 = Convert.ToString(row[13]),
                    Cancelado = Convert.ToString(row[14]),
                    Impreso = Convert.ToString(row[15]),
                    Afectado = Convert.ToString(row[16]),
                    IDCliente = Convert.ToString(row[17]),
                    IDNombreConcepto = Convert.ToString(row[18]),
                    NombreConcepto = ModeloSDK.GetNombreConcepto(Convert.ToString(row[18])),
                    TotalUnidades = (float)(double)row[19]

                };

                DataRow rowagente = ModeloSDK.GETNombreAgente(newDocument.IDAgente);//obtengo el nombre y coidgo del agente
                if (rowagente != null)//si regresa null significa que existio algun error y por lo cual no ara nada
                {
                    newDocument.CodigoAgente = Convert.ToString(rowagente[0]);
                    newDocument.NombreAgente = Convert.ToString(rowagente[1]);
                }
                //se necesitan sacar los datos del cliente/proveedor

                DataRow rowClientePRoveedor = ModeloSDK.GETCLientePRoveedor(newDocument.IDCliente);
                if (rowClientePRoveedor != null)//Saca ñps datos del cliente/proveedor si es que existe
                {
                    Tipos_Datos_CRU.Cliente_Proveedor Proveedor = new Tipos_Datos_CRU.Cliente_Proveedor()
                    {
                        CodigoCliente = Convert.ToString(rowClientePRoveedor[0]),
                        RazonSocial = Convert.ToString(rowClientePRoveedor[1]),
                        ValorClasificación1 = Convert.ToString(rowClientePRoveedor[2]),
                        ValorClasificación2 = Convert.ToString(rowClientePRoveedor[3]),
                        ValorClasificación3 = Convert.ToString(rowClientePRoveedor[4]),
                        Clasificación1 = ModeloSDK.GetValoresClasificacionClientesPRoveedores(Convert.ToString(rowClientePRoveedor[2])),
                        Clasificación2 = ModeloSDK.GetValoresClasificacionClientesPRoveedores(Convert.ToString(rowClientePRoveedor[3])),
                        Clasificación3 = ModeloSDK.GetValoresClasificacionClientesPRoveedores(Convert.ToString(rowClientePRoveedor[4]))

                    };
                    newDocument.proveedor = Proveedor;
                }
                else
                {
                    newDocument.proveedor = null;//sino existen almacena un null
                }
                /*
                   //comienza a sacar los datos de movimientos
                    DataTable dtM = ModeloSDK.get_MovimientosCRU(newDocument.IdDocumento);
                    List<Tipos_Datos_CRU.Movimientos> ListMovimientos = new List<Tipos_Datos_CRU.Movimientos>();
                    foreach (DataRow rowM in dtM.Rows)
                    {
                        Tipos_Datos_CRU.Movimientos newMovimiento = new Tipos_Datos_CRU.Movimientos()
                        {
                            IDProducto=Convert.ToString(rowM[0]),
                            CantidadProducto = Convert.ToString(rowM[1]),
                            PrecioProducto = Convert.ToString(rowM[2]),
                            Importe = (float)(double)rowM[3],
                            Total = (float)(double)rowM[4],
                            IVA = float.Parse(Math.Round((float)(double)rowM[4] - (float)(double)rowM[3], 2).ToString())
                        };
                        //sacar los productos
                        DataRow rowProducto = ModeloSDK.getProductos(newMovimiento.IDProducto);
                        if (rowProducto != null)
                        {
                            Tipos_Datos_CRU.Producto newProducto = new Tipos_Datos_CRU.Producto()
                            {
                                codigo = Convert.ToString(rowProducto[0]),
                                Descripcion = Convert.ToString(rowProducto[1]),
                                ValorClasificación1 = Convert.ToString(rowProducto[2]),
                                ValorClasificación2 = Convert.ToString(rowProducto[3]),
                                ValorClasificación3 = Convert.ToString(rowProducto[4]),
                                Clasifiacion1 = ModeloSDK.GetValoresClasificacionClientesPRoveedores(Convert.ToString(rowProducto[2])),
                                Clasificacion2 = ModeloSDK.GetValoresClasificacionClientesPRoveedores(Convert.ToString(rowProducto[3])),
                                Clasificacion3 = ModeloSDK.GetValoresClasificacionClientesPRoveedores(Convert.ToString(rowProducto[4]))
                            };
                            newMovimiento.producto = newProducto;
                        }
                        else newMovimiento.producto = null;//como tiene producto o marco algun error pon el producto como null
                        ListMovimientos.Add(newMovimiento);
                    }

                    newDocument.Listmovimiento = ListMovimientos;//ya se agregaron la lista de los movimientos
                    //temina y guarda los datos de los movimientos
                   */




                ListDocmuentos.Add(newDocument);
                //}//else MessageBox.Show("fecha");
            }
            return ListDocmuentos;//regresa la lista
        }



       

        #endregion


        #region FILTROS
        /// <summary>
        /// filtro para cualquier RFC
        /// </summary>
        /// <param name="ListDocumentos">lista de documentos donde se realizara el filtro</param>
        /// <param name="RFCFiltro">rfc por el cual se realizara el filtro</param>
        /// <returns></returns>
        public List<Tipos_Datos_CRU.FacturasCRU> FiltroRFCCRU(List<Tipos_Datos_CRU.FacturasCRU> ListDocumentos, string RFCFiltro)
        {
            List<Tipos_Datos_CRU.FacturasCRU> ListFiltroRFC = new List<Tipos_Datos_CRU.FacturasCRU>();
            
            for (int i = 0; i < ListDocumentos.Count; i++)
            {
                if (ListDocumentos[i].RFC.ToUpper().Equals(RFCFiltro.ToUpper()))
                {
                    ListFiltroRFC.Add(ListDocumentos[i]);
                } 

            }
            return ListFiltroRFC;
        }


        public List<Tipos_Datos_CRU.FiltroAgentes> FiltroAgente(List<Tipos_Datos_CRU.FacturasCRU> ListDocumentos)
        {

            List<Tipos_Datos_CRU.FiltroAgentes> ListFiltroAgente = new List<Tipos_Datos_CRU.FiltroAgentes>();
            Tipos_Datos_CRU.FiltroAgentes Agente = new Tipos_Datos_CRU.FiltroAgentes();

            if (ListDocumentos.Count > 0)//choco si existe algo en la lista entonces creo mi primer lista de agente para 
            {
                Agente.IDAgente = ListDocumentos[0].IDAgente;
                Agente.NombreAgente = ListDocumentos[0].NombreAgente;
                List<Tipos_Datos_CRU.FacturasCRU> ProductosCRU = new List<Tipos_Datos_CRU.FacturasCRU>();
                Agente.Listproductos = ProductosCRU;
                Agente.Listproductos.Add(ListDocumentos[0]);//meto el primer valor por default
                ListFiltroAgente.Add(Agente);//agrego todo en la lista donde se realizan las comparaciones
            }
            int exist = 0;
            for (int i = 1; i < ListDocumentos.Count; i++)
            {
                exist = 0;
                for (int j = 0; j < ListFiltroAgente.Count; j++)
                {
                    if (ListFiltroAgente[j].IDAgente.Equals(ListDocumentos[i].IDAgente))
                    {
                        ListFiltroAgente[j].Listproductos.Add(ListDocumentos[i]);
                        exist = 1;
                        break;
                    }
                    
                }
                if (exist == 0)
                {//no existe y se debe de crear un nuevo objeto para meter los datos
                    Tipos_Datos_CRU.FiltroAgentes Agente1 = new Tipos_Datos_CRU.FiltroAgentes();
                    Agente1.IDAgente = ListDocumentos[i].IDAgente;
                    Agente1.NombreAgente = ListDocumentos[i].NombreAgente;
                    List<Tipos_Datos_CRU.FacturasCRU> ProductosCRU = new List<Tipos_Datos_CRU.FacturasCRU>();
                    Agente1.Listproductos = ProductosCRU;
                    Agente1.Listproductos.Add(ListDocumentos[i]);//meto el primer valor por default
                    ListFiltroAgente.Add(Agente);//agrego todo en la lista donde se realizan las comparaciones
                }//fin if
            }//fin for
            return ListFiltroAgente;
        }
        


        #endregion
        #region CONSULTAS CRU ABONOS
        /// <summary>
        /// consigue los documentos CRU
        /// </summary>
        /// <returns>regresa lo documentos leidos</returns>
        public List<Tipos_Datos_CRU.FacturasCRU> get_AbonosCRU(string fechainicial, string fechafinal)
        {
            List<Tipos_Datos_CRU.FacturasCRU> ListDocmuentos = new List<Tipos_Datos_CRU.FacturasCRU>();//Creo el objeto donde regresare los documentos
            DataTable dtD = ModeloSDK.get_AbonosCRU(fechainicial, fechafinal);//obtengo los docuemtos en un datatable
            if (dtD == null)//si tiene null siginifica que sucedio algun error 
                return ListDocmuentos;//regresa la lista vacia
            foreach (DataRow row in dtD.Rows)//recorro el databel
            {//si estan entre el filtro de fechas almacenalas en la lista si no no realizar anda
                //if (Convert.ToDateTime(row[7]) >= Convert.ToDateTime(fechainicial) && Convert.ToDateTime(row[7]) <= Convert.ToDateTime(fechafinal))
                //{
                Tipos_Datos_CRU.FacturasCRU newDocument = new Tipos_Datos_CRU.FacturasCRU()//crea el objeto para la lista
                {
                    IdDocumento = Convert.ToString(row[0]),
                    Serie = Convert.ToString(row[1]),
                    Folio = Convert.ToString(row[2]),
                    IDAgente = Convert.ToString(row[3]),
                    RazonSocial = Convert.ToString(row[4]),
                    FechaVencimiento = Convert.ToString(row[5]),
                    RFC = Convert.ToString(row[6]),
                    Fecha = Convert.ToString(row[7]),
                    Subtotal = (float)(double)row[8],
                    Total = (float)(double)row[9],
                    IVA = float.Parse(Math.Round((float)(double)row[9] - (float)(double)row[8], 2).ToString()),//redondea a 2 valores 
                    Pendiente = (float)(double)row[10],
                    TextoExtra1 = Convert.ToString(row[11]),
                    TextoExtra2 = Convert.ToString(row[12]),
                    TextoExtra3 = Convert.ToString(row[13]),
                    Cancelado = Convert.ToString(row[14]),
                    Impreso = Convert.ToString(row[15]),
                    Afectado = Convert.ToString(row[16]),
                    IDCliente = Convert.ToString(row[17]),
                    IDNombreConcepto = Convert.ToString(row[18]),
                    NombreConcepto = ModeloSDK.GetNombreConcepto(Convert.ToString(row[18])),
                    TotalUnidades = (float)(double)row[19]

                };

                DataRow rowagente = ModeloSDK.GETNombreAgente(newDocument.IDAgente);//obtengo el nombre y coidgo del agente
                if (rowagente != null)//si regresa null significa que existio algun error y por lo cual no ara nada
                {
                    newDocument.CodigoAgente = Convert.ToString(rowagente[0]);
                    newDocument.NombreAgente = Convert.ToString(rowagente[1]);
                }
                //se necesitan sacar los datos del cliente/proveedor

                DataRow rowClientePRoveedor = ModeloSDK.GETCLientePRoveedor(newDocument.IDCliente);
                if (rowClientePRoveedor != null)//Saca ñps datos del cliente/proveedor si es que existe
                {
                    Tipos_Datos_CRU.Cliente_Proveedor Proveedor = new Tipos_Datos_CRU.Cliente_Proveedor()
                    {
                        CodigoCliente = Convert.ToString(rowClientePRoveedor[0]),
                        RazonSocial = Convert.ToString(rowClientePRoveedor[1]),
                        ValorClasificación1 = Convert.ToString(rowClientePRoveedor[2]),
                        ValorClasificación2 = Convert.ToString(rowClientePRoveedor[3]),
                        ValorClasificación3 = Convert.ToString(rowClientePRoveedor[4]),
                        Clasificación1 = ModeloSDK.GetValoresClasificacionClientesPRoveedores(Convert.ToString(rowClientePRoveedor[2])),
                        Clasificación2 = ModeloSDK.GetValoresClasificacionClientesPRoveedores(Convert.ToString(rowClientePRoveedor[3])),
                        Clasificación3 = ModeloSDK.GetValoresClasificacionClientesPRoveedores(Convert.ToString(rowClientePRoveedor[4]))

                    };
                    newDocument.proveedor = Proveedor;
                }
                else
                {
                    newDocument.proveedor = null;//sino existen almacena un null
                }
                /*
                   //comienza a sacar los datos de movimientos
                    DataTable dtM = ModeloSDK.get_MovimientosCRU(newDocument.IdDocumento);
                    List<Tipos_Datos_CRU.Movimientos> ListMovimientos = new List<Tipos_Datos_CRU.Movimientos>();
                    foreach (DataRow rowM in dtM.Rows)
                    {
                        Tipos_Datos_CRU.Movimientos newMovimiento = new Tipos_Datos_CRU.Movimientos()
                        {
                            IDProducto=Convert.ToString(rowM[0]),
                            CantidadProducto = Convert.ToString(rowM[1]),
                            PrecioProducto = Convert.ToString(rowM[2]),
                            Importe = (float)(double)rowM[3],
                            Total = (float)(double)rowM[4],
                            IVA = float.Parse(Math.Round((float)(double)rowM[4] - (float)(double)rowM[3], 2).ToString())
                        };
                        //sacar los productos
                        DataRow rowProducto = ModeloSDK.getProductos(newMovimiento.IDProducto);
                        if (rowProducto != null)
                        {
                            Tipos_Datos_CRU.Producto newProducto = new Tipos_Datos_CRU.Producto()
                            {
                                codigo = Convert.ToString(rowProducto[0]),
                                Descripcion = Convert.ToString(rowProducto[1]),
                                ValorClasificación1 = Convert.ToString(rowProducto[2]),
                                ValorClasificación2 = Convert.ToString(rowProducto[3]),
                                ValorClasificación3 = Convert.ToString(rowProducto[4]),
                                Clasifiacion1 = ModeloSDK.GetValoresClasificacionClientesPRoveedores(Convert.ToString(rowProducto[2])),
                                Clasificacion2 = ModeloSDK.GetValoresClasificacionClientesPRoveedores(Convert.ToString(rowProducto[3])),
                                Clasificacion3 = ModeloSDK.GetValoresClasificacionClientesPRoveedores(Convert.ToString(rowProducto[4]))
                            };
                            newMovimiento.producto = newProducto;
                        }
                        else newMovimiento.producto = null;//como tiene producto o marco algun error pon el producto como null
                        ListMovimientos.Add(newMovimiento);
                    }

                    newDocument.Listmovimiento = ListMovimientos;//ya se agregaron la lista de los movimientos
                    //temina y guarda los datos de los movimientos
                   */




                ListDocmuentos.Add(newDocument);
                //}//else MessageBox.Show("fecha");
            }
            return ListDocmuentos;//regresa la lista
        }
        #endregion


        #region CONSULTAS CRU compras
        /// <summary>
        /// consigue los documentos CRU
        /// </summary>
        /// <returns>regresa lo documentos leidos</returns>
        public List<Tipos_Datos_CRU.FacturasCRU> get_ComprasCRU(string fechainicial, string fechafinal)
        {
            List<Tipos_Datos_CRU.FacturasCRU> ListDocmuentos = new List<Tipos_Datos_CRU.FacturasCRU>();//Creo el objeto donde regresare los documentos
            DataTable dtD = ModeloSDK.get_ComprasCRU(fechainicial, fechafinal);//obtengo los docuemtos en un datatable
            if (dtD == null)//si tiene null siginifica que sucedio algun error 
                return ListDocmuentos;//regresa la lista vacia
            foreach (DataRow row in dtD.Rows)//recorro el databel
            {//si estan entre el filtro de fechas almacenalas en la lista si no no realizar anda
                //if (Convert.ToDateTime(row[7]) >= Convert.ToDateTime(fechainicial) && Convert.ToDateTime(row[7]) <= Convert.ToDateTime(fechafinal))
                //{
                Tipos_Datos_CRU.FacturasCRU newDocument = new Tipos_Datos_CRU.FacturasCRU()//crea el objeto para la lista
                {
                    IdDocumento = Convert.ToString(row[0]),
                    Serie = Convert.ToString(row[1]),
                    Folio = Convert.ToString(row[2]),
                    IDAgente = Convert.ToString(row[3]),
                    RazonSocial = Convert.ToString(row[4]),
                    FechaVencimiento = Convert.ToString(row[5]),
                    RFC = Convert.ToString(row[6]),
                    Fecha = Convert.ToString(row[7]),
                    Subtotal = (float)(double)row[8],
                    Total = (float)(double)row[9],
                    IVA = float.Parse(Math.Round((float)(double)row[9] - (float)(double)row[8], 2).ToString()),//redondea a 2 valores 
                    Pendiente = (float)(double)row[10],
                    TextoExtra1 = Convert.ToString(row[11]),
                    TextoExtra2 = Convert.ToString(row[12]),
                    TextoExtra3 = Convert.ToString(row[13]),
                    Cancelado = Convert.ToString(row[14]),
                    Impreso = Convert.ToString(row[15]),
                    Afectado = Convert.ToString(row[16]),
                    IDCliente = Convert.ToString(row[17]),
                    IDNombreConcepto = Convert.ToString(row[18]),
                    NombreConcepto = ModeloSDK.GetNombreConcepto(Convert.ToString(row[18])),
                    TotalUnidades = (float)(double)row[19]

                };

                DataRow rowagente = ModeloSDK.GETNombreAgente(newDocument.IDAgente);//obtengo el nombre y coidgo del agente
                if (rowagente != null)//si regresa null significa que existio algun error y por lo cual no ara nada
                {
                    newDocument.CodigoAgente = Convert.ToString(rowagente[0]);
                    newDocument.NombreAgente = Convert.ToString(rowagente[1]);
                }
                //se necesitan sacar los datos del cliente/proveedor

                DataRow rowClientePRoveedor = ModeloSDK.GETCLientePRoveedor(newDocument.IDCliente);
                if (rowClientePRoveedor != null)//Saca ñps datos del cliente/proveedor si es que existe
                {
                    Tipos_Datos_CRU.Cliente_Proveedor Proveedor = new Tipos_Datos_CRU.Cliente_Proveedor()
                    {
                        CodigoCliente = Convert.ToString(rowClientePRoveedor[0]),
                        RazonSocial = Convert.ToString(rowClientePRoveedor[1]),
                        ValorClasificación1 = Convert.ToString(rowClientePRoveedor[2]),
                        ValorClasificación2 = Convert.ToString(rowClientePRoveedor[3]),
                        ValorClasificación3 = Convert.ToString(rowClientePRoveedor[4]),
                        Clasificación1 = ModeloSDK.GetValoresClasificacionClientesPRoveedores(Convert.ToString(rowClientePRoveedor[2])),
                        Clasificación2 = ModeloSDK.GetValoresClasificacionClientesPRoveedores(Convert.ToString(rowClientePRoveedor[3])),
                        Clasificación3 = ModeloSDK.GetValoresClasificacionClientesPRoveedores(Convert.ToString(rowClientePRoveedor[4]))

                    };
                    newDocument.proveedor = Proveedor;
                }
                else
                {
                    newDocument.proveedor = null;//sino existen almacena un null
                }
                /*
                   //comienza a sacar los datos de movimientos
                    DataTable dtM = ModeloSDK.get_MovimientosCRU(newDocument.IdDocumento);
                    List<Tipos_Datos_CRU.Movimientos> ListMovimientos = new List<Tipos_Datos_CRU.Movimientos>();
                    foreach (DataRow rowM in dtM.Rows)
                    {
                        Tipos_Datos_CRU.Movimientos newMovimiento = new Tipos_Datos_CRU.Movimientos()
                        {
                            IDProducto=Convert.ToString(rowM[0]),
                            CantidadProducto = Convert.ToString(rowM[1]),
                            PrecioProducto = Convert.ToString(rowM[2]),
                            Importe = (float)(double)rowM[3],
                            Total = (float)(double)rowM[4],
                            IVA = float.Parse(Math.Round((float)(double)rowM[4] - (float)(double)rowM[3], 2).ToString())
                        };
                        //sacar los productos
                        DataRow rowProducto = ModeloSDK.getProductos(newMovimiento.IDProducto);
                        if (rowProducto != null)
                        {
                            Tipos_Datos_CRU.Producto newProducto = new Tipos_Datos_CRU.Producto()
                            {
                                codigo = Convert.ToString(rowProducto[0]),
                                Descripcion = Convert.ToString(rowProducto[1]),
                                ValorClasificación1 = Convert.ToString(rowProducto[2]),
                                ValorClasificación2 = Convert.ToString(rowProducto[3]),
                                ValorClasificación3 = Convert.ToString(rowProducto[4]),
                                Clasifiacion1 = ModeloSDK.GetValoresClasificacionClientesPRoveedores(Convert.ToString(rowProducto[2])),
                                Clasificacion2 = ModeloSDK.GetValoresClasificacionClientesPRoveedores(Convert.ToString(rowProducto[3])),
                                Clasificacion3 = ModeloSDK.GetValoresClasificacionClientesPRoveedores(Convert.ToString(rowProducto[4]))
                            };
                            newMovimiento.producto = newProducto;
                        }
                        else newMovimiento.producto = null;//como tiene producto o marco algun error pon el producto como null
                        ListMovimientos.Add(newMovimiento);
                    }

                    newDocument.Listmovimiento = ListMovimientos;//ya se agregaron la lista de los movimientos
                    //temina y guarda los datos de los movimientos
                   */




                ListDocmuentos.Add(newDocument);
                //}//else MessageBox.Show("fecha");
            }
            return ListDocmuentos;//regresa la lista
        }
        #endregion

        #region CONSULTAS CRU compras
        /// <summary>
        /// consigue los documentos CRU
        /// </summary>
        /// <returns>regresa lo documentos leidos</returns>
        public List<Tipos_Datos_CRU.FacturasCRU> get_PagosProveedorCRU(string fechainicial, string fechafinal)
        {
            List<Tipos_Datos_CRU.FacturasCRU> ListDocmuentos = new List<Tipos_Datos_CRU.FacturasCRU>();//Creo el objeto donde regresare los documentos
            DataTable dtD = ModeloSDK.get_PagosPRoveedorCRU(fechainicial, fechafinal);//obtengo los docuemtos en un datatable
            if (dtD == null)//si tiene null siginifica que sucedio algun error 
                return ListDocmuentos;//regresa la lista vacia
            foreach (DataRow row in dtD.Rows)//recorro el databel
            {//si estan entre el filtro de fechas almacenalas en la lista si no no realizar anda
                //if (Convert.ToDateTime(row[7]) >= Convert.ToDateTime(fechainicial) && Convert.ToDateTime(row[7]) <= Convert.ToDateTime(fechafinal))
                //{
                Tipos_Datos_CRU.FacturasCRU newDocument = new Tipos_Datos_CRU.FacturasCRU()//crea el objeto para la lista
                {
                    IdDocumento = Convert.ToString(row[0]),
                    Serie = Convert.ToString(row[1]),
                    Folio = Convert.ToString(row[2]),
                    IDAgente = Convert.ToString(row[3]),
                    RazonSocial = Convert.ToString(row[4]),
                    FechaVencimiento = Convert.ToString(row[5]),
                    RFC = Convert.ToString(row[6]),
                    Fecha = Convert.ToString(row[7]),
                    Subtotal = (float)(double)row[8],
                    Total = (float)(double)row[9],
                    IVA = float.Parse(Math.Round((float)(double)row[9] - (float)(double)row[8], 2).ToString()),//redondea a 2 valores 
                    Pendiente = (float)(double)row[10],
                    TextoExtra1 = Convert.ToString(row[11]),
                    TextoExtra2 = Convert.ToString(row[12]),
                    TextoExtra3 = Convert.ToString(row[13]),
                    Cancelado = Convert.ToString(row[14]),
                    Impreso = Convert.ToString(row[15]),
                    Afectado = Convert.ToString(row[16]),
                    IDCliente = Convert.ToString(row[17]),
                    IDNombreConcepto = Convert.ToString(row[18]),
                    NombreConcepto = ModeloSDK.GetNombreConcepto(Convert.ToString(row[18])),
                    TotalUnidades = (float)(double)row[19]

                };

                DataRow rowagente = ModeloSDK.GETNombreAgente(newDocument.IDAgente);//obtengo el nombre y coidgo del agente
                if (rowagente != null)//si regresa null significa que existio algun error y por lo cual no ara nada
                {
                    newDocument.CodigoAgente = Convert.ToString(rowagente[0]);
                    newDocument.NombreAgente = Convert.ToString(rowagente[1]);
                }
                //se necesitan sacar los datos del cliente/proveedor

                DataRow rowClientePRoveedor = ModeloSDK.GETCLientePRoveedor(newDocument.IDCliente);
                if (rowClientePRoveedor != null)//Saca ñps datos del cliente/proveedor si es que existe
                {
                    Tipos_Datos_CRU.Cliente_Proveedor Proveedor = new Tipos_Datos_CRU.Cliente_Proveedor()
                    {
                        CodigoCliente = Convert.ToString(rowClientePRoveedor[0]),
                        RazonSocial = Convert.ToString(rowClientePRoveedor[1]),
                        ValorClasificación1 = Convert.ToString(rowClientePRoveedor[2]),
                        ValorClasificación2 = Convert.ToString(rowClientePRoveedor[3]),
                        ValorClasificación3 = Convert.ToString(rowClientePRoveedor[4]),
                        Clasificación1 = ModeloSDK.GetValoresClasificacionClientesPRoveedores(Convert.ToString(rowClientePRoveedor[2])),
                        Clasificación2 = ModeloSDK.GetValoresClasificacionClientesPRoveedores(Convert.ToString(rowClientePRoveedor[3])),
                        Clasificación3 = ModeloSDK.GetValoresClasificacionClientesPRoveedores(Convert.ToString(rowClientePRoveedor[4]))

                    };
                    newDocument.proveedor = Proveedor;
                }
                else
                {
                    newDocument.proveedor = null;//sino existen almacena un null
                }
                /*
                   //comienza a sacar los datos de movimientos
                    DataTable dtM = ModeloSDK.get_MovimientosCRU(newDocument.IdDocumento);
                    List<Tipos_Datos_CRU.Movimientos> ListMovimientos = new List<Tipos_Datos_CRU.Movimientos>();
                    foreach (DataRow rowM in dtM.Rows)
                    {
                        Tipos_Datos_CRU.Movimientos newMovimiento = new Tipos_Datos_CRU.Movimientos()
                        {
                            IDProducto=Convert.ToString(rowM[0]),
                            CantidadProducto = Convert.ToString(rowM[1]),
                            PrecioProducto = Convert.ToString(rowM[2]),
                            Importe = (float)(double)rowM[3],
                            Total = (float)(double)rowM[4],
                            IVA = float.Parse(Math.Round((float)(double)rowM[4] - (float)(double)rowM[3], 2).ToString())
                        };
                        //sacar los productos
                        DataRow rowProducto = ModeloSDK.getProductos(newMovimiento.IDProducto);
                        if (rowProducto != null)
                        {
                            Tipos_Datos_CRU.Producto newProducto = new Tipos_Datos_CRU.Producto()
                            {
                                codigo = Convert.ToString(rowProducto[0]),
                                Descripcion = Convert.ToString(rowProducto[1]),
                                ValorClasificación1 = Convert.ToString(rowProducto[2]),
                                ValorClasificación2 = Convert.ToString(rowProducto[3]),
                                ValorClasificación3 = Convert.ToString(rowProducto[4]),
                                Clasifiacion1 = ModeloSDK.GetValoresClasificacionClientesPRoveedores(Convert.ToString(rowProducto[2])),
                                Clasificacion2 = ModeloSDK.GetValoresClasificacionClientesPRoveedores(Convert.ToString(rowProducto[3])),
                                Clasificacion3 = ModeloSDK.GetValoresClasificacionClientesPRoveedores(Convert.ToString(rowProducto[4]))
                            };
                            newMovimiento.producto = newProducto;
                        }
                        else newMovimiento.producto = null;//como tiene producto o marco algun error pon el producto como null
                        ListMovimientos.Add(newMovimiento);
                    }

                    newDocument.Listmovimiento = ListMovimientos;//ya se agregaron la lista de los movimientos
                    //temina y guarda los datos de los movimientos
                   */




                ListDocmuentos.Add(newDocument);
                //}//else MessageBox.Show("fecha");
            }
            return ListDocmuentos;//regresa la lista
        }
        #endregion





















        #region CONSULTAS MOVIMIENTOS COMPRAS PARA GRAFICAS

        /// <summary>
        /// consigue los movimientos de compras por fecha
        /// </summary>
        /// <returns>regresa lo documentos leidos</returns>
        public List<Tipos_Datos_CRU.Movimientos_Cuentas> get_Movimientos_Compras(string fechainicial, string fechafinal)
        {
            List<Tipos_Datos_CRU.Movimientos_Cuentas> ListMovimientos_Compras = new List<Tipos_Datos_CRU.Movimientos_Cuentas>();//Creo el objeto donde regresare los movimientos de las compras 
            List<Tipos_Datos_CRU.Clasificacion1> List_clasificacion1 = new List<Tipos_Datos_CRU.Clasificacion1>();
            List<Tipos_Datos_CRU.Clasificacion1> List_clasificacion2 = new List<Tipos_Datos_CRU.Clasificacion1>();
            List<Tipos_Datos_CRU.Dataclient> ListCliente = new List<Tipos_Datos_CRU.Dataclient>();
            List<Tipos_Datos_CRU.Clasificacion1> List_clasificacion_producto1 = new List<Tipos_Datos_CRU.Clasificacion1>();
            List<Tipos_Datos_CRU.Clasificacion1> List_clasificacion_producto2 = new List<Tipos_Datos_CRU.Clasificacion1>();
            List<Tipos_Datos_CRU.Clasificacion1> List_clasificacion_producto3 = new List<Tipos_Datos_CRU.Clasificacion1>();

            List<Tipos_Datos_CRU.Producto_data> List_productos_datos = new List<Tipos_Datos_CRU.Producto_data>();
            DataTable dtD = ModeloSDK.get_documentos_Compras(fechainicial, fechafinal);//obtengo los movimientos en un datatable

            if (dtD == null)//si tiene null siginifica que sucedio algun error 
                return ListMovimientos_Compras;//regresa la lista vacia

            foreach (DataRow row in dtD.Rows)//recorro el databel
            {
                string[] fecha = Convert.ToString(row[0]).Split(' ');//obtengo la fecha de la lista 
                //  string[] fecha_partes = fecha[0].Split('/');//separa la fecha por dia mes y año [DIA][MES][AÑO]
                Tipos_Datos_CRU.Movimientos_Cuentas newDocument = new Tipos_Datos_CRU.Movimientos_Cuentas()//crea el objeto para la lista
                {
                    fecha = fecha[0].Trim(),
                    Proveedor = Convert.ToString(row[1]).Trim(),
                    CantidadProducto = Convert.ToString(row[2]).Trim(),
                    PrecioProducto = Convert.ToString(row[3]).Trim(),
                    Subtotal = (float)(double)row[4],
                    IVA = (float)(double)row[5],
                    Total = (float)(double)row[6],
                    ID_doc = Convert.ToString(row[7]).Trim(),
                    IDCliente = Convert.ToString(row[8]).Trim(),
                    pendiente = Convert.ToString(row[9]).Trim(),
                    folio = Convert.ToString(row[10]).Trim()
                };

                //aqui obtneo los datos del movieminto con sus respectivos productos
                DataRow rowproducto = ModeloSDK.get_Movimientos_Productos(newDocument.ID_doc);

                if (rowproducto != null)//si regresa null significa que existio algun error y por lo cual no ara nada
                {
                    newDocument.producto_codigo = Convert.ToString(rowproducto[0]).Trim();
                    newDocument.producto_nombre = Convert.ToString(rowproducto[1]).Trim();
                


                //obtengo las clasificaciones meter en un arreglo los datos de la clasificacion 1 *********************
                int set_clasificaion1 = -8;
                for (int l = 0; l < List_clasificacion1.Count; l++)
                {//si encuentra el mismo id dentro del arreglo entonces solo meto los datos luna mal
                    if (List_clasificacion1[l].id.Trim().Equals(Convert.ToString(rowproducto[2])))
                    {
                        set_clasificaion1 = l;
                        break;
                    }
                    
                }
                if (set_clasificaion1 < 0)
                {
                    string resp = Convert.ToString(ModeloSDK.getclasificacion(Convert.ToString(rowproducto[2]).Trim()));
                    string[] words = resp.Split(',');
                    newDocument.Valor_Clasificacion_1_producto = words[0].Trim();
                    newDocument.Clasificacion_1_producto = words[1].Trim();

                    Tipos_Datos_CRU.Clasificacion1 new_clasif = new Tipos_Datos_CRU.Clasificacion1()
                    {
                        id = Convert.ToString(rowproducto[2]),
                        nombre_clasifiacion1 = resp
                    };
                    List_clasificacion1.Add(new_clasif);
                }
                else {

                    string resp = List_clasificacion1[set_clasificaion1].nombre_clasifiacion1;
                    string[] words = resp.Split(',');
                    newDocument.Valor_Clasificacion_1_producto = words[0].Trim();
                    newDocument.Clasificacion_1_producto = words[1].Trim();
                }
                //***********************************************************************************
                int set_clasificaion2 = -8;
                for (int l = 0; l < List_clasificacion2.Count; l++)
                {//si encuentra el mismo id dentro del arreglo entonces solo meto los datos 
                    if (List_clasificacion2[l].id.Trim().Equals(Convert.ToString(rowproducto[3])))
                    {
                        set_clasificaion2 = l;
                        break;
                    }

                }
                if (set_clasificaion2 < 0)
                {
                    //obtengo las clasificaciones meter en un arreglo los datos de la clasificacion 2 *****************
                    string resp2 = Convert.ToString(ModeloSDK.getclasificacion(Convert.ToString(rowproducto[3]).Trim()));
                    string[] words2 = resp2.Split(',');
                    newDocument.Valor_Clasificacion_2_producto = words2[0].Trim();
                    newDocument.Clasificacion_2_producto = words2[1].Trim();

                    Tipos_Datos_CRU.Clasificacion1 new_clasif = new Tipos_Datos_CRU.Clasificacion1()
                    {
                        id = Convert.ToString(rowproducto[3]),
                        nombre_clasifiacion1 = resp2
                    };
                    List_clasificacion2.Add(new_clasif);
                }
                else
                {

                    string resp = List_clasificacion2[set_clasificaion2].nombre_clasifiacion1;
                    string[] words = resp.Split(',');
                    newDocument.Valor_Clasificacion_2_producto = words[0].Trim();
                    newDocument.Clasificacion_2_producto = words[1].Trim();
                }

                //************************************
                }//rowproducto mal


                //guardar los datos de los clietne **********
                int set_cliente = -8;
                for (int l = 0; l < ListCliente.Count; l++)
                {
                    if (ListCliente[l].id.Trim().Equals(newDocument.IDCliente.Trim()))
                    {
                        set_cliente = l;
                        break;
                    }
                }

                if (set_cliente < 0)
                {
                    DataRow rowprovedor = ModeloSDK.get_codigo_proveedor(newDocument.IDCliente);//obtengo el codigo del provedor  sus valores 

                    if (rowprovedor != null)
                    {
                        newDocument.Proveedor_codigo = Convert.ToString(rowprovedor[0]);

                        string response = Convert.ToString(ModeloSDK.getclasificacion(Convert.ToString(rowprovedor[1])));
                        string[] words_ = response.Split(',');
                        newDocument.Valor_Clasificacion_1_proveedor = words_[0].Trim();
                        newDocument.Clasificacion_1_proveedor = words_[1].Trim();


                        string responce2 = Convert.ToString(ModeloSDK.getclasificacion(Convert.ToString(rowprovedor[2])));
                        string[] words2_ = responce2.Split(',');
                        newDocument.Valor_Clasificacion_2_proveedor = words2_[0].Trim();
                        newDocument.Clasificacion_2_proveedor = words2_[1].Trim();
                        Tipos_Datos_CRU.Dataclient new_clien = new Tipos_Datos_CRU.Dataclient()
                        {
                            id = newDocument.IDCliente,
                            nombre_cliente = Convert.ToString(rowprovedor[0]),
                            clasificacion_1 = response,
                            clasificacion_2 = responce2
                        };
                        ListCliente.Add(new_clien);
                    }
                }
                else {
                    newDocument.Proveedor_codigo = ListCliente[set_cliente].nombre_cliente;

                    string response = ListCliente[set_cliente].clasificacion_1;
                    string[] words_ = response.Split(',');
                    newDocument.Valor_Clasificacion_1_proveedor = words_[0].Trim();
                    newDocument.Clasificacion_1_proveedor = words_[1].Trim();


                    string responce2 = ListCliente[set_cliente].clasificacion_1;
                    string[] words2_ = responce2.Split(',');
                    newDocument.Valor_Clasificacion_2_proveedor = words2_[0].Trim();
                    newDocument.Clasificacion_2_proveedor = words2_[1].Trim();
                }

               


                //comienza a sacar los datos de movimientos
                DataTable dtM = ModeloSDK.get_MovimientosCRU(newDocument.ID_doc);
                List<Tipos_Datos_CRU.Movimientos1> ListMovimientos = new List<Tipos_Datos_CRU.Movimientos1>();
                foreach (DataRow rowM in dtM.Rows)
                {
                    Tipos_Datos_CRU.Movimientos1 newMovimiento = new Tipos_Datos_CRU.Movimientos1()
                    {
                        IDProducto = Convert.ToString(rowM[0]).Trim(),
                        CantidadProducto = Convert.ToString(rowM[1]).Trim(),
                        PrecioProducto = Convert.ToString(rowM[2]).Trim(),
                        Importe = (float)(double)rowM[3],
                        Total = (float)(double)rowM[4],
                        IVA = float.Parse(Math.Round((float)(double)rowM[4] - (float)(double)rowM[3], 2).ToString())
                    };

                    //sacar el dato del producto   ******************************************
                    int set_product=-8;
                    for (int l = 0; l < List_productos_datos.Count; l++)
			        {
			            if(List_productos_datos[l].id.Trim().Equals(newMovimiento.IDProducto.Trim()))
                        {
                            set_product=l;
                            break;
                        }
			        }
                    if(set_product<0)
                    {//saco los datos del admipaq
                        //sacar los productos
                    DataRow rowProducto = ModeloSDK.getProductos(newMovimiento.IDProducto);
                    if (rowProducto != null)
                    {
                        Tipos_Datos_CRU.Producto1 newProducto = new Tipos_Datos_CRU.Producto1()
                        {
                            codigo = Convert.ToString(rowProducto[0]).Trim(),
                            Descripcion = Convert.ToString(rowProducto[1]).Trim(),
                            ValorClasificación1 = Convert.ToString(rowProducto[2]).Trim(),
                            ValorClasificación2 = Convert.ToString(rowProducto[3]).Trim(),
                            ValorClasificación3 = Convert.ToString(rowProducto[4]).Trim(),
                        };
                        //clasificacion 1 de productos 
                        int clasifiacion1_produto = -8;
                        for (int l = 0; l < List_clasificacion_producto1.Count; l++)
                        {
                            if (List_clasificacion_producto1[l].id.Trim().Equals(newProducto.ValorClasificación1.Trim()))
                            {
                                clasifiacion1_produto = l;
                                break;
                            }
                        }
                        if (clasifiacion1_produto < 0)
                        {
                            newProducto.Clasifiacion1 = ModeloSDK.GetValoresClasificacionClientesPRoveedores(Convert.ToString(newProducto.ValorClasificación1.Trim()));
                            Tipos_Datos_CRU.Clasificacion1 new_clas_produc = new Tipos_Datos_CRU.Clasificacion1()
                            {
                                id = newProducto.ValorClasificación1.Trim(),
                                nombre_clasifiacion1 = newProducto.Clasifiacion1
                            };
                            List_clasificacion_producto1.Add(new_clas_produc);
                        }
                        else {
                            newProducto.Clasifiacion1 = List_clasificacion_producto1[clasifiacion1_produto].nombre_clasifiacion1;
                        }
                        //fin clasifiacion 1 de productos
                        //clasificacion 2 de productos
                        int clasifiacion2_produto = -8;
                        for (int l = 0; l < List_clasificacion_producto2.Count; l++)
                        {
                            if (List_clasificacion_producto2[l].id.Trim().Equals(newProducto.ValorClasificación2.Trim()))
                            {
                                clasifiacion2_produto = l;
                                break;
                            }
                        }
                        if (clasifiacion2_produto < 0)
                        {
                            newProducto.Clasificacion2 = ModeloSDK.GetValoresClasificacionClientesPRoveedores(Convert.ToString(newProducto.ValorClasificación2.Trim()));
                            Tipos_Datos_CRU.Clasificacion1 new_clas_produc = new Tipos_Datos_CRU.Clasificacion1()
                            {
                                id = newProducto.ValorClasificación2.Trim(),
                                nombre_clasifiacion1 = newProducto.Clasificacion2
                            };
                            List_clasificacion_producto2.Add(new_clas_produc);
                        }
                        else
                        {
                            newProducto.Clasificacion2 = List_clasificacion_producto2[clasifiacion2_produto].nombre_clasifiacion1;
                        }
                        //fin clasificacion 2 de productos
                        //clasificacion 3 de productos
                        int clasifiacion3_produto = -8;
                        for (int l = 0; l < List_clasificacion_producto3.Count; l++)
                        {
                            if (List_clasificacion_producto3[l].id.Trim().Equals(newProducto.ValorClasificación3.Trim()))
                            {
                                clasifiacion3_produto = l;
                                break;
                            }
                        }
                        if (clasifiacion3_produto < 0)
                        {
                            newProducto.Clasificacion3 = ModeloSDK.GetValoresClasificacionClientesPRoveedores(newProducto.ValorClasificación3.Trim());
                            Tipos_Datos_CRU.Clasificacion1 new_clas_produc = new Tipos_Datos_CRU.Clasificacion1()
                            {
                                id = newProducto.ValorClasificación3.Trim(),
                                nombre_clasifiacion1 = newProducto.Clasificacion3
                            };
                            List_clasificacion_producto3.Add(new_clas_produc);
                        }
                        else
                        {
                            newProducto.Clasificacion3 = List_clasificacion_producto3[clasifiacion3_produto].nombre_clasifiacion1;
                        }
                        //fin clasificacion 3 de productos luna
                        Tipos_Datos_CRU.Producto_data new_prod = new Tipos_Datos_CRU.Producto_data()
                        {
                            id = newMovimiento.IDProducto,
                            codigo = newProducto.codigo,
                            Descripcion = newProducto.codigo,
                            ValorClasificación1 = newProducto.ValorClasificación1,
                            ValorClasificación2 = newProducto.ValorClasificación2,
                            ValorClasificación3 = newProducto.ValorClasificación3,
                            Clasifiacion1 = newProducto.Clasifiacion1,
                            Clasificacion2 = newProducto.Clasificacion2,
                            Clasificacion3 = newProducto.Clasificacion3,
                        };
                        List_productos_datos.Add(new_prod);
                        newMovimiento.producto = newProducto;
                    }
                    else newMovimiento.producto = null;//como tiene producto o marco algun error pon el producto como null
                    ListMovimientos.Add(newMovimiento);
                    }else{//ya tengo los datos almacenados en el arreglo de list_product
                        //sacar los productos
                    
                        Tipos_Datos_CRU.Producto1 newProducto = new Tipos_Datos_CRU.Producto1()
                        {
                            codigo = List_productos_datos[set_product].codigo,
                            Descripcion = List_productos_datos[set_product].Descripcion,
                            ValorClasificación1 = List_productos_datos[set_product].ValorClasificación1,
                            ValorClasificación2 = List_productos_datos[set_product].ValorClasificación2,
                            ValorClasificación3 = List_productos_datos[set_product].ValorClasificación3,
                            Clasifiacion1 = List_productos_datos[set_product].Clasifiacion1,
                            Clasificacion2 = List_productos_datos[set_product].Clasificacion2,
                            Clasificacion3 = List_productos_datos[set_product].Clasificacion3,
                            
                        };
                        
                        //fin clasificacion 3 de productos
                        newMovimiento.producto = newProducto;
                    
                            ListMovimientos.Add(newMovimiento);

                    }
                 //}//no null
                    
                }

                newDocument.Listmovimiento = ListMovimientos;//ya se agregaron la lista de los movimientos
                //temina y guarda los datos de los movimientos

                ListMovimientos_Compras.Add(newDocument);
            }
            return ListMovimientos_Compras;//regresa la lista
        }
        #endregion
    }
}
