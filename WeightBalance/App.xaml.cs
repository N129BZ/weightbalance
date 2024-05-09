namespace WeightBalance
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            Task.Run(() => CopyResourceFile()).Wait();
            
            MainPage = new AppShell();
        }

        private async void CopyResourceFile()
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
