using MoveArc.View;
using MoveArc.ViewModel;
using System.Windows;

namespace MoveArc
{
    /// <summary>
    /// Логика взаимодействия для App.xaml
    /// </summary>
    public partial class App : Application
    {
        public App()
        {
            MainView view = new MainView();
            view.DataContext = new MainViewModel();
            view.Show();
        }
    }
}
