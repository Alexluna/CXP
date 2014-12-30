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
using System.Windows.Shapes;
using IndicadoresV1._001.Vista.CRU;
using System.Windows.Forms;
using IndicadoresV1._001.Vista.CXP;

namespace IndicadoresV1._001.Vista.Contenedor_Principal
{
    /// <summary>
    /// Lógica de interacción para AplicacionPrincipal.xaml
    /// </summary>
    public partial class AplicacionPrincipal : Window
    {
        Indicadores_CRU CRU;
        Indicadores_CXP CXP;
        public AplicacionPrincipal()
        {
            InitializeComponent();
            
            
        }



        /// <summary>
        /// Inicializa que user control debe de estar puesto en el la ventana aplicacion principa
        /// </summary>
        /// <param name="value">valor que me dira que user control se debe de colocar</param>
        public void inicializaGrid(int value)
        {
            switch (value)
            {
                case 0:
                    CXP = new Indicadores_CXP();
                    contenedor.Children.Clear();
                    contenedor.Children.Add(CXP);
                    break;
                case 1:
                    CRU = new Indicadores_CRU();//inicializo en User control que voy a colocar 
                    contenedor.Children.Clear();//limpio el grid donde se va a colocar el usercontrol
                    contenedor.Children.Add(CRU);//agrego el user control al Grid
                    break;


            }
        }
    }
}
