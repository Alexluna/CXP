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

                    ListDocmuentos = new List<Tipos_Datos_CRU.Movimientos_Cuentas>();//inicializo mi lista donde tendra mis documentos
                    ListDocmuentos = controladorSDK.get_Movimientos_Compras(fechainicial.SelectedDate.Value.ToString("MM/dd/yyyy"), fechafinal.SelectedDate.Value.ToString("MM/dd/yyyy"));
                    //dataGrid1.ItemsSource = ListDocmuentos; //((System.ComponentModel.IListSource)view.ToTable()).GetList();//meto los datos del dataview a mi datagrid
                    string fecha = fechainicial.SelectedDate.Value.ToString("dd/MM/yyyy") + "-" + fechafinal.SelectedDate.Value.ToString("dd/MM/yyyy");
                    controlaimpresion.impresion_movimientos_productos(ListDocmuentos, fecha, fecha, RuteEmpresa.Text);
                    MessageBox.Show("Termino");
                }
                else System.Windows.MessageBox.Show("Necesita Seleccionar una Empresa", "Alerta", MessageBoxButton.OK, MessageBoxImage.Warning);//ma
            }
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
