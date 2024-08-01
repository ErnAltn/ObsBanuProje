
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
    public class DerslikRepository : EfEntityRepositoryBase<Derslik, ProjectDbContext>, IDerslikRepository
    {
        private readonly ProjectDbContext _context;
        public DerslikRepository(ProjectDbContext context) : base(context)
        {
            _context = context;
        }

        public List<DerslikDto> GetDerslikDto()
        {
            var result = from derslik in _context.Dersliks
                         join derslikTuru in _context.ST_DerslikTuru
                         on derslik.DerslikTuruId equals derslikTuru.Id
                         select new DerslikDto
                         {
                             Id = derslik.Id,
                             DerslikAdi = derslik.DerslikAdi,
                             DerslikTuruAdi = derslikTuru.Ad,
                             Kapasite = derslik.Kapasite,
                             CreatedDate = derslik.CreatedDate,
                             UpdatedDate = derslik.UpdatedDate,
                             DeletedDate = derslik.DeletedDate,
                         };
            return result.ToList();
        }
    }
}
