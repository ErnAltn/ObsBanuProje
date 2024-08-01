
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
    public class DersProgramiRepository : EfEntityRepositoryBase<DersProgrami, ProjectDbContext>, IDersProgramiRepository
    {
        private readonly ProjectDbContext _context;
        public DersProgramiRepository(ProjectDbContext context) : base(context)
        {
            _context = context;
        }

        public List<DersProgramiDto> GetDersProgramiDto()
        {
            var result = from dersProgrami in _context.DersProgrami
                         join dersAcma in _context.DersAcma
                         on dersProgrami.DersAcmaId equals dersAcma.Id
                         join derslik in _context.Derslik
                         on dersProgrami.DerslikId equals derslik.Id
                         join mufredat in _context.Mufredat
                         on dersAcma.MufredatId equals mufredat.Id
                         join dersHavuzu in _context.DersHavuzu
                         on mufredat.DersId equals dersHavuzu.Id
                         join bolum in _context.Bolum
                         on mufredat.BolumId equals bolum.Id
                         join dersGunu in _context.ST_DersGunu
                         on dersProgrami.DersGunuId equals dersGunu.Id
                         join ogrElm in _context.OgretimElemani
                         on dersAcma.OgrElmId equals ogrElm.Id
                         select new DersProgramiDto
                         {
                             Id = dersProgrami.Id,
                             OgretimElemaniBilgisi = ogrElm.Unvan + " " + ogrElm.Adi.ToUpper() + " " + ogrElm.Soyadi.ToUpper(),
                             BolumAdi =bolum.BolumAdi,
                             DersAdi=dersHavuzu.DersAdi,
                             DersKodu=dersHavuzu.DersKodu,
                             DerslikAdi=derslik.DerslikAdi,
                             DersGunu=dersGunu.Ad,
                             DersSaati=dersProgrami.DersSaati,
                             CreatedDate=dersProgrami.CreatedDate,
                             UpdatedDate= dersProgrami.UpdatedDate,
                             DeletedDate = dersProgrami.DeletedDate
                         };

                         return result.ToList();

        }
    }
}
