using AppAdvertisement.DataAccess.Contexts;
using AppAdvertisement.DataAccess.Interfaces;
using AppAdvertisement.DataAccess.Repositories;
using AppAdvertisement.Entities;
using Microsoft.EntityFrameworkCore.Diagnostics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppAdvertisement.DataAccess.UnitOfWork
{
    //DEPENDENCY INJECTİONI BURDA KULLANIYORUZ CONTEXT NESNESİ İÇİN
    //BU ŞEKİLDE REPOSİTORY DBCONTEXT NESNESİ İSTEDİĞİNDE TEK BİR CONTEXT NESNESİ GİTMİŞ OLUCAK
    public class Uow:IUow
    {
        private readonly AdvertisementContext _context;

        public Uow(AdvertisementContext context)
        {
            _context = context;
        }
        public IRepository<T> GetRepository<T>() where T : BaseEntity
        {
            return new Repository<T>(_context);//REPOSİTORY NESNESİ ÜRETİYORUZ CONSTRUCTIRINA DA _CONTEXT NESNEMİZİ GEÇİYORUZ
            //DI DAN ALDIĞIIMIZ
        }
        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
        public void SaveChanges()
        {
            _context.SaveChanges();
        }
    }
}
