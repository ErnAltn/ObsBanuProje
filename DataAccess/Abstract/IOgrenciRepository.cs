
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.DataAccess;
using Entities.Concrete;
using Entities.Dtos;

namespace DataAccess.Abstract
{
    public interface IOgrenciRepository : IEntityRepository<Ogrenci>
    {
        Task<IQueryable<OzlukBilgileriDto>>  GetOzlukBilgileriAsync(int userId);
        List<OgrenciDto> GetOgrenciDto();

        List<AlinanDerslerDto> GetOgrenciAlinanDersler(int userId);
        List<DersProgramiDto> GetDersProgramiAsync(int userId);
        List<OgrenciSinavDto> GetOgrenciNotBilgisiAsync(int userId);
    }
}