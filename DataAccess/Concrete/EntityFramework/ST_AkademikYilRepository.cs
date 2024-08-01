
using System;
using System.Linq;
using Core.DataAccess.EntityFramework;
using Entities.Concrete;
using DataAccess.Concrete.EntityFramework.Contexts;
using DataAccess.Abstract;
namespace DataAccess.Concrete.EntityFramework
{
    public class ST_AkademikYilRepository : EfEntityRepositoryBase<ST_AkademikYil, ProjectDbContext>, IST_AkademikYilRepository
    {
        public ST_AkademikYilRepository(ProjectDbContext context) : base(context)
        {
        }
    }
}
