
using System;
using System.Linq;
using Core.DataAccess.EntityFramework;
using Entities.Concrete;
using DataAccess.Concrete.EntityFramework.Contexts;
using DataAccess.Abstract;
namespace DataAccess.Concrete.EntityFramework
{
    public class ST_DersAlmaDurumuRepository : EfEntityRepositoryBase<ST_DersAlmaDurumu, ProjectDbContext>, IST_DersAlmaDurumuRepository
    {
        public ST_DersAlmaDurumuRepository(ProjectDbContext context) : base(context)
        {
        }
    }
}
