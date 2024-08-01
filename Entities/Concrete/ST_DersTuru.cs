using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Concrete
{
    public class ST_DersTuru: IEntity
    {
        public int Id { get; set; }
        public string Ad { get; set; }
        public string Ekstra { get; set; }
    }
}
