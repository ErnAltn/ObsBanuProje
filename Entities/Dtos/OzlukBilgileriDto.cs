using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Dtos
{
    public class OzlukBilgileriDto:IDto
    {
        public string Adi { get; set; }
        public string Soyadi { get; set; }
        public string TcKimlikNo { get; set; }
        public string DogumTarihi { get; set; }
        public string BolumAdi { get; set; }
        public string OgrNo { get; set; }
        public string KayitTarihi { get; set; }
        public string Durum { get; set; }
        public string AyrilmaTarihi { get; set; }
        public string  KurumSicilNo { get; set; }
        public string Unvan { get; set; }
        public string DanismanAdi { get; set; }
        public string DanismanSoyadi { get; set; }
        public string Adres { get; set; }
        public string TelefonNo { get; set; }
    }
}
