using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using JT1078.Protocol.Tools.Extensions;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace JT1078.Protocol.Tools
{
    public partial class JT1078Form : Form
    {
        public JT1078Form()
        {
            InitializeComponent();
            Newtonsoft.Json.JsonSerializerSettings setting = new Newtonsoft.Json.JsonSerializerSettings();
            JsonConvert.DefaultSettings = new Func<JsonSerializerSettings>(() =>
            {
                setting.Converters.Add(new StringEnumConverter());
                setting.Converters.Add(new ByteArrayHexConverter());
                return setting;
            });
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(this.textBox1.Text))
            {
                MessageBox.Show("请输入数据包！");
                return;
            }
            try
            {
                var buffer = this.textBox1.Text.ToHexBytes();

                JT1078Package package = JT1078Serializer.Deserialize(buffer);

                this.textBox2.Text= JsonConvert.SerializeObject(package, Formatting.Indented);


            }
            catch (Exception ex)
            {
                this.textBox2.Text = JsonConvert.SerializeObject(ex);
            }
        }
    }
}
