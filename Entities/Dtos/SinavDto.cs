using Core.Entities;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Dtos
{
    public class SinavDto:BaseEntityModel,IDto
    {
        public string BolumAdi { get; set; }
        public string DersAdi { get; set; }
        public string DersKodu { get; set; }
        public string SinavTuru { get; set; }
        public string Derslik { get; set; }
        public string OgrElmAdi { get; set; }
        public string OgrElmSoyadi { get; set; }
        public int EtkiOrani { get; set; }
        public DateTime SinavTarihi { get; set; }


    }
}
