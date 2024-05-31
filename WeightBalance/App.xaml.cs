
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
            Task.Run(() => CopyFileToAppDataDirectory("aircraft.json")).Wait();
        }
        
        MainPage = new NavigationPage(new MainPage()); 
    }

    public static async Task CopyFileToAppDataDirectory(string filename)
    {
        using Stream inputStream = await FileSystem.Current.OpenAppPackageFileAsync(filename);
        string targetFile = Path.Combine(FileSystem.Current.AppDataDirectory, filename);
        using FileStream outputStream = File.Create(targetFile);
        await inputStream.CopyToAsync(outputStream);
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
