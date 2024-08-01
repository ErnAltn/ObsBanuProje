
using System;
using System.Linq;
using Core.DataAccess.EntityFramework;
using Entities.Concrete;
using DataAccess.Concrete.EntityFramework.Contexts;
using DataAccess.Abstract;
namespace DataAccess.Concrete.EntityFramework
{
    public class DersAlmaRepository : EfEntityRepositoryBase<DersAlma, ProjectDbContext>, IDersAlmaRepository
    {
        public DersAlmaRepository(ProjectDbContext context) : base(context)
        {
        }
    }
}
