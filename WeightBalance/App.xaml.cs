
using System.IO;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

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
            using StreamWriter outstream = File.CreateText(filename);
            outstream.Write(json);
            outstream.Flush();
            outstream.Close();
        }
        
        MainPage = new NavigationPage(new MainPage()); 
    }

    private static async Task<string> GetAircraftJson()
    {
        using Stream inputStream = await FileSystem.Current.OpenAppPackageFileAsync("aircraft.json");
        using StreamReader reader = new StreamReader(inputStream);
        return await reader.ReadToEndAsync();
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

#if IOS
    [DllImport("__Internal", EntryPoint = "exit")]
    public static extern void exit(int exitCode);
#endif
}
