using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms.DataVisualization.Charting;
using System.Drawing;
using System.ComponentModel;
using System.Globalization;

namespace IndicadoresV1._001.SDK_Admipaq.Modelo
{
    class Cargar_graficas
    {
        Chart pieChart;
        Chart barChart;
        List<Tipos_Datos_CRU.Movimientos_Cuentas> lista_cuentas;

        public Cargar_graficas()
        {
            lista_cuentas = new List<Tipos_Datos_CRU.Movimientos_Cuentas>();
            pieChart = new Chart();
            barChart = new Chart();
        }



        public void InitializeChart()
        {
            ChartArea chartArea1 = new ChartArea();
            Legend legend1 = new Legend() { BackColor = Color.White, ForeColor = Color.Black, Title = "" };
            Legend legend2 = new Legend() { BackColor = Color.White, ForeColor = Color.Black, Title = "" };
            pieChart = new Chart();
            barChart = new Chart();

            ((ISupportInitialize)(pieChart)).BeginInit();
            ((ISupportInitialize)(barChart)).BeginInit();


            //===Pie chart
            chartArea1.Name = "PieChartArea";
            pieChart.ChartAreas.Add(chartArea1);
            pieChart.Dock = System.Windows.Forms.DockStyle.Fill;
            legend1.Name = "Legend1";
            pieChart.Legends.Add(legend1);
            pieChart.Location = new System.Drawing.Point(0, 50);

            //====Bar Chart
            chartArea1 = new ChartArea();
            chartArea1.Name = "BarChartArea";
            barChart.ChartAreas.Add(chartArea1);
            barChart.Dock = System.Windows.Forms.DockStyle.Fill;
            legend2.Name = "Legend3";
            barChart.Legends.Add(legend2);
            ((ISupportInitialize)(this.pieChart)).EndInit();
            ((ISupportInitialize)(this.barChart)).EndInit();
        }


        public void LoadPieChart(List<Tipos_Datos_CRU.Movimientos_Cuentas> lista_cuentas_)
        {
            lista_cuentas = lista_cuentas_;
            pieChart.Series.Clear();
            pieChart.Palette = ChartColorPalette.Pastel;
            pieChart.BackColor = Color.White;
            pieChart.ChartAreas[0].BackColor = Color.Transparent;


            Series series1 = new Series()
            {
                Name = "series1",
                IsVisibleInLegend = true,
                Color = System.Drawing.Color.Green,
                ChartType = SeriesChartType.Pie
            };

            for (int i = 0; i < lista_cuentas.Count; i++)
            {
                string[] words = lista_cuentas[i].fecha.Split(' ');//separa la fecha de la hora
                string[] words2 = words[0].Split('/');//separa la fecha en [dia]/[mes]/[año]
                series1.Points.Add(lista_cuentas[i].Total);

                var p1 = series1.Points[i];
                p1.AxisLabel = lista_cuentas[i].Total.ToString();
                p1.LegendText = words2[1];


            }
            pieChart.Series.Add(series1);
            pieChart.Invalidate();
            this.pieChart.SaveImage(@"C:\chart.png", ChartImageFormat.Png);
        }


