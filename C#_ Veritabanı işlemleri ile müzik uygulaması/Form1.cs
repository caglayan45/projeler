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

namespace Spotify
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        string girisTuru = "";
        string[] kolonAdlari;
        string girenKullaniciID = "";
        string girenKullaniciUlkeID = "";
        string oynatilanSarkiID = "";

        Size kucukEkran = new Size(423, 327);
        Size buyukEkran = new Size(967, 623);

        SqlConnection con = new SqlConnection("Server=localhost; Database=spotify; Integrated Security=True;");

        void PanelGecis(Panel ekran, Size boyut)
        {
            this.Size = boyut;
            ekran.BringToFront();
        }

        string Top10UlkeListbox(string selectCumlesi)
        {
            con.Close();
            string veri = "";
            try
            {
                con.Open();
                SqlCommand cmd = new SqlCommand(selectCumlesi, con);
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    veri = dr["ulke_adi"].ToString() + " - " + dr["sarki_adi"].ToString() + " - " + dr["sure"].ToString();
                    con.Close();
                }
                con.Close();
                return veri;
            }
            catch (Exception e)
            {
                MessageBox.Show("Hata : " + e.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                con.Close();
            }
            con.Close();
            return null;
        }

        string TopListeler(string selectCumlesi)
        {
            con.Close();
            string veri = "";
            try
            {
                con.Open();
                SqlCommand cmd = new SqlCommand(selectCumlesi, con);
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    veri = dr["sarki_adi"].ToString() + " - " + dr["sure"].ToString();
                    con.Close();
                }
                con.Close();
                return veri;
            }
            catch (Exception e)
            {
                MessageBox.Show("Hata : " + e.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                con.Close();
            }
            con.Close();
            return null;
        }

        void TopListeleriVericekme()
        {
            con.Close();
            try
            {
                listBoxKullaniciListelerUlkeTop10.Items.Clear();
                listBoxKullaniciListelerTop10.Items.Clear();
                listBoxKullaniciListelerPopTop10.Items.Clear();
                listBoxKullaniciListelerJazzTop10.Items.Clear();
                listBoxKullaniciListelerKlasikTop10.Items.Clear();
                con.Open();
                SqlCommand cmd = new SqlCommand("select TOP 10 ulke_ID,sarki_ID,sum(dinlenme_sayisi) as 'dinlenme_sayisi' from UlkelerSarkilar GROUP BY ulke_ID, sarki_ID ORDER BY sum(dinlenme_sayisi) DESC", con);
                SqlDataReader dr = cmd.ExecuteReader();
                List<string> ulkeler = new List<string>(), sarkilar = new List<string>(), dinlenmeSayilari = new List<string>();
                while (dr.Read())
                {
                    ulkeler.Add(dr["ulke_ID"].ToString());
                    sarkilar.Add(dr["sarki_ID"].ToString());
                    dinlenmeSayilari.Add(dr["dinlenme_sayisi"].ToString());
                }
                con.Close();
                for (int i = 0; i < ulkeler.Count; i++)
                {
                    string veri = sarkilar[i] + " - " + dinlenmeSayilari[i] + " - " + Top10UlkeListbox("SELECT S.sarki_adi,U.ulke_adi,S.sure FROM Sarkilar as S, ulkeler as U, UlkelerSarkilar as US WHERE S.ID = US.sarki_ID and US.ulke_ID = U.ID and U.ID = " + ulkeler[i] + " and US.sarki_ID = " + sarkilar[i] + "");
                    listBoxKullaniciListelerUlkeTop10.Items.Add(veri);
                }
                for (int i = 1; i <= 3; i++)
                {
                    con.Open();
                    SqlCommand cmd1 = new SqlCommand("SELECT TOP 10 ID,tur_ID,sum(dinlenme_sayisi) as 'dinlenme_sayisi' FROM Sarkilar WHERE tur_ID = " + i + " GROUP BY tur_ID,ID  ORDER BY sum(dinlenme_sayisi) DESC", con);
                    SqlDataReader dr1 = cmd1.ExecuteReader();
                    sarkilar = new List<string>();
                    dinlenmeSayilari = new List<string>();
                    while (dr1.Read())
                    {
                        sarkilar.Add(dr1["ID"].ToString());
                        dinlenmeSayilari.Add(dr1["dinlenme_sayisi"].ToString());
                    }
                    con.Close();
                    for (int j = 0; j < sarkilar.Count; j++)
                    {
                        string veri = sarkilar[j] + " - " + dinlenmeSayilari[j] + " - " + TopListeler("SELECT sarki_adi,sure from Sarkilar WHERE ID = " + sarkilar[j] + "");
                        if (i == 1)
                            listBoxKullaniciListelerPopTop10.Items.Add(veri);
                        else if (i == 2)
                            listBoxKullaniciListelerJazzTop10.Items.Add(veri);
                        else
                            listBoxKullaniciListelerKlasikTop10.Items.Add(veri);
                    }
                }
                con.Open();
                cmd = new SqlCommand("SELECT TOP 10 ID,dinlenme_sayisi,sarki_adi,sure FROM Sarkilar ORDER BY dinlenme_sayisi DESC", con);
                dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    string veri = dr["ID"].ToString() + " - " + dr["dinlenme_sayisi"].ToString() + " - " + dr["sarki_adi"].ToString() + " - " + dr["sure"].ToString();
                    listBoxKullaniciListelerTop10.Items.Add(veri);
                }
                con.Close();
            }
            catch (Exception e)
            {
                MessageBox.Show("Hata : " + e.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                con.Close();
            }
            con.Close();
        }

        void HepsiniListeneEkle(string sarki_id, string tur)
        {
            con.Close();
            bool varMi = KayitVarmi("SELECT * FROM Sarkilar as S, CalmaListeleri as CL, CalmaListeleriSarkilari as CLS WHERE S.ID = CLS.sarki_ID and CLS.calma_listesi_ID = CL.ID and CL.kullanici_ID = " + girenKullaniciID + " and CLS.sarki_ID = " + sarki_id + "");
            if (!varMi)
            {
                string calmaListesiID = IDCekme("SELECT * FROM CalmaListeleri WHERE kullanici_ID = " + girenKullaniciID + " and tur_ID = " + tur + "", "ID");
                CRUDIslemleri("INSERT INTO CalmaListeleriSarkilari(calma_listesi_ID, sarki_ID) VALUES (" + calmaListesiID + "," + sarki_id + ")");
                if (tur.Equals("1"))
                    ListboxaVeriCekme(listBoxKullaniciPop, "SELECT S.ID,S.sarki_adi,S.sure FROM Sarkilar as S, CalmaListeleri as CL, CalmaListeleriSarkilari as CLS WHERE S.ID = CLS.sarki_ID and CL.ID = CLS.calma_listesi_ID and CL.kullanici_ID = " + girenKullaniciID + " and CL.tur_ID=" + tur + "");
                else if (tur.Equals("2"))
                    ListboxaVeriCekme(listBoxKullaniciJazz, "SELECT S.ID,S.sarki_adi,S.sure FROM Sarkilar as S, CalmaListeleri as CL, CalmaListeleriSarkilari as CLS WHERE S.ID = CLS.sarki_ID and CL.ID = CLS.calma_listesi_ID and CL.kullanici_ID = " + girenKullaniciID + " and CL.tur_ID=" + tur + "");
                else
                    ListboxaVeriCekme(listBoxKullaniciKlasik, "SELECT S.ID,S.sarki_adi,S.sure FROM Sarkilar as S, CalmaListeleri as CL, CalmaListeleriSarkilari as CLS WHERE S.ID = CLS.sarki_ID and CL.ID = CLS.calma_listesi_ID and CL.kullanici_ID = " + girenKullaniciID + " and CL.tur_ID=" + tur + "");
            }
        }

        void KendiListeneEkleme(ListBox listbx, MouseEventArgs e, string tur)
        {
            con.Close();
            int index = listbx.IndexFromPoint(e.Location);
            if (index != System.Windows.Forms.ListBox.NoMatches)
            {
                string sarki_id = listbx.SelectedItem.ToString().Split(' ')[0];
                bool varMi = KayitVarmi("SELECT * FROM Sarkilar as S, CalmaListeleri as CL, CalmaListeleriSarkilari as CLS WHERE S.ID = CLS.sarki_ID and CLS.calma_listesi_ID = CL.ID and CL.kullanici_ID = " + girenKullaniciID + " and CLS.sarki_ID = " + sarki_id + "");
                if (!varMi)
                {
                    if (tur.Equals("0"))
                    {
                        tur = IDCekme("SELECT tur_ID FROM Sarkilar WHERE ID=" + sarki_id + "", "tur_ID");
                    }
                    string calmaListesiID = IDCekme("SELECT * FROM CalmaListeleri WHERE kullanici_ID = " + girenKullaniciID + " and tur_ID = " + tur + "", "ID");
                    CRUDIslemleri("INSERT INTO CalmaListeleriSarkilari(calma_listesi_ID, sarki_ID) VALUES (" + calmaListesiID + "," + sarki_id + ")");
                    if(tur.Equals("1"))
                        ListboxaVeriCekme(listBoxKullaniciPop, "SELECT S.ID,S.sarki_adi,S.sure FROM Sarkilar as S, CalmaListeleri as CL, CalmaListeleriSarkilari as CLS WHERE S.ID = CLS.sarki_ID and CL.ID = CLS.calma_listesi_ID and CL.kullanici_ID = " + girenKullaniciID + " and CL.tur_ID=" + tur + "");
                    else if (tur.Equals("2"))
                        ListboxaVeriCekme(listBoxKullaniciJazz, "SELECT S.ID,S.sarki_adi,S.sure FROM Sarkilar as S, CalmaListeleri as CL, CalmaListeleriSarkilari as CLS WHERE S.ID = CLS.sarki_ID and CL.ID = CLS.calma_listesi_ID and CL.kullanici_ID = " + girenKullaniciID + " and CL.tur_ID=" + tur + "");
                    else
                        ListboxaVeriCekme(listBoxKullaniciKlasik, "SELECT S.ID,S.sarki_adi,S.sure FROM Sarkilar as S, CalmaListeleri as CL, CalmaListeleriSarkilari as CLS WHERE S.ID = CLS.sarki_ID and CL.ID = CLS.calma_listesi_ID and CL.kullanici_ID = " + girenKullaniciID + " and CL.tur_ID=" + tur + "");
                }
            }
        }

        bool KayitVarmi(string selectCumlesi)
        {
            con.Close();
            try
            {
                con.Open();
                SqlCommand cmd = new SqlCommand(selectCumlesi, con);
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    con.Close();
                    return true;
                }
                con.Close();
            }
            catch (Exception e)
            {
                MessageBox.Show("Hata : " + e.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                con.Close();
            }
            con.Close();
            return false;
        }

        void OynatmaVerisiCek(string sarki_ID)
        {
            con.Close();
            try
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("SELECT S.ID,S.sarki_adi,S.sure,S.dinlenme_sayisi,A.album_adi FROM Sarkilar as S,SarkiAlbum as SA, Albumler as A WHERE S.ID = SA.sarki_ID and A.ID = SA.album_ID and S.ID=" + sarki_ID + "", con);
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    oynatilanSarkiID = dr["ID"].ToString();
                    lblKullaniciOynatmaSarkiAdi.Text = dr["sarki_adi"].ToString();
                    lblKullaniciOynatmaSuresi.Text = dr["sure"].ToString();
                    lblKullaniciOynatmaAlbumu.Text = dr["album_adi"].ToString();
                    lblKullaniciOynatmaDinlenme.Text = dr["dinlenme_sayisi"].ToString();
                }
                con.Close();
            }
            catch (Exception e)
            {
                MessageBox.Show("Hata : " + e.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                con.Close();
            }
            con.Close();
        }

        void ComboBoxVerileriCekme(ComboBox cmbx, string selectCumlesi, string parametre)
        {
            con.Close();
            try
            {
                con.Open();
                cmbx.Items.Clear();
                SqlCommand cmd = new SqlCommand(selectCumlesi, con);
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    string tur = dr["ID"].ToString() + " - " + dr[parametre].ToString();
                    cmbx.Items.Add(tur);
                }
                cmbx.SelectedIndex = 0;
                con.Close();
            }
            catch(Exception e)
            {
                MessageBox.Show("Hata : " + e.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                con.Close();
            }
        }

        void DataGridViewVeriCekme(DataGridView dgv, string sorguCumlesi, params string[] kolonlar)
        {
            con.Close();
            try
            {
                dgv.DataSource = "";
                con.Open();
                SqlDataAdapter da = new SqlDataAdapter(sorguCumlesi, con);
                DataSet ds = new DataSet();
                da.Fill(ds);
                dgv.DataSource = ds.Tables[0];
                for (int i = 0; i < kolonlar.Length; i++)
                {
                    dgv.Columns[i].HeaderText = kolonlar[i];
                }
                con.Close();
            }
            catch (Exception e)
            {
                MessageBox.Show("Hata : " + e.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                con.Close();
            }
        }

        void CRUDIslemleri(string CRUD_Cumlesi)
        {
            con.Close();
            try
            {
                con.Open();
                SqlCommand sql = new SqlCommand(CRUD_Cumlesi, con);
                sql.ExecuteNonQuery();
                con.Close();
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
            finally
            {
                con.Close();
            }
        }

        void ListboxaVeriCekme(ListBox listbx, string selectCumlesi)
        {
            con.Close();
            try
            {
                listbx.Items.Clear();
                con.Open();
                SqlCommand cmd = new SqlCommand(selectCumlesi, con);
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    string tur = dr["ID"].ToString() + " - " + dr["sarki_adi"].ToString() + " - " + dr["sure"].ToString();
                    listbx.Items.Add(tur);
                }
                con.Close();
            }
            catch (Exception e)
            {
                MessageBox.Show("Hata : " + e.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                con.Close();
            }
            con.Close();
        }

        string IDCekme(string Select_Cumlesi, string parametre)
        {
            con.Close();
            string id = "-1";
            try
            {
                con.Open();
                SqlCommand cmd = new SqlCommand(Select_Cumlesi, con);
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    id = dr[parametre].ToString();
                }
                con.Close();
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
            finally
            {
                con.Close();
            }
            return id;
        }

        bool MailGecerliMi(string email)
        {
            try
            {
                var temp = new System.Net.Mail.MailAddress(email);
                return temp.Address == email;
            }
            catch
            {
                return false;
            }
        }

        private void btn_kayitOlGeri_Click(object sender, EventArgs e)
        {
            PanelGecis(panel_BaslangicEkrani, kucukEkran);
            txt_kayitOlKullaniciAdi.Text = txt_kayitOlMail.Text = txt_kayitOlSifre.Text = txt_kayitOlSifreTekrar.Text = "";
            comboBox_kayitOlUlkeler.SelectedIndex = 0;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            PanelGecis(panel_BaslangicEkrani, kucukEkran);
            comboBox_kayitOlUlkeler.SelectedIndex = 0;
        }

        private void btn_BaslangicYoneticiGirisi_Click(object sender, EventArgs e)
        {
            this.girisTuru = "Yonetici";
            PanelGecis(panel_GirisEkrani, kucukEkran);
        }

        private void btn_BaslangicKullaniciGirisi_Click(object sender, EventArgs e)
        {
            this.girisTuru = "Kullanici";
            PanelGecis(panel_GirisEkrani, kucukEkran);
        }

        private void btn_GirisGeri_Click(object sender, EventArgs e)
        {
            PanelGecis(panel_BaslangicEkrani, kucukEkran);
            txt_GirisKullaniciAdi.Text = txt_GirisSifre.Text = "";
        }

        private void btn_BaslangicKayitOl_Click(object sender, EventArgs e)
        {
            PanelGecis(panel_KayitEkrani, kucukEkran);
        }

        void SadeceHarfGirisi(KeyPressEventArgs e)
        {
            if (char.IsLetter(e.KeyChar) || e.KeyChar == '\b' || e.KeyChar == ' ')
                e.Handled = false;
            else
                e.Handled = true;
        }

        void SadeceRakamGirisi(KeyPressEventArgs e)
        {
            if (char.IsDigit(e.KeyChar) || e.KeyChar == '\b')
                e.Handled = false;
            else
                e.Handled = true;
        }

        private void btn_GirisGiris_Click(object sender, EventArgs e)
        {
            con.Close();
            if(String.IsNullOrEmpty(txt_GirisKullaniciAdi.Text) || String.IsNullOrEmpty(txt_GirisSifre.Text))
            {
                MessageBox.Show("Boş alan bırakmayınız.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                if (girisTuru.Equals("Yonetici"))
                {
                    try
                    {
                        con.Open();
                        SqlCommand cmd = new SqlCommand("SELECT * FROM Yoneticiler WHERE kullanici_adi='" + txt_GirisKullaniciAdi.Text + "' and sifre='" + txt_GirisSifre.Text + "'", con);
                        SqlDataReader dr = cmd.ExecuteReader();
                        if (dr.Read())
                            MessageBox.Show("Yönetici olarak oturum açıldı, Hoşgeldiniz.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        else
                            MessageBox.Show("Kullanıcı adı ya da şifre yanlış.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        con.Close();
                        PanelGecis(panelAdmin, buyukEkran);
                        
                        kolonAdlari = new string[] {"ID", "ŞARKI ADI", "SÜRESİ", "DİNLENME SAYISI", "TÜRÜ", "YAYINLANMA TARİHİ"};
                        DataGridViewVeriCekme(dataGridViewAdminSarkilar, "SELECT s.ID, s.sarki_adi, s.sure, s.dinlenme_sayisi, st.tur_adi, s.tarih FROM Sarkilar as s,turler as st WHERE s.tur_ID = st.ID", kolonAdlari);
                        kolonAdlari = new string[] { "ID", "ALBÜM ADI", "OLUŞTURULMA TARİHİ", "TÜRÜ" };
                        DataGridViewVeriCekme(dataGridViewAdminAlbumler, "SELECT a.ID, a.album_adi, a.olusturulma_tarihi, a_t.tur_adi FROM Albumler as a, turler as a_t WHERE a.tur_ID = a_t.ID", kolonAdlari);
                        kolonAdlari = new string[] { "ID", "SANATÇI ADI", "ÜLKESİ" };
                        DataGridViewVeriCekme(dataGridViewAdminSanatcilar, "SELECT s.ID, s.sanatci_adi, u.ulke_adi FROM Sanatcilar as s, ulkeler as u WHERE s.sanatci_ulkesi = u.ID", kolonAdlari);
                        ComboBoxVerileriCekme(comboBoxAdminSarkiTuru, "SELECT * FROM turler", "tur_adi");
                        comboBoxAdminSarkiEskiSanatci.Enabled = false;
                        ComboBoxVerileriCekme(comboBoxAdminSarkiYeniSanatci, "Select * from Sanatcilar", "sanatci_adi");
                        ComboBoxVerileriCekme(comboBoxAdminAlbumlerTuru, "SELECT * FROM turler", "tur_adi");
                        ComboBoxVerileriCekme(comboBoxAdminSarkiAlbum, "SELECT * FROM Albumler WHERE tur_ID = " + comboBoxAdminSarkiTuru.Items[0].ToString().Split(' ')[0] + "", "album_adi");
                    }
                    catch (Exception hata)
                    {
                        MessageBox.Show("Hata : " + hata, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        con.Close();
                    }
                }
                else if(girisTuru.Equals("Kullanici"))
                {
                    try
                    {
                        con.Open();
                        SqlCommand cmd = new SqlCommand("SELECT * FROM Kullanicilar WHERE kullanici_adi='" + txt_GirisKullaniciAdi.Text + "' and sifre='" + txt_GirisSifre.Text + "'", con);
                        SqlDataReader dr = cmd.ExecuteReader();
                        if (dr.Read())
                        {
                            MessageBox.Show("Kullanıcı olarak oturum açıldı, Hoşgeldiniz.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            PanelGecis(panel_Kullanici, buyukEkran);
                            tabControlKullanici.SelectedTab = tabControlKullanici.TabPages[0];
                            con.Close();
                            girenKullaniciID = IDCekme("SELECT ID FROM Kullanicilar WHERE kullanici_adi='" + txt_GirisKullaniciAdi.Text + "'", "ID");
                            girenKullaniciUlkeID = IDCekme("SELECT ulke FROM Kullanicilar WHERE kullanici_adi='" + txt_GirisKullaniciAdi.Text + "'", "ulke");
                        }
                        else
                            MessageBox.Show("Kullanıcı adı ya da şifre yanlış.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        con.Close();
                        if (!string.IsNullOrEmpty(girenKullaniciID))
                        {
                            lblKullaniciayarlarKullaniciAdi.Text = txt_GirisKullaniciAdi.Text;
                            ListboxaVeriCekme(listBoxKullaniciPop, "SELECT S.ID,S.sarki_adi,S.sure FROM Sarkilar as S, CalmaListeleri as CL, CalmaListeleriSarkilari as CLS WHERE S.ID = CLS.sarki_ID and CL.ID = CLS.calma_listesi_ID and CL.kullanici_ID = " + girenKullaniciID + " and CL.tur_ID=1");
                            ListboxaVeriCekme(listBoxKullaniciJazz, "SELECT S.ID,S.sarki_adi,S.sure FROM Sarkilar as S, CalmaListeleri as CL, CalmaListeleriSarkilari as CLS WHERE S.ID = CLS.sarki_ID and CL.ID = CLS.calma_listesi_ID and CL.kullanici_ID = " + girenKullaniciID + " and CL.tur_ID=2");
                            ListboxaVeriCekme(listBoxKullaniciKlasik, "SELECT S.ID,S.sarki_adi,S.sure FROM Sarkilar as S, CalmaListeleri as CL, CalmaListeleriSarkilari as CLS WHERE S.ID = CLS.sarki_ID and CL.ID = CLS.calma_listesi_ID and CL.kullanici_ID = " + girenKullaniciID + " and CL.tur_ID=3");
                            kolonAdlari = new string[] { "ID", "ALBÜM ADI", "OLUŞTURULMA TARİHİ", "TÜRÜ" };
                            DataGridViewVeriCekme(dataGridViewKullaniciAlbumler, "SELECT A.ID,A.album_adi,A.olusturulma_tarihi,T.tur_adi FROM Albumler as A, turler as T WHERE A.tur_ID = T.ID", kolonAdlari);
                            kolonAdlari = new string[] { "ID", "SANATÇI ADI", "ÜLKESİ" };
                            DataGridViewVeriCekme(dataGridViewKullaniciSanatcilar, "SELECT S.ID,S.sanatci_adi,U.ulke_adi FROM Sanatcilar as S, ulkeler as U WHERE S.sanatci_ulkesi = U.ID", kolonAdlari);
                            TopListeleriVericekme();
                            kolonAdlari = new string[] { "ID", "KULLANICI ADI", "ÜLKESİ" };
                            DataGridViewVeriCekme(dataGridViewKullaniciKullanicilarPremiumUyeler, "SELECT K.ID,K.kullanici_adi,U.ulke_adi FROM Kullanicilar as K, ulkeler as U WHERE K.ulke = U.ID and abonelik_turu=1", kolonAdlari);
                            DataGridViewVeriCekme(dataGridViewKullaniciKullanicilarTakipEdilenUyeler, "SELECT K.ID,K.kullanici_adi,U.ulke_adi FROM Kullanicilar as K, ulkeler as U WHERE K.ulke = U.ID and K.ID IN(SELECT takip_edilen_kullanici_ID FROM KullanicilarTakiplesme WHERE takip_eden_kullanici_ID = " + girenKullaniciID + ")", kolonAdlari);
                        }
                    }
                    catch (Exception hata)
                    {
                        MessageBox.Show("Hata : " + hata, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        con.Close();
                    }
                }
                txt_GirisKullaniciAdi.Text = txt_GirisSifre.Text = "";
            }
        }

        private void btn_kayitOl_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(txt_kayitOlKullaniciAdi.Text) || String.IsNullOrEmpty(txt_kayitOlMail.Text) || String.IsNullOrEmpty(txt_kayitOlSifre.Text) || String.IsNullOrEmpty(txt_kayitOlSifreTekrar.Text))
            {
                MessageBox.Show("Boş alan bırakmayınız.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                if (!MailGecerliMi(txt_kayitOlMail.Text))
                {
                    MessageBox.Show("Geçerli bir mail adresi giriniz.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (txt_kayitOlSifre.Text.Equals(txt_kayitOlSifreTekrar.Text))
                {
                    try
                    {
                        CRUDIslemleri("INSERT INTO Kullanicilar(kullanici_adi,e_mail,sifre,abonelik_turu,ulke,odeme_bilgisi) VALUES('" + txt_kayitOlKullaniciAdi.Text + "','" + txt_kayitOlMail.Text + "','" + txt_kayitOlSifre.Text + "','0'," + (Convert.ToInt32(comboBox_kayitOlUlkeler.SelectedIndex) + 1).ToString() + ",'0')");
                        string id = IDCekme("SELECT * FROM kullanicilar WHERE kullanici_adi='" + txt_kayitOlKullaniciAdi.Text + "'", "ID");
                        CRUDIslemleri("INSERT INTO CalmaListeleri(kullanici_ID,tur_ID) VALUES(" + id + ",1)");
                        CRUDIslemleri("INSERT INTO CalmaListeleri(kullanici_ID,tur_ID) VALUES(" + id + ",2)");
                        CRUDIslemleri("INSERT INTO CalmaListeleri(kullanici_ID,tur_ID) VALUES(" + id + ",3)");
                        MessageBox.Show("Kayıt başarılı, çalma listeleriniz oluşturuldu.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    catch (Exception hata)
                    {
                        if (String.Compare("Cannot insert duplicate key in object \'dbo.kullanicilar\'.", hata.ToString()) == -1)
                            MessageBox.Show("Kullanıcı adı kullanılıyor.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        else
                            MessageBox.Show("Hata : " + hata.ToString(), "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        con.Close();
                    }
                }
                else
                    MessageBox.Show("Şifreler aynı değil.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void radioButtonAdminSarkilarGuncelleme_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButtonAdminSarkilarGuncelleme.Checked)
            {
                btnAdminSarkiEkleGuncelle.Text = "GÜNCELLE";
                if(dataGridViewAdminSarkilar.CurrentRow != null)
                {
                    txtAdminSarkiID.Text = dataGridViewAdminSarkilar.CurrentRow.Cells[0].Value.ToString();
                    txtAdminSarkiAdi.Text = dataGridViewAdminSarkilar.CurrentRow.Cells[1].Value.ToString();
                    string sure = dataGridViewAdminSarkilar.CurrentRow.Cells[2].Value.ToString();
                    txtAdminSarkiSureSaat.Text = sure.Split(':')[0];
                    txtAdminSarkiSureDakika.Text = sure.Split(':')[1];
                    txtAdminSarkiSureSaniye.Text = sure.Split(':')[2];
                    txtAdminSarkiDinlenmeSayisi.Text = dataGridViewAdminSarkilar.CurrentRow.Cells[3].Value.ToString();
                    comboBoxAdminSarkiTuru.SelectedItem = dataGridViewAdminSarkilar.CurrentRow.Cells[4].Value.ToString();
                    ComboBoxVerileriCekme(comboBoxAdminSarkiEskiSanatci, "Select * from Sanatcilar WHERE ID IN(SELECT sanatci_ID FROM SarkiSanatci WHERE sarki_ID="+ txtAdminSarkiID.Text + ")", "sanatci_adi");
                    ComboBoxVerileriCekme(comboBoxAdminSarkiYeniSanatci, "Select * from Sanatcilar", "sanatci_adi");
                    comboBoxAdminSarkiYeniSanatci.SelectedItem = comboBoxAdminSarkiEskiSanatci.SelectedItem;
                    ComboBoxVerileriCekme(comboBoxAdminSarkiAlbum, "Select * from Albumler", "album_adi");
                    string tarih = dataGridViewAdminSarkilar.CurrentRow.Cells[5].Value.ToString().Split(' ')[0];
                    int gun = Convert.ToInt32(tarih.Split('.')[0]);
                    int ay = Convert.ToInt32(tarih.Split('.')[1]);
                    int yil = Convert.ToInt32(tarih.Split('.')[2]);
                    dateTimePickerAdminSarkiYayinlanmaTarihi.Value = new DateTime(yil, ay, gun);
                }
            }
        }

        private void dataGridViewAdminSarkilar_SelectionChanged(object sender, EventArgs e)
        {
            con.Close();
            if ((radioButtonAdminSarkilarGuncelleme.Checked || radioButtonAdminSarkilarSilme.Checked) && dataGridViewAdminSarkilar.CurrentRow != null)
            {
                txtAdminSarkiID.Text = dataGridViewAdminSarkilar.CurrentRow.Cells[0].Value.ToString();
                txtAdminSarkiAdi.Text = dataGridViewAdminSarkilar.CurrentRow.Cells[1].Value.ToString();
                string sure = dataGridViewAdminSarkilar.CurrentRow.Cells[2].Value.ToString();
                txtAdminSarkiSureSaat.Text = sure.Split(':')[0];
                txtAdminSarkiSureDakika.Text = sure.Split(':')[1];
                txtAdminSarkiSureSaniye.Text = sure.Split(':')[2];
                txtAdminSarkiDinlenmeSayisi.Text = dataGridViewAdminSarkilar.CurrentRow.Cells[3].Value.ToString();
                ComboBoxVerileriCekme(comboBoxAdminSarkiEskiSanatci, "Select * from Sanatcilar WHERE ID IN(SELECT sanatci_ID FROM SarkiSanatci WHERE sarki_ID=" + txtAdminSarkiID.Text + ")", "sanatci_adi");
                string id = IDCekme("SELECT * from SarkiAlbum where sarki_ID=" + txtAdminSarkiID.Text + "", "album_ID");
                for (int i = 0; i < comboBoxAdminSarkiAlbum.Items.Count; i++)
                {
                    if (comboBoxAdminSarkiAlbum.Items[i].ToString().Split(' ')[0].Equals(id))
                    {
                        comboBoxAdminSarkiAlbum.SelectedIndex = i;
                        break;
                    }
                }
                
                for (int i = 0; i < comboBoxAdminSarkiTuru.Items.Count; i++)
                {
                    if (comboBoxAdminSarkiTuru.Items[i].ToString().Contains(dataGridViewAdminSarkilar.CurrentRow.Cells[4].Value.ToString()))
                    {
                        comboBoxAdminSarkiTuru.SelectedIndex = i;
                        break;
                    }
                }
                string tarih = dataGridViewAdminSarkilar.CurrentRow.Cells[5].Value.ToString().Split(' ')[0];
                int gun = Convert.ToInt32(tarih.Split('.')[0]);
                int ay = Convert.ToInt32(tarih.Split('.')[1]);
                int yil = Convert.ToInt32(tarih.Split('.')[2]);
                dateTimePickerAdminSarkiYayinlanmaTarihi.Value = new DateTime(yil, ay, gun);
            }
        }

        private void txtAdminSarkiAdi_KeyPress(object sender, KeyPressEventArgs e)
        {
            SadeceHarfGirisi(e);
        }

        private void btnAdminSarkiEkleGuncelle_Click(object sender, EventArgs e)
        {
            if (radioButtonAdminSarkilarSilme.Checked)
            {
                DialogResult dr = MessageBox.Show(txtAdminSarkiID.Text + " idsi olan şarkıyı silmek istediğinize emin misiniz?(Tüm listelerden silinecek)", "Uyarı", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dr == DialogResult.Yes)
                {
                    CRUDIslemleri("DELETE FROM Sarkilar WHERE ID=" + txtAdminSarkiID.Text + "");
                    txtAdminSarkiID.Text = txtAdminSarkiAdi.Text = txtAdminSarkiDinlenmeSayisi.Text = txtAdminSarkiSureSaat.Text = txtAdminSarkiSureSaniye.Text = txtAdminSarkiSureDakika.Text = "";
                    comboBoxAdminSarkiTuru.SelectedIndex = 0;
                    dateTimePickerAdminSarkiYayinlanmaTarihi.Value = DateTime.Today;
                    kolonAdlari = new string[] { "ID", "ŞARKI ADI", "SÜRESİ", "DİNLENME SAYISI", "TÜRÜ", "YAYINLANMA TARİHİ" };
                    DataGridViewVeriCekme(dataGridViewAdminSarkilar, "SELECT s.ID, s.sarki_adi, s.sure, s.dinlenme_sayisi, st.tur_adi, s.tarih FROM Sarkilar as s,turler as st WHERE s.tur_ID = st.ID", kolonAdlari);
                }
                return;
            }

            if (string.IsNullOrEmpty(txtAdminSarkiAdi.Text) || string.IsNullOrEmpty(txtAdminSarkiSureSaat.Text) || string.IsNullOrEmpty(txtAdminSarkiDinlenmeSayisi.Text) || string.IsNullOrEmpty(txtAdminSarkiSureDakika.Text) || string.IsNullOrEmpty(txtAdminSarkiSureSaniye.Text))
            {
                MessageBox.Show("Boş alan bırakmayınız.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if(Convert.ToInt32(txtAdminSarkiSureDakika.Text) > 59 || Convert.ToInt32(txtAdminSarkiSureSaniye.Text) > 59)
            {
                MessageBox.Show("Geçerli saat giriniz.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            string tarih = dateTimePickerAdminSarkiYayinlanmaTarihi.Value.ToString();
            tarih = tarih.Split(' ')[0];
            tarih = tarih.Split('.')[2] + "-" + tarih.Split('.')[1] + "-" + tarih.Split('.')[0] + " 00:00:00.000";
            string sure = txtAdminSarkiSureSaat.Text + ":" + txtAdminSarkiSureDakika.Text + ":" + txtAdminSarkiSureSaniye.Text;
            if (radioButtonAdminSarkilarEkleme.Checked)
            {
                CRUDIslemleri("INSERT INTO Sarkilar(sarki_adi,sure,dinlenme_sayisi,tur_ID,tarih) VALUES('" + txtAdminSarkiAdi.Text + "','" + sure + "'," + txtAdminSarkiDinlenmeSayisi.Text + "," + comboBoxAdminSarkiTuru.SelectedItem.ToString().Split(' ')[0] + ",'" + tarih + "')");
                string id = IDCekme("SELECT * FROM sarkilar where sarki_adi='" + txtAdminSarkiAdi.Text + "'", "ID");
                CRUDIslemleri("INSERT INTO SarkiSanatci(sarki_ID,sanatci_ID) VALUES(" + id + "," + comboBoxAdminSarkiYeniSanatci.SelectedItem.ToString().Split(' ')[0] + ")");
                CRUDIslemleri("INSERT INTO SarkiAlbum(sarki_ID,album_ID) VALUES(" + id + "," + comboBoxAdminSarkiAlbum.SelectedItem.ToString().Split(' ')[0] + ")");
                txtAdminSarkiID.Text = txtAdminSarkiAdi.Text = txtAdminSarkiDinlenmeSayisi.Text = txtAdminSarkiSureSaat.Text = txtAdminSarkiSureSaniye.Text = txtAdminSarkiSureDakika.Text = "";
                comboBoxAdminSarkiTuru.SelectedIndex = 0;
                dateTimePickerAdminSarkiYayinlanmaTarihi.Value = DateTime.Today;
            }
            else
            {
                CRUDIslemleri("UPDATE Sarkilar SET sarki_adi='" + txtAdminSarkiAdi.Text + "', sure='" + sure + "', dinlenme_sayisi=" + txtAdminSarkiDinlenmeSayisi.Text + ", tur_ID=" + comboBoxAdminSarkiTuru.SelectedItem.ToString().Split(' ')[0] + ", tarih='" + tarih + "' WHERE ID=" + txtAdminSarkiID.Text + "");
                string id = IDCekme("SELECT * FROM sarkilar where sarki_adi='" + txtAdminSarkiAdi.Text + "'", "ID");                CRUDIslemleri("UPDATE SarkiSanatci SET sanatci_ID=" + comboBoxAdminSarkiYeniSanatci.SelectedItem.ToString().Split(' ')[0] + " WHERE sarki_ID =" + id + " and sanatci_ID=" + comboBoxAdminSarkiEskiSanatci.SelectedItem.ToString().Split(' ')[0] + "");
                CRUDIslemleri("UPDATE SarkiAlbum SET album_ID=" + comboBoxAdminSarkiAlbum.SelectedItem.ToString().Split(' ')[0] + " WHERE sarki_ID =" + id + "");
            }
            kolonAdlari = new string[] { "ID", "ŞARKI ADI", "SÜRESİ", "DİNLENME SAYISI", "TÜRÜ", "YAYINLANMA TARİHİ" };
            DataGridViewVeriCekme(dataGridViewAdminSarkilar, "SELECT s.ID, s.sarki_adi, s.sure, s.dinlenme_sayisi, st.tur_adi, s.tarih FROM Sarkilar as s,turler as st WHERE s.tur_ID = st.ID", kolonAdlari);
        }

        private void txtAdminSarkiDinlenmeSayisi_KeyPress(object sender, KeyPressEventArgs e)
        {
            SadeceRakamGirisi(e);
        }

        private void txtAdminSarkiSureSaat_KeyPress(object sender, KeyPressEventArgs e)
        {
            SadeceRakamGirisi(e);
        }

        private void txtAdminSarkiSureDakika_KeyPress(object sender, KeyPressEventArgs e)
        {
            SadeceRakamGirisi(e);
        }

        private void txtAdminSarkiSureSaniye_KeyPress(object sender, KeyPressEventArgs e)
        {
            SadeceRakamGirisi(e);
        }

        private void radioButtonAdminSarkilarEkleme_CheckedChanged(object sender, EventArgs e)
        {
            if(radioButtonAdminSarkilarEkleme.Checked)
            {
                comboBoxAdminSarkiEskiSanatci.Enabled = false;
                btnAdminSarkiEkleGuncelle.Text = "EKLE";
                txtAdminSarkiID.Text = txtAdminSarkiAdi.Text = txtAdminSarkiSureSaat.Text = txtAdminSarkiSureSaniye.Text = txtAdminSarkiSureDakika.Text = txtAdminSarkiDinlenmeSayisi.Text = "";
                comboBoxAdminSarkiTuru.SelectedIndex = 0;
                dateTimePickerAdminSarkiYayinlanmaTarihi.Value = DateTime.Today;
                ComboBoxVerileriCekme(comboBoxAdminSarkiYeniSanatci, "Select * from Sanatcilar", "sanatci_adi");
            }
            else
                comboBoxAdminSarkiEskiSanatci.Enabled = true;
        }

        private void radioButtonAdminSarkilarSilme_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButtonAdminSarkilarSilme.Checked)
            {
                comboBoxAdminSarkiEskiSanatci.Enabled = comboBoxAdminSarkiYeniSanatci.Enabled = comboBoxAdminSarkiAlbum.Enabled = false;
                btnAdminSarkiEkleGuncelle.Text = "SİL";
                txtAdminSarkiID.Text = dataGridViewAdminSarkilar.CurrentRow.Cells[0].Value.ToString();
                txtAdminSarkiAdi.Text = dataGridViewAdminSarkilar.CurrentRow.Cells[1].Value.ToString();
                string sure = dataGridViewAdminSarkilar.CurrentRow.Cells[2].Value.ToString();
                txtAdminSarkiSureSaat.Text = sure.Split(':')[0];
                txtAdminSarkiSureDakika.Text = sure.Split(':')[1];
                txtAdminSarkiSureSaniye.Text = sure.Split(':')[2];
                txtAdminSarkiDinlenmeSayisi.Text = dataGridViewAdminSarkilar.CurrentRow.Cells[3].Value.ToString();
                comboBoxAdminSarkiTuru.SelectedItem = dataGridViewAdminSarkilar.CurrentRow.Cells[4].Value.ToString();
                string tarih = dataGridViewAdminSarkilar.CurrentRow.Cells[5].Value.ToString().Split(' ')[0];
                int gun = Convert.ToInt32(tarih.Split('.')[0]);
                int ay = Convert.ToInt32(tarih.Split('.')[1]);
                int yil = Convert.ToInt32(tarih.Split('.')[2]);
                dateTimePickerAdminSarkiYayinlanmaTarihi.Value = new DateTime(yil, ay, gun);
            }
            else
            {
                comboBoxAdminSarkiEskiSanatci.Enabled = comboBoxAdminSarkiYeniSanatci.Enabled = comboBoxAdminSarkiAlbum.Enabled = true;
            }
        }

        private void radioButtonAdminAlbumlerEkleme_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButtonAdminAlbumlerEkleme.Checked)
            {
                dateTimePickerAdminAlbumlerTarih.Value = DateTime.Today;
                txtAdminAlbumlerAdi.Text = txtAdminAlbumlerID.Text = "";
                btnAdminAlbumlerEkle.Text = "EKLE";
                ComboBoxVerileriCekme(comboBoxAdminAlbumlerTuru, "Select * from turler", "tur_adi");
            }
        }

        private void radioButtonAdminAlbumlerGuncelleme_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButtonAdminAlbumlerGuncelleme.Checked)
            {
                btnAdminAlbumlerEkle.Text = "GÜNCELLE";
                if (dataGridViewAdminAlbumler.CurrentRow != null)
                {
                    txtAdminAlbumlerID.Text = dataGridViewAdminAlbumler.CurrentRow.Cells[0].Value.ToString();
                    txtAdminAlbumlerAdi.Text = dataGridViewAdminAlbumler.CurrentRow.Cells[1].Value.ToString();
                    string tarih = dataGridViewAdminAlbumler.CurrentRow.Cells[2].Value.ToString().Split(' ')[0];
                    int gun = Convert.ToInt32(tarih.Split('.')[0]);
                    int ay = Convert.ToInt32(tarih.Split('.')[1]);
                    int yil = Convert.ToInt32(tarih.Split('.')[2]);
                    dateTimePickerAdminAlbumlerTarih.Value = new DateTime(yil, ay, gun);
                    for (int i = 0; i < comboBoxAdminAlbumlerTuru.Items.Count; i++)
                    {
                        if (comboBoxAdminAlbumlerTuru.Items[i].ToString().Contains(dataGridViewAdminAlbumler.CurrentRow.Cells[3].Value.ToString()))
                        {
                            comboBoxAdminAlbumlerTuru.SelectedIndex = i;
                            break;
                        }
                    }
                }
            }
        }

        private void radioButtonAdminAlbumlerSilme_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButtonAdminAlbumlerSilme.Checked)
            {
                comboBoxAdminAlbumlerTuru.Enabled = txtAdminAlbumlerAdi.Enabled = dateTimePickerAdminAlbumlerTarih.Enabled = false;
                btnAdminAlbumlerEkle.Text = "SİL";
                if (dataGridViewAdminAlbumler.CurrentRow != null)
                {
                    txtAdminAlbumlerID.Text = dataGridViewAdminAlbumler.CurrentRow.Cells[0].Value.ToString();
                    txtAdminAlbumlerAdi.Text = dataGridViewAdminAlbumler.CurrentRow.Cells[1].Value.ToString();
                    string tarih = dataGridViewAdminAlbumler.CurrentRow.Cells[2].Value.ToString().Split(' ')[0];
                    int gun = Convert.ToInt32(tarih.Split('.')[0]);
                    int ay = Convert.ToInt32(tarih.Split('.')[1]);
                    int yil = Convert.ToInt32(tarih.Split('.')[2]);
                    dateTimePickerAdminAlbumlerTarih.Value = new DateTime(yil, ay, gun);
                    for (int i = 0; i < comboBoxAdminAlbumlerTuru.Items.Count; i++)
                    {
                        if (comboBoxAdminAlbumlerTuru.Items[i].ToString().Contains(dataGridViewAdminAlbumler.CurrentRow.Cells[3].Value.ToString()))
                        {
                            comboBoxAdminAlbumlerTuru.SelectedIndex = i;
                            break;
                        }
                    }
                }
            }
            else
            {
                comboBoxAdminAlbumlerTuru.Enabled = txtAdminAlbumlerAdi.Enabled = dateTimePickerAdminAlbumlerTarih.Enabled = true;
            }
        }

        private void dataGridViewAdminAlbumler_SelectionChanged(object sender, EventArgs e)
        {
            con.Close();
            if ((radioButtonAdminAlbumlerSilme.Checked || radioButtonAdminAlbumlerGuncelleme.Checked) && dataGridViewAdminAlbumler.CurrentRow != null)
            {
                txtAdminAlbumlerID.Text = dataGridViewAdminAlbumler.CurrentRow.Cells[0].Value.ToString();
                txtAdminAlbumlerAdi.Text = dataGridViewAdminAlbumler.CurrentRow.Cells[1].Value.ToString();
                string tarih = dataGridViewAdminAlbumler.CurrentRow.Cells[2].Value.ToString().Split(' ')[0];
                int gun = Convert.ToInt32(tarih.Split('.')[0]);
                int ay = Convert.ToInt32(tarih.Split('.')[1]);
                int yil = Convert.ToInt32(tarih.Split('.')[2]);
                dateTimePickerAdminAlbumlerTarih.Value = new DateTime(yil, ay, gun);
                for (int i = 0; i < comboBoxAdminAlbumlerTuru.Items.Count; i++)
                {
                    if (comboBoxAdminAlbumlerTuru.Items[i].ToString().Contains(dataGridViewAdminAlbumler.CurrentRow.Cells[3].Value.ToString()))
                    {
                        comboBoxAdminAlbumlerTuru.SelectedIndex = i;
                        break;
                    }
                }
            }
        }

        private void btnAdminAlbumlerEkle_Click(object sender, EventArgs e)
        {
            if (radioButtonAdminAlbumlerSilme.Checked)
            {
                DialogResult dr = MessageBox.Show(txtAdminAlbumlerID.Text + " idsi olan albümü silmek istediğinize emin misiniz?(Tüm listelerden silinecek)", "Uyarı", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dr == DialogResult.Yes)
                {
                    CRUDIslemleri("DELETE FROM Albumler WHERE ID=" + txtAdminAlbumlerID.Text + "");
                    txtAdminAlbumlerID.Text = txtAdminSarkiAdi.Text = "";
                    comboBoxAdminAlbumlerTuru.SelectedIndex = 0;
                    dateTimePickerAdminAlbumlerTarih.Value = DateTime.Today;
                    kolonAdlari = new string[] { "ID", "ALBÜM ADI", "OLUŞTURULMA TARİHİ", "TÜRÜ" };
                    DataGridViewVeriCekme(dataGridViewAdminAlbumler, "SELECT a.ID, a.album_adi, a.olusturulma_tarihi, a_t.tur_adi FROM Albumler as a, turler as a_t WHERE a.tur_ID = a_t.ID", kolonAdlari);
                    ComboBoxVerileriCekme(comboBoxAdminSarkiAlbum, "SELECT * FROM Albumler", "album_adi");
                }
                return;
            }

            if (string.IsNullOrEmpty(txtAdminAlbumlerAdi.Text))
            {
                MessageBox.Show("Boş alan bırakmayınız.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            string tarih = dateTimePickerAdminSarkiYayinlanmaTarihi.Value.ToString();
            tarih = tarih.Split(' ')[0];
            tarih = tarih.Split('.')[2] + "-" + tarih.Split('.')[1] + "-" + tarih.Split('.')[0] + " 00:00:00.000";
           
            if (radioButtonAdminAlbumlerEkleme.Checked)
            {
                CRUDIslemleri("INSERT INTO Albumler(album_adi,olusturulma_tarihi,tur_ID) VALUES('" + txtAdminAlbumlerAdi.Text + "','" + tarih + "'," + comboBoxAdminAlbumlerTuru.SelectedItem.ToString().Split(' ')[0] + ")");
                txtAdminAlbumlerAdi.Text = txtAdminAlbumlerID.Text = "";
                comboBoxAdminAlbumlerTuru.SelectedIndex = 0;
                dateTimePickerAdminSarkiYayinlanmaTarihi.Value = DateTime.Today;
            }
            else
            {
                CRUDIslemleri("UPDATE Albumler SET album_adi='" + txtAdminAlbumlerAdi.Text + "', olusturulma_tarihi='" + tarih + "', tur_ID=" + comboBoxAdminAlbumlerTuru.SelectedItem.ToString().Split(' ')[0] + " WHERE ID=" + txtAdminAlbumlerID.Text + "");
            }
            kolonAdlari = new string[] { "ID", "ALBÜM ADI", "OLUŞTURULMA TARİHİ", "TÜRÜ" };
            DataGridViewVeriCekme(dataGridViewAdminAlbumler, "SELECT a.ID, a.album_adi, a.olusturulma_tarihi, a_t.tur_adi FROM Albumler as a, turler as a_t WHERE a.tur_ID = a_t.ID", kolonAdlari);
            ComboBoxVerileriCekme(comboBoxAdminSarkiAlbum, "SELECT * FROM Albumler", "album_adi");
        }

        private void radioButtonAdminSanatcilarEkleme_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButtonAdminSanatcilarEkleme.Checked)
            {
                txtAdminSanatcilarAdi.Text = txtAdminSanatcilarID.Text = "";
                btnAdminSanatcilarEkle.Text = "EKLE";
                comboBoxAdminSanatcilarUlkesi.SelectedIndex = 0;
            }
        }

        private void radioButtonAdminSanatcilarGuncelleme_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButtonAdminSanatcilarGuncelleme.Checked)
            {
                btnAdminSanatcilarEkle.Text = "GÜNCELLE";
                if (dataGridViewAdminSanatcilar.CurrentRow != null)
                {
                    txtAdminSanatcilarID.Text = dataGridViewAdminSanatcilar.CurrentRow.Cells[0].Value.ToString();
                    txtAdminSanatcilarAdi.Text = dataGridViewAdminSanatcilar.CurrentRow.Cells[1].Value.ToString();
                    for (int i = 0; i < comboBoxAdminSanatcilarUlkesi.Items.Count; i++)
                    {
                        if (comboBoxAdminSanatcilarUlkesi.Items[i].ToString().Contains(dataGridViewAdminSanatcilar.CurrentRow.Cells[2].Value.ToString()))
                        {
                            comboBoxAdminSanatcilarUlkesi.SelectedIndex = i;
                            break;
                        }
                    }
                }
            }
        }

        private void radioButtonAdminSanatcilarSilme_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButtonAdminSanatcilarSilme.Checked)
            {
                comboBoxAdminSanatcilarUlkesi.Enabled = txtAdminSanatcilarAdi.Enabled = false;
                btnAdminSanatcilarEkle.Text = "SİL";
                if (dataGridViewAdminSanatcilar.CurrentRow != null)
                {
                    txtAdminSanatcilarID.Text = dataGridViewAdminSanatcilar.CurrentRow.Cells[0].Value.ToString();
                    txtAdminSanatcilarAdi.Text = dataGridViewAdminSanatcilar.CurrentRow.Cells[1].Value.ToString();
                    for (int i = 0; i < comboBoxAdminSanatcilarUlkesi.Items.Count; i++)
                    {
                        if (comboBoxAdminSanatcilarUlkesi.Items[i].ToString().Contains(dataGridViewAdminSanatcilar.CurrentRow.Cells[2].Value.ToString()))
                        {
                            comboBoxAdminSanatcilarUlkesi.SelectedIndex = i;
                            break;
                        }
                    }
                }
            }
            else
            {
                comboBoxAdminSanatcilarUlkesi.Enabled = txtAdminSanatcilarAdi.Enabled = txtAdminSanatcilarID.Enabled = true;
            }
        }

        private void btnAdminSanatcilarEkle_Click(object sender, EventArgs e)
        {
            if (radioButtonAdminSanatcilarSilme.Checked)
            {
                DialogResult dr = MessageBox.Show(txtAdminSanatcilarID.Text + " idsi olan sanatçıyı silmek istediğinize emin misiniz?(Tüm listelerden silinecek)", "Uyarı", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dr == DialogResult.Yes)
                {
                    CRUDIslemleri("DELETE FROM Sanatcilar WHERE ID=" + txtAdminSanatcilarID.Text + "");
                    txtAdminSanatcilarID.Text = txtAdminSanatcilarAdi.Text = "";
                    comboBoxAdminSanatcilarUlkesi.SelectedIndex = 0;
                    kolonAdlari = new string[] { "ID", "SANATÇI ADI", "ÜLKESİ" };
                    DataGridViewVeriCekme(dataGridViewAdminSanatcilar, "SELECT s.ID, s.sanatci_adi, u.ulke_adi FROM Sanatcilar as s, ulkeler as u WHERE s.sanatci_ulkesi = u.ID", kolonAdlari);
                    ComboBoxVerileriCekme(comboBoxAdminSarkiYeniSanatci, "Select * from Sanatcilar", "sanatci_adi");
                    ComboBoxVerileriCekme(comboBoxAdminSarkiEskiSanatci, "Select * from Sanatcilar", "sanatci_adi");
                }
                return;
            }

            if (string.IsNullOrEmpty(txtAdminSanatcilarAdi.Text))
            {
                MessageBox.Show("Boş alan bırakmayınız.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (radioButtonAdminSanatcilarEkleme.Checked)
            {
                CRUDIslemleri("INSERT INTO Sanatcilar(sanatci_adi,sanatci_ulkesi) VALUES('" + txtAdminSanatcilarAdi.Text + "'," + (Convert.ToInt32(comboBoxAdminSanatcilarUlkesi.SelectedIndex) + 1).ToString() + ")");
                txtAdminSanatcilarAdi.Text = txtAdminSanatcilarID.Text = "";
                comboBoxAdminSanatcilarUlkesi.SelectedIndex = 0;
            }
            else
            {
                CRUDIslemleri("UPDATE Sanatcilar SET sanatci_adi='" + txtAdminSanatcilarAdi.Text + "', sanatci_ulkesi=" + (Convert.ToInt32(comboBoxAdminSanatcilarUlkesi.SelectedIndex) + 1).ToString() + " WHERE ID=" + txtAdminSanatcilarID.Text + "");
            }
            kolonAdlari = new string[] { "ID", "SANATÇI ADI", "ÜLKESİ" };
            DataGridViewVeriCekme(dataGridViewAdminSanatcilar, "SELECT s.ID, s.sanatci_adi, u.ulke_adi FROM Sanatcilar as s, ulkeler as u WHERE s.sanatci_ulkesi = u.ID", kolonAdlari);
            ComboBoxVerileriCekme(comboBoxAdminSarkiYeniSanatci, "Select * from Sanatcilar", "sanatci_adi");
            ComboBoxVerileriCekme(comboBoxAdminSarkiEskiSanatci, "Select * from Sanatcilar", "sanatci_adi");
        }

        private void dataGridViewAdminSanatcilar_SelectionChanged(object sender, EventArgs e)
        {
            con.Close();
            if ((radioButtonAdminSanatcilarGuncelleme.Checked || radioButtonAdminSanatcilarSilme.Checked) && dataGridViewAdminSanatcilar.CurrentRow != null)
            {
                txtAdminSanatcilarID.Text = dataGridViewAdminSanatcilar.CurrentRow.Cells[0].Value.ToString();
                txtAdminSanatcilarAdi.Text = dataGridViewAdminSanatcilar.CurrentRow.Cells[1].Value.ToString();
                for (int i = 0; i < comboBoxAdminSanatcilarUlkesi.Items.Count; i++)
                {
                    if (comboBoxAdminSanatcilarUlkesi.Items[i].ToString().Contains(dataGridViewAdminSanatcilar.CurrentRow.Cells[2].Value.ToString()))
                    {
                        comboBoxAdminSanatcilarUlkesi.SelectedIndex = i;
                        break;
                    }
                }
            }
        }

        private void btnKullaniciOynatma_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(oynatilanSarkiID))
            {
                int sayi = Convert.ToInt32(lblKullaniciOynatmaDinlenme.Text) + 1;
                CRUDIslemleri("UPDATE Sarkilar SET dinlenme_sayisi=" + sayi + " WHERE ID=" + oynatilanSarkiID + "");
                lblKullaniciOynatmaDinlenme.Text = sayi.ToString();
                bool varMi = KayitVarmi("SELECT * FROM UlkelerSarkilar WHERE sarki_ID=" + oynatilanSarkiID + "  and ulke_ID=" + girenKullaniciUlkeID + "");
                if (varMi)
                {
                    CRUDIslemleri("UPDATE UlkelerSarkilar SET dinlenme_sayisi=dinlenme_sayisi+1 WHERE sarki_ID=" + oynatilanSarkiID + "  and ulke_ID=" + girenKullaniciUlkeID + "");
                }
                else
                {
                    CRUDIslemleri("INSERT INTO UlkelerSarkilar(sarki_ID,ulke_ID,dinlenme_sayisi) VALUES(" + oynatilanSarkiID + "," + girenKullaniciUlkeID + ",1)");
                }
                TopListeleriVericekme();
            }
        }

        private void dataGridViewKullaniciAlbumler_SelectionChanged(object sender, EventArgs e)
        {
            con.Close();
            if (dataGridViewKullaniciAlbumler.CurrentRow != null)
            {
                listBoxKullaniciAlbumlerPop.Items.Clear();
                listBoxKullaniciAlbumlerJazz.Items.Clear();
                listBoxKullaniciAlbumlerKlasik.Items.Clear();
                string id = dataGridViewKullaniciAlbumler.CurrentRow.Cells[0].Value.ToString();
                string tur = dataGridViewKullaniciAlbumler.CurrentRow.Cells[3].Value.ToString();
                if(tur.Equals("Pop"))
                    ListboxaVeriCekme(listBoxKullaniciAlbumlerPop, "SELECT S.ID,S.sarki_adi,S.sure FROM SarkiAlbum as SA, Sarkilar as S WHERE S.ID = SA.Sarki_ID and album_ID = " + id + "");
                else if (tur.Equals("Jazz"))
                    ListboxaVeriCekme(listBoxKullaniciAlbumlerJazz, "SELECT S.ID,S.sarki_adi,S.sure FROM SarkiAlbum as SA, Sarkilar as S WHERE S.ID = SA.Sarki_ID and album_ID = " + id + "");
                else if (tur.Equals("Klasik"))
                    ListboxaVeriCekme(listBoxKullaniciAlbumlerKlasik, "SELECT S.ID,S.sarki_adi,S.sure FROM SarkiAlbum as SA, Sarkilar as S WHERE S.ID = SA.Sarki_ID and album_ID = " + id + "");
            }
        }

        private void listBoxKullaniciAlbumlerPop_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            KendiListeneEkleme(((ListBox)sender), e, "1");
        }

        private void listBoxKullaniciJazz_MouseClick(object sender, MouseEventArgs e)
        {
            int index = ((ListBox)sender).IndexFromPoint(e.Location);
            if (index != System.Windows.Forms.ListBox.NoMatches)
            {
                btnKullaniciOynatma.Enabled = true;
                OynatmaVerisiCek(((ListBox)sender).SelectedItem.ToString().Split(' ')[0]);
            }
        }

        private void listBoxKullaniciAlbumlerJazz_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            KendiListeneEkleme(((ListBox)sender), e, "2");
        }

        private void listBoxKullaniciAlbumlerKlasik_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            KendiListeneEkleme(((ListBox)sender), e, "3");
        }

        private void btnKullaniciAlbumlerPop_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < listBoxKullaniciAlbumlerPop.Items.Count; i++)
            {
                HepsiniListeneEkle(listBoxKullaniciAlbumlerPop.Items[i].ToString().Split(' ')[0], "1");
            }
        }

        private void btnKullaniciAlbumlerJazz_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < listBoxKullaniciAlbumlerJazz.Items.Count; i++)
            {
                HepsiniListeneEkle(listBoxKullaniciAlbumlerJazz.Items[i].ToString().Split(' ')[0], "2");
            }
        }

        private void btnKullaniciAlbumlerKlasik_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < listBoxKullaniciAlbumlerKlasik.Items.Count; i++)
            {
                HepsiniListeneEkle(listBoxKullaniciAlbumlerKlasik.Items[i].ToString().Split(' ')[0], "3");
            }
        }

        private void dataGridViewKullaniciSanatcilar_SelectionChanged(object sender, EventArgs e)
        {
            con.Close();
            if (dataGridViewKullaniciSanatcilar.CurrentRow != null)
            {
                listBoxKullaniciSanatcilarPop.Items.Clear();
                listBoxKullaniciSanatcilarJazz.Items.Clear();
                listBoxKullaniciSanatcilarKlasik.Items.Clear();
                string id = dataGridViewKullaniciSanatcilar.CurrentRow.Cells[0].Value.ToString();
                ListboxaVeriCekme(listBoxKullaniciSanatcilarPop, "SELECT S.ID,S.sarki_adi,S.sure,S.tur_ID FROM SarkiSanatci as SS, Sarkilar as S WHERE S.ID = SS.Sarki_ID and sanatci_ID = " + id + " and S.tur_ID = 1");
                ListboxaVeriCekme(listBoxKullaniciSanatcilarJazz, "SELECT S.ID,S.sarki_adi,S.sure,S.tur_ID FROM SarkiSanatci as SS, Sarkilar as S WHERE S.ID = SS.Sarki_ID and sanatci_ID = " + id + " and S.tur_ID = 2");
                ListboxaVeriCekme(listBoxKullaniciSanatcilarKlasik, "SELECT S.ID,S.sarki_adi,S.sure,S.tur_ID FROM SarkiSanatci as SS, Sarkilar as S WHERE S.ID = SS.Sarki_ID and sanatci_ID = " + id + " and S.tur_ID = 3");
            }
        }

        private void dataGridViewKullaniciKullanicilarPremiumUyeler_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (dataGridViewKullaniciKullanicilarPremiumUyeler.CurrentRow != null)
            {
                string id = dataGridViewKullaniciKullanicilarPremiumUyeler.CurrentRow.Cells[0].Value.ToString();
                for (int i = 0; i < dataGridViewKullaniciKullanicilarTakipEdilenUyeler.Rows.Count; i++)
                {
                    if (dataGridViewKullaniciKullanicilarTakipEdilenUyeler["ID", i].Value != null && dataGridViewKullaniciKullanicilarTakipEdilenUyeler["ID", i].Value.ToString().Equals(id))
                        return;
                }
                if (!id.Equals(girenKullaniciID))
                {
                    DialogResult dr = MessageBox.Show(id + " idsi olan kullanıcıyı takip etmek istediğinize emin misiniz?", "Uyarı", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (dr == DialogResult.Yes)
                    {
                        CRUDIslemleri("INSERT INTO KullanicilarTakiplesme(takip_eden_kullanici_ID, takip_edilen_kullanici_ID) VALUES(" + girenKullaniciID + "," + id + ")");
                        con.Close();
                        kolonAdlari = new string[] { "ID", "KULLANICI ADI", "ÜLKESİ" };
                        DataGridViewVeriCekme(dataGridViewKullaniciKullanicilarTakipEdilenUyeler, "SELECT K.ID,K.kullanici_adi,U.ulke_adi FROM Kullanicilar as K, ulkeler as U WHERE K.ulke = U.ID and K.ID IN(SELECT takip_edilen_kullanici_ID FROM KullanicilarTakiplesme WHERE takip_eden_kullanici_ID = " + girenKullaniciID + ")", kolonAdlari);
                    }
                }
            }
        }

        private void dataGridViewKullaniciKullanicilarTakipEdilenUyeler_SelectionChanged(object sender, EventArgs e)
        {
            con.Close();
            if (dataGridViewKullaniciKullanicilarTakipEdilenUyeler.CurrentRow != null)
            {
                con.Close();
                listBoxKullaniciKullanicilarPop.Items.Clear();
                listBoxKullaniciKullanicilarJazz.Items.Clear();
                listBoxKullaniciKullanicilarKlasik.Items.Clear();
                string id = dataGridViewKullaniciKullanicilarTakipEdilenUyeler.CurrentRow.Cells[0].Value.ToString();
                ListboxaVeriCekme(listBoxKullaniciKullanicilarPop, "SELECT S.ID,S.sarki_adi,S.sure FROM Sarkilar as S, CalmaListeleri as CL, CalmaListeleriSarkilari as CLS WHERE S.ID = CLS.sarki_ID and CL.ID = CLS.calma_listesi_ID and CL.kullanici_ID = " + id + " and CL.tur_ID=1");
                ListboxaVeriCekme(listBoxKullaniciKullanicilarJazz, "SELECT S.ID,S.sarki_adi,S.sure FROM Sarkilar as S, CalmaListeleri as CL, CalmaListeleriSarkilari as CLS WHERE S.ID = CLS.sarki_ID and CL.ID = CLS.calma_listesi_ID and CL.kullanici_ID = " + id + " and CL.tur_ID=2");
                ListboxaVeriCekme(listBoxKullaniciKullanicilarKlasik, "SELECT S.ID,S.sarki_adi,S.sure FROM Sarkilar as S, CalmaListeleri as CL, CalmaListeleriSarkilari as CLS WHERE S.ID = CLS.sarki_ID and CL.ID = CLS.calma_listesi_ID and CL.kullanici_ID = " + id + " and CL.tur_ID=3");
            }
        }

        private void listBoxKullaniciKullanicilarPop_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            KendiListeneEkleme(((ListBox)sender), e, "1");
        }

        private void listBoxKullaniciKullanicilarJazz_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            KendiListeneEkleme(((ListBox)sender), e, "2");
        }

        private void listBoxKullaniciKullanicilarKlasik_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            KendiListeneEkleme(((ListBox)sender), e, "3");
        }

        private void btnKullaniciSanatcilarPop_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < listBoxKullaniciSanatcilarPop.Items.Count; i++)
            {
                HepsiniListeneEkle(listBoxKullaniciSanatcilarPop.Items[i].ToString().Split(' ')[0], "1");
            }
        }

        private void listBoxKullaniciSanatcilarPop_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            KendiListeneEkleme(((ListBox)sender), e, "1");
        }

        private void listBoxKullaniciSanatcilarJazz_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            KendiListeneEkleme(((ListBox)sender), e, "2");
        }

        private void listBoxKullaniciSanatcilarKlasik_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            KendiListeneEkleme(((ListBox)sender), e, "3");
        }

        private void btnKullaniciSanatcilarJazz_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < listBoxKullaniciSanatcilarJazz.Items.Count; i++)
            {
                HepsiniListeneEkle(listBoxKullaniciSanatcilarJazz.Items[i].ToString().Split(' ')[0], "2");
            }
        }

        private void btnKullaniciSanatcilarKlasik_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < listBoxKullaniciSanatcilarKlasik.Items.Count; i++)
            {
                HepsiniListeneEkle(listBoxKullaniciSanatcilarKlasik.Items[i].ToString().Split(' ')[0], "3");
            }
        }

        private void btnKullaniciKullanicilarPop_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < listBoxKullaniciKullanicilarPop.Items.Count; i++)
            {
                HepsiniListeneEkle(listBoxKullaniciKullanicilarPop.Items[i].ToString().Split(' ')[0], "1");
            }
        }

        private void btnKullaniciKullanicilarJazzz_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < listBoxKullaniciKullanicilarJazz.Items.Count; i++)
            {
                HepsiniListeneEkle(listBoxKullaniciKullanicilarJazz.Items[i].ToString().Split(' ')[0], "2");
            }
        }

        private void btnKullaniciKullanicilarKlasik_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < listBoxKullaniciKullanicilarKlasik.Items.Count; i++)
            {
                HepsiniListeneEkle(listBoxKullaniciKullanicilarKlasik.Items[i].ToString().Split(' ')[0], "3");
            }
        }

        private void listBoxKullaniciListelerPopTop10_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            KendiListeneEkleme(((ListBox)sender), e, "1");
        }

        private void listBoxKullaniciListelerJazzTop10_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            KendiListeneEkleme(((ListBox)sender), e, "2");
        }

        private void listBoxKullaniciListelerKlasikTop10_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            KendiListeneEkleme(((ListBox)sender), e, "3");
        }

        private void listBoxKullaniciListelerTop10_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            KendiListeneEkleme(((ListBox)sender), e, "0");
            con.Close();
        }

        private void listBoxKullaniciListelerUlkeTop10_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            KendiListeneEkleme(((ListBox)sender), e, "0");
        }

        private void btnKullaniciKullanicilarListelerTop10_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < listBoxKullaniciListelerTop10.Items.Count; i++)
            {
                string id = listBoxKullaniciListelerTop10.Items[i].ToString().Split(' ')[0];
                string tur_id = IDCekme("SELECT tur_ID FROM Sarkilar WHERE ID=" + id + "","tur_ID");
                HepsiniListeneEkle(id, tur_id);
            }
        }

        private void btnKullaniciKullanicilarListelerUlkelerTop10_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < listBoxKullaniciListelerUlkeTop10.Items.Count; i++)
            {
                string id = listBoxKullaniciListelerUlkeTop10.Items[i].ToString().Split(' ')[0];
                string tur_id = IDCekme("SELECT tur_ID FROM Sarkilar WHERE ID=" + id + "", "tur_ID");
                HepsiniListeneEkle(id, tur_id);
            }
        }

        private void btnKullaniciKullanicilarListelerPopTop10_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < listBoxKullaniciListelerPopTop10.Items.Count; i++)
            {
                HepsiniListeneEkle(listBoxKullaniciListelerPopTop10.Items[i].ToString().Split(' ')[0], "1");
            }
        }

        private void btnKullaniciKullanicilarListelerJazzTop10_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < listBoxKullaniciListelerJazzTop10.Items.Count; i++)
            {
                HepsiniListeneEkle(listBoxKullaniciListelerJazzTop10.Items[i].ToString().Split(' ')[0], "2");
            }
        }

        private void btnKullaniciKullanicilarListelerKlasikTop10_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < listBoxKullaniciListelerKlasikTop10.Items.Count; i++)
            {
                HepsiniListeneEkle(listBoxKullaniciListelerKlasikTop10.Items[i].ToString().Split(' ')[0], "3");
            }
        }

        private void btnKullaniciCikis_Click(object sender, EventArgs e)
        {
            PanelGecis(panel_BaslangicEkrani, kucukEkran);
            girenKullaniciID = girenKullaniciUlkeID = "";
        }

        private void btnKullaniciOdemeYap_Click(object sender, EventArgs e)
        {
            string abonelik = IDCekme("SELECT abonelik_turu FROM Kullanicilar WHERE ID = " + girenKullaniciID + "","abonelik_turu");
            if (abonelik.Equals("True"))
            {
                MessageBox.Show("Zaten premium üyesiniz.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                DialogResult dr = MessageBox.Show("Ödeme yapmak istediğinize emin misiniz?", "Uyarı", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dr == DialogResult.Yes) 
                {
                    CRUDIslemleri("UPDATE Kullanicilar SET abonelik_turu=1, odeme_bilgisi=1 WHERE ID=" + girenKullaniciID + "");
                    MessageBox.Show("Ödeme başarılı, Premium üyeliğiniz aktifleştirildi.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    kolonAdlari = new string[] { "ID", "KULLANICI ADI", "ÜLKESİ" };
                    DataGridViewVeriCekme(dataGridViewKullaniciKullanicilarPremiumUyeler, "SELECT K.ID,K.kullanici_adi,U.ulke_adi FROM Kullanicilar as K, ulkeler as U WHERE K.ulke = U.ID and abonelik_turu=1", kolonAdlari);
                }
            }
        }

        private void btnAdminCikis_Click(object sender, EventArgs e)
        {
            PanelGecis(panel_BaslangicEkrani, kucukEkran);
        }

        private void comboBoxAdminSarkiTuru_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBoxVerileriCekme(comboBoxAdminSarkiAlbum, "SELECT * FROM Albumler WHERE tur_ID = " + comboBoxAdminSarkiTuru.SelectedItem.ToString().Split(' ')[0] + "", "album_adi");
        }
    }
}
