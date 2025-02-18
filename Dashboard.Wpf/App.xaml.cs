using Dashboard.Wpf.Views;
using System.Windows;

namespace Dashboard.Wpf;

/// <summary>
/// Interaction logic for App.xaml
/// </summary>
public partial class App : Application
{

    protected void ApplicationStart(object sender, StartupEventArgs args)
    {
        //var loginView = new LoginView();
        //loginView.Show();

        //loginView.IsVisibleChanged += (sender, args) =>
        //{
        //    if (!loginView.IsVisible && loginView.IsLoaded)
        //    {
        //        var mainWindow = new MainWindow();
        //        mainWindow.Show();
        //        loginView.Close();
        //    }
        //};

        var loginView = new LoginView();
        loginView.Show();
        loginView.IsVisibleChanged += (s, ev) =>
        {
            if (loginView.IsVisible == false && loginView.IsLoaded)
            {
                var mainView = new MainWindow();
                mainView.Show();
                loginView.Close();
            }
        };
    }


}
