
using System;
using System.Linq;
using Core.DataAccess.EntityFramework;
using Entities.Concrete;
using DataAccess.Concrete.EntityFramework.Contexts;
using DataAccess.Abstract;
namespace DataAccess.Concrete.EntityFramework
{
    public class ST_AkademikDonemRepository : EfEntityRepositoryBase<ST_AkademikDonem, ProjectDbContext>, IST_AkademikDonemRepository
    {
        public ST_AkademikDonemRepository(ProjectDbContext context) : base(context)
        {
        }
    }
}
