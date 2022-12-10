using CarsRent.LIB.DataBase;
using CarsRent.LIB.Model;
using CarsRent.LIB.Settings;

namespace CarsRent.LIB.Controllers;

public class RentersPageController : BaseDataGridViewController
{
    public int CurrentPage;
    public readonly int PageSize;
    
    public RentersPageController()
    {
        var settings = SettingsController<DisplaySettings>.GetSettings();
        PageSize = settings.TableOnePageElementsCount;
        CurrentPage = 1;
    }

    public override void DeleteEntity<T>(T item)
    {
        if (item is not Human human)
        {
            return;
        }

        var renters = BaseCommands<Renter>.SelectAll()
            .Where(renter => renter.HumanId == human.Id);

        foreach (var renter in renters)
        {
            base.DeleteEntity(renter);
        }
        
        base.DeleteEntity(human);
    }
    
    public int GetSkipCount()
    {
        return GetSkipCount(CurrentPage, PageSize);
    }
}