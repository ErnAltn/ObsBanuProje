
using System;
using System.Collections.Generic;
using Core.DataAccess;
using Entities.Concrete;
using Entities.Dtos;

namespace DataAccess.Abstract
{
    public interface IDanismanlikRepository : IEntityRepository<Danismanlik>
    {
        List<DanismanlikDto> GetDanismanlikDtoAsync();
    }
}