using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using CifradoCs;

namespace Encriptacion
{
    public partial class Form1 : Form
    {
        private string key = "webCTGespPROYsept2007";
        private string iv = "deptoInfoCTG19092007";

        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //encriptar
            Crypto objCrypto = new Crypto(Crypto.CryptoProvider.TripleDES);
            objCrypto.Key = key;
            objCrypto.IV = iv;
            String resul = objCrypto.CifrarCadena(this.txtToEncrypt.Text.ToString());
            this.txtEncrypted.Text = resul;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //desencriptar
            Crypto objCrypto = new Crypto(Crypto.CryptoProvider.TripleDES);
            objCrypto.Key = key;
            objCrypto.IV = iv;
            String resul = objCrypto.DescifrarCadena(this.txtEncrypted.Text.ToString());
            this.txtDecryptedText.Text = resul;
        }

        

        
    }
}