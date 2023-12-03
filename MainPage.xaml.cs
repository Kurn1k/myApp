using System.Runtime.Intrinsics.Arm;
using System.Text;
using System.Security.Cryptography;
using K4os.Compression.LZ4;
using Force.Crc32;

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
                string hexResult = ConvertToHex(inputWord);
                string crc32Result = CalculateCRC32(inputWord);

                BinaryLabel.Text = $"Бинарный код: {binaryResult}";
                HexLabel.Text = $"Hex: {hexResult}";
                CRC32Label.Text = $"CRC32: {crc32Result}";

                // Копирование результатов в буфер обмена
                Clipboard.SetTextAsync($"{binaryResult}\n{hexResult}\nCRC32: {crc32Result}");

                SemanticScreenReader.Announce("Text converted and copied to clipboard.");
            }
            else
            {
                BinaryLabel.Text = HexLabel.Text = CRC32Label.Text = "Ошибка!";
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
        private string ConvertToHex(string word)
        {
            StringBuilder hexStringBuilder = new StringBuilder();

            foreach (char c in word)
            {
                hexStringBuilder.Append($"{((int)c):X} ");
            }

            return hexStringBuilder.ToString().Trim();
        }

        private string CalculateCRC32(string word)
        {
            var crc32 = Crc32Algorithm.Compute(Encoding.UTF8.GetBytes(word));

            // Преобразование CRC32 в строку
            return crc32.ToString("X8");
        }
    }
}
