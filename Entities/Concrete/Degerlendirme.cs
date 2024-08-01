using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Concrete
{
    public class Degerlendirme : BaseEntityModel, IEntity
    {
        public int SinavId { get; set; }
        public int OgrenciId { get; set; }
        public float SinavNotu { get; set; }
    }
}
