using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SQLite;

namespace YemekSepeti
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        SQLiteConnection con = new SQLiteConnection("Data Source=yemeksepeti.db;");
        Kullanici user;
        List<Restoran> restaurants = new List<Restoran>();
        Dictionary<string, int> orderMenus = new Dictionary<string, int>();
        List<string> orderMenusNames = new List<string>();
        int restaurantID = -1, selectedMenuID = -1;
        

        private void Form1_Load(object sender, EventArgs e)
        {
            panelGirisEkrani.BringToFront();
            comboBoxRestoranSiparisDurumu.SelectedIndex = 0;
        }

        Button NewButton(string name, int id, Point location)
        {//Her restoran için buton oluşturuyor
            Button buton = new Button();
            buton.Width = 120;
            buton.Height = 50;
            buton.Location = location;
            buton.Font = new Font(Button.DefaultFont, FontStyle.Bold);
            buton.Text = name;
            buton.Name = id.ToString();
            buton.Click += Click;
            return buton;
        }

        Button NewButtonMenus(string name, int id, Point location)
        {//her menü için 1 buton oluşturuyor
            Button buton = new Button();
            buton.Width = 120;
            buton.Height = 50;
            buton.Location = location;
            buton.Font = new Font(Button.DefaultFont, FontStyle.Bold);
            buton.Text = name;
            buton.Name = id.ToString();
            buton.Click += ClickMenu;
            return buton;
        }

        //menü butonlarının click olayı
        //tıklanan menünün bilgilerini sepete ekler
        private void ClickMenu(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            checkedListBoxSepet.Items.Add(btn.Text);
            int price = Convert.ToInt32(btn.Text.Split(' ')[btn.Text.Split(' ').Length-2]);
            txtRestoranIciToplamUcret.Text = (Convert.ToInt32(txtRestoranIciToplamUcret.Text) + price).ToString();
        }

        //restoran butonlarının click olayı
        //tıklanan restoranın içini(menülerin ekranı) açar
        private void Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            restaurantID = Convert.ToInt32(btn.Name);
            Point location = new Point(10, 63);
            int i = 0;
            con.Open();
            SQLiteCommand cmd = new SQLiteCommand("select menu_id,ad,fiyat FROM menus where restoran_id=" + btn.Name + "", con);
            using (SQLiteDataReader dr = cmd.ExecuteReader())
            {
                while (dr.Read())
                {
                    if (i == 0)
                    {
                        panelRestoranIci.Controls.Add(NewButtonMenus(dr["ad"].ToString() + " - " + dr["fiyat"].ToString() + " ₺", Convert.ToInt32(dr["menu_id"]), location));
                        i++;
                        continue;
                    }
                    if (i % 6 == 0)
                    {
                        location.X += 126;
                        location.Y = 10;
                        panelRestoranIci.Controls.Add(NewButtonMenus(dr["ad"].ToString() + " - " + dr["fiyat"].ToString() + " ₺", Convert.ToInt32(dr["menu_id"]), location));
                        i++;
                        continue;
                    }
                    location.Y += 56;
                    panelRestoranIci.Controls.Add(NewButtonMenus(dr["ad"].ToString() + " - " + dr["fiyat"].ToString() + " ₺", Convert.ToInt32(dr["menu_id"]), location));
                    i++;
                } 
            }
            con.Close();
            panelRestoranIci.BringToFront();
        }

        //Her restoran için buton oluşturma fonksiyonunu gerekli parametlerle beraber çağırıyor
        void CreateRestaurants()
        {
            Point location = new Point(10, 30);
            for (int i = 0; i < restaurants.Count; i++)
            {
                if(i == 0)
                {
                    panelKullaniciEkrani.Controls.Add(NewButton(restaurants[i].name,restaurants[i].id, location));
                    continue;
                }
                if(i%7 == 0)
                {
                    location.X += 126;
                    location.Y = 30;
                    panelKullaniciEkrani.Controls.Add(NewButton(restaurants[i].name, restaurants[i].id, location));
                    continue;
                }
                location.Y += 56;
                panelKullaniciEkrani.Controls.Add(NewButton(restaurants[i].name, restaurants[i].id, location));
            }
        }

        //içine gönderilen sql cümlesini çalıştırıyor
        void RunSQLcommand(string SQLCommand)
        {
            con.Open();
            try
            {
                using (SQLiteCommand cmd = new SQLiteCommand(SQLCommand, con))
                {
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
            con.Close();
        }

        //sipariş datagridview ının kolonlarının düzenliyor
        void RestaurantOrderDataGrid()
        {
            dataGridViewSiparisler.Columns[0].HeaderText = "MENU";
            dataGridViewSiparisler.Columns[1].HeaderText = "PRICE";
            dataGridViewSiparisler.Columns[2].HeaderText = "COUNT";
            dataGridViewSiparisler.Columns[3].HeaderText = "ADDRESS";
            dataGridViewSiparisler.Columns[4].HeaderText = "STATUS";
            dataGridViewSiparisler.Columns[0].Width = 45;
            dataGridViewSiparisler.Columns[1].Width = 42;
            dataGridViewSiparisler.Columns[2].Width = 40;
            dataGridViewSiparisler.Columns[3].Width = 140;
            dataGridViewSiparisler.Columns[4].Width = 75;
        }

        //menü datagridview ının kolonlarının düzenliyor
        void RestaurantMenuDataGrid()
        {
            dataGridViewRestoranMenuler.Columns[0].HeaderText = "ID";
            dataGridViewRestoranMenuler.Columns[1].HeaderText = "MENU";
            dataGridViewRestoranMenuler.Columns[2].HeaderText = "PRICE";
            dataGridViewRestoranMenuler.Columns[0].Width = 20;
            dataGridViewRestoranMenuler.Columns[1].Width = 60;
            dataGridViewRestoranMenuler.Columns[2].Width = 50;
        }

        private void linkLabelKayitOl_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            panelKayitOl.BringToFront();
        }

        //giriş ekranına döner
        private void linkLabelKayitOlGeriDon_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            panelGirisEkrani.BringToFront();
            for (int i = 0; i < groupBox1.Controls.Count; i++)
            {
                Control item = groupBox1.Controls[i];
                if(item is TextBox)
                {
                    item.Text = "";
                }
            }
            for (int i = 0; i < groupBox2.Controls.Count; i++)
            {
                Control item = groupBox2.Controls[i];
                if(item is TextBox)
                {
                    item.Text = "";
                }
            }
        }

        //girilen kullanıcı bilgileriyle yeni bir kullanıcı(veritabanı) kaydı oluşturur
        private void btnKullaniciEkle_Click(object sender, EventArgs e)
        {
            if(String.IsNullOrEmpty(txtKullaniciTC.Text) || String.IsNullOrEmpty(txtKullaniciAdi.Text) || String.IsNullOrEmpty(txtKullaniciAdres.Text) || String.IsNullOrEmpty(txtKullaniciKulAdi.Text) || String.IsNullOrEmpty(txtKullaniciSifre.Text) || String.IsNullOrEmpty(txtKullaniciSoyadi.Text) || String.IsNullOrEmpty(txtKullaniciTelefon.Text))
            {
                MessageBox.Show("Don't leave blank component.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            RunSQLcommand("INSERT into users VALUES('" + txtKullaniciTC.Text + "','" + txtKullaniciKulAdi.Text + "','" + txtKullaniciSifre.Text + "','" + txtKullaniciAdi.Text + "','" + txtKullaniciSoyadi.Text + "','" + txtKullaniciTelefon.Text + "','" + txtKullaniciAdres.Text + "')");
            txtKullaniciTC.Text = txtKullaniciKulAdi.Text = txtKullaniciSifre.Text = txtKullaniciAdi.Text = txtKullaniciSoyadi.Text = txtKullaniciTelefon.Text = txtKullaniciAdres.Text = "";
            MessageBox.Show("Registration Successful.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        //girilen restoran bilgileriyle yeni bir restoran(veritabanı) kaydı oluşturur
        private void btnRestoranEkle_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(txtRestoranAdi.Text) || String.IsNullOrEmpty(txtRestoranKulAdi.Text) || String.IsNullOrEmpty(txtRestoranSifre.Text) || String.IsNullOrEmpty(txtRestoranTelefon.Text))
            {
                MessageBox.Show("Don't leave blank component.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            RunSQLcommand("INSERT into restaurants(kullanici_adi,sifre,ad,telefon) VALUES('" + txtRestoranKulAdi.Text + "','" + txtRestoranSifre.Text + "','" + txtRestoranAdi.Text + "','" + txtRestoranTelefon.Text + "')");
            txtRestoranAdi.Text = txtRestoranKulAdi.Text = txtRestoranSifre.Text = txtRestoranTelefon.Text = "";
            MessageBox.Show("Registration Successful.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        //kullanıcı adı ve şifresi doğruysa bu kullanıcı girişini sağlar
        //kullanıcı ve tüm restoranlar için nesne üretir
        //restoranları üretme fonksiyonunu çağırır, bilgileri üretilen restoranlar nesnelerinden alır
        private void btnGirisKullanici_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(txtGirisKullaniciAdi.Text) || String.IsNullOrEmpty(txtGirisSifre.Text))
            {
                MessageBox.Show("Don't leave blank component.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            con.Open();
            using (SQLiteCommand sql = new SQLiteCommand("Select * FROM users where kullanici_adi='" + txtGirisKullaniciAdi.Text + "' and sifre='" + txtGirisSifre.Text + "'", con))
            {
                using (SQLiteDataReader dr = sql.ExecuteReader())
                {
                    if (dr.Read())
                    {
                        MessageBox.Show("Login successfully.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        panelKullaniciEkrani.BringToFront();
                        txtGirisKullaniciAdi.Text = txtGirisSifre.Text = "";
                        user = new Kullanici(dr["kullanici_adi"].ToString(), dr["sifre"].ToString(), dr["tc_kimlik"].ToString(), dr["ad"].ToString(), dr["soyad"].ToString(), dr["telefon"].ToString(), dr["adres"].ToString());
                        restaurants.Clear();

                        for (int i = 0; i < panelKullaniciEkrani.Controls.Count; i++)
                        {
                            Control item = panelKullaniciEkrani.Controls[i];
                            for (int k = 1; k < 50; k++)
                            {
                                string a = k.ToString();
                                if (item.Name.Equals(a))
                                {
                                    panelKullaniciEkrani.Controls.Remove(item);
                                    i--;
                                    break;
                                }
                            }
                        }

                        SQLiteCommand sql1 = new SQLiteCommand("Select * FROM restaurants", con);
                        using (SQLiteDataReader dr1 = sql1.ExecuteReader())
                        {
                            while (dr1.Read())
                                restaurants.Add(new Restoran(Convert.ToInt32(dr1["id"]), dr1["ad"].ToString(), dr1["telefon"].ToString()));
                        }


                        SQLiteCommand sql2 = new SQLiteCommand("Select * FROM menus", con);
                        using (SQLiteDataReader dr2 = sql2.ExecuteReader())
                        {
                            while (dr2.Read())
                            {
                                foreach (var item in restaurants)
                                    if (item.id == Convert.ToInt32(dr2["restoran_id"]))
                                        item.AddMenu(Convert.ToInt32(dr2["menu_id"]), dr2["ad"].ToString(), Convert.ToInt32(dr2["fiyat"]));
                            }
                        }
                        CreateRestaurants();
                    }
                    else
                        MessageBox.Show("Invalid username or password.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    con.Close();
                }
            }
                
        }

        //kullanıcı adı ve şifre doğruysa restoran girişini sağlar
        //menüler ve siparişler datagridviewlarını doldurur(veritabanından veri çekerek)
        private void btnGirisRestoran_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(txtGirisKullaniciAdi.Text) || String.IsNullOrEmpty(txtGirisSifre.Text))
            {
                MessageBox.Show("Don't leave blank component.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            con.Open();
            SQLiteCommand sql = new SQLiteCommand("Select * FROM restaurants where kullanici_adi='" + txtGirisKullaniciAdi.Text + "' and sifre='" + txtGirisSifre.Text + "'", con);
            using (SQLiteDataReader dr = sql.ExecuteReader())
            {
                if (dr.Read())
                {
                    MessageBox.Show("Login successfully.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    panelRestoranEkrani.BringToFront();
                    txtGirisKullaniciAdi.Text = txtGirisSifre.Text = "";
                    restaurantID = Convert.ToInt32(dr["id"]);

                    dataGridViewSiparisler.DataSource = "";
                    SQLiteDataAdapter da = new SQLiteDataAdapter("SELECT M.ad,M.fiyat,S.adet,K.adres,S.siparis_durumu FROM orders as S,users as K,menus as M where S.kullanici_tc = K.tc_kimlik and M.menu_id = S.menu_id and S.restoran_id = M.restoran_id and S.restoran_id=" + restaurantID + "", con);
                    using (DataSet ds = new DataSet())
                    {
                        da.Fill(ds);
                        dataGridViewSiparisler.DataSource = ds.Tables[0];
                    }
                    RestaurantOrderDataGrid();

                    comboBoxRestoranSiparis.Items.Clear();
                    comboBoxRestoranSiparis.Text = "";
                    SQLiteCommand cmd = new SQLiteCommand("SELECT S.id,M.restoran_id,M.menu_id,M.ad,M.fiyat,K.adres,S.siparis_durumu FROM orders as S,users as K,menus as M where S.kullanici_tc = K.tc_kimlik and M.menu_id = S.menu_id and S.restoran_id = M.restoran_id and S.restoran_id=" + restaurantID + "", con);
                    using (SQLiteDataReader dr1 = cmd.ExecuteReader())
                    {
                        while (dr1.Read())
                        {
                            comboBoxRestoranSiparis.Items.Add(dr1["id"].ToString() + " " + dr1["ad"].ToString() + " " + dr1["fiyat"].ToString() + " " + dr1["adres"].ToString() + " " + dr1["siparis_durumu"].ToString());
                            comboBoxRestoranSiparis.SelectedIndex = 0;
                        }
                    }

                    dataGridViewRestoranMenuler.DataSource = "";
                    SQLiteDataAdapter da1 = new SQLiteDataAdapter("SELECT menu_id,ad,fiyat FROM menus where restoran_id=" + restaurantID + "", con);
                    using (DataSet ds = new DataSet())
                    {
                        da1.Fill(ds);
                        dataGridViewRestoranMenuler.DataSource = ds.Tables[0];
                    }
                    RestaurantMenuDataGrid();
                }
                else
                    MessageBox.Show("Invalid username or password.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            con.Close();
        }

        //comboboxtan seçilen siparişin sipariş durumunu yine comboboxtan seçilen sipariş durumu olarak günceller
        private void btnRestoranSiparisGuncelle_Click(object sender, EventArgs e)
        {
            if(comboBoxRestoranSiparis.SelectedIndex == -1)
            {
                MessageBox.Show("Don't leave blank component.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            RunSQLcommand("UPDATE orders SET siparis_durumu='" + comboBoxRestoranSiparisDurumu.SelectedItem.ToString() + "' where id=" + comboBoxRestoranSiparis.SelectedItem.ToString().Split(' ')[0]);
            
            dataGridViewSiparisler.DataSource = "";
            con.Open();
            SQLiteDataAdapter da = new SQLiteDataAdapter("SELECT M.ad,M.fiyat,S.adet,K.adres,S.siparis_durumu FROM orders as S,users as K,menus as M where S.kullanici_tc = K.tc_kimlik and M.menu_id = S.menu_id and S.restoran_id = M.restoran_id and S.restoran_id=" + restaurantID + "", con);
            DataSet ds = new DataSet();
            da.Fill(ds);
            dataGridViewSiparisler.DataSource = ds.Tables[0];
            con.Close();
            RestaurantOrderDataGrid();
        }

        //ismi ve fiyatı yazılan menüyü veritabanına ekler
        private void btnRestoranMenuEkle_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(txtRestoranMenuAdi.Text) || String.IsNullOrEmpty(txtRestoranMenuFiyati.Text))
            {
                MessageBox.Show("Don't leave blank component.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            int menu_id = 1;
            con.Open();
            SQLiteCommand cmd = new SQLiteCommand("select max(menu_id) FROM menus where restoran_id=" + restaurantID + "",con);
            using (SQLiteDataReader dr = cmd.ExecuteReader())
            {
                if (dr.Read())
                    if(dr["max(menu_id)"] != DBNull.Value)
                        menu_id += Convert.ToInt32(dr["max(menu_id)"]);
            }

            using (SQLiteCommand cmd1 = new SQLiteCommand("INSERT INTO menus VALUES(" + restaurantID + "," + menu_id + ",'" + txtRestoranMenuAdi.Text + "'," + txtRestoranMenuFiyati.Text + ")", con))
            { cmd1.ExecuteNonQuery(); }

            dataGridViewRestoranMenuler.DataSource = "";
            SQLiteDataAdapter da1 = new SQLiteDataAdapter("SELECT menu_id,ad,fiyat FROM menus where restoran_id=" + restaurantID + "", con);
            using (DataSet ds = new DataSet())
            {
                da1.Fill(ds);
                dataGridViewRestoranMenuler.DataSource = ds.Tables[0];
            }
            RestaurantMenuDataGrid();
            txtRestoranMenuAdi.Text = txtRestoranMenuFiyati.Text = "";
            con.Close();
        }

        //menüler datagridview ında seçilen menünün bilgilerini textboxlara yazar
        private void dataGridViewRestoranMenuler_SelectionChanged(object sender, EventArgs e)
        {
            if(dataGridViewRestoranMenuler.CurrentRow != null && dataGridViewRestoranMenuler.CurrentRow.Cells[0].Value.ToString() != "")
            {
                selectedMenuID = Convert.ToInt32(dataGridViewRestoranMenuler.CurrentRow.Cells[0].Value.ToString());
                txtRestoranGuncelleMenuAdi.Text = dataGridViewRestoranMenuler.CurrentRow.Cells[1].Value.ToString();
                txtRestoranGuncelleMenuFiyati.Text = dataGridViewRestoranMenuler.CurrentRow.Cells[2].Value.ToString();
            }
        }

        //seçilen menüyü siler(databaseden)
        private void btnRestoranMenuSil_Click(object sender, EventArgs e)
        {
            int cevap = Convert.ToInt32(MessageBox.Show("Menu number "+ selectedMenuID + " will be deleted, are you sure?", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Warning));
            if (cevap == 6)
            {
                RunSQLcommand("DELETE FROM menus where restoran_id=" + restaurantID + " and menu_id=" + selectedMenuID + "");
                dataGridViewRestoranMenuler.DataSource = "";
                SQLiteDataAdapter da1 = new SQLiteDataAdapter("SELECT menu_id,ad,fiyat FROM menus where restoran_id=" + restaurantID + "", con);
                using (DataSet ds = new DataSet())
                {
                    da1.Fill(ds);
                    dataGridViewRestoranMenuler.DataSource = ds.Tables[0];
                }
                RestaurantMenuDataGrid();
            }
        }

        //restorandan çıkış yapıp giriş ekranına gider
        private void btnRestoranCikis_Click(object sender, EventArgs e)
        {
            panelGirisEkrani.BringToFront();
        }

        //sepette ürün var ise boşaltılacak diye uyarır evet denirse restoranlar ekranına geçer
        private void btnCikisMenuler_Click(object sender, EventArgs e)
        {
            if(checkedListBoxSepet.Items.Count > 0)
            {
                int cevap = Convert.ToInt32(MessageBox.Show("Cart gonna be empty, are you sure ?", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Warning));
                if (cevap != 6)
                    return;
            }
            for (int i = 0; i < panelRestoranIci.Controls.Count; i++)
            {
                Control item = panelRestoranIci.Controls[i];
                for (int k = 1; k < 50; k++)
                {
                    string a = k.ToString();
                    if (item.Name.Equals(a))
                    {
                        panelRestoranIci.Controls.Remove(item);
                        i--;
                        break;
                    }
                }
            }
            txtRestoranIciToplamUcret.Text = "0";
            checkedListBoxSepet.Items.Clear();
            panelKullaniciEkrani.BringToFront();
        }

        //seçilen ürünleri sepetten çıkarır
        private void btnSepettenCikar_Click(object sender, EventArgs e)
        {
            if(checkedListBoxSepet.CheckedItems.Count > 0)
            {
                for (int i = checkedListBoxSepet.CheckedItems.Count-1; i >= 0; i--)
                {
                    int ucret = Convert.ToInt32(checkedListBoxSepet.CheckedItems[i].ToString().Split(' ')[checkedListBoxSepet.CheckedItems[i].ToString().Split(' ').Length-2]);
                    txtRestoranIciToplamUcret.Text = (Convert.ToInt32(txtRestoranIciToplamUcret.Text) - ucret).ToString();
                    checkedListBoxSepet.Items.Remove(checkedListBoxSepet.CheckedItems[i]);
                }
                for (int i = 0; i < checkedListBoxSepet.Items.Count; i++)
                    checkedListBoxSepet.SetItemChecked(i,false);
            }
        }

        //sipariş verildiği zaman sepetteki menülerin adetlerini bulur
        void FindCount(string menu)
        {
            if (orderMenus.ContainsKey(menu))
                return;
            int count = 0;
            for (int i = 0; i < checkedListBoxSepet.Items.Count; i++)
                if (checkedListBoxSepet.Items[i].Equals(menu))
                    count++;
            orderMenus.Add(menu, count);
            orderMenusNames.Add(menu);
        }

        //restoranlar nesnelerinden seçilenrestoranı bulur
        //seçilen restoranların menülerinden sepette olanları tek tek bulur ve sipariş verir(veritabanına ekler - orders tablosuna)
        private void btnSiparisVer_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < checkedListBoxSepet.Items.Count; i++)
                FindCount(checkedListBoxSepet.Items[i].ToString());
            bool inserted = false;
            for (int i = 0; i < restaurants.Count; i++)
            {
                if (restaurants[i].id == restaurantID)
                {
                    for (int k = 0; k < orderMenusNames.Count; k++)
                    {
                        for (int j = 0; j < restaurants[i].menus.Count; j++)
                        {
                            string menu = orderMenusNames[k].ToString().Split(' ')[0] + " " + orderMenusNames[k].ToString().Split(' ')[1];
                            if (restaurants[i].menus[j].name == menu && restaurants[i].menus[j].price == Convert.ToInt32(orderMenusNames[k].ToString().Split(' ')[orderMenusNames[k].ToString().Split(' ').Length-2]))
                            {
                                RunSQLcommand("INSERT into orders(kullanici_tc,restoran_id,menu_id,adet,siparis_durumu) VALUES(" + user.tcID + "," + restaurantID + ",'" + restaurants[i].menus[j].id + "'," + orderMenus[checkedListBoxSepet.Items[k].ToString()] + ",'Preparing')");
                                inserted = true;
                            }
                        }
                    }
                }
            }
            if(inserted)
            {
                checkedListBoxSepet.Items.Clear();
                txtRestoranIciToplamUcret.Text = "0";
                MessageBox.Show("Order created.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        //kullanıcıdan çıkış yapıp giriş ekranına geçer
        private void btnKullaniciCikis_Click(object sender, EventArgs e)
        {
            panelGirisEkrani.BringToFront();
        }

        //datagridview dan seçilen menünün bilgilerini günceller(textboxlara yazılan verilere göre)
        private void btnRestoranMenuGuncelle_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(txtRestoranGuncelleMenuAdi.Text) || String.IsNullOrEmpty(txtRestoranGuncelleMenuFiyati.Text))
            {
                MessageBox.Show("Don't leave blank component.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            RunSQLcommand("UPDATE menus SET ad='" + txtRestoranGuncelleMenuAdi.Text + "',fiyat=" + txtRestoranGuncelleMenuFiyati.Text + " WHERE restoran_id=" + restaurantID + " and menu_id=" + selectedMenuID + "");
            dataGridViewRestoranMenuler.DataSource = "";
            SQLiteDataAdapter da1 = new SQLiteDataAdapter("SELECT menu_id,ad,fiyat FROM menus where restoran_id=" + restaurantID + "", con);
            using (DataSet ds = new DataSet())
            {
                da1.Fill(ds);
                dataGridViewRestoranMenuler.DataSource = ds.Tables[0];
            }
            RestaurantMenuDataGrid();
        }
    }
}
