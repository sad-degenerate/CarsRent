using System.Diagnostics;
using System.Windows;
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

    public int GetSkipCount()
    {
        return base.GetSkipCount(CurrentPage, PageSize);
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