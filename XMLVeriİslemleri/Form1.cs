using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Xml;
using System.Xml.XPath;
using System.IO;

namespace XMLVeriİslemleri
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        string dosyaYolu = Application.StartupPath + "\\calısanlistesi.xml";
        const string veritabani = "Data Source = WISSEN\\MSSQLSRV; Initial Catalog = Northwind; Integrated Security = True";
        private void button4_Click(object sender, EventArgs e)
        {
            //nodelerın ıcınde gezınmemızı saglar
            XPathDocument xmlDock = new XPathDocument(dosyaYolu);
            XPathNavigator xNav = xmlDock.CreateNavigator();
            XPathNodeIterator secilenNode = xNav.Select("Calisanlar/Calisan/Adi");
            string metin = " ";
            while (secilenNode.MoveNext())
            {
                if (secilenNode.Current.InnerXml.StartsWith("C"))

                    metin += secilenNode.Current.InnerXml + "\n";
            }
            if (metin != "")
                MessageBox.Show("Adi C ile baslayanlar:\n\n" + metin);
            else
                MessageBox.Show("Adi C ile baslayan bulunamadı");
            webBrowser1.Url = new Uri(dosyaYolu);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            webBrowser1.Url = new Uri(dosyaYolu);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DataSet ds = new DataSet();
            ds.ReadXml(dosyaYolu);
            dataGridView1.DataSource = ds.Tables[0];
            webBrowser1.Url = new Uri(dosyaYolu);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            XmlDocument xmldock = new XmlDocument();
            xmldock.Load(dosyaYolu);
            XmlNode secilenNode = xmldock.ChildNodes[1];
            bool bulundu = false;
            foreach (XmlNode item in secilenNode.ChildNodes)
            {
                if (item.ChildNodes[0].InnerText == "Can" || item.Attributes["TCNo"].Value == "123456789")
                {

                    MessageBox.Show("Aranan kişi bulundu \n\n"
                        + item.ChildNodes[0].InnerText + " "
                        + item.ChildNodes[1].InnerText + "\n"
                        + item.ChildNodes[2].InnerText + "\n"
                        + "TCNo:" + item.Attributes["TCNo"].Value);
                    bulundu = true;
                    break;
                }
            }
            if (bulundu)
            {
                MessageBox.Show("Aranan kişi 'Can' bulunamadı", " ",
                MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

        }

        private void button3_Click(object sender, EventArgs e)
        {
            XmlDocument xmldock = new XmlDocument();
            xmldock.Load(dosyaYolu);
            XmlNode secilenNode = xmldock.SelectSingleNode("Calisanlar/Calisan[Adi='Melek']");
            XmlNode xSoyadi = xmldock.CreateElement("Soyadi");
            xSoyadi.InnerText = "Erol";
            secilenNode.AppendChild(xSoyadi);
            xmldock.DocumentElement.AppendChild(secilenNode);
            xmldock.Save(dosyaYolu);


            //XmlNode secilenNode = xmldock.SelectSingleNode("Calisanlar/Calisan[starts-with(Adi,'C')]");
            if (secilenNode != null)
            {
                MessageBox.Show("Aranan kişi XPath ile bulundu \n\n"
                    + secilenNode.ChildNodes[0].InnerText + " "
                    + secilenNode.ChildNodes[1].InnerText + "\n "
                    + secilenNode.ChildNodes[2].InnerText + "\n"
                    + "TCNo:" + secilenNode.Attributes["TCNo"].Value);

            }
            else
            {
                MessageBox.Show("Aranan kişi 'Melek' bulunamadı", " ",
                MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            webBrowser1.Url = new Uri(dosyaYolu);

        }

        private void button5_Click(object sender, EventArgs e)
        {
            XmlDocument xmldock = new XmlDocument();
            xmldock.Load(dosyaYolu);
            XmlNode secilenNode = xmldock.SelectSingleNode("Calisanlar/Calisan[Adi='Melek']");
            if (secilenNode != null)
            {
                //soyadına ekle
                secilenNode.ChildNodes[1].InnerText += " Galipler";
                xmldock.Save(dosyaYolu);

                MessageBox.Show("Soyadına ekleme yapıldı \n\n"
                  + secilenNode.ChildNodes[0].InnerText + " "
                  + secilenNode.ChildNodes[1].InnerText);

            }
            else
            {
                MessageBox.Show("Aranan kişi 'Melek' bulunamadı", " ",
              MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            webBrowser1.Url = new Uri(dosyaYolu);
        }

        private void button6_Click(object sender, EventArgs e)
        {
            XmlDocument xmldock = new XmlDocument();
            xmldock.Load(dosyaYolu);
            XmlNode secilenNode = xmldock.SelectSingleNode("Calisanlar/Calisan[Adi='Melek']");
            if (secilenNode != null)
            {
                xmldock.DocumentElement.RemoveChild(secilenNode);
                xmldock.Save(dosyaYolu);
                MessageBox.Show("Kayıt silindi");
            }
            else
            {
                MessageBox.Show("Aranan kişi 'Melek' bulunamadı", " ",
           MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            webBrowser1.Url = new Uri(dosyaYolu);
        }

        private void button7_Click(object sender, EventArgs e)
        {
            XmlDocument xmldock = new XmlDocument();
            xmldock.Load(dosyaYolu);
            XmlElement yeniEleman = xmldock.CreateElement("Calisan");
            XmlAttribute tcno = xmldock.CreateAttribute("TCKNo");
            tcno.Value = "123456789";
            yeniEleman.Attributes.Append(tcno);

            XmlNode xAdi = xmldock.CreateElement("Adi");
            xAdi.InnerText = "Buse";
            yeniEleman.AppendChild(xAdi);

            XmlNode xSoyadi = xmldock.CreateElement("Soyadi");
            xSoyadi.InnerText = "Erol";
            yeniEleman.AppendChild(xSoyadi);


            xmldock.DocumentElement.AppendChild(yeniEleman);
            xmldock.Save(dosyaYolu);

            MessageBox.Show("Buse eklendi");

            webBrowser1.Url = new Uri(dosyaYolu);
        }

        private void button8_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(veritabani);
            SqlDataAdapter adp = new SqlDataAdapter("Select*from Products", con);
            DataTable dt = new DataTable("Products");
            adp.Fill(dt);
            DataSet ds = new DataSet("Products");
            ds.Tables.Add(dt);

            FolderBrowserDialog fd = new FolderBrowserDialog();
            DialogResult dr = fd.ShowDialog();
            if (dr != DialogResult.OK)

                return;
            string dosya = fd.SelectedPath + "\\SQLtoXML.xml";
            ds.WriteXml(dosya);
            MessageBox.Show("SQL den gelen veriler XML dosyasına yazıldı \n" + dosya);
            webBrowser1.Url = new Uri(dosya);

        }

        private void button9_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog fd = new FolderBrowserDialog();
            DialogResult dr = fd.ShowDialog();
            if (dr == DialogResult.OK)
            {
                string dosya = fd.SelectedPath + "\\SQLtoXML.xml";
                if (File.Exists(dosya))
                {
                    SqlConnection con = new SqlConnection(veritabani);
                    SqlDataAdapter adp = new SqlDataAdapter("Select*from ProductsX", con);

                    SqlCommandBuilder cb = new SqlCommandBuilder(adp);
                    DataSet ds = new DataSet();
                    ds.ReadXml(dosya);
                    adp.Update(ds.Tables[0]);
                    MessageBox.Show("XML den okunan veriler ProductsX tablosuna kaydedildi");
                    webBrowser1.Url = new Uri(dosya);
                }
            }

        }

        private void button10_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(veritabani);
            SqlDataAdapter adp = new SqlDataAdapter("Select ProductID,ProductName,UnitPrice from Products", con);

            DataTable dt = new DataTable("XProduct");
            adp.Fill(dt);
            DataSet ds = new DataSet("XProducts");
            ds.Tables.Add(dt);

            FolderBrowserDialog fd = new FolderBrowserDialog();
            DialogResult dr = fd.ShowDialog();
            if (dr == DialogResult.OK)
            {

                string dosya = fd.SelectedPath + "\\mySchema.xsd";
                ds.WriteXml(dosya);
                MessageBox.Show("SQL den gelen sorguya göre xml sema bilgisi dosyaya yazıldı \n" + dosya);
                webBrowser1.Url = new Uri(dosya);

            }
        }

        private void button11_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog fd = new FolderBrowserDialog();
            fd.Description = "XML schema dosyasının bulundugu klasoru secınız";
            DialogResult dr = fd.ShowDialog();
            if (dr == DialogResult.OK)
            {

                string semadosya = fd.SelectedPath + "\\mySchema.xsd";
                if (File.Exists(semadosya))
                {
                    //farklı bir veritabanından xml semasına uygun olarak veri alınıyor
                    SqlConnection con = new SqlConnection("Data Source = WISSEN\\MSSQLSRV; Initial Catalog = pubs; Integrated Security = True");

                    SqlDataAdapter adp = new SqlDataAdapter("Select pub_id AS ProductID,title AS ProductName, price AS UnitPrice from titles", con);

                    DataTable dt = new DataTable("XProduct");
                    //semayı kullanarak veri okumasu gerekıyor
                    dt.ReadXmlSchema(semadosya);
                    adp.Fill(dt);
                    DataSet ds = new DataSet("XProducts");
                    ds.Tables.Add(dt);

                    FolderBrowserDialog fd2 = new FolderBrowserDialog();
                    fd2.Description = "XML dosyasının kaydedılecegı klasoru secınız";
                    DialogResult dr2 = fd2.ShowDialog();
                    if (dr == DialogResult.OK)
                    {
                        string xmlDosya = fd2.SelectedPath + "\\semaya_uygun_veri.xml";
                        ds.WriteXml(xmlDosya);
                        MessageBox.Show("SQL den gelen sorguya gore XML sema bilgisi dosyaya yazıldı \n" + xmlDosya);
                        webBrowser1.Url = new Uri(xmlDosya);

                    }
                }
                else
                {
                    MessageBox.Show("XML sema dosyası bulunamadı \n" + semadosya, " ", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }



            }
        }
    }
}
