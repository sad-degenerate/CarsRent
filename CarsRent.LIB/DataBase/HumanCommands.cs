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

    private static ValueTask<bool> IsOwner(Human human)
    {
        var list = BaseCommands<Owner>.SelectAllAsync().AsTask().Result;
        return new ValueTask<bool>(list.Any(owner => owner.HumanId == human.Id));
    }
}