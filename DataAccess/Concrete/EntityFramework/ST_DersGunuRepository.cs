
using System;
using System.Linq;
using Core.DataAccess.EntityFramework;
using Entities.Concrete;
using DataAccess.Concrete.EntityFramework.Contexts;
using DataAccess.Abstract;
namespace DataAccess.Concrete.EntityFramework
{
    public class ST_DersGunuRepository : EfEntityRepositoryBase<ST_DersGunu, ProjectDbContext>, IST_DersGunuRepository
    {
        public ST_DersGunuRepository(ProjectDbContext context) : base(context)
        {
        }
    }
}
