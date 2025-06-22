using CozyComfort.WindowsFormsClient.Models;
using CozyComfort.WindowsFormsClient.Services;

namespace CozyComfort.WindowsFormsClient.Forms
{
    public partial class MainForm : Form
    {
        private User _currentUser;
        private ServiceClient _serviceClient;

        public MainForm(User user)
        {
            _currentUser = user;
            _serviceClient = new ServiceClient();
            InitializeComponent();
            CustomizeForUserType();
        }

        private void InitializeComponent()
        {
            this.menuStrip = new MenuStrip();
            this.productsMenu = new ToolStripMenuItem();
            this.inventoryMenu = new ToolStripMenuItem();
            this.ordersMenu = new ToolStripMenuItem();
            this.usersMenu = new ToolStripMenuItem();
            this.lblWelcome = new Label();
            this.dgvMain = new DataGridView();
            this.btnRefresh = new Button();
            this.btnNew = new Button();
            this.btnEdit = new Button();
            this.btnDelete = new Button();
            this.statusStrip = new StatusStrip();
            this.statusLabel = new ToolStripStatusLabel();

            this.menuStrip.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvMain)).BeginInit();
            this.statusStrip.SuspendLayout();
            this.SuspendLayout();

            // 
            // menuStrip
            // 
            this.menuStrip.Items.AddRange(new ToolStripItem[] {
                this.productsMenu,
                this.inventoryMenu,
                this.ordersMenu,
                this.usersMenu});
            this.menuStrip.Location = new Point(0, 0);
            this.menuStrip.Name = "menuStrip";
            this.menuStrip.Size = new Size(1000, 24);
            this.menuStrip.TabIndex = 0;
            this.menuStrip.Text = "menuStrip";

            // 
            // productsMenu
            // 
            this.productsMenu.Name = "productsMenu";
            this.productsMenu.Size = new Size(66, 20);
            this.productsMenu.Text = "Products";
            this.productsMenu.Click += new EventHandler(this.productsMenu_Click);

            // 
            // inventoryMenu
            // 
            this.inventoryMenu.Name = "inventoryMenu";
            this.inventoryMenu.Size = new Size(69, 20);
            this.inventoryMenu.Text = "Inventory";
            this.inventoryMenu.Click += new EventHandler(this.inventoryMenu_Click);

            // 
            // ordersMenu
            // 
            this.ordersMenu.Name = "ordersMenu";
            this.ordersMenu.Size = new Size(54, 20);
            this.ordersMenu.Text = "Orders";
            this.ordersMenu.Click += new EventHandler(this.ordersMenu_Click);

            // 
            // usersMenu
            // 
            this.usersMenu.Name = "usersMenu";
            this.usersMenu.Size = new Size(47, 20);
            this.usersMenu.Text = "Users";
            this.usersMenu.Click += new EventHandler(this.usersMenu_Click);

            // 
            // lblWelcome
            // 
            this.lblWelcome.AutoSize = true;
            this.lblWelcome.Font = new Font("Microsoft Sans Serif", 12F, FontStyle.Bold, GraphicsUnit.Point);
            this.lblWelcome.Location = new Point(20, 40);
            this.lblWelcome.Name = "lblWelcome";
            this.lblWelcome.Size = new Size(200, 20);
            this.lblWelcome.TabIndex = 1;
            this.lblWelcome.Text = $"Welcome, {_currentUser.Username} ({_currentUser.UserType}) - {_currentUser.CompanyName}";

            // 
            // dgvMain
            // 
            this.dgvMain.AllowUserToAddRows = false;
            this.dgvMain.AllowUserToDeleteRows = false;
            this.dgvMain.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvMain.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvMain.Location = new Point(20, 80);
            this.dgvMain.MultiSelect = false;
            this.dgvMain.Name = "dgvMain";
            this.dgvMain.ReadOnly = true;
            this.dgvMain.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            this.dgvMain.Size = new Size(950, 400);
            this.dgvMain.TabIndex = 2;

