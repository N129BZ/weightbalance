
namespace WeightBalance;

public partial class App : Application
{
    public App()
    {
        Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("Ngo9BigBOggjHTQxAR8/V1NBaF5cXmZCf1FpRmJGdld5fUVHYVZUTXxaS00DNHVRdkdnWXpfcnVWRmJZV0xwXEQ=");

        InitializeComponent();

        MainPage = new NavigationPage(new MainPage()); 
    }
}

public static class Globals
{
    private static bool isFirstRun = true;
    public static bool IsFirstRun
    {
        get { return isFirstRun; }
        set { isFirstRun = value; }
    }
}
