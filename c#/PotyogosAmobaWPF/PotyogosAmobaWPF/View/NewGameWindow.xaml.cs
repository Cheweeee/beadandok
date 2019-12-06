using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Diagnostics;

namespace PotyogosAmobaWPF.View {
    /// <summary>
    /// Interaction logic for NewGameWindow.xaml
    /// </summary>
    public partial class NewGameWindow : Window {
        public int size { get; set; }
        public NewGameWindow() {
            InitializeComponent();
        }

        private void Btn10_Click(object sender, RoutedEventArgs e) {
            size = 10;
        }

        private void Btn20_Click(object sender, RoutedEventArgs e) {
            size = 20;
        }

        private void Btn30_Click(object sender, RoutedEventArgs e) {
            size = 30;
        }

        private void OK_Click(object sender, RoutedEventArgs e) {
            Debug.WriteLine(size);
            this.DialogResult = true;
        }

        private void Cancel_Click(object sender, RoutedEventArgs e) {
            this.DialogResult = false;
        }
    }
}
