using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Forms;
using IndicadoresV1._001.SDK_Admipaq.Controlador;
using IndicadoresV1._001.SDK_Admipaq.Modelo;
using System.Globalization;

namespace IndicadoresV1._001.Vista.CRU
{
    /// <summary>
    /// Lógica de interacción para Indicadores_CRU.xaml
    /// </summary>
    public partial class Indicadores_CRU 
    {
        Controlador__SDKAdmipaq controladorSDK;//para lalamr al controlador del sdk admipaq
        List<Tipos_Datos_CRU.FacturasCRU> ListDocmuentos;//liusta de todo los documentos
        Controlador_Impresion controlaimpresion;//para poder mandar a imprimir en PDF
        public Indicadores_CRU()
        {
            InitializeComponent();
            controladorSDK = new Controlador__SDKAdmipaq();//para manear y llamar metodos del controlador
            controlaimpresion = new Controlador_Impresion();
        }


        /// <summary>
        /// Método para seleccionar una ruta de alguna empresa
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Selecciona_Click(object sender, RoutedEventArgs e)
        {
            FolderBrowserDialog folderBrowserDialog1 = new FolderBrowserDialog();
            if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
            {
                controladorSDK.ConexionAdmipaq(folderBrowserDialog1.SelectedPath);//Verifica si la carpeta seleccionada es correcta
                if (controladorSDK.GetConexion())
                {//como es correcta imprime en el label la direccion de la empresa
                    RuteEmpresa.Text = folderBrowserDialog1.SelectedPath + "\\";
                    //ahora ya tienes la empresa correctamente seleccionada
                }
            }
        }


        /// <summary>
        /// Obten todos los datos de los documentos
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, RoutedEventArgs e)
        {
            //String pp = "25.55";
            //System.Windows.MessageBox.Show(pp);
            //System.Windows.MessageBox.Show(float.Parse(pp, CultureInfo.InvariantCulture.NumberFormat)+"");
            if (controladorSDK.GetConexion())//antes de hacer algo verifico si existe alguna conexion con alguna empresa
            {
                ListDocmuentos = new List<Tipos_Datos_CRU.FacturasCRU>();//inicializo mi lista donde tendramis documentos
                int mes = Convert.ToInt32(comboBoxMes.SelectedIndex) + 1;//obtengo el mes del cual se realizara el filtro
                string fechainicial = mes + "/01" +  "/" + textBoxanio.Text;//obtengo mi fechaincial para mi filtro
                string fechafinal = mes + "/31" + "/" + textBoxanio.Text;//obtengo mi fecha final para mi filtro
                ListDocmuentos = controladorSDK.get_Documentos(fechainicial,fechafinal);//obtengo todas las listas de mis documentos conforme el filtro que se dio
                System.Data.DataView view = AddRows().Tables[0].DefaultView;//agrego en mi data view los datos k se mostraran en el datagrid
                dataGrid1.ItemsSource = ((System.ComponentModel.IListSource)view.ToTable()).GetList();//meto los datos del dataview a mi datagrid
                //manda a imprimir al pdf
               // System.Windows.MessageBox.Show("PDF");
                //manda a llmar 2 eventos para realizar filtros por RFC (publico y ol) y despues los manda para que se impriman en un PDF
                //se envia la ruta de la empresa ya que en la ruta de la empresa se guardara el pdf y se manda el rango de fechas que es por mes para imprimirlo en el pdf
                controlaimpresion.ImpresionCRUFacturas(ListDocmuentos, "01/" + mes + "/" + textBoxanio.Text + "--" + "31/" + mes + "/" + textBoxanio.Text, RuteEmpresa.Text, controladorSDK.FiltroRFCCRU(ListDocmuentos, "XAXX010101000       "), controladorSDK.FiltroRFCCRU(ListDocmuentos, "OLU120912UM0        "));
            }
            else System.Windows.MessageBox.Show("Necesita Seleccionar una Empresa");//mando mensaje cuando no existe una empresa seleccionada
        }



