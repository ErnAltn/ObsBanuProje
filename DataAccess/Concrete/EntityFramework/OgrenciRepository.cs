using System;
using System.Linq;
using Core.DataAccess.EntityFramework;
using Entities.Concrete;
using DataAccess.Concrete.EntityFramework.Contexts;
using DataAccess.Abstract;
using Entities.Dtos;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Core.Entities;
using System.Collections.Generic;

namespace DataAccess.Concrete.EntityFramework
{
    public class OgrenciRepository : EfEntityRepositoryBase<Ogrenci, ProjectDbContext>, IOgrenciRepository
    {
        private readonly ProjectDbContext _context;

        public OgrenciRepository(ProjectDbContext context) : base(context)
        {
            _context = context;
        }



        public List<OgrenciDto> GetOgrenciDto()
        {
            var result = from ogr in _context.Ogrenci
                         join bolum in _context.Bolum
                         on ogr.BolumId equals bolum.Id
                         join ogrDurum in _context.ST_OgrenciDurum
                         on ogr.DurumId equals ogrDurum.Id
                         join user in _context.Users
                         on ogr.UserId equals user.UserId
                         select new OgrenciDto
                         {
                             Id = ogr.Id,
                             BolumAdi = bolum.BolumAdi,
                             OgrenciNo = ogr.OgrenciNo,
                             Durum = ogrDurum.Ad,
                             AyrilmaTarihi = ogr.AyrilmaTarihi,
                             Adi = ogr.Adi,
                             Soyadi = ogr.Soyadi,
                             TcKimlikNo = ogr.TcKimlikNo,
                             Cinsiyet = ogr.Cinsiyet ? "Kadın" : "Erkek",
                             DogumTarihi = ogr.DogumTarihi.ToString(),
                             Mail = user.Email,
                             TelefonNo = user.MobilePhones,
                             Adres = user.Address,
                             CreatedDate = ogr.CreatedDate,
                             UpdatedDate = ogr.UpdatedDate,
                             DeletedDate = ogr.DeletedDate
                             // Sinif = (DateTime.Now - ogrenci.CreatedDate).Days / 182.625 <= 1 ? "1.Sınıf Güz " :
                             //((DateTime.Now - ogrenci.CreatedDate).Days / 182.625 <= 2 ? "1.Sınıf Bahar" :
                             //((DateTime.Now - ogrenci.CreatedDate).Days / 182.625 <= 3 ? "2.Sınıf Güz" :
                             //((DateTime.Now - ogrenci.CreatedDate).Days / 182.625 <= 4 ? "2.Sınıf Bahar" :
                             //((DateTime.Now - ogrenci.CreatedDate).Days / 182.625 <= 5 ? "3.Sınıf Güz" :
                             //((DateTime.Now - ogrenci.CreatedDate).Days / 182.625 <= 6 ? "3.Sınıf Bahar" :
                             //((DateTime.Now - ogrenci.CreatedDate).Days / 182.625 <= 7 ? "4.Sınıf Güz" :
                             //((DateTime.Now - ogrenci.CreatedDate).Days / 182.625 <= 8 ? "4.Sınıf Bahar" :
                             //((DateTime.Now - ogrenci.CreatedDate).Days / 182.625).ToString())))

                         };
            return result.ToList();
        }

        public async Task<IQueryable<OzlukBilgileriDto>> GetOzlukBilgileriAsync(int userId)
        {
            var result = from u in _context.Users
                         join o in _context.Ogrenci
                         on u.UserId equals o.UserId
                         join durum in _context.ST_OgrenciDurum
                         on o.DurumId equals durum.Id
                         join b in _context.Bolum
                         on o.BolumId equals b.Id
                         join d in _context.Danismanlik
                         on o.Id equals d.OgrenciId
                         join ogr in _context.OgretimElemani
                         on d.OgrElmID equals ogr.Id
                         where u.UserId == userId
                         select new OzlukBilgileriDto
                         {
                             Adi = o.Adi,
                             Soyadi = o.Soyadi,
                             TcKimlikNo = o.TcKimlikNo,
                             DogumTarihi = o.DogumTarihi.ToString(),
                             BolumAdi = b.BolumAdi,
                             OgrNo = o.OgrenciNo,
                             KayitTarihi = o.CreatedDate.ToShortDateString(),
                             Durum = durum.Ad,
                             AyrilmaTarihi = o.AyrilmaTarihi.ToShortDateString(),
                             DanismanAdi = ogr.Adi,
                             DanismanSoyadi = ogr.Soyadi,
                             Adres = u.Address,
                             TelefonNo = u.MobilePhones
                         };

            return await Task.FromResult(result.AsQueryable());
        }

