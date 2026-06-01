using Client.Presenters;
using Client.Views;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Client
{
    public partial class LoginForm : Form, ILoginView
    {
        public event EventHandler LoginClicked;
        public LoginForm(LoginPresenter presenter)
        {
            InitializeComponent();
            presenter.AttachView(this);

            txtPassword.UseSystemPasswordChar = true;
            btnLogin.Click += (sender, args) => LoginClicked?.Invoke(this, EventArgs.Empty);
        }

        private void LoginForm_Load(object sender, EventArgs e)
        {

        }
        public string Login => txtLogin.Text.Trim();

        public string Password => txtPassword.Text;


        public void CloseWithSuccess()
        {
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        public void ShowError(string message)
        {
            MessageBox.Show(message, "Authorization error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {

        }
    }
}
