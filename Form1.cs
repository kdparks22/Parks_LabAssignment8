using System;
using System.Drawing;
using System.Windows.Forms;

namespace Parks_LabAssignment8
{
    public partial class frmLogin : Form
    {
        private TextBox txtLoginUsername;
        private TextBox txtLoginPassword;
        private TextBox txtNewUsername;
        private TextBox txtNewPassword;
        private TextBox txtConfirmPassword;
        private Label lblStatus;

        public frmLogin()
        {
            InitializeComponent();
            BuildLoginInterface();
        }

        private void BuildLoginInterface()
        {
            BackColor = Color.FromArgb(245, 247, 250);
            Font = new Font("Segoe UI", 10F);
            ClientSize = new Size(900, 650);
            Text = "User Login";
            StartPosition = FormStartPosition.CenterScreen;
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;

            Label heading = new Label();
            heading.Text = "ACCOUNT ACCESS";
            heading.Font = new Font("Segoe UI", 22F, FontStyle.Bold);
            heading.ForeColor = Color.FromArgb(35, 52, 75);
            heading.Location = new Point(45, 25);
            heading.Size = new Size(800, 45);
            Controls.Add(heading);

            Label subtitle = new Label();
            subtitle.Text = "Log in to your account or create a new one below.";
            subtitle.ForeColor = Color.FromArgb(100, 112, 128);
            subtitle.Location = new Point(48, 73);
            subtitle.Size = new Size(800, 25);
            Controls.Add(subtitle);

            Panel loginPanel = CreatePanel(new Point(45, 125), new Size(390, 420));
            Controls.Add(loginPanel);
            AddSectionTitle(loginPanel, "Existing User Login", "Sign in with your username and password.");

            AddLabel(loginPanel, "Username", 30, 105);
            txtLoginUsername = AddTextBox(loginPanel, "txtLoginUsername", 30, 132, 325, false);

            AddLabel(loginPanel, "Password", 30, 190);
            txtLoginPassword = AddTextBox(loginPanel, "txtLoginPassword", 30, 217, 325, true);

            Button btnLogin = CreateButton("Login", new Point(30, 285), new Size(325, 45), true);
            btnLogin.Click += BtnLogin_Click;
            loginPanel.Controls.Add(btnLogin);

            Label loginHint = new Label();
            loginHint.Text = "Your password is checked securely. It is never stored as plain text.";
            loginHint.ForeColor = Color.FromArgb(110, 120, 135);
            loginHint.Font = new Font("Segoe UI", 8.5F);
            loginHint.Location = new Point(30, 345);
            loginHint.Size = new Size(325, 42);
            Controls.Add(loginHint);
            // Hint belongs visually inside the login panel.
            loginPanel.Controls.Add(loginHint);
            loginHint.Location = new Point(30, 345);

            Panel createPanel = CreatePanel(new Point(465, 125), new Size(390, 420));
            Controls.Add(createPanel);
            AddSectionTitle(createPanel, "Create New User", "Register a new account.");

            AddLabel(createPanel, "Username", 30, 90);
            txtNewUsername = AddTextBox(createPanel, "txtNewUsername", 30, 117, 325, false);

            AddLabel(createPanel, "Password", 30, 165);
            txtNewPassword = AddTextBox(createPanel, "txtNewPassword", 30, 192, 325, true);

            AddLabel(createPanel, "Confirm Password", 30, 240);
            txtConfirmPassword = AddTextBox(createPanel, "txtConfirmPassword", 30, 267, 325, true);

            Button btnCreateUser = CreateButton("Create User", new Point(30, 325), new Size(325, 45), false);
            btnCreateUser.Click += BtnCreateUser_Click;
            createPanel.Controls.Add(btnCreateUser);

            lblStatus = new Label();
            lblStatus.Text = "Ready.";
            lblStatus.ForeColor = Color.FromArgb(75, 88, 105);
            lblStatus.Location = new Point(48, 570);
            lblStatus.Size = new Size(805, 45);
            lblStatus.Font = new Font("Segoe UI", 9.5F);
            Controls.Add(lblStatus);
        }

        private Panel CreatePanel(Point location, Size size)
        {
            Panel panel = new Panel();
            panel.Location = location;
            panel.Size = size;
            panel.BackColor = Color.White;
            panel.BorderStyle = BorderStyle.FixedSingle;
            return panel;
        }

