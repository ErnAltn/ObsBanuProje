
using System;
using System.Linq;
using Core.DataAccess.EntityFramework;
using Entities.Concrete;
using DataAccess.Concrete.EntityFramework.Contexts;
using DataAccess.Abstract;
namespace DataAccess.Concrete.EntityFramework
{
    public class ST_OgrenciDurumRepository : EfEntityRepositoryBase<ST_OgrenciDurum, ProjectDbContext>, IST_OgrenciDurumRepository
    {
        public ST_OgrenciDurumRepository(ProjectDbContext context) : base(context)
        {
        }
    }
}
