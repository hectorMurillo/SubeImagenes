using MetadataExtractor;
using MetadataExtractor.Formats.Exif;
using MetadataExtractor.IO;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace TareaDatos
{
    public class metadatos
    {
        public string _latitud { get; set; }        
        public string _longitud { get; set; }
        public metadatos(string latitud, string longitud)
        {
            _latitud = latitud;
            _longitud = longitud;
        }
        public metadatos()
        {

        }
        public void valoresGPS(String imagen)
        {
            Uri uri = new Uri(@"http://127.0.0.1:10000/devstoreaccount1/imagenes/congeo.jpg");
            string url = uri.AbsolutePath;
            url = "~" + url;
            var directories = ImageMetadataReader.ReadMetadata(url);            
            
            //var directories = ImageMetadataReader.ReadMetadata(ruta);

            var gps = directories.OfType<GpsDirectory>().FirstOrDefault();

            if (gps != null)
            {
                var location = gps.GetGeoLocation();

                if (location != null)
                {
                    _latitud = location.Latitude.ToString();
                    _longitud = location.Longitude.ToString();
                }                
            }
            else
            {
                _latitud = "0";
                _longitud = "0";
            }
        }
    }
}