using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TinyScanner
{
    public partial class Form1 : Form
    {
        Scanner Scan = new Scanner();
        public Form1()
        {
            InitializeComponent();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            string Code = textBox1.Text.ToLower();
            Scan.StartScanning(Code);
            PrintTokens();
            PrintErrors();
        }
        void PrintTokens()
        {
            for (int i = 0; i < Scan.Tokens.Count; i++)
            {
                dataGridView1.Rows.Add(Scan.Tokens.ElementAt(i).lex, Scan.Tokens.ElementAt(i).token_type);
            }
        }
        void PrintErrors()
        {
            textBox2.Text = "";
            for (int i = 0; i < Scan.Errors.Count; i++)
            {
                textBox2.Text += Scan.Errors[i];
            }
        }

        private void Clear_Click(object sender, EventArgs e)
        {
            textBox1.Text = "";
            textBox2.Text = "";
            dataGridView1.Rows.Clear();
            Scan = new Scanner();
        }
    }
}
