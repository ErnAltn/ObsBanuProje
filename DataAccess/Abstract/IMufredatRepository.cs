
using System;
using System.Collections.Generic;
using Core.DataAccess;
using Entities.Concrete;
using Entities.Dtos;

namespace DataAccess.Abstract
{
    public interface IMufredatRepository : IEntityRepository<Mufredat>
    {
        List<MufredatDto> GetMufredatDto();
        List<MufredatDto> GetOgrenciMufredat(int userId);
    }
}