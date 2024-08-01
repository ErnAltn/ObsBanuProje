
using System;
using System.Linq;
using Core.DataAccess.EntityFramework;
using Entities.Concrete;
using DataAccess.Concrete.EntityFramework.Contexts;
using DataAccess.Abstract;
namespace DataAccess.Concrete.EntityFramework
{
    public class ST_SinavTuruRepository : EfEntityRepositoryBase<ST_SinavTuru, ProjectDbContext>, IST_SinavTuruRepository
    {
        public ST_SinavTuruRepository(ProjectDbContext context) : base(context)
        {
        }
    }
}
