
using System;
using System.Linq;
using Core.DataAccess.EntityFramework;
using Entities.Concrete;
using DataAccess.Concrete.EntityFramework.Contexts;
using DataAccess.Abstract;
namespace DataAccess.Concrete.EntityFramework
{
    public class ST_OgretimTuruRepository : EfEntityRepositoryBase<ST_OgretimTuru, ProjectDbContext>, IST_OgretimTuruRepository
    {
        public ST_OgretimTuruRepository(ProjectDbContext context) : base(context)
        {
        }
    }
}
