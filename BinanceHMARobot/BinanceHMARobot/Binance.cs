using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BinanceHMARobot.BinanceAPI;
using BinanceHMARobot.Indicators;
using BinanceHMARobot.Strategies;
using System.Threading;
using ServiceStack;
using Binance.API.Csharp.Client.Models.Market;
using Binance.API.Csharp.Client.Models.Account;

namespace BinanceHMARobot
{
    public partial class Binance : Form
    {
        private string ApiKey = "";
        private string SecretFile = @"Infor\Secret.Info";
        private string ConfigFile = @"Infor\Config.Info";
        private string ApiSecret = "";
        private string[] Symbols = null;
        private int nSymbolsCount = 0;
        private string TimeInterval = "";
        private string HMAPrice = "";
        private int HMAPeriod = 14;
        private decimal Quantity = 1;
        private string Path = "";
        private BinanceApiClient m_BinanceApi = null;
        private List<HmaStrategy> m_HmaStrategies;
        private int ThreadStep = 1000;
        private bool ThreadStart = false;
        private Thread EngineThread;
        //private bool ThreadStop = true;
        //private Mutex ThreadMutex = new Mutex();
        private readonly object syncLock = new object();
        private DateTime m_ExpiredDate = new DateTime(2020, 6, 11);

        public Binance()
        {
            InitializeComponent();
            this.JOIN_BUTTON.BackColor = Color.FromArgb(200, 200, 200, 200);
            this.START_BUTTON.BackColor = Color.FromArgb(200, 200, 200, 200);
            this.JOIN_BUTTON.Text = "JOIN";
            this.START_BUTTON.Text = "START";

            Path = System.Reflection.Assembly.GetExecutingAssembly().GetName().CodeBase;
            Path = System.IO.Path.GetDirectoryName(Path).Substring(6);
            ReadSecretInfo();
            ReadConfigInfo();
            nSymbolsCount = 0;
            m_HmaStrategies = new List<HmaStrategy>();
            //CheckForIllegalCrossThreadCalls = false;
        }


        private void JOIN_BUTTON_Click(object sender, EventArgs e)
        {
            var Now = DateTime.Now;
            if (m_ExpiredDate < Now) return;

            if (Connected())
            {
                Disconnect();
                return;
            }
            if (API_KEY_TEXT.Text.Length == 0)
            {
                string message = "You did not enter a Binance API KEY. Cancel this operation?";
                string caption = "Error Detected in Input";
                MessageBoxEx(message, caption);
                return;
            }
            if (API_SECRET_TEXT.Text.Length == 0)
            {
                string message = "You did not enter a Binance API SECRET. Cancel this operation?";
                string caption = "Error Detected in Input";
                MessageBoxEx(message, caption);
                return;
            }
            if (SYMBOLS_TEXT.Text.Length == 0)
            {
                string message = "You did not enter a SYMBOLS List. Cancel this operation?";
                string caption = "Error Detected in Input";
                MessageBoxEx(message, caption);
                return;
            }
            if (this.TIME_INTERVAL_COMBO.Text.Length == 0)
            {
                string message = "You did not select TIME INTERVAL. Cancel this operation?";
                string caption = "Error Detected in Input";
                MessageBoxEx(message, caption);
                return;
            }
            this.ApiSecret = this.API_SECRET_TEXT.Text;
            this.ApiKey = this.API_KEY_TEXT.Text;
            nSymbolsCount = SplitSymbols(this.SYMBOLS_TEXT.Text);
            this.TimeInterval = this.TIME_INTERVAL_COMBO.Text;
            if (this.m_BinanceApi != null) this.m_BinanceApi = null;
            this.m_BinanceApi = new BinanceApiClient(this.ApiKey, this.ApiSecret);
            //DateTime date = m_BinanceApi.GetServerTime();
            if (!this.m_BinanceApi.GetServerConnection())
            {
                Disconnect();
                string message = "Server Connection is failed. Please use the correct API KEY and API SECRET?";
                string caption = "Error Detected in Input";
                MessageBoxEx(message, caption);
                return;
            }
            //DateTime serverTime = this.m_BinanceApi.GetServerTime();
            this.JOIN_BUTTON.BackColor = Color.FromArgb(200, 0, 255, 0);
            this.JOIN_BUTTON.Text = "LOG OUT";
            SaveSecretInfo();
        }

        public bool Connected()
        {
            if (this.m_BinanceApi == null) return false;
            return this.m_BinanceApi.GetServerConnection();
        }

