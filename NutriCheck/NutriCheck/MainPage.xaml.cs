using Microsoft.Maui.Controls;

namespace NutriCheck;

public partial class MainPage : ContentPage
{
    public MainPage()
    {
        InitializeComponent();
    }

    private async void OnAnalizScrollClicked(object sender, EventArgs e)
    {
        // Butona basınca ekranın hafifçe aşağı kayıp boy girişine odaklanmasını sağlar
        await Task.Delay(100);
        txtBoy.Focus();
    }

    private void OnHesaplaClicked(object sender, EventArgs e)
    {
        if (double.TryParse(txtBoy.Text, out double boy) &&
            double.TryParse(txtKilo.Text, out double kilo) &&
            pckCinsiyet.SelectedIndex != -1 &&
            pckBeslenmeTarzi.SelectedIndex != -1)
        {
            double metreBoy = boy > 3 ? boy / 100 : boy;
            double boyCm = metreBoy * 100;

            if (boyCm < 120 || boyCm > 230 || kilo < 25 || kilo > 300)
            {
                DisplayAlert("Hata", "Hatalı giriş yaptınız.", "Tamam");
                return;
            }

            double vki = kilo / (metreBoy * metreBoy);
            lblVkiSonuc.Text = vki.ToString("F1");

            double bmh = (pckCinsiyet.SelectedItem.ToString() == "Erkek")
                ? (10 * kilo) + (6.25 * boyCm) - (5 * 25) + 5
                : (10 * kilo) + (6.25 * boyCm) - (5 * 25) - 161;

            double hedefKalori = bmh * 1.2;
            string kahvaltiTxt = "", ogleTxt = "", araOgunTxt = "", aksamTxt = "";
            int tarzIdx = pckBeslenmeTarzi.SelectedIndex;

            // --- BESLENME PROGRAMI (ARA ÖĞÜNLER DAHİL) ---
            if (vki < 18.5) // 1. ZAYIF
            {
                lblDurum.Text = "Zayıf"; hedefKalori += 500;
                if (tarzIdx == 0)
                { // Geleneksel
                    kahvaltiTxt = "• 3 Yumurtalı Omlet (150g)\n• Sucuklu Yumurta (100g)\n• Bal-Kaymak & Ekmek";
                    ogleTxt = "• Soslu Tavuk Sote & Pilav\n• Etli Nohut & Bulgur\n• Kuzu Güveç (300g)";
                    araOgunTxt = "• 1 Avuç Ceviz & Kuru Üzüm\n• 1 Kase Tam Yağlı Yoğurt\n• 2 Dilim Kek & Süt";
                    aksamTxt = "• Kıymalı Makarna (300g)\n• İzmir Köfte & Püre\n• Tavuk Pirzola (200g)";
                }
                else if (tarzIdx == 1)
                { // Fit
                    kahvaltiTxt = "• Muzlu-Fıstık Yulaf (100g)\n• Granola & Tam Yağlı Yoğurt\n• Peynirli Krep & Bal";
                    ogleTxt = "• Izgara Biftek (200g) & Patates\n• Hindi Göğüs (200g)\n• Izgara Somon (250g)";
                    araOgunTxt = "• Protein Bar & 10 Badem\n• Fıstık Ezmeli Elma Dilimleri\n• 1 Bardak Kefir & 1 Muz";
                    aksamTxt = "• Somon (250g) & Kinoa\n• Ton Balıklı Makarna (200g)\n• Bonfile (200g) & Sebze";
                }
                else
                { // Hızlı
                    kahvaltiTxt = "• Avokadolu & Lorlu Sandviç\n• Ton Balıklı Sandviç\n• Karışık Tost & Meyve Suyu";
                    ogleTxt = "• 2 Adet Lahmacun & Ayran\n• Tavuklu Dürüm\n• Etli Ekmek & Cacık";
                    araOgunTxt = "• Hazır Meyveli Yoğurt\n• 1 Paket Çubuk Kraker\n• 2 Adet Muz & Süt";
                    aksamTxt = "• Ev Yapımı Burger (150g Et)\n• Kıymalı Pide & Yoğurt\n• Tavuklu Penne";
                }
            }
            else if (vki <= 24.9) // 2. NORMAL
            {
                lblDurum.Text = "Normal";
                if (tarzIdx == 0)
                { // Geleneksel
                    kahvaltiTxt = "• 1 Yumurta & Peynir (60g)\n• Menemen & 1 Dilim Ekmek\n• Peynirli Omlet & Zeytin";
                    ogleTxt = "• Sebze Yemeği (200g) & Bulgur\n• Etli Türlü (150g) & Pilav\n• Kuru Fasulye (200g)";
                    araOgunTxt = "• 1 Porsiyon Mevsim Meyvesi\n• 3 Adet Ceviz İçi\n• 1 Bardak Ayran & Galeta";
                    aksamTxt = "• Izgara Tavuk (150g) & Salata\n• Izgara Balık (180g) & Roka\n• 6 Adet Köfte & Salata";
                }
                else if (tarzIdx == 1)
                { // Fit
                    kahvaltiTxt = "• Meyveli Yoğurt & Yulaf\n• Smoothie (Ispanak-Muz)\n• Chia Puding & 3 Ceviz";
                    ogleTxt = "• Ton Balıklı Salata (160g)\n• Izgara Hindi & Kinoa\n• 6 Adet Mercimek Köftesi";
                    araOgunTxt = "• Yeşil Çay & 10 Çiğ Badem\n• 1 Kase Sade Yoğurt\n• Yarım Avokado & Lor Peyniri";
                    aksamTxt = "• Fırın Mücver (200g) & Yoğurt\n• Izgara Somon (150g)\n• Izgara Sebze & Tavuk";
                }
                else
                { // Hızlı
                    kahvaltiTxt = "• Peynirli Tost\n• Simit & Peynir (30g)\n• 3 Galeta & Lor (60g)";
                    ogleTxt = "• Izgara Köfte & Piyaz\n• Tavuklu Salata\n• Ton Balıklı Sandviç";
                    araOgunTxt = "• 1 Adet Elma\n• Peynirli Galeta (2 adet)\n• Sütlü Kahve & 2 Gün kurusu";
                    aksamTxt = "• Zeytinyağlı Fasulye (150g)\n• Mercimek Çorbası & Ekmek\n• Mantar Sote & Yoğurt";
                }
            }
            else // 3. KİLOLU ve OBEZ
            {
                lblDurum.Text = vki <= 29.9 ? "Kilolu" : "Obez";
                hedefKalori -= (vki <= 29.9 ? 500 : 750);
                if (tarzIdx == 0)
                { // Geleneksel
                    kahvaltiTxt = "• Lorlu Omlet (Yağsız)\n• 1 Yumurta & Bol Yeşillik\n• Yağsız Menemen";
                    ogleTxt = "• Haşlama Tavuk & Salata\n• Izgara Hindi (150g)\n• Haşlama Sebze & Yoğurt";
                    araOgunTxt = "• 1 Finan Türk Kahvesi\n• Yeşil Çay & 2 Ceviz\n• Yarım Greyfurt";
                    aksamTxt = "• Kabak Sote (200g) & Yoğurt\n• Sebze Çorbası & Yoğurt\n• Fırın Balık (100g) & Roka";
                }
                else if (tarzIdx == 1)
                { // Fit
                    kahvaltiTxt = "• 1 Peynir (Yağsız) & Marul\n• Chia'lı Yulaf (Su ile)\n• Yarım Avokado & Yeşillik";
                    ogleTxt = "• Sebze Çorbası & Yoğurt\n• Ton Balığı (Yağsız)\n• Zeytinyağlı Pırasa";
                    araOgunTxt = "• 5-6 Adet Çiğ Fındık\n• 1 Bardak Tarçınlı Süt\n• Kereviz Sapı & Yoğurt";
                    aksamTxt = "• Mantar Sote (200g) & Hindi\n• Buharda Brokoli (250g)\n• Haşlama Et (100g) & Salata";
                }
                else
                { // Hızlı
                    kahvaltiTxt = "• 1 Etimek & Yarım Avokado\n• Yeşil Smoothie\n• Lor (60g) & Dereotu";
                    ogleTxt = "• Buharda Sebze & Limon\n• Yoğurtlu Kabak (200g)\n• Izgara Mantar (250g)";
                    araOgunTxt = "• Salatalık & Dereotu\n• Sade Maden Suyu & Limon\n• 1 Dilim Ananas";
                    aksamTxt = "• Hindi Füme (120g) & Roka\n• Mercimek Çorbası (Yağsız)\n• Haşlanmış Tavuk (100g)";
                }
            }

            // 6. Çıktıları Yazdırma (FormattedText ile Bold Yapma)
            double kK = hedefKalori * 0.25;
            double kO = hedefKalori * 0.30;
            double kA = hedefKalori * 0.15;
            double kAk = hedefKalori * 0.30;

            lblKaloriOnerisi.Text = $"🏆 NutriCheck Hedefi: {hedefKalori:F0} kcal";

            // KAHVALTI
            lblKahvalti.FormattedText = new FormattedString
            {
                Spans = {
        new Span { Text = "🍳 SEÇENEKLER:\n\n", FontAttributes = FontAttributes.Bold, FontSize = 16 },
        new Span { Text = kahvaltiTxt + "\n\n" },
        new Span { Text = $"🏆 Öğün Hedefi: {kK:F0} kcal", FontAttributes = FontAttributes.Bold, TextColor = Colors.Gold }
    }
            };

            // ÖĞLE
            lblOgle.FormattedText = new FormattedString
            {
                Spans = {
        new Span { Text = "🥗 SEÇENEKLER:\n\n", FontAttributes = FontAttributes.Bold, FontSize = 16 },
        new Span { Text = ogleTxt + "\n\n" },
        new Span { Text = $"🏆 Öğün Hedefi: {kO:F0} kcal", FontAttributes = FontAttributes.Bold, TextColor = Colors.Gold }
    }
            };

            // ARA ÖĞÜN
            lblAraOgun.FormattedText = new FormattedString
            {
                Spans = {
        new Span { Text = "🍎 SEÇENEKLER:\n\n", FontAttributes = FontAttributes.Bold, FontSize = 16 },
        new Span { Text = araOgunTxt + "\n\n" },
        new Span { Text = $"🏆 Öğün Hedefi: {kA:F0} kcal", FontAttributes = FontAttributes.Bold, TextColor = Colors.Gold }
    }
            };

            // AKŞAM
            lblAksam.FormattedText = new FormattedString
            {
                Spans = {
        new Span { Text = "🌙 SEÇENEKLER:\n\n", FontAttributes = FontAttributes.Bold, FontSize = 16 },
        new Span { Text = aksamTxt + "\n\n" },
        new Span { Text = $"🏆 Öğün Hedefi: {kAk:F0} kcal", FontAttributes = FontAttributes.Bold, TextColor = Colors.Gold }
    }
            };

            string[] tavsiyeler = {
                "Günde en az 2.5 - 3 litre su içmeyi ihmal etmeyiniz.",
                "Uykudan en az 3 saat önce yemek yemeyi kesmelisiniz.",
                "Öğünlerinizde tuzu azaltıp baharatlara şans verin.",
                "Asitli ve şekerli içeceklerden uzak durunuz.",
                "Yemeklerinizi yavaş yiyerek sindirimi kolaylaştırın.",
                "Her gün en az 30 dakika hafif tempolu yürüyüş yapın."
            };
            lblAsistanNotu.Text = tavsiyeler[new Random().Next(tavsiyeler.Length)];
        }
        else
        {
            DisplayAlert("Uyarı", "Lütfen tüm seçimleri yapınız.", "Tamam");
        }
    }
}