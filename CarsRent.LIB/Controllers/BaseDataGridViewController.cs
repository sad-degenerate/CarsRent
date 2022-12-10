using CarsRent.LIB.DataBase;
using CarsRent.LIB.Model;

namespace CarsRent.LIB.Controllers;

public abstract class BaseDataGridViewController
{
    public ValueTask<List<T>> GetDataGridItemsAsync<T>(string searchText, int startPoint, int count) where T : class, IBaseModel
    {
        return new ValueTask<List<T>>(BaseCommands<T>.SelectGroup(startPoint, count, searchText).ToList());
    }

    protected int GetSkipCount(int currentPage, int pageSize)
    {
        return currentPage == 1
            ? 0
            : currentPage * (pageSize - 1);
    }

    public virtual void DeleteEntity<T>(T item) where T : class, IBaseModel
    {
        BaseCommands<T>.Delete(item);
    }
}