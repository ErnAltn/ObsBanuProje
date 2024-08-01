
using System;
using System.Linq;
using Core.DataAccess.EntityFramework;
using Entities.Concrete;
using DataAccess.Concrete.EntityFramework.Contexts;
using DataAccess.Abstract;
namespace DataAccess.Concrete.EntityFramework
{
    public class ST_DerslikTuruRepository : EfEntityRepositoryBase<ST_DerslikTuru, ProjectDbContext>, IST_DerslikTuruRepository
    {
        public ST_DerslikTuruRepository(ProjectDbContext context) : base(context)
        {
        }
    }
}
