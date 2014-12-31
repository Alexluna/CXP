using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.Diagnostics;
using System.Windows;
using System.IO;
using System.Globalization;

namespace IndicadoresV1._001.SDK_Admipaq.Modelo
{
    class Modelo_Impresion
    {

        List<Tipos_Datos_CRU.Movimientos_Cuentas> lista_cuentas;//cuentas por pagar
        Cargar_graficas instance_graficas;

        List<Tipos_Datos_CRU.ComprasMensualesXClasificacionIMagenes> PDFProveedoresClasificacion1;
        List<Tipos_Datos_CRU.ComprasMensualesXClasificacionIMagenes2> PDFProveedoresClasificacion2;

        List<Tipos_Datos_CRU.ComprasMensualesXClasificacion1Productos> PDFProveedoresClasificacion1Productos;
        List<Tipos_Datos_CRU.ComprasMensualesXClasificacion2Productos> PDFProveedoresClasificacion2Productos;


        List<Tipos_Datos_CRU.ComprasMensualesXClasificacion1ProductosMes> PDFClasificacion1PRoductoMes;
        List<Tipos_Datos_CRU.ComprasMensualesXClasificacion2ProductosMes> PDFClasificacion2PRoductoMes;


        #region CRU FACTURAS 
        /// <summary>
        /// Método para imprimir las facturas de CRU
        /// </summary>
        /// <param name="ListFactrurasCRU">lista de las facturas que se van a imprimir</param>
        public void ImpresionCRUFacturas(List<Tipos_Datos_CRU.FacturasCRU> ListFactrurasCRU, string fechas, string path, List<Tipos_Datos_CRU.FacturasCRU> ListFactrurasCRUFiltroRFCPublico, List<Tipos_Datos_CRU.FacturasCRU> ListFactrurasCRUFiltroRFCOL)
        {

            try
            {
                
                Document doc = new Document(PageSize.TABLOID, 10, 10, 10, 10);//Creacion del documento configuracion de tipo de hoja y margenes
                doc.AddAuthor("Indicadores");//Autor del PDF
                doc.AddKeywords("pdf, PdfWriter; Indicadores V1");
                
                //para almacenamiento del archivo
                string nombre_archivo = "FacturasCRU.PDF";//Nombre del Archivo
                string rut = @path + nombre_archivo;
                PdfWriter wri = PdfWriter.GetInstance(doc, new FileStream(rut, FileMode.Create));
                doc.AddTitle("REPORTE");
                doc.AddCreator("*********");
                doc.Open();
                //tipo de letras que se pueda usar en el archivo PDF
                iTextSharp.text.Font _mediumFont = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 8, iTextSharp.text.Font.NORMAL, BaseColor.BLACK);
                iTextSharp.text.Font _standardFont = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 9, iTextSharp.text.Font.BOLD, BaseColor.BLACK);
                iTextSharp.text.Font _standardFont1 = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 14, iTextSharp.text.Font.BOLD, BaseColor.WHITE);
                iTextSharp.text.Font _smallFont = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 8, iTextSharp.text.Font.NORMAL, BaseColor.BLACK);
                iTextSharp.text.Font _titulo = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 14, iTextSharp.text.Font.NORMAL, BaseColor.BLACK);
                iTextSharp.text.Font _titulos = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 12, iTextSharp.text.Font.NORMAL, BaseColor.BLACK);

                
                // Cabecera
                doc.Add(new Paragraph(" Desglose general de Facturas CRU "+fechas, _titulo));
                doc.Add(new Paragraph("\n"));
                doc.Add(new Paragraph("\n"));

                //***********************************************
                #region **********PRIMERA TABLA *********
                doc.Add(new Paragraph("20 Acumulado de Facturación en CRU", _titulos));
                doc.Add(new Paragraph("\n"));
                PdfPTable tabla_cuentas = new PdfPTable(19);
                //PdfPCell cell = new PdfPCell(new Phrase("Reporte de Compras"));
                //cell.Colspan = 3;
                //cell.BackgroundColor = BaseColor.BLUE;
                //cell.HorizontalAlignment = 1;//0=Left, 1=Centre, 2=Right 
                tabla_cuentas.WidthPercentage = 100;
                //tabla_cuentas.AddCell(cell);



                #region configuracion de columnas
                // Configuramos el título de las columnas de la tabla 
                PdfPCell clFecha = new PdfPCell(new Phrase("Fecha", _standardFont));
                clFecha.BorderWidth = 0.5f;
                clFecha.BorderWidthBottom = 0.5f;
                clFecha.HorizontalAlignment = 1;

                PdfPCell clSerie = new PdfPCell(new Phrase("Serie", _standardFont));
                clSerie.BorderWidth = 0.5f;
                clSerie.BorderWidthBottom = 0.5f;
                clSerie.HorizontalAlignment = 1;

                PdfPCell clFolio = new PdfPCell(new Phrase("Folio", _standardFont));
                clFolio.BorderWidth = 0.5f;
                clFolio.BorderWidthBottom = 0.5f;
                clFolio.HorizontalAlignment = 1;

                PdfPCell clNombreAgente = new PdfPCell(new Phrase("Nombre del Agente", _standardFont));
                clNombreAgente.BorderWidth = 0.5f;
                clNombreAgente.BorderWidthBottom = 0.5f;
                clNombreAgente.HorizontalAlignment = 1;

                PdfPCell clRazonSocial = new PdfPCell(new Phrase("Razón Social", _standardFont));
                clRazonSocial.BorderWidth = 0.5f;
                clRazonSocial.BorderWidthBottom = 0.5f;
                clRazonSocial.HorizontalAlignment = 1;

                PdfPCell clFechaVencimiento = new PdfPCell(new Phrase("Fecha de Vencimiento", _standardFont));
                clFechaVencimiento.BorderWidth = 0.5f;
                clFechaVencimiento.BorderWidthBottom = 0.5f;
                clFechaVencimiento.HorizontalAlignment = 1;

                PdfPCell clRFC = new PdfPCell(new Phrase("R.F.C.", _standardFont));
                clRFC.BorderWidth = 0.5f;
                clRFC.BorderWidthBottom = 0.5f;
                clRFC.HorizontalAlignment = 1;

                PdfPCell clSubtotal = new PdfPCell(new Phrase("Subtotal", _standardFont));
                clSubtotal.BorderWidth = 0.5f;
                clSubtotal.BorderWidthBottom = 0.5f;
                clSubtotal.HorizontalAlignment = 1;

                PdfPCell clIVA = new PdfPCell(new Phrase("IVA", _standardFont));
                clIVA.BorderWidth = 0.5f;
                clIVA.BorderWidthBottom = 0.5f;
                clIVA.HorizontalAlignment = 1;

                PdfPCell clTotal = new PdfPCell(new Phrase("Total", _standardFont));
                clTotal.BorderWidth = 0.5f;
                clTotal.BorderWidthBottom = 0.5f;
                clTotal.HorizontalAlignment = 1;

                PdfPCell clPendiente = new PdfPCell(new Phrase("Pendiente", _standardFont));
                clPendiente.BorderWidth = 0.5f;
                clPendiente.BorderWidthBottom = 0.5f;
                clPendiente.HorizontalAlignment = 1;

                PdfPCell clTextoExtra3 = new PdfPCell(new Phrase("Texto Extra 3", _standardFont));
                clTextoExtra3.BorderWidth = 0.5f;
                clTextoExtra3.BorderWidthBottom = 0.5f;
                clTextoExtra3.HorizontalAlignment = 1;

                PdfPCell clAfectado = new PdfPCell(new Phrase("Afectado", _standardFont));
                clAfectado.BorderWidth = 0.5f;
                clAfectado.BorderWidthBottom = 0.5f;
                clAfectado.HorizontalAlignment = 1;

                PdfPCell clImpreso = new PdfPCell(new Phrase("Impreso", _standardFont));
                clImpreso.BorderWidth = 0.5f;
                clImpreso.BorderWidthBottom = 0.5f;
                clImpreso.HorizontalAlignment = 1;

                PdfPCell clCancelado = new PdfPCell(new Phrase("Cancelado", _standardFont));
                clCancelado.BorderWidth = 0.5f;
                clCancelado.BorderWidthBottom = 0.5f;
                clCancelado.HorizontalAlignment = 1;

                PdfPCell clTotalUnidades = new PdfPCell(new Phrase("Total de Unidades", _standardFont));
                clTotalUnidades.BorderWidth = 0.5f;
                clTotalUnidades.BorderWidthBottom = 0.5f;
                clTotalUnidades.HorizontalAlignment = 1;

                PdfPCell clClasificacionCliente2 = new PdfPCell(new Phrase("Clasificación Cliente 2", _standardFont));
                clClasificacionCliente2.BorderWidth = 0.5f;
                clClasificacionCliente2.BorderWidthBottom = 0.5f;
                clClasificacionCliente2.HorizontalAlignment = 1;

                PdfPCell clTextoExtra1 = new PdfPCell(new Phrase("Texto Extra 1", _standardFont));
                clTextoExtra1.BorderWidth = 0.5f;
                clTextoExtra1.BorderWidthBottom = 0.5f;
                clTextoExtra1.HorizontalAlignment = 1;

                PdfPCell clNombreConcepto = new PdfPCell(new Phrase("Nombre del Concepto", _standardFont));
                clNombreConcepto.BorderWidth = 0.5f;
                clNombreConcepto.BorderWidthBottom = 0.5f;
                clNombreConcepto.HorizontalAlignment = 1;
                #endregion
                //***************************************************************************************************************************************
                #region Agrega titulos en las tablas
                //agrega las tablas en el pdf
                tabla_cuentas.AddCell(clFecha);
                tabla_cuentas.AddCell(clSerie);
                tabla_cuentas.AddCell(clFolio);
                tabla_cuentas.AddCell(clNombreAgente);
                tabla_cuentas.AddCell(clRazonSocial);
                tabla_cuentas.AddCell(clFechaVencimiento);
                tabla_cuentas.AddCell(clRFC);
                tabla_cuentas.AddCell(clSubtotal);
                tabla_cuentas.AddCell(clIVA);
                tabla_cuentas.AddCell(clTotal);
                tabla_cuentas.AddCell(clPendiente);
                tabla_cuentas.AddCell(clTextoExtra3);
                tabla_cuentas.AddCell(clAfectado);
                tabla_cuentas.AddCell(clImpreso);
                tabla_cuentas.AddCell(clCancelado);
                tabla_cuentas.AddCell(clTotalUnidades);
                tabla_cuentas.AddCell(clClasificacionCliente2);
                tabla_cuentas.AddCell(clTextoExtra1);
                tabla_cuentas.AddCell(clNombreConcepto);
                #endregion

                double ValorTotal = 0;
                for (int k = 0; k < ListFactrurasCRU.Count; k++)
                {
                    #region AGREGA DATOS EN LA TABLA
                    clFecha = new PdfPCell(new Phrase(ListFactrurasCRU[k].Fecha, _smallFont));
                    clFecha.BorderWidth = 0.5f;
                    clFecha.HorizontalAlignment = 1;

                    clSerie = new PdfPCell(new Phrase(ListFactrurasCRU[k].Serie, _smallFont));
                    clSerie.BorderWidth = 0.5f;
                    clSerie.HorizontalAlignment = 1;

                    clFolio = new PdfPCell(new Phrase(ListFactrurasCRU[k].Folio, _smallFont));
                    clFolio.BorderWidth = 0.5f;
                    clFolio.HorizontalAlignment = 1;

                    clNombreAgente = new PdfPCell(new Phrase(ListFactrurasCRU[k].NombreAgente, _smallFont));
                    clNombreAgente.BorderWidth = 0.5f;
                    clNombreAgente.HorizontalAlignment = 1;

                    clRazonSocial = new PdfPCell(new Phrase(ListFactrurasCRU[k].RazonSocial, _smallFont));
                    clRazonSocial.BorderWidth = 0.5f;
                    clRazonSocial.HorizontalAlignment = 1;

                    clFechaVencimiento = new PdfPCell(new Phrase(ListFactrurasCRU[k].FechaVencimiento, _smallFont));
                    clFechaVencimiento.BorderWidth = 0.5f;
                    clFechaVencimiento.HorizontalAlignment = 1;

                    clRFC = new PdfPCell(new Phrase(ListFactrurasCRU[k].RFC, _smallFont));
                    clRFC.BorderWidth = 0.5f;
                    clRFC.HorizontalAlignment = 1;

                    clSubtotal = new PdfPCell(new Phrase(ListFactrurasCRU[k].Subtotal.ToString(), _smallFont));
                    clSubtotal.BorderWidth = 0.5f;
                    clSubtotal.HorizontalAlignment = 1;

                    clIVA = new PdfPCell(new Phrase(ListFactrurasCRU[k].IVA.ToString(), _smallFont));
                    clIVA.BorderWidth = 0.5f;
                    clIVA.HorizontalAlignment = 1;

                    clTotal = new PdfPCell(new Phrase(ListFactrurasCRU[k].Total.ToString(), _smallFont));
                    clTotal.BorderWidth = 0.5f;
                    clTotal.HorizontalAlignment = 1;
                    ValorTotal += ListFactrurasCRU[k].Total;

                    clPendiente = new PdfPCell(new Phrase(ListFactrurasCRU[k].Pendiente.ToString(), _smallFont));
                    clPendiente.BorderWidth = 0.5f;
                    clPendiente.HorizontalAlignment = 1;

                    clTextoExtra3 = new PdfPCell(new Phrase(ListFactrurasCRU[k].TextoExtra3, _smallFont));
                    clTextoExtra3.BorderWidth = 0.5f;
                    clTextoExtra3.HorizontalAlignment = 1;

                    clAfectado = new PdfPCell(new Phrase(ListFactrurasCRU[k].Afectado, _smallFont));
                    clAfectado.BorderWidth = 0.5f;
                    clAfectado.HorizontalAlignment = 1;

                    clImpreso = new PdfPCell(new Phrase(ListFactrurasCRU[k].Impreso, _smallFont));
                    clImpreso.BorderWidth = 0.5f;
                    clImpreso.HorizontalAlignment = 1;

                    clCancelado = new PdfPCell(new Phrase(ListFactrurasCRU[k].Cancelado, _smallFont));
                    clCancelado.BorderWidth = 0.5f;
                    clCancelado.HorizontalAlignment = 1;

                    clTotalUnidades = new PdfPCell(new Phrase(ListFactrurasCRU[k].TotalUnidades.ToString(), _smallFont));
                    clTotalUnidades.BorderWidth = 0.5f;
                    clTotalUnidades.HorizontalAlignment = 1;

                    clClasificacionCliente2 = new PdfPCell(new Phrase(ListFactrurasCRU[k].proveedor.Clasificación2, _smallFont));
                    clClasificacionCliente2.BorderWidth = 0.5f;
                    clClasificacionCliente2.HorizontalAlignment = 1;

                    clTextoExtra1 = new PdfPCell(new Phrase(ListFactrurasCRU[k].TextoExtra1, _smallFont));
                    clTextoExtra1.BorderWidth = 0.5f;
                    clTextoExtra1.HorizontalAlignment = 1;

                    clNombreConcepto = new PdfPCell(new Phrase(ListFactrurasCRU[k].NombreConcepto, _smallFont));
                    clNombreConcepto.BorderWidth = 0.5f;
                    clNombreConcepto.HorizontalAlignment = 1;
                    #endregion
                    #region Agrega titulos en las tablas
                    //agrega las tablas en el pdf
                    tabla_cuentas.AddCell(clFecha);
                    tabla_cuentas.AddCell(clSerie);
                    tabla_cuentas.AddCell(clFolio);
                    tabla_cuentas.AddCell(clNombreAgente);
                    tabla_cuentas.AddCell(clRazonSocial);
                    tabla_cuentas.AddCell(clFechaVencimiento);
                    tabla_cuentas.AddCell(clRFC);
                    tabla_cuentas.AddCell(clSubtotal);
                    tabla_cuentas.AddCell(clIVA);
                    tabla_cuentas.AddCell(clTotal);
                    tabla_cuentas.AddCell(clPendiente);
                    tabla_cuentas.AddCell(clTextoExtra3);
                    tabla_cuentas.AddCell(clAfectado);
                    tabla_cuentas.AddCell(clImpreso);
                    tabla_cuentas.AddCell(clCancelado);
                    tabla_cuentas.AddCell(clTotalUnidades);
                    tabla_cuentas.AddCell(clClasificacionCliente2);
                    tabla_cuentas.AddCell(clTextoExtra1);
                    tabla_cuentas.AddCell(clNombreConcepto);
                    #endregion
                }//fin for

                doc.Add(new Paragraph("Total: $" + Math.Round(ValorTotal, 2), _titulos));
                doc.Add(new Paragraph("\n"));

                //agrego la tabla al pdf
                doc.Add(tabla_cuentas);

                #endregion
                doc.Add(new Paragraph("\n"));
                doc.Add(new Paragraph("\n"));
                doc.Add(new Paragraph("\n"));
                
                /************************************************************/
                #region **********SEGUNDA TABLA  FILTRO RFC PUBLICO*********
                doc.Add(new Paragraph("21 Acumulado de Facturación en CRU Público ", _titulos));
                doc.Add(new Paragraph("\n"));
                tabla_cuentas = new PdfPTable(19);
                //PdfPCell cell = new PdfPCell(new Phrase("Reporte de Compras"));
                //cell.Colspan = 3;
                //cell.BackgroundColor = BaseColor.BLUE;
                //cell.HorizontalAlignment = 1;//0=Left, 1=Centre, 2=Right 
                tabla_cuentas.WidthPercentage = 100;
                //tabla_cuentas.AddCell(cell);



                #region configuracion de columnas
                // Configuramos el título de las columnas de la tabla 
                clFecha = new PdfPCell(new Phrase("Fecha", _standardFont));
                clFecha.BorderWidth = 0.5f;
                clFecha.BorderWidthBottom = 0.5f;
                clFecha.HorizontalAlignment = 1;

                clSerie = new PdfPCell(new Phrase("Serie", _standardFont));
                clSerie.BorderWidth = 0.5f;
                clSerie.BorderWidthBottom = 0.5f;
                clSerie.HorizontalAlignment = 1;

                clFolio = new PdfPCell(new Phrase("Folio", _standardFont));
                clFolio.BorderWidth = 0.5f;
                clFolio.BorderWidthBottom = 0.5f;
                clFolio.HorizontalAlignment = 1;

                clNombreAgente = new PdfPCell(new Phrase("Nombre del Agente", _standardFont));
                clNombreAgente.BorderWidth = 0.5f;
                clNombreAgente.BorderWidthBottom = 0.5f;
                clNombreAgente.HorizontalAlignment = 1;

                clRazonSocial = new PdfPCell(new Phrase("Razón Social", _standardFont));
                clRazonSocial.BorderWidth = 0.5f;
                clRazonSocial.BorderWidthBottom = 0.5f;
                clRazonSocial.HorizontalAlignment = 1;

                clFechaVencimiento = new PdfPCell(new Phrase("Fecha de Vencimiento", _standardFont));
                clFechaVencimiento.BorderWidth = 0.5f;
                clFechaVencimiento.BorderWidthBottom = 0.5f;
                clFechaVencimiento.HorizontalAlignment = 1;

                clRFC = new PdfPCell(new Phrase("R.F.C.", _standardFont));
                clRFC.BorderWidth = 0.5f;
                clRFC.BorderWidthBottom = 0.5f;
                clRFC.HorizontalAlignment = 1;

                clSubtotal = new PdfPCell(new Phrase("Subtotal", _standardFont));
                clSubtotal.BorderWidth = 0.5f;
                clSubtotal.BorderWidthBottom = 0.5f;
                clSubtotal.HorizontalAlignment = 1;

                clIVA = new PdfPCell(new Phrase("IVA", _standardFont));
                clIVA.BorderWidth = 0.5f;
                clIVA.BorderWidthBottom = 0.5f;
                clIVA.HorizontalAlignment = 1;

                clTotal = new PdfPCell(new Phrase("Total", _standardFont));
                clTotal.BorderWidth = 0.5f;
                clTotal.BorderWidthBottom = 0.5f;
                clTotal.HorizontalAlignment = 1;

                clPendiente = new PdfPCell(new Phrase("Pendiente", _standardFont));
                clPendiente.BorderWidth = 0.5f;
                clPendiente.BorderWidthBottom = 0.5f;
                clPendiente.HorizontalAlignment = 1;

                clTextoExtra3 = new PdfPCell(new Phrase("Texto Extra 3", _standardFont));
                clTextoExtra3.BorderWidth = 0.5f;
                clTextoExtra3.BorderWidthBottom = 0.5f;
                clTextoExtra3.HorizontalAlignment = 1;

                clAfectado = new PdfPCell(new Phrase("Afectado", _standardFont));
                clAfectado.BorderWidth = 0.5f;
                clAfectado.BorderWidthBottom = 0.5f;
                clAfectado.HorizontalAlignment = 1;

                clImpreso = new PdfPCell(new Phrase("Impreso", _standardFont));
                clImpreso.BorderWidth = 0.5f;
                clImpreso.BorderWidthBottom = 0.5f;
                clImpreso.HorizontalAlignment = 1;

                clCancelado = new PdfPCell(new Phrase("Cancelado", _standardFont));
                clCancelado.BorderWidth = 0.5f;
                clCancelado.BorderWidthBottom = 0.5f;
                clCancelado.HorizontalAlignment = 1;

                clTotalUnidades = new PdfPCell(new Phrase("Total de Unidades", _standardFont));
                clTotalUnidades.BorderWidth = 0.5f;
                clTotalUnidades.BorderWidthBottom = 0.5f;
                clTotalUnidades.HorizontalAlignment = 1;

                clClasificacionCliente2 = new PdfPCell(new Phrase("Clasificación Cliente 2", _standardFont));
                clClasificacionCliente2.BorderWidth = 0.5f;
                clClasificacionCliente2.BorderWidthBottom = 0.5f;
                clClasificacionCliente2.HorizontalAlignment = 1;

                clTextoExtra1 = new PdfPCell(new Phrase("Texto Extra 1", _standardFont));
                clTextoExtra1.BorderWidth = 0.5f;
                clTextoExtra1.BorderWidthBottom = 0.5f;
                clTextoExtra1.HorizontalAlignment = 1;

                clNombreConcepto = new PdfPCell(new Phrase("Nombre del Concepto", _standardFont));
                clNombreConcepto.BorderWidth = 0.5f;
                clNombreConcepto.BorderWidthBottom = 0.5f;
                clNombreConcepto.HorizontalAlignment = 1;
                #endregion
                //***************************************************************************************************************************************
                #region Agrega titulos en las tablas
                //agrega las tablas en el pdf
                tabla_cuentas.AddCell(clFecha);
                tabla_cuentas.AddCell(clSerie);
                tabla_cuentas.AddCell(clFolio);
                tabla_cuentas.AddCell(clNombreAgente);
                tabla_cuentas.AddCell(clRazonSocial);
                tabla_cuentas.AddCell(clFechaVencimiento);
                tabla_cuentas.AddCell(clRFC);
                tabla_cuentas.AddCell(clSubtotal);
                tabla_cuentas.AddCell(clIVA);
                tabla_cuentas.AddCell(clTotal);
                tabla_cuentas.AddCell(clPendiente);
                tabla_cuentas.AddCell(clTextoExtra3);
                tabla_cuentas.AddCell(clAfectado);
                tabla_cuentas.AddCell(clImpreso);
                tabla_cuentas.AddCell(clCancelado);
                tabla_cuentas.AddCell(clTotalUnidades);
                tabla_cuentas.AddCell(clClasificacionCliente2);
                tabla_cuentas.AddCell(clTextoExtra1);
                tabla_cuentas.AddCell(clNombreConcepto);
                #endregion
                ListFactrurasCRU = ListFactrurasCRUFiltroRFCPublico;
                ValorTotal = 0;
                for (int k = 0; k < ListFactrurasCRU.Count; k++)
                {
                    #region AGREGA DATOS EN LA TABLA
                    clFecha = new PdfPCell(new Phrase(ListFactrurasCRU[k].Fecha, _smallFont));
                    clFecha.BorderWidth = 0.5f;
                    clFecha.HorizontalAlignment = 1;

                    clSerie = new PdfPCell(new Phrase(ListFactrurasCRU[k].Serie, _smallFont));
                    clSerie.BorderWidth = 0.5f;
                    clSerie.HorizontalAlignment = 1;

                    clFolio = new PdfPCell(new Phrase(ListFactrurasCRU[k].Folio, _smallFont));
                    clFolio.BorderWidth = 0.5f;
                    clFolio.HorizontalAlignment = 1;

                    clNombreAgente = new PdfPCell(new Phrase(ListFactrurasCRU[k].NombreAgente, _smallFont));
                    clNombreAgente.BorderWidth = 0.5f;
                    clNombreAgente.HorizontalAlignment = 1;

                    clRazonSocial = new PdfPCell(new Phrase(ListFactrurasCRU[k].RazonSocial, _smallFont));
                    clRazonSocial.BorderWidth = 0.5f;
                    clRazonSocial.HorizontalAlignment = 1;

                    clFechaVencimiento = new PdfPCell(new Phrase(ListFactrurasCRU[k].FechaVencimiento, _smallFont));
                    clFechaVencimiento.BorderWidth = 0.5f;
                    clFechaVencimiento.HorizontalAlignment = 1;

                    clRFC = new PdfPCell(new Phrase(ListFactrurasCRU[k].RFC, _smallFont));
                    clRFC.BorderWidth = 0.5f;
                    clRFC.HorizontalAlignment = 1;

                    clSubtotal = new PdfPCell(new Phrase(ListFactrurasCRU[k].Subtotal.ToString(), _smallFont));
                    clSubtotal.BorderWidth = 0.5f;
                    clSubtotal.HorizontalAlignment = 1;

                    clIVA = new PdfPCell(new Phrase(ListFactrurasCRU[k].IVA.ToString(), _smallFont));
                    clIVA.BorderWidth = 0.5f;
                    clIVA.HorizontalAlignment = 1;

                    clTotal = new PdfPCell(new Phrase(ListFactrurasCRU[k].Total.ToString(), _smallFont));
                    clTotal.BorderWidth = 0.5f;
                    clTotal.HorizontalAlignment = 1;
                    ValorTotal += ListFactrurasCRU[k].Total;

                    clPendiente = new PdfPCell(new Phrase(ListFactrurasCRU[k].Pendiente.ToString(), _smallFont));
                    clPendiente.BorderWidth = 0.5f;
                    clPendiente.HorizontalAlignment = 1;

                    clTextoExtra3 = new PdfPCell(new Phrase(ListFactrurasCRU[k].TextoExtra3, _smallFont));
                    clTextoExtra3.BorderWidth = 0.5f;
                    clTextoExtra3.HorizontalAlignment = 1;

                    clAfectado = new PdfPCell(new Phrase(ListFactrurasCRU[k].Afectado, _smallFont));
                    clAfectado.BorderWidth = 0.5f;
                    clAfectado.HorizontalAlignment = 1;

                    clImpreso = new PdfPCell(new Phrase(ListFactrurasCRU[k].Impreso, _smallFont));
                    clImpreso.BorderWidth = 0.5f;
                    clImpreso.HorizontalAlignment = 1;

                    clCancelado = new PdfPCell(new Phrase(ListFactrurasCRU[k].Cancelado, _smallFont));
                    clCancelado.BorderWidth = 0.5f;
                    clCancelado.HorizontalAlignment = 1;

                    clTotalUnidades = new PdfPCell(new Phrase(ListFactrurasCRU[k].TotalUnidades.ToString(), _smallFont));
                    clTotalUnidades.BorderWidth = 0.5f;
                    clTotalUnidades.HorizontalAlignment = 1;

                    clClasificacionCliente2 = new PdfPCell(new Phrase(ListFactrurasCRU[k].proveedor.Clasificación2, _smallFont));
                    clClasificacionCliente2.BorderWidth = 0.5f;
                    clClasificacionCliente2.HorizontalAlignment = 1;

                    clTextoExtra1 = new PdfPCell(new Phrase(ListFactrurasCRU[k].TextoExtra1, _smallFont));
                    clTextoExtra1.BorderWidth = 0.5f;
                    clTextoExtra1.HorizontalAlignment = 1;

                    clNombreConcepto = new PdfPCell(new Phrase(ListFactrurasCRU[k].NombreConcepto, _smallFont));
                    clNombreConcepto.BorderWidth = 0.5f;
                    clNombreConcepto.HorizontalAlignment = 1;
                    #endregion
                    #region Agrega titulos en las tablas
                    //agrega las tablas en el pdf
                    tabla_cuentas.AddCell(clFecha);
                    tabla_cuentas.AddCell(clSerie);
                    tabla_cuentas.AddCell(clFolio);
                    tabla_cuentas.AddCell(clNombreAgente);
                    tabla_cuentas.AddCell(clRazonSocial);
                    tabla_cuentas.AddCell(clFechaVencimiento);
                    tabla_cuentas.AddCell(clRFC);
                    tabla_cuentas.AddCell(clSubtotal);
                    tabla_cuentas.AddCell(clIVA);
                    tabla_cuentas.AddCell(clTotal);
                    tabla_cuentas.AddCell(clPendiente);
                    tabla_cuentas.AddCell(clTextoExtra3);
                    tabla_cuentas.AddCell(clAfectado);
                    tabla_cuentas.AddCell(clImpreso);
                    tabla_cuentas.AddCell(clCancelado);
                    tabla_cuentas.AddCell(clTotalUnidades);
                    tabla_cuentas.AddCell(clClasificacionCliente2);
                    tabla_cuentas.AddCell(clTextoExtra1);
                    tabla_cuentas.AddCell(clNombreConcepto);
                    #endregion
                }//fin for

                doc.Add(new Paragraph("Total: $" + Math.Round(ValorTotal, 2), _titulos));
                doc.Add(new Paragraph("\n"));

                //agrego la tabla al pdf
                doc.Add(tabla_cuentas);

                #endregion
                /******************************************************************************************/

                doc.Add(new Paragraph("\n"));
                doc.Add(new Paragraph("\n"));
                doc.Add(new Paragraph("\n"));

                /************************************************************/
                #region **********TERCER TABLA  FILTRO RFC OL*********
                doc.Add(new Paragraph("22 Acumulado de Facturación en CRU OL ", _titulos));
                doc.Add(new Paragraph("\n"));
                tabla_cuentas = new PdfPTable(19);
                //PdfPCell cell = new PdfPCell(new Phrase("Reporte de Compras"));
                //cell.Colspan = 3;
                //cell.BackgroundColor = BaseColor.BLUE;
                //cell.HorizontalAlignment = 1;//0=Left, 1=Centre, 2=Right 
                tabla_cuentas.WidthPercentage = 100;
                //tabla_cuentas.AddCell(cell);



                #region configuracion de columnas
                // Configuramos el título de las columnas de la tabla 
                clFecha = new PdfPCell(new Phrase("Fecha", _standardFont));
                clFecha.BorderWidth = 0.5f;
                clFecha.BorderWidthBottom = 0.5f;
                clFecha.HorizontalAlignment = 1;

                clSerie = new PdfPCell(new Phrase("Serie", _standardFont));
                clSerie.BorderWidth = 0.5f;
                clSerie.BorderWidthBottom = 0.5f;
                clSerie.HorizontalAlignment = 1;

                clFolio = new PdfPCell(new Phrase("Folio", _standardFont));
                clFolio.BorderWidth = 0.5f;
                clFolio.BorderWidthBottom = 0.5f;
                clFolio.HorizontalAlignment = 1;

                clNombreAgente = new PdfPCell(new Phrase("Nombre del Agente", _standardFont));
                clNombreAgente.BorderWidth = 0.5f;
                clNombreAgente.BorderWidthBottom = 0.5f;
                clNombreAgente.HorizontalAlignment = 1;

                clRazonSocial = new PdfPCell(new Phrase("Razón Social", _standardFont));
                clRazonSocial.BorderWidth = 0.5f;
                clRazonSocial.BorderWidthBottom = 0.5f;
                clRazonSocial.HorizontalAlignment = 1;

                clFechaVencimiento = new PdfPCell(new Phrase("Fecha de Vencimiento", _standardFont));
                clFechaVencimiento.BorderWidth = 0.5f;
                clFechaVencimiento.BorderWidthBottom = 0.5f;
                clFechaVencimiento.HorizontalAlignment = 1;

                clRFC = new PdfPCell(new Phrase("R.F.C.", _standardFont));
                clRFC.BorderWidth = 0.5f;
                clRFC.BorderWidthBottom = 0.5f;
                clRFC.HorizontalAlignment = 1;

                clSubtotal = new PdfPCell(new Phrase("Subtotal", _standardFont));
                clSubtotal.BorderWidth = 0.5f;
                clSubtotal.BorderWidthBottom = 0.5f;
                clSubtotal.HorizontalAlignment = 1;

                clIVA = new PdfPCell(new Phrase("IVA", _standardFont));
                clIVA.BorderWidth = 0.5f;
                clIVA.BorderWidthBottom = 0.5f;
                clIVA.HorizontalAlignment = 1;

                clTotal = new PdfPCell(new Phrase("Total", _standardFont));
                clTotal.BorderWidth = 0.5f;
                clTotal.BorderWidthBottom = 0.5f;
                clTotal.HorizontalAlignment = 1;

                clPendiente = new PdfPCell(new Phrase("Pendiente", _standardFont));
                clPendiente.BorderWidth = 0.5f;
                clPendiente.BorderWidthBottom = 0.5f;
                clPendiente.HorizontalAlignment = 1;

                clTextoExtra3 = new PdfPCell(new Phrase("Texto Extra 3", _standardFont));
                clTextoExtra3.BorderWidth = 0.5f;
                clTextoExtra3.BorderWidthBottom = 0.5f;
                clTextoExtra3.HorizontalAlignment = 1;

                clAfectado = new PdfPCell(new Phrase("Afectado", _standardFont));
                clAfectado.BorderWidth = 0.5f;
                clAfectado.BorderWidthBottom = 0.5f;
                clAfectado.HorizontalAlignment = 1;

                clImpreso = new PdfPCell(new Phrase("Impreso", _standardFont));
                clImpreso.BorderWidth = 0.5f;
                clImpreso.BorderWidthBottom = 0.5f;
                clImpreso.HorizontalAlignment = 1;

                clCancelado = new PdfPCell(new Phrase("Cancelado", _standardFont));
                clCancelado.BorderWidth = 0.5f;
                clCancelado.BorderWidthBottom = 0.5f;
                clCancelado.HorizontalAlignment = 1;

                clTotalUnidades = new PdfPCell(new Phrase("Total de Unidades", _standardFont));
                clTotalUnidades.BorderWidth = 0.5f;
                clTotalUnidades.BorderWidthBottom = 0.5f;
                clTotalUnidades.HorizontalAlignment = 1;

                clClasificacionCliente2 = new PdfPCell(new Phrase("Clasificación Cliente 2", _standardFont));
                clClasificacionCliente2.BorderWidth = 0.5f;
                clClasificacionCliente2.BorderWidthBottom = 0.5f;
                clClasificacionCliente2.HorizontalAlignment = 1;

                clTextoExtra1 = new PdfPCell(new Phrase("Texto Extra 1", _standardFont));
                clTextoExtra1.BorderWidth = 0.5f;
                clTextoExtra1.BorderWidthBottom = 0.5f;
                clTextoExtra1.HorizontalAlignment = 1;

                clNombreConcepto = new PdfPCell(new Phrase("Nombre del Concepto", _standardFont));
                clNombreConcepto.BorderWidth = 0.5f;
                clNombreConcepto.BorderWidthBottom = 0.5f;
                clNombreConcepto.HorizontalAlignment = 1;
                #endregion
                //***************************************************************************************************************************************
                #region Agrega titulos en las tablas
                //agrega las tablas en el pdf
                tabla_cuentas.AddCell(clFecha);
                tabla_cuentas.AddCell(clSerie);
                tabla_cuentas.AddCell(clFolio);
                tabla_cuentas.AddCell(clNombreAgente);
                tabla_cuentas.AddCell(clRazonSocial);
                tabla_cuentas.AddCell(clFechaVencimiento);
                tabla_cuentas.AddCell(clRFC);
                tabla_cuentas.AddCell(clSubtotal);
                tabla_cuentas.AddCell(clIVA);
                tabla_cuentas.AddCell(clTotal);
                tabla_cuentas.AddCell(clPendiente);
                tabla_cuentas.AddCell(clTextoExtra3);
                tabla_cuentas.AddCell(clAfectado);
                tabla_cuentas.AddCell(clImpreso);
                tabla_cuentas.AddCell(clCancelado);
                tabla_cuentas.AddCell(clTotalUnidades);
                tabla_cuentas.AddCell(clClasificacionCliente2);
                tabla_cuentas.AddCell(clTextoExtra1);
                tabla_cuentas.AddCell(clNombreConcepto);
                #endregion
                ListFactrurasCRU = ListFactrurasCRUFiltroRFCOL;
                ValorTotal = 0;
                for (int k = 0; k < ListFactrurasCRU.Count; k++)
                {
                    #region AGREGA DATOS EN LA TABLA
                    clFecha = new PdfPCell(new Phrase(ListFactrurasCRU[k].Fecha, _smallFont));
                    clFecha.BorderWidth = 0.5f;
                    clFecha.HorizontalAlignment = 1;

                    clSerie = new PdfPCell(new Phrase(ListFactrurasCRU[k].Serie, _smallFont));
                    clSerie.BorderWidth = 0.5f;
                    clSerie.HorizontalAlignment = 1;

                    clFolio = new PdfPCell(new Phrase(ListFactrurasCRU[k].Folio, _smallFont));
                    clFolio.BorderWidth = 0.5f;
                    clFolio.HorizontalAlignment = 1;

                    clNombreAgente = new PdfPCell(new Phrase(ListFactrurasCRU[k].NombreAgente, _smallFont));
                    clNombreAgente.BorderWidth = 0.5f;
                    clNombreAgente.HorizontalAlignment = 1;

                    clRazonSocial = new PdfPCell(new Phrase(ListFactrurasCRU[k].RazonSocial, _smallFont));
                    clRazonSocial.BorderWidth = 0.5f;
                    clRazonSocial.HorizontalAlignment = 1;

                    clFechaVencimiento = new PdfPCell(new Phrase(ListFactrurasCRU[k].FechaVencimiento, _smallFont));
                    clFechaVencimiento.BorderWidth = 0.5f;
                    clFechaVencimiento.HorizontalAlignment = 1;

                    clRFC = new PdfPCell(new Phrase(ListFactrurasCRU[k].RFC, _smallFont));
                    clRFC.BorderWidth = 0.5f;
                    clRFC.HorizontalAlignment = 1;

                    clSubtotal = new PdfPCell(new Phrase(ListFactrurasCRU[k].Subtotal.ToString(), _smallFont));
                    clSubtotal.BorderWidth = 0.5f;
                    clSubtotal.HorizontalAlignment = 1;

                    clIVA = new PdfPCell(new Phrase(ListFactrurasCRU[k].IVA.ToString(), _smallFont));
                    clIVA.BorderWidth = 0.5f;
                    clIVA.HorizontalAlignment = 1;

                    clTotal = new PdfPCell(new Phrase(ListFactrurasCRU[k].Total.ToString(), _smallFont));
                    clTotal.BorderWidth = 0.5f;
                    clTotal.HorizontalAlignment = 1;
                    ValorTotal += ListFactrurasCRU[k].Total;

                    clPendiente = new PdfPCell(new Phrase(ListFactrurasCRU[k].Pendiente.ToString(), _smallFont));
                    clPendiente.BorderWidth = 0.5f;
                    clPendiente.HorizontalAlignment = 1;

                    clTextoExtra3 = new PdfPCell(new Phrase(ListFactrurasCRU[k].TextoExtra3, _smallFont));
                    clTextoExtra3.BorderWidth = 0.5f;
                    clTextoExtra3.HorizontalAlignment = 1;

                    clAfectado = new PdfPCell(new Phrase(ListFactrurasCRU[k].Afectado, _smallFont));
                    clAfectado.BorderWidth = 0.5f;
                    clAfectado.HorizontalAlignment = 1;

                    clImpreso = new PdfPCell(new Phrase(ListFactrurasCRU[k].Impreso, _smallFont));
                    clImpreso.BorderWidth = 0.5f;
                    clImpreso.HorizontalAlignment = 1;

                    clCancelado = new PdfPCell(new Phrase(ListFactrurasCRU[k].Cancelado, _smallFont));
                    clCancelado.BorderWidth = 0.5f;
                    clCancelado.HorizontalAlignment = 1;

                    clTotalUnidades = new PdfPCell(new Phrase(ListFactrurasCRU[k].TotalUnidades.ToString(), _smallFont));
                    clTotalUnidades.BorderWidth = 0.5f;
                    clTotalUnidades.HorizontalAlignment = 1;

                    clClasificacionCliente2 = new PdfPCell(new Phrase(ListFactrurasCRU[k].proveedor.Clasificación2, _smallFont));
                    clClasificacionCliente2.BorderWidth = 0.5f;
                    clClasificacionCliente2.HorizontalAlignment = 1;

                    clTextoExtra1 = new PdfPCell(new Phrase(ListFactrurasCRU[k].TextoExtra1, _smallFont));
                    clTextoExtra1.BorderWidth = 0.5f;
                    clTextoExtra1.HorizontalAlignment = 1;

                    clNombreConcepto = new PdfPCell(new Phrase(ListFactrurasCRU[k].NombreConcepto, _smallFont));
                    clNombreConcepto.BorderWidth = 0.5f;
                    clNombreConcepto.HorizontalAlignment = 1;
                    #endregion
                    #region Agrega titulos en las tablas
                    //agrega las tablas en el pdf
                    tabla_cuentas.AddCell(clFecha);
                    tabla_cuentas.AddCell(clSerie);
                    tabla_cuentas.AddCell(clFolio);
                    tabla_cuentas.AddCell(clNombreAgente);
                    tabla_cuentas.AddCell(clRazonSocial);
                    tabla_cuentas.AddCell(clFechaVencimiento);
                    tabla_cuentas.AddCell(clRFC);
                    tabla_cuentas.AddCell(clSubtotal);
                    tabla_cuentas.AddCell(clIVA);
                    tabla_cuentas.AddCell(clTotal);
                    tabla_cuentas.AddCell(clPendiente);
                    tabla_cuentas.AddCell(clTextoExtra3);
                    tabla_cuentas.AddCell(clAfectado);
                    tabla_cuentas.AddCell(clImpreso);
                    tabla_cuentas.AddCell(clCancelado);
                    tabla_cuentas.AddCell(clTotalUnidades);
                    tabla_cuentas.AddCell(clClasificacionCliente2);
                    tabla_cuentas.AddCell(clTextoExtra1);
                    tabla_cuentas.AddCell(clNombreConcepto);
                    #endregion
                }//fin for

                doc.Add(new Paragraph("Total: $" + Math.Round(ValorTotal, 2), _titulos));
                doc.Add(new Paragraph("\n"));

                //agrego la tabla al pdf
                doc.Add(tabla_cuentas);

                #endregion
                /******************************************************************************************/
                // cierro la edicion del pdf
                doc.Close();
                
                ////LO EJECUTO
                Process prc = new System.Diagnostics.Process();
                prc.StartInfo.FileName = rut;
                prc.Start();

            }
            catch (Exception g)
            { MessageBox.Show("" + g.Message); }
        }
        #endregion




        #region CRU ABONOS
        /// <summary>
        /// Método para imprimir las facturas de CRU
        /// </summary>
        /// <param name="ListFactrurasCRU">lista de las facturas que se van a imprimir</param>
        public void ImpresionCRUAbonos(List<Tipos_Datos_CRU.FacturasCRU> ListFactrurasCRU, string fechas, string path, List<Tipos_Datos_CRU.FacturasCRU> ListFactrurasCRUFiltroRFCPublico, List<Tipos_Datos_CRU.FacturasCRU> ListFactrurasCRUFiltroRFCOL)
        {

            try
            {

                Document doc = new Document(PageSize.TABLOID, 10, 10, 10, 10);//Creacion del documento configuracion de tipo de hoja y margenes
                doc.AddAuthor("Indicadores");//Autor del PDF
                doc.AddKeywords("pdf, PdfWriter; Indicadores V1");

                //para almacenamiento del archivo
                string nombre_archivo = "AbonosCRU.PDF";//Nombre del Archivo
                string rut = @path + nombre_archivo;
                PdfWriter wri = PdfWriter.GetInstance(doc, new FileStream(rut, FileMode.Create));
                doc.AddTitle("REPORTE");
                doc.AddCreator("*********");
                doc.Open();
                //tipo de letras que se pueda usar en el archivo PDF
                iTextSharp.text.Font _mediumFont = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 8, iTextSharp.text.Font.NORMAL, BaseColor.BLACK);
                iTextSharp.text.Font _standardFont = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 9, iTextSharp.text.Font.BOLD, BaseColor.BLACK);
                iTextSharp.text.Font _standardFont1 = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 14, iTextSharp.text.Font.BOLD, BaseColor.WHITE);
                iTextSharp.text.Font _smallFont = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 8, iTextSharp.text.Font.NORMAL, BaseColor.BLACK);
                iTextSharp.text.Font _titulo = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 14, iTextSharp.text.Font.NORMAL, BaseColor.BLACK);
                iTextSharp.text.Font _titulos = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 12, iTextSharp.text.Font.NORMAL, BaseColor.BLACK);


                // Cabecera
                doc.Add(new Paragraph(" Desglose general de Abonos CRU " + fechas, _titulo));
                doc.Add(new Paragraph("\n"));
                doc.Add(new Paragraph("\n"));

                //***********************************************
                #region **********PRIMERA TABLA 23 Acumulado de CXC (Abonos) en CRU*********
                doc.Add(new Paragraph("23 Acumulado de CXC (Abonos) en CRU", _titulos));
                doc.Add(new Paragraph("\n"));
                PdfPTable tabla_cuentas = new PdfPTable(19);
                //PdfPCell cell = new PdfPCell(new Phrase("Reporte de Compras"));
                //cell.Colspan = 3;
                //cell.BackgroundColor = BaseColor.BLUE;
                //cell.HorizontalAlignment = 1;//0=Left, 1=Centre, 2=Right 
                tabla_cuentas.WidthPercentage = 100;
                //tabla_cuentas.AddCell(cell);



                #region configuracion de columnas
                // Configuramos el título de las columnas de la tabla 
                PdfPCell clFecha = new PdfPCell(new Phrase("Fecha", _standardFont));
                clFecha.BorderWidth = 0.5f;
                clFecha.BorderWidthBottom = 0.5f;
                clFecha.HorizontalAlignment = 1;

                PdfPCell clSerie = new PdfPCell(new Phrase("Serie", _standardFont));
                clSerie.BorderWidth = 0.5f;
                clSerie.BorderWidthBottom = 0.5f;
                clSerie.HorizontalAlignment = 1;

                PdfPCell clFolio = new PdfPCell(new Phrase("Folio", _standardFont));
                clFolio.BorderWidth = 0.5f;
                clFolio.BorderWidthBottom = 0.5f;
                clFolio.HorizontalAlignment = 1;

                PdfPCell clNombreAgente = new PdfPCell(new Phrase("Nombre del Agente", _standardFont));
                clNombreAgente.BorderWidth = 0.5f;
                clNombreAgente.BorderWidthBottom = 0.5f;
                clNombreAgente.HorizontalAlignment = 1;

                PdfPCell clRazonSocial = new PdfPCell(new Phrase("Razón Social", _standardFont));
                clRazonSocial.BorderWidth = 0.5f;
                clRazonSocial.BorderWidthBottom = 0.5f;
                clRazonSocial.HorizontalAlignment = 1;

                PdfPCell clFechaVencimiento = new PdfPCell(new Phrase("Fecha de Vencimiento", _standardFont));
                clFechaVencimiento.BorderWidth = 0.5f;
                clFechaVencimiento.BorderWidthBottom = 0.5f;
                clFechaVencimiento.HorizontalAlignment = 1;

                PdfPCell clRFC = new PdfPCell(new Phrase("R.F.C.", _standardFont));
                clRFC.BorderWidth = 0.5f;
                clRFC.BorderWidthBottom = 0.5f;
                clRFC.HorizontalAlignment = 1;

                PdfPCell clSubtotal = new PdfPCell(new Phrase("Subtotal", _standardFont));
                clSubtotal.BorderWidth = 0.5f;
                clSubtotal.BorderWidthBottom = 0.5f;
                clSubtotal.HorizontalAlignment = 1;

                PdfPCell clIVA = new PdfPCell(new Phrase("IVA", _standardFont));
                clIVA.BorderWidth = 0.5f;
                clIVA.BorderWidthBottom = 0.5f;
                clIVA.HorizontalAlignment = 1;

                PdfPCell clTotal = new PdfPCell(new Phrase("Total", _standardFont));
                clTotal.BorderWidth = 0.5f;
                clTotal.BorderWidthBottom = 0.5f;
                clTotal.HorizontalAlignment = 1;

                PdfPCell clPendiente = new PdfPCell(new Phrase("Pendiente", _standardFont));
                clPendiente.BorderWidth = 0.5f;
                clPendiente.BorderWidthBottom = 0.5f;
                clPendiente.HorizontalAlignment = 1;

                PdfPCell clTextoExtra3 = new PdfPCell(new Phrase("Texto Extra 3", _standardFont));
                clTextoExtra3.BorderWidth = 0.5f;
                clTextoExtra3.BorderWidthBottom = 0.5f;
                clTextoExtra3.HorizontalAlignment = 1;

                PdfPCell clAfectado = new PdfPCell(new Phrase("Afectado", _standardFont));
                clAfectado.BorderWidth = 0.5f;
                clAfectado.BorderWidthBottom = 0.5f;
                clAfectado.HorizontalAlignment = 1;

                PdfPCell clImpreso = new PdfPCell(new Phrase("Impreso", _standardFont));
                clImpreso.BorderWidth = 0.5f;
                clImpreso.BorderWidthBottom = 0.5f;
                clImpreso.HorizontalAlignment = 1;

                PdfPCell clCancelado = new PdfPCell(new Phrase("Cancelado", _standardFont));
                clCancelado.BorderWidth = 0.5f;
                clCancelado.BorderWidthBottom = 0.5f;
                clCancelado.HorizontalAlignment = 1;

                PdfPCell clTotalUnidades = new PdfPCell(new Phrase("Total de Unidades", _standardFont));
                clTotalUnidades.BorderWidth = 0.5f;
                clTotalUnidades.BorderWidthBottom = 0.5f;
                clTotalUnidades.HorizontalAlignment = 1;

                PdfPCell clClasificacionCliente2 = new PdfPCell(new Phrase("Clasificación Cliente 2", _standardFont));
                clClasificacionCliente2.BorderWidth = 0.5f;
                clClasificacionCliente2.BorderWidthBottom = 0.5f;
                clClasificacionCliente2.HorizontalAlignment = 1;

                PdfPCell clTextoExtra1 = new PdfPCell(new Phrase("Texto Extra 1", _standardFont));
                clTextoExtra1.BorderWidth = 0.5f;
                clTextoExtra1.BorderWidthBottom = 0.5f;
                clTextoExtra1.HorizontalAlignment = 1;

                PdfPCell clNombreConcepto = new PdfPCell(new Phrase("Nombre del Concepto", _standardFont));
                clNombreConcepto.BorderWidth = 0.5f;
                clNombreConcepto.BorderWidthBottom = 0.5f;
                clNombreConcepto.HorizontalAlignment = 1;
                #endregion
                //***************************************************************************************************************************************
                #region Agrega titulos en las tablas
                //agrega las tablas en el pdf
                tabla_cuentas.AddCell(clFecha);
                tabla_cuentas.AddCell(clSerie);
                tabla_cuentas.AddCell(clFolio);
                tabla_cuentas.AddCell(clNombreAgente);
                tabla_cuentas.AddCell(clRazonSocial);
                tabla_cuentas.AddCell(clFechaVencimiento);
                tabla_cuentas.AddCell(clRFC);
                tabla_cuentas.AddCell(clSubtotal);
                tabla_cuentas.AddCell(clIVA);
                tabla_cuentas.AddCell(clTotal);
                tabla_cuentas.AddCell(clPendiente);
                tabla_cuentas.AddCell(clTextoExtra3);
                tabla_cuentas.AddCell(clAfectado);
                tabla_cuentas.AddCell(clImpreso);
                tabla_cuentas.AddCell(clCancelado);
                tabla_cuentas.AddCell(clTotalUnidades);
                tabla_cuentas.AddCell(clClasificacionCliente2);
                tabla_cuentas.AddCell(clTextoExtra1);
                tabla_cuentas.AddCell(clNombreConcepto);
                #endregion

                double ValorTotal = 0;
                for (int k = 0; k < ListFactrurasCRU.Count; k++)
                {
                    #region AGREGA DATOS EN LA TABLA
                    clFecha = new PdfPCell(new Phrase(ListFactrurasCRU[k].Fecha, _smallFont));
                    clFecha.BorderWidth = 0.5f;
                    clFecha.HorizontalAlignment = 1;

                    clSerie = new PdfPCell(new Phrase(ListFactrurasCRU[k].Serie, _smallFont));
                    clSerie.BorderWidth = 0.5f;
                    clSerie.HorizontalAlignment = 1;

                    clFolio = new PdfPCell(new Phrase(ListFactrurasCRU[k].Folio, _smallFont));
                    clFolio.BorderWidth = 0.5f;
                    clFolio.HorizontalAlignment = 1;

                    clNombreAgente = new PdfPCell(new Phrase(ListFactrurasCRU[k].NombreAgente, _smallFont));
                    clNombreAgente.BorderWidth = 0.5f;
                    clNombreAgente.HorizontalAlignment = 1;

                    clRazonSocial = new PdfPCell(new Phrase(ListFactrurasCRU[k].RazonSocial, _smallFont));
                    clRazonSocial.BorderWidth = 0.5f;
                    clRazonSocial.HorizontalAlignment = 1;

                    clFechaVencimiento = new PdfPCell(new Phrase(ListFactrurasCRU[k].FechaVencimiento, _smallFont));
                    clFechaVencimiento.BorderWidth = 0.5f;
                    clFechaVencimiento.HorizontalAlignment = 1;

                    clRFC = new PdfPCell(new Phrase(ListFactrurasCRU[k].RFC, _smallFont));
                    clRFC.BorderWidth = 0.5f;
                    clRFC.HorizontalAlignment = 1;

                    clSubtotal = new PdfPCell(new Phrase(ListFactrurasCRU[k].Subtotal.ToString(), _smallFont));
                    clSubtotal.BorderWidth = 0.5f;
                    clSubtotal.HorizontalAlignment = 1;

                    clIVA = new PdfPCell(new Phrase(ListFactrurasCRU[k].IVA.ToString(), _smallFont));
                    clIVA.BorderWidth = 0.5f;
                    clIVA.HorizontalAlignment = 1;

                    clTotal = new PdfPCell(new Phrase(ListFactrurasCRU[k].Total.ToString(), _smallFont));
                    clTotal.BorderWidth = 0.5f;
                    clTotal.HorizontalAlignment = 1;
                    ValorTotal += ListFactrurasCRU[k].Total;

                    clPendiente = new PdfPCell(new Phrase(ListFactrurasCRU[k].Pendiente.ToString(), _smallFont));
                    clPendiente.BorderWidth = 0.5f;
                    clPendiente.HorizontalAlignment = 1;

                    clTextoExtra3 = new PdfPCell(new Phrase(ListFactrurasCRU[k].TextoExtra3, _smallFont));
                    clTextoExtra3.BorderWidth = 0.5f;
                    clTextoExtra3.HorizontalAlignment = 1;

                    clAfectado = new PdfPCell(new Phrase(ListFactrurasCRU[k].Afectado, _smallFont));
                    clAfectado.BorderWidth = 0.5f;
                    clAfectado.HorizontalAlignment = 1;

                    clImpreso = new PdfPCell(new Phrase(ListFactrurasCRU[k].Impreso, _smallFont));
                    clImpreso.BorderWidth = 0.5f;
                    clImpreso.HorizontalAlignment = 1;

                    clCancelado = new PdfPCell(new Phrase(ListFactrurasCRU[k].Cancelado, _smallFont));
                    clCancelado.BorderWidth = 0.5f;
                    clCancelado.HorizontalAlignment = 1;

                    clTotalUnidades = new PdfPCell(new Phrase(ListFactrurasCRU[k].TotalUnidades.ToString(), _smallFont));
                    clTotalUnidades.BorderWidth = 0.5f;
                    clTotalUnidades.HorizontalAlignment = 1;

                    clClasificacionCliente2 = new PdfPCell(new Phrase(ListFactrurasCRU[k].proveedor.Clasificación2, _smallFont));
                    clClasificacionCliente2.BorderWidth = 0.5f;
                    clClasificacionCliente2.HorizontalAlignment = 1;

                    clTextoExtra1 = new PdfPCell(new Phrase(ListFactrurasCRU[k].TextoExtra1, _smallFont));
                    clTextoExtra1.BorderWidth = 0.5f;
                    clTextoExtra1.HorizontalAlignment = 1;

                    clNombreConcepto = new PdfPCell(new Phrase(ListFactrurasCRU[k].NombreConcepto, _smallFont));
                    clNombreConcepto.BorderWidth = 0.5f;
                    clNombreConcepto.HorizontalAlignment = 1;
                    #endregion
                    #region Agrega titulos en las tablas
                    //agrega las tablas en el pdf
                    tabla_cuentas.AddCell(clFecha);
                    tabla_cuentas.AddCell(clSerie);
                    tabla_cuentas.AddCell(clFolio);
                    tabla_cuentas.AddCell(clNombreAgente);
                    tabla_cuentas.AddCell(clRazonSocial);
                    tabla_cuentas.AddCell(clFechaVencimiento);
                    tabla_cuentas.AddCell(clRFC);
                    tabla_cuentas.AddCell(clSubtotal);
                    tabla_cuentas.AddCell(clIVA);
                    tabla_cuentas.AddCell(clTotal);
                    tabla_cuentas.AddCell(clPendiente);
                    tabla_cuentas.AddCell(clTextoExtra3);
                    tabla_cuentas.AddCell(clAfectado);
                    tabla_cuentas.AddCell(clImpreso);
                    tabla_cuentas.AddCell(clCancelado);
                    tabla_cuentas.AddCell(clTotalUnidades);
                    tabla_cuentas.AddCell(clClasificacionCliente2);
                    tabla_cuentas.AddCell(clTextoExtra1);
                    tabla_cuentas.AddCell(clNombreConcepto);
                    #endregion
                }//fin for

                doc.Add(new Paragraph("Total: $" + Math.Round(ValorTotal, 2), _titulos));
                doc.Add(new Paragraph("\n"));

                //agrego la tabla al pdf
                doc.Add(tabla_cuentas);

                #endregion
                doc.Add(new Paragraph("\n"));
                doc.Add(new Paragraph("\n"));
                doc.Add(new Paragraph("\n"));

                /************************************************************/
                #region **********SEGUNDA TABLA  filtro por RFC publico*********
                doc.Add(new Paragraph("24 Acumulado de Abonos en  Público ", _titulos));
                doc.Add(new Paragraph("\n"));
                tabla_cuentas = new PdfPTable(19);
                //PdfPCell cell = new PdfPCell(new Phrase("Reporte de Compras"));
                //cell.Colspan = 3;
                //cell.BackgroundColor = BaseColor.BLUE;
                //cell.HorizontalAlignment = 1;//0=Left, 1=Centre, 2=Right 
                tabla_cuentas.WidthPercentage = 100;
                //tabla_cuentas.AddCell(cell);



                #region configuracion de columnas
                // Configuramos el título de las columnas de la tabla 
                clFecha = new PdfPCell(new Phrase("Fecha", _standardFont));
                clFecha.BorderWidth = 0.5f;
                clFecha.BorderWidthBottom = 0.5f;
                clFecha.HorizontalAlignment = 1;

                clSerie = new PdfPCell(new Phrase("Serie", _standardFont));
                clSerie.BorderWidth = 0.5f;
                clSerie.BorderWidthBottom = 0.5f;
                clSerie.HorizontalAlignment = 1;

                clFolio = new PdfPCell(new Phrase("Folio", _standardFont));
                clFolio.BorderWidth = 0.5f;
                clFolio.BorderWidthBottom = 0.5f;
                clFolio.HorizontalAlignment = 1;

                clNombreAgente = new PdfPCell(new Phrase("Nombre del Agente", _standardFont));
                clNombreAgente.BorderWidth = 0.5f;
                clNombreAgente.BorderWidthBottom = 0.5f;
                clNombreAgente.HorizontalAlignment = 1;

                clRazonSocial = new PdfPCell(new Phrase("Razón Social", _standardFont));
                clRazonSocial.BorderWidth = 0.5f;
                clRazonSocial.BorderWidthBottom = 0.5f;
                clRazonSocial.HorizontalAlignment = 1;

                clFechaVencimiento = new PdfPCell(new Phrase("Fecha de Vencimiento", _standardFont));
                clFechaVencimiento.BorderWidth = 0.5f;
                clFechaVencimiento.BorderWidthBottom = 0.5f;
                clFechaVencimiento.HorizontalAlignment = 1;

                clRFC = new PdfPCell(new Phrase("R.F.C.", _standardFont));
                clRFC.BorderWidth = 0.5f;
                clRFC.BorderWidthBottom = 0.5f;
                clRFC.HorizontalAlignment = 1;

                clSubtotal = new PdfPCell(new Phrase("Subtotal", _standardFont));
                clSubtotal.BorderWidth = 0.5f;
                clSubtotal.BorderWidthBottom = 0.5f;
                clSubtotal.HorizontalAlignment = 1;

                clIVA = new PdfPCell(new Phrase("IVA", _standardFont));
                clIVA.BorderWidth = 0.5f;
                clIVA.BorderWidthBottom = 0.5f;
                clIVA.HorizontalAlignment = 1;

                clTotal = new PdfPCell(new Phrase("Total", _standardFont));
                clTotal.BorderWidth = 0.5f;
                clTotal.BorderWidthBottom = 0.5f;
                clTotal.HorizontalAlignment = 1;

                clPendiente = new PdfPCell(new Phrase("Pendiente", _standardFont));
                clPendiente.BorderWidth = 0.5f;
                clPendiente.BorderWidthBottom = 0.5f;
                clPendiente.HorizontalAlignment = 1;

                clTextoExtra3 = new PdfPCell(new Phrase("Texto Extra 3", _standardFont));
                clTextoExtra3.BorderWidth = 0.5f;
                clTextoExtra3.BorderWidthBottom = 0.5f;
                clTextoExtra3.HorizontalAlignment = 1;

                clAfectado = new PdfPCell(new Phrase("Afectado", _standardFont));
                clAfectado.BorderWidth = 0.5f;
                clAfectado.BorderWidthBottom = 0.5f;
                clAfectado.HorizontalAlignment = 1;

                clImpreso = new PdfPCell(new Phrase("Impreso", _standardFont));
                clImpreso.BorderWidth = 0.5f;
                clImpreso.BorderWidthBottom = 0.5f;
                clImpreso.HorizontalAlignment = 1;

                clCancelado = new PdfPCell(new Phrase("Cancelado", _standardFont));
                clCancelado.BorderWidth = 0.5f;
                clCancelado.BorderWidthBottom = 0.5f;
                clCancelado.HorizontalAlignment = 1;

                clTotalUnidades = new PdfPCell(new Phrase("Total de Unidades", _standardFont));
                clTotalUnidades.BorderWidth = 0.5f;
                clTotalUnidades.BorderWidthBottom = 0.5f;
                clTotalUnidades.HorizontalAlignment = 1;

                clClasificacionCliente2 = new PdfPCell(new Phrase("Clasificación Cliente 2", _standardFont));
                clClasificacionCliente2.BorderWidth = 0.5f;
                clClasificacionCliente2.BorderWidthBottom = 0.5f;
                clClasificacionCliente2.HorizontalAlignment = 1;

                clTextoExtra1 = new PdfPCell(new Phrase("Texto Extra 1", _standardFont));
                clTextoExtra1.BorderWidth = 0.5f;
                clTextoExtra1.BorderWidthBottom = 0.5f;
                clTextoExtra1.HorizontalAlignment = 1;

                clNombreConcepto = new PdfPCell(new Phrase("Nombre del Concepto", _standardFont));
                clNombreConcepto.BorderWidth = 0.5f;
                clNombreConcepto.BorderWidthBottom = 0.5f;
                clNombreConcepto.HorizontalAlignment = 1;
                #endregion
                //***************************************************************************************************************************************
                #region Agrega titulos en las tablas
                //agrega las tablas en el pdf
                tabla_cuentas.AddCell(clFecha);
                tabla_cuentas.AddCell(clSerie);
                tabla_cuentas.AddCell(clFolio);
                tabla_cuentas.AddCell(clNombreAgente);
                tabla_cuentas.AddCell(clRazonSocial);
                tabla_cuentas.AddCell(clFechaVencimiento);
                tabla_cuentas.AddCell(clRFC);
                tabla_cuentas.AddCell(clSubtotal);
                tabla_cuentas.AddCell(clIVA);
                tabla_cuentas.AddCell(clTotal);
                tabla_cuentas.AddCell(clPendiente);
                tabla_cuentas.AddCell(clTextoExtra3);
                tabla_cuentas.AddCell(clAfectado);
                tabla_cuentas.AddCell(clImpreso);
                tabla_cuentas.AddCell(clCancelado);
                tabla_cuentas.AddCell(clTotalUnidades);
                tabla_cuentas.AddCell(clClasificacionCliente2);
                tabla_cuentas.AddCell(clTextoExtra1);
                tabla_cuentas.AddCell(clNombreConcepto);
                #endregion
                ListFactrurasCRU = ListFactrurasCRUFiltroRFCPublico;
                ValorTotal = 0;
                for (int k = 0; k < ListFactrurasCRU.Count; k++)
                {
                    #region AGREGA DATOS EN LA TABLA
                    clFecha = new PdfPCell(new Phrase(ListFactrurasCRU[k].Fecha, _smallFont));
                    clFecha.BorderWidth = 0.5f;
                    clFecha.HorizontalAlignment = 1;

                    clSerie = new PdfPCell(new Phrase(ListFactrurasCRU[k].Serie, _smallFont));
                    clSerie.BorderWidth = 0.5f;
                    clSerie.HorizontalAlignment = 1;

                    clFolio = new PdfPCell(new Phrase(ListFactrurasCRU[k].Folio, _smallFont));
                    clFolio.BorderWidth = 0.5f;
                    clFolio.HorizontalAlignment = 1;

                    clNombreAgente = new PdfPCell(new Phrase(ListFactrurasCRU[k].NombreAgente, _smallFont));
                    clNombreAgente.BorderWidth = 0.5f;
                    clNombreAgente.HorizontalAlignment = 1;

                    clRazonSocial = new PdfPCell(new Phrase(ListFactrurasCRU[k].RazonSocial, _smallFont));
                    clRazonSocial.BorderWidth = 0.5f;
                    clRazonSocial.HorizontalAlignment = 1;

                    clFechaVencimiento = new PdfPCell(new Phrase(ListFactrurasCRU[k].FechaVencimiento, _smallFont));
                    clFechaVencimiento.BorderWidth = 0.5f;
                    clFechaVencimiento.HorizontalAlignment = 1;

                    clRFC = new PdfPCell(new Phrase(ListFactrurasCRU[k].RFC, _smallFont));
                    clRFC.BorderWidth = 0.5f;
                    clRFC.HorizontalAlignment = 1;

                    clSubtotal = new PdfPCell(new Phrase(ListFactrurasCRU[k].Subtotal.ToString(), _smallFont));
                    clSubtotal.BorderWidth = 0.5f;
                    clSubtotal.HorizontalAlignment = 1;

                    clIVA = new PdfPCell(new Phrase(ListFactrurasCRU[k].IVA.ToString(), _smallFont));
                    clIVA.BorderWidth = 0.5f;
                    clIVA.HorizontalAlignment = 1;

                    clTotal = new PdfPCell(new Phrase(ListFactrurasCRU[k].Total.ToString(), _smallFont));
                    clTotal.BorderWidth = 0.5f;
                    clTotal.HorizontalAlignment = 1;
                    ValorTotal += ListFactrurasCRU[k].Total;

                    clPendiente = new PdfPCell(new Phrase(ListFactrurasCRU[k].Pendiente.ToString(), _smallFont));
                    clPendiente.BorderWidth = 0.5f;
                    clPendiente.HorizontalAlignment = 1;

                    clTextoExtra3 = new PdfPCell(new Phrase(ListFactrurasCRU[k].TextoExtra3, _smallFont));
                    clTextoExtra3.BorderWidth = 0.5f;
                    clTextoExtra3.HorizontalAlignment = 1;

                    clAfectado = new PdfPCell(new Phrase(ListFactrurasCRU[k].Afectado, _smallFont));
                    clAfectado.BorderWidth = 0.5f;
                    clAfectado.HorizontalAlignment = 1;

                    clImpreso = new PdfPCell(new Phrase(ListFactrurasCRU[k].Impreso, _smallFont));
                    clImpreso.BorderWidth = 0.5f;
                    clImpreso.HorizontalAlignment = 1;

                    clCancelado = new PdfPCell(new Phrase(ListFactrurasCRU[k].Cancelado, _smallFont));
                    clCancelado.BorderWidth = 0.5f;
                    clCancelado.HorizontalAlignment = 1;

                    clTotalUnidades = new PdfPCell(new Phrase(ListFactrurasCRU[k].TotalUnidades.ToString(), _smallFont));
                    clTotalUnidades.BorderWidth = 0.5f;
                    clTotalUnidades.HorizontalAlignment = 1;

                    clClasificacionCliente2 = new PdfPCell(new Phrase(ListFactrurasCRU[k].proveedor.Clasificación2, _smallFont));
                    clClasificacionCliente2.BorderWidth = 0.5f;
                    clClasificacionCliente2.HorizontalAlignment = 1;

                    clTextoExtra1 = new PdfPCell(new Phrase(ListFactrurasCRU[k].TextoExtra1, _smallFont));
                    clTextoExtra1.BorderWidth = 0.5f;
                    clTextoExtra1.HorizontalAlignment = 1;

                    clNombreConcepto = new PdfPCell(new Phrase(ListFactrurasCRU[k].NombreConcepto, _smallFont));
                    clNombreConcepto.BorderWidth = 0.5f;
                    clNombreConcepto.HorizontalAlignment = 1;
                    #endregion
                    #region Agrega titulos en las tablas
                    //agrega las tablas en el pdf
                    tabla_cuentas.AddCell(clFecha);
                    tabla_cuentas.AddCell(clSerie);
                    tabla_cuentas.AddCell(clFolio);
                    tabla_cuentas.AddCell(clNombreAgente);
                    tabla_cuentas.AddCell(clRazonSocial);
                    tabla_cuentas.AddCell(clFechaVencimiento);
                    tabla_cuentas.AddCell(clRFC);
                    tabla_cuentas.AddCell(clSubtotal);
                    tabla_cuentas.AddCell(clIVA);
                    tabla_cuentas.AddCell(clTotal);
                    tabla_cuentas.AddCell(clPendiente);
                    tabla_cuentas.AddCell(clTextoExtra3);
                    tabla_cuentas.AddCell(clAfectado);
                    tabla_cuentas.AddCell(clImpreso);
                    tabla_cuentas.AddCell(clCancelado);
                    tabla_cuentas.AddCell(clTotalUnidades);
                    tabla_cuentas.AddCell(clClasificacionCliente2);
                    tabla_cuentas.AddCell(clTextoExtra1);
                    tabla_cuentas.AddCell(clNombreConcepto);
                    #endregion
                }//fin for

                doc.Add(new Paragraph("Total: $" + Math.Round(ValorTotal, 2), _titulos));
                doc.Add(new Paragraph("\n"));

                //agrego la tabla al pdf
                doc.Add(tabla_cuentas);

                #endregion
                /******************************************************************************************/

                doc.Add(new Paragraph("\n"));
                doc.Add(new Paragraph("\n"));
                doc.Add(new Paragraph("\n"));

                /************************************************************/
                #region **********TERCER TABLA  FILTRO RFC OL*********
                doc.Add(new Paragraph("25 Acumulado de Abonos a OL ", _titulos));
                doc.Add(new Paragraph("\n"));
                tabla_cuentas = new PdfPTable(19);
                //PdfPCell cell = new PdfPCell(new Phrase("Reporte de Compras"));
                //cell.Colspan = 3;
                //cell.BackgroundColor = BaseColor.BLUE;
                //cell.HorizontalAlignment = 1;//0=Left, 1=Centre, 2=Right 
                tabla_cuentas.WidthPercentage = 100;
                //tabla_cuentas.AddCell(cell);



                #region configuracion de columnas
                // Configuramos el título de las columnas de la tabla 
                clFecha = new PdfPCell(new Phrase("Fecha", _standardFont));
                clFecha.BorderWidth = 0.5f;
                clFecha.BorderWidthBottom = 0.5f;
                clFecha.HorizontalAlignment = 1;

                clSerie = new PdfPCell(new Phrase("Serie", _standardFont));
                clSerie.BorderWidth = 0.5f;
                clSerie.BorderWidthBottom = 0.5f;
                clSerie.HorizontalAlignment = 1;

                clFolio = new PdfPCell(new Phrase("Folio", _standardFont));
                clFolio.BorderWidth = 0.5f;
                clFolio.BorderWidthBottom = 0.5f;
                clFolio.HorizontalAlignment = 1;

                clNombreAgente = new PdfPCell(new Phrase("Nombre del Agente", _standardFont));
                clNombreAgente.BorderWidth = 0.5f;
                clNombreAgente.BorderWidthBottom = 0.5f;
                clNombreAgente.HorizontalAlignment = 1;

                clRazonSocial = new PdfPCell(new Phrase("Razón Social", _standardFont));
                clRazonSocial.BorderWidth = 0.5f;
                clRazonSocial.BorderWidthBottom = 0.5f;
                clRazonSocial.HorizontalAlignment = 1;

                clFechaVencimiento = new PdfPCell(new Phrase("Fecha de Vencimiento", _standardFont));
                clFechaVencimiento.BorderWidth = 0.5f;
                clFechaVencimiento.BorderWidthBottom = 0.5f;
                clFechaVencimiento.HorizontalAlignment = 1;

                clRFC = new PdfPCell(new Phrase("R.F.C.", _standardFont));
                clRFC.BorderWidth = 0.5f;
                clRFC.BorderWidthBottom = 0.5f;
                clRFC.HorizontalAlignment = 1;

                clSubtotal = new PdfPCell(new Phrase("Subtotal", _standardFont));
                clSubtotal.BorderWidth = 0.5f;
                clSubtotal.BorderWidthBottom = 0.5f;
                clSubtotal.HorizontalAlignment = 1;

                clIVA = new PdfPCell(new Phrase("IVA", _standardFont));
                clIVA.BorderWidth = 0.5f;
                clIVA.BorderWidthBottom = 0.5f;
                clIVA.HorizontalAlignment = 1;

                clTotal = new PdfPCell(new Phrase("Total", _standardFont));
                clTotal.BorderWidth = 0.5f;
                clTotal.BorderWidthBottom = 0.5f;
                clTotal.HorizontalAlignment = 1;

                clPendiente = new PdfPCell(new Phrase("Pendiente", _standardFont));
                clPendiente.BorderWidth = 0.5f;
                clPendiente.BorderWidthBottom = 0.5f;
                clPendiente.HorizontalAlignment = 1;

                clTextoExtra3 = new PdfPCell(new Phrase("Texto Extra 3", _standardFont));
                clTextoExtra3.BorderWidth = 0.5f;
                clTextoExtra3.BorderWidthBottom = 0.5f;
                clTextoExtra3.HorizontalAlignment = 1;

                clAfectado = new PdfPCell(new Phrase("Afectado", _standardFont));
                clAfectado.BorderWidth = 0.5f;
                clAfectado.BorderWidthBottom = 0.5f;
                clAfectado.HorizontalAlignment = 1;

                clImpreso = new PdfPCell(new Phrase("Impreso", _standardFont));
                clImpreso.BorderWidth = 0.5f;
                clImpreso.BorderWidthBottom = 0.5f;
                clImpreso.HorizontalAlignment = 1;

                clCancelado = new PdfPCell(new Phrase("Cancelado", _standardFont));
                clCancelado.BorderWidth = 0.5f;
                clCancelado.BorderWidthBottom = 0.5f;
                clCancelado.HorizontalAlignment = 1;

                clTotalUnidades = new PdfPCell(new Phrase("Total de Unidades", _standardFont));
                clTotalUnidades.BorderWidth = 0.5f;
                clTotalUnidades.BorderWidthBottom = 0.5f;
                clTotalUnidades.HorizontalAlignment = 1;

                clClasificacionCliente2 = new PdfPCell(new Phrase("Clasificación Cliente 2", _standardFont));
                clClasificacionCliente2.BorderWidth = 0.5f;
                clClasificacionCliente2.BorderWidthBottom = 0.5f;
                clClasificacionCliente2.HorizontalAlignment = 1;

                clTextoExtra1 = new PdfPCell(new Phrase("Texto Extra 1", _standardFont));
                clTextoExtra1.BorderWidth = 0.5f;
                clTextoExtra1.BorderWidthBottom = 0.5f;
                clTextoExtra1.HorizontalAlignment = 1;

                clNombreConcepto = new PdfPCell(new Phrase("Nombre del Concepto", _standardFont));
                clNombreConcepto.BorderWidth = 0.5f;
                clNombreConcepto.BorderWidthBottom = 0.5f;
                clNombreConcepto.HorizontalAlignment = 1;
                #endregion
                //***************************************************************************************************************************************
                #region Agrega titulos en las tablas
                //agrega las tablas en el pdf
                tabla_cuentas.AddCell(clFecha);
                tabla_cuentas.AddCell(clSerie);
                tabla_cuentas.AddCell(clFolio);
                tabla_cuentas.AddCell(clNombreAgente);
                tabla_cuentas.AddCell(clRazonSocial);
                tabla_cuentas.AddCell(clFechaVencimiento);
                tabla_cuentas.AddCell(clRFC);
                tabla_cuentas.AddCell(clSubtotal);
                tabla_cuentas.AddCell(clIVA);
                tabla_cuentas.AddCell(clTotal);
                tabla_cuentas.AddCell(clPendiente);
                tabla_cuentas.AddCell(clTextoExtra3);
                tabla_cuentas.AddCell(clAfectado);
                tabla_cuentas.AddCell(clImpreso);
                tabla_cuentas.AddCell(clCancelado);
                tabla_cuentas.AddCell(clTotalUnidades);
                tabla_cuentas.AddCell(clClasificacionCliente2);
                tabla_cuentas.AddCell(clTextoExtra1);
                tabla_cuentas.AddCell(clNombreConcepto);
                #endregion
                ListFactrurasCRU = ListFactrurasCRUFiltroRFCOL;
                ValorTotal = 0;
                for (int k = 0; k < ListFactrurasCRU.Count; k++)
                {
                    #region AGREGA DATOS EN LA TABLA
                    clFecha = new PdfPCell(new Phrase(ListFactrurasCRU[k].Fecha, _smallFont));
                    clFecha.BorderWidth = 0.5f;
                    clFecha.HorizontalAlignment = 1;

                    clSerie = new PdfPCell(new Phrase(ListFactrurasCRU[k].Serie, _smallFont));
                    clSerie.BorderWidth = 0.5f;
                    clSerie.HorizontalAlignment = 1;

                    clFolio = new PdfPCell(new Phrase(ListFactrurasCRU[k].Folio, _smallFont));
                    clFolio.BorderWidth = 0.5f;
                    clFolio.HorizontalAlignment = 1;

                    clNombreAgente = new PdfPCell(new Phrase(ListFactrurasCRU[k].NombreAgente, _smallFont));
                    clNombreAgente.BorderWidth = 0.5f;
                    clNombreAgente.HorizontalAlignment = 1;

                    clRazonSocial = new PdfPCell(new Phrase(ListFactrurasCRU[k].RazonSocial, _smallFont));
                    clRazonSocial.BorderWidth = 0.5f;
                    clRazonSocial.HorizontalAlignment = 1;

                    clFechaVencimiento = new PdfPCell(new Phrase(ListFactrurasCRU[k].FechaVencimiento, _smallFont));
                    clFechaVencimiento.BorderWidth = 0.5f;
                    clFechaVencimiento.HorizontalAlignment = 1;

                    clRFC = new PdfPCell(new Phrase(ListFactrurasCRU[k].RFC, _smallFont));
                    clRFC.BorderWidth = 0.5f;
                    clRFC.HorizontalAlignment = 1;

                    clSubtotal = new PdfPCell(new Phrase(ListFactrurasCRU[k].Subtotal.ToString(), _smallFont));
                    clSubtotal.BorderWidth = 0.5f;
                    clSubtotal.HorizontalAlignment = 1;

                    clIVA = new PdfPCell(new Phrase(ListFactrurasCRU[k].IVA.ToString(), _smallFont));
                    clIVA.BorderWidth = 0.5f;
                    clIVA.HorizontalAlignment = 1;

                    clTotal = new PdfPCell(new Phrase(ListFactrurasCRU[k].Total.ToString(), _smallFont));
                    clTotal.BorderWidth = 0.5f;
                    clTotal.HorizontalAlignment = 1;
                    ValorTotal += ListFactrurasCRU[k].Total;

                    clPendiente = new PdfPCell(new Phrase(ListFactrurasCRU[k].Pendiente.ToString(), _smallFont));
                    clPendiente.BorderWidth = 0.5f;
                    clPendiente.HorizontalAlignment = 1;

                    clTextoExtra3 = new PdfPCell(new Phrase(ListFactrurasCRU[k].TextoExtra3, _smallFont));
                    clTextoExtra3.BorderWidth = 0.5f;
                    clTextoExtra3.HorizontalAlignment = 1;

                    clAfectado = new PdfPCell(new Phrase(ListFactrurasCRU[k].Afectado, _smallFont));
                    clAfectado.BorderWidth = 0.5f;
                    clAfectado.HorizontalAlignment = 1;

                    clImpreso = new PdfPCell(new Phrase(ListFactrurasCRU[k].Impreso, _smallFont));
                    clImpreso.BorderWidth = 0.5f;
                    clImpreso.HorizontalAlignment = 1;

                    clCancelado = new PdfPCell(new Phrase(ListFactrurasCRU[k].Cancelado, _smallFont));
                    clCancelado.BorderWidth = 0.5f;
                    clCancelado.HorizontalAlignment = 1;

                    clTotalUnidades = new PdfPCell(new Phrase(ListFactrurasCRU[k].TotalUnidades.ToString(), _smallFont));
                    clTotalUnidades.BorderWidth = 0.5f;
                    clTotalUnidades.HorizontalAlignment = 1;

                    clClasificacionCliente2 = new PdfPCell(new Phrase(ListFactrurasCRU[k].proveedor.Clasificación2, _smallFont));
                    clClasificacionCliente2.BorderWidth = 0.5f;
                    clClasificacionCliente2.HorizontalAlignment = 1;

                    clTextoExtra1 = new PdfPCell(new Phrase(ListFactrurasCRU[k].TextoExtra1, _smallFont));
                    clTextoExtra1.BorderWidth = 0.5f;
                    clTextoExtra1.HorizontalAlignment = 1;

                    clNombreConcepto = new PdfPCell(new Phrase(ListFactrurasCRU[k].NombreConcepto, _smallFont));
                    clNombreConcepto.BorderWidth = 0.5f;
                    clNombreConcepto.HorizontalAlignment = 1;
                    #endregion
                    #region Agrega titulos en las tablas
                    //agrega las tablas en el pdf
                    tabla_cuentas.AddCell(clFecha);
                    tabla_cuentas.AddCell(clSerie);
                    tabla_cuentas.AddCell(clFolio);
                    tabla_cuentas.AddCell(clNombreAgente);
                    tabla_cuentas.AddCell(clRazonSocial);
                    tabla_cuentas.AddCell(clFechaVencimiento);
                    tabla_cuentas.AddCell(clRFC);
                    tabla_cuentas.AddCell(clSubtotal);
                    tabla_cuentas.AddCell(clIVA);
                    tabla_cuentas.AddCell(clTotal);
                    tabla_cuentas.AddCell(clPendiente);
                    tabla_cuentas.AddCell(clTextoExtra3);
                    tabla_cuentas.AddCell(clAfectado);
                    tabla_cuentas.AddCell(clImpreso);
                    tabla_cuentas.AddCell(clCancelado);
                    tabla_cuentas.AddCell(clTotalUnidades);
                    tabla_cuentas.AddCell(clClasificacionCliente2);
                    tabla_cuentas.AddCell(clTextoExtra1);
                    tabla_cuentas.AddCell(clNombreConcepto);
                    #endregion
                }//fin for

                doc.Add(new Paragraph("Total: $" + Math.Round(ValorTotal, 2), _titulos));
                doc.Add(new Paragraph("\n"));

                //agrego la tabla al pdf
                doc.Add(tabla_cuentas);

                #endregion
                /******************************************************************************************/
                // cierro la edicion del pdf
                doc.Close();

                ////LO EJECUTO
                Process prc = new System.Diagnostics.Process();
                prc.StartInfo.FileName = rut;
                prc.Start();

            }
            catch (Exception g)
            { MessageBox.Show("" + g.Message); }
        }
        #endregion



        #region CRU Compras
        /// <summary>
        /// Método para imprimir las facturas de CRU
        /// </summary>
        /// <param name="ListFactrurasCRU">lista de las facturas que se van a imprimir</param>
        public void ImpresionCRUCompras(List<Tipos_Datos_CRU.FacturasCRU> ListFactrurasCRU, string fechas, string path, List<Tipos_Datos_CRU.FacturasCRU> ListFactrurasCRUFiltroRFCPublico)
        {

            try
            {

                Document doc = new Document(PageSize.TABLOID, 10, 10, 10, 10);//Creacion del documento configuracion de tipo de hoja y margenes
                doc.AddAuthor("Indicadores");//Autor del PDF
                doc.AddKeywords("pdf, PdfWriter; Indicadores V1");

                //para almacenamiento del archivo
                string nombre_archivo = "ComprasCRU.PDF";//Nombre del Archivo
                string rut = @path + nombre_archivo;
                PdfWriter wri = PdfWriter.GetInstance(doc, new FileStream(rut, FileMode.Create));
                doc.AddTitle("REPORTE");
                doc.AddCreator("*********");
                doc.Open();
                //tipo de letras que se pueda usar en el archivo PDF
                iTextSharp.text.Font _mediumFont = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 8, iTextSharp.text.Font.NORMAL, BaseColor.BLACK);
                iTextSharp.text.Font _standardFont = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 9, iTextSharp.text.Font.BOLD, BaseColor.BLACK);
                iTextSharp.text.Font _standardFont1 = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 14, iTextSharp.text.Font.BOLD, BaseColor.WHITE);
                iTextSharp.text.Font _smallFont = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 8, iTextSharp.text.Font.NORMAL, BaseColor.BLACK);
                iTextSharp.text.Font _titulo = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 14, iTextSharp.text.Font.NORMAL, BaseColor.BLACK);
                iTextSharp.text.Font _titulos = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 12, iTextSharp.text.Font.NORMAL, BaseColor.BLACK);


                // Cabecera
                doc.Add(new Paragraph(" Desglose general de Compras CRU " + fechas, _titulo));
                doc.Add(new Paragraph("\n"));
                doc.Add(new Paragraph("\n"));

                //***********************************************
                #region **********PRIMERA TABLA 26 Acumulado de Compras en CRU*********
                doc.Add(new Paragraph("26 Acumulado de Compras en CRU", _titulos));
                doc.Add(new Paragraph("\n"));
                PdfPTable tabla_cuentas = new PdfPTable(19);
                //PdfPCell cell = new PdfPCell(new Phrase("Reporte de Compras"));
                //cell.Colspan = 3;
                //cell.BackgroundColor = BaseColor.BLUE;
                //cell.HorizontalAlignment = 1;//0=Left, 1=Centre, 2=Right 
                tabla_cuentas.WidthPercentage = 100;
                //tabla_cuentas.AddCell(cell);



                #region configuracion de columnas
                // Configuramos el título de las columnas de la tabla 
                PdfPCell clFecha = new PdfPCell(new Phrase("Fecha", _standardFont));
                clFecha.BorderWidth = 0.5f;
                clFecha.BorderWidthBottom = 0.5f;
                clFecha.HorizontalAlignment = 1;

                PdfPCell clSerie = new PdfPCell(new Phrase("Serie", _standardFont));
                clSerie.BorderWidth = 0.5f;
                clSerie.BorderWidthBottom = 0.5f;
                clSerie.HorizontalAlignment = 1;

                PdfPCell clFolio = new PdfPCell(new Phrase("Folio", _standardFont));
                clFolio.BorderWidth = 0.5f;
                clFolio.BorderWidthBottom = 0.5f;
                clFolio.HorizontalAlignment = 1;

                PdfPCell clNombreAgente = new PdfPCell(new Phrase("Nombre del Agente", _standardFont));
                clNombreAgente.BorderWidth = 0.5f;
                clNombreAgente.BorderWidthBottom = 0.5f;
                clNombreAgente.HorizontalAlignment = 1;

                PdfPCell clRazonSocial = new PdfPCell(new Phrase("Razón Social", _standardFont));
                clRazonSocial.BorderWidth = 0.5f;
                clRazonSocial.BorderWidthBottom = 0.5f;
                clRazonSocial.HorizontalAlignment = 1;

                PdfPCell clFechaVencimiento = new PdfPCell(new Phrase("Fecha de Vencimiento", _standardFont));
                clFechaVencimiento.BorderWidth = 0.5f;
                clFechaVencimiento.BorderWidthBottom = 0.5f;
                clFechaVencimiento.HorizontalAlignment = 1;

                PdfPCell clRFC = new PdfPCell(new Phrase("R.F.C.", _standardFont));
                clRFC.BorderWidth = 0.5f;
                clRFC.BorderWidthBottom = 0.5f;
                clRFC.HorizontalAlignment = 1;

                PdfPCell clSubtotal = new PdfPCell(new Phrase("Subtotal", _standardFont));
                clSubtotal.BorderWidth = 0.5f;
                clSubtotal.BorderWidthBottom = 0.5f;
                clSubtotal.HorizontalAlignment = 1;

                PdfPCell clIVA = new PdfPCell(new Phrase("IVA", _standardFont));
                clIVA.BorderWidth = 0.5f;
                clIVA.BorderWidthBottom = 0.5f;
                clIVA.HorizontalAlignment = 1;

                PdfPCell clTotal = new PdfPCell(new Phrase("Total", _standardFont));
                clTotal.BorderWidth = 0.5f;
                clTotal.BorderWidthBottom = 0.5f;
                clTotal.HorizontalAlignment = 1;

                PdfPCell clPendiente = new PdfPCell(new Phrase("Pendiente", _standardFont));
                clPendiente.BorderWidth = 0.5f;
                clPendiente.BorderWidthBottom = 0.5f;
                clPendiente.HorizontalAlignment = 1;

                PdfPCell clTextoExtra3 = new PdfPCell(new Phrase("Texto Extra 3", _standardFont));
                clTextoExtra3.BorderWidth = 0.5f;
                clTextoExtra3.BorderWidthBottom = 0.5f;
                clTextoExtra3.HorizontalAlignment = 1;

                PdfPCell clAfectado = new PdfPCell(new Phrase("Afectado", _standardFont));
                clAfectado.BorderWidth = 0.5f;
                clAfectado.BorderWidthBottom = 0.5f;
                clAfectado.HorizontalAlignment = 1;

                PdfPCell clImpreso = new PdfPCell(new Phrase("Impreso", _standardFont));
                clImpreso.BorderWidth = 0.5f;
                clImpreso.BorderWidthBottom = 0.5f;
                clImpreso.HorizontalAlignment = 1;

                PdfPCell clCancelado = new PdfPCell(new Phrase("Cancelado", _standardFont));
                clCancelado.BorderWidth = 0.5f;
                clCancelado.BorderWidthBottom = 0.5f;
                clCancelado.HorizontalAlignment = 1;

                PdfPCell clTotalUnidades = new PdfPCell(new Phrase("Total de Unidades", _standardFont));
                clTotalUnidades.BorderWidth = 0.5f;
                clTotalUnidades.BorderWidthBottom = 0.5f;
                clTotalUnidades.HorizontalAlignment = 1;

                PdfPCell clClasificacionCliente2 = new PdfPCell(new Phrase("Clasificación Cliente 2", _standardFont));
                clClasificacionCliente2.BorderWidth = 0.5f;
                clClasificacionCliente2.BorderWidthBottom = 0.5f;
                clClasificacionCliente2.HorizontalAlignment = 1;

                PdfPCell clTextoExtra1 = new PdfPCell(new Phrase("Texto Extra 1", _standardFont));
                clTextoExtra1.BorderWidth = 0.5f;
                clTextoExtra1.BorderWidthBottom = 0.5f;
                clTextoExtra1.HorizontalAlignment = 1;

                PdfPCell clNombreConcepto = new PdfPCell(new Phrase("Nombre del Concepto", _standardFont));
                clNombreConcepto.BorderWidth = 0.5f;
                clNombreConcepto.BorderWidthBottom = 0.5f;
                clNombreConcepto.HorizontalAlignment = 1;
                #endregion
                //***************************************************************************************************************************************
                #region Agrega titulos en las tablas
                //agrega las tablas en el pdf
                tabla_cuentas.AddCell(clFecha);
                tabla_cuentas.AddCell(clSerie);
                tabla_cuentas.AddCell(clFolio);
                tabla_cuentas.AddCell(clNombreAgente);
                tabla_cuentas.AddCell(clRazonSocial);
                tabla_cuentas.AddCell(clFechaVencimiento);
                tabla_cuentas.AddCell(clRFC);
                tabla_cuentas.AddCell(clSubtotal);
                tabla_cuentas.AddCell(clIVA);
                tabla_cuentas.AddCell(clTotal);
                tabla_cuentas.AddCell(clPendiente);
                tabla_cuentas.AddCell(clTextoExtra3);
                tabla_cuentas.AddCell(clAfectado);
                tabla_cuentas.AddCell(clImpreso);
                tabla_cuentas.AddCell(clCancelado);
                tabla_cuentas.AddCell(clTotalUnidades);
                tabla_cuentas.AddCell(clClasificacionCliente2);
                tabla_cuentas.AddCell(clTextoExtra1);
                tabla_cuentas.AddCell(clNombreConcepto);
                #endregion

                double ValorTotal = 0;
                for (int k = 0; k < ListFactrurasCRU.Count; k++)
                {
                    #region AGREGA DATOS EN LA TABLA
                    clFecha = new PdfPCell(new Phrase(ListFactrurasCRU[k].Fecha, _smallFont));
                    clFecha.BorderWidth = 0.5f;
                    clFecha.HorizontalAlignment = 1;

                    clSerie = new PdfPCell(new Phrase(ListFactrurasCRU[k].Serie, _smallFont));
                    clSerie.BorderWidth = 0.5f;
                    clSerie.HorizontalAlignment = 1;

                    clFolio = new PdfPCell(new Phrase(ListFactrurasCRU[k].Folio, _smallFont));
                    clFolio.BorderWidth = 0.5f;
                    clFolio.HorizontalAlignment = 1;

                    clNombreAgente = new PdfPCell(new Phrase(ListFactrurasCRU[k].NombreAgente, _smallFont));
                    clNombreAgente.BorderWidth = 0.5f;
                    clNombreAgente.HorizontalAlignment = 1;

                    clRazonSocial = new PdfPCell(new Phrase(ListFactrurasCRU[k].RazonSocial, _smallFont));
                    clRazonSocial.BorderWidth = 0.5f;
                    clRazonSocial.HorizontalAlignment = 1;

                    clFechaVencimiento = new PdfPCell(new Phrase(ListFactrurasCRU[k].FechaVencimiento, _smallFont));
                    clFechaVencimiento.BorderWidth = 0.5f;
                    clFechaVencimiento.HorizontalAlignment = 1;

                    clRFC = new PdfPCell(new Phrase(ListFactrurasCRU[k].RFC, _smallFont));
                    clRFC.BorderWidth = 0.5f;
                    clRFC.HorizontalAlignment = 1;

                    clSubtotal = new PdfPCell(new Phrase(ListFactrurasCRU[k].Subtotal.ToString(), _smallFont));
                    clSubtotal.BorderWidth = 0.5f;
                    clSubtotal.HorizontalAlignment = 1;

                    clIVA = new PdfPCell(new Phrase(ListFactrurasCRU[k].IVA.ToString(), _smallFont));
                    clIVA.BorderWidth = 0.5f;
                    clIVA.HorizontalAlignment = 1;

                    clTotal = new PdfPCell(new Phrase(ListFactrurasCRU[k].Total.ToString(), _smallFont));
                    clTotal.BorderWidth = 0.5f;
                    clTotal.HorizontalAlignment = 1;
                    ValorTotal += ListFactrurasCRU[k].Total;

                    clPendiente = new PdfPCell(new Phrase(ListFactrurasCRU[k].Pendiente.ToString(), _smallFont));
                    clPendiente.BorderWidth = 0.5f;
                    clPendiente.HorizontalAlignment = 1;

                    clTextoExtra3 = new PdfPCell(new Phrase(ListFactrurasCRU[k].TextoExtra3, _smallFont));
                    clTextoExtra3.BorderWidth = 0.5f;
                    clTextoExtra3.HorizontalAlignment = 1;

                    clAfectado = new PdfPCell(new Phrase(ListFactrurasCRU[k].Afectado, _smallFont));
                    clAfectado.BorderWidth = 0.5f;
                    clAfectado.HorizontalAlignment = 1;

                    clImpreso = new PdfPCell(new Phrase(ListFactrurasCRU[k].Impreso, _smallFont));
                    clImpreso.BorderWidth = 0.5f;
                    clImpreso.HorizontalAlignment = 1;

                    clCancelado = new PdfPCell(new Phrase(ListFactrurasCRU[k].Cancelado, _smallFont));
                    clCancelado.BorderWidth = 0.5f;
                    clCancelado.HorizontalAlignment = 1;

                    clTotalUnidades = new PdfPCell(new Phrase(ListFactrurasCRU[k].TotalUnidades.ToString(), _smallFont));
                    clTotalUnidades.BorderWidth = 0.5f;
                    clTotalUnidades.HorizontalAlignment = 1;

                    clClasificacionCliente2 = new PdfPCell(new Phrase(ListFactrurasCRU[k].proveedor.Clasificación2, _smallFont));
                    clClasificacionCliente2.BorderWidth = 0.5f;
                    clClasificacionCliente2.HorizontalAlignment = 1;

                    clTextoExtra1 = new PdfPCell(new Phrase(ListFactrurasCRU[k].TextoExtra1, _smallFont));
                    clTextoExtra1.BorderWidth = 0.5f;
                    clTextoExtra1.HorizontalAlignment = 1;

                    clNombreConcepto = new PdfPCell(new Phrase(ListFactrurasCRU[k].NombreConcepto, _smallFont));
                    clNombreConcepto.BorderWidth = 0.5f;
                    clNombreConcepto.HorizontalAlignment = 1;
                    #endregion
                    #region Agrega titulos en las tablas
                    //agrega las tablas en el pdf
                    tabla_cuentas.AddCell(clFecha);
                    tabla_cuentas.AddCell(clSerie);
                    tabla_cuentas.AddCell(clFolio);
                    tabla_cuentas.AddCell(clNombreAgente);
                    tabla_cuentas.AddCell(clRazonSocial);
                    tabla_cuentas.AddCell(clFechaVencimiento);
                    tabla_cuentas.AddCell(clRFC);
                    tabla_cuentas.AddCell(clSubtotal);
                    tabla_cuentas.AddCell(clIVA);
                    tabla_cuentas.AddCell(clTotal);
                    tabla_cuentas.AddCell(clPendiente);
                    tabla_cuentas.AddCell(clTextoExtra3);
                    tabla_cuentas.AddCell(clAfectado);
                    tabla_cuentas.AddCell(clImpreso);
                    tabla_cuentas.AddCell(clCancelado);
                    tabla_cuentas.AddCell(clTotalUnidades);
                    tabla_cuentas.AddCell(clClasificacionCliente2);
                    tabla_cuentas.AddCell(clTextoExtra1);
                    tabla_cuentas.AddCell(clNombreConcepto);
                    #endregion
                }//fin for

                doc.Add(new Paragraph("Total: $" + Math.Round(ValorTotal, 2), _titulos));
                doc.Add(new Paragraph("\n"));

                //agrego la tabla al pdf
                doc.Add(tabla_cuentas);

                #endregion
                doc.Add(new Paragraph("\n"));
                doc.Add(new Paragraph("\n"));
                doc.Add(new Paragraph("\n"));

                /************************************************************/
                #region **********SEGUNDA TABLA  filtro por RFC ANJI*********
                doc.Add(new Paragraph("27 Acumulado de Compras en CRU a ANJI", _titulos));
                doc.Add(new Paragraph("\n"));
                tabla_cuentas = new PdfPTable(19);
                //PdfPCell cell = new PdfPCell(new Phrase("Reporte de Compras"));
                //cell.Colspan = 3;
                //cell.BackgroundColor = BaseColor.BLUE;
                //cell.HorizontalAlignment = 1;//0=Left, 1=Centre, 2=Right 
                tabla_cuentas.WidthPercentage = 100;
                //tabla_cuentas.AddCell(cell);



                #region configuracion de columnas
                // Configuramos el título de las columnas de la tabla 
                clFecha = new PdfPCell(new Phrase("Fecha", _standardFont));
                clFecha.BorderWidth = 0.5f;
                clFecha.BorderWidthBottom = 0.5f;
                clFecha.HorizontalAlignment = 1;

                clSerie = new PdfPCell(new Phrase("Serie", _standardFont));
                clSerie.BorderWidth = 0.5f;
                clSerie.BorderWidthBottom = 0.5f;
                clSerie.HorizontalAlignment = 1;

                clFolio = new PdfPCell(new Phrase("Folio", _standardFont));
                clFolio.BorderWidth = 0.5f;
                clFolio.BorderWidthBottom = 0.5f;
                clFolio.HorizontalAlignment = 1;

                clNombreAgente = new PdfPCell(new Phrase("Nombre del Agente", _standardFont));
                clNombreAgente.BorderWidth = 0.5f;
                clNombreAgente.BorderWidthBottom = 0.5f;
                clNombreAgente.HorizontalAlignment = 1;

                clRazonSocial = new PdfPCell(new Phrase("Razón Social", _standardFont));
                clRazonSocial.BorderWidth = 0.5f;
                clRazonSocial.BorderWidthBottom = 0.5f;
                clRazonSocial.HorizontalAlignment = 1;

                clFechaVencimiento = new PdfPCell(new Phrase("Fecha de Vencimiento", _standardFont));
                clFechaVencimiento.BorderWidth = 0.5f;
                clFechaVencimiento.BorderWidthBottom = 0.5f;
                clFechaVencimiento.HorizontalAlignment = 1;

                clRFC = new PdfPCell(new Phrase("R.F.C.", _standardFont));
                clRFC.BorderWidth = 0.5f;
                clRFC.BorderWidthBottom = 0.5f;
                clRFC.HorizontalAlignment = 1;

                clSubtotal = new PdfPCell(new Phrase("Subtotal", _standardFont));
                clSubtotal.BorderWidth = 0.5f;
                clSubtotal.BorderWidthBottom = 0.5f;
                clSubtotal.HorizontalAlignment = 1;

                clIVA = new PdfPCell(new Phrase("IVA", _standardFont));
                clIVA.BorderWidth = 0.5f;
                clIVA.BorderWidthBottom = 0.5f;
                clIVA.HorizontalAlignment = 1;

                clTotal = new PdfPCell(new Phrase("Total", _standardFont));
                clTotal.BorderWidth = 0.5f;
                clTotal.BorderWidthBottom = 0.5f;
                clTotal.HorizontalAlignment = 1;

                clPendiente = new PdfPCell(new Phrase("Pendiente", _standardFont));
                clPendiente.BorderWidth = 0.5f;
                clPendiente.BorderWidthBottom = 0.5f;
                clPendiente.HorizontalAlignment = 1;

                clTextoExtra3 = new PdfPCell(new Phrase("Texto Extra 3", _standardFont));
                clTextoExtra3.BorderWidth = 0.5f;
                clTextoExtra3.BorderWidthBottom = 0.5f;
                clTextoExtra3.HorizontalAlignment = 1;

                clAfectado = new PdfPCell(new Phrase("Afectado", _standardFont));
                clAfectado.BorderWidth = 0.5f;
                clAfectado.BorderWidthBottom = 0.5f;
                clAfectado.HorizontalAlignment = 1;

                clImpreso = new PdfPCell(new Phrase("Impreso", _standardFont));
                clImpreso.BorderWidth = 0.5f;
                clImpreso.BorderWidthBottom = 0.5f;
                clImpreso.HorizontalAlignment = 1;

                clCancelado = new PdfPCell(new Phrase("Cancelado", _standardFont));
                clCancelado.BorderWidth = 0.5f;
                clCancelado.BorderWidthBottom = 0.5f;
                clCancelado.HorizontalAlignment = 1;

                clTotalUnidades = new PdfPCell(new Phrase("Total de Unidades", _standardFont));
                clTotalUnidades.BorderWidth = 0.5f;
                clTotalUnidades.BorderWidthBottom = 0.5f;
                clTotalUnidades.HorizontalAlignment = 1;

                clClasificacionCliente2 = new PdfPCell(new Phrase("Clasificación Cliente 2", _standardFont));
                clClasificacionCliente2.BorderWidth = 0.5f;
                clClasificacionCliente2.BorderWidthBottom = 0.5f;
                clClasificacionCliente2.HorizontalAlignment = 1;

                clTextoExtra1 = new PdfPCell(new Phrase("Texto Extra 1", _standardFont));
                clTextoExtra1.BorderWidth = 0.5f;
                clTextoExtra1.BorderWidthBottom = 0.5f;
                clTextoExtra1.HorizontalAlignment = 1;

                clNombreConcepto = new PdfPCell(new Phrase("Nombre del Concepto", _standardFont));
                clNombreConcepto.BorderWidth = 0.5f;
                clNombreConcepto.BorderWidthBottom = 0.5f;
                clNombreConcepto.HorizontalAlignment = 1;
                #endregion
                //***************************************************************************************************************************************
                #region Agrega titulos en las tablas
                //agrega las tablas en el pdf
                tabla_cuentas.AddCell(clFecha);
                tabla_cuentas.AddCell(clSerie);
                tabla_cuentas.AddCell(clFolio);
                tabla_cuentas.AddCell(clNombreAgente);
                tabla_cuentas.AddCell(clRazonSocial);
                tabla_cuentas.AddCell(clFechaVencimiento);
                tabla_cuentas.AddCell(clRFC);
                tabla_cuentas.AddCell(clSubtotal);
                tabla_cuentas.AddCell(clIVA);
                tabla_cuentas.AddCell(clTotal);
                tabla_cuentas.AddCell(clPendiente);
                tabla_cuentas.AddCell(clTextoExtra3);
                tabla_cuentas.AddCell(clAfectado);
                tabla_cuentas.AddCell(clImpreso);
                tabla_cuentas.AddCell(clCancelado);
                tabla_cuentas.AddCell(clTotalUnidades);
                tabla_cuentas.AddCell(clClasificacionCliente2);
                tabla_cuentas.AddCell(clTextoExtra1);
                tabla_cuentas.AddCell(clNombreConcepto);
                #endregion
                ListFactrurasCRU = ListFactrurasCRUFiltroRFCPublico;
                ValorTotal = 0;
                for (int k = 0; k < ListFactrurasCRU.Count; k++)
                {
                    #region AGREGA DATOS EN LA TABLA
                    clFecha = new PdfPCell(new Phrase(ListFactrurasCRU[k].Fecha, _smallFont));
                    clFecha.BorderWidth = 0.5f;
                    clFecha.HorizontalAlignment = 1;

                    clSerie = new PdfPCell(new Phrase(ListFactrurasCRU[k].Serie, _smallFont));
                    clSerie.BorderWidth = 0.5f;
                    clSerie.HorizontalAlignment = 1;

                    clFolio = new PdfPCell(new Phrase(ListFactrurasCRU[k].Folio, _smallFont));
                    clFolio.BorderWidth = 0.5f;
                    clFolio.HorizontalAlignment = 1;

                    clNombreAgente = new PdfPCell(new Phrase(ListFactrurasCRU[k].NombreAgente, _smallFont));
                    clNombreAgente.BorderWidth = 0.5f;
                    clNombreAgente.HorizontalAlignment = 1;

                    clRazonSocial = new PdfPCell(new Phrase(ListFactrurasCRU[k].RazonSocial, _smallFont));
                    clRazonSocial.BorderWidth = 0.5f;
                    clRazonSocial.HorizontalAlignment = 1;

                    clFechaVencimiento = new PdfPCell(new Phrase(ListFactrurasCRU[k].FechaVencimiento, _smallFont));
                    clFechaVencimiento.BorderWidth = 0.5f;
                    clFechaVencimiento.HorizontalAlignment = 1;

                    clRFC = new PdfPCell(new Phrase(ListFactrurasCRU[k].RFC, _smallFont));
                    clRFC.BorderWidth = 0.5f;
                    clRFC.HorizontalAlignment = 1;

                    clSubtotal = new PdfPCell(new Phrase(ListFactrurasCRU[k].Subtotal.ToString(), _smallFont));
                    clSubtotal.BorderWidth = 0.5f;
                    clSubtotal.HorizontalAlignment = 1;

                    clIVA = new PdfPCell(new Phrase(ListFactrurasCRU[k].IVA.ToString(), _smallFont));
                    clIVA.BorderWidth = 0.5f;
                    clIVA.HorizontalAlignment = 1;

                    clTotal = new PdfPCell(new Phrase(ListFactrurasCRU[k].Total.ToString(), _smallFont));
                    clTotal.BorderWidth = 0.5f;
                    clTotal.HorizontalAlignment = 1;
                    ValorTotal += ListFactrurasCRU[k].Total;

                    clPendiente = new PdfPCell(new Phrase(ListFactrurasCRU[k].Pendiente.ToString(), _smallFont));
                    clPendiente.BorderWidth = 0.5f;
                    clPendiente.HorizontalAlignment = 1;

                    clTextoExtra3 = new PdfPCell(new Phrase(ListFactrurasCRU[k].TextoExtra3, _smallFont));
                    clTextoExtra3.BorderWidth = 0.5f;
                    clTextoExtra3.HorizontalAlignment = 1;

                    clAfectado = new PdfPCell(new Phrase(ListFactrurasCRU[k].Afectado, _smallFont));
                    clAfectado.BorderWidth = 0.5f;
                    clAfectado.HorizontalAlignment = 1;

                    clImpreso = new PdfPCell(new Phrase(ListFactrurasCRU[k].Impreso, _smallFont));
                    clImpreso.BorderWidth = 0.5f;
                    clImpreso.HorizontalAlignment = 1;

                    clCancelado = new PdfPCell(new Phrase(ListFactrurasCRU[k].Cancelado, _smallFont));
                    clCancelado.BorderWidth = 0.5f;
                    clCancelado.HorizontalAlignment = 1;

                    clTotalUnidades = new PdfPCell(new Phrase(ListFactrurasCRU[k].TotalUnidades.ToString(), _smallFont));
                    clTotalUnidades.BorderWidth = 0.5f;
                    clTotalUnidades.HorizontalAlignment = 1;

                    clClasificacionCliente2 = new PdfPCell(new Phrase(ListFactrurasCRU[k].proveedor.Clasificación2, _smallFont));
                    clClasificacionCliente2.BorderWidth = 0.5f;
                    clClasificacionCliente2.HorizontalAlignment = 1;

                    clTextoExtra1 = new PdfPCell(new Phrase(ListFactrurasCRU[k].TextoExtra1, _smallFont));
                    clTextoExtra1.BorderWidth = 0.5f;
                    clTextoExtra1.HorizontalAlignment = 1;

                    clNombreConcepto = new PdfPCell(new Phrase(ListFactrurasCRU[k].NombreConcepto, _smallFont));
                    clNombreConcepto.BorderWidth = 0.5f;
                    clNombreConcepto.HorizontalAlignment = 1;
                    #endregion
                    #region Agrega titulos en las tablas
                    //agrega las tablas en el pdf
                    tabla_cuentas.AddCell(clFecha);
                    tabla_cuentas.AddCell(clSerie);
                    tabla_cuentas.AddCell(clFolio);
                    tabla_cuentas.AddCell(clNombreAgente);
                    tabla_cuentas.AddCell(clRazonSocial);
                    tabla_cuentas.AddCell(clFechaVencimiento);
                    tabla_cuentas.AddCell(clRFC);
                    tabla_cuentas.AddCell(clSubtotal);
                    tabla_cuentas.AddCell(clIVA);
                    tabla_cuentas.AddCell(clTotal);
                    tabla_cuentas.AddCell(clPendiente);
                    tabla_cuentas.AddCell(clTextoExtra3);
                    tabla_cuentas.AddCell(clAfectado);
                    tabla_cuentas.AddCell(clImpreso);
                    tabla_cuentas.AddCell(clCancelado);
                    tabla_cuentas.AddCell(clTotalUnidades);
                    tabla_cuentas.AddCell(clClasificacionCliente2);
                    tabla_cuentas.AddCell(clTextoExtra1);
                    tabla_cuentas.AddCell(clNombreConcepto);
                    #endregion
                }//fin for

                doc.Add(new Paragraph("Total: $" + Math.Round(ValorTotal, 2), _titulos));
                doc.Add(new Paragraph("\n"));

                //agrego la tabla al pdf
                doc.Add(tabla_cuentas);

                #endregion
                /******************************************************************************************/

                doc.Add(new Paragraph("\n"));
                doc.Add(new Paragraph("\n"));
                doc.Add(new Paragraph("\n"));

                /************************************************************/
                
                /******************************************************************************************/
                // cierro la edicion del pdf
                doc.Close();

                ////LO EJECUTO
                Process prc = new System.Diagnostics.Process();
                prc.StartInfo.FileName = rut;
                prc.Start();

            }
            catch (Exception g)
            { MessageBox.Show("" + g.Message); }
        }
        #endregion


        #region CRU PAGOS AL PROVEEDOR
        /// <summary>
        /// Método para imprimir las facturas de CRU
        /// </summary>
        /// <param name="ListFactrurasCRU">lista de las facturas que se van a imprimir</param>
        public void ImpresionCRUPAgosPRoveedor(List<Tipos_Datos_CRU.FacturasCRU> ListFactrurasCRU, string fechas, string path, List<Tipos_Datos_CRU.FacturasCRU> ListFactrurasCRUFiltroRFCPublico)
        {

            try
            {

                Document doc = new Document(PageSize.TABLOID, 10, 10, 10, 10);//Creacion del documento configuracion de tipo de hoja y margenes
                doc.AddAuthor("Indicadores");//Autor del PDF
                doc.AddKeywords("pdf, PdfWriter; Indicadores V1");

                //para almacenamiento del archivo
                string nombre_archivo = "PagosProveedoresCRU.PDF";//Nombre del Archivo
                string rut = @path + nombre_archivo;
                PdfWriter wri = PdfWriter.GetInstance(doc, new FileStream(rut, FileMode.Create));
                doc.AddTitle("REPORTE");
                doc.AddCreator("*********");
                doc.Open();
                //tipo de letras que se pueda usar en el archivo PDF
                iTextSharp.text.Font _mediumFont = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 8, iTextSharp.text.Font.NORMAL, BaseColor.BLACK);
                iTextSharp.text.Font _standardFont = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 9, iTextSharp.text.Font.BOLD, BaseColor.BLACK);
                iTextSharp.text.Font _standardFont1 = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 14, iTextSharp.text.Font.BOLD, BaseColor.WHITE);
                iTextSharp.text.Font _smallFont = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 8, iTextSharp.text.Font.NORMAL, BaseColor.BLACK);
                iTextSharp.text.Font _titulo = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 14, iTextSharp.text.Font.NORMAL, BaseColor.BLACK);
                iTextSharp.text.Font _titulos = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 12, iTextSharp.text.Font.NORMAL, BaseColor.BLACK);


                // Cabecera
                doc.Add(new Paragraph(" Desglose general de Pagos al Proveedor CRU " + fechas, _titulo));
                doc.Add(new Paragraph("\n"));
                doc.Add(new Paragraph("\n"));

                //***********************************************
                #region **********PRIMERA TABLA 32 Acumulado de Pagos al Proveedor en CRU*********
                doc.Add(new Paragraph("32 Acumulado de Pagos al Proveedor en CRU", _titulos));
                doc.Add(new Paragraph("\n"));
                PdfPTable tabla_cuentas = new PdfPTable(19);
                //PdfPCell cell = new PdfPCell(new Phrase("Reporte de Compras"));
                //cell.Colspan = 3;
                //cell.BackgroundColor = BaseColor.BLUE;
                //cell.HorizontalAlignment = 1;//0=Left, 1=Centre, 2=Right 
                tabla_cuentas.WidthPercentage = 100;
                //tabla_cuentas.AddCell(cell);



                #region configuracion de columnas
                // Configuramos el título de las columnas de la tabla 
                PdfPCell clFecha = new PdfPCell(new Phrase("Fecha", _standardFont));
                clFecha.BorderWidth = 0.5f;
                clFecha.BorderWidthBottom = 0.5f;
                clFecha.HorizontalAlignment = 1;

                PdfPCell clSerie = new PdfPCell(new Phrase("Serie", _standardFont));
                clSerie.BorderWidth = 0.5f;
                clSerie.BorderWidthBottom = 0.5f;
                clSerie.HorizontalAlignment = 1;

                PdfPCell clFolio = new PdfPCell(new Phrase("Folio", _standardFont));
                clFolio.BorderWidth = 0.5f;
                clFolio.BorderWidthBottom = 0.5f;
                clFolio.HorizontalAlignment = 1;

                PdfPCell clNombreAgente = new PdfPCell(new Phrase("Nombre del Agente", _standardFont));
                clNombreAgente.BorderWidth = 0.5f;
                clNombreAgente.BorderWidthBottom = 0.5f;
                clNombreAgente.HorizontalAlignment = 1;

                PdfPCell clRazonSocial = new PdfPCell(new Phrase("Razón Social", _standardFont));
                clRazonSocial.BorderWidth = 0.5f;
                clRazonSocial.BorderWidthBottom = 0.5f;
                clRazonSocial.HorizontalAlignment = 1;

                PdfPCell clFechaVencimiento = new PdfPCell(new Phrase("Fecha de Vencimiento", _standardFont));
                clFechaVencimiento.BorderWidth = 0.5f;
                clFechaVencimiento.BorderWidthBottom = 0.5f;
                clFechaVencimiento.HorizontalAlignment = 1;

                PdfPCell clRFC = new PdfPCell(new Phrase("R.F.C.", _standardFont));
                clRFC.BorderWidth = 0.5f;
                clRFC.BorderWidthBottom = 0.5f;
                clRFC.HorizontalAlignment = 1;

                PdfPCell clSubtotal = new PdfPCell(new Phrase("Subtotal", _standardFont));
                clSubtotal.BorderWidth = 0.5f;
                clSubtotal.BorderWidthBottom = 0.5f;
                clSubtotal.HorizontalAlignment = 1;

                PdfPCell clIVA = new PdfPCell(new Phrase("IVA", _standardFont));
                clIVA.BorderWidth = 0.5f;
                clIVA.BorderWidthBottom = 0.5f;
                clIVA.HorizontalAlignment = 1;

                PdfPCell clTotal = new PdfPCell(new Phrase("Total", _standardFont));
                clTotal.BorderWidth = 0.5f;
                clTotal.BorderWidthBottom = 0.5f;
                clTotal.HorizontalAlignment = 1;

                PdfPCell clPendiente = new PdfPCell(new Phrase("Pendiente", _standardFont));
                clPendiente.BorderWidth = 0.5f;
                clPendiente.BorderWidthBottom = 0.5f;
                clPendiente.HorizontalAlignment = 1;

                PdfPCell clTextoExtra3 = new PdfPCell(new Phrase("Texto Extra 3", _standardFont));
                clTextoExtra3.BorderWidth = 0.5f;
                clTextoExtra3.BorderWidthBottom = 0.5f;
                clTextoExtra3.HorizontalAlignment = 1;

                PdfPCell clAfectado = new PdfPCell(new Phrase("Afectado", _standardFont));
                clAfectado.BorderWidth = 0.5f;
                clAfectado.BorderWidthBottom = 0.5f;
                clAfectado.HorizontalAlignment = 1;

                PdfPCell clImpreso = new PdfPCell(new Phrase("Impreso", _standardFont));
                clImpreso.BorderWidth = 0.5f;
                clImpreso.BorderWidthBottom = 0.5f;
                clImpreso.HorizontalAlignment = 1;

                PdfPCell clCancelado = new PdfPCell(new Phrase("Cancelado", _standardFont));
                clCancelado.BorderWidth = 0.5f;
                clCancelado.BorderWidthBottom = 0.5f;
                clCancelado.HorizontalAlignment = 1;

                PdfPCell clTotalUnidades = new PdfPCell(new Phrase("Total de Unidades", _standardFont));
                clTotalUnidades.BorderWidth = 0.5f;
                clTotalUnidades.BorderWidthBottom = 0.5f;
                clTotalUnidades.HorizontalAlignment = 1;

                PdfPCell clClasificacionCliente2 = new PdfPCell(new Phrase("Clasificación Cliente 2", _standardFont));
                clClasificacionCliente2.BorderWidth = 0.5f;
                clClasificacionCliente2.BorderWidthBottom = 0.5f;
                clClasificacionCliente2.HorizontalAlignment = 1;

                PdfPCell clTextoExtra1 = new PdfPCell(new Phrase("Texto Extra 1", _standardFont));
                clTextoExtra1.BorderWidth = 0.5f;
                clTextoExtra1.BorderWidthBottom = 0.5f;
                clTextoExtra1.HorizontalAlignment = 1;

                PdfPCell clNombreConcepto = new PdfPCell(new Phrase("Nombre del Concepto", _standardFont));
                clNombreConcepto.BorderWidth = 0.5f;
                clNombreConcepto.BorderWidthBottom = 0.5f;
                clNombreConcepto.HorizontalAlignment = 1;
                #endregion
                //***************************************************************************************************************************************
                #region Agrega titulos en las tablas
                //agrega las tablas en el pdf
                tabla_cuentas.AddCell(clFecha);
                tabla_cuentas.AddCell(clSerie);
                tabla_cuentas.AddCell(clFolio);
                tabla_cuentas.AddCell(clNombreAgente);
                tabla_cuentas.AddCell(clRazonSocial);
                tabla_cuentas.AddCell(clFechaVencimiento);
                tabla_cuentas.AddCell(clRFC);
                tabla_cuentas.AddCell(clSubtotal);
                tabla_cuentas.AddCell(clIVA);
                tabla_cuentas.AddCell(clTotal);
                tabla_cuentas.AddCell(clPendiente);
                tabla_cuentas.AddCell(clTextoExtra3);
                tabla_cuentas.AddCell(clAfectado);
                tabla_cuentas.AddCell(clImpreso);
                tabla_cuentas.AddCell(clCancelado);
                tabla_cuentas.AddCell(clTotalUnidades);
                tabla_cuentas.AddCell(clClasificacionCliente2);
                tabla_cuentas.AddCell(clTextoExtra1);
                tabla_cuentas.AddCell(clNombreConcepto);
                #endregion

                double ValorTotal = 0;
                for (int k = 0; k < ListFactrurasCRU.Count; k++)
                {
                    #region AGREGA DATOS EN LA TABLA
                    clFecha = new PdfPCell(new Phrase(ListFactrurasCRU[k].Fecha, _smallFont));
                    clFecha.BorderWidth = 0.5f;
                    clFecha.HorizontalAlignment = 1;

                    clSerie = new PdfPCell(new Phrase(ListFactrurasCRU[k].Serie, _smallFont));
                    clSerie.BorderWidth = 0.5f;
                    clSerie.HorizontalAlignment = 1;

                    clFolio = new PdfPCell(new Phrase(ListFactrurasCRU[k].Folio, _smallFont));
                    clFolio.BorderWidth = 0.5f;
                    clFolio.HorizontalAlignment = 1;

                    clNombreAgente = new PdfPCell(new Phrase(ListFactrurasCRU[k].NombreAgente, _smallFont));
                    clNombreAgente.BorderWidth = 0.5f;
                    clNombreAgente.HorizontalAlignment = 1;

                    clRazonSocial = new PdfPCell(new Phrase(ListFactrurasCRU[k].RazonSocial, _smallFont));
                    clRazonSocial.BorderWidth = 0.5f;
                    clRazonSocial.HorizontalAlignment = 1;

                    clFechaVencimiento = new PdfPCell(new Phrase(ListFactrurasCRU[k].FechaVencimiento, _smallFont));
                    clFechaVencimiento.BorderWidth = 0.5f;
                    clFechaVencimiento.HorizontalAlignment = 1;

                    clRFC = new PdfPCell(new Phrase(ListFactrurasCRU[k].RFC, _smallFont));
                    clRFC.BorderWidth = 0.5f;
                    clRFC.HorizontalAlignment = 1;

                    clSubtotal = new PdfPCell(new Phrase(ListFactrurasCRU[k].Subtotal.ToString(), _smallFont));
                    clSubtotal.BorderWidth = 0.5f;
                    clSubtotal.HorizontalAlignment = 1;

                    clIVA = new PdfPCell(new Phrase(ListFactrurasCRU[k].IVA.ToString(), _smallFont));
                    clIVA.BorderWidth = 0.5f;
                    clIVA.HorizontalAlignment = 1;

                    clTotal = new PdfPCell(new Phrase(ListFactrurasCRU[k].Total.ToString(), _smallFont));
                    clTotal.BorderWidth = 0.5f;
                    clTotal.HorizontalAlignment = 1;
                    ValorTotal += ListFactrurasCRU[k].Total;

                    clPendiente = new PdfPCell(new Phrase(ListFactrurasCRU[k].Pendiente.ToString(), _smallFont));
                    clPendiente.BorderWidth = 0.5f;
                    clPendiente.HorizontalAlignment = 1;

                    clTextoExtra3 = new PdfPCell(new Phrase(ListFactrurasCRU[k].TextoExtra3, _smallFont));
                    clTextoExtra3.BorderWidth = 0.5f;
                    clTextoExtra3.HorizontalAlignment = 1;

                    clAfectado = new PdfPCell(new Phrase(ListFactrurasCRU[k].Afectado, _smallFont));
                    clAfectado.BorderWidth = 0.5f;
                    clAfectado.HorizontalAlignment = 1;

                    clImpreso = new PdfPCell(new Phrase(ListFactrurasCRU[k].Impreso, _smallFont));
                    clImpreso.BorderWidth = 0.5f;
                    clImpreso.HorizontalAlignment = 1;

                    clCancelado = new PdfPCell(new Phrase(ListFactrurasCRU[k].Cancelado, _smallFont));
                    clCancelado.BorderWidth = 0.5f;
                    clCancelado.HorizontalAlignment = 1;

                    clTotalUnidades = new PdfPCell(new Phrase(ListFactrurasCRU[k].TotalUnidades.ToString(), _smallFont));
                    clTotalUnidades.BorderWidth = 0.5f;
                    clTotalUnidades.HorizontalAlignment = 1;

                    clClasificacionCliente2 = new PdfPCell(new Phrase(ListFactrurasCRU[k].proveedor.Clasificación2, _smallFont));
                    clClasificacionCliente2.BorderWidth = 0.5f;
                    clClasificacionCliente2.HorizontalAlignment = 1;

                    clTextoExtra1 = new PdfPCell(new Phrase(ListFactrurasCRU[k].TextoExtra1, _smallFont));
                    clTextoExtra1.BorderWidth = 0.5f;
                    clTextoExtra1.HorizontalAlignment = 1;

                    clNombreConcepto = new PdfPCell(new Phrase(ListFactrurasCRU[k].NombreConcepto, _smallFont));
                    clNombreConcepto.BorderWidth = 0.5f;
                    clNombreConcepto.HorizontalAlignment = 1;
                    #endregion
                    #region Agrega titulos en las tablas
                    //agrega las tablas en el pdf
                    tabla_cuentas.AddCell(clFecha);
                    tabla_cuentas.AddCell(clSerie);
                    tabla_cuentas.AddCell(clFolio);
                    tabla_cuentas.AddCell(clNombreAgente);
                    tabla_cuentas.AddCell(clRazonSocial);
                    tabla_cuentas.AddCell(clFechaVencimiento);
                    tabla_cuentas.AddCell(clRFC);
                    tabla_cuentas.AddCell(clSubtotal);
                    tabla_cuentas.AddCell(clIVA);
                    tabla_cuentas.AddCell(clTotal);
                    tabla_cuentas.AddCell(clPendiente);
                    tabla_cuentas.AddCell(clTextoExtra3);
                    tabla_cuentas.AddCell(clAfectado);
                    tabla_cuentas.AddCell(clImpreso);
                    tabla_cuentas.AddCell(clCancelado);
                    tabla_cuentas.AddCell(clTotalUnidades);
                    tabla_cuentas.AddCell(clClasificacionCliente2);
                    tabla_cuentas.AddCell(clTextoExtra1);
                    tabla_cuentas.AddCell(clNombreConcepto);
                    #endregion
                }//fin for

                doc.Add(new Paragraph("Total: $" + Math.Round(ValorTotal, 2), _titulos));
                doc.Add(new Paragraph("\n"));

                //agrego la tabla al pdf
                doc.Add(tabla_cuentas);

                #endregion
                doc.Add(new Paragraph("\n"));
                doc.Add(new Paragraph("\n"));
                doc.Add(new Paragraph("\n"));

                /************************************************************/
                #region **********SEGUNDA TABLA  filtro por RFC ANJI*********
                doc.Add(new Paragraph("33 Acumulado de Pagos al Proveedor en CRU a ANJI", _titulos));
                doc.Add(new Paragraph("\n"));
                tabla_cuentas = new PdfPTable(19);
                //PdfPCell cell = new PdfPCell(new Phrase("Reporte de Compras"));
                //cell.Colspan = 3;
                //cell.BackgroundColor = BaseColor.BLUE;
                //cell.HorizontalAlignment = 1;//0=Left, 1=Centre, 2=Right 
                tabla_cuentas.WidthPercentage = 100;
                //tabla_cuentas.AddCell(cell);



                #region configuracion de columnas
                // Configuramos el título de las columnas de la tabla 
                clFecha = new PdfPCell(new Phrase("Fecha", _standardFont));
                clFecha.BorderWidth = 0.5f;
                clFecha.BorderWidthBottom = 0.5f;
                clFecha.HorizontalAlignment = 1;

                clSerie = new PdfPCell(new Phrase("Serie", _standardFont));
                clSerie.BorderWidth = 0.5f;
                clSerie.BorderWidthBottom = 0.5f;
                clSerie.HorizontalAlignment = 1;

                clFolio = new PdfPCell(new Phrase("Folio", _standardFont));
                clFolio.BorderWidth = 0.5f;
                clFolio.BorderWidthBottom = 0.5f;
                clFolio.HorizontalAlignment = 1;

                clNombreAgente = new PdfPCell(new Phrase("Nombre del Agente", _standardFont));
                clNombreAgente.BorderWidth = 0.5f;
                clNombreAgente.BorderWidthBottom = 0.5f;
                clNombreAgente.HorizontalAlignment = 1;

                clRazonSocial = new PdfPCell(new Phrase("Razón Social", _standardFont));
                clRazonSocial.BorderWidth = 0.5f;
                clRazonSocial.BorderWidthBottom = 0.5f;
                clRazonSocial.HorizontalAlignment = 1;

                clFechaVencimiento = new PdfPCell(new Phrase("Fecha de Vencimiento", _standardFont));
                clFechaVencimiento.BorderWidth = 0.5f;
                clFechaVencimiento.BorderWidthBottom = 0.5f;
                clFechaVencimiento.HorizontalAlignment = 1;

                clRFC = new PdfPCell(new Phrase("R.F.C.", _standardFont));
                clRFC.BorderWidth = 0.5f;
                clRFC.BorderWidthBottom = 0.5f;
                clRFC.HorizontalAlignment = 1;

                clSubtotal = new PdfPCell(new Phrase("Subtotal", _standardFont));
                clSubtotal.BorderWidth = 0.5f;
                clSubtotal.BorderWidthBottom = 0.5f;
                clSubtotal.HorizontalAlignment = 1;

                clIVA = new PdfPCell(new Phrase("IVA", _standardFont));
                clIVA.BorderWidth = 0.5f;
                clIVA.BorderWidthBottom = 0.5f;
                clIVA.HorizontalAlignment = 1;

                clTotal = new PdfPCell(new Phrase("Total", _standardFont));
                clTotal.BorderWidth = 0.5f;
                clTotal.BorderWidthBottom = 0.5f;
                clTotal.HorizontalAlignment = 1;

                clPendiente = new PdfPCell(new Phrase("Pendiente", _standardFont));
                clPendiente.BorderWidth = 0.5f;
                clPendiente.BorderWidthBottom = 0.5f;
                clPendiente.HorizontalAlignment = 1;

                clTextoExtra3 = new PdfPCell(new Phrase("Texto Extra 3", _standardFont));
                clTextoExtra3.BorderWidth = 0.5f;
                clTextoExtra3.BorderWidthBottom = 0.5f;
                clTextoExtra3.HorizontalAlignment = 1;

                clAfectado = new PdfPCell(new Phrase("Afectado", _standardFont));
                clAfectado.BorderWidth = 0.5f;
                clAfectado.BorderWidthBottom = 0.5f;
                clAfectado.HorizontalAlignment = 1;

                clImpreso = new PdfPCell(new Phrase("Impreso", _standardFont));
                clImpreso.BorderWidth = 0.5f;
                clImpreso.BorderWidthBottom = 0.5f;
                clImpreso.HorizontalAlignment = 1;

                clCancelado = new PdfPCell(new Phrase("Cancelado", _standardFont));
                clCancelado.BorderWidth = 0.5f;
                clCancelado.BorderWidthBottom = 0.5f;
                clCancelado.HorizontalAlignment = 1;

                clTotalUnidades = new PdfPCell(new Phrase("Total de Unidades", _standardFont));
                clTotalUnidades.BorderWidth = 0.5f;
                clTotalUnidades.BorderWidthBottom = 0.5f;
                clTotalUnidades.HorizontalAlignment = 1;

                clClasificacionCliente2 = new PdfPCell(new Phrase("Clasificación Cliente 2", _standardFont));
                clClasificacionCliente2.BorderWidth = 0.5f;
                clClasificacionCliente2.BorderWidthBottom = 0.5f;
                clClasificacionCliente2.HorizontalAlignment = 1;

                clTextoExtra1 = new PdfPCell(new Phrase("Texto Extra 1", _standardFont));
                clTextoExtra1.BorderWidth = 0.5f;
                clTextoExtra1.BorderWidthBottom = 0.5f;
                clTextoExtra1.HorizontalAlignment = 1;

                clNombreConcepto = new PdfPCell(new Phrase("Nombre del Concepto", _standardFont));
                clNombreConcepto.BorderWidth = 0.5f;
                clNombreConcepto.BorderWidthBottom = 0.5f;
                clNombreConcepto.HorizontalAlignment = 1;
                #endregion
                //***************************************************************************************************************************************
                #region Agrega titulos en las tablas
                //agrega las tablas en el pdf
                tabla_cuentas.AddCell(clFecha);
                tabla_cuentas.AddCell(clSerie);
                tabla_cuentas.AddCell(clFolio);
                tabla_cuentas.AddCell(clNombreAgente);
                tabla_cuentas.AddCell(clRazonSocial);
                tabla_cuentas.AddCell(clFechaVencimiento);
                tabla_cuentas.AddCell(clRFC);
                tabla_cuentas.AddCell(clSubtotal);
                tabla_cuentas.AddCell(clIVA);
                tabla_cuentas.AddCell(clTotal);
                tabla_cuentas.AddCell(clPendiente);
                tabla_cuentas.AddCell(clTextoExtra3);
                tabla_cuentas.AddCell(clAfectado);
                tabla_cuentas.AddCell(clImpreso);
                tabla_cuentas.AddCell(clCancelado);
                tabla_cuentas.AddCell(clTotalUnidades);
                tabla_cuentas.AddCell(clClasificacionCliente2);
                tabla_cuentas.AddCell(clTextoExtra1);
                tabla_cuentas.AddCell(clNombreConcepto);
                #endregion
                ListFactrurasCRU = ListFactrurasCRUFiltroRFCPublico;
                ValorTotal = 0;
                for (int k = 0; k < ListFactrurasCRU.Count; k++)
                {
                    #region AGREGA DATOS EN LA TABLA
                    clFecha = new PdfPCell(new Phrase(ListFactrurasCRU[k].Fecha, _smallFont));
                    clFecha.BorderWidth = 0.5f;
                    clFecha.HorizontalAlignment = 1;

                    clSerie = new PdfPCell(new Phrase(ListFactrurasCRU[k].Serie, _smallFont));
                    clSerie.BorderWidth = 0.5f;
                    clSerie.HorizontalAlignment = 1;

                    clFolio = new PdfPCell(new Phrase(ListFactrurasCRU[k].Folio, _smallFont));
                    clFolio.BorderWidth = 0.5f;
                    clFolio.HorizontalAlignment = 1;

                    clNombreAgente = new PdfPCell(new Phrase(ListFactrurasCRU[k].NombreAgente, _smallFont));
                    clNombreAgente.BorderWidth = 0.5f;
                    clNombreAgente.HorizontalAlignment = 1;

                    clRazonSocial = new PdfPCell(new Phrase(ListFactrurasCRU[k].RazonSocial, _smallFont));
                    clRazonSocial.BorderWidth = 0.5f;
                    clRazonSocial.HorizontalAlignment = 1;

                    clFechaVencimiento = new PdfPCell(new Phrase(ListFactrurasCRU[k].FechaVencimiento, _smallFont));
                    clFechaVencimiento.BorderWidth = 0.5f;
                    clFechaVencimiento.HorizontalAlignment = 1;

                    clRFC = new PdfPCell(new Phrase(ListFactrurasCRU[k].RFC, _smallFont));
                    clRFC.BorderWidth = 0.5f;
                    clRFC.HorizontalAlignment = 1;

                    clSubtotal = new PdfPCell(new Phrase(ListFactrurasCRU[k].Subtotal.ToString(), _smallFont));
                    clSubtotal.BorderWidth = 0.5f;
                    clSubtotal.HorizontalAlignment = 1;

                    clIVA = new PdfPCell(new Phrase(ListFactrurasCRU[k].IVA.ToString(), _smallFont));
                    clIVA.BorderWidth = 0.5f;
                    clIVA.HorizontalAlignment = 1;

                    clTotal = new PdfPCell(new Phrase(ListFactrurasCRU[k].Total.ToString(), _smallFont));
                    clTotal.BorderWidth = 0.5f;
                    clTotal.HorizontalAlignment = 1;
                    ValorTotal += ListFactrurasCRU[k].Total;

                    clPendiente = new PdfPCell(new Phrase(ListFactrurasCRU[k].Pendiente.ToString(), _smallFont));
                    clPendiente.BorderWidth = 0.5f;
                    clPendiente.HorizontalAlignment = 1;

                    clTextoExtra3 = new PdfPCell(new Phrase(ListFactrurasCRU[k].TextoExtra3, _smallFont));
                    clTextoExtra3.BorderWidth = 0.5f;
                    clTextoExtra3.HorizontalAlignment = 1;

                    clAfectado = new PdfPCell(new Phrase(ListFactrurasCRU[k].Afectado, _smallFont));
                    clAfectado.BorderWidth = 0.5f;
                    clAfectado.HorizontalAlignment = 1;

                    clImpreso = new PdfPCell(new Phrase(ListFactrurasCRU[k].Impreso, _smallFont));
                    clImpreso.BorderWidth = 0.5f;
                    clImpreso.HorizontalAlignment = 1;

                    clCancelado = new PdfPCell(new Phrase(ListFactrurasCRU[k].Cancelado, _smallFont));
                    clCancelado.BorderWidth = 0.5f;
                    clCancelado.HorizontalAlignment = 1;

                    clTotalUnidades = new PdfPCell(new Phrase(ListFactrurasCRU[k].TotalUnidades.ToString(), _smallFont));
                    clTotalUnidades.BorderWidth = 0.5f;
                    clTotalUnidades.HorizontalAlignment = 1;

                    clClasificacionCliente2 = new PdfPCell(new Phrase(ListFactrurasCRU[k].proveedor.Clasificación2, _smallFont));
                    clClasificacionCliente2.BorderWidth = 0.5f;
                    clClasificacionCliente2.HorizontalAlignment = 1;

                    clTextoExtra1 = new PdfPCell(new Phrase(ListFactrurasCRU[k].TextoExtra1, _smallFont));
                    clTextoExtra1.BorderWidth = 0.5f;
                    clTextoExtra1.HorizontalAlignment = 1;

                    clNombreConcepto = new PdfPCell(new Phrase(ListFactrurasCRU[k].NombreConcepto, _smallFont));
                    clNombreConcepto.BorderWidth = 0.5f;
                    clNombreConcepto.HorizontalAlignment = 1;
                    #endregion
                    #region Agrega titulos en las tablas
                    //agrega las tablas en el pdf
                    tabla_cuentas.AddCell(clFecha);
                    tabla_cuentas.AddCell(clSerie);
                    tabla_cuentas.AddCell(clFolio);
                    tabla_cuentas.AddCell(clNombreAgente);
                    tabla_cuentas.AddCell(clRazonSocial);
                    tabla_cuentas.AddCell(clFechaVencimiento);
                    tabla_cuentas.AddCell(clRFC);
                    tabla_cuentas.AddCell(clSubtotal);
                    tabla_cuentas.AddCell(clIVA);
                    tabla_cuentas.AddCell(clTotal);
                    tabla_cuentas.AddCell(clPendiente);
                    tabla_cuentas.AddCell(clTextoExtra3);
                    tabla_cuentas.AddCell(clAfectado);
                    tabla_cuentas.AddCell(clImpreso);
                    tabla_cuentas.AddCell(clCancelado);
                    tabla_cuentas.AddCell(clTotalUnidades);
                    tabla_cuentas.AddCell(clClasificacionCliente2);
                    tabla_cuentas.AddCell(clTextoExtra1);
                    tabla_cuentas.AddCell(clNombreConcepto);
                    #endregion
                }//fin for

                doc.Add(new Paragraph("Total: $" + Math.Round(ValorTotal, 2), _titulos));
                doc.Add(new Paragraph("\n"));

                //agrego la tabla al pdf
                doc.Add(tabla_cuentas);

                #endregion
                /******************************************************************************************/

                doc.Add(new Paragraph("\n"));
                doc.Add(new Paragraph("\n"));
                doc.Add(new Paragraph("\n"));

                /************************************************************/

                /******************************************************************************************/
                // cierro la edicion del pdf
                doc.Close();

                ////LO EJECUTO
                Process prc = new System.Diagnostics.Process();
                prc.StartInfo.FileName = rut;
                prc.Start();

            }
            catch (Exception g)
            { MessageBox.Show("" + g.Message); }
        }
        #endregion


        #region COMPRAS DE FORMA GRÁFICA

        #region archivo REPORTE_COMPRAS.PDF

        public void Reporte_Compras(List<Tipos_Datos_CRU.Movimientos_Cuentas> cuentas, string fechas, string fechastitulo, string path)
        {

            #region desglose general
            lista_cuentas = new List<Tipos_Datos_CRU.Movimientos_Cuentas>();
            lista_cuentas = cuentas;
            string[] mes = new string[12];
            mes[0] = "Enero"; mes[1] = "Febrero"; mes[2] = "Marzo"; mes[3] = "Abril"; mes[4] = "Mayo"; mes[5] = "Junio"; mes[6] = "Julio"; mes[7] = "Agosto"; mes[8] = "Septiembre"; mes[9] = "Octubre"; mes[10] = "Noviembre"; mes[11] = "Diciembre";

            try
            {
                instance_graficas = new Cargar_graficas();//para crear gráficas
                Document doc = new Document(PageSize.LETTER.Rotate(), 15, 15, 15, 15);
                doc.AddAuthor("ADMISEL");
                doc.AddKeywords("pdf, PdfWriter; Reporte de movimientos de compras");
                string nombre_archivo = "Reporte_Indicadores_CXP" + fechas.Replace("/", "_") + ".PDF";
                nombre_archivo = nombre_archivo.Replace(" ", "_");
                string rut = @path + nombre_archivo;
                PdfWriter wri = PdfWriter.GetInstance(doc, new FileStream(rut, FileMode.Create));
                doc.AddTitle("REPORTE");
                doc.AddCreator("*********");
                doc.Open();
                iTextSharp.text.Font _mediumFont = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 8, iTextSharp.text.Font.NORMAL, BaseColor.BLACK);
                iTextSharp.text.Font _standardFont = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 9, iTextSharp.text.Font.BOLD, BaseColor.BLACK);
                iTextSharp.text.Font _standardFont1 = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 14, iTextSharp.text.Font.BOLD, BaseColor.WHITE);
                iTextSharp.text.Font _smallFont = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 8, iTextSharp.text.Font.NORMAL, BaseColor.BLACK);
                iTextSharp.text.Font _titulo = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 14, iTextSharp.text.Font.NORMAL, BaseColor.BLACK);
                iTextSharp.text.Font _titulos = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 12, iTextSharp.text.Font.NORMAL, BaseColor.BLACK);

                //Lo que vamos a imprimir
                // Cabecera
                doc.Add(new Paragraph(" Desglose general del  " + fechastitulo, _titulo));
                doc.Add(new Paragraph("\n"));


                //***********************************************
                //*******TITULO REPORTE COMPRAS
                PdfPTable tabla_cuentastitulo = new PdfPTable(1);
                tabla_cuentastitulo.WidthPercentage = 100;
                PdfPCell clTitul = new PdfPCell(new Phrase("Reporte Compras", _standardFont1));
                clTitul.BorderWidth = 0.5f;
                clTitul.BorderWidthBottom = 0.5f;
                clTitul.HorizontalAlignment = 1;
                clTitul.PaddingTop = 5;
                clTitul.PaddingBottom = 10;
                clTitul.BackgroundColor = new BaseColor(70, 163, 255);
                tabla_cuentastitulo.AddCell(clTitul);

                doc.Add(tabla_cuentastitulo);
                //***********************************************
                #region **********PRIMERA TABLA *********
                PdfPTable tabla_cuentas = new PdfPTable(10);
                tabla_cuentas.WidthPercentage = 100;

                // Configuramos el título de las columnas de la tabla
                PdfPCell clFecha = new PdfPCell(new Phrase("Fecha", _standardFont));
                clFecha.BorderWidth = 0.5f;
                clFecha.BorderWidthBottom = 0.5f;
                clFecha.HorizontalAlignment = 1;

                PdfPCell clFolio = new PdfPCell(new Phrase("Folio", _standardFont));
                clFolio.BorderWidth = 0;
                clFolio.BorderWidthBottom = 0.75f;
                clFolio.HorizontalAlignment = 1;

                PdfPCell clRzSocial = new PdfPCell(new Phrase("Razon Social", _standardFont));
                clRzSocial.BorderWidth = 0.5f;
                clRzSocial.BorderWidthBottom = 0.5f;
                clRzSocial.HorizontalAlignment = 1;

                PdfPCell clCodigo = new PdfPCell(new Phrase("Codigo", _standardFont));
                clCodigo.BorderWidth = 0.5f;
                clCodigo.BorderWidthBottom = 0.5f;
                clCodigo.HorizontalAlignment = 1;

                PdfPCell climporte = new PdfPCell(new Phrase("Importe", _standardFont));
                climporte.BorderWidth = 0.5f;
                climporte.BorderWidthBottom = 0.5f;
                climporte.HorizontalAlignment = 1;

                PdfPCell clIva = new PdfPCell(new Phrase("IVA", _standardFont));
                clIva.BorderWidth = 0.5f;
                clIva.BorderWidthBottom = 0.5f;
                clIva.HorizontalAlignment = 1;

                PdfPCell clTotal = new PdfPCell(new Phrase("Total", _standardFont));
                clTotal.BorderWidth = 0.5f;
                clTotal.BorderWidthBottom = 0.5f;
                clTotal.HorizontalAlignment = 1;

                PdfPCell clPendiente = new PdfPCell(new Phrase("Pendiente", _standardFont));
                clPendiente.BorderWidth = 0.5f;
                clPendiente.BorderWidthBottom = 0.5f;
                clPendiente.HorizontalAlignment = 1;

                PdfPCell clClasificacion1 = new PdfPCell(new Phrase("Clasificacion 1", _standardFont));
                clClasificacion1.BorderWidth = 0.5f;
                clClasificacion1.BorderWidthBottom = 0.5f;
                clClasificacion1.HorizontalAlignment = 1;

                PdfPCell clClasificacion2 = new PdfPCell(new Phrase("Clasificacion 2", _standardFont));
                clClasificacion2.BorderWidth = 0.5f;
                clClasificacion2.BorderWidthBottom = 0.5f;
                clClasificacion2.HorizontalAlignment = 1;


                lista_cuentas = cuentas;
                for (int k = 0; k < lista_cuentas.Count; k++)
                {
                    // Añadimos las celdas a la tabla
                    tabla_cuentas.AddCell(clFecha);
                    tabla_cuentas.AddCell(clFolio);
                    tabla_cuentas.AddCell(clRzSocial);
                    tabla_cuentas.AddCell(clCodigo);
                    tabla_cuentas.AddCell(climporte);
                    tabla_cuentas.AddCell(clIva);
                    tabla_cuentas.AddCell(clTotal);
                    tabla_cuentas.AddCell(clPendiente);
                    tabla_cuentas.AddCell(clClasificacion1);
                    tabla_cuentas.AddCell(clClasificacion2);

                    clFecha = new PdfPCell(new Phrase(lista_cuentas[k].fecha));
                    clFecha.BorderWidth = 0.5f;
                    clFecha.HorizontalAlignment = 1;

                    clFolio = new PdfPCell(new Phrase(lista_cuentas[k].folio));
                    clFolio.BorderWidth = 0.5f;
                    clFolio.HorizontalAlignment = 1;

                    clRzSocial = new PdfPCell(new Phrase(lista_cuentas[k].Proveedor, _smallFont));
                    clRzSocial.BorderWidth = 0.5f;
                    clRzSocial.HorizontalAlignment = 1;

                    clCodigo = new PdfPCell(new Phrase(lista_cuentas[k].Proveedor_codigo, _smallFont));
                    clCodigo.BorderWidth = 0.5f;
                    clCodigo.HorizontalAlignment = 1;

                    climporte = new PdfPCell(new Phrase(lista_cuentas[k].Subtotal.ToString(), _smallFont));
                    climporte.BorderWidth = 0.5f;
                    climporte.HorizontalAlignment = 1;

                    clIva = new PdfPCell(new Phrase(lista_cuentas[k].IVA.ToString(), _smallFont));
                    clIva.BorderWidth = 0.5f;
                    clIva.HorizontalAlignment = 1;

                    clTotal = new PdfPCell(new Phrase(lista_cuentas[k].Total.ToString(), _smallFont));
                    clTotal.BorderWidth = 0.5f;
                    clTotal.HorizontalAlignment = 1;

                    // ******* AQUI SE ASIGNA EL PENDIENTE ******
                    clPendiente = new PdfPCell(new Phrase(lista_cuentas[k].Total.ToString(), _smallFont));  //  clPendiente = new PdfPCell(new Phrase(lista_cuentas[k].Total.ToString(), _smallFont));
                    clPendiente.BorderWidth = 0.5f;
                    clPendiente.HorizontalAlignment = 1;

                    clClasificacion1 = new PdfPCell(new Phrase(lista_cuentas[k].Clasificacion_1_proveedor, _smallFont));
                    clClasificacion1.BorderWidth = 0.5f;
                    clClasificacion1.HorizontalAlignment = 1;

                    clClasificacion2 = new PdfPCell(new Phrase(lista_cuentas[k].Clasificacion_2_proveedor, _smallFont));
                    clClasificacion2.BorderWidth = 0.5f;
                    clClasificacion2.HorizontalAlignment = 1;
                }//for


                // Añadimos las celdas a la tabla

                tabla_cuentas.AddCell(clFecha);
                tabla_cuentas.AddCell(clFolio);
                tabla_cuentas.AddCell(clRzSocial);
                tabla_cuentas.AddCell(clCodigo);
                tabla_cuentas.AddCell(climporte);
                tabla_cuentas.AddCell(clIva);
                tabla_cuentas.AddCell(clTotal);
                tabla_cuentas.AddCell(clPendiente);
                tabla_cuentas.AddCell(clClasificacion1);
                tabla_cuentas.AddCell(clClasificacion2);

                //agrego la tabla al pdf
                doc.Add(tabla_cuentas);

                #endregion

                doc.Add(new Paragraph("\n"));
                //***********************************************
                //*******TITULO CXP
                PdfPTable tabla_cuentastitulo1 = new PdfPTable(1);
                tabla_cuentastitulo1.WidthPercentage = 100;
                PdfPCell clTitul1 = new PdfPCell(new Phrase("      CXP      ", _standardFont1));
                clTitul1.BorderWidth = 0.5f;
                clTitul1.BorderWidthBottom = 0.75f;
                clTitul1.HorizontalAlignment = 1;
                clTitul1.PaddingBottom = 10;
                clTitul1.PaddingTop = 5;
                clTitul1.BackgroundColor = new BaseColor(70, 163, 255);
                tabla_cuentastitulo1.AddCell(clTitul1);

                doc.Add(tabla_cuentastitulo1);

                /************************************************************/
                /************************************************************/
                /************************************************************/
                #region   /********SEGUNDA TABLA************/



                PdfPTable tabla_cuentas_por_pagar = new PdfPTable(3);
                tabla_cuentas_por_pagar.WidthPercentage = 100;

                // Configuramos el título de las columnas de la tabla
                PdfPCell clFechaCXP = new PdfPCell(new Phrase("Fecha", _standardFont));
                clFechaCXP.BorderWidth = 0;
                clFechaCXP.BorderWidthBottom = 0.75f;
                clFechaCXP.HorizontalAlignment = 1;


                PdfPCell clPendienteCXP = new PdfPCell(new Phrase("Pendiente", _standardFont));
                clPendienteCXP.BorderWidth = 0;
                clPendienteCXP.BorderWidthBottom = 0.75f;
                clPendienteCXP.HorizontalAlignment = 1;

                PdfPCell clCXP = new PdfPCell(new Phrase("CXP", _standardFont));
                clCXP.BorderWidth = 0;
                clCXP.BorderWidthBottom = 0.75f;
                clCXP.HorizontalAlignment = 1;




                lista_cuentas = cuentas;
                float CXP = 0;

                for (int k = 0; k < lista_cuentas.Count; k++)
                {
                    // Añadimos las celdas a la tabla
                    tabla_cuentas_por_pagar.AddCell(clFechaCXP);
                    tabla_cuentas_por_pagar.AddCell(clPendienteCXP);
                    tabla_cuentas_por_pagar.AddCell(clCXP);

                    clFechaCXP = new PdfPCell(new Phrase(lista_cuentas[k].fecha, _standardFont));
                    clFechaCXP.BorderWidth = 0;
                    clFechaCXP.HorizontalAlignment = 1;
                    if (k % 2 == 0)
                        clFechaCXP.BackgroundColor = new BaseColor(159, 207, 255);

                    // ******* AQUI SE ASIGNA EL PENDIENTE ******
                    clPendienteCXP = new PdfPCell(new Phrase(lista_cuentas[k].Total.ToString(), _standardFont));// clPendienteCXP = new PdfPCell(new Phrase(lista_cuentas[k].Total.ToString(), _standardFont));
                    clPendienteCXP.BorderWidth = 0;
                    clPendienteCXP.HorizontalAlignment = 1;
                    if (k % 2 == 0)
                        clPendienteCXP.BackgroundColor = new BaseColor(159, 207, 255);

                    /**se hace la cuenta de CXP**/
                    CXP = CXP + lista_cuentas[k].Total;
                    clCXP = new PdfPCell(new Phrase(CXP.ToString(), _standardFont));
                    clCXP.BorderWidth = 0;
                    clCXP.HorizontalAlignment = 1;
                    if (k % 2 == 0)
                        clCXP.BackgroundColor = new BaseColor(159, 207, 255);

                }//for


                // Añadimos las celdas a la tabla
                tabla_cuentas_por_pagar.AddCell(clFechaCXP);
                tabla_cuentas_por_pagar.AddCell(clPendienteCXP);
                tabla_cuentas_por_pagar.AddCell(clCXP);
                #endregion
                //agrego la tabla al pdf
                doc.Add(tabla_cuentas_por_pagar);
                doc.Add(new Paragraph("\n"));
            #endregion

                /************************************************************/
                /************************************************************/
                /************************************************************/
                #region COPRAS POR DIA POR MES



                doc.Add(new Paragraph("(1) Reporte Compras por Dia por Mes", _titulos));
                doc.Add(new Paragraph("\n"));







                #region   /********TABLA************/

                lista_cuentas = cuentas;
                DateTime fecha_inicial;//fecha inicial
                DateTime fecha_final;// fecha final

                //lista_cuentas.Sort(delegate(Tipos_Dato.CuentasXPagar x, Tipos_Dato.CuentasXPagar y)
                //{
                //    if (x.Fecha == null && y.Fecha == null) return 0;
                //    else if (x.Fecha == null) return -1;
                //    else if (y.Fecha == null) return 1;
                //    else return x.Fecha.CompareTo(y.Fecha);
                //});

                string fecha_Actual;//fecha en la que se encuentra actualmente el temporal 
                string[] fecha = fechas.Split('-');
                fecha_inicial = Convert.ToDateTime(fecha[0]);
                fecha_final = Convert.ToDateTime(fecha[1]);
                //MessageBox.Show("Fecha  inicial " + fecha_inicial);
                //MessageBox.Show("Fecha  final " + fecha_final);

                instance_graficas.InitializeChart();//inicializar la graficacion de las tablas 
                int dia = 0;//dias en los que avanza la fecha temporal 
                DateTime tmp = fecha_inicial;// fecha temporal que ira aumentado hasta llegar a la fecha final 
                string band = ""; // bandera para saber si los datos de un mes ya estan impresos en el reporte 

                // imprime las tablas de cada mes 
                //MessageBox.Show("antes del while");
                while (tmp <= fecha_final)// mientras la fecha temporal es menor que la fecha final se incrementa de un dia en un dia hasta llegar a la fecha final 
                {
                    //MessageBox.Show("despues  del while");
                    PdfPTable tabla_cuentas_por_pagar1 = new PdfPTable(2); //        tabla_cuentas_por_pagar
                    tabla_cuentas_por_pagar1.WidthPercentage = 100;
                    // Configuramos el título de las columnas de la tabla
                    PdfPCell clFecha1 = new PdfPCell(new Phrase("Fecha", _standardFont));
                    clFecha1.BorderWidth = 0;
                    clFecha1.BorderWidthBottom = 0.75f;
                    PdfPCell clCompra = new PdfPCell(new Phrase("Compras por día " + mes[tmp.Month - 1], _standardFont));
                    clCompra.BorderWidth = 0;
                    clCompra.BorderWidthBottom = 0.75f;
                    fecha_Actual = tmp.Month.ToString();

                    // agrega las cabeceras a la tablas             
                    tabla_cuentas_por_pagar1.AddCell(clFecha1);
                    tabla_cuentas_por_pagar1.AddCell(clCompra);


                    for (int i = 0; i < lista_cuentas.Count; i++)// for 1
                    {
                        string[] words = lista_cuentas[i].fecha.Split(' ');
                        string[] words2 = words[0].Split('/');
                        //MessageBox.Show("dia "+words2[0] + " == " + tmp.Day);//Dia
                        //MessageBox.Show("mes "+words2[1] + " == " + tmp.Month);//mes
                        //MessageBox.Show("año "+words2[2] + " == " + tmp.Year);//año
                        if (int.Parse(words2[0]) == tmp.Day && int.Parse(words2[1]) == tmp.Month && int.Parse(words2[2]) == tmp.Year && band != fecha_Actual)// TRABAJO EN EL MES ACTUAL MIENTRAS NO SE HAYA IMPRESO ANTES 
                        {

                            instance_graficas.LoadBarChart_compras_dia(lista_cuentas, fecha_Actual, path);

                            // HACE LA GRAFICA CORRESPONDIENTE A ESE MES 

                            for (int k = 0; k < lista_cuentas.Count; k++)// for 2
                            {
                                //MessageBox.Show("" + lista_cuentas[k].Mes);
                                //MessageBox.Show("" + fecha_Actual);

                                if (Convert.ToInt32(words2[1]) == Convert.ToInt32(fecha_Actual)) // SI LA FECHA COINCIDE CON MES IMPRIMIR  LOS DATOS CORRESPONDIENTES A ESE MES 
                                {
                                    //   MessageBox.Show("condicion");

                                    clFecha1 = new PdfPCell(new Phrase(lista_cuentas[k].fecha, _standardFont));
                                    clFecha1.BorderWidth = 0;

                                    clCompra = new PdfPCell(new Phrase(lista_cuentas[k].Total.ToString(), _standardFont));
                                    clCompra.BorderWidth = 0;
                                    // Añadimos las celdas  con la informacion a la tabla
                                    tabla_cuentas_por_pagar1.AddCell(clFecha1);
                                    tabla_cuentas_por_pagar1.AddCell(clCompra);
                                }

                            }//for 2

                            //31_03_2014-06_05_2014  
                            #region agregar imagen

                            string imageFilePath = @path+"chart.png";
                            iTextSharp.text.Image jpg1 = iTextSharp.text.Image.GetInstance(imageFilePath);

                            //jpg.ScaleToFit(wdthfoto, heighfoto);
                            jpg1.WidthPercentage = 100;
                            jpg1.Alignment = Element.ALIGN_RIGHT;

                            #endregion







                            PdfPTable tabla_parrafo = new PdfPTable(2);
                            tabla_parrafo.WidthPercentage = 100;

                            PdfPCell cl1 = new PdfPCell(tabla_cuentas_por_pagar1);
                            cl1.BorderWidth = 0;
                            cl1.BorderWidthBottom = 0;

                            PdfPCell cl2 = new PdfPCell(jpg1);//imagen
                            cl2.BorderWidth = 0;
                            cl2.BorderWidthBottom = 0;
                            cl2.MinimumHeight = 250f;
                            cl2.FixedHeight = 250f;

                            tabla_parrafo.AddCell(cl1);
                            tabla_parrafo.AddCell(cl2);
                            // cierro la edicion del pdf
                            doc.Add(tabla_parrafo);
                            doc.Add(new Paragraph("\n"));
                            doc.Add(new Paragraph("\n"));


                            band = fecha_Actual;// ya se imprimio el mes 
                        }// if 



                    }// for 1

                    dia++;//cantidad de dias de avanza la fecha 
                    tmp = fecha_inicial.AddDays(dia);//avanza la fecha temporal 1 dia 
                }
                #endregion
                #endregion

                /************************************************************/
                /************************************************************/
                /************************************************************/
                #region COMPRAS POR SEMANA

                doc.Add(new Paragraph("(2) Reporte Compras por Semana", _titulos));
                doc.Add(new Paragraph("\n"));

                #region   /********TABLA************/

                lista_cuentas = cuentas;
                asignar_semana();

                instance_graficas.InitializeChart();//inicializar la graficacion de las tablas 
                dia = 0;//dias en los que avanza la fecha temporal 
                tmp = fecha_inicial;// fecha temporal que ira aumentado hasta llegar a la fecha final 
                band = ""; // bandera para saber si los datos de un mes ya estan impresos en el reporte 


                tabla_cuentas_por_pagar = new PdfPTable(3); //        tabla_cuentas_por_pagar
                tabla_cuentas_por_pagar.WidthPercentage = 100;
                // Configuramos el título de las columnas de la tabla
                clFecha = new PdfPCell(new Phrase("Fecha", _standardFont));
                clFecha.BorderWidth = 0;
                clFecha.BorderWidthBottom = 0.75f;
                PdfPCell clSemana = new PdfPCell(new Phrase("Semana", _standardFont));
                clSemana.BorderWidth = 0;
                clSemana.BorderWidthBottom = 0.75f;
                PdfPCell clComprad = new PdfPCell(new Phrase("Compras por día", _standardFont));
                clComprad.BorderWidth = 0;
                clComprad.BorderWidthBottom = 0.75f;
                fecha_Actual = tmp.Month.ToString();

                // Añadimos las cabeceras a la tabla
                tabla_cuentas_por_pagar.AddCell(clFecha);
                tabla_cuentas_por_pagar.AddCell(clSemana);
                tabla_cuentas_por_pagar.AddCell(clComprad);

                // imprime las tablas de cada mes 
                while (tmp <= fecha_final)// mientras la fecha temporal es menor que la fecha final se incrementa de un dia en un dia hasta llegar a la fecha final 
                {


                    for (int i = 0; i < lista_cuentas.Count; i++)
                    {

                        string[] words = lista_cuentas[i].fecha.Split(' ');
                        string[] words2 = words[0].Split('/');
                        if (int.Parse(words2[0]) == tmp.Day && int.Parse(words2[1]) == tmp.Month && int.Parse(words2[2]) == tmp.Year)// && band != fecha_Actual)// TRABAJO EN EL MES ACTUAL MIENTRAS NO SE HAYA IMPRESO ANTES 
                        {
                            clFecha = new PdfPCell(new Phrase(lista_cuentas[i].fecha, _standardFont));
                            clFecha.BorderWidth = 0;

                            /****calcula la semana****/
                            DateTime date = new DateTime(tmp.Year, tmp.Month, tmp.Day);
                            System.Globalization.CultureInfo norwCulture =
                            System.Globalization.CultureInfo.CreateSpecificCulture("es");
                            System.Globalization.Calendar cal = norwCulture.Calendar;
                            int weekNo = cal.GetWeekOfYear(date,
                            norwCulture.DateTimeFormat.CalendarWeekRule,
                            norwCulture.DateTimeFormat.FirstDayOfWeek);
                            // Show the result
                            clSemana = new PdfPCell(new Phrase(weekNo.ToString()));
                            clSemana.BorderWidth = 0;
                            /*********************/

                            clComprad = new PdfPCell(new Phrase(lista_cuentas[i].Total.ToString(), _standardFont));
                            clComprad.BorderWidth = 0;

                            // Añadimos la informacion  a la tabla
                            tabla_cuentas_por_pagar.AddCell(clFecha);
                            tabla_cuentas_por_pagar.AddCell(clSemana);
                            tabla_cuentas_por_pagar.AddCell(clComprad);

                            band = fecha_Actual;// ya se imprimio el mes 
                        }

                    }

                    dia++;//cantidad de dias de avanza la fecha 
                    tmp = fecha_inicial.AddDays(dia);//avanza la fecha temporal 1 dia 
                }


                instance_graficas.LoadBarChart_ComprasPorSemana(lista_cuentas, fechas, path);

                #region agregar imagen

                string imageFilePaths = @path+"chart.png";
                iTextSharp.text.Image jpg = iTextSharp.text.Image.GetInstance(imageFilePaths);

                //jpg.ScaleToFit(wdthfoto, heighfoto);
                jpg.WidthPercentage = 100;
                jpg.Alignment = Element.ALIGN_RIGHT;

                #endregion

                PdfPTable tabla_parrafod = new PdfPTable(2);
                tabla_parrafod.WidthPercentage = 100;

                PdfPCell cl1d = new PdfPCell(tabla_cuentas_por_pagar);
                cl1d.BorderWidth = 0;
                cl1d.BorderWidthBottom = 0;

                PdfPCell cl2s = new PdfPCell(jpg);
                cl2s.BorderWidth = 0;
                cl2s.BorderWidthBottom = 0;
                cl2s.MinimumHeight = 250f;
                cl2s.FixedHeight = 250f;

                tabla_parrafod.AddCell(cl1d);
                tabla_parrafod.AddCell(cl2s);
                //cierro la edicion del pdf
                doc.Add(tabla_parrafod);
                #endregion

                #endregion


                /************************************************************/
                /************************************************************/
                /************************************************************/
                #region compras por mes
                doc.Add(new Paragraph("(3) Reporte Compras por Mes", _titulos));
                doc.Add(new Paragraph("\n"));







                #region   /********TABLA************/

                lista_cuentas = cuentas;


                instance_graficas.InitializeChart();//inicializar la graficacion de las tablas 



                tabla_cuentas_por_pagar = new PdfPTable(2); //        tabla_cuentas_por_pagar
                tabla_cuentas_por_pagar.WidthPercentage = 100;
                // Configuramos el título de las columnas de la tabla
                clFecha = new PdfPCell(new Phrase("Mes", _standardFont));
                clFecha.BorderWidth = 0;
                clFecha.BorderWidthBottom = 0.75f;

                PdfPCell clComprads = new PdfPCell(new Phrase("Compras por Mes", _standardFont));
                clComprads.BorderWidth = 0;
                clComprads.BorderWidthBottom = 0.75f;


                // Añadimos las cabeceras a la tabla
                tabla_cuentas_por_pagar.AddCell(clFecha);
                tabla_cuentas_por_pagar.AddCell(clComprads);
                float total_mes = 0;
                // imprime las tablas de cada mes 
                for (int j = 0; j < 12; j++)
                {


                    for (int i = 0; i < lista_cuentas.Count; i++)
                    {
                        string[] words = lista_cuentas[i].fecha.Split(' ');//separa la fecha de la hora
                        string[] words2 = words[0].Split('/');//separa la fecha en [dia]/[mes]/[año]
                        if (int.Parse(words2[1]) == j + 1)// mes
                        {
                            total_mes = total_mes + lista_cuentas[i].Total;
                        }

                    }
                    clFecha = new PdfPCell(new Phrase(mes[j], _standardFont));
                    clFecha.BorderWidth = 0;

                    clComprads = new PdfPCell(new Phrase(total_mes.ToString(), _standardFont));
                    clComprads.BorderWidth = 0;

                    // Añadimos la informacion  a la tabla
                    tabla_cuentas_por_pagar.AddCell(clFecha);

                    tabla_cuentas_por_pagar.AddCell(clComprads);
                    total_mes = 0;

                }


                instance_graficas.LoadBarChart_ComprasPorMes(lista_cuentas, path);
                //31_03_2014-06_05_2014  
                #region agregar imagen

                string imageFilePathsd = @path+"chart.png";
                jpg = iTextSharp.text.Image.GetInstance(imageFilePathsd);

                //jpg.ScaleToFit(wdthfoto, heighfoto);
                jpg.WidthPercentage = 100;
                jpg.Alignment = Element.ALIGN_RIGHT;

                #endregion

                PdfPTable tabla_parrafosd = new PdfPTable(2);
                tabla_parrafosd.WidthPercentage = 100;

                PdfPCell cl1sd = new PdfPCell(tabla_cuentas_por_pagar);
                cl1sd.BorderWidth = 0;
                cl1sd.BorderWidthBottom = 0;

                PdfPCell cl2sd = new PdfPCell(jpg);//imagen 
                cl2sd.BorderWidth = 0;
                cl2sd.BorderWidthBottom = 0;
                cl2sd.MinimumHeight = 250f;
                cl2sd.FixedHeight = 250f;

                tabla_parrafosd.AddCell(cl1sd);
                tabla_parrafosd.AddCell(cl2sd);
                //cierro la edicion del pdf
                doc.Add(tabla_parrafosd);
                #endregion
                #endregion
                /************************************************************/
                /************************************************************/
                /************************************************************/

                #region Clasificacion1 Proveedores  GRAFICAS
                //MessageBox.Show("crear gradicas");
                creaGraficasClasificacion1Proveedores(path);
                //MessageBox.Show("acabo de crear  gradicas");
                doc.Add(new Paragraph("(4) Reporte Compras Mensuales por clasificación 1 Proveedores", _titulos));
                doc.Add(new Paragraph("\n"));

                //MessageBox.Show("metio reporte");



                // iTextSharp.text.Image jpg;
                //MessageBox.Show("antes del for");
                for (int i = 0; i < PDFProveedoresClasificacion1.Count; i++)
                {


                    #region   /********SEGUNDA TABLA************/


                    //MessageBox.Show("primer tabla");
                    PdfPTable tabla_cuentas_por_pagar1 = new PdfPTable(2); //        tabla_cuentas_por_pagar
                    tabla_cuentas_por_pagar1.WidthPercentage = 100;

                    //MessageBox.Show("titulois1");
                    // Configuramos el título de las columnas de la tabla
                    PdfPCell clFecha1 = new PdfPCell(new Phrase("Fecha", _standardFont));
                    clFecha1.BorderWidth = 0;
                    clFecha1.BorderWidthBottom = 0.75f;
                    //MessageBox.Show("titulois2");
                    PdfPCell clCompra = new PdfPCell(new Phrase("Compras por dia (" + PDFProveedoresClasificacion1[i].Clasificacion1.TrimEnd(' ') + ")", _standardFont));
                    clCompra.BorderWidth = 0;
                    clCompra.BorderWidthBottom = 0.75f;

                    tabla_cuentas_por_pagar1.AddCell(clFecha1);
                    tabla_cuentas_por_pagar1.AddCell(clCompra);
                    int meselegido = 0;
                    //MessageBox.Show("segundo for");
                    for (int l = 1; l < 13; l++)
                    {
                        meselegido = 0;

                        for (int j = 0; j < PDFProveedoresClasificacion1[i].compras.Count; j++)
                        {

                            //MessageBox.Show("dentro segundo for");
                            // List<ComprasMensualesXClasificacion> lista_cuentas = cuentas[i].compras;
                            //for (int k = 0; k < lista_cuentas.Count; k++)
                            //{
                            // Añadimos las celdas a la tabla

                            if (l == Convert.ToInt32(PDFProveedoresClasificacion1[i].compras[j].Mes))
                            {
                                //MessageBox.Show("sgrega fecha");
                                clFecha = new PdfPCell(new Phrase(mes[Convert.ToInt32(PDFProveedoresClasificacion1[i].compras[j].Mes) - 1], _standardFont));
                                clFecha.BorderWidth = 0;

                                //MessageBox.Show("sgrega total");
                                clCompra = new PdfPCell(new Phrase(PDFProveedoresClasificacion1[i].compras[j].total.ToString(), _standardFont));
                                clCompra.BorderWidth = 0;

                                tabla_cuentas_por_pagar1.AddCell(clFecha);
                                tabla_cuentas_por_pagar1.AddCell(clCompra);
                                meselegido = 1;
                                break;
                            }// }//for
                        }
                        if (meselegido == 0)
                        {
                            clFecha = new PdfPCell(new Phrase(mes[l - 1], _standardFont));
                            clFecha.BorderWidth = 0;

                            //MessageBox.Show("sgrega total");
                            clCompra = new PdfPCell(new Phrase("0", _standardFont));
                            clCompra.BorderWidth = 0;

                            tabla_cuentas_por_pagar1.AddCell(clFecha);
                            tabla_cuentas_por_pagar1.AddCell(clCompra);
                        }
                    }
                    //MessageBox.Show("añadelos");
                    // Añadimos las celdas a la tabla
                    //tabla_cuentas_por_pagar1.AddCell(clFecha);
                    //tabla_cuentas_por_pagar1.AddCell(clCompra);

                    #endregion

                    #region agregar imagen
                    //MessageBox.Show("saza imagenes");
                    string imageFilePath = @path + PDFProveedoresClasificacion1[i].nombreimagen + ".png";
                    jpg = iTextSharp.text.Image.GetInstance(imageFilePath);

                    // jpg.ScaleToFit(wdthfoto, heighfoto);//130
                    jpg.WidthPercentage = 100;
                    jpg.Alignment = Element.ALIGN_RIGHT;
                    //MessageBox.Show("acxtualizoc imagenes");
                    #endregion

                    PdfPTable tabla_parrafo = new PdfPTable(2);
                    tabla_parrafo.WidthPercentage = 100;


                    PdfPCell cl1 = new PdfPCell(tabla_cuentas_por_pagar1);
                    cl1.BorderWidth = 0;
                    cl1.BorderWidthBottom = 0;

                    PdfPCell cl2 = new PdfPCell(jpg);
                    cl2.BorderWidth = 0;
                    cl2.BorderWidthBottom = 0;
                    cl2.MinimumHeight = 250f;
                    cl2.FixedHeight = 250f;

                    tabla_parrafo.AddCell(cl1);
                    tabla_parrafo.AddCell(cl2);

                    doc.Add(tabla_parrafo);
                    doc.Add(new Paragraph("\n"));
                    doc.Add(new Paragraph("\n"));
                    doc.Add(new Paragraph("\n"));
                }
                //cierro la edicion del pdf

                #endregion

                /************************************************************/
                /************************************************************/
                /************************************************************/

                #region Clasificacion2 Proveedores  GRAFICAS
                //MessageBox.Show("crear gradicas");
                creaGraficasClasificacion2Proveedores(path);
                //MessageBox.Show("acabo de crear  gradicas");
                doc.Add(new Paragraph("(5) Reporte Compras Mensuales por clasificación 2 Proveedores", _titulos));
                doc.Add(new Paragraph("\n"));

                //MessageBox.Show("metio reporte");



                //iTextSharp.text.Image jpg;
                //MessageBox.Show("antes del for");
                for (int i = 0; i < PDFProveedoresClasificacion2.Count; i++)
                {


                    #region   /********SEGUNDA TABLA************/


                    //MessageBox.Show("primer tabla");
                    PdfPTable tabla_cuentas_por_pagar1 = new PdfPTable(2); //        tabla_cuentas_por_pagar
                    tabla_cuentas_por_pagar1.WidthPercentage = 100;

                    //MessageBox.Show("titulois1");
                    // Configuramos el título de las columnas de la tabla
                    PdfPCell clFecha1 = new PdfPCell(new Phrase("Fecha", _standardFont));
                    clFecha1.BorderWidth = 0;
                    clFecha1.BorderWidthBottom = 0.75f;
                    //MessageBox.Show("titulois2");
                    PdfPCell clCompra = new PdfPCell(new Phrase("Compras por dia (" + PDFProveedoresClasificacion2[i].Clasificacion2.TrimEnd(' ') + ")", _standardFont));
                    clCompra.BorderWidth = 0;
                    clCompra.BorderWidthBottom = 0.75f;
                    tabla_cuentas_por_pagar1.AddCell(clFecha1);
                    tabla_cuentas_por_pagar1.AddCell(clCompra);
                    int meselegido = 0;
                    for (int l = 1; l < 13; l++)
                    {
                        meselegido = 0;
                        //MessageBox.Show("segundo for");
                        for (int j = 0; j < PDFProveedoresClasificacion2[i].compras.Count; j++)
                        {

                            //MessageBox.Show("dentro segundo for");
                            // List<ComprasMensualesXClasificacion> lista_cuentas = cuentas[i].compras;
                            //for (int k = 0; k < lista_cuentas.Count; k++)
                            //{
                            // Añadimos las celdas a la tabla

                            if (l == Convert.ToInt32(PDFProveedoresClasificacion2[i].compras[j].Mes))
                            {
                                //MessageBox.Show("sgrega fecha");
                                clFecha1 = new PdfPCell(new Phrase(mes[Convert.ToInt32(PDFProveedoresClasificacion2[i].compras[j].Mes) - 1], _standardFont));
                                clFecha1.BorderWidth = 0;

                                //MessageBox.Show("sgrega total");
                                clCompra = new PdfPCell(new Phrase(PDFProveedoresClasificacion2[i].compras[j].total.ToString(), _standardFont));
                                clCompra.BorderWidth = 0;

                                tabla_cuentas_por_pagar1.AddCell(clFecha1);
                                tabla_cuentas_por_pagar1.AddCell(clCompra);
                                // }//for
                                meselegido = 1;
                                break;
                            }
                        }
                        if (meselegido == 0)
                        {
                            clFecha1 = new PdfPCell(new Phrase(mes[l - 1], _standardFont));
                            clFecha1.BorderWidth = 0;

                            //MessageBox.Show("sgrega total");
                            clCompra = new PdfPCell(new Phrase("0", _standardFont));
                            clCompra.BorderWidth = 0;

                            tabla_cuentas_por_pagar1.AddCell(clFecha1);
                            tabla_cuentas_por_pagar1.AddCell(clCompra);
                        }
                    }

                    //MessageBox.Show("añadelos");
                    // Añadimos las celdas a la tabla
                    //tabla_cuentas_por_pagar1.AddCell(clFecha1);
                    //tabla_cuentas_por_pagar1.AddCell(clCompra);

                    #endregion

                    #region agregar imagen
                    //MessageBox.Show("saza imagenes");
                    string imageFilePath = @path + PDFProveedoresClasificacion2[i].nombreimagen + ".png";
                    jpg = iTextSharp.text.Image.GetInstance(imageFilePath);

                    //jpg.ScaleToFit(wdthfoto, heighfoto);//130
                    jpg.WidthPercentage = 100;
                    jpg.Alignment = Element.ALIGN_RIGHT;
                    //MessageBox.Show("acxtualizoc imagenes");
                    #endregion

                    PdfPTable tabla_parrafo = new PdfPTable(2);
                    tabla_parrafo.WidthPercentage = 100;


                    PdfPCell cl1 = new PdfPCell(tabla_cuentas_por_pagar1);
                    cl1.BorderWidth = 0;
                    cl1.BorderWidthBottom = 0;

                    PdfPCell cl2 = new PdfPCell(jpg);
                    cl2.BorderWidth = 0;
                    cl2.BorderWidthBottom = 0;
                    cl2.MinimumHeight = 250f;
                    cl2.FixedHeight = 250f;

                    tabla_parrafo.AddCell(cl1);
                    tabla_parrafo.AddCell(cl2);

                    doc.Add(tabla_parrafo);
                    doc.Add(new Paragraph("\n"));
                    doc.Add(new Paragraph("\n"));
                    doc.Add(new Paragraph("\n"));
                }
                //cierro la edicion del pdf

                #endregion

                /*************************************************************************/
                /*************************************************************************/
                /*************************************************************************/

                #region Clasificacion1 Productos  GRAFICAS
                //MessageBox.Show("crear gradicas");
                CreargraficasClasificacion1productos(path);
                //MessageBox.Show("acabo de crear  gradicas");
                doc.Add(new Paragraph("(6) Reporte Compras Mensuales por clasificación 1 Productos", _titulos));
                doc.Add(new Paragraph("\n"));

                //MessageBox.Show("metio reporte");



                //iTextSharp.text.Image jpg;
                //MessageBox.Show("antes del for");
                for (int i = 0; i < PDFProveedoresClasificacion1Productos.Count; i++)
                {


                    #region   /********SEGUNDA TABLA************/


                    //MessageBox.Show("primer tabla");
                    PdfPTable tabla_cuentas_por_pagar1 = new PdfPTable(2); //        tabla_cuentas_por_pagar
                    tabla_cuentas_por_pagar1.WidthPercentage = 100;

                    //MessageBox.Show("titulois1");
                    // Configuramos el título de las columnas de la tabla
                    PdfPCell clFecha1 = new PdfPCell(new Phrase("Fecha", _standardFont));
                    clFecha1.BorderWidth = 0;
                    clFecha1.BorderWidthBottom = 0.75f;
                    //MessageBox.Show("titulois2");
                    PdfPCell clCompra = new PdfPCell(new Phrase("Compras por dia (" + PDFProveedoresClasificacion1Productos[i].Clasificacion1.TrimEnd(' ') + ")", _standardFont));
                    clCompra.BorderWidth = 0;
                    clCompra.BorderWidthBottom = 0.75f;
                    tabla_cuentas_por_pagar1.AddCell(clFecha1);
                    tabla_cuentas_por_pagar1.AddCell(clCompra);
                    int meselegido = 0;
                    for (int l = 1; l < 13; l++)
                    {
                        meselegido = 0;
                        //MessageBox.Show("segundo for");
                        for (int j = 0; j < PDFProveedoresClasificacion1Productos[i].compras.Count; j++)
                        {

                            //MessageBox.Show("dentro segundo for");
                            // List<ComprasMensualesXClasificacion> lista_cuentas = cuentas[i].compras;
                            //for (int k = 0; k < lista_cuentas.Count; k++)
                            //{
                            // Añadimos las celdas a la tabla


                            if (l == Convert.ToInt32(PDFProveedoresClasificacion1Productos[i].compras[j].Mes))
                            {
                                //MessageBox.Show("sgrega fecha");
                                clFecha1 = new PdfPCell(new Phrase(mes[Convert.ToInt32(PDFProveedoresClasificacion1Productos[i].compras[j].Mes) - 1], _standardFont));
                                clFecha1.BorderWidth = 0;

                                //MessageBox.Show("sgrega total");
                                clCompra = new PdfPCell(new Phrase(PDFProveedoresClasificacion1Productos[i].compras[j].total.ToString(), _standardFont));
                                clCompra.BorderWidth = 0;
                                tabla_cuentas_por_pagar1.AddCell(clFecha1);
                                tabla_cuentas_por_pagar1.AddCell(clCompra);
                                meselegido = 1;
                                break;
                            }
                            // }//for
                        }
                        if (meselegido == 0)
                        {
                            clFecha1 = new PdfPCell(new Phrase(mes[Convert.ToInt32(l - 1)], _standardFont));
                            clFecha1.BorderWidth = 0;

                            //MessageBox.Show("sgrega total");
                            clCompra = new PdfPCell(new Phrase("0", _standardFont));
                            clCompra.BorderWidth = 0;
                            tabla_cuentas_por_pagar1.AddCell(clFecha1);
                            tabla_cuentas_por_pagar1.AddCell(clCompra);
                        }
                    }
                    //MessageBox.Show("añadelos");
                    // Añadimos las celdas a la tabla
                    //tabla_cuentas_por_pagar1.AddCell(clFecha1);
                    //tabla_cuentas_por_pagar1.AddCell(clCompra);

                    #endregion

                    #region agregar imagen
                    //MessageBox.Show("saza imagenes");
                    string imageFilePath = @path + PDFProveedoresClasificacion1Productos[i].nombreimagen + ".png";
                    jpg = iTextSharp.text.Image.GetInstance(imageFilePath);

                    //jpg.ScaleToFit(wdthfoto, heighfoto);//130
                    jpg.WidthPercentage = 100;
                    jpg.Alignment = Element.ALIGN_RIGHT;
                    //MessageBox.Show("acxtualizoc imagenes");
                    #endregion

                    PdfPTable tabla_parrafo = new PdfPTable(2);
                    tabla_parrafo.WidthPercentage = 100;


                    PdfPCell cl1 = new PdfPCell(tabla_cuentas_por_pagar1);
                    cl1.BorderWidth = 0;
                    cl1.BorderWidthBottom = 0;

                    PdfPCell cl2 = new PdfPCell(jpg);
                    cl2.BorderWidth = 0;
                    cl2.BorderWidthBottom = 0;
                    cl2.MinimumHeight = 250f;
                    cl2.FixedHeight = 250f;

                    tabla_parrafo.AddCell(cl1);
                    tabla_parrafo.AddCell(cl2);

                    doc.Add(tabla_parrafo);
                    doc.Add(new Paragraph("\n"));
                    doc.Add(new Paragraph("\n"));
                    doc.Add(new Paragraph("\n"));
                }
                //cierro la edicion del pdf

                #endregion
                /*************************************************************************/
                /*************************************************************************/
                /*************************************************************************/
                /*************************************************************************/
                #region Clasificacion1 Productos  GRAFICAS
                //MessageBox.Show("crear gradicas");
                CrearGraficasCalsifiacion2Productos(path);
                //MessageBox.Show("acabo de crear  gradicas");
                doc.Add(new Paragraph("(7) Reporte Compras Mensuales por clasificación 2 Productos", _titulos));
                doc.Add(new Paragraph("\n"));

                //MessageBox.Show("metio reporte");



                //iTextSharp.text.Image jpg;
                //MessageBox.Show("antes del for");
                for (int i = 0; i < PDFProveedoresClasificacion2Productos.Count; i++)
                {


                    #region   /********SEGUNDA TABLA************/


                    //MessageBox.Show("primer tabla");
                    PdfPTable tabla_cuentas_por_pagar1 = new PdfPTable(2); //        tabla_cuentas_por_pagar
                    tabla_cuentas_por_pagar1.WidthPercentage = 100;

                    //MessageBox.Show("titulois1");
                    // Configuramos el título de las columnas de la tabla
                    PdfPCell clFecha1 = new PdfPCell(new Phrase("Fecha", _standardFont));
                    clFecha1.BorderWidth = 0;
                    clFecha1.BorderWidthBottom = 0.75f;
                    //MessageBox.Show("titulois2");
                    PdfPCell clCompra = new PdfPCell(new Phrase("Compras por dia (" + PDFProveedoresClasificacion2Productos[i].Clasificacion2.TrimEnd(' ') + ")", _standardFont));
                    clCompra.BorderWidth = 0;
                    clCompra.BorderWidthBottom = 0.75f;
                    tabla_cuentas_por_pagar1.AddCell(clFecha1);
                    tabla_cuentas_por_pagar1.AddCell(clCompra);
                    int meselegido = 0;
                    for (int l = 1; l < 13; l++)
                    {
                        meselegido = 0;
                        //MessageBox.Show("segundo for");
                        for (int j = 0; j < PDFProveedoresClasificacion2Productos[i].compras.Count; j++)
                        {

                            //MessageBox.Show("dentro segundo for");
                            // List<ComprasMensualesXClasificacion> lista_cuentas = cuentas[i].compras;
                            //for (int k = 0; k < lista_cuentas.Count; k++)
                            //{
                            // Añadimos las celdas a la tabla

                            if (l == Convert.ToInt32(PDFProveedoresClasificacion2Productos[i].compras[j].Mes))
                            {
                                //MessageBox.Show("sgrega fecha");
                                clFecha1 = new PdfPCell(new Phrase(mes[Convert.ToInt32(PDFProveedoresClasificacion2Productos[i].compras[j].Mes) - 1], _standardFont));
                                clFecha1.BorderWidth = 0;

                                //MessageBox.Show("sgrega total");
                                clCompra = new PdfPCell(new Phrase(PDFProveedoresClasificacion2Productos[i].compras[j].total.ToString(), _standardFont));
                                clCompra.BorderWidth = 0;
                                tabla_cuentas_por_pagar1.AddCell(clFecha1);
                                tabla_cuentas_por_pagar1.AddCell(clCompra);
                                meselegido = 1;
                                break;
                            }
                            // }//for
                        }
                        if (meselegido == 0)
                        {
                            clFecha1 = new PdfPCell(new Phrase(mes[l - 1], _standardFont));
                            clFecha1.BorderWidth = 0;

                            //MessageBox.Show("sgrega total");
                            clCompra = new PdfPCell(new Phrase("0", _standardFont));
                            clCompra.BorderWidth = 0;
                            tabla_cuentas_por_pagar1.AddCell(clFecha1);
                            tabla_cuentas_por_pagar1.AddCell(clCompra);
                        }
                    }
                    //MessageBox.Show("añadelos");
                    // Añadimos las celdas a la tabla
                    //tabla_cuentas_por_pagar1.AddCell(clFecha1);
                    //tabla_cuentas_por_pagar1.AddCell(clCompra);

                    #endregion

                    #region agregar imagen
                    //MessageBox.Show("saza imagenes");
                    string imageFilePath = @path + PDFProveedoresClasificacion2Productos[i].nombreimagen + ".png";
                    jpg = iTextSharp.text.Image.GetInstance(imageFilePath);

                    //jpg.ScaleToFit(wdthfoto, heighfoto);//130
                    jpg.WidthPercentage = 100;
                    jpg.Alignment = Element.ALIGN_CENTER;
                    //MessageBox.Show("acxtualizoc imagenes");
                    #endregion

                    PdfPTable tabla_parrafo = new PdfPTable(2);
                    tabla_parrafo.WidthPercentage = 100;


                    PdfPCell cl1 = new PdfPCell(tabla_cuentas_por_pagar1);
                    cl1.BorderWidth = 0;
                    cl1.BorderWidthBottom = 0;

                    PdfPCell cl2 = new PdfPCell(jpg);
                    cl2.BorderWidth = 0;
                    cl2.BorderWidthBottom = 0;
                    cl2.MinimumHeight = 250f;
                    cl2.FixedHeight = 250f;

                    tabla_parrafo.AddCell(cl1);
                    tabla_parrafo.AddCell(cl2);

                    doc.Add(tabla_parrafo);
                    doc.Add(new Paragraph("\n"));
                    doc.Add(new Paragraph("\n"));
                    doc.Add(new Paragraph("\n"));
                }
                //cierro la edicion del pdf

                #endregion
                /******************************************************************************************/
                /******************************************************************************************/
                /******************************************************************************************/
                /******************************************************************************************/
                /******************************************************************************************/
                #region Clasificacion1 Productos  GRAFICAS por Mes
                //MessageBox.Show("crear gradicas");
                Graficarclasificacion1ProductoXMes(path);
                //MessageBox.Show("acabo de crear  gradicas");
                doc.Add(new Paragraph("(8) Reporte Compras Mensuales por clasificación 1 Productos por Mes", _titulos));
                doc.Add(new Paragraph("\n"));

                //MessageBox.Show("metio reporte");



                //iTextSharp.text.Image jpg;
                //MessageBox.Show("antes del for");
                for (int i = 0; i < PDFClasificacion1PRoductoMes.Count; i++)
                {


                    #region   /********SEGUNDA TABLA************/


                    //MessageBox.Show("primer tabla");
                    PdfPTable tabla_cuentas_por_pagar1 = new PdfPTable(2); //        tabla_cuentas_por_pagar
                    tabla_cuentas_por_pagar1.WidthPercentage = 100;

                    //MessageBox.Show("titulois1");
                    // Configuramos el título de las columnas de la tabla
                    PdfPCell clFecha1 = new PdfPCell(new Phrase("Fecha", _standardFont));
                    clFecha1.BorderWidth = 0;
                    clFecha1.BorderWidthBottom = 0.75f;
                    //MessageBox.Show("titulois2");
                    PdfPCell clCompra = new PdfPCell(new Phrase("(" + PDFClasificacion1PRoductoMes[i].Mes.TrimEnd(' ') + ") Por casificación 1 de Producto por mes ", _standardFont));
                    clCompra.BorderWidth = 0;
                    clCompra.BorderWidthBottom = 0.75f;


                    //MessageBox.Show("segundo for");
                    for (int j = 0; j < PDFClasificacion1PRoductoMes[i].compras.Count; j++)
                    {

                        //MessageBox.Show("dentro segundo for");
                        // List<ComprasMensualesXClasificacion> lista_cuentas = cuentas[i].compras;
                        //for (int k = 0; k < lista_cuentas.Count; k++)
                        //{
                        // Añadimos las celdas a la tabla
                        tabla_cuentas_por_pagar1.AddCell(clFecha1);
                        tabla_cuentas_por_pagar1.AddCell(clCompra);

                        //MessageBox.Show("sgrega fecha");
                        clFecha1 = new PdfPCell(new Phrase(PDFClasificacion1PRoductoMes[i].compras[j].Clasificacion1.TrimEnd(' '), _standardFont));
                        clFecha1.BorderWidth = 0;

                        //MessageBox.Show("sgrega total");
                        clCompra = new PdfPCell(new Phrase(PDFClasificacion1PRoductoMes[i].compras[j].total.ToString() + " (" + PDFClasificacion1PRoductoMes[i].compras[j].Clasificacion1.TrimEnd(' ') + ")", _standardFont));
                        clCompra.BorderWidth = 0;


                        // }//for
                    }
                    //MessageBox.Show("añadelos");
                    // Añadimos las celdas a la tabla
                    tabla_cuentas_por_pagar1.AddCell(clFecha1);
                    tabla_cuentas_por_pagar1.AddCell(clCompra);

                    #endregion

                    #region agregar imagen
                    //MessageBox.Show("saza imagenes");
                    string imageFilePath = @path + PDFClasificacion1PRoductoMes[i].nombreimagen + ".png";
                    jpg = iTextSharp.text.Image.GetInstance(imageFilePath);

                    //jpg.ScaleToFit(wdthfoto, heighfoto);//130
                    jpg.WidthPercentage = 100;
                    jpg.Alignment = Element.ALIGN_RIGHT;
                    //MessageBox.Show("acxtualizoc imagenes");
                    #endregion

                    PdfPTable tabla_parrafo = new PdfPTable(2);
                    tabla_parrafo.WidthPercentage = 100;


                    PdfPCell cl1 = new PdfPCell(tabla_cuentas_por_pagar1);
                    cl1.BorderWidth = 0;
                    cl1.BorderWidthBottom = 0;

                    PdfPCell cl2 = new PdfPCell(jpg);
                    cl2.BorderWidth = 0;
                    cl2.BorderWidthBottom = 0;
                    cl2.MinimumHeight = 250f;
                    cl2.FixedHeight = 250f;

                    tabla_parrafo.AddCell(cl1);
                    tabla_parrafo.AddCell(cl2);

                    doc.Add(tabla_parrafo);
                    doc.Add(new Paragraph("\n"));
                    doc.Add(new Paragraph("\n"));
                    doc.Add(new Paragraph("\n"));
                }
                //cierro la edicion del pdf

                #endregion
                /******************************************************************************************/
                /******************************************************************************************/
                /******************************************************************************************/
                #region Clasificacion2 Productos  GRAFICAS por Mes
                //MessageBox.Show("crear gradicas");
                Graficarclasificacion2XMes(path);
                //MessageBox.Show("acabo de crear  gradicas");
                doc.Add(new Paragraph("(9) Reporte Compras Mensuales por clasificación 2 Productos por Mes", _titulos));
                doc.Add(new Paragraph("\n"));

                //MessageBox.Show("metio reporte");



                //iTextSharp.text.Image jpg;
                //MessageBox.Show("antes del for");
                for (int i = 0; i < PDFClasificacion2PRoductoMes.Count; i++)
                {


                    #region   /********SEGUNDA TABLA************/


                    //MessageBox.Show("primer tabla");
                    PdfPTable tabla_cuentas_por_pagar1 = new PdfPTable(2); //        tabla_cuentas_por_pagar
                    tabla_cuentas_por_pagar1.WidthPercentage = 100;

                    //MessageBox.Show("titulois1");
                    // Configuramos el título de las columnas de la tabla
                    PdfPCell clFecha1 = new PdfPCell(new Phrase("Fecha", _standardFont));
                    clFecha1.BorderWidth = 0;
                    clFecha1.BorderWidthBottom = 0.75f;
                    //MessageBox.Show("titulois2");
                    PdfPCell clCompra = new PdfPCell(new Phrase("(" + PDFClasificacion2PRoductoMes[i].Mes.TrimEnd(' ') + ") Por casificación 1 de Producto por mes ", _standardFont));
                    clCompra.BorderWidth = 0;
                    clCompra.BorderWidthBottom = 0.75f;


                    //MessageBox.Show("segundo for");
                    for (int j = 0; j < PDFClasificacion2PRoductoMes[i].compras.Count; j++)
                    {

                        //MessageBox.Show("dentro segundo for");
                        // List<ComprasMensualesXClasificacion> lista_cuentas = cuentas[i].compras;
                        //for (int k = 0; k < lista_cuentas.Count; k++)
                        //{
                        // Añadimos las celdas a la tabla
                        tabla_cuentas_por_pagar1.AddCell(clFecha1);
                        tabla_cuentas_por_pagar1.AddCell(clCompra);

                        //MessageBox.Show("sgrega fecha");
                        clFecha1 = new PdfPCell(new Phrase(PDFClasificacion2PRoductoMes[i].compras[j].Clasificacion2.TrimEnd(' '), _standardFont));
                        clFecha1.BorderWidth = 0;

                        //MessageBox.Show("sgrega total");
                        clCompra = new PdfPCell(new Phrase(PDFClasificacion2PRoductoMes[i].compras[j].total.ToString() + " (" + PDFClasificacion2PRoductoMes[i].compras[j].Clasificacion2.TrimEnd(' ') + ")", _standardFont));
                        clCompra.BorderWidth = 0;


                        // }//for
                    }
                    //MessageBox.Show("añadelos");
                    // Añadimos las celdas a la tabla
                    tabla_cuentas_por_pagar1.AddCell(clFecha1);
                    tabla_cuentas_por_pagar1.AddCell(clCompra);

                    #endregion

                    #region agregar imagen
                    //MessageBox.Show("saza imagenes");
                    string imageFilePath = @path + PDFClasificacion2PRoductoMes[i].nombreimagen + ".png";
                    jpg = iTextSharp.text.Image.GetInstance(imageFilePath);

                    //jpg.ScaleToFit(wdthfoto, heighfoto);//130
                    jpg.WidthPercentage = 100;
                    jpg.Alignment = Element.ALIGN_RIGHT;
                    //MessageBox.Show("acxtualizoc imagenes");
                    #endregion

                    PdfPTable tabla_parrafo = new PdfPTable(2);
                    tabla_parrafo.WidthPercentage = 100;


                    PdfPCell cl1 = new PdfPCell(tabla_cuentas_por_pagar1);
                    cl1.BorderWidth = 0;
                    cl1.BorderWidthBottom = 0;

                    PdfPCell cl2 = new PdfPCell(jpg);
                    cl2.BorderWidth = 0;
                    cl2.BorderWidthBottom = 0;
                    cl2.MinimumHeight = 250f;
                    cl2.FixedHeight = 250f;

                    tabla_parrafo.AddCell(cl1);
                    tabla_parrafo.AddCell(cl2);

                    doc.Add(tabla_parrafo);
                    doc.Add(new Paragraph("\n"));
                    doc.Add(new Paragraph("\n"));
                    doc.Add(new Paragraph("\n"));
                }
                //cierro la edicion del pdf

                #endregion
                /******************************************************************************************/
                /******************************************************************************************/
                // cierro la edicion del pdf
                doc.Close();

                //LO EJECUTO
                Process prc = new System.Diagnostics.Process();
                prc.StartInfo.FileName = rut;
                prc.Start();


            }
            catch (Exception g)
            {
                MessageBox.Show("" + g.Message);
            }
        }


        #endregion

        #region ASIGNA NUMERO DE SEMANA
        public void asignar_semana()
        {
            string[] words = lista_cuentas[0].fecha.Split(' ');//separa la fecha de la hora
            string[] words2 = words[0].Split('/');//separa la fecha en [dia]/[mes]/[año]
            DateTime fecha_inicial = new DateTime(Convert.ToInt32(words2[2]), 1, 1); ;//fecha inicial
            DateTime fecha_final = new DateTime(Convert.ToInt32(words2[2]), 12, 31);// fecha final
            int weekNo = 0;
            int dia = 0;//dias en los que avanza la fecha temporal 
            DateTime tmp = fecha_final; // fecha temporal que ira aumentado hasta llegar a la fecha final 

            while (tmp <= fecha_final)// mientras la fecha temporal es menor que la fecha final se incrementa de un dia en un dia hasta llegar a la fecha final 
            {
                /****calcula la semana****/
                DateTime date = new DateTime(tmp.Year, tmp.Month, tmp.Day);
                System.Globalization.CultureInfo norwCulture =
                System.Globalization.CultureInfo.CreateSpecificCulture("es");
                System.Globalization.Calendar cal = norwCulture.Calendar;
                weekNo = cal.GetWeekOfYear(date,
                norwCulture.DateTimeFormat.CalendarWeekRule,
                norwCulture.DateTimeFormat.FirstDayOfWeek);
                // Show the result         weekNo.ToString()                 
                /*********************/

                for (int i = 0; i < lista_cuentas.Count; i++)
                {
                    string[] fecha_lista = lista_cuentas[i].fecha.Split(' ');//separa la fecha de la hora
                    string[] fecha_separada = fecha_lista[0].Split('/');//separa la fecha en [dia]/[mes]/[año]
                    //MessageBox.Show(lista_cuentas[i].Dia + " " + tmp.Day);
                    //MessageBox.Show(lista_cuentas[i].Mes + " " + tmp.Month);
                    //MessageBox.Show(lista_cuentas[i].Anio + " " + tmp.Year);
                    if (int.Parse(fecha_separada[0]) == tmp.Day && int.Parse(fecha_separada[1]) == tmp.Month && int.Parse(fecha_separada[2]) == tmp.Year)// TRABAJO EN LA SEMANA QUE LE CORRESPONDE A ESA FECHA 
                    {
                        lista_cuentas[i].semana = weekNo;
                        //MessageBox.Show("");
                    }


                }
                dia++;//cantidad de dias de avanza la fecha 
                tmp = fecha_inicial.AddDays(dia);//avanza la fecha temporal 1 dia 
            }


        }
        #endregion

        #region CLasificacion 1PRoveedores
        /// <summary>
        /// Crea las gráficas para los proveedores
        /// </summary>
        public void creaGraficasClasificacion1Proveedores(string path)
        {

            List<Tipos_Datos_CRU.ComprasMensualesXClasificacion> conprasmensualaes = new List<Tipos_Datos_CRU.ComprasMensualesXClasificacion>();

            //checo si e4xiste algun datos en la lista que se va a mandar si existe entonces realizo lo siguiente
            if (lista_cuentas.Count > 0)
            {
                string[] words = lista_cuentas[0].fecha.Split(' ');
                string[] words2 = words[0].Split('/');
                //creo un objeto de tipo ComprasMensualesXClasificacion y lo lleno con los datos del primer dato de mi lista
                Tipos_Datos_CRU.ComprasMensualesXClasificacion nuevo = new Tipos_Datos_CRU.ComprasMensualesXClasificacion()
                {
                    Anio = words2[2],
                    Clasificacion1 = lista_cuentas[0].Clasificacion_1_proveedor,
                    CodigoClasificacion = lista_cuentas[0].Valor_Clasificacion_1_proveedor,
                    Mes = words2[1],
                    total = lista_cuentas[0].Total,
                    Dia = words2[0],

                };
                conprasmensualaes.Add(nuevo);//agrego mi objeto a mi nueva lista


                int nuevoobject = 0;
                for (int i = 1; i < lista_cuentas.Count; i++)
                {
                    string[] words3 = lista_cuentas[i].fecha.Split(' ');
                    string[] words4 = words[0].Split('/');
                    //checar todos los datos de mi lista
                    nuevoobject = 0;
                    for (int j = 0; j < conprasmensualaes.Count; j++)
                    {
                        //checo la lista de mis compras mensuales
                        //sin el tipo de clasificacion 1 es igual y el mes y el año entonces sumo su total de ese mes y si no
                        if (lista_cuentas[i].Clasificacion_1_proveedor.Equals(conprasmensualaes[j].Clasificacion1) && words4[1].Equals(conprasmensualaes[j].Mes) && words4[2].Equals(conprasmensualaes[j].Anio))
                        {
                            conprasmensualaes[j].total += lista_cuentas[i].Total;
                            nuevoobject = 1;
                            break;
                        }



                    }

                    if (nuevoobject == 0)//entonces creo otro nuevo objeto con los datos nuevos de la lista
                    {
                        Tipos_Datos_CRU.ComprasMensualesXClasificacion nuevo1 = new Tipos_Datos_CRU.ComprasMensualesXClasificacion()
                        {
                            Anio = words4[2],
                            Clasificacion1 = lista_cuentas[i].Clasificacion_1_proveedor,
                            CodigoClasificacion = lista_cuentas[i].Valor_Clasificacion_1_proveedor,
                            Mes = words4[1],
                            total = lista_cuentas[i].Total,
                            Dia = words4[0]
                        };
                        conprasmensualaes.Add(nuevo1);

                    }
                }//fin del primer for donde mi lista  conprasmensualaes tendra los datos que se necesitaran gráficar
                //alamcenarlas en la lisa pdfconprasmensualaes
                string clasificacion = conprasmensualaes[0].Clasificacion1;
                string Anio = conprasmensualaes[0].Anio;
                int band = 0;
                List<Tipos_Datos_CRU.ComprasMensualesXClasificacion> conprasmensualaes1 = new List<Tipos_Datos_CRU.ComprasMensualesXClasificacion>();
                PDFProveedoresClasificacion1 = new List<Tipos_Datos_CRU.ComprasMensualesXClasificacionIMagenes>();
                Tipos_Datos_CRU.ComprasMensualesXClasificacionIMagenes nuev = new Tipos_Datos_CRU.ComprasMensualesXClasificacionIMagenes()
                {
                    Anio = Anio,
                    Clasificacion1 = clasificacion,
                    nombreimagen = conprasmensualaes[0].Mes + conprasmensualaes[0].Anio + conprasmensualaes[0].Clasificacion1
                };
                PDFProveedoresClasificacion1.Add(nuev);
                /****SE CREARAN TODAS LAD IMAGENES QUE SE NECESITAN PARA CREAR EL PDF******/
                for (int j = 0; j < conprasmensualaes.Count; j++)
                {
                    if (conprasmensualaes[j].Anio.Equals(Anio) && conprasmensualaes[j].Clasificacion1.Equals(clasificacion))
                    {
                        conprasmensualaes1.Add(conprasmensualaes[j]);
                    }
                    else
                    {
                        PDFProveedoresClasificacion1[band].compras = conprasmensualaes1;
                        Tipos_Datos_CRU.ComprasMensualesXClasificacionIMagenes nuev1 = new Tipos_Datos_CRU.ComprasMensualesXClasificacionIMagenes()
                        {
                            Anio = conprasmensualaes[j].Anio,
                            Clasificacion1 = conprasmensualaes[j].Clasificacion1,
                            nombreimagen = conprasmensualaes[j].Mes + conprasmensualaes[j].Anio + conprasmensualaes[j].Clasificacion1
                        };
                        Anio = conprasmensualaes[j].Anio;
                        clasificacion = conprasmensualaes[j].Clasificacion1;
                        PDFProveedoresClasificacion1.Add(nuev1);
                        conprasmensualaes1 = new List<Tipos_Datos_CRU.ComprasMensualesXClasificacion>();
                        conprasmensualaes1.Add(conprasmensualaes[j]);
                        band++;
                    }
                    //conprasmensualaes[j].nombreimagen = conprasmensualaes[j].Dia + conprasmensualaes[j].Mes + conprasmensualaes[j].Anio;

                }
                PDFProveedoresClasificacion1[band].compras = conprasmensualaes1;


                for (int i = 0; i < PDFProveedoresClasificacion1.Count; i++)
                {
                    instance_graficas.InitializeChart();
                    instance_graficas.LoadBarChart_compras_Clasificacion1(PDFProveedoresClasificacion1[i], PDFProveedoresClasificacion1[i].nombreimagen, path);
                }
            }

        }
        #endregion

        #region CLasificacion 2 PRoveedores
        public void creaGraficasClasificacion2Proveedores(string path)
        {
            List<Tipos_Datos_CRU.ComprasMensualesXClasificacion2> conprasmensualaes = new List<Tipos_Datos_CRU.ComprasMensualesXClasificacion2>();

            //checo si e4xiste algun datos en la lista que se va a mandar si existe entonces realizo lo siguiente
            if (lista_cuentas.Count > 0)
            {
                string[] words = lista_cuentas[0].fecha.Split(' ');
                string[] words2 = words[0].Split('/');
                //creo un objeto de tipo ComprasMensualesXClasificacion y lo lleno con los datos del primer dato de mi lista
                Tipos_Datos_CRU.ComprasMensualesXClasificacion2 nuevo = new Tipos_Datos_CRU.ComprasMensualesXClasificacion2()
                {
                    Anio = words2[2],
                    Clasificacion2 = lista_cuentas[0].Clasificacion_2_proveedor,
                    CodigoClasificacion = lista_cuentas[0].Valor_Clasificacion_2_proveedor,
                    Mes = words2[1],
                    total = lista_cuentas[0].Total,
                    Dia = words2[0],

                };
                conprasmensualaes.Add(nuevo);//agrego mi objeto a mi nueva lista


                int nuevoobject = 0;
                for (int i = 1; i < lista_cuentas.Count; i++)
                {
                    //checar todos los datos de mi lista
                    string[] words3 = lista_cuentas[i].fecha.Split(' ');
                    string[] words4 = words[0].Split('/');
                    nuevoobject = 0;
                    for (int j = 0; j < conprasmensualaes.Count; j++)
                    {

                        //checo la lista de mis compras mensuales
                        //sin el tipo de clasificacion 1 es igual y el mes y el año entonces sumo su total de ese mes y si no
                        if (lista_cuentas[i].Clasificacion_2_proveedor.Equals(conprasmensualaes[j].Clasificacion2) && words4[1].Equals(conprasmensualaes[j].Mes) && words4[2].Equals(conprasmensualaes[j].Anio))
                        {
                            conprasmensualaes[j].total += lista_cuentas[i].Total;
                            nuevoobject = 1;
                            break;
                        }



                    }
                    if (nuevoobject == 0)//entonces creo otro nuevo objeto con los datos nuevos de la lista
                    {
                        Tipos_Datos_CRU.ComprasMensualesXClasificacion2 nuevo1 = new Tipos_Datos_CRU.ComprasMensualesXClasificacion2()
                        {
                            Anio = words4[2],
                            Clasificacion2 = lista_cuentas[i].Clasificacion_2_proveedor,
                            CodigoClasificacion = lista_cuentas[i].Valor_Clasificacion_2_proveedor,
                            Mes = words4[1],
                            total = lista_cuentas[i].Total,
                            Dia = words4[0]
                        };
                        conprasmensualaes.Add(nuevo1);

                    }
                }//fin del primer for donde mi lista  conprasmensualaes tendra los datos que se necesitaran gráficar
                //alamcenarlas en la lisa pdfconprasmensualaes
                string clasificacion = conprasmensualaes[0].Clasificacion2;
                string Anio = conprasmensualaes[0].Anio;
                int band = 0;
                List<Tipos_Datos_CRU.ComprasMensualesXClasificacion2> conprasmensualaes2 = new List<Tipos_Datos_CRU.ComprasMensualesXClasificacion2>();
                PDFProveedoresClasificacion2 = new List<Tipos_Datos_CRU.ComprasMensualesXClasificacionIMagenes2>();
                Tipos_Datos_CRU.ComprasMensualesXClasificacionIMagenes2 nuev = new Tipos_Datos_CRU.ComprasMensualesXClasificacionIMagenes2()
                {
                    Anio = Anio,
                    Clasificacion2 = clasificacion,
                    nombreimagen = conprasmensualaes[0].Mes + conprasmensualaes[0].Anio + conprasmensualaes[0].Clasificacion2
                };
                PDFProveedoresClasificacion2.Add(nuev);
                /****SE CREARAN TODAS LAD IMAGENES QUE SE NECESITAN PARA CREAR EL PDF******/
                for (int j = 0; j < conprasmensualaes.Count; j++)
                {
                    if (conprasmensualaes[j].Anio.Equals(Anio) && conprasmensualaes[j].Clasificacion2.Equals(clasificacion))
                    {
                        conprasmensualaes2.Add(conprasmensualaes[j]);
                    }
                    else
                    {
                        PDFProveedoresClasificacion2[band].compras = conprasmensualaes2;
                        Tipos_Datos_CRU.ComprasMensualesXClasificacionIMagenes2 nuev1 = new Tipos_Datos_CRU.ComprasMensualesXClasificacionIMagenes2()
                        {
                            Anio = conprasmensualaes[j].Anio,
                            Clasificacion2 = conprasmensualaes[j].Clasificacion2,
                            nombreimagen = conprasmensualaes[j].Mes + conprasmensualaes[j].Anio + conprasmensualaes[j].Clasificacion2
                        };
                        Anio = conprasmensualaes[j].Anio;
                        clasificacion = conprasmensualaes[j].Clasificacion2;
                        PDFProveedoresClasificacion2.Add(nuev1);
                        conprasmensualaes2 = new List<Tipos_Datos_CRU.ComprasMensualesXClasificacion2>();
                        conprasmensualaes2.Add(conprasmensualaes[j]);
                        band++;
                    }
                    //conprasmensualaes[j].nombreimagen = conprasmensualaes[j].Dia + conprasmensualaes[j].Mes + conprasmensualaes[j].Anio;

                }
                PDFProveedoresClasificacion2[band].compras = conprasmensualaes2;


                for (int i = 0; i < PDFProveedoresClasificacion2.Count; i++)
                {
                    instance_graficas.InitializeChart();
                    instance_graficas.LoadBarChart_compras_Clasificacion2(PDFProveedoresClasificacion2[i], PDFProveedoresClasificacion2[i].nombreimagen, path);
                }


                //instance_graficas.LoadBarChart_compras_dia(lista_cuentas, textBox2.Text);
                //instance_impresion.Reporte_Compras_Por_Dia(PDFProveedoresClasificacion2, textBox2.Text);
            }
            else MessageBox.Show("No se mostrara ningun PDF por que noexiste ningun dato");


        }
        #endregion


        #region Clasificacion 1 Productos

        public void CreargraficasClasificacion1productos(string path)
        {
            List<Tipos_Datos_CRU.ComprasMensualesXClasificacion1> conprasmensualaes = new List<Tipos_Datos_CRU.ComprasMensualesXClasificacion1>();

            //checo si e4xiste algun datos en la lista que se va a mandar si existe entonces realizo lo siguiente
            if (lista_cuentas.Count > 0)
            {
                string[] words = lista_cuentas[0].fecha.Split(' ');
                string[] words2 = words[0].Split('/');
                //creo un objeto de tipo ComprasMensualesXClasificacion y lo lleno con los datos del primer dato de mi lista
                Tipos_Datos_CRU.ComprasMensualesXClasificacion1 nuevo = new Tipos_Datos_CRU.ComprasMensualesXClasificacion1()
                {
                    Anio = words2[2],
                    Clasificacion1 = lista_cuentas[0].Listmovimiento[0].producto.Clasifiacion1,
                    CodigoClasificacion = lista_cuentas[0].Listmovimiento[0].producto.ValorClasificación1,
                    Mes = words2[1],
                    total = lista_cuentas[0].Listmovimiento[0].Total,
                    Dia = words2[0],

                };
                conprasmensualaes.Add(nuevo);//agrego mi objeto a mi nueva lista


                int nuevoobject = 0;
                for (int i = 1; i < lista_cuentas.Count; i++)
                {
                    string[] words3 = lista_cuentas[i].fecha.Split(' ');
                    string[] words4 = words[0].Split('/');
                    //checar todos los datos de mi lista
                    for (int l = 0; l < lista_cuentas[i].Listmovimiento.Count; l++)
                    {

                        nuevoobject = 0;
                        for (int j = 0; j < conprasmensualaes.Count; j++)
                        {//checo la lista de mis compras mensuales
                            //sin el tipo de clasificacion 1 es igual y el mes y el año entonces sumo su total de ese mes y si no
                            if (lista_cuentas[i].Listmovimiento[l].producto.Clasifiacion1.Equals(conprasmensualaes[j].Clasificacion1) && words4[1].Equals(conprasmensualaes[j].Mes) && words4[2].Equals(conprasmensualaes[j].Anio))
                            {
                                conprasmensualaes[j].total += lista_cuentas[i].Listmovimiento[l].Total;
                                nuevoobject = 1;
                                break;
                            }

                        }//fin primer for
                        if (nuevoobject == 0)//entonces creo otro nuevo objeto con los datos nuevos de la lista
                        {
                            Tipos_Datos_CRU.ComprasMensualesXClasificacion1 nuevo1 = new Tipos_Datos_CRU.ComprasMensualesXClasificacion1()
                            {
                                Anio = words4[2],
                                Clasificacion1 = lista_cuentas[i].Listmovimiento[l].producto.Clasifiacion1,
                                CodigoClasificacion = lista_cuentas[i].Listmovimiento[l].producto.ValorClasificación1,
                                Mes = words4[1],
                                total = lista_cuentas[i].Listmovimiento[l].Total,
                                Dia = words4[0]
                            };
                            conprasmensualaes.Add(nuevo1);

                        }
                    }//fin segun for
                }//fin del primer for donde mi lista  conprasmensualaes tendra los datos que se necesitaran gráficar
                //alamcenarlas en la lisa pdfconprasmensualaes
                string clasificacion = conprasmensualaes[0].Clasificacion1;
                string Anio = conprasmensualaes[0].Anio;
                int band = 0;
                List<Tipos_Datos_CRU.ComprasMensualesXClasificacion1> conprasmensualaes2 = new List<Tipos_Datos_CRU.ComprasMensualesXClasificacion1>();
                PDFProveedoresClasificacion1Productos = new List<Tipos_Datos_CRU.ComprasMensualesXClasificacion1Productos>();
                Tipos_Datos_CRU.ComprasMensualesXClasificacion1Productos nuev = new Tipos_Datos_CRU.ComprasMensualesXClasificacion1Productos()
                {
                    Anio = Anio,
                    Clasificacion1 = clasificacion,
                    nombreimagen = conprasmensualaes[0].Mes + conprasmensualaes[0].Anio + conprasmensualaes[0].Clasificacion1 + "p"
                };
                PDFProveedoresClasificacion1Productos.Add(nuev);
                /****SE CREARAN TODAS LAD IMAGENES QUE SE NECESITAN PARA CREAR EL PDF******/
                for (int j = 0; j < conprasmensualaes.Count; j++)
                {
                    if (conprasmensualaes[j].Anio.Equals(Anio) && conprasmensualaes[j].Clasificacion1.Equals(clasificacion))
                    {
                        conprasmensualaes2.Add(conprasmensualaes[j]);
                    }
                    else
                    {
                        PDFProveedoresClasificacion1Productos[band].compras = conprasmensualaes2;
                        Tipos_Datos_CRU.ComprasMensualesXClasificacion1Productos nuev1 = new Tipos_Datos_CRU.ComprasMensualesXClasificacion1Productos()
                        {
                            Anio = conprasmensualaes[j].Anio,
                            Clasificacion1 = conprasmensualaes[j].Clasificacion1,
                            nombreimagen = conprasmensualaes[j].Mes + conprasmensualaes[j].Anio + conprasmensualaes[j].Clasificacion1
                        };
                        Anio = conprasmensualaes[j].Anio;
                        clasificacion = conprasmensualaes[j].Clasificacion1;
                        PDFProveedoresClasificacion1Productos.Add(nuev1);
                        conprasmensualaes2 = new List<Tipos_Datos_CRU.ComprasMensualesXClasificacion1>();
                        conprasmensualaes2.Add(conprasmensualaes[j]);
                        band++;
                    }
                    //conprasmensualaes[j].nombreimagen = conprasmensualaes[j].Dia + conprasmensualaes[j].Mes + conprasmensualaes[j].Anio;

                }
                PDFProveedoresClasificacion1Productos[band].compras = conprasmensualaes2;


                for (int i = 0; i < PDFProveedoresClasificacion1Productos.Count; i++)
                {
                    instance_graficas.InitializeChart();
                    instance_graficas.LoadBarChart_compras_Clasificacion1Productos(PDFProveedoresClasificacion1Productos[i], PDFProveedoresClasificacion1Productos[i].nombreimagen, path);
                }


                //instance_graficas.LoadBarChart_compras_dia(lista_cuentas, textBox2.Text);
                //   instance_impresion.Reporte_Compras_Por_Dia(PDFProveedoresClasificacion2, textBox2.Text);
            }
            else MessageBox.Show("No se mostrara ningun PDF por que noexiste ningun dato");
        }

        #endregion


        #region Calificacion 2 productos
        public void CrearGraficasCalsifiacion2Productos(string path)
        {
            List<Tipos_Datos_CRU.ComprasMensualesXClasificacion2> conprasmensualaes = new List<Tipos_Datos_CRU.ComprasMensualesXClasificacion2>();

            //checo si e4xiste algun datos en la lista que se va a mandar si existe entonces realizo lo siguiente
            if (lista_cuentas.Count > 0)
            {
                string[] fecha = lista_cuentas[0].fecha.Split(' ');
                string[] fecha_partes = fecha[0].Split('/');
                //creo un objeto de tipo ComprasMensualesXClasificacion y lo lleno con los datos del primer dato de mi lista
                Tipos_Datos_CRU.ComprasMensualesXClasificacion2 nuevo = new Tipos_Datos_CRU.ComprasMensualesXClasificacion2()
                {

                    Clasificacion2 = lista_cuentas[0].Listmovimiento[0].producto.Clasificacion2,
                    CodigoClasificacion = lista_cuentas[0].Listmovimiento[0].producto.ValorClasificación2,
                    Mes = fecha_partes[1],
                    total = lista_cuentas[0].Listmovimiento[0].Total,
                    Dia = fecha_partes[0]

                };
                conprasmensualaes.Add(nuevo);//agrego mi objeto a mi nueva lista


                int nuevoobject = 0;
                for (int i = 1; i < lista_cuentas.Count; i++)
                {
                    string[] fecha_ = lista_cuentas[i].fecha.Split(' ');
                    string[] fecha_partes_ = fecha_[0].Split('/');
                    //checar todos los datos de mi lista
                    for (int l = 0; l < lista_cuentas[i].Listmovimiento.Count; l++)
                    {
                        nuevoobject = 0;
                        for (int j = 0; j < conprasmensualaes.Count; j++)
                        {
                            //checo la lista de mis compras mensuales
                            //sin el tipo de clasificacion 1 es igual y el mes y el año entonces sumo su total de ese mes y si no
                            if (lista_cuentas[i].Listmovimiento[l].producto.Clasificacion2.Equals(conprasmensualaes[j].Clasificacion2) && fecha_partes_[1].Equals(conprasmensualaes[j].Mes) && fecha_partes_[2].Equals(conprasmensualaes[j].Anio))
                            {
                                conprasmensualaes[j].total += lista_cuentas[i].Listmovimiento[l].Total;
                                nuevoobject = 1;
                                break;
                            }

                        }//fin primer for
                        if (nuevoobject == 0)//entonces creo otro nuevo objeto con los datos nuevos de la lista
                        {
                            Tipos_Datos_CRU.ComprasMensualesXClasificacion2 nuevo1 = new Tipos_Datos_CRU.ComprasMensualesXClasificacion2()
                            {
                                Anio = fecha_partes_[2],
                                Clasificacion2 = lista_cuentas[i].Listmovimiento[l].producto.Clasificacion2,
                                CodigoClasificacion = lista_cuentas[i].Listmovimiento[l].producto.ValorClasificación2,
                                Mes = fecha_partes_[1],
                                total = lista_cuentas[i].Listmovimiento[l].Total,
                                Dia = fecha_partes_[0]
                            };
                            conprasmensualaes.Add(nuevo1);
                        }

                    }//fin segun for
                }//fin del primer for donde mi lista  conprasmensualaes tendra los datos que se necesitaran gráficar
                //alamcenarlas en la lisa pdfconprasmensualaes
                string clasificacion = conprasmensualaes[0].Clasificacion2;
                string Anio = conprasmensualaes[0].Anio;
                int band = 0;
                List<Tipos_Datos_CRU.ComprasMensualesXClasificacion2> conprasmensualaes2 = new List<Tipos_Datos_CRU.ComprasMensualesXClasificacion2>();
                PDFProveedoresClasificacion2Productos = new List<Tipos_Datos_CRU.ComprasMensualesXClasificacion2Productos>();
                Tipos_Datos_CRU.ComprasMensualesXClasificacion2Productos nuev = new Tipos_Datos_CRU.ComprasMensualesXClasificacion2Productos()
                {
                    Anio = Anio,
                    Clasificacion2 = clasificacion,
                    nombreimagen = conprasmensualaes[0].Mes + conprasmensualaes[0].Anio + conprasmensualaes[0].Clasificacion2
                };
                PDFProveedoresClasificacion2Productos.Add(nuev);
                /****SE CREARAN TODAS LAD IMAGENES QUE SE NECESITAN PARA CREAR EL PDF******/
                for (int j = 0; j < conprasmensualaes.Count; j++)
                {
                    if (conprasmensualaes[j].Anio == Anio && conprasmensualaes[j].Clasificacion2 == clasificacion)
                    {
                        conprasmensualaes2.Add(conprasmensualaes[j]);
                    }
                    else
                    {
                        PDFProveedoresClasificacion2Productos[band].compras = conprasmensualaes2;
                        Tipos_Datos_CRU.ComprasMensualesXClasificacion2Productos nuev1 = new Tipos_Datos_CRU.ComprasMensualesXClasificacion2Productos()
                        {
                            Anio = conprasmensualaes[j].Anio,
                            Clasificacion2 = conprasmensualaes[j].Clasificacion2,
                            nombreimagen = conprasmensualaes[j].Mes + conprasmensualaes[j].Anio + conprasmensualaes[j].Clasificacion2
                        };
                        Anio = conprasmensualaes[j].Anio;
                        clasificacion = conprasmensualaes[j].Clasificacion2;
                        PDFProveedoresClasificacion2Productos.Add(nuev1);
                        conprasmensualaes2 = new List<Tipos_Datos_CRU.ComprasMensualesXClasificacion2>();
                        conprasmensualaes2.Add(conprasmensualaes[j]);
                        band++;
                    }
                    //conprasmensualaes[j].nombreimagen = conprasmensualaes[j].Dia + conprasmensualaes[j].Mes + conprasmensualaes[j].Anio;

                }
                PDFProveedoresClasificacion2Productos[band].compras = conprasmensualaes2;


                for (int i = 0; i < PDFProveedoresClasificacion2Productos.Count; i++)
                {
                    instance_graficas.InitializeChart();
                    instance_graficas.LoadBarChart_compras_Clasificacion2Productos(PDFProveedoresClasificacion2Productos[i], PDFProveedoresClasificacion2Productos[i].nombreimagen, path);
                }


                //instance_graficas.LoadBarChart_compras_dia(lista_cuentas, textBox2.Text);
                // instance_impresion.Reporte_Compras_Por_Dia(PDFProveedoresClasificacion2Productos, textBox2.Text);
            }
            else MessageBox.Show("No se mostrara ningun PDF por que noexiste ningun dato");
        }
        #endregion


        #region Clasificacion 1 producto por mes
        public void Graficarclasificacion1ProductoXMes(string path)
        {
            PDFClasificacion1PRoductoMes = new List<Tipos_Datos_CRU.ComprasMensualesXClasificacion1ProductosMes>();
            Tipos_Datos_CRU.ComprasMensualesXClasificacion1ProductosMes clasificacion1mes = new Tipos_Datos_CRU.ComprasMensualesXClasificacion1ProductosMes();
            List<Tipos_Datos_CRU.ComprasMensualesXClasificacion1> conprasmensualaes = new List<Tipos_Datos_CRU.ComprasMensualesXClasificacion1>();//por cada lista se debe de crear una grafica


            //checo si e4xiste algun datos en la lista que se va a mandar si existe entonces realizo lo siguiente
            if (lista_cuentas.Count > 0)
            {
                //creo un objeto de tipo ComprasMensualesXClasificacion y lo lleno con los datos del primer dato de mi lista
                string[] fecha = lista_cuentas[0].fecha.Split(' ');
                string[] fecha_partes = fecha[0].Split('/');
                /*********************************************************************************************************/
                /*********************************************************************************************************/
                Tipos_Datos_CRU.ComprasMensualesXClasificacion1 nuevo = new Tipos_Datos_CRU.ComprasMensualesXClasificacion1()
                {
                    Anio = fecha_partes[2],
                    Clasificacion1 = lista_cuentas[0].Listmovimiento[0].producto.Clasifiacion1,
                    CodigoClasificacion = lista_cuentas[0].Listmovimiento[0].producto.ValorClasificación1,
                    Mes = fecha_partes[1],
                    total = lista_cuentas[0].Listmovimiento[0].Total,
                    Dia = fecha_partes[0],

                };
                conprasmensualaes.Add(nuevo);//agrego mi objeto a mi nueva lista
                clasificacion1mes.Mes = nuevo.Mes;
                clasificacion1mes.Anio = nuevo.Anio;
                clasificacion1mes.compras = conprasmensualaes;
                clasificacion1mes.nombreimagen = nuevo.Mes + nuevo.Anio + nuevo.Clasificacion1;
                clasificacion1mes.Clasificacion1 = nuevo.Clasificacion1;

                PDFClasificacion1PRoductoMes.Add(clasificacion1mes);//se agrega el dato
                /*********************************************************************************************************/
                /*********************************************************************************************************/



                int nuevoObject = 0;
                for (int i = 1; i < lista_cuentas.Count; i++)
                {
                    string[] fecha2 = lista_cuentas[i].fecha.Split(' ');
                    string[] fecha_partes2 = fecha2[0].Split('/');
                    //checar todos los datos de mi lista
                    for (int l = 0; l < lista_cuentas[i].Listmovimiento.Count; l++)
                    {
                        nuevoObject = 0;
                        for (int j = 0; j < PDFClasificacion1PRoductoMes.Count; j++)//entra a las listas de clasificaciones
                        {//checo la lista de mis compras mensuales
                            //sin el tipo de clasificacion 1 es igual y el mes y el año entonces sumo su total de ese mes y si no

                            for (int k = 0; k < PDFClasificacion1PRoductoMes[j].compras.Count; k++)//entra para comparar si son del mismo mes y clasificacion 1 sumalos sino si solo es el del mismo mes agregalo en la lista de compras
                            {
                                if (fecha_partes2[1].Equals(PDFClasificacion1PRoductoMes[j].compras[k].Mes))
                                {
                                    if (lista_cuentas[i].Listmovimiento[l].producto.Clasifiacion1.Equals(PDFClasificacion1PRoductoMes[j].compras[k].Clasificacion1))
                                    {
                                        PDFClasificacion1PRoductoMes[j].compras[k].total += lista_cuentas[i].Listmovimiento[l].Total;
                                    }
                                    else
                                    {
                                        Tipos_Datos_CRU.ComprasMensualesXClasificacion1 nuevo1 = new Tipos_Datos_CRU.ComprasMensualesXClasificacion1()
                                        {
                                            Anio = fecha_partes2[2],
                                            Clasificacion1 = lista_cuentas[i].Listmovimiento[l].producto.Clasifiacion1,
                                            CodigoClasificacion = lista_cuentas[i].Listmovimiento[l].producto.ValorClasificación1,
                                            Mes = fecha_partes2[1],
                                            total = lista_cuentas[i].Listmovimiento[l].Total,
                                            Dia = fecha_partes2[0],
                                        };
                                        PDFClasificacion1PRoductoMes[j].compras.Add(nuevo1);

                                    }
                                    nuevoObject = 1;
                                    break;
                                }
                            }


                        }//fin primer for
                        if (nuevoObject == 0)
                        {
                            List<Tipos_Datos_CRU.ComprasMensualesXClasificacion1> conprasmensualaes1 = new List<Tipos_Datos_CRU.ComprasMensualesXClasificacion1>();
                            Tipos_Datos_CRU.ComprasMensualesXClasificacion1 nuevo2 = new Tipos_Datos_CRU.ComprasMensualesXClasificacion1()
                            {
                                Anio = fecha_partes2[2],
                                Clasificacion1 = lista_cuentas[i].Listmovimiento[l].producto.Clasifiacion1,
                                CodigoClasificacion = lista_cuentas[i].Listmovimiento[l].producto.ValorClasificación2,
                                Mes = fecha_partes2[1],
                                total = lista_cuentas[i].Listmovimiento[l].Total,
                                Dia = fecha_partes2[0],

                            };
                            conprasmensualaes1.Add(nuevo2);//agrego mi objeto a mi nueva lista
                            Tipos_Datos_CRU.ComprasMensualesXClasificacion1ProductosMes clasificacion1me = new Tipos_Datos_CRU.ComprasMensualesXClasificacion1ProductosMes();
                            clasificacion1me.Mes = nuevo2.Mes;
                            clasificacion1me.Anio = nuevo2.Anio;
                            clasificacion1me.compras = conprasmensualaes1;
                            clasificacion1me.nombreimagen = nuevo2.Mes + nuevo2.Anio + nuevo2.Clasificacion1;
                            clasificacion1me.Clasificacion1 = nuevo2.Clasificacion1;

                            PDFClasificacion1PRoductoMes.Add(clasificacion1me);//se agrega el dato                            

                        }
                    }//fin segun for
                }//fin del primer for donde mi lista  conprasmensualaes tendra los datos que se necesitaran gráficar
                for (int i = 0; i < PDFClasificacion1PRoductoMes.Count; i++)
                {
                    instance_graficas.InitializeChart();

                    instance_graficas.LoadPieChartclasificacion1ProductoMes(PDFClasificacion1PRoductoMes[i].compras, PDFClasificacion1PRoductoMes[i].nombreimagen, path);
                }
            }
        }
        #endregion


        #region Clasificacion 2 producto por mes
        public void Graficarclasificacion2XMes(string path)
        {
            PDFClasificacion2PRoductoMes = new List<Tipos_Datos_CRU.ComprasMensualesXClasificacion2ProductosMes>();
            Tipos_Datos_CRU.ComprasMensualesXClasificacion2ProductosMes clasificacion1mes = new Tipos_Datos_CRU.ComprasMensualesXClasificacion2ProductosMes();
            List<Tipos_Datos_CRU.ComprasMensualesXClasificacion2> conprasmensualaes = new List<Tipos_Datos_CRU.ComprasMensualesXClasificacion2>();//por cada lista se debe de crear una grafica


            //checo si e4xiste algun datos en la lista que se va a mandar si existe entonces realizo lo siguiente
            if (lista_cuentas.Count > 0)
            {
                //creo un objeto de tipo ComprasMensualesXClasificacion y lo lleno con los datos del primer dato de mi lista
                string[] fecha = lista_cuentas[0].fecha.Split(' ');//obtengo la fecha de la lista 
                string[] fecha_partes = fecha[0].Split('/');//separa la fecha por dia mes y año [DIA][MES][AÑO]
                /*********************************************************************************************************/
                /*********************************************************************************************************/
                Tipos_Datos_CRU.ComprasMensualesXClasificacion2 nuevo = new Tipos_Datos_CRU.ComprasMensualesXClasificacion2()
                {
                    Anio = fecha_partes[2],
                    Clasificacion2 = lista_cuentas[0].Listmovimiento[0].producto.Clasificacion2,
                    CodigoClasificacion = lista_cuentas[0].Listmovimiento[0].producto.ValorClasificación2,
                    Mes = fecha_partes[1],
                    total = lista_cuentas[0].Listmovimiento[0].Total,
                    Dia = fecha_partes[0],

                };
                conprasmensualaes.Add(nuevo);//agrego mi objeto a mi nueva lista
                clasificacion1mes.Mes = nuevo.Mes;
                clasificacion1mes.Anio = nuevo.Anio;
                clasificacion1mes.compras = conprasmensualaes;
                clasificacion1mes.nombreimagen = nuevo.Mes + nuevo.Anio + nuevo.Clasificacion2.TrimEnd(' ');
                clasificacion1mes.Clasificacion2 = nuevo.Clasificacion2.TrimEnd(' ');

                PDFClasificacion2PRoductoMes.Add(clasificacion1mes);//se agrega el dato
                /*********************************************************************************************************/
                /*********************************************************************************************************/



                int nuevoObject = 0;
                for (int i = 1; i < lista_cuentas.Count; i++)
                {
                    string[] fecha2 = lista_cuentas[i].fecha.Split(' ');
                    string[] fecha_partes2 = fecha2[0].Split('/');

                    //checar todos los datos de mi lista
                    for (int l = 0; l < lista_cuentas[i].Listmovimiento.Count; l++)
                    {
                        nuevoObject = 0;
                        for (int j = 0; j < PDFClasificacion2PRoductoMes.Count; j++)//entra a las listas de clasificaciones
                        {//checo la lista de mis compras mensuales
                            //sin el tipo de clasificacion 1 es igual y el mes y el año entonces sumo su total de ese mes y si no

                            for (int k = 0; k < PDFClasificacion2PRoductoMes[j].compras.Count; k++)//entra para comparar si son del mismo mes y clasificacion 1 sumalos sino si solo es el del mismo mes agregalo en la lista de compras
                            {
                                if (fecha_partes2[1].Equals(PDFClasificacion2PRoductoMes[j].compras[k].Mes.TrimEnd(' ')))
                                {
                                    if (lista_cuentas[i].Listmovimiento[l].producto.Clasifiacion1.Equals(PDFClasificacion2PRoductoMes[j].compras[k].Clasificacion2.TrimEnd(' ')))
                                    {
                                        PDFClasificacion2PRoductoMes[j].compras[k].total += lista_cuentas[i].Listmovimiento[l].Total;
                                    }
                                    else
                                    {
                                        Tipos_Datos_CRU.ComprasMensualesXClasificacion2 nuevo1 = new Tipos_Datos_CRU.ComprasMensualesXClasificacion2()
                                        {
                                            Anio = fecha_partes2[2],
                                            Clasificacion2 = lista_cuentas[i].Listmovimiento[l].producto.Clasifiacion1,
                                            CodigoClasificacion = lista_cuentas[i].Listmovimiento[l].producto.ValorClasificación2,
                                            Mes = fecha_partes2[1],
                                            total = lista_cuentas[i].Listmovimiento[l].Total,
                                            Dia = fecha_partes2[0],
                                        };
                                        PDFClasificacion2PRoductoMes[j].compras.Add(nuevo1);

                                    }
                                    nuevoObject = 1;
                                    break;
                                }
                            }


                        }//fin primer for
                        if (nuevoObject == 0)
                        {
                            List<Tipos_Datos_CRU.ComprasMensualesXClasificacion2> conprasmensualaes1 = new List<Tipos_Datos_CRU.ComprasMensualesXClasificacion2>();
                            Tipos_Datos_CRU.ComprasMensualesXClasificacion2 nuevo2 = new Tipos_Datos_CRU.ComprasMensualesXClasificacion2()
                            {
                                Anio = fecha_partes2[2],
                                Clasificacion2 = lista_cuentas[i].Listmovimiento[l].producto.Clasifiacion1,
                                CodigoClasificacion = lista_cuentas[i].Listmovimiento[l].producto.ValorClasificación2,
                                Mes = fecha_partes2[1],
                                total = lista_cuentas[i].Listmovimiento[l].Total,
                                Dia = fecha_partes2[0],

                            };
                            conprasmensualaes1.Add(nuevo2);//agrego mi objeto a mi nueva lista
                            Tipos_Datos_CRU.ComprasMensualesXClasificacion2ProductosMes clasificacion1me = new Tipos_Datos_CRU.ComprasMensualesXClasificacion2ProductosMes();
                            clasificacion1me.Mes = nuevo2.Mes;
                            clasificacion1me.Anio = nuevo2.Anio;
                            clasificacion1me.compras = conprasmensualaes1;
                            clasificacion1me.nombreimagen = nuevo2.Mes + nuevo2.Anio + nuevo2.Clasificacion2;
                            clasificacion1me.Clasificacion2 = nuevo2.Clasificacion2;

                            PDFClasificacion2PRoductoMes.Add(clasificacion1me);//se agrega el dato                            

                        }
                    }//fin segun for
                }//fin del primer for donde mi lista  conprasmensualaes tendra los datos que se necesitaran gráficar
                for (int i = 0; i < PDFClasificacion2PRoductoMes.Count; i++)
                {
                    instance_graficas.InitializeChart();

                    instance_graficas.LoadPieChartclasificacion2ProductoMes(PDFClasificacion2PRoductoMes[i].compras, PDFClasificacion2PRoductoMes[i].nombreimagen , path);
                }
            }
        }
        #endregion
        #endregion


    }
}
