
using System;
using System.Linq;
using Core.DataAccess.EntityFramework;
using Entities.Concrete;
using DataAccess.Concrete.EntityFramework.Contexts;
using DataAccess.Abstract;
using System.Collections.Generic;
using Entities.Dtos;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;

namespace DataAccess.Concrete.EntityFramework
{
    public class SinavRepository : EfEntityRepositoryBase<Sinav, ProjectDbContext>, ISinavRepository
    {
        private readonly ProjectDbContext _context;
        public SinavRepository(ProjectDbContext context) : base(context)
        {
            _context = context;
        }

        public List<SinavDto> GetSinavDto()
        {
            var result = from sinav in _context.Sinav
                         join dersAcma in _context.DersAcma
                         on sinav.DersAcmaId equals dersAcma.Id
                         join sinavTuru in _context.ST_SinavTuru
                         on sinav.SinavTuruId equals sinavTuru.Id
                         join ogrElm in _context.OgretimElemani
                         on sinav.OgrElmID equals ogrElm.Id
                         join mufredat in _context.Mufredat
                         on dersAcma.MufredatId equals mufredat.Id
                         join bolum in _context.Bolum
                         on mufredat.BolumId equals bolum.Id
                         join dersHavuzu in _context.DersHavuzu
                         on mufredat.DersId equals dersHavuzu.Id
                         join derslik in _context.Derslik
                         on sinav.DerslikId equals derslik.Id
                         select new SinavDto
                         {
                             Id = sinav.Id,
                             BolumAdi=bolum.BolumAdi,
                             DersAdi=dersHavuzu.DersAdi,
                             DersKodu=dersHavuzu.DersKodu,
                             SinavTuru=sinavTuru.Ad,
                             Derslik=derslik.DerslikAdi,
                             OgrElmAdi=ogrElm.Adi,
                             OgrElmSoyadi=ogrElm.Soyadi,
                             EtkiOrani=sinav.EtkiOrani,
                             SinavTarihi=sinav.SinavTarihi,
                             CreatedDate=sinav.CreatedDate,
                             UpdatedDate=sinav.UpdatedDate,
                             DeletedDate = sinav.DeletedDate
                         };
                    return result.ToList();
        }
    }
}
