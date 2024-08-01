
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
    public class DersAcmaRepository : EfEntityRepositoryBase<DersAcma, ProjectDbContext>, IDersAcmaRepository
    {
           private readonly ProjectDbContext _context;

        public DersAcmaRepository(ProjectDbContext context) : base(context)
        {
            _context = context;
        }

        public List<DersAcmaDto> GetDersAcmaDtoAsync()
        {
            var result = from dAcma in _context.DersAcma
                         join m in _context.Mufredat
                         on dAcma.MufredatId equals m.Id
                         join bolum in _context.Bolum
                         on m.BolumId equals bolum.Id
                         join ders in _context.DersHavuzu
                         on m.DersId equals ders.Id
                         join akademikDonem in _context.ST_AkademikDonem
                         on dAcma.AkademikDonemId equals akademikDonem.Id
                         join akademikYil in _context.ST_AkademikYil
                         on dAcma.AkademikYilId equals akademikYil.Id
                         join ogrElm in _context.OgretimElemani
                         on dAcma.OgrElmId equals ogrElm.Id
                         join dersDili in _context.ST_DersDili
                         on ders.DersDiliId equals dersDili.Id
                         select new DersAcmaDto
                         {
                             Id = dAcma.Id,
                             AkademikYil = akademikYil.Ad,
                             AkademikDonem=akademikDonem.Ad,
                             DersAdi=ders.DersAdi,
                             DersDili=dersDili.Ad,
                             BolumAdi=bolum.BolumAdi,
                             OgrElmAdi=ogrElm.Adi,
                             OgrElmSoyadi=ogrElm.Soyadi,
                             Kontenjan=dAcma.Kontenjan,
                             CreatedDate=dAcma.CreatedDate,
                             UpdatedDate=dAcma.UpdatedDate,
                             DeletedDate=dAcma.DeletedDate,
                         };
            return result.ToList();
        }
    }
}
