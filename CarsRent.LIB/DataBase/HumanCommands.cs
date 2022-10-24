using CarsRent.LIB.Model;

namespace CarsRent.LIB.DataBase;

public static class HumanCommands
{
    private static ValueTask<List<Human>> SelectAllOwnersAsync()
    {
        var list = BaseCommands<Human>.SelectAllAsync().AsTask().Result;
        return new ValueTask<List<Human>>(list.Where(human => IsOwner(human).AsTask().Result).ToList());
    }

    private static ValueTask<List<Human>> SelectAllRentersAsync()
    {
        var list = BaseCommands<Human>.SelectAllAsync().AsTask().Result;
        return new ValueTask<List<Human>>(list.Where(human => IsOwner(human).AsTask().Result == false).ToList());
    }

    public static ValueTask<List<Human>>  SelectRentersGroupAsync(int startPoint, int count)
    {
        var list = SelectAllRentersAsync().AsTask().Result;
        return new ValueTask<List<Human>>(list.Skip(startPoint).Take(count).ToList());
    }
    
    public static ValueTask<List<Human>>  SelectOwnersGroupAsync(int startPoint, int count)
    {
        var list = SelectAllOwnersAsync().AsTask().Result;
        return new ValueTask<List<Human>>(list.Skip(startPoint).Take(count).ToList());
    }

    public static ValueTask<List<Human>>  FindAndSelectOwnersAsync(string searchText, int startPoint, int count)
    {
        var list = SelectAllOwnersAsync().AsTask().Result;
        var find = BaseCommands<Human>.FindAsync(list, searchText).AsTask().Result;
        return new ValueTask<List<Human>>(find.Skip(startPoint).Take(count).ToList());
    }
        
    public static ValueTask<List<Human>> FindAndSelectRentersAsync(string searchText, int startPoint, int count)
    {
        var list = SelectAllRentersAsync().AsTask().Result;
        var find = BaseCommands<Human>.FindAsync(list, searchText).AsTask().Result;
        return new ValueTask<List<Human>>(find.Skip(startPoint).Take(count).ToList());
    }

    public static void DeleteOwnerAsync(Human human)
    {
        var owners = BaseCommands<Owner>.SelectAllAsync().AsTask().Result;

        foreach (var owner in owners.Where(owner => owner.HumanId == human.Id))
        {
            BaseCommands<Owner>.DeleteAsync(owner);
        }
        
        BaseCommands<Human>.DeleteAsync(human);
    }
    
    public static void DeleteRenterAsync(Human human)
    {
        var renters = BaseCommands<Renter>.SelectAllAsync().AsTask().Result;

        foreach (var renter in renters.Where(renter => renter.HumanId == human.Id))
        {
            BaseCommands<Renter>.DeleteAsync(renter);
        }
        
        BaseCommands<Human>.DeleteAsync(human);
    }

    public static ValueTask<Owner> GetOwnerFromHumanAsync(Human human)
    {
        var list = BaseCommands<Owner>.SelectAllAsync().AsTask().Result;
        return new ValueTask<Owner>(list.Where(owner => owner.HumanId == human.Id).FirstOrDefault());
    }
    
    private static ValueTask<bool> IsOwner(Human human)
    {
        var list = BaseCommands<Owner>.SelectAllAsync().AsTask().Result;
        return new ValueTask<bool>(list.Any(owner => owner.HumanId == human.Id));
    }
}