        public void Disconnect()
        {
            //ThreadStart = false;
            //Thread.Sleep(1000);
            if (ThreadStart)
            {
                string message = "HMA Robot is running now. Please Stop Robot first";
                string caption = "Warning";
                MessageBoxEx(message, caption);
                return;
            }
            this.JOIN_BUTTON.BackColor = Color.FromArgb(200, 200, 200, 200);
            this.JOIN_BUTTON.Text = "JOIN";
            if (this.m_BinanceApi == null) return;
            //this.m_BinanceApi.CloseUserStream();
            this.m_BinanceApi = null;
        }

        private void ReadSecretInfo()
        {
            string fullname = this.Path + @"\" + this.SecretFile;
            if (!System.IO.File.Exists(fullname)) return;
            string[] lines = System.IO.File.ReadAllLines(fullname);
            if (lines.Length >= 4)
            {
                this.API_KEY_TEXT.Text = lines[0];
                this.API_SECRET_TEXT.Text = lines[1];
                this.SYMBOLS_TEXT.Text = lines[2];
                this.TIME_INTERVAL_COMBO.Text = lines[3];
            }
        }

        private void ReadConfigInfo()
        {
            string fullname = this.Path + @"\" + this.ConfigFile;
            if (!System.IO.File.Exists(fullname)) return;
            string[] lines = System.IO.File.ReadAllLines(fullname);
            if (lines.Length >= 3)
            {
                this.HMA_PRICE_COMBO.Text = lines[0];
                this.HMA_PERIOD_TEXT.Text = lines[1];
                this.QUANTITY_TEXT.Text = lines[2];
            }
        }

        private void SaveSecretInfo()
        {
            string fullname = this.Path + @"\" + this.SecretFile;

            using (System.IO.StreamWriter file =
            new System.IO.StreamWriter(fullname))
            {
                file.WriteLine(this.API_KEY_TEXT.Text);
                file.WriteLine(this.API_SECRET_TEXT.Text);
                file.WriteLine(this.SYMBOLS_TEXT.Text);
                file.WriteLine(this.TIME_INTERVAL_COMBO.Text);
                file.Close();
            }
        }
        private void SaveConfigInfo()
        {
            string fullname = this.Path + @"\" + this.ConfigFile;

            using (System.IO.StreamWriter file =
            new System.IO.StreamWriter(fullname))
            {
                file.WriteLine(this.HMA_PRICE_COMBO.Text);
                file.WriteLine(this.HMA_PERIOD_TEXT.Text);
                file.WriteLine(this.QUANTITY_TEXT.Text);
                file.Close();
            }
        }

        private void START_BUTTON_Click(object sender, EventArgs e)
        {
            if (ThreadStart)
            {
                ThreadStart = EngineStop();
                return;
            }
            if (HMA_PRICE_COMBO.Text.Length == 0)
            {
                string message = "You did not Select a HMA PRICE. Cancel this operation?";
                string caption = "Error Detected in Input";
                MessageBoxEx(message, caption);
                return;
            }

            if (HMA_PERIOD_TEXT.Text.Length == 0)
            {
                string message = "You did not Select a HMA PERIOD. Cancel this operation?";
                string caption = "Error Detected in Input";
                MessageBoxEx(message, caption);
                return;
            }

            if (QUANTITY_TEXT.Text.Length == 0)
            {
                string message = "You did not Select a QUANTITY. Cancel this operation?";
                string caption = "Error Detected in Input";
                MessageBoxEx(message, caption);
                return;
            }
            if (!Connected())
            {
                string message = "Binace Server disconnected. Cancel this operation?";
                string caption = "Error Detected in Input";
                MessageBoxEx(message, caption);
                return;
            }

            nSymbolsCount = SplitSymbols(this.SYMBOLS_TEXT.Text);
            this.TimeInterval = this.TIME_INTERVAL_COMBO.Text;

            this.HMAPrice = this.HMA_PRICE_COMBO.Text;
            this.HMAPeriod = Convert.ToInt32(this.HMA_PERIOD_TEXT.Text);
            this.Quantity = Convert.ToDecimal(this.QUANTITY_TEXT.Text);

            if (!Connected()) return;
            if (!ThreadStart)
            {
                ThreadStart = EngineStart();
            }
            else return;


            SaveConfigInfo();
        }

