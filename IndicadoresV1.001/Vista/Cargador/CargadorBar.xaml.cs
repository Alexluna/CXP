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

namespace IndicadoresV1._001.Vista.Cargador
{
    /// <summary>
    /// Lógica de interacción para CargadorBar.xaml
    /// </summary>
    public partial class CargadorBar : Window
    {
        public int second;
        public int minute;
        public int hour;
        public CargadorBar()
        {
            InitializeComponent();

            second = minute = hour = 0;//para crear un contador
            second = 1;

            System.Windows.Threading.DispatcherTimer dispatcherTimer = new System.Windows.Threading.DispatcherTimer();
            dispatcherTimer.Tick += new EventHandler(dispatcherTimer_Tick);
            dispatcherTimer.Interval = new TimeSpan(0, 0, 1);
            dispatcherTimer.Start();
        }

        private void dispatcherTimer_Tick(object sender, EventArgs e)
        {
            string h = "";
            string m = "";
            string s = "";

            if (hour.ToString().Length == 1)
            { h = "0" + hour; }
            else { h = hour.ToString(); }

            if (minute.ToString().Length == 1)
            { m = "0" + minute; }
            else { m = minute.ToString(); }

            if (second.ToString().Length == 1)
            { s = "0" + second; }
            else { s = second.ToString(); }

            label15.Content = h + ":" + m + ":" + s;
            second++;
            if (second > 59)
            {
                second = 0;
                minute++;
            }

            if (minute > 59)
            {
                minute++;
                hour++;
            }
        }


    }
}
