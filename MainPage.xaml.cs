using System.Text;

namespace MyApp
{
    public partial class MainPage : ContentPage
    {
        int count = 0;

        public MainPage()
        {
            InitializeComponent();
        }

        private void OnCounterClicked(object sender, EventArgs e)
        {
            string inputWord = EntryField.Text; // Получаем введенное слово из Entry
            if (!string.IsNullOrWhiteSpace(inputWord))
            {
                string binaryResult = ConvertToBinary(inputWord);
                BinaryLabel.Text = $"Binary: {binaryResult}";
                SemanticScreenReader.Announce(BinaryLabel.Text);
            }
            else
            {
                BinaryLabel.Text = "Please enter a word";
            }
        }
        private string ConvertToBinary(string word)
        {
            StringBuilder binaryStringBuilder = new StringBuilder();

            foreach (char c in word)
            {
                binaryStringBuilder.Append(Convert.ToString(c, 2).PadLeft(8, '0'));
                binaryStringBuilder.Append(" ");
            }

            return binaryStringBuilder.ToString().Trim();
        }

    }
}
