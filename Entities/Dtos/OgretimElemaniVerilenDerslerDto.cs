using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Dtos
{
    public class OgretimElemaniVerilenDerslerDto:IDto
    {
        public string BolumAdi { get; set; }
        public string DersAdi { get; set; }
        public string DersKodu { get; set; }
        public string DersTuru { get; set; }
        public int Teorik { get; set; }
        public int Uygulama { get; set; }
        public float Kredi { get; set; }
        public int ECTS { get; set; }
    }
}
