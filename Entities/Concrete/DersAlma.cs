using System;
using Core.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Concrete
{
    public class DersAlma : BaseEntityModel, IEntity
    {
        public int DersAcmaId { get; set; }
        public int OgrenciId { get; set; }
        public int DersDurumId { get; set; }
    }
}
