using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.Extensions.DependencyInjection;

using PaperBooks.Data;
using PaperBooks.Services;
using PaperBooks.ViewModels;

using System.Configuration;
using System.Data;
using System.Windows;

namespace PaperBooks
{
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            var services = new ServiceCollection();

            // DbContext через фабрику 
            services.AddDbContext<LibraryDbContext>(options =>
                options.UseSqlServer
                    (LibraryDbContextFactory.ConnectionString),
                contextLifetime: ServiceLifetime.Singleton,
                optionsLifetime: ServiceLifetime.Singleton);

            // Репозитории
            services.AddSingleton<IReadersRepository, ReadersRepositoryEF>();
            services.AddSingleton<IBooksRepository, BooksRepositoryEF>();
            services.AddSingleton<IReservationsRepository, ReservationsRepositoryEF>();
            services.AddSingleton<ILoansRepository, LoansRepositoryEF>();

            // Сервисы
            services.AddSingleton<IReadersService, ReadersService>();
            services.AddSingleton<IBooksService, BooksService>();
            services.AddSingleton<ILoansService, LoansService>();
            services.AddSingleton<IReservationsService, ReservationsService>();

            // ViewModel
            services.AddSingleton<MainVM>();

            var serviceProvider = services.BuildServiceProvider();

            var mainVm = serviceProvider.GetRequiredService<MainVM>();
            var mainWindow = new MainWindow { DataContext = mainVm };
            mainWindow.Show();
        }
    }

}
