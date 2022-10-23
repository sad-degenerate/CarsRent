using CarsRent.LIB.Model;

namespace CarsRent.LIB.DataBase;

public static class HumanCommands
{
    private static IEnumerable<Human> SelectAllOwners()
    {
        var humans = BaseCommands<Human>.SelectAll().ToList();
        return humans.Where(human => IsOwner(human)).ToList();
    }
    
    private static IEnumerable<Human> SelectAllRenters()
    {
        var humans = BaseCommands<Human>.SelectAll().ToList();
        return humans.Where(human => IsOwner(human) == false).ToList();
    }

    public static IEnumerable<Human> SelectRentersGroup(int startPoint, int count)
    {
        return SelectAllRenters().Skip(startPoint).Take(count);
    }
    
    public static IEnumerable<Human> SelectOwnersGroup(int startPoint, int count)
    {
        return SelectAllOwners().Skip(startPoint).Take(count);
    }

    public static IEnumerable<Human> FindAndSelectOwners(string searchText, int startPoint, int count)
    {
        return BaseCommands<Human>.Find(SelectAllOwners(), searchText).Skip(startPoint).Take(count);
    }
        
    public static IEnumerable<Human> FindAndSelectRenters(string searchText, int startPoint, int count)
    {
        return BaseCommands<Human>.Find(SelectAllRenters(), searchText).Skip(startPoint).Take(count);
    }
    
    private static bool IsOwner(Human human)
    {
        return BaseCommands<Owner>.SelectAll().Any(owner => owner.HumanId == human.Id);
    }
}