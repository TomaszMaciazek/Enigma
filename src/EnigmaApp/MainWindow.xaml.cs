using Machine.Interfaces;
using Machine.Machine_Classes;
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

namespace EnigmaApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly IEnigmaMachine _machine;

        private string _availableLetters;

        private char _awaitingForPair;

        private readonly ICollection<Tuple<char, char>> _pairs;
        public MainWindow()
        {
            InitializeComponent();
            _machine = new EnigmaMachine();
            _availableLetters = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
        }

        private void EncodeButton_Click(object sender, RoutedEventArgs e)
        {
            EncodedText.Text = _machine.EncodeString(NotEncodedText.Text);
        }

        private void DecrementFirstRotorNumber_Click(object sender, RoutedEventArgs e)
        {
            FirstRotorNumber.Text = GetDecrementedRotorNumber(FirstRotorNumber.Text);
        }

        private void IncrementFirstRotorNumber_Click(object sender, RoutedEventArgs e)
        {
            FirstRotorNumber.Text = GetIncrementedRotorNumber(FirstRotorNumber.Text);
        }

        private void IncrementFirstRotorPosition_Click(object sender, RoutedEventArgs e)
        {
            FirstRotorPosition.Text = GetIncrementedRotorPosition(FirstRotorPosition.Text[0]).ToString();
        }

        private void DecrementFirstRotorPosition_Click(object sender, RoutedEventArgs e)
        {
            FirstRotorPosition.Text = GetDecrementedRotorPosition(FirstRotorPosition.Text[0]).ToString();
        }

        private void DecrementSecondRotorNumber_Click(object sender, RoutedEventArgs e)
        {
            SecondRotorNumber.Text = GetDecrementedRotorNumber(SecondRotorNumber.Text);
        }

        private void IncrementSecondRotorNumber_Click(object sender, RoutedEventArgs e)
        {
            SecondRotorNumber.Text = GetIncrementedRotorNumber(SecondRotorNumber.Text);
        }

        private void IncrementSecondRotorPosition_Click(object sender, RoutedEventArgs e)
        {
            SecondRotorPosition.Text = GetIncrementedRotorPosition(SecondRotorPosition.Text[0]).ToString();
        }

        private void DecrementSecondRotorPosition_Click(object sender, RoutedEventArgs e)
        {
            SecondRotorPosition.Text = GetDecrementedRotorPosition(SecondRotorPosition.Text[0]).ToString();
        }

        private void DecrementThirdRotorNumber_Click(object sender, RoutedEventArgs e)
        {
            ThirdRotorNumber.Text = GetDecrementedRotorNumber(ThirdRotorNumber.Text);
        }

        private void IncrementThirdRotorNumber_Click(object sender, RoutedEventArgs e)
        {
            ThirdRotorNumber.Text = GetIncrementedRotorNumber(ThirdRotorNumber.Text);
        }

        private void DecrementThirdRotorPosition_Click(object sender, RoutedEventArgs e)
        {
            ThirdRotorPosition.Text = GetDecrementedRotorPosition(ThirdRotorPosition.Text[0]).ToString();
        }

        private void IncrementThirdRotorPosition_Click(object sender, RoutedEventArgs e)
        {
            ThirdRotorPosition.Text = GetIncrementedRotorPosition(ThirdRotorPosition.Text[0]).ToString();
        }

        private string GetIncrementedRotorNumber(string rotorNumber)
        {
            var result = string.Empty;
            switch (rotorNumber)
            {
                case "I":
                    result = "II";
                    break;
                case "II":
                    result = "III";
                    break;
                case "III":
                    result = "IV";
                    break;
                case "IV":
                    result = "V";
                    break;
                case "V":
                    result = "I";
                    break;
            }
            return result;
        }

        private string GetDecrementedRotorNumber(string rotorNumber)
        {
            var result = string.Empty;
            switch (rotorNumber)
            {
                case "I":
                    result = "V";
                    break;
                case "II":
                    result = "I";
                    break;
                case "III":
                    result = "II";
                    break;
                case "IV":
                    result = "III";
                    break;
                case "V":
                    result = "IV";
                    break;
            }
            return result;
        }

        private char GetIncrementedRotorPosition(char position)
        {
            return position == 'Z' ? 'A' : (char)(position + 1);
        }

        private char GetDecrementedRotorPosition(char position)
        {
            return position == 'A' ? 'Z' : (char)(position-1);
        }

        private void PlugboardButton_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            var letter = button.Content.ToString()[0];
            if (_availableLetters.Contains(letter))
            {
                if (_availableLetters.Length % 2 == 0)
                {
                    if (_availableLetters.Contains(letter))
                    {
                        _awaitingForPair = letter;
                    }
                }
                else
                {

                    var pair = new Tuple<char, char>(_awaitingForPair, letter);
                    _pairs.Add(pair);
                }
                int index = _availableLetters.IndexOf(letter);
                _availableLetters.Remove(index);
            }
            else
            {
                var pair = FindPairWithGivenLetter(letter);
                StringBuilder builder = new StringBuilder(_availableLetters + pair.Item1 + pair.Item2);
                _availableLetters = builder.ToString();
            }
        }

        private Tuple<char, char> FindPairWithGivenLetter(char letter)
            => _pairs.Where(x => x.Item1 == letter || x.Item2 == letter).SingleOrDefault();

    }
}
