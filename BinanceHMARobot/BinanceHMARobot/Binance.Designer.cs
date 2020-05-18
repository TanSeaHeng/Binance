namespace BinanceHMARobot
{
    partial class Binance
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (ThreadStart)
            {
                string message = "HMA Robot is running now. Please Stop Robot first";
                string caption = "Warning";
                MessageBoxEx(message, caption);
                return;
            }
            Disconnect();
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Binance));
            this.CONTROL_GROUP = new System.Windows.Forms.GroupBox();
            this.QUOTES_GRID = new System.Windows.Forms.DataGridView();
            this.QUOTES_SYMBOL = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.QUOTES_PRICE = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.QUOTES_HMA = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ACCOUNT_GROUP = new System.Windows.Forms.GroupBox();
            this.BALANCE_GRID = new System.Windows.Forms.DataGridView();
            this.FIRST_ASSET = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.FIRST_FREE = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.FIRST_LOCKED = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.API_KEY_LABEL = new System.Windows.Forms.Label();
            this.API_KEY_TEXT = new System.Windows.Forms.TextBox();
            this.API_SECRET_LABEL = new System.Windows.Forms.Label();
            this.API_SECRET_TEXT = new System.Windows.Forms.TextBox();
            this.SYMBOL_LABEL = new System.Windows.Forms.Label();
            this.SYMBOLS_TEXT = new System.Windows.Forms.TextBox();
            this.TIMEFRAME_LABEL = new System.Windows.Forms.Label();
            this.TIME_INTERVAL_COMBO = new System.Windows.Forms.ComboBox();
            this.HMA_PRICE_LABEL = new System.Windows.Forms.Label();
            this.HMA_PRICE_COMBO = new System.Windows.Forms.ComboBox();
            this.HMA_PERIOD_LABEL = new System.Windows.Forms.Label();
            this.HMA_PERIOD_TEXT = new System.Windows.Forms.TextBox();
            this.QUANTITY_LABEL = new System.Windows.Forms.Label();
            this.QUANTITY_TEXT = new System.Windows.Forms.TextBox();
            this.START_BUTTON = new System.Windows.Forms.Button();
            this.JOIN_BUTTON = new System.Windows.Forms.Button();
            this.PARAM_GROUP = new System.Windows.Forms.GroupBox();
            this.HISTORY_GROUP = new System.Windows.Forms.GroupBox();
            this.HISTORY_ORDERS_GRID = new System.Windows.Forms.DataGridView();
            this.HISTORY_ORDER = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.HISTORY_SYMBOL = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.HISTORY_TIME = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.HISTORY_SIZE = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.HISTORY_PRICE = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.HISTORY_COMMISSION = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CONTROL_GROUP.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.QUOTES_GRID)).BeginInit();
            this.ACCOUNT_GROUP.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.BALANCE_GRID)).BeginInit();
            this.PARAM_GROUP.SuspendLayout();
            this.HISTORY_GROUP.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.HISTORY_ORDERS_GRID)).BeginInit();
            this.SuspendLayout();
            // 
            // CONTROL_GROUP
            // 
            this.CONTROL_GROUP.Controls.Add(this.QUOTES_GRID);
            this.CONTROL_GROUP.Location = new System.Drawing.Point(12, 12);
            this.CONTROL_GROUP.Name = "CONTROL_GROUP";
            this.CONTROL_GROUP.Size = new System.Drawing.Size(386, 338);
            this.CONTROL_GROUP.TabIndex = 3;
            this.CONTROL_GROUP.TabStop = false;
            this.CONTROL_GROUP.Text = "MARKET WATCH";
            // 
            // QUOTES_GRID
            // 
            this.QUOTES_GRID.AllowUserToAddRows = false;
            this.QUOTES_GRID.AllowUserToDeleteRows = false;
            this.QUOTES_GRID.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.QUOTES_GRID.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.QUOTES_SYMBOL,
            this.QUOTES_PRICE,
            this.QUOTES_HMA});
            this.QUOTES_GRID.Location = new System.Drawing.Point(0, 21);
            this.QUOTES_GRID.Name = "QUOTES_GRID";
            this.QUOTES_GRID.Size = new System.Drawing.Size(386, 317);
            this.QUOTES_GRID.TabIndex = 0;
            // 
            // QUOTES_SYMBOL
            // 
            this.QUOTES_SYMBOL.HeaderText = "SYMBOL";
            this.QUOTES_SYMBOL.Name = "QUOTES_SYMBOL";
            this.QUOTES_SYMBOL.ReadOnly = true;
            this.QUOTES_SYMBOL.Width = 120;
            // 
            // QUOTES_PRICE
            // 
            this.QUOTES_PRICE.HeaderText = "PRICE";
            this.QUOTES_PRICE.Name = "QUOTES_PRICE";
            this.QUOTES_PRICE.ReadOnly = true;
            this.QUOTES_PRICE.Width = 110;
            // 
            // QUOTES_HMA
            // 
            this.QUOTES_HMA.HeaderText = "HMA";
            this.QUOTES_HMA.Name = "QUOTES_HMA";
            this.QUOTES_HMA.ReadOnly = true;
            this.QUOTES_HMA.Width = 110;
            // 
            // ACCOUNT_GROUP
            // 
            this.ACCOUNT_GROUP.Controls.Add(this.BALANCE_GRID);
            this.ACCOUNT_GROUP.Location = new System.Drawing.Point(404, 356);
            this.ACCOUNT_GROUP.Name = "ACCOUNT_GROUP";
            this.ACCOUNT_GROUP.Size = new System.Drawing.Size(746, 325);
            this.ACCOUNT_GROUP.TabIndex = 5;
            this.ACCOUNT_GROUP.TabStop = false;
            this.ACCOUNT_GROUP.Text = "BALANCE";
            // 
            // BALANCE_GRID
            // 
            this.BALANCE_GRID.AllowUserToAddRows = false;
            this.BALANCE_GRID.AllowUserToDeleteRows = false;
            this.BALANCE_GRID.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.BALANCE_GRID.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.FIRST_ASSET,
            this.FIRST_FREE,
            this.FIRST_LOCKED});
            this.BALANCE_GRID.Location = new System.Drawing.Point(0, 23);
            this.BALANCE_GRID.Name = "BALANCE_GRID";
            this.BALANCE_GRID.Size = new System.Drawing.Size(745, 302);
            this.BALANCE_GRID.TabIndex = 0;
            // 
            // FIRST_ASSET
            // 
            this.FIRST_ASSET.HeaderText = "ASSET";
            this.FIRST_ASSET.Name = "FIRST_ASSET";
            this.FIRST_ASSET.ReadOnly = true;
            this.FIRST_ASSET.Width = 220;
            // 
            // FIRST_FREE
            // 
            this.FIRST_FREE.HeaderText = "FREE";
            this.FIRST_FREE.Name = "FIRST_FREE";
            this.FIRST_FREE.ReadOnly = true;
            this.FIRST_FREE.Width = 240;
            // 
            // FIRST_LOCKED
            // 
            this.FIRST_LOCKED.HeaderText = "LOCKED";
            this.FIRST_LOCKED.Name = "FIRST_LOCKED";
            this.FIRST_LOCKED.ReadOnly = true;
            this.FIRST_LOCKED.Width = 240;
            // 
            // API_KEY_LABEL
            // 
            this.API_KEY_LABEL.AutoSize = true;
            this.API_KEY_LABEL.Location = new System.Drawing.Point(7, 23);
            this.API_KEY_LABEL.Name = "API_KEY_LABEL";
            this.API_KEY_LABEL.Size = new System.Drawing.Size(51, 13);
            this.API_KEY_LABEL.TabIndex = 0;
            this.API_KEY_LABEL.Text = "API KEY:";
            // 
            // API_KEY_TEXT
            // 
            this.API_KEY_TEXT.Location = new System.Drawing.Point(87, 20);
            this.API_KEY_TEXT.Name = "API_KEY_TEXT";
            this.API_KEY_TEXT.Size = new System.Drawing.Size(280, 20);
            this.API_KEY_TEXT.TabIndex = 1;
            // 
            // API_SECRET_LABEL
            // 
            this.API_SECRET_LABEL.AutoSize = true;
            this.API_SECRET_LABEL.Location = new System.Drawing.Point(7, 52);
            this.API_SECRET_LABEL.Name = "API_SECRET_LABEL";
            this.API_SECRET_LABEL.Size = new System.Drawing.Size(73, 13);
            this.API_SECRET_LABEL.TabIndex = 2;
            this.API_SECRET_LABEL.Text = "API SECRET:";
            // 
            // API_SECRET_TEXT
            // 
            this.API_SECRET_TEXT.Location = new System.Drawing.Point(87, 49);
            this.API_SECRET_TEXT.Name = "API_SECRET_TEXT";
            this.API_SECRET_TEXT.Size = new System.Drawing.Size(280, 20);
            this.API_SECRET_TEXT.TabIndex = 3;
            // 
            // SYMBOL_LABEL
            // 
            this.SYMBOL_LABEL.AutoSize = true;
            this.SYMBOL_LABEL.Location = new System.Drawing.Point(7, 85);
            this.SYMBOL_LABEL.Name = "SYMBOL_LABEL";
            this.SYMBOL_LABEL.Size = new System.Drawing.Size(54, 13);
            this.SYMBOL_LABEL.TabIndex = 4;
            this.SYMBOL_LABEL.Text = "SYMBOL:";
            // 
            // SYMBOLS_TEXT
            // 
            this.SYMBOLS_TEXT.Location = new System.Drawing.Point(87, 82);
            this.SYMBOLS_TEXT.Name = "SYMBOLS_TEXT";
            this.SYMBOLS_TEXT.Size = new System.Drawing.Size(280, 20);
            this.SYMBOLS_TEXT.TabIndex = 5;
            this.SYMBOLS_TEXT.Text = "BTCUSDT";
            // 
            // TIMEFRAME_LABEL
            // 
            this.TIMEFRAME_LABEL.AutoSize = true;
            this.TIMEFRAME_LABEL.Location = new System.Drawing.Point(7, 118);
            this.TIMEFRAME_LABEL.Name = "TIMEFRAME_LABEL";
            this.TIMEFRAME_LABEL.Size = new System.Drawing.Size(63, 13);
            this.TIMEFRAME_LABEL.TabIndex = 6;
            this.TIMEFRAME_LABEL.Text = "INTERVAL:";
            // 
            // TIME_INTERVAL_COMBO
            // 
            this.TIME_INTERVAL_COMBO.FormattingEnabled = true;
            this.TIME_INTERVAL_COMBO.Items.AddRange(new object[] {
            "M1",
            "M5",
            "M15",
            "M30",
            "H1",
            "H4",
            "D1",
            "W1",
            "MN1"});
            this.TIME_INTERVAL_COMBO.Location = new System.Drawing.Point(87, 116);
            this.TIME_INTERVAL_COMBO.Name = "TIME_INTERVAL_COMBO";
            this.TIME_INTERVAL_COMBO.Size = new System.Drawing.Size(280, 21);
            this.TIME_INTERVAL_COMBO.TabIndex = 7;
            this.TIME_INTERVAL_COMBO.Text = "H1";
            // 
            // HMA_PRICE_LABEL
            // 
            this.HMA_PRICE_LABEL.AutoSize = true;
            this.HMA_PRICE_LABEL.Location = new System.Drawing.Point(7, 156);
            this.HMA_PRICE_LABEL.Name = "HMA_PRICE_LABEL";
            this.HMA_PRICE_LABEL.Size = new System.Drawing.Size(69, 13);
            this.HMA_PRICE_LABEL.TabIndex = 8;
            this.HMA_PRICE_LABEL.Text = "HMA PRICE:";
            // 
            // HMA_PRICE_COMBO
            // 
            this.HMA_PRICE_COMBO.FormattingEnabled = true;
            this.HMA_PRICE_COMBO.Items.AddRange(new object[] {
            "Close",
            "Open",
            "High",
            "Low"});
            this.HMA_PRICE_COMBO.Location = new System.Drawing.Point(87, 151);
            this.HMA_PRICE_COMBO.Name = "HMA_PRICE_COMBO";
            this.HMA_PRICE_COMBO.Size = new System.Drawing.Size(280, 21);
            this.HMA_PRICE_COMBO.TabIndex = 9;
            this.HMA_PRICE_COMBO.Text = "Close";
            // 
            // HMA_PERIOD_LABEL
            // 
            this.HMA_PERIOD_LABEL.AutoSize = true;
            this.HMA_PERIOD_LABEL.Location = new System.Drawing.Point(7, 194);
            this.HMA_PERIOD_LABEL.Name = "HMA_PERIOD_LABEL";
            this.HMA_PERIOD_LABEL.Size = new System.Drawing.Size(78, 13);
            this.HMA_PERIOD_LABEL.TabIndex = 10;
            this.HMA_PERIOD_LABEL.Text = "HMA PERIOD:";
            // 
            // HMA_PERIOD_TEXT
            // 
            this.HMA_PERIOD_TEXT.Location = new System.Drawing.Point(87, 189);
            this.HMA_PERIOD_TEXT.Name = "HMA_PERIOD_TEXT";
            this.HMA_PERIOD_TEXT.Size = new System.Drawing.Size(280, 20);
            this.HMA_PERIOD_TEXT.TabIndex = 11;
            this.HMA_PERIOD_TEXT.Text = "50";
            // 
            // QUANTITY_LABEL
            // 
            this.QUANTITY_LABEL.AutoSize = true;
            this.QUANTITY_LABEL.Location = new System.Drawing.Point(7, 226);
            this.QUANTITY_LABEL.Name = "QUANTITY_LABEL";
            this.QUANTITY_LABEL.Size = new System.Drawing.Size(65, 13);
            this.QUANTITY_LABEL.TabIndex = 12;
            this.QUANTITY_LABEL.Text = "QUANTITY:";
            // 
            // QUANTITY_TEXT
            // 
            this.QUANTITY_TEXT.Location = new System.Drawing.Point(87, 223);
            this.QUANTITY_TEXT.Name = "QUANTITY_TEXT";
            this.QUANTITY_TEXT.Size = new System.Drawing.Size(280, 20);
            this.QUANTITY_TEXT.TabIndex = 13;
            this.QUANTITY_TEXT.Text = "1";
            // 
            // START_BUTTON
            // 
            this.START_BUTTON.Location = new System.Drawing.Point(230, 278);
            this.START_BUTTON.Name = "START_BUTTON";
            this.START_BUTTON.Size = new System.Drawing.Size(114, 26);
            this.START_BUTTON.TabIndex = 14;
            this.START_BUTTON.Text = "START";
            this.START_BUTTON.UseVisualStyleBackColor = true;
            this.START_BUTTON.Click += new System.EventHandler(this.START_BUTTON_Click);
            // 
            // JOIN_BUTTON
            // 
            this.JOIN_BUTTON.Location = new System.Drawing.Point(27, 278);
            this.JOIN_BUTTON.Name = "JOIN_BUTTON";
            this.JOIN_BUTTON.Size = new System.Drawing.Size(114, 26);
            this.JOIN_BUTTON.TabIndex = 15;
            this.JOIN_BUTTON.Text = "JOIN";
            this.JOIN_BUTTON.UseVisualStyleBackColor = true;
            this.JOIN_BUTTON.Click += new System.EventHandler(this.JOIN_BUTTON_Click);
            // 
            // PARAM_GROUP
            // 
            this.PARAM_GROUP.Controls.Add(this.JOIN_BUTTON);
            this.PARAM_GROUP.Controls.Add(this.START_BUTTON);
            this.PARAM_GROUP.Controls.Add(this.QUANTITY_TEXT);
            this.PARAM_GROUP.Controls.Add(this.QUANTITY_LABEL);
            this.PARAM_GROUP.Controls.Add(this.HMA_PERIOD_TEXT);
            this.PARAM_GROUP.Controls.Add(this.HMA_PERIOD_LABEL);
            this.PARAM_GROUP.Controls.Add(this.HMA_PRICE_COMBO);
            this.PARAM_GROUP.Controls.Add(this.HMA_PRICE_LABEL);
            this.PARAM_GROUP.Controls.Add(this.TIME_INTERVAL_COMBO);
            this.PARAM_GROUP.Controls.Add(this.TIMEFRAME_LABEL);
            this.PARAM_GROUP.Controls.Add(this.SYMBOLS_TEXT);
            this.PARAM_GROUP.Controls.Add(this.SYMBOL_LABEL);
            this.PARAM_GROUP.Controls.Add(this.API_SECRET_TEXT);
            this.PARAM_GROUP.Controls.Add(this.API_SECRET_LABEL);
            this.PARAM_GROUP.Controls.Add(this.API_KEY_TEXT);
            this.PARAM_GROUP.Controls.Add(this.API_KEY_LABEL);
            this.PARAM_GROUP.Location = new System.Drawing.Point(12, 356);
            this.PARAM_GROUP.Name = "PARAM_GROUP";
            this.PARAM_GROUP.Size = new System.Drawing.Size(386, 325);
            this.PARAM_GROUP.TabIndex = 2;
            this.PARAM_GROUP.TabStop = false;
            this.PARAM_GROUP.Text = "SETTINGS";
            // 
            // HISTORY_GROUP
            // 
            this.HISTORY_GROUP.Controls.Add(this.HISTORY_ORDERS_GRID);
            this.HISTORY_GROUP.Location = new System.Drawing.Point(405, 12);
            this.HISTORY_GROUP.Name = "HISTORY_GROUP";
            this.HISTORY_GROUP.Size = new System.Drawing.Size(744, 338);
            this.HISTORY_GROUP.TabIndex = 6;
            this.HISTORY_GROUP.TabStop = false;
            this.HISTORY_GROUP.Text = "TODAY ORDERS";
            // 
            // HISTORY_ORDERS_GRID
            // 
            this.HISTORY_ORDERS_GRID.AllowUserToAddRows = false;
            this.HISTORY_ORDERS_GRID.AllowUserToDeleteRows = false;
            this.HISTORY_ORDERS_GRID.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.HISTORY_ORDERS_GRID.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.HISTORY_ORDER,
            this.HISTORY_SYMBOL,
            this.HISTORY_TIME,
            this.HISTORY_SIZE,
            this.HISTORY_PRICE,
            this.HISTORY_COMMISSION});
            this.HISTORY_ORDERS_GRID.Location = new System.Drawing.Point(-1, 19);
            this.HISTORY_ORDERS_GRID.Name = "HISTORY_ORDERS_GRID";
            this.HISTORY_ORDERS_GRID.Size = new System.Drawing.Size(745, 319);
            this.HISTORY_ORDERS_GRID.TabIndex = 0;
            // 
            // HISTORY_ORDER
            // 
            this.HISTORY_ORDER.HeaderText = "ORDER";
            this.HISTORY_ORDER.Name = "HISTORY_ORDER";
            this.HISTORY_ORDER.ReadOnly = true;
            this.HISTORY_ORDER.Width = 110;
            // 
            // HISTORY_SYMBOL
            // 
            this.HISTORY_SYMBOL.HeaderText = "ASSET";
            this.HISTORY_SYMBOL.Name = "HISTORY_SYMBOL";
            this.HISTORY_SYMBOL.ReadOnly = true;
            this.HISTORY_SYMBOL.Width = 110;
            // 
            // HISTORY_TIME
            // 
            this.HISTORY_TIME.HeaderText = "TIME";
            this.HISTORY_TIME.Name = "HISTORY_TIME";
            this.HISTORY_TIME.ReadOnly = true;
            this.HISTORY_TIME.Width = 120;
            // 
            // HISTORY_SIZE
            // 
            this.HISTORY_SIZE.HeaderText = "SIZE";
            this.HISTORY_SIZE.Name = "HISTORY_SIZE";
            this.HISTORY_SIZE.ReadOnly = true;
            this.HISTORY_SIZE.Width = 120;
            // 
            // HISTORY_PRICE
            // 
            this.HISTORY_PRICE.HeaderText = "PRICE";
            this.HISTORY_PRICE.Name = "HISTORY_PRICE";
            this.HISTORY_PRICE.ReadOnly = true;
            this.HISTORY_PRICE.Width = 120;
            // 
            // HISTORY_COMMISSION
            // 
            this.HISTORY_COMMISSION.HeaderText = "COMMISSION";
            this.HISTORY_COMMISSION.Name = "HISTORY_COMMISSION";
            this.HISTORY_COMMISSION.ReadOnly = true;
            this.HISTORY_COMMISSION.Width = 120;
            // 
            // Binance
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1162, 688);
            this.Controls.Add(this.HISTORY_GROUP);
            this.Controls.Add(this.ACCOUNT_GROUP);
            this.Controls.Add(this.CONTROL_GROUP);
            this.Controls.Add(this.PARAM_GROUP);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(1178, 727);
            this.MinimumSize = new System.Drawing.Size(1178, 727);
            this.Name = "Binance";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Binance HMA Robot";
            this.CONTROL_GROUP.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.QUOTES_GRID)).EndInit();
            this.ACCOUNT_GROUP.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.BALANCE_GRID)).EndInit();
            this.PARAM_GROUP.ResumeLayout(false);
            this.PARAM_GROUP.PerformLayout();
            this.HISTORY_GROUP.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.HISTORY_ORDERS_GRID)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.GroupBox CONTROL_GROUP;
        private System.Windows.Forms.GroupBox ACCOUNT_GROUP;
        private System.Windows.Forms.Label API_KEY_LABEL;
        private System.Windows.Forms.TextBox API_KEY_TEXT;
        private System.Windows.Forms.Label API_SECRET_LABEL;
        private System.Windows.Forms.TextBox API_SECRET_TEXT;
        private System.Windows.Forms.Label SYMBOL_LABEL;
        private System.Windows.Forms.TextBox SYMBOLS_TEXT;
        private System.Windows.Forms.Label TIMEFRAME_LABEL;
        private System.Windows.Forms.ComboBox TIME_INTERVAL_COMBO;
        private System.Windows.Forms.Label HMA_PRICE_LABEL;
        private System.Windows.Forms.ComboBox HMA_PRICE_COMBO;
        private System.Windows.Forms.Label HMA_PERIOD_LABEL;
        private System.Windows.Forms.TextBox HMA_PERIOD_TEXT;
        private System.Windows.Forms.Label QUANTITY_LABEL;
        private System.Windows.Forms.TextBox QUANTITY_TEXT;
        private System.Windows.Forms.Button START_BUTTON;
        private System.Windows.Forms.Button JOIN_BUTTON;
        private System.Windows.Forms.GroupBox PARAM_GROUP;
        private System.Windows.Forms.DataGridView QUOTES_GRID;
        private System.Windows.Forms.GroupBox HISTORY_GROUP;
        private System.Windows.Forms.DataGridView HISTORY_ORDERS_GRID;
        private System.Windows.Forms.DataGridViewTextBoxColumn QUOTES_SYMBOL;
        private System.Windows.Forms.DataGridViewTextBoxColumn QUOTES_PRICE;
        private System.Windows.Forms.DataGridViewTextBoxColumn QUOTES_HMA;
        private System.Windows.Forms.DataGridViewTextBoxColumn HISTORY_ORDER;
        private System.Windows.Forms.DataGridViewTextBoxColumn HISTORY_SYMBOL;
        private System.Windows.Forms.DataGridViewTextBoxColumn HISTORY_TIME;
        private System.Windows.Forms.DataGridViewTextBoxColumn HISTORY_SIZE;
        private System.Windows.Forms.DataGridViewTextBoxColumn HISTORY_PRICE;
        private System.Windows.Forms.DataGridViewTextBoxColumn HISTORY_COMMISSION;
        private System.Windows.Forms.DataGridView BALANCE_GRID;
        private System.Windows.Forms.DataGridViewTextBoxColumn FIRST_ASSET;
        private System.Windows.Forms.DataGridViewTextBoxColumn FIRST_FREE;
        private System.Windows.Forms.DataGridViewTextBoxColumn FIRST_LOCKED;
    }
}

