using CarsRent.LIB.Model;
using Microsoft.EntityFrameworkCore;

namespace CarsRent.LIB.DataBase
{
    public static class BaseCommands<T> where T : class, IBaseModel
    {
        public static void AddAsync(T item)
        {
            ChangeEntityStateAsync(item, EntityState.Added);
        }

        public static void ModifyAsync(T item)
        {
            ChangeEntityStateAsync(item, EntityState.Modified);
        }

        public static void DeleteAsync(T item)
        {
            ChangeEntityStateAsync(item, EntityState.Deleted);
        }

        public static ValueTask<List<T>> SelectAllAsync()
        {
            return new ValueTask<List<T>>(new ApplicationContext().Set<T>().ToListAsync());
        }

        public static ValueTask<List<T>> SelectGroupAsync(int startPoint, int count)
        {
            var list = SelectAllAsync().AsTask().Result;
            return new ValueTask<List<T>>(list.Skip(startPoint).Take(count).ToList());
        }
        
        public static ValueTask<T?> SelectByIdAsync(int? id)
        {
            var list = SelectAllAsync().AsTask().Result;
            return new ValueTask<T?>(list.FirstOrDefault(x => x.Id == id));
        }
        
        public static ValueTask<List<T>> FindAsync(IEnumerable<T> items, string searchText)
        {
            return new ValueTask<List<T>>(items.Where(item => IsSearched(item, searchText)).ToList());
        }
        
        public static ValueTask<List<T>> FindAndSelectAsync(string searchText, int startPoint, int count)
        {
            var list = SelectAllAsync().AsTask().Result;
            var find = FindAsync(list, searchText).AsTask().Result;
            return new ValueTask<List<T>>(find.Skip(startPoint).Take(count).ToList());
        }

        private static async void ChangeEntityStateAsync(T item, EntityState state)
        {
            var context = new ApplicationContext();
            context.Entry(item).State = state;
            await context.SaveChangesAsync();
        }
        
        private static bool IsSearched(T item, string searchText)
        {
            var searchWords = searchText.Split(' ');

            return searchWords.All(word => item.ToString().Contains(word));
        }
    }
}