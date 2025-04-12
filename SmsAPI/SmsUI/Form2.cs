using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;
using System.Windows.Forms;

namespace SmsUI
{
    public partial class Form2 : Form
    {
        SqlConnection con = new SqlConnection("Server=(localdb)\\mssqllocaldb;Database= SmsInfoDB;Trusted_Connection=True");
        public Form2()
        {
            InitializeComponent();
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            //var content = "0";

            //try
            //{
            //    HttpClient client = new HttpClient();

            //    client.BaseAddress = new Uri("https://localhost:5001/api/sms/");

            //    HttpResponseMessage response = client.GetAsync("SmsTwilio/GetDataTwilio").Result;

            //    content = await response.Content.ReadAsStringAsync();

            //    if (content != null)
            //    {
            //        var dataTable = JsonConvert.DeserializeObject<DataTable>(content);
            //        dataGridView1.DataSource = dataTable;

            //    }
            //}
            //catch (Exception ex)
            //{
            //    Console.WriteLine(ex);
            //}
            //descobrir como interligar o de cima e o debaixo para juntar as string no formato json sheeeeeeeeeeeesh
            //------------------------------------------
            var content = "0";
            var content2 = "0";
            try
            {
                HttpClient client = new HttpClient();
                HttpClient client2 = new HttpClient();
                client.BaseAddress = new Uri("https://localhost:5001/api/sms/");
                client2.BaseAddress = new Uri("https://localhost:5001/api/sms/");
                HttpResponseMessage response = client.GetAsync("SmsTwilio/GetDataTwilio").Result;
                HttpResponseMessage response2 = client2.GetAsync("SmsBulk/GetDataBulk").Result;
                content = await response.Content.ReadAsStringAsync();
                content2 = await response2.Content.ReadAsStringAsync();
 
                var x = JArray.Parse(content);
                var y = JArray.Parse(content2);

                x.Merge(y, new JsonMergeSettings
                {
                    MergeArrayHandling = MergeArrayHandling.Union,
                });

                var result = x.GroupBy(i => i["Id"]).ToList();

                var final = JsonConvert.SerializeObject(result);
                final = final.Substring(1, final.Length - 2);

                var dataTable = JsonConvert.DeserializeObject<DataTable>(final);
                dataGridView1.DataSource = dataTable;

                
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            //------------------------------------------
            //var twilio = "";
            //var smsapi = "";
            //var smsbulk = "";
            //if (ckboxTwilio.Checked)
            //{
            //    twilio = "Twilio";
            //}
            //if (ckboxSmsApi.Checked)
            //{
            //    smsapi = "SMSAPI";
            //}
            //if (ckboxSmsBulk.Checked)
            //{
            //    smsbulk = "SMSBULKPT";
            //}

            //con.Open();
            //SqlCommand cmd = con.CreateCommand();
            //cmd.CommandType = CommandType.Text;
            //cmd.CommandText = "select * from dbo.SmsLogs where provider = '" + twilio + "' OR provider = '" + smsapi + "' OR provider = '" + smsbulk + "' ";
            //cmd.ExecuteNonQuery();
            //DataTable dt = new DataTable();
            //SqlDataAdapter da = new SqlDataAdapter(cmd);
            //da.Fill(dt);
            //dataGridView1.DataSource = dt;
            //con.Close();
        }
    }
}