        public List<AlinanDerslerDto> GetOgrenciAlinanDersler(int userId)
        {
            var result = from dersalma in _context.DersAlma
                         join ogrenci in _context.Ogrenci
                         on dersalma.OgrenciId equals ogrenci.Id
                         join dersAcma in _context.DersAcma
                         on dersalma.DersAcmaId equals dersAcma.Id
                         join ogrElemani in _context.OgretimElemani
                         on dersAcma.OgrElmId equals ogrElemani.Id
                         join mufredat in _context.Mufredat
                         on dersAcma.MufredatId equals mufredat.Id
                         join bolum in _context.Bolum
                         on mufredat.BolumId equals bolum.Id
                         join ders in _context.DersHavuzu
                         on mufredat.DersId equals ders.Id
                         join dersDili in _context.ST_DersDili
                         on ders.DersDiliId equals dersDili.Id
                         join dersTuru in _context.ST_DersTuru
                         on ders.DersturuId equals dersTuru.Id
                         where ogrenci.UserId == userId
                         select new AlinanDerslerDto
                         {
                             BolumAdi = bolum.BolumAdi,
                             DersAdi = ders.DersAdi,
                             DersDili = dersDili.Ad,
                             DersDonemi = mufredat.DersDonemi,
                             DersKodu = ders.DersKodu,
                             DersTuru = dersTuru.Ad,
                             ECTS = ders.ECTS,
                             Kredi = ders.Kredi,
                             Teorik = ders.Teorik,
                             Uygulama = ders.Uygulama,
                             OgretimElemaniBilgisi = ogrElemani.Unvan + " " + ogrElemani.Adi.ToUpper() + " " + ogrElemani.Soyadi.ToUpper(),
                             Sinif = (mufredat.DersDonemi == 1 || mufredat.DersDonemi == 2 ? "1.Sınıf" :
                             mufredat.DersDonemi == 3 || mufredat.DersDonemi == 4 ? "2.Sınıf" :
                             mufredat.DersDonemi == 5 || mufredat.DersDonemi == 6 ? "3.Sınıf" :
                             mufredat.DersDonemi == 7 || mufredat.DersDonemi == 8 ? "4.Sınıf" : null).ToString(),
                             OgrenciSinifi = (DateTime.Now - ogrenci.CreatedDate).Days / 182.625 <= 1 ? "1.Sınıf Güz" :
                             ((DateTime.Now - ogrenci.CreatedDate).Days / 182.625 <= 2 ? "1.Sınıf Bahar" :
                             ((DateTime.Now - ogrenci.CreatedDate).Days / 182.625 <= 3 ? "2.Sınıf Güz" :
                             ((DateTime.Now - ogrenci.CreatedDate).Days / 182.625 <= 4 ? "2.Sınıf Bahar" :
                             ((DateTime.Now - ogrenci.CreatedDate).Days / 182.625 <= 5 ? "3.Sınıf Güz" :
                             ((DateTime.Now - ogrenci.CreatedDate).Days / 182.625 <= 6 ? "3.Sınıf Bahar" :
                             ((DateTime.Now - ogrenci.CreatedDate).Days / 182.625 <= 7 ? "4.Sınıf Güz" :
                             ((DateTime.Now - ogrenci.CreatedDate).Days / 182.625 <= 8 ? "4.Sınıf Bahar" :
                             ((DateTime.Now - ogrenci.CreatedDate).Days / 182.625).ToString()))))))),
                             OgrenciDonemi = (DateTime.Now - ogrenci.CreatedDate).Days / 182.625 <= 1 ? $"{ogrenci.CreatedDate.Year}-{ogrenci.CreatedDate.Year + 1} Güz Dönemi" :
                            (DateTime.Now - ogrenci.CreatedDate).Days / 182.625 <= 2 ? $"{ogrenci.CreatedDate.Year}-{ogrenci.CreatedDate.Year + 1} Bahar Dönemi" :
                            (DateTime.Now - ogrenci.CreatedDate).Days / 182.625 <= 3 ? $"{ogrenci.CreatedDate.Year + 1}-{ogrenci.CreatedDate.Year + 2} Güz Dönemi" :
                            (DateTime.Now - ogrenci.CreatedDate).Days / 182.625 <= 4 ? $"{ogrenci.CreatedDate.Year + 1}-{ogrenci.CreatedDate.Year + 2} Bahar Dönemi" :
                            (DateTime.Now - ogrenci.CreatedDate).Days / 182.625 <= 5 ? $"{ogrenci.CreatedDate.Year + 2}-{ogrenci.CreatedDate.Year + 3} Güz Dönemi" :
                            (DateTime.Now - ogrenci.CreatedDate).Days / 182.625 <= 6 ? $"{ogrenci.CreatedDate.Year + 2}-{ogrenci.CreatedDate.Year + 3} Bahar Dönemi" :
                            (DateTime.Now - ogrenci.CreatedDate).Days / 182.625 <= 7 ? $"{ogrenci.CreatedDate.Year + 3}-{ogrenci.CreatedDate.Year + 4} Güz Dönemi" :
                            (DateTime.Now - ogrenci.CreatedDate).Days / 182.625 <= 8 ? $"{ogrenci.CreatedDate.Year + 3}-{ogrenci.CreatedDate.Year + 4} Bahar Dönemi" :
                            "Bulunamadı",
                             DersVerilenDonem = (DateTime.Now - dersalma.CreatedDate).Days / 182.625 <= 1 ? $"{dersalma.CreatedDate.Year}-{dersalma.CreatedDate.Year + 1} Güz Dönemi" :
                            (DateTime.Now - ogrenci.CreatedDate).Days / 182.625 <= 2 ? $"{dersalma.CreatedDate.Year - 1}-{dersalma.CreatedDate.Year} Bahar Dönemi" :
                            (DateTime.Now - ogrenci.CreatedDate).Days / 182.625 <= 3 ? $"{dersalma.CreatedDate.Year}-{dersalma.CreatedDate.Year + 1} Güz Dönemi" :
                            (DateTime.Now - ogrenci.CreatedDate).Days / 182.625 <= 4 ? $"{dersalma.CreatedDate.Year - 1}-{dersalma.CreatedDate.Year} Bahar Dönemi" :
                            (DateTime.Now - ogrenci.CreatedDate).Days / 182.625 <= 5 ? $"{dersalma.CreatedDate.Year}-{dersalma.CreatedDate.Year - 1} Güz Dönemi" :
                            (DateTime.Now - ogrenci.CreatedDate).Days / 182.625 <= 6 ? $"{dersalma.CreatedDate.Year - 1}-{dersalma.CreatedDate.Year} Bahar Dönemi" :
                            (DateTime.Now - ogrenci.CreatedDate).Days / 182.625 <= 7 ? $"{dersalma.CreatedDate.Year}-{dersalma.CreatedDate.Year + 1} Güz Dönemi" :
                            (DateTime.Now - ogrenci.CreatedDate).Days / 182.625 <= 8 ? $"{dersalma.CreatedDate.Year - 1}-{dersalma.CreatedDate.Year} Bahar Dönemi" :
                            "Bulunamadı",

                         };

            return result.ToList();

        }

