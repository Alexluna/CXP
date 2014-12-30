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
using IndicadoresV1._001.Vista.Contenedor_Principal;

namespace IndicadoresV1._001.Vista.Principal
{
    /// <summary>
    /// Lógica de interacción para Principal.xaml
    /// </summary>
    public partial class Principal : UserControl
    {
        AplicacionPrincipal aplicacionprincipal;//objeto para llamar a la ventana principal
        public Principal()
        {
            InitializeComponent();
            
        }

        private void cxp_Click(object sender, RoutedEventArgs e)
        {
            aplicacionprincipal = new AplicacionPrincipal();//inicializa el objeto que se va a mostrar
            aplicacionprincipal.inicializaGrid(0);//me dice k user control se debe de mostrar
            aplicacionprincipal.ShowDialog();//muestra la nueva ventana
        }

        /// <summary>
        /// Evento para llamar al usercontrol de CRU dentro de la ventana de aplicacion principal
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cru_Click(object sender, RoutedEventArgs e)
        {
            aplicacionprincipal = new AplicacionPrincipal();//inicializa el objeto que se va a mostrar
            aplicacionprincipal.inicializaGrid(1);//me dice k user control se debe de mostrar
            aplicacionprincipal.ShowDialog();//muestra la nueva ventana
        }
       
    }
}
