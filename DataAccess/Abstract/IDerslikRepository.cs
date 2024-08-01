
using System;
using System.Collections.Generic;
using Core.DataAccess;
using Entities.Concrete;
using Entities.Dtos;

namespace DataAccess.Abstract
{
    public interface IDerslikRepository : IEntityRepository<Derslik>
    {
        List<DerslikDto> GetDerslikDto();
    }
}