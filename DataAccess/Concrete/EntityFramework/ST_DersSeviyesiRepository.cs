
using System;
using System.Linq;
using Core.DataAccess.EntityFramework;
using Entities.Concrete;
using DataAccess.Concrete.EntityFramework.Contexts;
using DataAccess.Abstract;
namespace DataAccess.Concrete.EntityFramework
{
    public class ST_DersSeviyesiRepository : EfEntityRepositoryBase<ST_DersSeviyesi, ProjectDbContext>, IST_DersSeviyesiRepository
    {
        public ST_DersSeviyesiRepository(ProjectDbContext context) : base(context)
        {
        }
    }
}