        public void MessageBoxEx(string message, string caption)
        {
            MessageBoxButtons buttons = MessageBoxButtons.YesNo;
            DialogResult result;

            // Displays the MessageBox.
            result = MessageBox.Show(message, caption, buttons);
            if (result == System.Windows.Forms.DialogResult.Yes)
            {
                return;
            }
        }
        public bool EngineStart()
        {
            if (this.Symbols.Count() <= 0) return false;
            foreach (string symbol in this.Symbols)
            {
                AddStrategy(symbol, this.TimeInterval, this.HMAPeriod, this.HMAPrice, this.Quantity);
            }
            ThreadStart = true;
            EngineThread = new Thread(new ThreadStart(Engine));
            EngineThread.Start();
            this.START_BUTTON.BackColor = Color.FromArgb(200, 0, 255, 0);
            this.START_BUTTON.Text = "STOP";
            return true;
        }

        public bool EngineStop()
        {
            ThreadStart = false;
            
//            ThreadMutex.ReleaseMutex();
            return false;
        }
        private void StartButtonStopView()
        {
            m_HmaStrategies.Clear();
            Symbols = null;
            nSymbolsCount = 0;
            if (START_BUTTON.InvokeRequired)
            {
                START_BUTTON.Invoke(new Action(() => this.START_BUTTON.BackColor = Color.FromArgb(200, 200, 200, 200)));
            }
            else this.START_BUTTON.BackColor = Color.FromArgb(200, 200, 200, 200);
            if (START_BUTTON.InvokeRequired)
            {
                START_BUTTON.Invoke(new Action(() => this.START_BUTTON.Text = "START"));
            }
            else this.START_BUTTON.Text = "START";
        }

        private void Engine()
        {
            int MaxTry = 5;   
            int tryIndex = 0;
            while (ThreadStart)
            {
                lock (syncLock)
                {
                    if (!ThreadStart)
                    {
                        //ThreadMutex.ReleaseMutex();
                        break;
                    }
                    //ThreadStop = false;
                    try
                    {
                        //var time = m_BinanceApi.GetServerTime();
                        var account = m_BinanceApi.GetAccountInfo();
                        if (account == null) break;
                        bool found = false;

                        for (int i = 0; i < account.Balances.Count(); i++)
                        {
                            Balance balance = account.Balances.ElementAt(i);
                            if (balance.Free == 0.0m && balance.Locked == 0.0m) continue;
                            found = false;
                            foreach (DataGridViewRow row in this.BALANCE_GRID.Rows)
                            {
                                if (row.Cells[0].Value == null) continue;
                                if (row.Cells[0].Value.ToString() == balance.Asset)
                                {
                                    row.Cells[1].Value = balance.Free.ToString();
                                    row.Cells[2].Value = balance.Locked.ToString();
                                    found = true;
                                    break;
                                }
                            }
                            if (found) continue;
                            string[] newBalance = { balance.Asset, balance.Free.ToString(), balance.Locked.ToString() };
                            if (BALANCE_GRID.InvokeRequired)
                            {
                                BALANCE_GRID.Invoke(new Action(() => this.BALANCE_GRID.Rows.Add(newBalance)));
                            }
                            else this.BALANCE_GRID.Rows.Add(newBalance);
                        }
                        

                        foreach (HmaStrategy strg in m_HmaStrategies)
                        {
                            try
                            {
                                var price = m_BinanceApi.GetCurrentPrice(strg.GetSymbol());
                                if (price == 0) continue;
                                var Trades = m_BinanceApi.GetTradesList(strg.GetSymbol());
                                found = false;
                                for (int i = Trades.Count() - 1; i >= 0; i--)
                                {
                                    var date = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);
                                    //var date = new DateTime(2019, 3, 5);
                                    DateTime start = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
                                    DateTime tradTime = start.AddMilliseconds(Trades.ElementAt(i).Time).ToLocalTime();
                                    if (tradTime < date) break;
                                    found = false;
                                    foreach (DataGridViewRow row in this.HISTORY_ORDERS_GRID.Rows)
                                    {
                                        if (row.Cells[0].Value == null) continue;
                                        if (row.Cells[0].Value.ToString().ToInt() != Trades.ElementAt(i).Id) continue;
                                        found = true;
                                        break;
                                    }
                                    if (found) continue;
                                    var hisOrder = Trades.ElementAt(i);

                                    string[] newHisOrder = { hisOrder.Id.ToString(), hisOrder.CommissionAsset, tradTime.ToString(), hisOrder.Quantity.ToString("N4"), 
                                                            hisOrder.Price.ToString("N8"), hisOrder.Commission.ToString("N4")};
                                    if (HISTORY_ORDERS_GRID.InvokeRequired)
                                    {
                                        HISTORY_ORDERS_GRID.Invoke(new Action(() => HISTORY_ORDERS_GRID.Rows.Add(newHisOrder)));
                                    }
                                    else HISTORY_ORDERS_GRID.Rows.Add(newHisOrder);
                                }
                                found = false;
                                HMA_RESULT hma = strg.Engine(m_BinanceApi);
                                foreach (DataGridViewRow row in this.QUOTES_GRID.Rows)
                                {
                                    if (row.Cells[0].Value == null) continue;
                                    if (row.Cells[0].Value.ToString() != strg.GetSymbol()) continue;
                                    if (hma.Trend == ENUM_TREND.UP_TREND) row.DefaultCellStyle.BackColor = Color.Green;
                                    else if (hma.Trend == ENUM_TREND.DOWN_TREND) row.DefaultCellStyle.BackColor = Color.Red;
                                    else row.DefaultCellStyle.BackColor = Color.Yellow;
                                    row.Cells[1].Value = price.ToString();
                                    row.Cells[2].Value = hma.Value.ToString("N8");
                                    found = true;
                                }
                                if (found) continue;
                                string[] newRow = { strg.GetSymbol(), price.ToString(), hma.Value.ToString("N8") };
                                if (QUOTES_GRID.InvokeRequired)
                                {
                                    QUOTES_GRID.Invoke(new Action(() => QUOTES_GRID.Rows.Add(newRow)));
                                }
                                else QUOTES_GRID.Rows.Add(newRow);
                            }
                            catch (Exception e)
                            {
                                throw e;
                            }
                        }
                    }
                    catch (Exception e)
                    {
                        string message;
                        if (e.InnerException == null)
                            message = e.Message;
                        else message = e.InnerException.Message;

                        string caption = "Exception Error";
                        MessageBoxEx(message, caption);
                        Thread.Sleep(1000);
                        if (tryIndex > MaxTry)
                        {
                            m_HmaStrategies.Clear();
                            Symbols = null;
                            nSymbolsCount = 0;
                            if (START_BUTTON.InvokeRequired)
                            {
                                this.START_BUTTON.Invoke(new Action(() => this.START_BUTTON.BackColor = Color.FromArgb(200, 200, 200, 200)));
                            }
                            else this.START_BUTTON.BackColor = Color.FromArgb(200, 200, 200, 200);
                            if (START_BUTTON.InvokeRequired)
                            {
                                this.START_BUTTON.Invoke(new Action(() => this.START_BUTTON.Text = "START"));
                            }
                            else this.START_BUTTON.Text = "START";
                            ThreadStart = false;
                            //ThreadMutex.ReleaseMutex();
                            break;
                        }
                        tryIndex++;
                    }
                    Thread.Sleep(1);
                    Thread.Sleep(ThreadStep);
                }
                //ThreadMutex.ReleaseMutex();
            }
            //ThreadStop = true;
            StartButtonStopView();
        }

