using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IndicadoresV1._001.SDK_Admipaq.Controlador
{
    class WorkerProgressBar
    {


        public delegate void DelegateCXP(string fechainicial, string fechafinal, string ruta_empresa);

        public event DelegateCXP get_data_CXP;

        public string fechainicial = "";
        public string fechafinal = "";
        public string ruta_empresa = "";
        

        public void CRU_mtehod()
        {
            get_data_CXP(fechainicial, fechafinal, ruta_empresa);
        }


    }
}
