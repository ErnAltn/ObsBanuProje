using System;
using Core.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Concrete
{
    public class Derslik : BaseEntityModel, IEntity
    {
        public int DerslikTuruId { get; set; }
        public string DerslikAdi { get; set; }
        public int Kapasite { get; set; }
    }
}
