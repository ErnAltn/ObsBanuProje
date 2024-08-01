using Core.Entities;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Dtos
{
    public class DersHavuzuDto: BaseEntityModel, IDto
    {
        public string DersDili { get; set; }
        public string DersSeviyesi { get; set; }
        public string Dersturu { get; set; }
        public string DersKodu { get; set; }
        public string DersAdi { get; set; }
        public int Teorik { get; set; }
        public int Uygulama { get; set; }
        public float Kredi { get; set; }
        public int ECTS { get; set; }
    }
}
