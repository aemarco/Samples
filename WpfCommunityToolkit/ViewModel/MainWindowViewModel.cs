namespace WpfCommunityToolkit.ViewModel;

public partial class MainWindowViewModel : BaseViewModel,
    IRecipient<FullNameChanged>
{

    private readonly IMessenger _messenger;
    public MainWindowViewModel(
        IMessenger messenger,
        ILogger<MainWindowViewModel> logger)
        : base(logger)
    {
        _messenger = messenger;
        _messenger.RegisterAll(this);
    }
    public void Receive(FullNameChanged message)
    {
        Logger.LogInformation("Clicked with name {name}", message.FullName);
    }


    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(FullName))]
    [NotifyCanExecuteChangedFor(nameof(ClickCommand))]
    private string? _firstName;

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(FullName))]
    [NotifyCanExecuteChangedFor(nameof(ClickCommand))]
    private string? _lastName;

    public string FullName => $"{FirstName} {LastName}";


    [RelayCommand(CanExecute = nameof(CanClick))]
    private void Click()
    {
        _messenger.Send(new FullNameChanged(FullName));
    }
    private bool CanClick() =>
        !string.IsNullOrWhiteSpace(FirstName) &&
        !string.IsNullOrWhiteSpace(LastName);



    [RelayCommand(IncludeCancelCommand = true)]
    private async Task Click2(string o, CancellationToken token)
    {
        try
        {
            Logger.LogInformation("Start waiting...");
            await Task.Delay(10000, token);
            Logger.LogInformation("Was patient...");
        }
        catch (OperationCanceledException)
        {
            Logger.LogWarning("Aborted");
        }
    }




}
