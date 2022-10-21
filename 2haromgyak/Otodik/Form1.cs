using Otodik.Entities;
using Otodik.MnbServiceReference;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using System.Xml;

namespace Otodik
{
    public partial class Form1 : Form
    {
        BindingList<RateData> Rates = new BindingList<RateData>();
        BindingList<string> Currencies = new BindingList<string>();
        

        public Form1()
        {
            
            InitializeComponent();
            var res = GetCurrencies().GetCurrenciesResult;
            XmlCurency(res);
            comboBox1.DataSource = Currencies;
            RefreshData();
        }

        private void XmlCurency(string res)
        {
            XmlDocument xml = new XmlDocument();
            xml.LoadXml(res);
            foreach (XmlElement x in xml.DocumentElement)
            {
                var child = (XmlElement)x.ChildNodes[0];
                Currencies.Add(child.GetAttribute("Curr").ToString());
            }
        }

        private GetCurrenciesResponseBody GetCurrencies()
        {
            var mnbService = new MNBArfolyamServiceSoapClient();
            var request = new GetCurrenciesRequestBody();
            var response = mnbService.GetCurrencies(request);
            return response;
        }

        private void RefreshData()
        {
            Rates.Clear();
            var result = GetExchangeRate().GetExchangeRatesResult;
            dataGridView1.DataSource = Rates;
            XmlProcess(result);
            CreateChart();
        }

        private void CreateChart()
        {
            chart1.DataSource = Rates;
            var series = chart1.Series[0];
            series.ChartType = SeriesChartType.Line;
            series.XValueMember = "Date";
            series.YValueMembers = "Value";

            series.BorderWidth = 2;
            chart1.Legends[0].Enabled = false;
            var chartArea = chart1.ChartAreas[0];
            chartArea.AxisX.MajorGrid.Enabled = false;
            chartArea.AxisY.MajorGrid.Enabled = false;
            chartArea.AxisY.IsStartedFromZero = false;
        }

        private void XmlProcess(string r)
        {
            XmlDocument xml = new XmlDocument();
            xml.LoadXml(r);
            foreach (XmlElement x in xml.DocumentElement)
            {
                RateData rd = new RateData();

                rd.Date=DateTime.Parse(x.GetAttribute("date"));
                var child = (XmlElement)x.ChildNodes[0];
                rd.Currency = child.GetAttribute("curr");
                var unit = decimal.Parse(child.GetAttribute("unit"));
                var value = decimal.Parse(x.InnerText);
                if (unit != 0)
                    rd.Value = value / unit;
                
                Rates.Add(rd);
            }

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private GetExchangeRatesResponseBody GetExchangeRate()
        {
            var mnbService = new MNBArfolyamServiceSoapClient();
            var request = new GetExchangeRatesRequestBody()
            {
                currencyNames = comboBox1.SelectedItem.ToString(),
                startDate = dateTimePicker1.Value.ToString(),
                endDate = dateTimePicker2.Value.ToString()
            };
            var response = mnbService.GetExchangeRates(request);
            return response;
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            RefreshData();
        }

        private void dateTimePicker2_ValueChanged(object sender, EventArgs e)
        {
            RefreshData();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            RefreshData();
        }
    }
}
