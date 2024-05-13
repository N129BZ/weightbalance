
namespace WeightBalance
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            //Register Syncfusion license
            Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("Ngo9BigBOggjHTQxAR8/V1NBaF5cXmZCf1FpRmJGdld5fUVHYVZUTXxaS00DNHVRdkdnWXpfcnVWRmJZV0xwXEQ=");

            Task.Run(() => CopyResourceFiles()).Wait();
            
            MainPage = new AppShell();
        }

        private async void CopyResourceFiles()
        {
            var filepath = Path.Combine(FileSystem.AppDataDirectory, "Aircraft.json");
           
            if (!File.Exists(filepath))
            {
                using var inputStream = await FileSystem.OpenAppPackageFileAsync("Aircraft.json");
                using var outputStream = File.Create(filepath);
                await inputStream.CopyToAsync(outputStream);
            }
        }
    }
}
