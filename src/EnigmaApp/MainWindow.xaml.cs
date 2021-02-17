using Machine.Args;
using Machine.Interfaces;
using Machine.Machine_Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace EnigmaApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly IEnigmaMachine _machine;

        private string _availableLetters;

        private Button _awaitingForPair;

        private readonly IDictionary<int, Tuple<Button, Button>> _pairs;

        private readonly IDictionary<int, string> _colors;

        private int _colorPosition;

        public MainWindow()
        {
            InitializeComponent();
            _machine = new EnigmaMachine();
            _availableLetters = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            _pairs = new Dictionary<int, Tuple<Button, Button>>();
            _colors = new Dictionary<int, string>() {
                { 0, "#555555" },
                { 1, "#f72338" },
                { 2, "#22f03a" },
                { 3, "#13e8e8" },
                { 4, "#eaff61" },
                { 5, "#b14df0" },
                { 6, "#f7b543" },
                { 7, "#2e990b" },
                { 8, "#7a0006" },
                { 9, "#090269" },
                { 10, "#808a5a" },
                { 11, "#411059" },
                { 12, "#73470a" },
                { 13, "#ff29db" }
            };
            _colorPosition = 1;
        }

        private void EncodeButton_Click(object sender, RoutedEventArgs e)
        {
            if (_awaitingForPair != null)
            {
                MessageBox.Show("One letter in plugboard is not connected to another");
            }
            else
            {
                var firstRotorNumber = GetRotorNumber(FirstRotorNumber.Text);
                var firstRotorPosition = Convert.ToByte(FirstRotorPosition.Text[0] - 65);
                var secondRotorNumber = GetRotorNumber(SecondRotorNumber.Text);
                var secondRotorPosition = Convert.ToByte(SecondRotorPosition.Text[0] - 65);
                var thirdRotorNumber = GetRotorNumber(ThirdRotorNumber.Text);
                var thirdRotorPosition = Convert.ToByte(ThirdRotorPosition.Text[0] - 65);
                var lettersToSwap = new List<Tuple<char, char>>();
                foreach (var pair in _pairs)
                {
                    lettersToSwap.Add(new Tuple<char, char>(pair.Value.Item1.Content.ToString()[0], pair.Value.Item2.Content.ToString()[0]));
                }
                var args = new MachineArgs
                {
                    RotorOneArgs = new RotorArgs
                    {
                        Number = firstRotorNumber,
                        StartPosition = firstRotorPosition
                    },
                    RotorTwoArgs = new RotorArgs
                    {
                        Number = secondRotorNumber,
                        StartPosition = secondRotorPosition
                    },
                    RotorThreeArgs = new RotorArgs
                    {
                        Number = thirdRotorNumber,
                        StartPosition = thirdRotorPosition
                    },
                    LettersToSwap = lettersToSwap
                };
                _machine.ConfigureMachine(args);
                EncodedText.Text = _machine.EncodeString(NotEncodedText.Text.ToUpper());
            }
        }

        #region ArrowButtonsClickEventMethods

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

        #endregion

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
            return position == 'A' ? 'Z' : (char)(position - 1);
        }

        private void PlugboardButton_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            var letter = button.Content.ToString()[0];
            if (_availableLetters.Contains(letter))
            {
                button.Background = (SolidColorBrush)(new BrushConverter().ConvertFrom(_colors[_colorPosition]));
                if (_availableLetters.Length % 2 == 0)
                {
                    _awaitingForPair = button;
                }
                else
                {
                    var pair = new Tuple<Button, Button>(_awaitingForPair, button);
                    _pairs.Add(_colorPosition, pair);
                    _awaitingForPair = null;
                    _colorPosition = _pairs.Count + 1;
                }
                int index = _availableLetters.IndexOf(letter);
                _availableLetters = _availableLetters.Remove(index, 1);
            }
            else
            {
                var pair = FindPairWithGivenButton(button);
                if (pair.Value == null)
                {
                    _awaitingForPair = null;
                    button.Background = (SolidColorBrush)(new BrushConverter().ConvertFrom(_colors[0]));
                }
                else
                {
                    pair.Value.Item1.Background = pair.Value.Item2.Background = (SolidColorBrush)(new BrushConverter().ConvertFrom(_colors[0]));
                    _colorPosition = pair.Key;
                    StringBuilder builder =
                    new StringBuilder(_availableLetters + pair.Value.Item1.Content.ToString() + pair.Value.Item2.Content.ToString());
                    _availableLetters = builder.ToString();
                    _pairs.Remove(pair.Key);
                }
            }
        }

        private KeyValuePair<int, Tuple<Button, Button>> FindPairWithGivenButton(Button button)
            => _pairs.Where(x => x.Value.Item1 == button || x.Value.Item2 == button).SingleOrDefault();


        private RotorNumber GetRotorNumber(string number)
        {
            RotorNumber result = RotorNumber.One;
            switch (number)
            {
                case "I":
                    result = RotorNumber.One;
                    break;
                case "II":
                    result = RotorNumber.Two;
                    break;
                case "III":
                    result = RotorNumber.Three;
                    break;
                case "IV":
                    result = RotorNumber.Four;
                    break;
                case "V":
                    result = RotorNumber.Five;
                    break;
            }
            return result;
        }

        private void ResetButton_Click(object sender, RoutedEventArgs e)
        {
            NotEncodedText.Text = String.Empty;
            EncodedText.Text = String.Empty;
            FirstRotorNumber.Text = "I";
            SecondRotorNumber.Text = "II";
            ThirdRotorNumber.Text = "III";
            FirstRotorPosition.Text = "A";
            SecondRotorPosition.Text = "A";
            ThirdRotorPosition.Text = "A";
            _availableLetters = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            if (_awaitingForPair != null)
            {
                _awaitingForPair.Background = (SolidColorBrush)(new BrushConverter().ConvertFrom(_colors[0]));
                _awaitingForPair = null;
            }
            foreach (var pair in _pairs)
            {
                pair.Value.Item1.Background = pair.Value.Item2.Background = (SolidColorBrush)(new BrushConverter().ConvertFrom(_colors[0]));
            }
            _pairs.Clear();
            _colorPosition = 1;
        }

        private void NotEncodedText_PreviewTextInput(object sender, System.Windows.Input.TextCompositionEventArgs e)
        {
            
            e.Handled = !Regex.IsMatch(e.Text, @"^[a-zA-Z0-9 \n\r]+$");
            if (e.Handled)
            {
                MessageBox.Show("Not allowed character");
            }
        }
    }
}
