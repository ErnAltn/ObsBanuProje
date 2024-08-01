﻿
using System;
using System.Linq;
using Core.DataAccess.EntityFramework;
using Entities.Concrete;
using DataAccess.Concrete.EntityFramework.Contexts;
using DataAccess.Abstract;
namespace DataAccess.Concrete.EntityFramework
{
    public class DegerlendirmeRepository : EfEntityRepositoryBase<Degerlendirme, ProjectDbContext>, IDegerlendirmeRepository
    {
        public DegerlendirmeRepository(ProjectDbContext context) : base(context)
        {
        }
    }
}