using CarsRent.LIB.Model;
using Microsoft.EntityFrameworkCore;

namespace CarsRent.LIB.DataBase
{
    public static class BaseCommands<T> where T : class, IBaseModel
    {
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
            return ApplicationContext.Instance().Set<T>().ToList();
        }

        public static IEnumerable<T> SelectGroup(int startPoint, int count)
        {
            return SelectAll().Skip(startPoint).Take(count).ToList();
        }
        
        public static T? SelectById(int? id)
        {
            return SelectAll().FirstOrDefault(x => x.Id == id);
        }
        
        public static IEnumerable<T> Find(IEnumerable<T> items, string searchText)
        {
            return items.Where(item => IsSearched(item, searchText)).ToList();
        }
        
        public static IEnumerable<T> FindAndSelect(string searchText, int startPoint, int count)
        {
            return Find(SelectAll(), searchText).Skip(startPoint).Take(count).ToList();
        }

        private static void ChangeEntityState(T item, EntityState state)
        {
            var context = ApplicationContext.Instance();
            context.Entry(item).State = state;
            context.SaveChanges();
        }
        
        private static bool IsSearched(T item, string searchText)
        {
            var searchWords = searchText.Split(' ');

            return searchWords.All(word => item.ToString().Contains(word));
        }
    }
}