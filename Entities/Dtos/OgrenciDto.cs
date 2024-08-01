using Core.Entities;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Dtos
{
    public class OgrenciDto:BaseEntityModel,IDto
    {
        public string BolumAdi { get; set; }
        public string OgrenciNo { get; set; }
        public string Durum { get; set; }
        public DateTime AyrilmaTarihi { get; set; }
        public string Adi { get; set; }
        public string Soyadi { get; set; }
        public string TcKimlikNo { get; set; }
        public string Cinsiyet { get; set; }
        public string DogumTarihi { get; set; }
        public string Mail { get; set; }
        public string TelefonNo { get; set; }
        public string Adres { get; set; }
    }
}
