using Core.Entities;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Dtos
{
    public class DanismanlikDto:BaseEntityModel,IDto
    {
        public string OgrElmAdi { get; set; }
        public string OgrElmSoyadi { get; set; }
        public string OgrenciAdi { get; set; }
        public string OgrenciSoyadi { get; set; }
    }
}
