using System;
using Core.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Concrete
{
    public class Mufredat : BaseEntityModel, IEntity
    {
        public int BolumId { get; set; }
        public int DersId { get; set; }
        public int AkedemikYilId { get; set; }
        public int AkedemikDonemId { get; set; }
        public int DersDonemi { get; set; }
    }
}
