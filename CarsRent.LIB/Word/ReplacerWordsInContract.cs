using CarsRent.LIB.Model;
using CarsRent.LIB.Settings;
using TemplateDocs.LIB;

namespace CarsRent.LIB.Word
{
    public class ReplacerWordsInContract
    {
        public void Replace(ContractDetails contract)
        {
            var landlordSettingsSerializator = new SettingsSerializator<LandlordSettings>();
            var landlordSettings = landlordSettingsSerializator.Deserialize();
            var templateSettingsSerializator = new SettingsSerializator<TemplatesSettings>();
            var templateSettings = templateSettingsSerializator.Deserialize();

            var outputFolder = Path.Combine(templateSettings.OutputFolder, $"{contract.Car.Color} {contract.Car.Brand} {contract.Car.Model}");

            var documentName = $"{contract.ConclusionDate:dd.MM.yyyy} {contract.Renter.Surname} {contract.Renter.Name[0]}.{contract.Renter.Patronymic[0]}.";

            var replaceWordsGenerator = new ReplaceWordsGenerator(landlordSettings, contract);

            var actReplacer = new DocumentReplacer(templateSettings.ActSample, outputFolder);
            actReplacer.ReplaceAsync(replaceWordsGenerator.GetWords(), $"{documentName} акт");
            var contractReplacer = new DocumentReplacer(templateSettings.ContractSample, outputFolder);
            contractReplacer.ReplaceAsync(replaceWordsGenerator.GetWords(), $"{documentName} договор");
            var notificationReplacer = new DocumentReplacer(templateSettings.NotificationSample, outputFolder);
            notificationReplacer.ReplaceAsync(replaceWordsGenerator.GetWords(), $"{documentName} уведомление");
        }
    }
}