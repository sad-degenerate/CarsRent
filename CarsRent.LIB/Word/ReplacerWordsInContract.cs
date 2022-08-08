using CarsRent.LIB.Model;
using CarsRent.LIB.Settings;
using TemplateDocs.LIB;

namespace CarsRent.LIB.Word
{
    public class ReplacerWordsInContract
    {
        public void Replace(ContractDetails contract)
        {
            var settingsSerializator = new SettingsSerializator<TemplatesSettings>();
            var settings = settingsSerializator.Deserialize();
            var outputFolder = Path.Combine(settings.OutputFolder, $"{contract.Car.Color} {contract.Car.Brand} {contract.Car.Model}");

            var documentName = $"{contract.ConclusionDate} {contract.Renter.Surname} {contract.Renter.Name[0]}.{contract.Renter.Patronymic[0]}.";

            var replaceWordsGenerator = new ReplaceWordsGenerator(settings, contract);

            var actReplacer = new DocumentReplacer(settings.ActSample, outputFolder);
            actReplacer.ReplaceAsync(replaceWordsGenerator.GetWords(), $"{documentName} договор");
            var contractReplacer = new DocumentReplacer(settings.ContractSample, outputFolder);
            contractReplacer.ReplaceAsync(replaceWordsGenerator.GetWords(), $"{documentName} акт");
            var notificationReplacer = new DocumentReplacer(settings.NotificationSample, outputFolder);
            notificationReplacer.ReplaceAsync(replaceWordsGenerator.GetWords(), $"{documentName} уведомление");
        }
    }
}