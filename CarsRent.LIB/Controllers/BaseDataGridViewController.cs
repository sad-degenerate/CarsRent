using CarsRent.LIB.DataBase;
using CarsRent.LIB.Model;

namespace CarsRent.LIB.Controllers;

public abstract class BaseDataGridViewController
{
    public ValueTask<List<T>> GetDataGridItems<T>(string searchText, int startPoint, int count) where T : class, IBaseModel
    {
        return string.IsNullOrWhiteSpace(searchText)
            ? BaseCommands<T>.SelectGroupAsync(startPoint, count)
            : BaseCommands<T>.FindAndSelectAsync(searchText, startPoint, count);
    }

    protected static int GetSkipCount(int currentPage, int pageSize)
    {
        return currentPage == 1
            ? 0
            : currentPage * (pageSize - 1);
    }
}