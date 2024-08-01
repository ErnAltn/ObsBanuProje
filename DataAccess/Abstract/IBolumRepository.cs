
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.DataAccess;
using Entities.Concrete;
using Entities.Dtos;

namespace DataAccess.Abstract
{
    public interface IBolumRepository : IEntityRepository<Bolum>
    {
        List<BolumDto> GetBolumDtoAsync();
    }
}