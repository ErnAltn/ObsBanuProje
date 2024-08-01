using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Concrete
{
    public class Danismanlik : BaseEntityModel, IEntity
    {
        public int OgrElmID { get; set; }
        public int OgrenciId { get; set; }
    }
}
