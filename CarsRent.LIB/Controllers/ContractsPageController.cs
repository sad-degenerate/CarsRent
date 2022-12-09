using System.Diagnostics;
using System.Windows;
using CarsRent.LIB.DataBase;
using CarsRent.LIB.Model;
using CarsRent.LIB.Settings;
using CarsRent.LIB.Word;

namespace CarsRent.LIB.Controllers;

public class ContractsPageController : BaseDataGridViewController
{
    public int CurrentPage;
    public readonly int PageSize;
    
    public ContractsPageController()
    {
        var settings = SettingsController<DisplaySettings>.GetSettings();
        PageSize = settings.TableOnePageElementsCount;
        CurrentPage = 1;
    }

    public ValueTask<List<Contract>> GetDataGridItems(string searchedText)
    {
        var contracts = base.GetDataGridItems<Contract>(searchedText, 
            base.GetSkipCount(CurrentPage, PageSize), PageSize).AsTask().Result;

        foreach (var contract in contracts)
        {
            var car = BaseCommands<Car>.SelectByIdAsync(contract.CarId).AsTask().Result;
            var renter = BaseCommands<Renter>.SelectByIdAsync(contract.RenterId).AsTask().Result;
            var human = BaseCommands<Human>.SelectByIdAsync(renter.HumanId).AsTask().Result;

            contract.Car = car;
            contract.Renter = renter;
            contract.Renter.Human = human;
        }

        return new ValueTask<List<Contract>>(contracts);
    }
    
    private string GetDocumentFolder(Contract contract)
    {
        var settings = SettingsController<TemplatesSettings>.GetSettings();

        return Path.Combine(settings.OutputFolder, 
            $"{contract.Car.Color} {contract.Car.Brand} {contract.Car.Model}");
    }

    public void OpenDocumentFolder(Contract contract)
    {
        string dir;

        try
        {
            dir = GetDocumentFolder(contract);
        }
        catch (Exception)
        {
            return;
        }
            

        if (Directory.Exists(dir) == false)
        {
            MessageBox.Show($"Не удалось найти директорию: {dir}");
            return;
        }
        
        Process.Start("explorer.exe", dir);
    }

    public void Print(Contract contract)
    {
        string? outputFolder;
        try
        {
            outputFolder = GetDocumentFolder(contract);
        }
        catch (Exception ex)
        {
            return;
        }
        
        if (Directory.Exists(outputFolder) == false)
        {
            MessageBox.Show($"Не удалось найти директорию: {outputFolder}");
            return;
        }

        var documentName = $"{contract.ConclusionDate:dd.MM.yyyy} {contract.Renter.Human.Surname} " +
                           $"{contract.Renter.Human.Name[0]}.{contract.Renter.Human.Patronymic[0]}.";
        var filesPath = Path.Combine(outputFolder, documentName);

        var settings = SettingsController<PrintSettings>.GetSettings();

        ContractPrinter.Print($"{filesPath} договор.docx", settings.CopiesCount, settings.DuplexPrint);
        ContractPrinter.Print($"{filesPath} акт.docx", settings.CopiesCount, settings.DuplexPrint);
        ContractPrinter.Print($"{filesPath} уведомление.docx", settings.CopiesCount, settings.DuplexPrint);
    }
}