namespace ödev6
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        // Oda sınıfı
        class Oda
        {
            public string adi;
            public int[,] OdaA = { { 0, 2 }, { 6, 8 }, { 12, 14 }, { 18, 20 } };
            public int[,] OdaB = { { 2, 4 }, { 8, 10 }, { 14, 16 }, { 20, 22 } };
            public int[,] OdaC = { { 4, 6 }, { 10, 12 }, { 16, 18 }, { 22, 24 } };
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.Text = "Form Örnek Uygulama";  // Form başlığı
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string odaIsmi = textBox1.Text;
            string tarihStr = textBox2.Text;
            string saatStr = textBox3.Text;
            string kisiAdi = textBox4.Text;

            string fileName = "input.txt";

            DateTime hedefTarih;
            if (!DateTime.TryParse(tarihStr, out hedefTarih))
            {
                label5.Text = "Geçerli bir tarih girin!";
                return;
            }

            int saat;
            if (!int.TryParse(saatStr, out saat) || saat < 0 || saat >= 24)
            {
                label5.Text = "Geçerli bir saat girin! (0-23 arası)";
                return;
            }

            if (string.IsNullOrWhiteSpace(odaIsmi) || (odaIsmi != "A" && odaIsmi != "B" && odaIsmi != "C"))
            {
                label5.Text = "Geçerli bir oda seçin (A, B veya C).";
                return;
            }

            Oda oda = new Oda();
            bool rezervasyonYapildi = false;
            DateTime yeniRezervasyonSaati = DateTime.Now;

            int[,] secilenOdaSaatleri = odaIsmi == "A" ? oda.OdaA : (odaIsmi == "B" ? oda.OdaB : oda.OdaC);

            for (int i = 0; i < secilenOdaSaatleri.GetLength(0); i++)
            {
                DateTime odaSaati = new DateTime(hedefTarih.Year, hedefTarih.Month, hedefTarih.Day, secilenOdaSaatleri[i, 0], 0, 0);

                if (odaSaati.Hour == saat && odaSaati <= DateTime.Now)
                {
                    label5.Text = $"{odaIsmi} odasında {odaSaati.ToString("HH:mm")} saati dolu.";

                    bool uygunSaatBulundu = false;

                    for (int j = i + 1; j < secilenOdaSaatleri.GetLength(0); j++)
                    {
                        DateTime sonrakiSaat = new DateTime(hedefTarih.Year, hedefTarih.Month, hedefTarih.Day, secilenOdaSaatleri[j, 0], 0, 0);
                        if (sonrakiSaat > DateTime.Now)
                        {
                            label5.Text += $"\nSonraki uygun saat: {sonrakiSaat.ToString("HH:mm")}";
                            uygunSaatBulundu = true;
                            break;
                        }
                    }

                    if (!uygunSaatBulundu)
                    {
                        hedefTarih = hedefTarih.AddDays(1);
                        DateTime sonrakiGunSaati = new DateTime(hedefTarih.Year, hedefTarih.Month, hedefTarih.Day, secilenOdaSaatleri[0, 0], 0, 0);
                        label5.Text += $"\nBugün dolu. Bir sonraki günün ilk uygun saati: {sonrakiGunSaati.ToString("HH:mm")}";
                    }

                    return;
                }
                else if (odaSaati.Hour == saat && odaSaati > DateTime.Now)
                {
                    yeniRezervasyonSaati = odaSaati;
                    rezervasyonYapildi = true;
                    break;
                }
            }

            if (!rezervasyonYapildi)
            {
                label5.Text = "Seçilen odada belirtilen saat dolu. Bir sonraki uygun saat için rezervasyon yapılacaktır.";

                hedefTarih = hedefTarih.AddDays(1);
                yeniRezervasyonSaati = new DateTime(hedefTarih.Year, hedefTarih.Month, hedefTarih.Day, secilenOdaSaatleri[0, 0], 0, 0);
            }

            List<string> mevcutRezervasyonlar = new List<string>();
            if (File.Exists(fileName))
            {
                using (StreamReader sr = new StreamReader(fileName))
                {
                    string line;
                    while ((line = sr.ReadLine()) != null)
                    {
                        mevcutRezervasyonlar.Add(line);
                    }
                }
            }

            foreach (var rezervasyon in mevcutRezervasyonlar)
            {
                if (rezervasyon.Contains($"{odaIsmi} - {yeniRezervasyonSaati.ToString("dd.MM.yyyy HH:mm")}"))
                {
                    label5.Text = $"Bu saatte ({yeniRezervasyonSaati.ToString("HH:mm")}) {odaIsmi} odasında zaten bir rezervasyon yapılmış.";
                    return;
                }
            }

            using (StreamWriter sr = new StreamWriter(fileName, true))
            {
                sr.WriteLine($"{kisiAdi} - {yeniRezervasyonSaati.ToString("dd.MM.yyyy HH:mm")} - {odaIsmi} - {saat} - Rezervasyon Başarılı.");
            }

            label5.Text = $"{kisiAdi} için {odaIsmi} odasında {yeniRezervasyonSaati.ToString("HH:mm")} tarihinde rezervasyon başarıyla yapıldı.";

            textBox1.Text = string.Empty;
            textBox2.Text = string.Empty;
            textBox3.Text = string.Empty;
            textBox4.Text = string.Empty;
        }

        // Özet Butonuna Tıklandığında Çalışan Kod
        private void button2_Click(object sender, EventArgs e)
        {
            string fileName = "input.txt";

            if (!File.Exists(fileName))
            {
                label5.Text = "Henüz rezervasyon yapılmamış.";
                return;
            }

            List<string> rezervasyonlar = new List<string>();
            using (StreamReader sr = new StreamReader(fileName))
            {
                string line;
                while ((line = sr.ReadLine()) != null)
                {
                    rezervasyonlar.Add(line);
                }
            }

            // Rezervasyon özetini label5'te göstermek
            label5.Text = "Rezervasyonlar:\n";
            foreach (var rezervasyon in rezervasyonlar)
            {
                label5.Text += rezervasyon + "\n";
                File.ReadAllText(fileName);
            }

        }
    }
}















