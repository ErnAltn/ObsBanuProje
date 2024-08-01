using Core.Entities;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Dtos
{
    public class DersProgramiDto: BaseEntityModel,IDto
    {
        public string OgretimElemaniBilgisi { get; set; }
        public string BolumAdi { get; set; }
        public string DersAdi { get; set; }
        public string DersKodu { get; set; }
        public string DerslikAdi { get; set; }
        public string DersGunu { get; set; }
        public string DersSaati { get; set; }
        public string OgrenciSinifi { get; set; }
        public string OgrenciDonemi { get; set; }
        public string DersVerilenDonem { get; set; }
    }
}
