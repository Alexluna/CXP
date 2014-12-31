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
using IndicadoresV1._001.SDK_Admipaq.Modelo;
using IndicadoresV1._001.SDK_Admipaq.Controlador;
using IndicadoresV1._001.Vista.Cargador;
using System.Threading;


namespace IndicadoresV1._001.Vista.CXP
{
    /// <summary>
    /// Lógica de interacción para Indicadores_CXP.xaml
    /// </summary>
    public partial class Indicadores_CXP : UserControl
    {
        Controlador__SDKAdmipaq controladorSDK;//para lalamr al controlador del sdk admipaq
        List<Tipos_Datos_CRU.Movimientos_Cuentas> ListDocmuentos;//para obtener la lista de todos los documentos
        Controlador_Impresion controlaimpresion;//para poder mandar a imprimir en PDF
        public Indicadores_CXP()
        {
            InitializeComponent();
            fechainicial.SelectedDate = DateTime.Now.Date;
            fechafinal.SelectedDate = DateTime.Now.Date;
            controladorSDK = new Controlador__SDKAdmipaq();
            controlaimpresion = new Controlador_Impresion();
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            if (fechafinal.Text == "" || fechainicial.Text == "")
            {
                System.Windows.MessageBox.Show("no ha seleccionado ninguna fecha", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                if (controladorSDK.GetConexion())//antes de hacer algo verifico si existe alguna conexion con alguna empresa
                {

                    OnWorkerMethodStart();


                    
                   
                }
                else System.Windows.MessageBox.Show("Necesita Seleccionar una Empresa", "Alerta", MessageBoxButton.OK, MessageBoxImage.Warning);//ma
            }
        }



        CargadorBar cargador;
        MainWindow ventaprincipal;
        private void OnWorkerMethodStart()
        {
            //creamos el objeto de nuestra clase 
            WorkerProgressBar workerfactura = new WorkerProgressBar();
            //por medio de un delegado instanciamos el metodo que se debera ejecutar en segundo plano, aqui seleccionamos el metodo logear
            //este metodo se encuentra en esta clase
            //int mes = Convert.ToInt32(comboBoxMes.SelectedIndex) + 1;//obtengo el mes del cual se realizara el filtro
            string fechainicial1 = fechainicial.SelectedDate.Value.ToString("MM/dd/yyyy");//obtengo mi fechaincial para mi filtro
            string fechafinal1 = fechafinal.SelectedDate.Value.Date.ToString("MM/dd/yyyy");//obtengo mi fecha final para mi filtro


            workerfactura.get_data_CXP += new WorkerProgressBar.DelegateCXP(get_data_);
            workerfactura.fechafinal = fechafinal1; // le asignamos el correo a la clase creada (ingresado por el usuairo)
            workerfactura.fechainicial = fechainicial1; // le asignamos el password a la clase creada (ingresado por el usuario)
            workerfactura.ruta_empresa = RuteEmpresa.Text;

            //creamos el hilo para ejecutar el proceso en segundo plano, en el pasamos como argumento el metodo que queremos ejecutar
            //el metodo que se ejecutara es el metodo que se encuentra en la clase creado
            ThreadStart tStart = new ThreadStart(workerfactura.CRU_mtehod);
            Thread t = new Thread(tStart); //iniciamos el hilo

            t.Start(); // inicializa el hilo

            cargador = new CargadorBar(); //Creamos el objeto de la clase CargadorBar (este clase contiene el cargador)
            cargador.Owner = ventaprincipal; //asignamos que este objeto es modela relacionando  cual es su propietario
            cargador.ShowDialog(); //mostramo el cargador (este metodo se ejecutara )

            //finalmente obtenemos el resultado del metodo logear para seleccionar la respuesta que tendra 


        }

        private void get_data_(string fechainicial, string fechafinal, string ruta_empresa)
        {

            ListDocmuentos = new List<Tipos_Datos_CRU.Movimientos_Cuentas>();//inicializo mi lista donde tendra mis documentos
            ListDocmuentos = controladorSDK.get_Movimientos_Compras(fechainicial, fechafinal);
            //dataGrid1.ItemsSource = ListDocmuentos; //((System.ComponentModel.IListSource)view.ToTable()).GetList();//meto los datos del dataview a mi datagrid
            string fecha = fechainicial+ "-" + fechafinal;
            controlaimpresion.impresion_movimientos_productos(ListDocmuentos, fecha, fecha, ruta_empresa);



            cargador.Dispatcher.Invoke(System.Windows.Threading.DispatcherPriority.Normal,
            new Action(
            delegate()
            {
                cargador.Close();
            }
            ));
        }
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        private void Selecciona_Click(object sender, RoutedEventArgs e)
        {
            System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            if (folderBrowserDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                controladorSDK.ConexionAdmipaq(folderBrowserDialog1.SelectedPath);//Verifica si la carpeta seleccionada es correcta
                if (controladorSDK.GetConexion())
                {//como es correcta imprime en el label la direccion de la empresa
                    RuteEmpresa.Text = folderBrowserDialog1.SelectedPath + "\\";
                    //ahora ya tienes la empresa correctamente seleccionada
                }
            }
        }
    }
}
