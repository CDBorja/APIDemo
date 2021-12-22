using ApiPrmHelper.Xml;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ApiPrmHelper
{
    public partial class frmMain : Form
    {
        frmAbout objfrmAbout;
        public frmMain()
        {
            InitializeComponent();
          
        }

        private void button1_Click(object sender, EventArgs e)
        {
            byte[] jsonData = Encoding.UTF8.GetBytes(TxtJson.Text);
            //byte[] jsonData = Encoding.ASCII.GetBytes(TxtJson.Text);

            //byte[] secretKeyBytes = Convert.FromBase64String(TxtSecretKey.Text);
            byte[] secretKeyBytes = Encoding.UTF8.GetBytes(TxtSecretKey.Text);

            HMACSHA256 hmac = new HMACSHA256(secretKeyBytes);

            byte[] signatureBytes = hmac.ComputeHash(jsonData);
            

            TxtLaValue.Text = Convert.ToBase64String(signatureBytes);

            TxtPayload.Text = Convert.ToBase64String(jsonData);

            Book nBook = new Book()
            {
                json = Convert.ToBase64String(jsonData),
                key = Convert.ToBase64String(secretKeyBytes)
            };
            XmlHelper.WriteXML(nBook);

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            objfrmAbout = new frmAbout();
            try
            {
                var sBook = XmlHelper.ReadXML();
                TxtJson.Text = Encoding.UTF8.GetString(Convert.FromBase64String(sBook.json));
                TxtSecretKey.Text = Encoding.UTF8.GetString(Convert.FromBase64String(sBook.key));
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
            
        }

        private void AbtTlStipItemAbout_Click(object sender, EventArgs e)
        {
            this.objfrmAbout.Show();
            this.objfrmAbout.TopMost = true;
        }
    }
}
