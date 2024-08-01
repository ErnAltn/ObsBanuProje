using Core.Entities;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Dtos
{
    public class MufredatDto:BaseEntityModel,IDto
    {
        
        public string OgrenciAdiSoyadi { get; set; }
        public DateTime OgrenciKayitTarihi { get; set; }
        //public int ECTSCount { get; set; }
        //public int KrediCount { get; set; }
        public string BolumAdi { get; set; }
        public string DersAdi { get; set; }
        public string DersDili { get; set; }
        public string DersTuru { get; set; }
        public string DersKodu { get; set; }
        public string DersSeviyesi { get; set; }
        public float Kredi { get; set; }
        public int ECTS { get; set; }
        public string AkedemikYil { get; set; }
        public string AkedemikDonem { get; set; }
        public int DersDonemi { get; set; }
    }
}
