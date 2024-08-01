using System;
using Core.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Concrete
{
    public class OgretimElemani : BaseEntityModel, IEntity
    {
        public int BolumId { get; set; }
        public int UserId { get; set; }
        public string KurumSicilNo { get; set; }
        public string Unvan { get; set; }
        public string Adi { get; set; }
        public string Soyadi { get; set; }
        public string TCKimlikNo { get; set; }
        public bool Cinsiyet { get; set; }
        public DateTime DogumTarihi { get; set; }
    }
}
