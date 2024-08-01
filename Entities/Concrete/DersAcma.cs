using System;
using Core.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Concrete
{
    public class DersAcma : BaseEntityModel, IEntity
    {
        public int AkademikYilId { get; set; }
        public int AkademikDonemId { get; set; }
        public int MufredatId { get; set; }
        public int OgrElmId { get; set; }
        public int Kontenjan { get; set; }
    }
}
