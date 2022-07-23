using CarsRent.LIB.Model;
using Microsoft.EntityFrameworkCore;

namespace CarsRent.LIB.DataBase
{
    public static class Commands<T> where T : class, IBaseModel
    {
        public static void Add(T item)
        {
            var context = ApplicationContext.Instance();
            context.Entry(item).State = EntityState.Added;
            context.SaveChanges();
        }

        public static void Modify(T item)
        {
            var context = ApplicationContext.Instance();
            context.Entry(item).State = EntityState.Modified;
            context.SaveChanges();
        }

        public static void Delete(T item)
        {
            var context = ApplicationContext.Instance();
            context.Entry(item).State = EntityState.Deleted;
            context.SaveChanges();
        }

        public static IEnumerable<T> Select(int startPoint, int count)
        {
            var context = ApplicationContext.Instance();
            return context.Set<T>().Skip(startPoint - 1).Take(count);
        }

        public static T SelectById(int id)
        {
            var context = ApplicationContext.Instance();
            return context.Set<T>().Where(x => x.Id == id).Single();
        }
    }
}