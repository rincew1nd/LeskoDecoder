using System.Windows;
using System.Windows.Documents;

namespace Decoder
{
    public partial class MainWindow
    {
        private readonly DecryptorViewModel _decryptorViewModel;

        public MainWindow()
        {
            InitializeComponent();

            _decryptorViewModel = new DecryptorViewModel();
            DataContext = _decryptorViewModel;
        }

        private void Decrypt_Click(object sender, RoutedEventArgs e)
        {
            var encryptedText = new TextRange(EncryptedText.Document.ContentStart, EncryptedText.Document.ContentEnd).Text;

            foreach (var alphabet in _decryptorViewModel.Alphabet)
                encryptedText = encryptedText.Replace(alphabet.Encrypted, alphabet.Decrypted);

            DecryptedText.Document.Blocks.Clear();
            DecryptedText.Document.Blocks.Add(new Paragraph(new Run(encryptedText)));
        }

        private void OpenFile_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
