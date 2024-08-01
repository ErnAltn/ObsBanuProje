
using System;
using System.Linq;
using Core.DataAccess.EntityFramework;
using Entities.Concrete;
using DataAccess.Concrete.EntityFramework.Contexts;
using DataAccess.Abstract;
namespace DataAccess.Concrete.EntityFramework
{
    public class ST_OgretimDiliRepository : EfEntityRepositoryBase<ST_OgretimDili, ProjectDbContext>, IST_OgretimDiliRepository
    {
        public ST_OgretimDiliRepository(ProjectDbContext context) : base(context)
        {
        }
    }
}
