using Client.Presenters;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Client
{
    internal static class Program
    {
        public static string JwtToken { get; set; } = string.Empty;
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            ApplicationConfiguration.Initialize();

            var host = Host.CreateDefaultBuilder()
                .ConfigureServices((context, services) =>
                {
                    services.AddHttpClient<ApiService>(client =>
                    {
                        client.BaseAddress = new Uri("https://localhost:7188/");
                    });

                    services.AddTransient<LoginForm>();
                    services.AddTransient<LoginPresenter>();

                    services.AddTransient<MainForm>();
                    services.AddTransient<StudentPresenter>();
                })
                .Build();

            using (var loginForm = host.Services.GetRequiredService<LoginForm>())
            {
                if (loginForm.ShowDialog() != DialogResult.OK)
                {
                    return;
                }
            }

            var mainForm = host.Services.GetRequiredService<MainForm>();
            Application.Run(mainForm);
        }
    }
}