        /// <summary>
        /// carga una grafica de barras
        /// </summary>
        /// <param name="lista_cuentas_"></param>
        public void LoadBarChart(List<Tipos_Datos_CRU.Movimientos_Cuentas> lista_cuentas_)
        {
            string[] mes = new string[12];
            mes[0] = "Enero"; mes[1] = "Febrero"; mes[2] = "Marzo"; mes[3] = "Abril"; mes[4] = "Mayo"; mes[5] = "Junio"; mes[6] = "Julio"; mes[7] = "Agosto"; mes[8] = "Septiembre"; mes[9] = "Octubre"; mes[10] = "Noviembre"; mes[11] = "Diciembre";

            barChart.Series.Clear();
            barChart.BackColor = Color.LightYellow;
            barChart.Palette = ChartColorPalette.Pastel;
            barChart.ChartAreas[0].BackColor = Color.Transparent;
            barChart.ChartAreas[0].AxisX.MajorGrid.Enabled = true;
            barChart.ChartAreas[0].AxisY.MajorGrid.Enabled = true;
            barChart.Width = 800;
            barChart.Height = 600;
            Series series = new Series
            {
                Name = "serie",
                IsVisibleInLegend = false,
                ChartType = SeriesChartType.Column
            };
            barChart.Series.Add(series);
            int MesSelec = 0;
            for (int j = 1; j <= 12; j++)
            {
                MesSelec = 0;
                for (int i = 0; i < lista_cuentas.Count; i++)
                {
                    string[] words = lista_cuentas[i].fecha.Split(' ');//separa la fecha de la hora
                    string[] words2 = words[0].Split('/');//separa la fecha en [dia]/[mes]/[año]
                    if (j == Convert.ToInt32(words2[1]))
                    {

                        series.Points.Add(lista_cuentas[i].Total);
                        var p1 = series.Points[j - 1];
                        //p1.Color = Color.Red;
                        p1.AxisLabel = mes[j - 1];
                        p1.LegendText = mes[j - 1];
                        p1.Label = lista_cuentas[i].Total + "";
                        p1.LabelAngle = 90;
                        MesSelec = 1;
                        barChart.Invalidate();
                        break;
                    }
                }//fin for interno
                if (MesSelec == 0)//no se gráfico
                {
                    series.Points.Add(0);
                    var p1 = series.Points[j - 1];
                    //p1.Color = Color.Red;
                    p1.AxisLabel = mes[j - 1];
                    p1.LegendText = mes[j - 1];
                    p1.Label = "0";
                    //barChart.Invalidate();

                }
            }
            barChart.Invalidate();
            //MessageBox.Show("" + lista_cuentas.nombreimagen);
            this.barChart.SaveImage(@"C:\chart.png" + ".png", ChartImageFormat.Png);



        }

        #region Clasificacion compras por dia por mes
        public void LoadBarChart_compras_dia(List<Tipos_Datos_CRU.Movimientos_Cuentas> lista_cuentas_, string fecha, string path)
        {
            //compras por dia por semana 
            lista_cuentas = lista_cuentas_;
            barChart.Series.Clear();
            barChart.BackColor = Color.White;
            barChart.Palette = ChartColorPalette.Pastel;
            barChart.ChartAreas[0].BackColor = Color.Transparent;
            barChart.ChartAreas[0].AxisX.MajorGrid.Enabled = true;
            barChart.ChartAreas[0].AxisY.MajorGrid.Enabled = true;
            barChart.ChartAreas[0].AxisY.MajorGrid.LineColor = Color.FromArgb(12, 12, 12);
            barChart.ChartAreas[0].AxisX.MajorGrid.LineColor = Color.FromArgb(12, 12, 12);
            barChart.Width = 800;
            barChart.Height = 600;

            int k = 0;
            Series series = new Series
            {
                Name = "serie",
                IsVisibleInLegend = false,
                ChartType = SeriesChartType.Column
            };

            for (int i = 0; i < lista_cuentas.Count; i++)
            {

                string[] words = lista_cuentas[i].fecha.Split(' ');//separa la fecha de la hora
                string[] words2 = words[0].Split('/');//separa la fecha en [dia]/[mes]/[año]

                if (int.Parse(words2[1]) == int.Parse(fecha))
                {

                    series.Points.Add(lista_cuentas[i].Total);
                    var p1 = series.Points[k]; ;
                    p1.AxisLabel = lista_cuentas[i].fecha;
                    p1.LegendText = lista_cuentas[i].fecha;
                    p1.Label = lista_cuentas[i].Total.ToString() + "";
                    p1.LabelAngle = 90;

                    k++;
                }

            }
            barChart.Series.Add(series);
            barChart.Invalidate();
            this.barChart.SaveImage(@path+"chart.png", ChartImageFormat.Png);

        }
        #endregion