        private void AddStrategy(string symbol,
                    string strTimeInterval, int period, string applied, decimal quantity)
        {
            foreach (HmaStrategy strategy in m_HmaStrategies)
            {
                if (strategy.IsExisted(symbol, strTimeInterval)) return;
            }
            
            HmaStrategy strg = new HmaStrategy(symbol, strTimeInterval, period, applied, quantity, m_BinanceApi, GetLastOrder(symbol));
            m_HmaStrategies.Add(strg);
        }

        private Order GetLastOrder(string symbol)
        {
            int MaxLoop = 2;
            int loop = 0;
            while (loop < MaxLoop)
            {
                try
                {
                    var orders = m_BinanceApi.GetAllOrders(symbol);
                    if (orders == null) return null;

                    for (int i = orders.Count() - 1; i >= 0; i--)
                    {
                        if (orders.ElementAt(i).Status == "FILLED" || orders.ElementAt(i).Status == "PARTIALLY_FILLED") return orders.ElementAt(i);
                    }
                    break;
                }
                catch (Exception e)
                {
                    string message;
                    if (e.InnerException == null)
                        message = e.Message;
                    else message = e.InnerException.Message;

                    string caption = "Exception Error";
                    MessageBoxEx(message, caption);
                    Thread.Sleep(100);
                }
            }         
            return null;
        }

        private int SplitSymbols(string symbols)
        {
            string temp = symbols.Replace(" ", "");
            //temp = temp.ToUpper();
            string[] arrayTemp;
            arrayTemp = temp.Split(',');
            if (arrayTemp.Length == 0) return 0;
            if (Symbols != null) Symbols = null;
            Symbols = new string[arrayTemp.Length];
            int index = 0;
            foreach (string line in arrayTemp)
            {
                try
                {
                    Symbols[index++] = line;
                }
                catch (Exception)
                {
                    continue;
                }
            }
            if (index == 0) return 0;
            if (index > 1) index = 1;
            System.Array.Resize(ref Symbols, index);
            return index;
        }
        
    }
}
