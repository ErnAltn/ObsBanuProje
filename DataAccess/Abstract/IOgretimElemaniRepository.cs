
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.DataAccess;
using Entities.Concrete;
using Entities.Dtos;

namespace DataAccess.Abstract
{
    public interface IOgretimElemaniRepository : IEntityRepository<OgretimElemani>
    {
        Task<IQueryable<OzlukBilgileriDto>> GetOgretimElemaniOzlukBilgileri(int userId);

        List<MufredatDto> GetOgretimElemaniMufredat(int userId);
        List<OgretimElemaniVerilenDerslerDto> GetOgretimElemaniVerilenDersler(int userId);
        List<OgretimElemaniSınavlarDto> GetOgretimElemaniSinavlar(int userId);
        List<DegerlendirmeDto> GetOgrenciler(int sinavId);
    }
}