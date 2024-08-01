using System;
using Core.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Concrete
{
    public class Sinav : BaseEntityModel, IEntity
    {
        public int DersAcmaId { get; set; }
        public int SinavTuruId { get; set; }
        public int DerslikId { get; set; }
        public int OgrElmID { get; set; }
        public int EtkiOrani { get; set; }
        public DateTime SinavTarihi { get; set; }
    }
}
