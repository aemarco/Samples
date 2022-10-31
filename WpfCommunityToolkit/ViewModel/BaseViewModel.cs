namespace WpfCommunityToolkit.ViewModel;

public abstract class BaseViewModel : ObservableObject
{
    protected readonly ILogger Logger;
    protected BaseViewModel(
        ILogger logger)
    {
        Logger = logger;
    }

    protected override void OnPropertyChanged(PropertyChangedEventArgs e)
    {
        base.OnPropertyChanged(e);

        if (e.PropertyName is null)
            return;


        var pi = GetType().GetProperty(e.PropertyName)!;
        var value = pi.GetValue(this);
        Logger.LogDebug(
            "Property {property} changed to {value}",
            e.PropertyName,
            value);
    }
}