using CarsRent.LIB.Model;
using Microsoft.EntityFrameworkCore;

namespace CarsRent.LIB.DataBase
{
    public static class Commands<T> where T : class, IBaseModel
    {
        public static void Add(T item)
        {
            using var context = ApplicationContext.Instance();
            context.Entry(item).State = EntityState.Added;
            context.SaveChanges();
        }

        public static void Modify(T item)
        {
            using var context = ApplicationContext.Instance();
            context.Entry(item).State = EntityState.Modified;
            context.SaveChanges();
        }

        public static void Delete(T item)
        {
            using var context = ApplicationContext.Instance();
            context.Entry(item).State = EntityState.Deleted;
            context.SaveChanges();
        }

        public static IEnumerable<T> Select(int startPoint, int count, int? id = null)
        {
            using var context = ApplicationContext.Instance();
            var items = context.Set<T>();

            // TODO: Если элемент не один?

            if (id != null)
                return items.Where(x => x.Id == id);
            return items.Skip(startPoint - 1).Take(count);
        }
    }
}