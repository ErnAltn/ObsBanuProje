
using System;
using System.Linq;
using Core.DataAccess.EntityFramework;
using Entities.Concrete;
using DataAccess.Concrete.EntityFramework.Contexts;
using DataAccess.Abstract;
using System.Threading.Tasks;
using Entities.Dtos;
using Core.Entities.Concrete;
using System.Collections.Generic;

namespace DataAccess.Concrete.EntityFramework
{
    public class OgretimElemaniRepository : EfEntityRepositoryBase<OgretimElemani, ProjectDbContext>, IOgretimElemaniRepository
    {
        private readonly ProjectDbContext _context;
        public OgretimElemaniRepository(ProjectDbContext context) : base(context)
        {
            _context = context;
        }

   

        public async Task<IQueryable<OzlukBilgileriDto>> GetOgretimElemaniOzlukBilgileri(int userId)
        {
            var result = from u in _context.Users
                         join o in _context.OgretimElemani
                         on u.UserId equals o.UserId
                         join b in _context.Bolum
            on o.BolumId equals b.Id
                         where u.UserId == userId
                         select new OzlukBilgileriDto
                         {
                             Adi = o.Adi,
                             Soyadi = o.Soyadi,
                             TcKimlikNo = o.TCKimlikNo,
                             DogumTarihi = o.DogumTarihi.ToShortDateString(),
                             BolumAdi = b.BolumAdi,
                             KayitTarihi = o.CreatedDate.ToShortDateString(),
                             KurumSicilNo = o.KurumSicilNo,
                             Unvan = o.Unvan,
                             Adres = u.Address,
                             TelefonNo = u.MobilePhones
                         };
            return await Task.FromResult(result.AsQueryable());
        }
        public List<MufredatDto> GetOgretimElemaniMufredat(int userId)
        {
                var result = from mufredat in _context.Mufredat
                             join bolum in _context.Bolum
                             on mufredat.BolumId equals bolum.Id
                             join ogretimElemani in _context.OgretimElemani
                             on bolum.Id equals ogretimElemani.BolumId
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
                             where ogretimElemani.UserId==userId
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
                                 DeletedDate = mufredat.DeletedDate
                             };
                return result.ToList();
        }

        public List<OgretimElemaniVerilenDerslerDto> GetOgretimElemaniVerilenDersler(int userId)
        {
            var result =
                from dersAcma in _context.DersAcma
                join ogretimElemani in _context.OgretimElemani
                on dersAcma.OgrElmId equals ogretimElemani.Id
                join bolum in _context.Bolum
                on ogretimElemani.BolumId equals bolum.Id
                join mufredat in _context.Mufredat
                on dersAcma.MufredatId equals mufredat.Id
                join dersHavuzu in _context.DersHavuzu
                on mufredat.DersId equals dersHavuzu.Id
                join dersTuru in _context.ST_DerslikTuru
                on dersHavuzu.DersturuId equals dersTuru.Id
                where ogretimElemani.UserId == userId
                select new OgretimElemaniVerilenDerslerDto
                {
                    BolumAdi=bolum.BolumAdi,
                    DersAdi=dersHavuzu.DersAdi,
                    DersKodu=dersHavuzu.DersKodu,
                    DersTuru=dersTuru.Ad,
                    Teorik=dersHavuzu.Teorik,
                    Kredi = dersHavuzu.Kredi,
                    ECTS = dersHavuzu.ECTS,
                    Uygulama=dersHavuzu.Uygulama,
                };
            return result.ToList();
        }

        public List<OgretimElemaniSınavlarDto> GetOgretimElemaniSinavlar(int userId)
        {
            var result = from sinav in _context.Sinav
                         join ogretimElemani in _context.OgretimElemani
                         on sinav.OgrElmID equals ogretimElemani.Id
                         join dersAcma in _context.DersAcma
                         on sinav.DersAcmaId equals dersAcma.Id
                         join mufredat in _context.Mufredat
                         on dersAcma.MufredatId equals mufredat.Id
                         join dersHavuzu in _context.DersHavuzu
                         on mufredat.DersId equals dersHavuzu.Id
                         join sinavTuru in _context.ST_SinavTuru
                         on sinav.SinavTuruId equals sinavTuru.Id
                         where ogretimElemani.UserId == userId
                         select new OgretimElemaniSınavlarDto
                         {
                             SinavId = sinav.Id,
                             DersAdi = dersHavuzu.DersAdi,
                             DersKodu = dersHavuzu.DersKodu,
                             EtkiOrani =sinav.EtkiOrani,
                             SinavTuru=sinavTuru.Ad,
                         };

            return result.ToList();

        }

        public List<DegerlendirmeDto> GetOgrenciler(int sinavId)
        {
            var result = from sinav in _context.Sinav
                         join dersAcma in _context.DersAcma
                         on sinav.DersAcmaId equals dersAcma.Id
                         join dersAlma in _context.DersAlma
                         on dersAcma.Id equals dersAlma.DersAcmaId
                         join ogrenci in _context.Ogrenci
                         on dersAlma.OgrenciId equals ogrenci.Id
                         where sinav.Id == sinavId
                         select new DegerlendirmeDto
                         {
                             OgrenciId = ogrenci.Id,
                             OgrenciNo = ogrenci.OgrenciNo,
                             OgrenciAdi = ogrenci.Adi,
                             OgrenciSoyadi = ogrenci.Soyadi,

                         };
            return result.ToList();
        }
    }
}
