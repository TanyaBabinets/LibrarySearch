using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
//Использовать БД Library
// Использовать отсоединенный режим.

//создать приложение для поиска нужной книги по разным критериям:
//по названию
//по автору
//по категории

namespace LibrarySearch
{
    public partial class Form1 : Form
    {
      
        DbLibrary db = new DbLibrary();
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            dataGridView1.DataSource=null;
            if (textBox1.Text != string.Empty)
            {               
               
                dataGridView1.DataSource = db.SearchByAuthor(textBox1.Text);
                textBox1.Text = string.Empty;


            }
            else if (textBox2.Text != string.Empty)
            {
                dataGridView1.DataSource = db.SearchByName(textBox2.Text);
                textBox2.Text = string.Empty;


            }
            else
            { 
                dataGridView1.DataSource = db.SearchByCat(textBox3.Text);
                textBox3.Text = string.Empty;


            }
        }
    }
}