        public List<DersProgramiDto> GetDersProgramiAsync(int userId)
        {
            var result = from dersalma in _context.DersAlma
                         join ogrenci in _context.Ogrenci
                         on dersalma.OgrenciId equals ogrenci.Id
                         join dersAcma in _context.DersAcma
                         on dersalma.DersAcmaId equals dersAcma.Id
                         join ogrElemani in _context.OgretimElemani
                         on dersAcma.OgrElmId equals ogrElemani.Id
                         join mufredat in _context.Mufredat
                         on dersAcma.MufredatId equals mufredat.Id
                         join bolum in _context.Bolum
                         on mufredat.BolumId equals bolum.Id
                         join ders in _context.DersHavuzu
                         on mufredat.DersId equals ders.Id
                         join dersDili in _context.ST_DersDili
                         on ders.DersDiliId equals dersDili.Id
                         join dersTuru in _context.ST_DersTuru
                         on ders.DersturuId equals dersTuru.Id
                         join dersProgrami in _context.DersProgrami
                         on dersAcma.Id equals dersProgrami.DersAcmaId
                         join dersGunu in _context.ST_DersGunu
                         on dersProgrami.DersGunuId equals dersGunu.Id
                         join derslik in _context.Derslik
                         on dersProgrami.DerslikId equals derslik.Id
                         where ogrenci.UserId == userId
                         select new DersProgramiDto
                         {
                             Id = dersProgrami.Id,
                             BolumAdi = bolum.BolumAdi,
                             DersAdi = ders.DersAdi,
                             DersKodu = ders.DersKodu,
                             OgretimElemaniBilgisi = ogrElemani.Unvan + " " + ogrElemani.Adi.ToUpper() + " " + ogrElemani.Soyadi.ToUpper(),
                             OgrenciSinifi = (DateTime.Now - ogrenci.CreatedDate).Days / 182.625 <= 1 ? "1.Sınıf Güz" :
                             ((DateTime.Now - ogrenci.CreatedDate).Days / 182.625 <= 2 ? "1.Sınıf Bahar" :
                             ((DateTime.Now - ogrenci.CreatedDate).Days / 182.625 <= 3 ? "2.Sınıf Güz" :
                             ((DateTime.Now - ogrenci.CreatedDate).Days / 182.625 <= 4 ? "2.Sınıf Bahar" :
                             ((DateTime.Now - ogrenci.CreatedDate).Days / 182.625 <= 5 ? "3.Sınıf Güz" :
                             ((DateTime.Now - ogrenci.CreatedDate).Days / 182.625 <= 6 ? "3.Sınıf Bahar" :
                             ((DateTime.Now - ogrenci.CreatedDate).Days / 182.625 <= 7 ? "4.Sınıf Güz" :
                             ((DateTime.Now - ogrenci.CreatedDate).Days / 182.625 <= 8 ? "4.Sınıf Bahar" :
                             ((DateTime.Now - ogrenci.CreatedDate).Days / 182.625).ToString()))))))),
                             OgrenciDonemi = (DateTime.Now - ogrenci.CreatedDate).Days / 182.625 <= 1 ? $"{ogrenci.CreatedDate.Year}-{ogrenci.CreatedDate.Year + 1} Güz Dönemi" :
                            (DateTime.Now - ogrenci.CreatedDate).Days / 182.625 <= 2 ? $"{ogrenci.CreatedDate.Year}-{ogrenci.CreatedDate.Year + 1} Bahar Dönemi" :
                            (DateTime.Now - ogrenci.CreatedDate).Days / 182.625 <= 3 ? $"{ogrenci.CreatedDate.Year + 1}-{ogrenci.CreatedDate.Year + 2} Güz Dönemi" :
                            (DateTime.Now - ogrenci.CreatedDate).Days / 182.625 <= 4 ? $"{ogrenci.CreatedDate.Year + 1}-{ogrenci.CreatedDate.Year + 2} Bahar Dönemi" :
                            (DateTime.Now - ogrenci.CreatedDate).Days / 182.625 <= 5 ? $"{ogrenci.CreatedDate.Year + 2}-{ogrenci.CreatedDate.Year + 3} Güz Dönemi" :
                            (DateTime.Now - ogrenci.CreatedDate).Days / 182.625 <= 6 ? $"{ogrenci.CreatedDate.Year + 2}-{ogrenci.CreatedDate.Year + 3} Bahar Dönemi" :
                            (DateTime.Now - ogrenci.CreatedDate).Days / 182.625 <= 7 ? $"{ogrenci.CreatedDate.Year + 3}-{ogrenci.CreatedDate.Year + 4} Güz Dönemi" :
                            (DateTime.Now - ogrenci.CreatedDate).Days / 182.625 <= 8 ? $"{ogrenci.CreatedDate.Year + 3}-{ogrenci.CreatedDate.Year + 4} Bahar Dönemi" :
                            "Bulunamadı",
                             DersVerilenDonem = (DateTime.Now - ogrenci.CreatedDate).Days / 182.625 <= 1 ? $"{dersalma.CreatedDate.Year}-{dersalma.CreatedDate.Year + 1} Güz Dönemi" :
                            (DateTime.Now - ogrenci.CreatedDate).Days / 182.625 <= 2 ? $"{dersalma.CreatedDate.Year - 1}-{dersalma.CreatedDate.Year} Bahar Dönemi" :
                            (DateTime.Now - ogrenci.CreatedDate).Days / 182.625 <= 3 ? $"{dersalma.CreatedDate.Year}-{dersalma.CreatedDate.Year + 1} Güz Dönemi" :
                            (DateTime.Now - ogrenci.CreatedDate).Days / 182.625 <= 4 ? $"{dersalma.CreatedDate.Year - 1}-{dersalma.CreatedDate.Year} Bahar Dönemi" :
                            (DateTime.Now - ogrenci.CreatedDate).Days / 182.625 <= 5 ? $"{dersalma.CreatedDate.Year}-{dersalma.CreatedDate.Year - 1} Güz Dönemi" :
                            (DateTime.Now - ogrenci.CreatedDate).Days / 182.625 <= 6 ? $"{dersalma.CreatedDate.Year - 1}-{dersalma.CreatedDate.Year} Bahar Dönemi" :
                            (DateTime.Now - ogrenci.CreatedDate).Days / 182.625 <= 7 ? $"{dersalma.CreatedDate.Year}-{dersalma.CreatedDate.Year + 1} Güz Dönemi" :
                            (DateTime.Now - ogrenci.CreatedDate).Days / 182.625 <= 8 ? $"{dersalma.CreatedDate.Year - 1}-{dersalma.CreatedDate.Year} Bahar Dönemi" :
                            "Bulunamadı",
                             DersGunu = dersGunu.Ad,
                             DerslikAdi = derslik.DerslikAdi,
                             DersSaati = dersProgrami.DersSaati,
                             CreatedDate = dersProgrami.CreatedDate,
                             UpdatedDate = dersProgrami.UpdatedDate,
                             DeletedDate = dersProgrami.DeletedDate,


                         };

            return result.ToList();


        }


