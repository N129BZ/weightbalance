
namespace WeightBalance;

public partial class App : Application
{
    public App()
    {
        Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("Ngo9BigBOggjHTQxAR8/V1NBaF5cXmZCf1FpRmJGdld5fUVHYVZUTXxaS00DNHVRdkdnWXpfcnVWRmJZV0xwXEQ=");

        InitializeComponent();

        var filename = Path.Combine(FileSystem.Current.AppDataDirectory, "aircraft.json");

        if (!File.Exists(filename))
        {
            var json = Task.Run(() => GetAircraftJson()).Result;
            using var outstream = File.CreateText(filename);
            outstream.Write(json);
            outstream.Flush();
            outstream.Close();
        }
        
        MainPage = new NavigationPage(new MainPage()); 
    }

    private static async Task<string> GetAircraftJson()
    {
        using Stream inputStream = FileSystem.Current.OpenAppPackageFileAsync("AboutAssets.txt").Result;
        using StreamReader reader = new(inputStream);
        return await reader.ReadToEndAsync();
    }
}
