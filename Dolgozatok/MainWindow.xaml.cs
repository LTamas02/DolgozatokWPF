using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Windows;

namespace WpfDolgozat
{
    public partial class MainWindow : Window
    {
        private ObservableCollection<Dolgozat> dolgozatok =
            new ObservableCollection<Dolgozat>();

        private string filePath;

        public MainWindow()
        {
            InitializeComponent();

            filePath = FindProjectFile("dolgozatok.txt");

            if (filePath == null)
            {
                MessageBox.Show("Nem található a dolgozatok.txt a projektben!");
                return;
            }

            MessageBox.Show("Használt fájl:\n" + filePath);

            Beolvas();
            dgAdatok.ItemsSource = dolgozatok;
        }

        private string FindProjectFile(string fileName)
        {
            string dir = AppDomain.CurrentDomain.BaseDirectory;

            // Legfeljebb 10 szinttel megyünk feljebb → biztosan elérjük a projekt gyökerét
            for (int i = 0; i < 10; i++)
            {
                string possibleFile = Path.Combine(dir, fileName);
                if (File.Exists(possibleFile))
                    return possibleFile;

                dir = Directory.GetParent(dir).FullName;
            }

            return null;
        }

    }
}