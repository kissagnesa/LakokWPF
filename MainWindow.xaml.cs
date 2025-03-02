using LakokCLI;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Windows;

namespace LakokWPF
{
   

    public partial class MainWindow : Window
    {
        public List<Lakas> LakokLista { get; set; }
        private string filePath = "lakok.csv";

        public MainWindow()
        {
            InitializeComponent();
            LakokLista = new List<Lakas>();
            Betoltes();
            dgLakok.ItemsSource = LakokLista;
        }

        private void Betoltes()
        {
            if (File.Exists(filePath))
            {
                var sorok = File.ReadAllLines(filePath);
                foreach (var sor in sorok)
                {
                    var adatok = sor.Split(';');
                    LakokLista.Add(new Lakas
                    {
                        Cim = adatok[0],
                        LakokSzama = int.Parse(adatok[1]),
                        LakokNeve = adatok[2],
                        Terulet = int.Parse(adatok[3]),
                    });
                }
            }
        }

        private void btnBevitel_Click(object sender, RoutedEventArgs e)
        {
            var ujLakas = new Lakas
            {
                Cim = txtCim.Text,
                LakokSzama = int.Parse(txtLakokSzama.Text),
                LakokNeve = txtNev.Text,
                Terulet = int.Parse(txtTerulet.Text),
                Tartozas = 0
            };
            LakokLista.Add(ujLakas);
            dgLakok.Items.Refresh();
            Mentes();
        }

        private void btnKiegyenlites_Click(object sender, RoutedEventArgs e)
        {
            if (dgLakok.SelectedItem is Lakas kijelolt)
            {
                kijelolt.Tartozas = 0;
                dgLakok.Items.Refresh();
                Mentes();
                MessageBox.Show("Tartozás kiegyenlítve!");
            }
        }

        private void Mentes()
        {
            var sorok = LakokLista.Select(l => $"{l.Cim};{l.LakokSzama};{l.LakokNeve};{l.Terulet};{l.Tartozas}");
            File.WriteAllLines(filePath, sorok);
        }
    }
}