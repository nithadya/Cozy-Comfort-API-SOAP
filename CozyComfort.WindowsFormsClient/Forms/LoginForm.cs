using CozyComfort.WindowsFormsClient.Models;
using CozyComfort.WindowsFormsClient.Services;

namespace CozyComfort.WindowsFormsClient.Forms
{
    public partial class LoginForm : Form
    {
        private ServiceClient _serviceClient;

        public LoginForm()
        {
            InitializeComponent();
            _serviceClient = new ServiceClient();
        }

        private void InitializeComponent()
        {
            this.txtUsername = new TextBox();
            this.txtPassword = new TextBox();
            this.btnLogin = new Button();
            this.lblUsername = new Label();
            this.lblPassword = new Label();
            this.lblTitle = new Label();
            this.cmbUserType = new ComboBox();
            this.lblUserType = new Label();
            this.SuspendLayout();

            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new Font("Microsoft Sans Serif", 16F, FontStyle.Bold, GraphicsUnit.Point);
            this.lblTitle.Location = new Point(150, 30);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new Size(200, 26);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "Cozy Comfort Login";

            // 
            // lblUsername
            // 
            this.lblUsername.AutoSize = true;
            this.lblUsername.Location = new Point(50, 100);
            this.lblUsername.Name = "lblUsername";
            this.lblUsername.Size = new Size(63, 15);
            this.lblUsername.TabIndex = 1;
            this.lblUsername.Text = "Username:";

            // 
            // txtUsername
            // 
            this.txtUsername.Location = new Point(150, 97);
            this.txtUsername.Name = "txtUsername";
            this.txtUsername.Size = new Size(200, 23);
            this.txtUsername.TabIndex = 2;

            // 
            // lblPassword
            // 
            this.lblPassword.AutoSize = true;
            this.lblPassword.Location = new Point(50, 140);
            this.lblPassword.Name = "lblPassword";
            this.lblPassword.Size = new Size(60, 15);
            this.lblPassword.TabIndex = 3;
            this.lblPassword.Text = "Password:";

            // 
            // txtPassword
            // 
            this.txtPassword.Location = new Point(150, 137);
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.PasswordChar = '*';
            this.txtPassword.Size = new Size(200, 23);
            this.txtPassword.TabIndex = 4;

            // 
            // lblUserType
            // 
            this.lblUserType.AutoSize = true;
            this.lblUserType.Location = new Point(50, 180);
            this.lblUserType.Name = "lblUserType";
            this.lblUserType.Size = new Size(64, 15);
            this.lblUserType.TabIndex = 5;
            this.lblUserType.Text = "User Type:";

            // 
            // cmbUserType
            // 
            this.cmbUserType.DropDownStyle = ComboBoxStyle.DropDownList;
            this.cmbUserType.FormattingEnabled = true;
            this.cmbUserType.Items.AddRange(new object[] { "Manufacturer", "Distributor", "Seller" });
            this.cmbUserType.Location = new Point(150, 177);
            this.cmbUserType.Name = "cmbUserType";
            this.cmbUserType.Size = new Size(200, 23);
            this.cmbUserType.TabIndex = 6;

            // 
            // btnLogin
            // 
            this.btnLogin.Location = new Point(200, 220);
            this.btnLogin.Name = "btnLogin";
            this.btnLogin.Size = new Size(100, 30);
            this.btnLogin.TabIndex = 7;
            this.btnLogin.Text = "Login";
            this.btnLogin.UseVisualStyleBackColor = true;
            this.btnLogin.Click += new EventHandler(this.btnLogin_Click);

            // 
            // LoginForm
            // 
            this.AutoScaleDimensions = new SizeF(7F, 15F);
            this.AutoScaleMode = AutoScaleMode.Font;
            this.ClientSize = new Size(500, 300);
            this.Controls.Add(this.btnLogin);
            this.Controls.Add(this.cmbUserType);
            this.Controls.Add(this.lblUserType);
            this.Controls.Add(this.txtPassword);
            this.Controls.Add(this.lblPassword);
            this.Controls.Add(this.txtUsername);
            this.Controls.Add(this.lblUsername);
            this.Controls.Add(this.lblTitle);
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "LoginForm";
            this.StartPosition = FormStartPosition.CenterScreen;
            this.Text = "Cozy Comfort - Login";
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        private TextBox txtUsername;
        private TextBox txtPassword;
        private Button btnLogin;
        private Label lblUsername;
        private Label lblPassword;
        private Label lblTitle;
        private ComboBox cmbUserType;
        private Label lblUserType;

        private async void btnLogin_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtUsername.Text) || 
                string.IsNullOrWhiteSpace(txtPassword.Text) ||
                cmbUserType.SelectedIndex == -1)
            {
                MessageBox.Show("Please fill in all fields.", "Login Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                // For demo purposes, we'll create a mock user based on selection
                var selectedUserType = (UserType)(cmbUserType.SelectedIndex + 1);
                var mockUser = new User
                {
                    UserId = cmbUserType.SelectedIndex + 1,
                    Username = txtUsername.Text,
                    UserType = selectedUserType,
                    CompanyName = GetCompanyName(selectedUserType),
                    IsActive = true
                };

                // In a real application, you would authenticate through the service
                // var user = await _serviceClient.AuthenticateUserAsync(txtUsername.Text, txtPassword.Text);

                if (mockUser != null)
                {
                    this.Hide();
                    var mainForm = new MainForm(mockUser);
                    mainForm.FormClosed += (s, args) => this.Close();
                    mainForm.Show();
                }
                else
                {
                    MessageBox.Show("Invalid credentials.", "Login Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Login failed: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private string GetCompanyName(UserType userType)
        {
            return userType switch
            {
                UserType.Manufacturer => "Cozy Comfort Manufacturing",
                UserType.Distributor => "ABC Distribution",
                UserType.Seller => "XYZ Retail",
                _ => "Unknown"
            };
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _serviceClient?.Dispose();
            }
            base.Dispose(disposing);
        }
    }
} 