        public List<OgrenciSinavDto> GetOgrenciNotBilgisiAsync(int userId)
        {

            var query = from degerlendirme in _context.Degerlendirme
                        join sinav in _context.Sinav on degerlendirme.SinavId equals sinav.Id
                        join dersAcma in _context.DersAcma on sinav.DersAcmaId equals dersAcma.Id
                        join mufredat in _context.Mufredat on dersAcma.MufredatId equals mufredat.Id
                        join dersHavuzu in _context.DersHavuzu on mufredat.DersId equals dersHavuzu.Id
                        join ogrenci in _context.Ogrenci on degerlendirme.OgrenciId equals ogrenci.Id
                        join sinavTuru in _context.ST_SinavTuru on sinav.SinavTuruId equals sinavTuru.Id
                        where ogrenci.UserId == userId
                        group new { degerlendirme, sinav, dersAcma, dersHavuzu, ogrenci } by new
                        {
                            dersAcma.Id,
                            dersHavuzu.DersKodu,
                            dersHavuzu.DersAdi
                        } into grouped
                        select new OgrenciSinavDto
                        {
                            DersKodu = grouped.Key.DersKodu,
                            DersAdi = grouped.Key.DersAdi,
                            VizeNotu = grouped.Max(g => g.sinav.SinavTuruId == 1 ? g.degerlendirme.SinavNotu : (float?)null),
                            FinalNotu = grouped.Max(g => g.sinav.SinavTuruId == 3 ? g.degerlendirme.SinavNotu : (float?)null),
                            ButunlemeNotu = grouped.Max(g => g.sinav.SinavTuruId == 4 ? g.degerlendirme.SinavNotu : (float?)null),
                            OgrenciSinifi = (DateTime.Now - grouped.First().ogrenci.CreatedDate).Days / 182.625 <= 1 ? "1.Sınıf Güz" :
                            ((DateTime.Now - grouped.First().ogrenci.CreatedDate).Days / 182.625 <= 2 ? "1.Sınıf Bahar" :
                            ((DateTime.Now - grouped.First().ogrenci.CreatedDate).Days / 182.625 <= 3 ? "2.Sınıf Güz" :
                            ((DateTime.Now - grouped.First().ogrenci.CreatedDate).Days / 182.625 <= 4 ? "2.Sınıf Bahar" :
                            ((DateTime.Now - grouped.First().ogrenci.CreatedDate).Days / 182.625 <= 5 ? "3.Sınıf Güz" :
                            ((DateTime.Now - grouped.First().ogrenci.CreatedDate).Days / 182.625 <= 6 ? "3.Sınıf Bahar" :
                            ((DateTime.Now - grouped.First().ogrenci.CreatedDate).Days / 182.625 <= 7 ? "4.Sınıf Güz" :
                            ((DateTime.Now - grouped.First().ogrenci.CreatedDate).Days / 182.625 <= 8 ? "4.Sınıf Bahar" :
                            ((DateTime.Now - grouped.First().ogrenci.CreatedDate).Days / 182.625).ToString()))))))),
                            OgrenciDonemi = (DateTime.Now - grouped.First().ogrenci.CreatedDate).Days / 182.625 <= 1 ? $"{grouped.First().ogrenci.CreatedDate.Year}-{grouped.First().ogrenci.CreatedDate.Year + 1} Güz Dönemi" :
                            (DateTime.Now - grouped.First().ogrenci.CreatedDate).Days / 182.625 <= 2 ? $"{grouped.First().ogrenci.CreatedDate.Year}-{grouped.First().ogrenci.CreatedDate.Year + 1} Bahar Dönemi" :
                            (DateTime.Now - grouped.First().ogrenci.CreatedDate).Days / 182.625 <= 3 ? $"{grouped.First().ogrenci.CreatedDate.Year + 1}-{grouped.First().ogrenci.CreatedDate.Year + 2} Güz Dönemi" :
                            (DateTime.Now - grouped.First().ogrenci.CreatedDate).Days / 182.625 <= 4 ? $"{grouped.First().ogrenci.CreatedDate.Year + 1}-{grouped.First().ogrenci.CreatedDate.Year + 2} Bahar Dönemi" :
                            (DateTime.Now - grouped.First().ogrenci.CreatedDate).Days / 182.625 <= 5 ? $"{grouped.First().ogrenci.CreatedDate.Year + 2}-{grouped.First().ogrenci.CreatedDate.Year + 3} Güz Dönemi" :
                            (DateTime.Now - grouped.First().ogrenci.CreatedDate).Days / 182.625 <= 6 ? $"{grouped.First().ogrenci.CreatedDate.Year + 2}-{grouped.First().ogrenci.CreatedDate.Year + 3} Bahar Dönemi" :
                            (DateTime.Now - grouped.First().ogrenci.CreatedDate).Days / 182.625 <= 7 ? $"{grouped.First().ogrenci.CreatedDate.Year + 3}-{grouped.First().ogrenci.CreatedDate.Year + 4} Güz Dönemi" :
                            (DateTime.Now - grouped.First().ogrenci.CreatedDate).Days / 182.625 <= 8 ? $"{grouped.First().ogrenci.CreatedDate.Year + 3}-{grouped.First().ogrenci.CreatedDate.Year + 4} Bahar Dönemi" :
                            "Bulunamadı",
                            NotOrt = grouped.Max(g => g.sinav.SinavTuruId == 4 ? g.degerlendirme.SinavNotu : (float?)null) == (float?)null? grouped.Max(g => g.sinav.SinavTuruId == 1 ? g.degerlendirme.SinavNotu : (float?)null) * 0.4f+ grouped.Max(g => g.sinav.SinavTuruId == 3 ? g.degerlendirme.SinavNotu : (float?)null) * 0.6f: grouped.Max(g => g.sinav.SinavTuruId == 1 ? g.degerlendirme.SinavNotu : (float?)null) * 0.4f + grouped.Max(g => g.sinav.SinavTuruId == 4 ? g.degerlendirme.SinavNotu : (float?)null) * 0.6f,

                            SinavDonemi =
                (grouped.First().ogrenci.CreatedDate.Month < grouped.First().sinav.CreatedDate.Month && grouped.First().sinav.CreatedDate.Month < 12) ? $"{grouped.First().sinav.CreatedDate.Year}-{grouped.First().sinav.CreatedDate.Year + 1} Güz Dönemi" :
                (new DateTime(grouped.First().ogrenci.CreatedDate.Year + 1, 1, 1)) < grouped.First().sinav.CreatedDate && grouped.First().sinav.CreatedDate < (new DateTime(grouped.First().ogrenci.CreatedDate.Year + 1, 3, 1)) ? $"{grouped.First().sinav.CreatedDate.Year - 1}-{grouped.First().sinav.CreatedDate.Year} Güz Dönemi" :
                (new DateTime(grouped.First().ogrenci.CreatedDate.Year + 1, 3, 1)) < grouped.First().sinav.CreatedDate && grouped.First().sinav.CreatedDate < (new DateTime(grouped.First().ogrenci.CreatedDate.Year + 1, 5, 15)) ? $"{grouped.First().sinav.CreatedDate.Year - 1}-{grouped.First().sinav.CreatedDate.Year} Bahar Dönemi" :
                (new DateTime(grouped.First().ogrenci.CreatedDate.Year + 1, 5, 15)) < grouped.First().sinav.CreatedDate && grouped.First().sinav.CreatedDate < (new DateTime(grouped.First().ogrenci.CreatedDate.Year + 1, 7, 15)) ? $"{grouped.First().sinav.CreatedDate.Year - 1}-{grouped.First().sinav.CreatedDate.Year} Bahar Dönemi" :
                (new DateTime(grouped.First().ogrenci.CreatedDate.Year + 1, 9, 1)) < grouped.First().sinav.CreatedDate && grouped.First().sinav.CreatedDate < (new DateTime(grouped.First().ogrenci.CreatedDate.Year + 1, 12, 1)) ? $"{grouped.First().sinav.CreatedDate.Year}-{grouped.First().sinav.CreatedDate.Year + 1} Güz Dönemi" :
                (new DateTime(grouped.First().ogrenci.CreatedDate.Year + 2, 1, 1)) < grouped.First().sinav.CreatedDate && grouped.First().sinav.CreatedDate < (new DateTime(grouped.First().ogrenci.CreatedDate.Year + 2, 3, 1)) ? $"{grouped.First().sinav.CreatedDate.Year - 1}-{grouped.First().sinav.CreatedDate.Year} Güz Dönemi" :
                (new DateTime(grouped.First().ogrenci.CreatedDate.Year + 2, 3, 1)) < grouped.First().sinav.CreatedDate && grouped.First().sinav.CreatedDate < (new DateTime(grouped.First().ogrenci.CreatedDate.Year + 2, 5, 15)) ? $"{grouped.First().sinav.CreatedDate.Year - 1}-{grouped.First().sinav.CreatedDate.Year} Bahar Dönemi" :
                (new DateTime(grouped.First().ogrenci.CreatedDate.Year + 2, 5, 15)) < grouped.First().sinav.CreatedDate && grouped.First().sinav.CreatedDate < (new DateTime(grouped.First().ogrenci.CreatedDate.Year + 2, 7, 15)) ? $"{grouped.First().sinav.CreatedDate.Year - 1}-{grouped.First().sinav.CreatedDate.Year} Bahar Dönemi" :
                (new DateTime(grouped.First().ogrenci.CreatedDate.Year + 2, 9, 1)) < grouped.First().sinav.CreatedDate && grouped.First().sinav.CreatedDate < (new DateTime(grouped.First().ogrenci.CreatedDate.Year + 2, 12, 1)) ? $"{grouped.First().sinav.CreatedDate.Year}-{grouped.First().sinav.CreatedDate.Year + 1} Güz Dönemi" :
                (new DateTime(grouped.First().ogrenci.CreatedDate.Year + 3, 1, 1)) < grouped.First().sinav.CreatedDate && grouped.First().sinav.CreatedDate < (new DateTime(grouped.First().ogrenci.CreatedDate.Year + 3, 3, 1)) ? $"{grouped.First().sinav.CreatedDate.Year - 1}-{grouped.First().sinav.CreatedDate.Year} Güz Dönemi" :
                (new DateTime(grouped.First().ogrenci.CreatedDate.Year + 3, 3, 1)) < grouped.First().sinav.CreatedDate && grouped.First().sinav.CreatedDate < (new DateTime(grouped.First().ogrenci.CreatedDate.Year + 3, 5, 15)) ? $"{grouped.First().sinav.CreatedDate.Year - 1}-{grouped.First().sinav.CreatedDate.Year} Bahar Dönemi" :
                (new DateTime(grouped.First().ogrenci.CreatedDate.Year + 3, 5, 15)) < grouped.First().sinav.CreatedDate && grouped.First().sinav.CreatedDate < (new DateTime(grouped.First().ogrenci.CreatedDate.Year + 3, 7, 15)) ? $"{grouped.First().sinav.CreatedDate.Year - 1}-{grouped.First().sinav.CreatedDate.Year} Bahar Dönemi" :
                (new DateTime(grouped.First().ogrenci.CreatedDate.Year + 3, 9, 1)) < grouped.First().sinav.CreatedDate && grouped.First().sinav.CreatedDate < (new DateTime(grouped.First().ogrenci.CreatedDate.Year + 3, 12, 1)) ? $"{grouped.First().sinav.CreatedDate.Year}-{grouped.First().sinav.CreatedDate.Year + 1} Güz Dönemi" :
                (new DateTime(grouped.First().ogrenci.CreatedDate.Year + 4, 1, 1)) < grouped.First().sinav.CreatedDate && grouped.First().sinav.CreatedDate < (new DateTime(grouped.First().ogrenci.CreatedDate.Year + 4, 3, 1)) ? $"{grouped.First().sinav.CreatedDate.Year - 1}-{grouped.First().sinav.CreatedDate.Year} Güz Dönemi" :
                (new DateTime(grouped.First().ogrenci.CreatedDate.Year + 4, 3, 1)) < grouped.First().sinav.CreatedDate && grouped.First().sinav.CreatedDate < (new DateTime(grouped.First().ogrenci.CreatedDate.Year + 4, 5, 15)) ? $"{grouped.First().sinav.CreatedDate.Year - 1} - {grouped.First().sinav.CreatedDate.Year} Bahar Dönemi" :
                (new DateTime(grouped.First().ogrenci.CreatedDate.Year + 4, 5, 15)) < grouped.First().sinav.CreatedDate && grouped.First().sinav.CreatedDate < (new DateTime(grouped.First().ogrenci.CreatedDate.Year + 4, 7, 15)) ? $"{grouped.First().sinav.CreatedDate.Year - 1}-{grouped.First().sinav.CreatedDate.Year} Bahar Dönemi" :
                "Bulunamadı"

                        };

            return query.ToList();


        }
    }
}
