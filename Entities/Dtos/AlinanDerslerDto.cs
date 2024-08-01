using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Dtos
{
    public class AlinanDerslerDto
    {
        public string BolumAdi { get; set; }
        public string DersAdi { get; set; }
        public string DersDili { get; set; }
        public string DersTuru { get; set; }
        public string DersKodu { get; set; }
        public float Kredi { get; set; }
        public int Teorik { get; set; }
        public int Uygulama { get; set; }
        public int ECTS { get; set; }
        public int DersDonemi { get; set; }
        public string Sinif { get; set; }
        public string OgretimElemaniBilgisi { get; set; }
        public string OgrenciSinifi { get; set; }
        public string OgrenciDonemi { get; set; }
        public string DersVerilenDonem { get; set; }
    }
}
