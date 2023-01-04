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

    public int GetSkipCount()
    {
        return GetSkipCount(CurrentPage, PageSize);
    }

    public void UpdateCarsStatus()
    {
        var cars = BaseCommands<Car>.SelectAll();
        foreach (var car in cars)
        {
            var contract = BaseCommands<Contract>.SelectAll()
                .FirstOrDefault(contract => contract.CarId == car.Id);

            if (contract == null)
            {
                continue;
            }
            
            if (car.CarStatus != Status.UnderRepair)
            {
                car.CarStatus = Status.Ready;
            }

            if (contract.EndDate > DateTime.Today)
            {
                car.CarStatus = Status.OnLease;
            }

            BaseCommands<Car>.Modify(car);
        }
    }
}