        #region compras por semana
        /// <summary>
        /// 
        /// </summary>
        /// <param name="lista_cuentas_">lista de la cuentas </param>
        /// <param name="fechas">fecha inicial y la final desde los datetime</param>
        public void LoadBarChart_ComprasPorSemana(List<Tipos_Datos_CRU.Movimientos_Cuentas> lista_cuentas_, string fechas, string path)
        {

            //compras por dia por semana 
            lista_cuentas = lista_cuentas_;
            barChart.Series.Clear();
            barChart.BackColor = Color.White;
            barChart.Palette = ChartColorPalette.Pastel;
            barChart.ChartAreas[0].BackColor = Color.Transparent;
            barChart.ChartAreas[0].AxisX.MajorGrid.Enabled = true;
            barChart.ChartAreas[0].AxisY.MajorGrid.Enabled = true;
            barChart.ChartAreas[0].AxisY.MajorGrid.LineColor = Color.FromArgb(12, 12, 12);
            barChart.ChartAreas[0].AxisX.MajorGrid.LineColor = Color.FromArgb(12, 12, 12);
            barChart.Width = 800;
            barChart.Height = 600;


            //lista_cuentas.Sort(delegate(Tipos_Dato.CuentasXPagar x, Tipos_Dato.CuentasXPagar y)
            //{
            //    if (x.Fecha == null && y.Fecha == null) return 0;
            //    else if (x.Fecha == null) return -1;
            //    else if (y.Fecha == null) return 1;
            //    else return x.Fecha.CompareTo(y.Fecha);
            //});
            DateTime fecha_inicial;//fecha inicial
            DateTime fecha_final;// fecha final
            //String[] result = fecha.Split('-');
            //string f_i = result[0].Replace("_", "/");
            //string f_f = result[1].Replace("_", "/");
            string[] fecha = fechas.Split('-');
            fecha_inicial = Convert.ToDateTime(fecha[0], new CultureInfo("es-ES"));
            fecha_final = Convert.ToDateTime(fecha[1], new CultureInfo("es-ES"));

            /****calcula la semana****/

            System.Globalization.CultureInfo norwCulture = System.Globalization.CultureInfo.CreateSpecificCulture("es");
            System.Globalization.Calendar cal = norwCulture.Calendar;
            int weekNoFinal = cal.GetWeekOfYear(fecha_final, norwCulture.DateTimeFormat.CalendarWeekRule, norwCulture.DateTimeFormat.FirstDayOfWeek);
            int weekNoInicial = cal.GetWeekOfYear(fecha_inicial, norwCulture.DateTimeFormat.CalendarWeekRule, norwCulture.DateTimeFormat.FirstDayOfWeek);
            float total_semana = 0;

            Series series = new Series
            {
                Name = "serie",
                IsVisibleInLegend = false,
                ChartType = SeriesChartType.Column
            };
            int k = 0;
            for (int j = weekNoInicial - 1; j <= weekNoFinal; j++)
            {

                for (int i = 0; i < lista_cuentas.Count; i++)
                {
                    // MessageBox.Show(lista_cuentas[i].semana+" "+j);
                    if (lista_cuentas[i].semana == j)// TRABAJO EN LA SEMANA QUE LE CORRESPONDE A ESA FECHA 
                    {
                        total_semana = total_semana + lista_cuentas[i].Total;
                    }
                }
                series.Points.Add(total_semana);
                var p1 = series.Points[k]; ;
                p1.AxisLabel = j.ToString();
                p1.LegendText = j.ToString();
                p1.Label = total_semana.ToString() + "";
                p1.LabelAngle = 90;
                total_semana = 0;
                k++;

            }
            barChart.Series.Add(series);
            barChart.Invalidate();
            this.barChart.SaveImage(@path+"chart.png", ChartImageFormat.Png);

        }
        #endregion

