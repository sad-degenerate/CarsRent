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

        public static IEnumerable<T> SelectGroup(int startPoint, int count)
        {
            var context = ApplicationContext.Instance();
            return context.Set<T>().Skip(startPoint - 1).Take(count);
        }

        public static IEnumerable<T> SelectAll()
        {
            var context = ApplicationContext.Instance();
            return context.Set<T>();
        }

        public static IEnumerable<T> FindAndSelect(string text, int startPoint, int count)
        {
            var items = Commands<T>.SelectAll();
            var itemsResult = new List<T>();
            var words = text.Split(' ');

            foreach (var item in items)
            {
                var itemText = item.ToString();

                var addToResult = true;
                foreach (var word in words)
                {
                    if (itemText.Contains(word) == false)
                    {
                        addToResult = false;
                        break;
                    }
                }

                if (addToResult == true)
                    itemsResult.Add(item);
            }

            return itemsResult.Skip(startPoint).Take(count).ToList();
        }
    }
}