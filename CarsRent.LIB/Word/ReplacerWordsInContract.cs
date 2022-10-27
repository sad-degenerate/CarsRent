using CarsRent.LIB.Model;
using CarsRent.LIB.Settings;
using TemplateDocs.LIB;

namespace CarsRent.LIB.Word
{
    public class ReplacerWordsInContract
    {
        public void Replace(Contract contract)
        {
            var templatesSettings = SettingsController<TemplatesSettings>.GetSettings();

            var outputFolder = Path.Combine(templatesSettings.OutputFolder, 
                $"{contract.Car.Color} {contract.Car.Brand} {contract.Car.Model}");

            var documentName = $"{contract.ConclusionDate:dd.MM.yyyy} {contract.Renter.Human.Surname} " +
                               $"{contract.Renter.Human.Name[0]}.{contract.Renter.Human.Patronymic[0]}.";

            var replaceWordsGenerator = new ReplaceWordsGenerator(contract);

            var actReplacer = new DocumentReplacer(templatesSettings.ActSample, outputFolder);
            actReplacer.ReplaceAsync(replaceWordsGenerator.GetWords(), $"{documentName} акт");
            var contractReplacer = new DocumentReplacer(templatesSettings.ContractSample, outputFolder);
            contractReplacer.ReplaceAsync(replaceWordsGenerator.GetWords(), $"{documentName} договор");
            var notificationReplacer = new DocumentReplacer(templatesSettings.NotificationSample, outputFolder);
            notificationReplacer.ReplaceAsync(replaceWordsGenerator.GetWords(), $"{documentName} уведомление");
        }
    }
}