using System.Windows.Controls;
using CarsRent.LIB.DataBase;
using CarsRent.LIB.Model;

namespace CarsRent.LIB.Controllers;

public abstract class BaseAddEntityController
{
    protected abstract Dictionary<string, string> CreateValuesRelationDict(IBaseModel item);
    public abstract ValueTask<string> AddEditEntityAsync(UIElementCollection collection);

    protected virtual void SaveItemInDbAsync<T>(T item) where T: class, IBaseModel
    {
        if (item.Id == 0)
        {
            BaseCommands<T>.AddAsync(item);
        }    
        else
        {
            BaseCommands<T>.ModifyAsync(item);
        }
    }

    private static string DeletePrefix(string fieldName)
    {
        if (fieldName.StartsWith("Tbx") || fieldName.StartsWith("Cbx") || fieldName.StartsWith("Lbx"))
        {
            fieldName = fieldName.Remove(0, 3);
        }

        var chars = fieldName.ToCharArray();
        chars[0] = char.ToLower(chars[0]);
        
        return chars.ToString() ?? string.Empty;
    }
    
    protected static void FillFields(ref UIElementCollection collection, Dictionary<string, string> valuesDict)
    {
        foreach (var element in collection)
        {
            switch (element)
            {
                case TextBox textBox:
                    textBox.Text = valuesDict[DeletePrefix(textBox.Name)];
                    break;
                
                case ComboBox comboBox when int.TryParse(valuesDict[DeletePrefix(comboBox.Name)], out var index):
                    comboBox.SelectedIndex = index;
                    break;
                
                default:
                    continue;
            }
        }
    }
    
    protected static IEnumerable<KeyValuePair<string, string>> CreateValuesRelationDict(UIElementCollection collection)
    {
        foreach (var item in collection)
        {
            string value;
            string name;
            
            switch (item)
            {
                case TextBox textBox:
                    name = textBox.Name;
                    value = textBox.Text;
                    break;
                
                case ComboBox comboBox:
                    name = comboBox.Name;
                    value = comboBox.SelectionBoxItemStringFormat;
                    break;
                
                case ListBox listBox:
                    name = listBox.Name;
                    if (listBox.SelectedItem is IBaseModel baseModel)
                    {
                        value = baseModel.Id.ToString();
                    }
                    else
                    {
                        value = string.Empty;
                    }
                    break;
                
                default:
                    continue;
            }

            yield return new KeyValuePair<string, string>(DeletePrefix(name), value);
        }
    }
}