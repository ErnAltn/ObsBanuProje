
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
    public class MufredatRepository : EfEntityRepositoryBase<Mufredat, ProjectDbContext>, IMufredatRepository
    {
        private readonly ProjectDbContext _context;
        public MufredatRepository(ProjectDbContext context) : base(context)
        {
            _context = context;
        }

        public List<MufredatDto> GetMufredatDto()
        {
            var result = from mufredat in _context.Mufredat
                         join bolum in _context.Bolum
                         on mufredat.BolumId equals bolum.Id
                         join dersHavuzu in _context.DersHavuzu
                         on mufredat.DersId equals dersHavuzu.Id
                         join dersDili in _context.ST_DersDili
                         on dersHavuzu.DersDiliId equals dersDili.Id
                         join dersTuru in _context.ST_DersTuru
                         on dersHavuzu.DersturuId equals dersTuru.Id
                         join dersSeviyesi in _context.ST_DersSeviyesi
                         on dersHavuzu.DersSeviyesiId equals dersSeviyesi.Id
                         join akademikYil in _context.ST_AkademikYil
                         on mufredat.AkedemikYilId equals akademikYil.Id
                         join akademikDonem in _context.ST_AkademikDonem
                         on mufredat.AkedemikDonemId equals akademikDonem.Id
                         select new MufredatDto
                         {
                             Id = mufredat.Id,
                             BolumAdi = bolum.BolumAdi,
                             DersAdi = dersHavuzu.DersAdi,
                             DersDili = dersDili.Ad,
                             DersTuru = dersTuru.Ad,
                             DersKodu = dersHavuzu.DersKodu,
                             DersSeviyesi = dersSeviyesi.Ad,
                             Kredi=dersHavuzu.Kredi,
                             ECTS=dersHavuzu.ECTS,
                             AkedemikYil=akademikYil.Ad,
                             AkedemikDonem =akademikDonem.Ad,
                             DersDonemi=mufredat.DersDonemi,
                             CreatedDate = mufredat.CreatedDate,
                             UpdatedDate = mufredat.UpdatedDate,
                             DeletedDate = mufredat.DeletedDate
                         };
            return result.ToList();

        }
        public List<MufredatDto> GetOgrenciMufredat(int userId)
        {
            var result = from mufredat in _context.Mufredat
                         join bolum in _context.Bolum
                         on mufredat.BolumId equals bolum.Id
                         join ogrenci in _context.Ogrenci
                         on bolum.Id equals ogrenci.BolumId
                         join user in _context.Users
                         on ogrenci.UserId equals user.UserId
                         join dersHavuzu in _context.DersHavuzu
                         on mufredat.DersId equals dersHavuzu.Id
                         join dersDili in _context.ST_DersDili
                         on dersHavuzu.DersDiliId equals dersDili.Id
                         join dersTuru in _context.ST_DersTuru
                         on dersHavuzu.DersturuId equals dersTuru.Id
                         join dersSeviyesi in _context.ST_DersSeviyesi
                         on dersHavuzu.DersSeviyesiId equals dersSeviyesi.Id
                         join akademikYil in _context.ST_AkademikYil
                         on mufredat.AkedemikYilId equals akademikYil.Id
                         join akademikDonem in _context.ST_AkademikDonem
                         on mufredat.AkedemikDonemId equals akademikDonem.Id
                         where ogrenci.UserId== userId && ogrenci.BolumId==mufredat.BolumId
                         select new MufredatDto
                         {
                             Id = mufredat.Id,
                             BolumAdi = bolum.BolumAdi,
                             DersAdi = dersHavuzu.DersAdi,
                             DersDili = dersDili.Ad,
                             DersTuru = dersTuru.Ad,
                             DersKodu = dersHavuzu.DersKodu,
                             DersSeviyesi = dersSeviyesi.Ad,
                             Kredi = dersHavuzu.Kredi,
                             ECTS = dersHavuzu.ECTS,
                             AkedemikYil = akademikYil.Ad,
                             AkedemikDonem = akademikDonem.Ad,
                             DersDonemi = mufredat.DersDonemi,
                             CreatedDate = mufredat.CreatedDate,
                             UpdatedDate = mufredat.UpdatedDate,
                             DeletedDate = mufredat.DeletedDate,
                           //  ECTSCount = (from dh in _context.DersHavuzu
                                      //    where dh.Id == mufredat.DersId 
                                       //   select dh.ECTS).Count()
                         };

            return result.ToList();

        }
    }
}