        private void button3_Click(object sender, RoutedEventArgs e)
        {
            if (controladorSDK.GetConexion())//antes de hacer algo verifico si existe alguna conexion con alguna empresa
            {
                ListDocmuentos = new List<Tipos_Datos_CRU.FacturasCRU>();//inicializo mi lista donde tendramis documentos
                int mes = Convert.ToInt32(comboBoxMes.SelectedIndex) + 1;//obtengo el mes del cual se realizara el filtro
                string fechainicial = mes + "/01" + "/" + textBoxanio.Text;//obtengo mi fechaincial para mi filtro
                string fechafinal = mes + "/31" + "/" + textBoxanio.Text;//obtengo mi fecha final para mi filtro
                ListDocmuentos = controladorSDK.get_AbonosCRU(fechainicial, fechafinal);//obtengo todas las listas de mis documentos conforme el filtro que se dio
                System.Data.DataView view = AddRows().Tables[0].DefaultView;//agrego en mi data view los datos k se mostraran en el datagrid
                dataGrid1.ItemsSource = ((System.ComponentModel.IListSource)view.ToTable()).GetList();//meto los datos del dataview a mi datagrid
                //SOLO PARA PRUEBAS
                controladorSDK.FiltroAgente(ListDocmuentos);
                //******************************///
                controlaimpresion.ImpresionCRUAbonos(ListDocmuentos, "01/" + mes + "/" + textBoxanio.Text + "--" + "31/" + mes + "/" + textBoxanio.Text, RuteEmpresa.Text, controladorSDK.FiltroRFCCRU(ListDocmuentos, "XAXX010101000       "), controladorSDK.FiltroRFCCRU(ListDocmuentos, "OLU120912UM0        "));
            }
            else System.Windows.MessageBox.Show("Necesita Seleccionar una Empresa");//mando mensaje cuando no existe una empresa seleccionada
        }











        //PARA ABAJO NO ES TAN IMPORTANTE







        #region  ingresa fila
        /// <summary>
        /// Ingresa la fila dentro de un data set el cual tendra las filas que se mostraran dentro del datagrid
        /// </summary>
        /// <returns>regresa el dataset para mostrarlo en el datagrid</returns>
        public System.Data.DataSet AddRows()
        {
            //creo un datable y añado todas la clolumnas que tiene mi datagrid  (binding)
            System.Data.DataTable table = new System.Data.DataTable();
            table.Columns.Add("Fecha");
            table.Columns.Add("Serie");
            table.Columns.Add("Folio");
            table.Columns.Add("IDAgente");
            table.Columns.Add("RazonSocial");
            table.Columns.Add("RFC");
            table.Columns.Add("IdDocumento");
            table.Columns.Add("FechaVencimiento");
            table.Columns.Add("Subtotal");
            table.Columns.Add("Total");
            table.Columns.Add("IVA");
            table.Columns.Add("Pendiente");
            table.Columns.Add("TEXT1");
            table.Columns.Add("TEXT2");
            table.Columns.Add("TEXT3");
            table.Columns.Add("Cancelado");
            table.Columns.Add("Impreso");
            table.Columns.Add("CodAgente");
            table.Columns.Add("NomAgente");
            table.Columns.Add("Afectado");
            // crear 10 filas y añadirlas a la tabla
           // System.Windows.MessageBox.Show(ListDocmuentos.Count+"");
            for (int i = 0; i < ListDocmuentos.Count; i++)
            {
                // crear la fila
                System.Data.DataRow row = table.NewRow();
                // añadir valores
                row["Fecha"] = ListDocmuentos[i].Fecha;
                row["Serie"] = ListDocmuentos[i].Serie;
                row["Folio"] = ListDocmuentos[i].Folio;
                row["IDAgente"] = ListDocmuentos[i].CodigoAgente;
                row["RazonSocial"] = ListDocmuentos[i].RazonSocial;
                row["RFC"] = ListDocmuentos[i].RFC;
                row["IdDocumento"] = ListDocmuentos[i].IdDocumento;
                row["FechaVencimiento"] = ListDocmuentos[i].FechaVencimiento;
                row["Subtotal"] = ListDocmuentos[i].Subtotal;
                row["Total"] = ListDocmuentos[i].Total;
                row["IVA"] = ListDocmuentos[i].IVA;
                row["Pendiente"] = ListDocmuentos[i].Pendiente;
                row["TEXT1"] = ListDocmuentos[i].TextoExtra1;
                row["TEXT2"] = ListDocmuentos[i].TextoExtra2;
                row["TEXT3"] = ListDocmuentos[i].TextoExtra3;
                row["Cancelado"] = ListDocmuentos[i].Cancelado;
                row["Impreso"] = ListDocmuentos[i].Impreso;
                row["CodAgente"] = ListDocmuentos[i].CodigoAgente;
                row["NomAgente"] = ListDocmuentos[i].NombreAgente;
                row["Afectado"] = ListDocmuentos[i].Afectado;
                // insertar la fila en la tabla
                table.Rows.Add(row);



            }

            // crear un DataSet y añadir la tabla
            System.Data.DataSet dataSet = new System.Data.DataSet();
            dataSet.Tables.Add(table);
            return dataSet;
        }
        #endregion


