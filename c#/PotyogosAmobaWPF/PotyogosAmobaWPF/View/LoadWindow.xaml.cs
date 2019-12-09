using PotyogosAmobaWPF.Persistence;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
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

namespace PotyogosAmobaWPF.View {
    /// <summary>
    /// Interaction logic for LoadWindow.xaml
    /// </summary>
    public partial class LoadWindow : Window {
        public string saveName { get;private set; }

        

        public ObservableCollection<string> loadList;


        public LoadWindow() {
            InitializeComponent();
        }
        public LoadWindow(List<string> db) {
            InitializeComponent();
            Loaded += loadComplete;
            loadList = new ObservableCollection<string>(db);
            Debug.WriteLine(loadList.Count);
        }

        private void loadComplete(object sender, RoutedEventArgs e) {
            loads.DataContext = loadList;
        }

        private void Button_Click(object sender, RoutedEventArgs e) {
            Debug.WriteLine(loads.SelectedItem.ToString());
            saveName = loads.SelectedItem.ToString();
            this.DialogResult = true;
        }
        
    }
}
