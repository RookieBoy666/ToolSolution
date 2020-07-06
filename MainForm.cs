using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Windows.Forms;

namespace ToolSolution
{
    public partial class MainForm : Form
    {
        AutoSizeFormClass asc = new AutoSizeFormClass();
        public MainForm()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string aa = oldString.Text.ToString().Trim();
            string bb = "";
            string cc = "";
            if (string.IsNullOrEmpty(aa))
            {
                MessageBox.Show("原串不能为空！");
                return;
            }

            if (string.IsNullOrEmpty(textBox1.Text.Trim().ToString()))
            {
                MessageBox.Show("加密串不能为空！");
                return;
            }
            if (textBox1.Text.Length != 8)
            {
                MessageBox.Show("加密串为8位！");
                return;
            }

            string newStr = Encode(textBox1.Text, oldString.Text.ToString());
            newString.Text = newStr;
        }



        private void MainForm_Load(object sender, EventArgs e)
        {
            asc.controllInitializeSize(this);
            textBox1.Text = "zjp1202!";
        }

        private void MainForm_SizeChanged(object sender, EventArgs e)
        {
            asc.controlAutoSize(this);
        }
        //加密解密
        public string Encode(string KEY, string encryptString)
        {
            try
            {
                //string KEY = "zjp1202!";
                byte[] _vector = { 0x12, 0x34, 0x56, 0x78, 0x90, 0xAB, 0xCD, 0xEF };

                var rgbKey = Encoding.UTF8.GetBytes(KEY.Substring(0, 8));
                var des = new DESCryptoServiceProvider();

                var inputByteArray = Encoding.UTF8.GetBytes(encryptString);
                var ms = new MemoryStream();
                var cs = new CryptoStream(ms, des.CreateEncryptor(rgbKey, _vector), CryptoStreamMode.Write);
                cs.Write(inputByteArray, 0, inputByteArray.Length);
                cs.FlushFinalBlock();
                return Convert.ToBase64String(ms.ToArray());
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public string Decode(string KEY, string decryptString)
        {
            try
            {
                //string KEY = "zjp1202!";
                byte[] _vector = { 0x12, 0x34, 0x56, 0x78, 0x90, 0xAB, 0xCD, 0xEF };
                var provider = new DESCryptoServiceProvider();
                var rgbKey = Encoding.UTF8.GetBytes(KEY.Substring(0, 8));

                var inputByteArray = Convert.FromBase64String(decryptString);

                var ms = new MemoryStream();
                var cs = new CryptoStream(ms, provider.CreateDecryptor(rgbKey, _vector), CryptoStreamMode.Write);
                cs.Write(inputByteArray, 0, inputByteArray.Length);
                cs.FlushFinalBlock();

                var encoding = new UTF8Encoding();

                return encoding.GetString(ms.ToArray());
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string aa = oldString.Text.ToString().Trim();
            string bb = "";
            string cc = "";
            if (string.IsNullOrEmpty(aa))
            {
                MessageBox.Show("原串不能为空！");
                return;
            }

            if (string.IsNullOrEmpty(textBox1.Text.Trim().ToString()))
            {
                MessageBox.Show("加密串不能为空！");
                return;
            }
            if (textBox1.Text.Length != 8)
            {
                MessageBox.Show("加密串为8位！");
                return;
            }

            string newStr = Decode(textBox1.Text, oldString.Text.ToString());
            newString.Text = newStr;
        }
    }
}
