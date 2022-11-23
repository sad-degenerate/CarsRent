using System.Windows.Controls;
using CarsRent.LIB.DataBase;
using CarsRent.LIB.Model;

namespace CarsRent.LIB.Controllers;

public abstract class BaseAddEntityController
{
    public abstract string AddEditEntity(UIElementCollection collection, Dictionary<string, string> valuesRelDict);

    protected virtual void SaveItemInDb<T>(T item) where T: class, IBaseModel
    {
        if (item.Id == 0)
        {
            BaseCommands<T>.Add(item);
        }    
        else
        {
            BaseCommands<T>.Modify(item);
        }
    }
}