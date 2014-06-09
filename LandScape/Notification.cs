using System;
using System.Windows.Forms;

namespace LandScape
{
    public partial class Notification : Form
    {
        public Notification()
        {
            InitializeComponent();
        }
        /// <summary>
        /// Реакция на неполадки в работе программы
        /// </summary>
        /// <param name="hint">Сообщение об ошибке</param>
        public void Oops(string hint)
        {
            Notification alarm = new Notification();
            alarm.hint_label.Text = hint;
            alarm.Show();
        }
        private void OK_button_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
