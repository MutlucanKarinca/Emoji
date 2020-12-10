using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private List<Category> GetEmojiList()
        {
            JavaScriptSerializer tercuman =new JavaScriptSerializer();
            string jsonContent = File.ReadAllText(@"C:\Users\mutlu\source\repos\Kurs2\WindowsFormsApp1\smiley_content.json");

            return tercuman.Deserialize<List<Category>>(jsonContent);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            var list = GetEmojiList();
            DisplayEmojiList(list);
        }

        private void DisplayEmojiList(List<Category> list)
        {
            foreach (Category c in list)
            {
                Label label = new Label() {Text = c.category};
                label.AutoSize = false;
                label.Width = this.ClientSize.Width;
                label.Font=new Font(FontFamily.GenericMonospace, 20);
                label.TextAlign = ContentAlignment.MiddleCenter;
                label.Margin=new Padding(0,20,0,20);
                flowLayoutPanel1.SetFlowBreak(label,true);//********************
                flowLayoutPanel1.Controls.Add(label);

                DisplayItems(c);
            }
        }

        private void DisplayItems(Category c)
        {
            foreach (Item item in c.items)
            {
                Button buton=new Button();
                buton.Text = item.art + Environment.NewLine + item.name;
                buton.Font=new Font(FontFamily.GenericMonospace, 14);
                buton.Padding=new Padding(5);
                buton.Width = flowLayoutPanel1.ClientSize.Width / 2 - 10;
                buton.Height = 80;
                buton.Click += ButonClick;

                flowLayoutPanel1.Controls.Add(buton);


            }
           
        }

        private void ButonClick(object sender, EventArgs e)
        {
            Button clickedBtn = (Button) sender;
            string[] infos = clickedBtn.Text.Split('\n');
            Clipboard.SetText(infos[0]);
            MessageBox.Show(clickedBtn.Text + " has coppied to Clipboard");
        }
    }
}
