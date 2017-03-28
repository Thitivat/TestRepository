using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BND.Services.IbanStore.Proxy;
using BND.Services.IbanStore.Proxy.Models;

namespace BND.Services.IbanStore.ProxyTestApp
{
    public partial class Form1 : Form
    {
        public string ApiUrl { get; set; }
        public string Action { get; set; }
        public string LastLocation { get; set; }
        public string LastStatus { get; set; }
        public string LastError { get; set; }

        public int LastIbanId { get; set; }
        public string AssignedUrl { get; set; }
        public NextAvailable NextAvailable { get; set; }

        public Form1()
        {
            InitializeComponent();
        }

        private void btnRun_Click(object sender, EventArgs e)
        {
            prepareRequest();
        }


        private void prepareRequest()
        {

            try
            {
                txtLocation.Text = "";
                txtResponse.Text = "";
                txtStatus.Text = "";
                ApiUrl = txtApiUrl.Text;

                Run(txtUidPrefix.Text, txtUid.Text);
                txtResponse.Text += string.Format("IBAN Id: {0}", LastIbanId);
                txtResponse.Text += string.Format("Assign Url: {0}", AssignedUrl);
                txtResponse.Text += string.Format("IBAN Id: {0}", LastIbanId);
            }
            catch (Exception e)
            {
                txtResponse.Text = e.Message;
            }
        }

        private void Run(string Prefix, string UID)
        {
            var Proxyobj = new IbanResource(ApiUrl, "atesttoken");
            switch (this.Action)
            {
                case "Reserve":
                    LastIbanId = Proxyobj.ReserveNextAvailable(Prefix, UID);
                    break;

                case "Assign":
                    AssignedUrl = Proxyobj.Assign(Prefix, UID, LastIbanId);
                    break;

                case "Get":
                    NextAvailable = Proxyobj.Get(Prefix, UID);
                    break;

                case "ReserveAndAssign":
                    NextAvailable = Proxyobj.ReserveAndAssign(Prefix, UID);
                    break;
            }
        }

        private void comboaction_SelectedIndexChanged(object sender, EventArgs e)
        {
            int i = comboaction.SelectedIndex;
            switch (i)
            {
                case 0:
                    Action = "Reserve"; break;
                case 1:
                    Action = "Assign"; break;
                case 2:
                    Action = "Get"; break;
                case 3:
                    Action = "ReserveAndAssign"; break;
            }
        }
    }
}
