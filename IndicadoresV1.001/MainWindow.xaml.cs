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
using IndicadoresV1._001.Vista.Principal;

namespace IndicadoresV1._001
{
    /// <summary>
    /// Lógica de interacción para MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Principal Home;//objeto para llamar al menu principal de programa
        /// <summary>
        /// Constructor de la pantalla inicial
        /// </summary>
        public MainWindow()
        {
            InitializeComponent();
            //inicializa el objeto de el menu principal
            Home = new Principal();
            //limpia el contenedor principal y despues añade el usertcontrol del menu principal
            contenedorPrincipal.Children.Clear();
            contenedorPrincipal.Children.Add(Home);
        }
    }
}
