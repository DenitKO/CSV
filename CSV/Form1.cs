using System.Text;

namespace CSV
{
    public partial class Form1 : Form
    {
        private const string V = "";
        public string first = V;
        string[] firstVariables = new string[0];
        char _separators = '\0';
        string filename = "";
        int parts = 0;
        int maxPercent = 0;
        public Form1()
        {
            InitializeComponent();
            openFileDialog.Filter = "CSV files(*.csv)|*.csv|Text files(*.txt)|*.txt|All files(*.*)|*.*";
        }
        
        private void btnOpen_Click(object sender, EventArgs e)
        {
            if (openFileDialog.ShowDialog() == DialogResult.Cancel)
                return;
            infoLabel.Text = "Opening CSV file...";
            // очитска листбокса
            lbStatistics.Items.Clear();
            CheckFirstLine();
            ChechSeparator();
            // PrintSeparator();
            lbChannels.Items.Clear();
            SplitIntoVariables();
            FOpenFile fOpenFile = new FOpenFile(this);
            fOpenFile.Show();
            fOpenFile.labelOpenFileDirection.Text = filename;
            ReadFile3(fOpenFile);
        }
        /// <summary>
        /// Method needed to add variables from a file in line to dictionary
        /// </summary>
        /// <param name="numbers"></param>
        void AddToDict(params int[] numbers)
        {
            int result = 0;
            foreach (var n in numbers)
            {
                result += n;
            }
            Console.WriteLine(result);
        }

        async void ReadFile()
        {
            await Task.Run(async () =>
            {
                using (FileStream fstream = File.OpenRead(filename))
                {
                    byte[] buffer = new byte[fstream.Length];
                    await fstream.ReadAsync(buffer, 0, buffer.Length);
                    string textFromFile = Encoding.Default.GetString(buffer);
                    string[] item = textFromFile.Split(_separators);
                    lbStatistics.Items.AddRange(item);
                }  
            });
        }

        async void ReadFile2()
        {
            string filename = openFileDialog.FileName;
            await Task.Run(() =>
            {
                string[] readFile = File.ReadAllLines(filename);
                string item = readFile[0];
                lbStatistics.Items.Add(item);
                first = item;
            });
        }

        async void ReadFile3(FOpenFile fOpenFile)
        {
            parts = 0;
            await Task.Run(() =>
            {
                while (OpeningFile()){
                    using (StreamReader stream = new StreamReader(filename))
                    {
                        StringBuilder stringBuilder = new StringBuilder();
                        stringBuilder.Append(stream.ReadToEnd());
                        stringBuilder.Remove(0, first.Length + 2); // deletes empty char after clipping the first row with variables
                        string[] line = stringBuilder.ToString().Split('\n');
                        maxPercent = line.Length;
                        fOpenFile.progBarOpening.Maximum = maxPercent;
                        foreach (string item in line)
                        {
                            // добавить переменные в коллекцию с учётом пропущенных элементов
                            // гуглить обработка пропущенных значений c#
                            // загуглить что такое LINQ

                            parts++;
                            fOpenFile.gbProgressBar.Text = $"Загружено: {(parts * 100 / maxPercent).ToString("#.#")} %";
                            fOpenFile.progBarOpening.Value = parts;
                            label3.Text = parts.ToString();
                            lbStatistics.Items.Add(item.ReplaceWhiteSpaces());
                        }
                    }
                }
            });
        }

        public bool OpeningFile(bool working = true)
        {
            if (working == true)
                return true;
            else if (working == false);
                return false;
        }

        void CheckFirstLine()
        {
            filename = openFileDialog.FileName;
            string[] readFile = File.ReadAllLines(filename); //ИСПРАВИТЬ на считывание только первой строки
            first = readFile[0];
        }

        void SplitIntoVariables()
        {
            firstVariables = first.Split(_separators);
            lbChannels.Items.AddRange(firstVariables);
        }

        void ChechSeparator()
        {
            char separator = '\0';
            while (separator == '\0')
            {
                for (int i = 0; i < first.Length; i++)
                {
                    if (first[i] == ',')
                    {
                        separator = ',';
                    }
                    else if (first[i] == ';')
                    {
                        separator = ';';
                    }
                    else if (first[i] == '\t')
                    {
                        separator = '\t';
                    }
                }
            }
            _separators = separator;
        }

        void PrintSeparator()
        {
            if (_separators == '\t')
            {
                MessageBox.Show("Separator = Tab");
            }
            else if ((_separators == ',') || (_separators == ';'))
            {
                MessageBox.Show(String.Format(@"Separator = {0}", _separators));
            }
            else MessageBox.Show("Separator = ???");
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            // close the form
            this.Close();
            // deactivate buttons
            //btnClose.Enabled = false;
        }
    }
    public static class Extensions
    {
        public static string ReplaceWhiteSpaces(this string str)
        {
            char[] whitespace = new char[] { ' ', '\r', '\n' };
            return String.Join(" ", str.Split(whitespace, StringSplitOptions.RemoveEmptyEntries));
        }
    }

}