using System.Windows;
using System.Windows.Controls;
using CarsRent.LIB.Settings;

namespace CarsRent.WPF.Pages.Settings;

public partial class PrintSettingsPage : Page
{
    public PrintSettingsPage()
    {
        InitializeComponent();
        Load();
    }

    private void Load()
    {
        var settings = SettingsController<PrintSettings>.GetSettings();
        TbxCopies.Text = settings.CopiesCount.ToString();
        CbxDuplexPrint.IsChecked = settings.DuplexPrint;
    }

    private void Save()
    {
        if (int.TryParse(TbxCopies.Text, out var copies) == false)
        {
            LblError.Content = "Не удалось преобразовать в число.";
            LblDone.Content = string.Empty;
            return;
        }

        var settings = new PrintSettings()
        {
            CopiesCount = copies,
            DuplexPrint = CbxDuplexPrint.IsChecked ?? false
        };

        var error = SettingsController<PrintSettings>.SaveSettings(settings);
        
        if (string.IsNullOrWhiteSpace(error))
        {
            LblDone.Content = "Настрйки успешно сохранены.";
            LblError.Content = string.Empty;
        }

        LblDone.Content = string.Empty;
        LblError.Content = error;
    }

    private void btnSave_Click(object sender, RoutedEventArgs e)
    {
        Save();
    }
}