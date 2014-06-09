using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.IO;
using System.Threading;

namespace LandScape
{
    public partial class Form1 : Form
    {
        /// <summary>
        /// Массив, в котором хранятся пути к изображениям необходимым для отрисовки карты
        /// </summary>
        
        Notification Ntf = new Notification();
        Engine en = new Engine(16, 12, 40);
        Image[] LoI = new Image[Enum.GetValues(typeof(Engine.TypeOfImg)).Length]; 
        /// <summary>
        /// Двумерный массив, по которому строится карта
        /// </summary>
        private int[][] MapLsc;
        public Form1()
        {
            InitializeComponent();
        }
        /// <summary>
        /// Завершение работы приложения
        /// </summary>
        private void Exit_button_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
        /// <summary>
        /// Флаг, устанавливающий факт генерации карты
        /// </summary>
        bool Used = false;
        /// <summary>
        /// Генерация первого уровня
        /// </summary>
        private void Level_1_button_Click(object sender, EventArgs e)
        {
            Map_pictureBox.Image = null;
            this.Refresh();
            label1.Text = "You've chosen Rise. \n Chapter 1 \n It's gonna be easy.";
            if (Used)
                en.ResetMatrix(MapLsc);
            if (!File.Exists(Environment.CurrentDirectory + @"\maps\map1.txt"))
            {
                Ntf.Oops("Файл 'map3.txt' не найден. \n Обратитесь к поставщику.");
                Ntf.Dispose();
            }
            else
            {
                MapLsc = en.FillMap(Environment.CurrentDirectory + @"\maps\map1.txt", MapLsc);
                en.FillMapObj(MapLsc);
                Used = true;
            }
                this.Refresh();
        }
        /// <summary>
        /// Генерация второго уровня
        /// </summary>
        private void Level_2_button_Click(object sender, EventArgs e)
        {
            Map_pictureBox.Image = null;
            this.Refresh();
            label1.Text = "You've chosen Earth. \n Chapter 2 \n It's medium.";
            if (Used)
                en.ResetMatrix(MapLsc);
            if (!File.Exists(Environment.CurrentDirectory + @"\maps\map2.txt"))
            {
                Ntf.Oops("Файл 'map3.txt' не найден. \n Обратитесь к поставщику.");
                Ntf.Dispose();
            }
            else
            {
                MapLsc = en.FillMap(Environment.CurrentDirectory + @"\maps\map2.txt", MapLsc);
                en.FillMapObj(MapLsc);
                Used = true;
            }
            this.Refresh();
        }
        /// <summary>
        /// Генерация третьего уровня
        /// </summary>
        private void Level_3_button_Click(object sender, EventArgs e)
        {
            Map_pictureBox.Image = null;
            this.Refresh();
            label1.Text = "You've chosen Down. \n Chapter 3 \n It's hard.";
            if (Used)
                en.ResetMatrix(MapLsc);
            if (!File.Exists(Environment.CurrentDirectory + @"\maps\map3.txt"))
            { 
                Ntf.Oops("Файл 'map3.txt' не найден. \n Обратитесь к поставщику.");
                Ntf.Dispose();
            }

            else
            {
                MapLsc = en.FillMap(Environment.CurrentDirectory + @"\maps\map3.txt", MapLsc);
                en.FillMapObj(MapLsc);
                Used = true;
            }
            this.Refresh();
        }
        private void Form1_Load_1(object sender, EventArgs e)
        {
            
            try 
            {
                foreach(object i in Enum.GetValues(typeof(Engine.TypeOfImg)))
                    LoI[(int)i] = Image.FromFile(Environment.CurrentDirectory + @"\img\" + Enum.GetName(typeof(Engine.TypeOfImg), i) + ".png");
                
            }
            catch (FileNotFoundException)
            {
                Ntf.Oops("Изображения, необходимые \n для построения карты \n отсутствуют.");
            }
            Bitmap startImg = new Bitmap(Environment.CurrentDirectory + @"\img\Start.bmp");
            Map_pictureBox.Image = startImg;
        }
        private void Map_pictureBox_Paint(object sender, PaintEventArgs e)
        {
            for (int i = 0; i < en.LandScape.Count; i++)
            {
                e.Graphics.DrawImage(LoI[(int)en.LandScape[i].ImgType], en.LandScape[i].x, en.LandScape[i].y);
            } 
        }
    }
}
