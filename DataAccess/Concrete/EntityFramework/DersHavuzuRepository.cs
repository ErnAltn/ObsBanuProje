
using System;
using System.Linq;
using Core.DataAccess.EntityFramework;
using Entities.Concrete;
using DataAccess.Concrete.EntityFramework.Contexts;
using DataAccess.Abstract;
using System.Collections.Generic;
using Entities.Dtos;

namespace DataAccess.Concrete.EntityFramework
{
    public class DersHavuzuRepository : EfEntityRepositoryBase<DersHavuzu, ProjectDbContext>, IDersHavuzuRepository
    {
        private readonly ProjectDbContext _context;
        public DersHavuzuRepository(ProjectDbContext context) : base(context)
        {
            _context = context;
        }

        public List<DersHavuzuDto> GetDersHavuzuDtoAsync()
        {
            var result = from ders in _context.DersHavuzu
                         join dersDili in _context.ST_DersDili
                         on ders.DersDiliId equals dersDili.Id
                         join dersSeviyesi in _context.ST_DersSeviyesi
                         on ders.DersSeviyesiId equals dersSeviyesi.Id
                         join dersTuru in _context.ST_DerslikTuru
                         on ders.DersturuId equals dersTuru.Id
                         select new DersHavuzuDto
                         {
                             Id = ders.Id,
                             DersDili = dersDili.Ad,
                             DersSeviyesi = dersSeviyesi.Ad,
                             Dersturu = dersTuru.Ad,
                             DersKodu = ders.DersKodu,
                             DersAdi = ders.DersAdi,
                             Teorik = ders.Teorik,
                             Uygulama = ders.Uygulama,
                             Kredi = ders.Kredi,
                             ECTS = ders.ECTS,
                         };
            return result.ToList();
        }
    }
}
