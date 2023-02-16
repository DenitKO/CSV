using System.DirectoryServices;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace CSV
{
    public partial class Form1 : Form
    {
        static CancellationTokenSource cts = new CancellationTokenSource();
        CancellationToken ct = cts.Token;
        private const string V = "";
        public string first = V;
        string[] firstVariables = new string[0];
        char _separators = '\0';
        string filename = "";
        int parts = 0;
        int countOfLinesInFile = 0;
        public bool _stopOpeningFile = false;
        Dictionary<string, List <double>> variables = new Dictionary<string, List <double>> ();


        public Form1()
        {
            InitializeComponent();
            openFileDialog.Filter = "CSV files(*.csv)|*.csv|Text files(*.txt)|*.txt|All files(*.*)|*.*";
            //fOpenFile.btnCancelOpeningFile.Click += (s, e) => cts.Cancel();
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
            //PrintSeparator();
            lbChannels.Items.Clear();
            SplitIntoVariables();
            FOpenFile fOpenFile = new FOpenFile(this);
            fOpenFile.Show();
            fOpenFile.labelOpenFileDirection.Text = filename;
            //ReadFile(fOpenFile);
            ReadFile2(fOpenFile);
            returnVariables();
        }

        async void returnVariables()
        {
            await Task.Run(() =>
            {
                string abc = "Cosine_o-431_a27_p63";
                if (variables.ContainsKey(abc))
                {
                    foreach (var variable in variables.Keys)
                    {
                        foreach (var value in variables[variable])
                        {
                            lbStatistics.Items.Add(value.ToString());
                        }
                    }
                }
            });
        }

        bool stopOpeningFile()
        {
            return _stopOpeningFile;
        }

        async void ReadFile(FOpenFile fOpenFile)
        {
            _stopOpeningFile = false;
            parts = 0;
            bool firstVar = true;
            string variablesInLine = "";
            string[] someStr = new string[0];

            await Task.Run(() =>
            {
                using (StreamReader stream = new StreamReader(filename))
                {
                    StringBuilder stringBuilder = new StringBuilder();
                    stringBuilder.Append(stream.ReadToEnd());
                    stringBuilder.Remove(0, first.Length + 2); // deletes empty char after clipping the first row with variables
                    string[] line = stringBuilder.ToString().Split('\n');
                    countOfLinesInFile = line.Length;
                    fOpenFile.progBarOpening.Maximum = countOfLinesInFile;
                    foreach (string item in line)
                    {
                        // добавить переменные в коллекцию с учётом пропущенных элементов
                        // гуглить обработка пропущенных значений c#
                        // загуглить что такое LINQ

                        parts++;
                        fOpenFile.gbProgressBar.Text = $"Загружено: {parts * 100 / countOfLinesInFile:#.#} %";
                        fOpenFile.progBarOpening.Value = parts;
                        label3.Text = parts.ToString();
                        variablesInLine = item.ReplaceWhiteSpaces();
                        //lbStatistics.Items.Add(variablesInLine);
                        if (firstVar)
                        {
                            firstVar = false;
                            if (_separators == '\t')
                            {
                                someStr = variablesInLine.Replace('\t', ' ').Split(' ');
                            }
                            else
                            {
                                someStr = variablesInLine.Split(_separators);
                            }
                            for (int i = 0; i < someStr.Length; i++)
                            {
                                someStr[i] = someStr[i].Replace('.', ',');
                            }
                            for (int i = 0; i < firstVariables.Length; i++)
                            {
                                variables.Add(firstVariables[i], AddToDict(Convert.ToDouble(someStr[i])));
                            }
                        }
                        else
                        {
                            if (_separators == '\t')
                            {
                                someStr = variablesInLine.Replace('\t', ' ').Split(' ');
                            }
                            else
                            {
                                someStr = variablesInLine.Split(_separators);
                            }
                            for (int i = 0; i < someStr.Length; i++)
                            {
                                someStr[i] = someStr[i].Replace('.', ',');
                                if (Double.TryParse(someStr[i], out double someDouble))
                                {
                                    variables[firstVariables[i]].Add(someDouble);
                                }
                                else
                                {
                                    variables[firstVariables[i]].Add(double.NaN);
                                }
                            }
                        }
                        if (stopOpeningFile())
                            break;
                    }
                }
            });
        }

        void ReadFile2(FOpenFile fOpenFile)
        {
            _stopOpeningFile = false;
            parts = 0;
            bool firstVar = true;
            string variablesInLine = "";
            string[] someStr = new string[0];

            using (StreamReader stream = new StreamReader(filename))
            {
                StringBuilder stringBuilder = new StringBuilder();
                stringBuilder.Append(stream.ReadToEnd());
                stringBuilder.Remove(0, first.Length + 2); // deletes empty char after clipping the first row with variables
                string[] line = stringBuilder.ToString().Split('\n');
                countOfLinesInFile = line.Length;
                fOpenFile.progBarOpening.Maximum = countOfLinesInFile;
                foreach (string item in line)
                {
                    // добавить переменные в коллекцию с учётом пропущенных элементов
                    // гуглить обработка пропущенных значений c#
                    // загуглить что такое LINQ

                    parts++;
                    fOpenFile.gbProgressBar.Text = $"Загружено: {parts * 100 / countOfLinesInFile:#.#} %";
                    fOpenFile.progBarOpening.Value = parts;
                    label3.Text = parts.ToString();
                    variablesInLine = item.ReplaceWhiteSpaces();
                    //lbStatistics.Items.Add(variablesInLine);
                    if (firstVar)
                    {
                        firstVar = false;
                        someStr = variablesInLine.Split(_separators);
                        for (int i = 0; i < someStr.Length; i++)
                        {
                            someStr[i] = someStr[i].Replace('.', ',');
                        }
                        for (int i = 0; i < firstVariables.Length; i++)
                        {
                            variables.Add(firstVariables[i], AddToDict(Convert.ToDouble(someStr[i])));
                        }
                    }
                    else
                    {
                        someStr = variablesInLine.Split(_separators);
                        for (int i = 0; i < someStr.Length; i++)
                        {
                            someStr[i] = someStr[i].Replace('.', ',');
                            if (Double.TryParse(someStr[i], out double someDouble))
                            {
                                variables[firstVariables[i]].Add(someDouble);
                            }
                            else
                            {
                                variables[firstVariables[i]].Add(double.NaN);
                            }
                        }
                    }
                    if (stopOpeningFile())
                        break;
                }
            }
        }

        /// <summary>
        /// Method needed to add variables from a file in line to dictionary
        /// </summary>
        /// <param name="numbers"></param>
        List<double> AddToDict(double number)
        {
            List<double> numbers = new List<double>();
            numbers.Add(number);
            return numbers;
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
                MessageBox.Show("Separator = Tab", "Separator");
            }
            else if ((_separators == ',') || (_separators == ';'))
            {
                MessageBox.Show(String.Format(@"Separator = {0}", _separators), "Separator");
            }
            else MessageBox.Show("Separator = ???", "Separator");
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
            char[] whitespace = new char[] { ' ', '\t', '\r', '\n'};
            //char[] whitespace = new char[] { ' ', '\r', '\n' };
            return String.Join(" ", str.Split(whitespace, StringSplitOptions.RemoveEmptyEntries));
        }
    }

}