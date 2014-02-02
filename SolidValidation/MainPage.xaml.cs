using System.Windows;
using System.Windows.Controls;
using SolidValidation.ViewModels;

namespace SolidValidation {
    public partial class MainPage : UserControl {
        public MainPage() {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e) {
            var vm = this.DataContext as MainViewModel;
            vm.SaveEmployee();
        }
    }
}
