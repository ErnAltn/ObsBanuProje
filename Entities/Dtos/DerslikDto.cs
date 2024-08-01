using Core.Entities;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Dtos
{
    public class DerslikDto:BaseEntityModel,IDto
    {
        public string DerslikTuruAdi { get; set; }
        public string DerslikAdi { get; set; }
        public int Kapasite { get; set; }
    }
}
