using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using BooksRental.Models;

namespace BookRental.Repositories
{
    public class Repository<T> where T: class
    {
        protected BookRentalEntities context = null;
        protected DbSet<T> DbSet { get; set; }

        public Repository()
        {
            context = new BookRentalEntities();
            DbSet = context.Set<T>();
        }

        public List<T> GetAll()
        {
            return DbSet.ToList();
        }

        public T Get (int? id)
        {
            return DbSet.Find(id);
        }

        public void Add(T pEntity)
        {
            DbSet.Add(pEntity);
        }

        public void Edit(T pEntity)
        {
            context.Entry(pEntity).State = EntityState.Modified;
        }

        public void Remove(T pEntity)
        {
            DbSet.Remove(pEntity);
        }

        public void Dispose()
        {            
            context.Dispose();
        }

        public void SaveChanges()
        {
            context.SaveChanges();
        }
    }
}