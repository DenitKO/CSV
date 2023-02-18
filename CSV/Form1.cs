using System.DirectoryServices;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace CSV
{
    public partial class Form1 : Form
    {
        private const string V = "";
        public string firstLineInFile = V;
        string[] firstVariables = Array.Empty<string>();
        char _separators = '\0';
        string filename = "";
        int counterOfReadedLines = 0;
        int countOfLinesInFile = 0;
        int counterOfNaN = 0;
        public bool _stopOpeningFile = false;
        bool firstVar = true;
        Dictionary<string, List <double>> variables = new();


        public Form1()
        {
            InitializeComponent();
            openFileDialog.Filter = "CSV files(*.csv)|*.csv|Text files(*.txt)|*.txt|All files(*.*)|*.*";
        }
        
        private void BtnOpen_Click(object sender, EventArgs e)
        {
            if (openFileDialog.ShowDialog() == DialogResult.Cancel)
                return;
            infoLabel.Text = "Opening CSV file...";
            // очитска листбокса
            lbStatistics.Items.Clear();
            CheckFirstLine3();
            ChechSeparator();
            //PrintSeparator();
            lbChannels.Items.Clear();
            SplitIntoVariables();
            FOpenFile fOpenFile = new(this);
            fOpenFile.Show();
            fOpenFile.labelOpenFileDirection.Text = filename;
            ReadFileAsync(fOpenFile);
            //ReadFile(fOpenFile);
            ReturnVariables();
        }


        async void ReturnVariables()
        {
            await Task.Run(() =>
            {
                foreach (var variable in variables.Keys)
                {
                    foreach (var value in variables[variable])
                    {
                        lbStatistics.Items.Add(value.ToString());
                    }
                }
            });
        }

        bool StopOpeningFile()
        {
            return _stopOpeningFile;
        }

        async void ReadFileAsync(FOpenFile fOpenFile)
        {
            string[] line = Array.Empty<string>();
            _stopOpeningFile = false;
            counterOfReadedLines = 0;
            firstVar = true;
            counterOfNaN = 0;
            variables.Clear();

            await Task.Run(() =>
            {
                using (StreamReader stream = new(filename))
                {
                    StringBuilder stringBuilder = new();
                    stringBuilder.Append(stream.ReadToEnd());
                    stringBuilder.Remove(0, firstLineInFile.Length + 2); // deletes empty char after clipping the first row with variables
                    line = stringBuilder.ToString().Trim().Split('\n');
                    countOfLinesInFile = line.Length;
                    fOpenFile.progBarOpening.Maximum = countOfLinesInFile;
                    foreach (string item in line)
                    {
                        addingToDictionary(item, fOpenFile);
                        if (StopOpeningFile())
                            break;
                    }
                }
            });
        }

        void ReadFile(FOpenFile fOpenFile)
        {
            string[] line = Array.Empty<string>();
            _stopOpeningFile = false;
            counterOfReadedLines = 0;
            firstVar = true;
            counterOfNaN = 0;
            variables.Clear();

            using (StreamReader stream = new(filename))
            {
                StringBuilder stringBuilder = new();
                stringBuilder.Append(stream.ReadToEnd());
                stringBuilder.Remove(0, firstLineInFile.Length + 2); // deletes empty char after clipping the first row with variables
                line = stringBuilder.ToString().Trim().Split('\n');
                countOfLinesInFile = line.Length;
                fOpenFile.progBarOpening.Maximum = countOfLinesInFile;
                foreach (string item in line)
                {
                    addingToDictionary(item, fOpenFile);
                    if (StopOpeningFile())
                        break;
                }
            }
        }

        void addingToDictionary(string item,FOpenFile fOpenFile)
        { 
            string variablesInLine = "";
            string[] someStr = Array.Empty<string>();

            counterOfReadedLines++;
            fOpenFile.gbProgressBar.Text = $"Download: {counterOfReadedLines * 100 / countOfLinesInFile:#.#} %";
            fOpenFile.progBarOpening.Value = counterOfReadedLines;
            label3.Text = counterOfReadedLines.ToString();
            //if (_separators != '\t') variablesInLine = item.ReplaceWhiteSpaces();
            //else variablesInLine = item;
            variablesInLine = item;
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
                    if (Double.TryParse(someStr[i], out double someDouble))
                    {
                        variables.Add(firstVariables[i], AddToDict(Convert.ToDouble(someDouble)));
                    }
                    else
                    {
                        variables.Add(firstVariables[i], AddToDict(double.NaN));
                        counterOfNaN++;
                    }
                    
                }
            }
            else
            {
                someStr = variablesInLine.Split(_separators);
                for (int i = 0; i < firstVariables.Length; i++)
                {
                    if (someStr.Length < firstVariables.Length)
                    {
                        while (i < someStr.Length)
                        {
                            someStr[i] = someStr[i].Replace('.', ',');
                            if (Double.TryParse(someStr[i], out double someDouble))
                            {
                                variables[firstVariables[i]].Add(someDouble);
                            }
                            else
                            {
                                variables[firstVariables[i]].Add(double.NaN);
                                counterOfNaN++;
                            }
                            i++;
                        }
                        variables[firstVariables[i]].Add(double.NaN);
                        counterOfNaN++;
                    }
                    else
                    {
                        while (i < firstVariables.Length)
                        {
                            someStr[i] = someStr[i].Replace('.', ',');
                            if (Double.TryParse(someStr[i], out double someDouble))
                            {
                                variables[firstVariables[i]].Add(someDouble);
                            }
                            else
                            {
                                variables[firstVariables[i]].Add(double.NaN);
                                counterOfNaN++;
                            }
                            i++;
                        }
                    }
                }
            }
        }

        static List<double> AddToDict(double number)
        {
            List<double> numbers = new()
            {
                number
            };
            return numbers;
        }    

        void CheckFirstLine() //650 ms
        {
            filename = openFileDialog.FileName;
            string[] readFile = File.ReadAllLines(filename);
            firstLineInFile = readFile[0];
        }

        void CheckFirstLine2() //13 ms
        {
            filename = openFileDialog.FileName;
            using (StreamReader reader = new StreamReader(filename))
            {
                firstLineInFile = reader.ReadLine() ?? "";
            }
        }

        void CheckFirstLine3() //8 ms
        {
            filename = openFileDialog.FileName;
            var readFile = File.ReadLines(filename);
            firstLineInFile = readFile.First();
        }

        void SplitIntoVariables()
        {
            firstVariables = firstLineInFile.Split(_separators);
            lbChannels.Items.AddRange(firstVariables);
        }

        void ChechSeparator()
        {
            char separator = '\0';
            while (separator == '\0')
            {
                for (int i = 0; i < firstLineInFile.Length; i++)
                {
                    if (firstLineInFile[i] == ',')
                    {
                        separator = ',';
                    }
                    else if (firstLineInFile[i] == ';')
                    {
                        separator = ';';
                    }
                    else if (firstLineInFile[i] == '\t')
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

        private void BtnClose_Click(object sender, EventArgs e)
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