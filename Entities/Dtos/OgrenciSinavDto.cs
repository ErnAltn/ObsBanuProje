using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Dtos
{
    public class OgrenciSinavDto
    {
        public int Id { get; set; }
        public string DersKodu { get; set; }
        public string DersAdi { get; set; }
        public string SonDurum { get; set; }
        public float? VizeNotu { get; set; }
        public float? FinalNotu { get; set; }
        public float? ButunlemeNotu { get; set; }
        public float? NotOrt { get; set; }
        public string HarfNotu { get; set; }
        public string Durumu { get; set; }
        public string OgrenciAktifDonem { get; set; }
        public int DersAcmaId { get; set; }
        public string OgrenciSinifi { get; set; }
        public string OgrenciDonemi { get; set; }
        public string SinavDonemi { get; set; }
    }
}
