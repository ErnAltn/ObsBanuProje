
using System;
using System.Linq;
using Core.DataAccess.EntityFramework;
using Entities.Concrete;
using DataAccess.Concrete.EntityFramework.Contexts;
using DataAccess.Abstract;
using System.Threading.Tasks;
using Entities.Dtos;
using System.Collections.Generic;

namespace DataAccess.Concrete.EntityFramework
{
    public class BolumRepository : EfEntityRepositoryBase<Bolum, ProjectDbContext>, IBolumRepository
    {
        private readonly ProjectDbContext _context;
        public BolumRepository(ProjectDbContext context) : base(context)
        {
            _context = context;
        }

        public List<BolumDto> GetBolumDtoAsync()
        {
            var result = from b in _context.Bolum
                         join pTuru in _context.ST_ProgramTuru
                         on b.ProgramTuruId equals pTuru.Id
                         join ogrTuru in _context.ST_OgretimTuru
                         on b.OgretimTuruId equals ogrTuru.Id
                         join ogrDili in _context.ST_OgretimDili
                         on b.OgretimDiliId equals ogrDili.Id
                         select new BolumDto
                         {
                             Id = b.Id,
                             BolumAdi = b.BolumAdi,
                             OgretimDiliAdi = ogrDili.Ad,
                             OgretimTuruAdi = ogrTuru.Ad,
                             ProgramTuruAdi = pTuru.Ad,
                             WebAdresi = b.WebAdresi,
                             CreatedDate = b.CreatedDate,
                             UpdatedDate = b.UpdatedDate,
                             DeletedDate = b.DeletedDate,
                         };
            return result.ToList();
        }
    }
}
