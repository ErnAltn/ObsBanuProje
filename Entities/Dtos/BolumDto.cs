using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Dtos
{
    public class BolumDto:IDto
    {
        public int Id { get; set; }
        public string BolumAdi { get; set; }
        public string ProgramTuruAdi { get; set; }
        public string OgretimTuruAdi { get; set; }
        public string OgretimDiliAdi { get; set; }
        public string WebAdresi { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
        public DateTime DeletedDate { get; set; }
    }
}