        #region compras por mes
        public void LoadBarChart_ComprasPorMes(List<Tipos_Datos_CRU.Movimientos_Cuentas> lista_cuentas_, string path)
        {

            //compras por dia por semana 
            lista_cuentas = lista_cuentas_;
            barChart.Series.Clear();
            barChart.BackColor = Color.White;
            barChart.Palette = ChartColorPalette.Pastel;
            barChart.ChartAreas[0].BackColor = Color.Transparent;
            barChart.ChartAreas[0].AxisX.MajorGrid.Enabled = true;
            barChart.ChartAreas[0].AxisY.MajorGrid.Enabled = true;
            barChart.ChartAreas[0].AxisY.MajorGrid.LineColor = Color.FromArgb(12, 12, 12);
            barChart.ChartAreas[0].AxisX.MajorGrid.LineColor = Color.FromArgb(12, 12, 12);
            barChart.Width = 800;
            barChart.Height = 600;

            lista_cuentas.Sort(delegate(Tipos_Datos_CRU.Movimientos_Cuentas x, Tipos_Datos_CRU.Movimientos_Cuentas y)
            {
                if (x.fecha == null && y.fecha == null) return 0;
                else if (x.fecha == null) return -1;
                else if (y.fecha == null) return 1;
                else return x.fecha.CompareTo(y.fecha);
            });

            string[] mes = new string[12];
            mes[0] = "Enero"; mes[1] = "Febrero"; mes[2] = "Marzo"; mes[3] = "Abril"; mes[4] = "Mayo"; mes[5] = "Junio"; mes[6] = "Julio"; mes[7] = "Agosto"; mes[8] = "Septiembre"; mes[9] = "Octubre"; mes[10] = "Noviembre"; mes[11] = "Diciembre";
            float total_mes = 0;

            Series series = new Series
            {
                Name = "serie",
                IsVisibleInLegend = false,
                ChartType = SeriesChartType.Column
            };


            for (int j = 0; j < 12; j++)
            {


                for (int i = 0; i < lista_cuentas.Count; i++)
                {
                    string[] words = lista_cuentas[i].fecha.Split(' ');//separa la fecha de la hora
                    string[] words2 = words[0].Split('/');//separa la fecha en [dia]/[mes]/[año]

                    if (int.Parse(words2[1]) == j + 1)
                    {
                        total_mes = total_mes + lista_cuentas[i].Total;
                    }

                }

                series.Points.Add(total_mes);
                var p1 = series.Points[j];//.Points[j];
                p1.AxisLabel = mes[j];
                p1.LegendText = mes[j];
                p1.Label = total_mes.ToString() + "";
                p1.LabelAngle = 90;
                total_mes = 0;

            }
            barChart.Series.Add(series);
            barChart.Invalidate();

            this.barChart.SaveImage(@path+"chart.png", ChartImageFormat.Png);
        }
        #endregion

        #region clasificacion 1 proveedores
        /// <summary>
        /// grafica las compras por dia 
        /// </summary>
        /// <param name="lista_cuentas_"></param>
        public void LoadBarChart_compras_Clasificacion1(Tipos_Datos_CRU.ComprasMensualesXClasificacionIMagenes lista_cuentas, string nombre, string path)
        {
            string[] mes = new string[12];
            mes[0] = "Enero"; mes[1] = "Febrero"; mes[2] = "Marzo"; mes[3] = "Abril"; mes[4] = "Mayo"; mes[5] = "Junio"; mes[6] = "Julio"; mes[7] = "Agosto"; mes[8] = "Septiembre"; mes[9] = "Octubre"; mes[10] = "Noviembre"; mes[11] = "Diciembre";

            barChart.Series.Clear();
            barChart.BackColor = Color.FromArgb(250, 251, 252);
            barChart.Palette = ChartColorPalette.Pastel;
            barChart.ChartAreas[0].BackColor = Color.FromArgb(250, 251, 252);

            barChart.ChartAreas[0].AxisX.MajorGrid.Enabled = true;
            barChart.ChartAreas[0].AxisY.MajorGrid.Enabled = true;
            barChart.ChartAreas[0].AxisY.MajorGrid.LineColor = Color.FromArgb(12, 12, 12);
            barChart.ChartAreas[0].AxisX.MajorGrid.LineColor = Color.FromArgb(12, 12, 12);

            barChart.Width = 800;
            barChart.Height = 600;
            Series series = new Series
            {
                Name = "serie",
                IsVisibleInLegend = false,
                ChartType = SeriesChartType.Column
            };
            barChart.Series.Add(series);
            int MesSelec = 0;
            for (int j = 1; j <= 12; j++)
            {
                MesSelec = 0;
                for (int i = 0; i < lista_cuentas.compras.Count; i++)
                {
                    if (j == Convert.ToInt32(lista_cuentas.compras[i].Mes))
                    {

                        series.Points.Add(lista_cuentas.compras[i].total);
                        var p1 = series.Points[j - 1];
                        //p1.Color = Color.Red;
                        p1.AxisLabel = mes[j - 1];
                        p1.LegendText = mes[j - 1];
                        p1.Label = lista_cuentas.compras[i].total + "";
                        p1.LabelAngle = 90;
                        MesSelec = 1;
                        barChart.Invalidate();
                        break;
                        //********************************
                        //Series series1 = this.barChart.Series.Add(lista_cuentas.compras[i].total.ToString() + "(" + lista_cuentas.compras[i].Clasificacion1 + ")");
                        //series1.Points.Add(lista_cuentas.compras[i].total).AxisLabel = lista_cuentas.compras[i].Mes;
                        //  MessageBox.Show("" + lista_cuentas.compras[i].Mes);
                    }
                }

                if (MesSelec == 0)//no se gráfico
                {
                    series.Points.Add(0);
                    var p1 = series.Points[j - 1];
                    //p1.Color = Color.Red;
                    p1.AxisLabel = mes[j - 1];
                    p1.LegendText = mes[j - 1];
                    p1.Label = "0";
                    //barChart.Invalidate();

                }
            }



            barChart.Invalidate();
            //MessageBox.Show("" + lista_cuentas.nombreimagen);
            this.barChart.SaveImage(@path+lista_cuentas.nombreimagen + ".png", ChartImageFormat.Png);

        }

