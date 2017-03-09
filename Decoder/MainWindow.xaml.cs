using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Web.Script.Serialization;
using System.Windows;
using System.Windows.Documents;
using Microsoft.Win32;
using System.Collections.Generic;

namespace Decoder
{
    public partial class MainWindow
    {
        private readonly DecryptorViewModel _decryptorViewModel;

        public MainWindow()
        {
            InitializeComponent();

            _decryptorViewModel = new DecryptorViewModel();
            this.DataContext = _decryptorViewModel;
        }

        private void Decrypt_Click(object sender, RoutedEventArgs e)
        {
            var decryptedText = new StringBuilder();
            var encryptedText = new TextRange(EncryptedText.Document.ContentStart, EncryptedText.Document.ContentEnd).Text;

            foreach (var cha in encryptedText)
            {
                var replace = _decryptorViewModel.Alphabet.FirstOrDefault(c => c.Encrypted == cha);
                decryptedText.Append(replace != null ?
                    (replace.Decrypted == replace.Encrypted) ?
                        char.ToUpper(replace.Decrypted) : char.ToLower(replace.Decrypted)
                    : char.ToUpper(cha));
            }

            DecryptedText.Document.Blocks.Clear();
            DecryptedText.Document.Blocks.Add(new Paragraph(new Run(decryptedText.ToString())));
        }

        private void OpenFile_Click(object sender, RoutedEventArgs e)
        {
            FileDialog fileDialog = new OpenFileDialog()
            {
                Filter = "Text file|*.txt",
                Title = "Open encrypted *.txt file"
            };
            
            if (fileDialog.ShowDialog().Value)
            {
                var text = File.ReadAllText(fileDialog.FileName);
            
                EncryptedText.Document.Blocks.Clear();
                EncryptedText.Document.Blocks.Add(new Paragraph(new Run(text)));
            }
        }

        private void SaveAlphabet_Click(object sender, RoutedEventArgs e)
        {
            var json = new JavaScriptSerializer().Serialize(_decryptorViewModel.Alphabet);
            File.WriteAllText("alphabet.txt", json);
        }

        private void LoadAlphabet_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var json = File.ReadAllText("alphabet.txt");
                if (!string.IsNullOrEmpty(json))
                    _decryptorViewModel.Alphabet =
                        new JavaScriptSerializer().Deserialize<List<Alphabet>>(json);
            }
            catch (FileNotFoundException exception)
            {
                MessageBox.Show("Файл словаря не найден!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
