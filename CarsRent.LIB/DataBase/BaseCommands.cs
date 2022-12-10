using CarsRent.LIB.Model;
using Microsoft.EntityFrameworkCore;

namespace CarsRent.LIB.DataBase
{
    public static class BaseCommands<T> where T : class, IBaseModel
    {
        private static void ChangeEntityState(T item, EntityState state)
        {
            var context = new ApplicationContext();
            context.Entry(item).State = state;
            context.SaveChanges();
        }
        
        public static void Add(T item)
        {
            ChangeEntityState(item, EntityState.Added);
        }

        public static void Modify(T item)
        {
            ChangeEntityState(item, EntityState.Modified);
        }

        public static void Delete(T item)
        {
            ChangeEntityState(item, EntityState.Deleted);
        }

        public static IEnumerable<T> SelectAll()
        {
            var context = new ApplicationContext();
            return context.Set<T>();
        }

        public static IEnumerable<T> SelectGroup(int startPoint, int count, string searchText = "")
        {
            var result = string.IsNullOrWhiteSpace(searchText) 
                ? SelectAll() 
                : Find(SelectAll(), searchText);

            return result.Skip(startPoint).Take(count);
        }
        
        public static T? SelectById(int? id)
        {
            return SelectAll().FirstOrDefault(x => x.Id == id);
        }

        private static IEnumerable<T> Find(IEnumerable<T> items, string searchText)
        {
            return items.Where(item => IsSearched(item, searchText));
        }

        private static bool IsSearched(T item, string searchText)
        {
            var searchWords = searchText.Split(' ');
            return searchWords.All(word => item.ToString()!.Contains(word));
        }
    }
}