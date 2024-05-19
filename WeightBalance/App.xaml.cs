
namespace WeightBalance;

public partial class App : Application
{
    public App()
    {
        Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("Ngo9BigBOggjHTQxAR8/V1NBaF5cXmZCf1FpRmJGdld5fUVHYVZUTXxaS00DNHVRdkdnWXpfcnVWRmJZV0xwXEQ=");

        InitializeComponent();

        Task.Run(() => CheckForJsonDataFile()).Wait();
       
        MainPage = new NavigationPage(new MainPage()); 
    }

    private static async void CheckForJsonDataFile()
    {
        var filePath = Path.Combine(FileSystem.Current.AppDataDirectory, "aircraft.json");

        if (!File.Exists(filePath))
        {
            using var inputStream = await FileSystem.OpenAppPackageFileAsync("aircraft.json");
            using Stream outputStream = File.OpenWrite(filePath);
            inputStream.CopyTo(outputStream);
        }
    }
}