        #endregion

        #region clasificacion2 proveedores
        /// <summary>
        /// grafica las compras por dia 
        /// </summary>
        /// <param name="lista_cuentas_"></param>
        public void LoadBarChart_compras_Clasificacion2(Tipos_Datos_CRU.ComprasMensualesXClasificacionIMagenes2 lista_cuentas, string nombre, string path)
        {

            string[] mes = new string[12];
            mes[0] = "Enero"; mes[1] = "Febrero"; mes[2] = "Marzo"; mes[3] = "Abril"; mes[4] = "Mayo"; mes[5] = "Junio"; mes[6] = "Julio"; mes[7] = "Agosto"; mes[8] = "Septiembre"; mes[9] = "Octubre"; mes[10] = "Noviembre"; mes[11] = "Diciembre";

            barChart.Series.Clear();
            barChart.BackColor = Color.FromArgb(250, 251, 252);
            barChart.Palette = ChartColorPalette.Pastel;
            barChart.ChartAreas[0].BackColor = Color.FromArgb(250, 251, 252);

            barChart.ChartAreas[0].AxisX.MajorGrid.Enabled = true;
            barChart.ChartAreas[0].AxisY.MajorGrid.Enabled = true;
            barChart.ChartAreas[0].AxisY.MajorGrid.LineColor = Color.FromArgb(12, 12, 12);
            barChart.ChartAreas[0].AxisX.MajorGrid.LineColor = Color.FromArgb(12, 12, 12);

            barChart.Width = 800;
            barChart.Height = 600;
            Series series = new Series
            {
                Name = "serie",
                IsVisibleInLegend = false,
                ChartType = SeriesChartType.Column
            };
            barChart.Series.Add(series);
            int MesSelec = 0;
            for (int j = 1; j <= 12; j++)
            {
                MesSelec = 0;
                for (int i = 0; i < lista_cuentas.compras.Count; i++)
                {
                    if (j == Convert.ToInt32(lista_cuentas.compras[i].Mes))
                    {

                        series.Points.Add(lista_cuentas.compras[i].total);
                        var p1 = series.Points[j - 1];
                        //p1.Color = Color.Red;
                        p1.AxisLabel = mes[j - 1];
                        p1.LegendText = mes[j - 1];
                        p1.Label = lista_cuentas.compras[i].total + "";
                        p1.LabelAngle = 90;
                        MesSelec = 1;
                        barChart.Invalidate();
                        break;
                        //********************************
                        //Series series1 = this.barChart.Series.Add(lista_cuentas.compras[i].total.ToString() + "(" + lista_cuentas.compras[i].Clasificacion1 + ")");
                        //series1.Points.Add(lista_cuentas.compras[i].total).AxisLabel = lista_cuentas.compras[i].Mes;
                        //  MessageBox.Show("" + lista_cuentas.compras[i].Mes);
                    }
                }

                if (MesSelec == 0)//no se gráfico
                {
                    series.Points.Add(0);
                    var p1 = series.Points[j - 1];
                    //p1.Color = Color.Red;
                    p1.AxisLabel = mes[j - 1];
                    p1.LegendText = mes[j - 1];
                    p1.Label = "0";
                    //barChart.Invalidate();

                }
            }
            barChart.Invalidate();
            //MessageBox.Show("" + lista_cuentas.nombreimagen);
            this.barChart.SaveImage(@path + lista_cuentas.nombreimagen + ".png", ChartImageFormat.Png);
        }
        #endregion



