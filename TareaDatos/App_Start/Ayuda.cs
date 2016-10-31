using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TareaDatos.App_Start
{

    public class Ayuda
    {
        public int Grados { get; set; }
        public int Minutos { get; set; }
        public float Segundos { get; set; }
        public int GradosLong { get; set; }
        public int MinutosLong { get; set; }
        public float SegundosLong { get; set; }
        public float totalLatitud { get; set; }
        public float totalLongitud { get; set; }

        public void Separador(string latitud, string longitud)
        {
            //latitud = "-107° 27' 49.7\"";
            //longitud = "24° 45' 48.56\"";
            char[] delimita = new char[3];
            var caracter = "'";
            delimita[0] = '°';
            delimita[1] = Char.Parse(caracter);
            delimita[2] = '"';
            String[] latitudConvert = latitud.Split(delimita);
            String[] longitudCovert = longitud.Split(delimita);
            for (int i = 0; i < delimita.Length; i++)
            {
                switch (i)
                {
                    case 0:
                        Grados = int.Parse(latitudConvert[0]);
                        GradosLong = int.Parse(longitudCovert[0]);
                        break;
                    case 1:
                        Minutos = int.Parse(latitudConvert[1]);
                        MinutosLong = int.Parse(longitudCovert[1]);
                        break;
                    case 2:
                        Segundos = float.Parse(latitudConvert[2]);
                        SegundosLong = float.Parse(longitudCovert[2]);
                        break;
                }
            }
        }
        public void Conversion(string latitud, string longitud)
        {
            Separador(latitud, longitud);
            totalLatitud = Segundos / 60;
            Math.Round(totalLatitud);
            totalLatitud = totalLatitud + Minutos;
            totalLatitud = totalLatitud / 60;
            Math.Round(totalLatitud);
            totalLatitud = totalLatitud + Grados;
            totalLongitud = SegundosLong / 60;
            Math.Round(totalLongitud);
            totalLongitud = totalLongitud + MinutosLong;
            totalLongitud = totalLongitud / 60;
            Math.Round(totalLongitud);
            totalLongitud =GradosLong - totalLongitud;
        }
    }
}
