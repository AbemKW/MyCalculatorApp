using System.Threading.Tasks;

namespace MyCalculatorApp.ViewModels;
public partial class MainViewModel : BaseViewModel
{
    [ObservableProperty]
    private string input;
    [ObservableProperty]
    private string sign;

    string Num1;
    string Num2;


    [RelayCommand]
    public void AppendDigit(string number)
    {
        Input = Input + number;
    }

    [RelayCommand]
    public async Task SetOperator(string op)
    {
        if (op == "=")
        {
            Num2 = Input;
            Clear();
            try
            {

            Input = $"{await Equate()}";
            }
            catch (Exception )
            {

                Input = "Error";
            }

            return;
        }
        Sign = op;
        Num1 = Input;
        Clear();
        //AppendDigit();
        Num2 = Input;
    }

    [RelayCommand]
    public async Task<float> Equate()
    {
        float num1 = float.Parse(Num1);
        float num2 = float.Parse(Num2);
        float result = 0;
        switch (Sign)
        {
            case "*":
                result = num1 * num2;
                break;
            case "/":
                result = num1 / num2;
                break;
            case "+":
                result = num1 + num2;
                break;
            case "-":
                result = num1 - num2;
                break;
            default:
                await Shell.Current.DisplayAlert("Syntax Error", "Enter again", "OK");
                break;
        }
        return result;
    }
    [RelayCommand]
    public void Clear()
    {
        Input = string.Empty;
    }
}
