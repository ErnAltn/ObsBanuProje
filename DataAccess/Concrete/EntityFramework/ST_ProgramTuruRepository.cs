
using System;
using System.Linq;
using Core.DataAccess.EntityFramework;
using Entities.Concrete;
using DataAccess.Concrete.EntityFramework.Contexts;
using DataAccess.Abstract;
namespace DataAccess.Concrete.EntityFramework
{
    public class ST_ProgramTuruRepository : EfEntityRepositoryBase<ST_ProgramTuru, ProjectDbContext>, IST_ProgramTuruRepository
    {
        public ST_ProgramTuruRepository(ProjectDbContext context) : base(context)
        {
        }
    }
}