        private void AddSectionTitle(Panel panel, string title, string subtitle)
        {
            Label titleLabel = new Label();
            titleLabel.Text = title;
            titleLabel.Font = new Font("Segoe UI", 15F, FontStyle.Bold);
            titleLabel.ForeColor = Color.FromArgb(35, 52, 75);
            titleLabel.Location = new Point(25, 22);
            titleLabel.Size = new Size(340, 30);
            panel.Controls.Add(titleLabel);

            Label subtitleLabel = new Label();
            subtitleLabel.Text = subtitle;
            subtitleLabel.Font = new Font("Segoe UI", 9F);
            subtitleLabel.ForeColor = Color.FromArgb(110, 120, 135);
            subtitleLabel.Location = new Point(27, 55);
            subtitleLabel.Size = new Size(340, 25);
            panel.Controls.Add(subtitleLabel);
        }

        private void AddLabel(Panel parent, string text, int x, int y)
        {
            Label label = new Label();
            label.Text = text;
            label.Location = new Point(x, y);
            label.Size = new Size(325, 23);
            label.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            label.ForeColor = Color.FromArgb(55, 68, 85);
            parent.Controls.Add(label);
        }

        private TextBox AddTextBox(Panel parent, string name, int x, int y, int width, bool password)
        {
            TextBox box = new TextBox();
            box.Name = name;
            box.Location = new Point(x, y);
            box.Size = new Size(width, 30);
            box.Font = new Font("Segoe UI", 10F);
            box.BorderStyle = BorderStyle.FixedSingle;
            box.UseSystemPasswordChar = password;
            parent.Controls.Add(box);
            return box;
        }

        private Button CreateButton(string text, Point location, Size size, bool primary)
        {
            Button button = new Button();
            button.Text = text;
            button.Location = location;
            button.Size = size;
            button.FlatStyle = FlatStyle.Flat;
            button.FlatAppearance.BorderSize = 0;
            button.BackColor = primary ? Color.FromArgb(43, 105, 176) : Color.FromArgb(38, 145, 112);
            button.ForeColor = Color.White;
            button.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            button.Cursor = Cursors.Hand;
            return button;
        }

        private void BtnCreateUser_Click(object sender, EventArgs e)
        {
            string message;
            if (!UserValidator.ValidateNewUser(txtNewUsername.Text, txtNewPassword.Text,
                txtConfirmPassword.Text, out message))
            {
                SetStatus(message, true);
                return;
            }

            try
            {
                if (DatabaseHelper.UsernameExists(txtNewUsername.Text.Trim()))
                {
                    SetStatus("That username already exists. Please choose a different username.", true);
                    txtNewUsername.Focus();
                    return;
                }

                byte[] salt;
                byte[] hash = PasswordHelper.CreatePasswordHash(txtNewPassword.Text, out salt);
                DatabaseHelper.CreateUser(txtNewUsername.Text.Trim(), hash, salt);

                string createdUsername = txtNewUsername.Text.Trim();
                SetStatus("Account created successfully! You can now log in.", false);
                txtNewUsername.Clear();
                txtNewPassword.Clear();
                txtConfirmPassword.Clear();
                txtLoginUsername.Text = createdUsername;
                txtLoginPassword.Clear();
                txtLoginPassword.Focus();
            }
            catch (Exception ex)
            {
                SetStatus("Could not create the account. Check your SQL Server connection. Details: " + ex.Message, true);
            }
        }

        private void BtnLogin_Click(object sender, EventArgs e)
        {
            string message;
            if (!UserValidator.ValidateLogin(txtLoginUsername.Text, txtLoginPassword.Text, out message))
            {
                SetStatus(message, true);
                return;
            }

            try
            {
                if (DatabaseHelper.AuthenticateUser(txtLoginUsername.Text.Trim(), txtLoginPassword.Text))
                {
                    txtLoginPassword.Clear();
                    SetStatus("Login successful.", false);
                    using (LoggedInForm welcome = new LoggedInForm(txtLoginUsername.Text.Trim()))
                    {
                        welcome.ShowDialog(this);
                    }
                    txtLoginPassword.Clear();
                    txtLoginUsername.Focus();
                }
                else
                {
                    SetStatus("Incorrect username or password.", true);
                }
            }
            catch (Exception ex)
            {
                SetStatus("Could not log in. Check your SQL Server connection. Details: " + ex.Message, true);
            }
        }

        private void SetStatus(string message, bool isError)
        {
            lblStatus.Text = message;
            lblStatus.ForeColor = isError ? Color.Firebrick : Color.SeaGreen;
        }
    }
}
