namespace CarsRent.LIB.Settings
{
    [Serializable]
    public class TemplatesSettings : SettingsBase
    {
        public string Surname { get; set; }
        public string Name { get; set; }
        public string Patronymic { get; set; }
        public string BirthDate { get; set; }
        public string PassportNumber { get; set; }
        public string IssuingOrganization { get; set; }
        public string IssuingDate { get; set; }
        public string RegistrationPlace { get; set; }

        public string ActSample { get; set; }
        public string ContractSample { get; set; }
        public string NotificationSample { get; set; }
        public string OutputFolder { get; set; }

        // Нужен для сериализации/десиреализации
        public TemplatesSettings() { }

        public TemplatesSettings(string surname, string name, string patronymic, string birthDate, string passportNumber,
                            string issuingOrganization, string issuingDate, string registrationPlace, string actSample,
                            string contractSample, string notificationSample, string outputFolder)
        {
            // TODO: Тут проверки

            Surname = surname;
            Name = name;
            Patronymic = patronymic;
            BirthDate = birthDate;
            PassportNumber = passportNumber;
            IssuingOrganization = issuingOrganization;
            IssuingDate = issuingDate;
            RegistrationPlace = registrationPlace;
            ActSample = actSample;
            ContractSample = contractSample;
            NotificationSample = notificationSample;
            OutputFolder = outputFolder;
        }
    }
}