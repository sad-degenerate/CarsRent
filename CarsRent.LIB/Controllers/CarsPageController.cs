using CarsRent.LIB.DataBase;
using CarsRent.LIB.Model;
using CarsRent.LIB.Settings;

namespace CarsRent.LIB.Controllers;

public class CarsPageController : BaseDataGridViewController
{
    public readonly int PageSize;
    public int CurrentPage;
    
    public CarsPageController()
    {
        var settings = SettingsController<DisplaySettings>.GetSettings();
        
        PageSize = settings.TableOnePageElementsCount;
        CurrentPage = 0;
    }

    public static ValueTask<bool> DeleteCar(Car car)
    {
        BaseCommands<Car>.DeleteAsync(car);
        return new ValueTask<bool>(true);
    }

    public int GetSkipCount()
    {
        return GetSkipCount(CurrentPage, PageSize);
    }
}