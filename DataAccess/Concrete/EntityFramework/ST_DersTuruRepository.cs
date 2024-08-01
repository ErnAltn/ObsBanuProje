
using System;
using System.Linq;
using Core.DataAccess.EntityFramework;
using Entities.Concrete;
using DataAccess.Concrete.EntityFramework.Contexts;
using DataAccess.Abstract;
namespace DataAccess.Concrete.EntityFramework
{
    public class ST_DersTuruRepository : EfEntityRepositoryBase<ST_DersTuru, ProjectDbContext>, IST_DersTuruRepository
    {
        public ST_DersTuruRepository(ProjectDbContext context) : base(context)
        {
        }
    }
}
