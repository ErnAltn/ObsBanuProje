using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Concrete
{
    public class DersHavuzu : BaseEntityModel, IEntity
    {
        public int DersDiliId { get; set; }
        public int DersSeviyesiId { get; set; }
        public int DersturuId { get; set; }
        public string DersKodu { get; set; }
        public string DersAdi { get; set; }
        public int Teorik { get; set; }
        public int Uygulama { get; set; }
        public float Kredi { get; set; }
        public int ECTS { get; set; }
    }
}