        #region Clasificacion 1 Productos
        public void LoadBarChart_compras_Clasificacion1Productos(Tipos_Datos_CRU.ComprasMensualesXClasificacion1Productos lista_cuentas, string nombre, string path)
        {
            string[] mes = new string[12];
            mes[0] = "Enero"; mes[1] = "Febrero"; mes[2] = "Marzo"; mes[3] = "Abril"; mes[4] = "Mayo"; mes[5] = "Junio"; mes[6] = "Julio"; mes[7] = "Agosto"; mes[8] = "Septiembre"; mes[9] = "Octubre"; mes[10] = "Noviembre"; mes[11] = "Diciembre";

            barChart.Series.Clear();
            barChart.BackColor = Color.FromArgb(250, 251, 252);
            barChart.Palette = ChartColorPalette.Pastel;
            barChart.ChartAreas[0].BackColor = Color.FromArgb(250, 251, 252);

            barChart.ChartAreas[0].AxisX.MajorGrid.Enabled = true;
            barChart.ChartAreas[0].AxisY.MajorGrid.Enabled = true;
            barChart.ChartAreas[0].AxisY.MajorGrid.LineColor = Color.FromArgb(12, 12, 12);
            barChart.ChartAreas[0].AxisX.MajorGrid.LineColor = Color.FromArgb(12, 12, 12);

            barChart.Width = 800;
            barChart.Height = 600;
            Series series = new Series
            {
                Name = "serie",
                IsVisibleInLegend = false,
                ChartType = SeriesChartType.Column
            };
            barChart.Series.Add(series);



            int MesSelec = 0;
            for (int j = 1; j <= 12; j++)
            {
                MesSelec = 0;
                for (int i = 0; i < lista_cuentas.compras.Count; i++)
                {

                    if (j == Convert.ToInt32(lista_cuentas.compras[i].Mes))
                    {

                        series.Points.Add(lista_cuentas.compras[i].total);
                        var p1 = series.Points[j - 1];
                        //p1.Color = Color.Red;
                        p1.AxisLabel = mes[j - 1];
                        p1.LegendText = mes[j - 1];
                        p1.Label = lista_cuentas.compras[i].total + "";
                        p1.LabelAngle = 90;
                        MesSelec = 1;
                        barChart.Invalidate();
                        break;
                    }
                }//fin for interno
                if (MesSelec == 0)//no se gráfico
                {
                    series.Points.Add(0);
                    var p1 = series.Points[j - 1];
                    //p1.Color = Color.Red;
                    p1.AxisLabel = mes[j - 1];
                    p1.LegendText = mes[j - 1];
                    p1.Label = "0";
                    //barChart.Invalidate();

                }
            }
            barChart.Invalidate();
            //MessageBox.Show("" + lista_cuentas.nombreimagen);
            this.barChart.SaveImage(@path + lista_cuentas.nombreimagen + ".png", ChartImageFormat.Png);

        }
        #endregion



