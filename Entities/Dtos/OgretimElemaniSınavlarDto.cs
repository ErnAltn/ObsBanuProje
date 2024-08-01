using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Dtos
{
    public class OgretimElemaniSınavlarDto:IDto
    {
        public int SinavId { get; set; }
        public string DersAdi { get; set; }
        public string DersKodu { get; set; }
        public string SinavTuru { get; set; }
        public int EtkiOrani { get; set; }
    }
}
