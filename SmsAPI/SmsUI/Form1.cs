using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Configuration;
using System.IO;
using System.Net;
using System.Net.Http.Headers;
using System.Text.RegularExpressions;
using SmsUI.Models;
using Newtonsoft.Json;
using Flurl.Http;
using System.Threading;

namespace SmsUI
{
    public partial class Form1 : Form
    {
        public SmsTwilioMessageModel sms = new SmsTwilioMessageModel();
        public CancellationToken cancellationToken;
        public Form1()
        {
            InitializeComponent();
        }
        
        private void label1_Click(object sender, EventArgs e)
        {
            
        }

        private void button3_Click(object sender, EventArgs e)
        {
            
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        public async void ChangeString()
        {
            await Task.Delay(3000);
            lblShowMessage.Text = string.Empty;
        }
        public async void ChangeStringPrice()
        {
            await Task.Delay(3000);
            lblShowBalance.Text = string.Empty;
        }

        private async void btnSend_Click(object sender, EventArgs e)
        {
            
            sms.tel= txtPhoneNumber.Text;
            sms.msg= txtMessage.Text;
            var content = "0";
            switch (cmbService.SelectedIndex)
            {
                case 0: 
                    {
                        try
                        {
                            
                            var request= await "https://localhost:5001/api/sms/SmsTwilio".WithHeader("Content-Type", "application/json").PostJsonAsync(sms);
                            content = request.ResponseMessage.ToString();

                            if(content.Substring(12, 3) == "200")
                            {
                                this.txtPhoneNumber.Text = "";
                                this.txtMessage.Text = "";

                                this.lblShowMessage.Visible = true;
                                this.lblShowMessage.Text = "Mensagem enviada";
                            }
                            
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(ex);
                            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        }
                        ChangeString();
                        break;
                    }
                case 1:
                    {
                        try
                        {
                            var request = await "https://localhost:5001/api/sms/SmsApi".WithHeader("Content-Type", "application/json").PostJsonAsync(sms);
                            content = request.ResponseMessage.ToString();

                            if (content.Substring(12, 3) == "200")
                            {
                                this.txtPhoneNumber.Text = "";
                                this.txtMessage.Text = "";

                                this.lblShowMessage.Visible = true;
                                this.lblShowMessage.Text = "Mensagem enviada";
                            }
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(ex);
                            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        }
                        ChangeString();
                        break;
                    }
                case 2:
                    {
                        try
                        {
                            var request = await "https://localhost:5001/api/sms/SmsBulk".WithHeader("Content-Type", "application/json").PostJsonAsync(sms);
                            content = request.ResponseMessage.ToString();

                            if (content.Substring(12, 3) == "200")
                            {
                                this.txtPhoneNumber.Text = "";
                                this.txtMessage.Text = "";

                                this.lblShowMessage.Visible = true;
                                this.lblShowMessage.Text = "Mensagem enviada";
                            }
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(ex);
                            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        }
                        ChangeString();
                        break;
                    }
            }
        }

        public string btnSend_Result = null;
        private async void btnAccountBalance_Click(object sender, EventArgs e)
        {
            var content = "0";
            if (cmbService.Text == "Twilio")
            {
                try
                {
                    HttpClient client = new HttpClient();
                    client.BaseAddress = new Uri("https://localhost:5001/api/sms/");
                    HttpResponseMessage response = client.GetAsync("SmsTwilio").Result;
                    content = await response.Content.ReadAsStringAsync();
                    btnSend_Result = content;
                    if (btnSend_Result != null)
                    {
                        var subString2 = btnSend_Result.Substring(btnSend_Result.Length - 3);
                        var subString1 = Regex.Replace(btnSend_Result, "[^0-9.]", "") + " ";

                        this.lblShowBalance.Text = subString1 + subString2;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                }
              
            }
            if (cmbService.Text == "SmsApi")
            {
                try
                {
                    HttpClient client = new HttpClient();
                    client.BaseAddress = new Uri("https://localhost:5001/api/sms/");
                    HttpResponseMessage response = client.GetAsync("SmsApi").Result;
                    content = await response.Content.ReadAsStringAsync();
                    btnSend_Result = content;
                    if (btnSend_Result != null)
                    {
                        var text2 = btnSend_Result.Substring(btnSend_Result.Length - 32);
                        this.lblShowBalance.Text = Regex.Replace(text2, "[^0-9.]", "") + " " + "EUR";
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                }
            }
            if (cmbService.Text == "SmsBulk")
            {
                try
                {
                    HttpClient client = new HttpClient();
                    client.BaseAddress = new Uri("https://localhost:5001/api/sms/");
                    HttpResponseMessage response = client.GetAsync("SmsBulk").Result;
                    content = await response.Content.ReadAsStringAsync();
                    btnSend_Result = content;
                    if (btnSend_Result != null)
                    {
                        var subString1 = btnSend_Result.Substring(3);
                        this.lblShowBalance.Text = subString1.Substring(0, 7) + " EUR";
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                }
            }
            ChangeStringPrice();
        }

        private void txtMessage_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnSentMessages_Click(object sender, EventArgs e)
        {
            Form2 myForm = new Form2();
            this.Show();
            myForm.ShowDialog();
            this.Close();
        }
    }
}