        #region clasificacion 2 PRoductos
        public void LoadBarChart_compras_Clasificacion2Productos(Tipos_Datos_CRU.ComprasMensualesXClasificacion2Productos lista_cuentas, string nombre, string path)
        {
            string[] mes = new string[12];
            mes[0] = "Enero"; mes[1] = "Febrero"; mes[2] = "Marzo"; mes[3] = "Abril"; mes[4] = "Mayo"; mes[5] = "Junio"; mes[6] = "Julio"; mes[7] = "Agosto"; mes[8] = "Septiembre"; mes[9] = "Octubre"; mes[10] = "Noviembre"; mes[11] = "Diciembre";

            barChart.Series.Clear();
            barChart.BackColor = Color.FromArgb(250, 251, 252);
            barChart.Palette = ChartColorPalette.Pastel;
            barChart.ChartAreas[0].BackColor = Color.FromArgb(250, 251, 252);

            barChart.ChartAreas[0].AxisX.MajorGrid.Enabled = true;
            barChart.ChartAreas[0].AxisY.MajorGrid.Enabled = true;
            barChart.ChartAreas[0].AxisY.MajorGrid.LineColor = Color.FromArgb(12, 12, 12);
            barChart.ChartAreas[0].AxisX.MajorGrid.LineColor = Color.FromArgb(12, 12, 12);

            barChart.Width = 800;
            barChart.Height = 600;
            Series series = new Series
            {
                Name = "serie",
                IsVisibleInLegend = false,
                ChartType = SeriesChartType.Column
            };
            barChart.Series.Add(series);



            int MesSelec = 0;
            for (int j = 1; j <= 12; j++)
            {
                MesSelec = 0;
                for (int i = 0; i < lista_cuentas.compras.Count; i++)
                {

                    if (j == Convert.ToInt32(lista_cuentas.compras[i].Mes))
                    {

                        series.Points.Add(lista_cuentas.compras[i].total);
                        var p1 = series.Points[j - 1];
                        //p1.Color = Color.Red;
                        p1.AxisLabel = mes[j - 1];
                        p1.LegendText = mes[j - 1];
                        p1.Label = lista_cuentas.compras[i].total + "";
                        p1.LabelAngle = 90;
                        MesSelec = 1;
                        barChart.Invalidate();
                        break;
                    }
                }//fin for interno
                if (MesSelec == 0)//no se gráfico
                {
                    series.Points.Add(0);
                    var p1 = series.Points[j - 1];
                    //p1.Color = Color.Red;
                    p1.AxisLabel = mes[j - 1];
                    p1.LegendText = mes[j - 1];
                    p1.Label = "0";
                    //barChart.Invalidate();

                }
            }
            barChart.Invalidate();
            //MessageBox.Show("" + lista_cuentas.nombreimagen);
            this.barChart.SaveImage(@path + lista_cuentas.nombreimagen + ".png", ChartImageFormat.Png);

        }
        #endregion


        #region Clasificacion 1 producto por mes
        public void LoadPieChartclasificacion1ProductoMes(List<Tipos_Datos_CRU.ComprasMensualesXClasificacion1> lista_cuentas_, string nombreimagen, string path)
        {

            pieChart.Series.Clear();
            pieChart.Palette = ChartColorPalette.Pastel;
            pieChart.BackColor = Color.White;
            pieChart.ChartAreas[0].BackColor = Color.Transparent;

            Series series1 = new Series()
            {
                Name = "series1",
                IsVisibleInLegend = true,
                Color = System.Drawing.Color.Green,
                ChartType = SeriesChartType.Pie
            };

            for (int i = 0; i < lista_cuentas_.Count; i++)
            {

                series1.Points.Add(lista_cuentas_[i].total);

                var p1 = series1.Points[i];
                p1.AxisLabel = lista_cuentas_[i].total.ToString();
                p1.LegendText = lista_cuentas_[i].Clasificacion1;


            }
            pieChart.Series.Add(series1);
            pieChart.Invalidate();
            this.pieChart.SaveImage(@path + nombreimagen + ".png", ChartImageFormat.Png);
        }
        #endregion


        #region Clasificacion 2 producto por mes
        public void LoadPieChartclasificacion2ProductoMes(List<Tipos_Datos_CRU.ComprasMensualesXClasificacion2> lista_cuentas_, string nombreimagen, string path)
        {

            pieChart.Series.Clear();
            pieChart.Palette = ChartColorPalette.Pastel;
            pieChart.BackColor = Color.White;
            pieChart.ChartAreas[0].BackColor = Color.Transparent;


            Series series1 = new Series()
            {
                Name = "series1",
                IsVisibleInLegend = true,
                Color = System.Drawing.Color.Green,
                ChartType = SeriesChartType.Pie
            };

            for (int i = 0; i < lista_cuentas_.Count; i++)
            {

                series1.Points.Add(lista_cuentas_[i].total);

                var p1 = series1.Points[i];
                p1.AxisLabel = lista_cuentas_[i].total.ToString();
                p1.LegendText = lista_cuentas_[i].Clasificacion2;


            }
            pieChart.Series.Add(series1);
            pieChart.Invalidate();
            this.pieChart.SaveImage(@path + nombreimagen + ".png", ChartImageFormat.Png);
        }
        #endregion
        
    }
}
