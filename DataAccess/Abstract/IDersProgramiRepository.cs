
using System;
using System.Collections.Generic;
using Core.DataAccess;
using Entities.Concrete;
using Entities.Dtos;

namespace DataAccess.Abstract
{
    public interface IDersProgramiRepository : IEntityRepository<DersProgrami>
    {
        List<DersProgramiDto> GetDersProgramiDto();
    }
}