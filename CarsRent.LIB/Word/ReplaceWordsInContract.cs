using CarsRent.LIB.Model;
using CarsRent.LIB.Settings;
using TemplateDocs.LIB;

namespace CarsRent.LIB.Word
{
    public class ReplaceWordsInContract
    {
        public void Replace(ContractDetails contract)
        {
            var settingsSerializator = new SettingsSerializator<TemplatesSettings>();
            var settings = settingsSerializator.Deserialize();
            var outputFolder = Path.Combine(settings.OutputFolder, $"{contract.Car.Color} {contract.Car.Brand} {contract.Car.Model}");

            var documentName = $"{contract.ConclusionDate} {contract.Renter.Surname} {contract.Renter.Name[0]}.{contract.Renter.Patronymic[0]}.";

            var replaceWordsGenerator = new ReplaceWordsGenerator(settings, contract);

            var documentReplacer = new DocumentReplacer(settings.ContractSample, outputFolder);
            documentReplacer.Replace(replaceWordsGenerator.GetWords(), $"{documentName} договор");
            documentReplacer = new DocumentReplacer(settings.ActSample, outputFolder);
            documentReplacer.Replace(replaceWordsGenerator.GetWords(), $"{documentName} акт");
            documentReplacer = new DocumentReplacer(settings.NotificationSample, outputFolder);
            documentReplacer.Replace(replaceWordsGenerator.GetWords(), $"{documentName} уведомление");
        }
    }
}