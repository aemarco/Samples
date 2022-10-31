namespace WpfCommunityToolkit.View;

public partial class MainWindow
{
    public MainWindow(
        MainWindowViewModel vm)
    {
        DataContext = vm;
        InitializeComponent();
    }
}
