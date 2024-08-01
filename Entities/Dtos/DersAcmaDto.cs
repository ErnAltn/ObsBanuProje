using Core.Entities;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Dtos
{
    public class DersAcmaDto:BaseEntityModel,IDto
    {
        public string AkademikYil { get; set; }
        public string AkademikDonem { get; set; }
        public string DersAdi { get; set; }
        public string DersDili { get; set; }
        public string BolumAdi { get; set; }
        public string OgrElmAdi { get; set; }
        public string OgrElmSoyadi { get; set; }
        public int Kontenjan { get; set; }
    }
}
