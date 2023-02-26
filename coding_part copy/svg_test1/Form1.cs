using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace svg_test1
{
    public partial class Form1 : Form
    {

        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e) //открытие окна с прочим
        {
            Form1 frm1 = new Form1();
            Form2 frm2 = new Form2();
            frm2.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            textBox1.Text = ""; //обнуление статуса файла

            string userName1 = Environment.UserName; //получение имя пользователя

            string[] path = new string[6]; //создание массива путей к нужным файлам

            for (int p = 0; p < 6; p++)  //задаём значение этому массиву
            {
                path[p] = $"C:\\Users\\{userName1}\\Downloads\\Layer{p + 1}.gcode";
            }

            string[][] lines = new string[6][]; //создание массива массивов строк

            for (int l = 0; l < 6; l++)
            {
                try
                {
                    lines[l] = File.ReadAllLines(path[l]); //записываем строки в массив массивов
                }
                catch
                {
                    break;
                }
            }

            if (Directory.Exists("C:\\Чертёжник") == false) //проверка наличии папки. если нет, то создает
            {
                Directory.CreateDirectory("C:\\Чертёжник");
            }

            int i = 1;
            string fileName = $"C:\\Чертёжник\\Draft_{i}"; //задаём путь к файлу

            try
            {
                while (File.Exists(fileName) == true)  //проверка наличия файла с данным названием. перебираются названия до тех пор, пока не найдется свободного
                {
                    i++;
                    fileName = $"C:\\Чертёжник\\Draft_{i}";
                }

                using (FileStream f = File.Create(fileName))  //создание файла, в который запишем данные из скачанных файлов 
                {
                    byte[] info = new UTF8Encoding(true).GetBytes("");
                    f.Write(info, 0, info.Length);
                }
            }
            catch (Exception Ex)  //проверка на ошибку
            {
                Console.WriteLine(Ex.ToString());
            }
            using (StreamWriter file = new StreamWriter(fileName, append: true)) //запись содержимого ранних файлов в файл, который мы создали
            {
                for (int a = 0; a < 6; a++)
                {
                    try
                    {
                        for (int b = 0; b < lines[a].Length; b++)
                        {
                            file.WriteLine(lines[a][b]);
                        }
                    }
                    catch
                    {
                        break;
                    }
                }
            }
            for (int n = 0; n < 6; n++)
            {
                try
                {
                    File.Delete(path[n]); //удаляем ранее использованные ненужные файлы
                }
                catch
                {
                    break;
                }
            }
            File.Delete($"C:\\Чертёжник\\Draft_{i}.gcode");
            File.Move($"C:\\Чертёжник\\Draft_{i}", $"C:\\Чертёжник\\Draft_{i}.gcode");
            if (File.Exists($"C:\\Чертёжник\\Draft_{i}.gcode") == true)
            {
                textBox1.Text = "Файл создан";
            } //статус файла
        }

        private void button3_Click(object sender, EventArgs e) //открытие папки с чертежами
        {
            Process.Start("C:\\Чертёжник");
        }

        private void button4_Click(object sender, EventArgs e) //открытие папки Загрузки
        {
            string userName2 = Environment.UserName;
            Process.Start($"C:\\Users\\{userName2}\\Downloads");
        }

        private void button5_Click(object sender, EventArgs e) //открытие окна с инструкцией
        {
            Form1 frm1 = new Form1();
            Form3 frm3 = new Form3();
            frm3.Show();
        }
        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button6_Click(object sender, EventArgs e)
        {
            if (webView != null && webView.CoreWebView2 != null)
            {
                webView.CoreWebView2.Navigate("https://sameer.github.io/svg2gcode/#close");
            }
        }
    }
}