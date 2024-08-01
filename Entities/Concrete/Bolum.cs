using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Concrete
{
    public class Bolum:BaseEntityModel,IEntity
    {
        public int ProgramTuruId { get; set; }
        public int OgretimTuruId { get; set; }
        public int OgretimDiliId { get; set; }
        public string BolumAdi { get; set; }
        public string WebAdresi { get; set; }
    }
}
