using System;
using Core.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Concrete
{
    public class DersProgrami : BaseEntityModel, IEntity
    {
        public int DersAcmaId { get; set; }
        public int DerslikId { get; set; }
        public int DersGunuId { get; set; }
        public string DersSaati { get; set; }
    }
}