            // 
            // btnRefresh
            // 
            this.btnRefresh.Location = new Point(20, 500);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new Size(80, 30);
            this.btnRefresh.TabIndex = 3;
            this.btnRefresh.Text = "Refresh";
            this.btnRefresh.UseVisualStyleBackColor = true;
            this.btnRefresh.Click += new EventHandler(this.btnRefresh_Click);

            // 
            // btnNew
            // 
            this.btnNew.Location = new Point(120, 500);
            this.btnNew.Name = "btnNew";
            this.btnNew.Size = new Size(80, 30);
            this.btnNew.TabIndex = 4;
            this.btnNew.Text = "New";
            this.btnNew.UseVisualStyleBackColor = true;
            this.btnNew.Click += new EventHandler(this.btnNew_Click);

            // 
            // btnEdit
            // 
            this.btnEdit.Location = new Point(220, 500);
            this.btnEdit.Name = "btnEdit";
            this.btnEdit.Size = new Size(80, 30);
            this.btnEdit.TabIndex = 5;
            this.btnEdit.Text = "Edit";
            this.btnEdit.UseVisualStyleBackColor = true;
            this.btnEdit.Click += new EventHandler(this.btnEdit_Click);

            // 
            // btnDelete
            // 
            this.btnDelete.Location = new Point(320, 500);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new Size(80, 30);
            this.btnDelete.TabIndex = 6;
            this.btnDelete.Text = "Delete";
            this.btnDelete.UseVisualStyleBackColor = true;
            this.btnDelete.Click += new EventHandler(this.btnDelete_Click);

            // 
            // statusStrip
            // 
            this.statusStrip.Items.AddRange(new ToolStripItem[] { this.statusLabel });
            this.statusStrip.Location = new Point(0, 578);
            this.statusStrip.Name = "statusStrip";
            this.statusStrip.Size = new Size(1000, 22);
            this.statusStrip.TabIndex = 7;
            this.statusStrip.Text = "statusStrip";

            // 
            // statusLabel
            // 
            this.statusLabel.Name = "statusLabel";
            this.statusLabel.Size = new Size(39, 17);
            this.statusLabel.Text = "Ready";

            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new SizeF(7F, 15F);
            this.AutoScaleMode = AutoScaleMode.Font;
            this.ClientSize = new Size(1000, 600);
            this.Controls.Add(this.statusStrip);
            this.Controls.Add(this.btnDelete);
            this.Controls.Add(this.btnEdit);
            this.Controls.Add(this.btnNew);
            this.Controls.Add(this.btnRefresh);
            this.Controls.Add(this.dgvMain);
            this.Controls.Add(this.lblWelcome);
            this.Controls.Add(this.menuStrip);
            this.MainMenuStrip = this.menuStrip;
            this.Name = "MainForm";
            this.StartPosition = FormStartPosition.CenterScreen;
            this.Text = "Cozy Comfort - Main Dashboard";
            this.WindowState = FormWindowState.Maximized;