        /// <summary>
        /// Limpia mi datagrid (solo por tiempo)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button2_Click(object sender, RoutedEventArgs e)
        {
            dataGrid1.ItemsSource = null;
        }

        private void button4_Click(object sender, RoutedEventArgs e)
        {
            if (controladorSDK.GetConexion())//antes de hacer algo verifico si existe alguna conexion con alguna empresa
            {
                ListDocmuentos = new List<Tipos_Datos_CRU.FacturasCRU>();//inicializo mi lista donde tendramis documentos
                int mes = Convert.ToInt32(comboBoxMes.SelectedIndex) + 1;//obtengo el mes del cual se realizara el filtro
                string fechainicial = mes + "/01" + "/" + textBoxanio.Text;//obtengo mi fechaincial para mi filtro
                string fechafinal = mes + "/31" + "/" + textBoxanio.Text;//obtengo mi fecha final para mi filtro
                ListDocmuentos = controladorSDK.get_ComprasCRU(fechainicial, fechafinal);//obtengo todas las listas de mis documentos conforme el filtro que se dio
                System.Data.DataView view = AddRows().Tables[0].DefaultView;//agrego en mi data view los datos k se mostraran en el datagrid
                dataGrid1.ItemsSource = ((System.ComponentModel.IListSource)view.ToTable()).GetList();//meto los datos del dataview a mi datagrid
                //solo se debe de cambiar el rfc por el de anji isel
                controlaimpresion.ImpresionCRUCompras(ListDocmuentos, "01/" + mes + "/" + textBoxanio.Text + "--" + "31/" + mes + "/" + textBoxanio.Text, RuteEmpresa.Text, controladorSDK.FiltroRFCCRU(ListDocmuentos, "XAXX010101000       "));
            }
            else System.Windows.MessageBox.Show("Necesita Seleccionar una Empresa");//mando mensaje cuando no existe una empresa seleccionada
        }

        private void button5_Click(object sender, RoutedEventArgs e)
        {
            if (controladorSDK.GetConexion())//antes de hacer algo verifico si existe alguna conexion con alguna empresa
            {
                ListDocmuentos = new List<Tipos_Datos_CRU.FacturasCRU>();//inicializo mi lista donde tendramis documentos
                int mes = Convert.ToInt32(comboBoxMes.SelectedIndex) + 1;//obtengo el mes del cual se realizara el filtro
                string fechainicial = mes + "/01" + "/" + textBoxanio.Text;//obtengo mi fechaincial para mi filtro
                string fechafinal = mes + "/31" + "/" + textBoxanio.Text;//obtengo mi fecha final para mi filtro
                ListDocmuentos = controladorSDK.get_PagosProveedorCRU(fechainicial, fechafinal);//obtengo todas las listas de mis documentos conforme el filtro que se dio
                System.Data.DataView view = AddRows().Tables[0].DefaultView;//agrego en mi data view los datos k se mostraran en el datagrid
                dataGrid1.ItemsSource = ((System.ComponentModel.IListSource)view.ToTable()).GetList();//meto los datos del dataview a mi datagrid
                //solo se debe de cambiar el rfc por el de anji isel
                controlaimpresion.ImpresionCRUPagosProveedor(ListDocmuentos, "01/" + mes + "/" + textBoxanio.Text + "--" + "31/" + mes + "/" + textBoxanio.Text, RuteEmpresa.Text, controladorSDK.FiltroRFCCRU(ListDocmuentos, "XAXX010101000       "));
            }
            else System.Windows.MessageBox.Show("Necesita Seleccionar una Empresa");//mando mensaje cuando no existe una empresa seleccionada
        }

        

    }
}
