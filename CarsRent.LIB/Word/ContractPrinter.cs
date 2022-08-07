using TemplateDocs.LIB;

namespace CarsRent.LIB.Word
{
    public static class ContractPrinter
    {
        public static void Print(string filePath, int copies)
        {
            var printer = new DocumentPrinter(filePath);
            printer.Print(copies);
        }
    }
}