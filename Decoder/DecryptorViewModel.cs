using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Decoder
{
    public class DecryptorViewModel : INotifyPropertyChanged
    {
        private List<Alphabet> _alphabets;
        public List<Alphabet> Alphabet
        {
            get { return _alphabets; }
            set
            {
                _alphabets = value;
                OnPropertyChanged();
            }
        }

        public DecryptorViewModel()
        {
            Alphabet = new List<Alphabet>();
            for (int i = 'А'; i <= 'Я'; i++)
                Alphabet.Add(new Alphabet((char)i, (char)i));
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }

    public class Alphabet : INotifyPropertyChanged
    {
        private char _decrypted;

        public char Encrypted { get; set; }
        public char Decrypted
        {
            get { return _decrypted; }
            set
            {
                _decrypted = value;
                OnPropertyChanged();
            }
        }

        public Alphabet() { }

        public Alphabet(char e, char d)
        {
            Encrypted = e;
            Decrypted = d;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
