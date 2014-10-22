using AnnotationsAnalizer.Model;

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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Data.Entity;

namespace AnnotationsAnalizer
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private AnnotationInfoContext _context = new AnnotationInfoContext();
        public MainWindow()
        {
            InitializeComponent();
        }

        ~MainWindow()
        {
            if (_context != null)
                _context.Dispose();
        }

        protected override void OnClosing(System.ComponentModel.CancelEventArgs e)
        {
            base.OnClosing(e);

            if (_context != null)
                _context.Dispose();
        }

        private void Open_Assembly_Click(object sender, RoutedEventArgs e)
        {
            var dlg = new Microsoft.Win32.OpenFileDialog();

            dlg.DefaultExt = ".exe";
            dlg.Filter = ".NET Assembly|*.exe;*.dll";

            var result = dlg.ShowDialog();

            if (result == true)
            {
                AssemblyFileNameTextBox.Text = dlg.FileName;
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Analyser analyser = new Analyser(_context);
            analyser.AnalizeAssembly(AssemblyFileNameTextBox.Text);
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            if (_context == null)
                return;

            _context.Annotations.Load();
            _context.Members.Load();

            System.Windows.Data.CollectionViewSource annotationInfoViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("annotationInfoViewSource")));
            annotationInfoViewSource.Source = _context.Annotations.Local;

            //annotationInfoDataGrid.ItemsSource = _context.Annotations.Local;
        }

        private void annotationInfoDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

    }
}
