
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
        var filePath = Path.Combine(FileSystem.AppDataDirectory, "aircraft.json");

        if (!File.Exists(filePath))
        {
            using var inputStream = await FileSystem.OpenAppPackageFileAsync("aircraft.json");
            using var outputStream = File.Create(filePath);
            await inputStream.CopyToAsync(outputStream);
        }
    }
}
