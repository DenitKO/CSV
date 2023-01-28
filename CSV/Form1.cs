using System.Windows.Forms;
using System.IO;
using System.Text;
using System.Runtime.ExceptionServices;

namespace CSV
{
    public partial class Form1 : Form
    {
        char _separators = '\0';
        public string first = "";
        public Form1()
        {
            InitializeComponent();
            openFileDialog.Filter = "Text files(*.txt)|*.txt|All files(*.*)|*.*";
        }
        
        private void btnOpen_Click(object sender, EventArgs e)
        {
            infoLabel.Text = "Opening CSV file...";
            // ������� ���������
            lbStatistics.Items.Clear();
            CheckFirstLine();
            ChechSeparator();
            PrintSeparator();
            lbChannels.Items.Clear();
            SplitIntoVariables();
        }

        async void ReadFile()
        {
            if (openFileDialog.ShowDialog() == DialogResult.Cancel)
                return;
            await Task.Run(async () =>
            {
                // �������� ��������� ����
                string filename = openFileDialog.FileName;
                // ������ ���� � ������
                // string[] fileText = File.ReadAllLines(filename);


                //using (var bufferedFileStream =
                //       new BufferedStream(File.OpenRead(filename), 1024 * 1024)) // ����� � ��������
                //{
                //    bufferedFileStream.Read();
                //}

                // ������ �� �����
                using (FileStream fstream = File.OpenRead(filename))
                {
                    // �������� ������ ��� ���������� ������ �� �����
                    byte[] buffer = new byte[fstream.Length];
                    // ��������� ������
                    await fstream.ReadAsync(buffer, 0, buffer.Length);
                    // ���������� ����� � ������
                    string textFromFile = Encoding.Default.GetString(buffer);
                    string[] item = textFromFile.Split('\n');
                    lbStatistics.Items.AddRange(item);
                }  
                // ���������� ������, ���������
                // foreach (string line in textFromFile)
                // {
                //     lbStatistics.Items.Add(line);
                // }
                // ��� ��������: lbStatistics.Items.AddRange(fileText);
            });
        }

        async void ReadFile2()
        {
            if (openFileDialog.ShowDialog() == DialogResult.Cancel)
                return;
            string filename = openFileDialog.FileName;
            await Task.Run(() =>
            {
                string[] readFile = File.ReadAllLines(filename);
                string item = readFile[0];
                lbStatistics.Items.Add(item);
                first = item;
            });
        }

        void CheckFirstLine()
        {
            if (openFileDialog.ShowDialog() == DialogResult.Cancel)
                return;
            string filename = openFileDialog.FileName;
            string[] readFile = File.ReadAllLines(filename);
            string item = readFile[0];
            lbStatistics.Items.Add(item);
            first = item;
        }

        void SplitIntoVariables()
        {
            lbChannels.Items.AddRange(first.Split(_separators));
        }

        void ChechSeparator()
        {
            char separator = '\0';
            foreach (string item in lbStatistics.Items)
            {
                int i = 0;
                while (separator == '\0')
                {
                    if (item[i] == ',')
                    {
                        separator = ',';
                    }
                    else if (item[i] == ';')
                    {
                        separator = ';';
                    }
                    else if (item[i] == '\t')
                    {
                        separator = '\t';
                    }
                   i++;
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
            // �������� �����
            this.Close();
            // ����������� ������
            //btnClose.Enabled = false;
        }
    }
    
}