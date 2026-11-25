using System.Drawing;
using System.Windows.Forms;

namespace Parks_LabAssignment8
{
    public class LoggedInForm : Form
    {
        public LoggedInForm(string username)
        {
            Text = "Logged In";
            StartPosition = FormStartPosition.CenterParent;
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            ClientSize = new Size(520, 330);
            BackColor = Color.FromArgb(245, 247, 250);
            Font = new Font("Segoe UI", 10F);

            Label title = new Label();
            title.Text = "Welcome, " + username + "!";
            title.Font = new Font("Segoe UI", 23F, FontStyle.Bold);
            title.ForeColor = Color.FromArgb(35, 52, 75);
            title.TextAlign = ContentAlignment.MiddleCenter;
            title.Location = new Point(30, 55);
            title.Size = new Size(460, 55);
            Controls.Add(title);

            Label message = new Label();
            message.Text = "You have successfully logged in.";
            message.Font = new Font("Segoe UI", 12F);
            message.ForeColor = Color.FromArgb(38, 145, 112);
            message.TextAlign = ContentAlignment.MiddleCenter;
            message.Location = new Point(30, 125);
            message.Size = new Size(460, 35);
            Controls.Add(message);

            Button logout = new Button();
            logout.Text = "Logout";
            logout.Location = new Point(95, 220);
            logout.Size = new Size(145, 42);
            StyleButton(logout, Color.FromArgb(43, 105, 176));
            logout.Click += delegate { Close(); };
            Controls.Add(logout);

            Button exit = new Button();
            exit.Text = "Exit";
            exit.Location = new Point(280, 220);
            exit.Size = new Size(145, 42);
            StyleButton(exit, Color.FromArgb(170, 70, 70));
            exit.Click += delegate { Application.Exit(); };
            Controls.Add(exit);
        }

        private void StyleButton(Button button, Color color)
        {
            button.BackColor = color;
            button.ForeColor = Color.White;
            button.FlatStyle = FlatStyle.Flat;
            button.FlatAppearance.BorderSize = 0;
            button.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            button.Cursor = Cursors.Hand;
        }
    }
}
