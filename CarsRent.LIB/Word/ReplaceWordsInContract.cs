using CarsRent.LIB.Model;
using CarsRent.LIB.Settings;
using TemplateDocs.LIB;

namespace CarsRent.LIB.Word
{
    public class ReplaceWordsInContract
    {
        public void Replace(ContractDetails contract)
        {
            var documentName = $"{contract.ConclusionDate} {contract.Car.Color} {contract.Car.Brand} {contract.Car.Model}" +
                $" {contract.Renter.Surname} {contract.Renter.Name[0]}.{contract.Renter.Patronymic[0]}.";
            var settingsSerializator = new SettingsSerializator<TemplatesSettings>();
            var settings = settingsSerializator.Deserialize();

            var replaceWordsGenerator = new ReplaceWordsGenerator(settings, contract);
            var documentReplacer = new DocumentReplacer(settings.ContractSample, settings.OutputFolder);
            documentReplacer.Replace(replaceWordsGenerator.GetWords(), documentName);
        }
    }
}