            this.menuStrip.ResumeLayout(false);
            this.menuStrip.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvMain)).EndInit();
            this.statusStrip.ResumeLayout(false);
            this.statusStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        private MenuStrip menuStrip;
        private ToolStripMenuItem productsMenu;
        private ToolStripMenuItem inventoryMenu;
        private ToolStripMenuItem ordersMenu;
        private ToolStripMenuItem usersMenu;
        private Label lblWelcome;
        private DataGridView dgvMain;
        private Button btnRefresh;
        private Button btnNew;
        private Button btnEdit;
        private Button btnDelete;
        private StatusStrip statusStrip;
        private ToolStripStatusLabel statusLabel;

        private void CustomizeForUserType()
        {
            lblWelcome.Text = $"Welcome, {_currentUser.Username} ({_currentUser.UserType}) - {_currentUser.CompanyName}";
            
            switch (_currentUser.UserType)
            {
                case UserType.Manufacturer:
                    // Manufacturers can see all functionality
                    break;
                case UserType.Distributor:
                    // Distributors have limited product management
                    break;
                case UserType.Seller:
                    // Sellers primarily work with orders and inventory
                    break;
            }

            // Load initial data - show products by default
            LoadProducts();
        }

        private async void productsMenu_Click(object sender, EventArgs e)
        {
            await LoadProducts();
        }

        private async void inventoryMenu_Click(object sender, EventArgs e)
        {
            await LoadInventory();
        }

        private async void ordersMenu_Click(object sender, EventArgs e)
        {
            await LoadOrders();
        }

        private async void usersMenu_Click(object sender, EventArgs e)
        {
            await LoadUsers();
        }

        private async Task LoadProducts()
        {
            try
            {
                statusLabel.Text = "Loading products...";
                
                // For demo purposes, create sample data
                var sampleProducts = new[]
                {
                    new { 
                        ProductId = 1, 
                        ProductName = "Luxury Wool Blanket", 
                        SKU = "LWB001", 
                        Material = "100% Wool", 
                        Size = "Queen", 
                        Color = "Navy Blue", 
                        Price = 199.99m 
                    },
                    new { 
                        ProductId = 2, 
                        ProductName = "Cotton Comfort Blanket", 
                        SKU = "CCB002", 
                        Material = "100% Cotton", 
                        Size = "King", 
                        Color = "Beige", 
                        Price = 89.99m 
                    },
                    new { 
                        ProductId = 3, 
                        ProductName = "Fleece Throw Blanket", 
                        SKU = "FTB003", 
                        Material = "Polyester Fleece", 
                        Size = "Throw", 
                        Color = "Gray", 
                        Price = 39.99m 
                    }
                };

                dgvMain.DataSource = sampleProducts;
                statusLabel.Text = $"Loaded {sampleProducts.Length} products";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading products: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                statusLabel.Text = "Error loading products";
            }
        }

        private async Task LoadInventory()
        {
            try
            {
                statusLabel.Text = "Loading inventory...";
                
                // For demo purposes, create sample inventory data
                var sampleInventory = new[]
                {
                    new { 
                        ProductId = 1, 
                        ProductName = "Luxury Wool Blanket", 
                        StockQuantity = _currentUser.UserType == UserType.Manufacturer ? 150 : 
                                       _currentUser.UserType == UserType.Distributor ? 25 : 5,
                        ReorderLevel = _currentUser.UserType == UserType.Manufacturer ? 50 : 
                                      _currentUser.UserType == UserType.Distributor ? 10 : 2,
                        Location = _currentUser.UserType == UserType.Manufacturer ? "Warehouse A" : 
                                  _currentUser.UserType == UserType.Distributor ? "Distribution Center 1" : "Store Floor"
                    },
                    new { 
                        ProductId = 2, 
                        ProductName = "Cotton Comfort Blanket", 
                        StockQuantity = _currentUser.UserType == UserType.Manufacturer ? 200 : 
                                       _currentUser.UserType == UserType.Distributor ? 30 : 8,
                        ReorderLevel = _currentUser.UserType == UserType.Manufacturer ? 75 : 
                                      _currentUser.UserType == UserType.Distributor ? 15 : 3,
                        Location = _currentUser.UserType == UserType.Manufacturer ? "Warehouse A" : 
                                  _currentUser.UserType == UserType.Distributor ? "Distribution Center 1" : "Store Floor"
                    },
                    new { 
                        ProductId = 3, 
                        ProductName = "Fleece Throw Blanket", 
                        StockQuantity = _currentUser.UserType == UserType.Manufacturer ? 250 : 
                                       _currentUser.UserType == UserType.Distributor ? 40 : 12,
                        ReorderLevel = _currentUser.UserType == UserType.Manufacturer ? 100 : 
                                      _currentUser.UserType == UserType.Distributor ? 20 : 5,
                        Location = _currentUser.UserType == UserType.Manufacturer ? "Warehouse A" : 
                                  _currentUser.UserType == UserType.Distributor ? "Distribution Center 1" : "Store Floor"
                    }
                };

                dgvMain.DataSource = sampleInventory;
                statusLabel.Text = $"Loaded {sampleInventory.Length} inventory items";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading inventory: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                statusLabel.Text = "Error loading inventory";
            }
        }

        private async Task LoadOrders()
        {
            try
            {
                statusLabel.Text = "Loading orders...";
                
                // For demo purposes, create sample order data
                var sampleOrders = new[]
                {
                    new { 
                        OrderId = 1, 
                        OrderNumber = "ORD20231201001", 
                        CustomerName = "John Smith", 
                        Status = "Pending", 
                        TotalAmount = 199.99m, 
                        OrderDate = DateTime.Now.AddDays(-2)
                    },
                    new { 
                        OrderId = 2, 
                        OrderNumber = "ORD20231201002", 
                        CustomerName = "Jane Doe", 
                        Status = "Confirmed", 
                        TotalAmount = 89.99m, 
                        OrderDate = DateTime.Now.AddDays(-1)
                    },
                    new { 
                        OrderId = 3, 
                        OrderNumber = "ORD20231201003", 
                        CustomerName = "Bob Johnson", 
                        Status = "Delivered", 
                        TotalAmount = 39.99m, 
                        OrderDate = DateTime.Now.AddDays(-5)
                    }
                };

                dgvMain.DataSource = sampleOrders;
                statusLabel.Text = $"Loaded {sampleOrders.Length} orders";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading orders: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                statusLabel.Text = "Error loading orders";
            }
        }

        private async Task LoadUsers()
        {
            try
            {
                statusLabel.Text = "Loading users...";
                
                // For demo purposes, create sample user data
                var sampleUsers = new[]
                {
                    new { 
                        UserId = 1, 
                        Username = "manufacturer", 
                        UserType = "Manufacturer", 
                        CompanyName = "Cozy Comfort Manufacturing", 
                        Email = "manufacturer@cozycomfort.com",
                        IsActive = true
                    },
                    new { 
                        UserId = 2, 
                        Username = "distributor1", 
                        UserType = "Distributor", 
                        CompanyName = "ABC Distribution", 
                        Email = "dist1@example.com",
                        IsActive = true
                    },
                    new { 
                        UserId = 3, 
                        Username = "seller1", 
                        UserType = "Seller", 
                        CompanyName = "XYZ Retail", 
                        Email = "seller1@example.com",
                        IsActive = true
                    }
                };

                dgvMain.DataSource = sampleUsers;
                statusLabel.Text = $"Loaded {sampleUsers.Length} users";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading users: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                statusLabel.Text = "Error loading users";
            }
        }

        private async void btnRefresh_Click(object sender, EventArgs e)
        {
            // Refresh current view based on what's currently displayed
            if (dgvMain.DataSource != null)
            {
                var currentData = dgvMain.DataSource;
                var dataType = currentData.GetType();
                
                if (dataType.Name.Contains("Product"))
                    await LoadProducts();
                else if (dataType.Name.Contains("Inventory"))
                    await LoadInventory();
                else if (dataType.Name.Contains("Order"))
                    await LoadOrders();
                else if (dataType.Name.Contains("User"))
                    await LoadUsers();
            }
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            MessageBox.Show("New item functionality would be implemented here.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (dgvMain.SelectedRows.Count > 0)
            {
                MessageBox.Show("Edit functionality would be implemented here.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Please select an item to edit.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (dgvMain.SelectedRows.Count > 0)
            {
                var result = MessageBox.Show("Are you sure you want to delete this item?", "Confirm Delete", 
                    MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                
                if (result == DialogResult.Yes)
                {
                    MessageBox.Show("Delete functionality would be implemented here.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            else
            {
                MessageBox.Show("Please select an item to delete.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
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