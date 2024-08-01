using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Dtos
{
    public class DegerlendirmeDto : IDto
    {
        public int OgrenciId { get; set; }
        public string OgrenciNo { get; set; }
        public string OgrenciAdi { get; set; }
        public string OgrenciSoyadi { get; set; }
        public float OgrenciSinavNotu { get; set; }
    }
}
