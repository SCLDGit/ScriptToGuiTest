using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using ScriptToGuiTest.ViewModels;

namespace ScriptToGuiTest.Views
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            #if DEBUG
            this.AttachDevTools();
            #endif

            DataContext = new MainWindowViewModel();
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}