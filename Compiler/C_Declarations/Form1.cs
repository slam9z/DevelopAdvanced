using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace AddressingModes
{
    public partial class Form1 : Form
    {
        Language lg = new Language();
        EAGrammar ldg = null;
        public Form1()
        {
            InitializeComponent();

            ldg = new EAGrammar(lg);
            ldg.Load();

        }

        private void btnParse_Click(object sender, EventArgs e)
        {
            bool parsing = true;
            EAGrammar._lineno = 1;
            LLParser lr = new LLParser(txtLine.Text+"$", lg);

            EAGrammar.ErrMsg = "";
            while (parsing)
                parsing = lr.Parse();

            txtTuples.Text= EAGrammar._type;

            if (EAGrammar.ErrMsg != "")
                MessageBox.Show(EAGrammar.ErrMsg);

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
