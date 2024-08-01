
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
    public class DanismanlikRepository : EfEntityRepositoryBase<Danismanlik, ProjectDbContext>, IDanismanlikRepository
    {

          private readonly ProjectDbContext _context;

        public DanismanlikRepository(ProjectDbContext context) : base(context)
        {
            _context = context;
        }

        public List<DanismanlikDto> GetDanismanlikDtoAsync()
        {

            var result = from d in _context.Danismanliks
                         join ogrElm in _context.OgretimElemani
                         on d.OgrElmID equals ogrElm.Id
                         join ogr in _context.Ogrenci
                         on d.OgrenciId equals ogr.Id
                         select new DanismanlikDto
                         {
                             Id = d.Id,
                             OgrElmAdi = ogrElm.Adi,
                             OgrElmSoyadi = ogrElm.Soyadi,
                             OgrenciAdi = ogr.Adi,
                             OgrenciSoyadi = ogr.Soyadi,
                             CreatedDate = d.CreatedDate,
                             UpdatedDate = d.UpdatedDate,
                             DeletedDate = d.DeletedDate,

                         };
            return result.ToList(); 
        }
    }
}
