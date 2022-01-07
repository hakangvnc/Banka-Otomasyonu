using System;
using System.Collections.Generic;
namespace banka_otomasyonu
{
    class Banka{
        public List<Hesap> Hesaplar = new List<Hesap>();
        public List<islem_Kayitlari> Kayitlar = new List<islem_Kayitlari>();

        //************************************** MENÜ İŞLEMLERİMİZ ************************************
        public void Main(){
            //Console.BackgroundColor = ConsoleColor.DarkRed;
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("\n \n \n ************************  TÜRKİYE ÖĞRENCİ BANKASI  ************************ \n");
            Console.WriteLine("\t 1-) Yeni Hesap Açma İşlemleri");
            Console.WriteLine("\t 2-) Hesap No ile Para Yatirma");
            Console.WriteLine("\t 3-) Hesap No ile Para Çekme");
            Console.WriteLine("\t 4-) Tüm Banka Hesaplarını Listeleme");
            Console.WriteLine("\t 5-) Hesap Durumu Sorgulama");
            Console.WriteLine("\t 6-) İşlem Kayıtları");
            Console.WriteLine("\t 7-) Çekiliş ");
            Console.WriteLine("\t 8-) Kar Durumu Sorgulama");
            Console.WriteLine("\t 9-) Çıkış Yap \n");
            Console.Write("İşlem Seçiniz : ");
            int deger = Convert.ToInt32(Console.ReadLine());
            if(deger == 1){
                hesap_Ac();
                Main();
            }
            else if(deger == 2){
                para_Yatirma();
                Main();
            }
            else if(deger == 3){
                para_Cekme();
                Main();
            }
            else if(deger == 4){
                hesap_listele();
                Main();
            }
            else if(deger == 5){
                hesap_Durum();
                Main();
            }
            else if(deger == 6){
                Console.WriteLine("\t\t1-)Hesap No İle Geçmiş Sorgulama.");
                Console.WriteLine("\t\t2-)Tüm Hesap Geçmişini Listele.");
                Console.Write("\t\tLütfen İşlem Seçiniz :");
                if(Convert.ToInt32(Console.ReadLine())== 1){
                    Console.Write("\t\tLütfen Hesap No Giriniz :");
                    islem_Kayit_listeleme(Console.ReadLine());
                }else{
                    islem_Kayit_listeleme("Hepsi");
                }
                Main();
            }
            else if(deger == 7){
                Cekilis();
                Main();

            }else if(deger == 8){
                Kar_hesapla();
                Main();
            }
            else if(deger == 9){
                Environment.Exit(0);
            }
            else{
                Console.WriteLine("Üzgünüm İsteğinizi Anlayamadım Tekrar Deneyiniz");
                Main();
            }
        }    

            //************************************** MENÜ İŞLEMLERİNİN METHODLARI *************************
        public void hesap_Ac(){
            //*************************************** HESAP NO OLUŞTURMA (UNIQUE)**********************************************************
            Random random=new Random();
            List<String> hesap_numaraları = new List<String>();
            var id = ""; // 7B5F8A2Y
            for (int i = 0; i <= 4; i++)
                {
                    id += Convert.ToChar(random.Next(48,58));
                    id += Convert.ToChar(random.Next(65,91));
                }
            foreach (var item in Hesaplar)
            {
                hesap_numaraları.Add(item.Hesap_No);
            }
            while(hesap_numaraları.Contains(id))
            {
                id ="";
                for (int i = 0; i <= 4; i++)
                {
                    id += Convert.ToChar(random.Next(48,58));
                    id += Convert.ToChar(random.Next(65,91));
                }
            }//unique (benzersiz) bir hesap numarası oluşturmak  amaçlı yukardaki işlmler yapılmıştır.
            //*****************************************************************************************************************************
            Console.WriteLine("****************************  HESAP AÇMA İŞLEMİ  **************************");
            Console.WriteLine("\t 1-) Kisa Vadeli Hesap");
            Console.WriteLine("\t\t - Yıllık kar oranı %15'dir. \n\t\t - Hesap açmak için minumum 5000 tl gereklidir.");
            Console.WriteLine("\t 2-) Uzun Vadeli Hesap");
            Console.WriteLine("\t\t - Yıllık kar oranı %17'dir. \n\t\t - Hesap açmak için minumum 10000 tl gereklidir.");
            Console.WriteLine("\t 3-) Özel Hesap");
            Console.WriteLine("\t\t - Yıllık kar oranı %10'dir. \n\t\t - Hesap açmak için minumum tutar gereksinimi yoktur.");
            Console.WriteLine("\t 4-) Cari Hesap ");
            Console.WriteLine("\t\t - Kar getirisi yoktur. \n\t\t - Hesap açmak için minumum tutar gereksinimi yoktur.\n");
            Console.Write("\t Hesap Tur seçiniz :");
            int H_deger= Convert.ToInt32(Console.ReadLine());
            while(H_deger !=1 && H_deger!=2 && H_deger!=3 && H_deger!=4){
                    Console.WriteLine("\t Üzgünüm isteğinizi tanımlayamadım.... :(");
                    Console.Write("\tTekrar Hesap Türü Seçiniz.. :");
                    H_deger = Convert.ToInt32(Console.ReadLine());
                }
            Console.Write("\t Adınızı Giriniz :");
            string Ad =Console.ReadLine();
            Console.Write("\t Soyadınızı Giriniz :");
            string Soyad =Console.ReadLine();
            Console.Write("\t Bakiyenizi Giriniz :");
            Double Bakiye = Convert.ToDouble(Console.ReadLine());
            Console.Write("\t Kayit Tarihinizi Giriniz Ör:(XX/XX/XXXX) :");
            DateTime Tarih =Convert.ToDateTime(Console.ReadLine());
            Hesap hesap = new Hesap(id,H_deger,Ad,Soyad,Bakiye,Tarih,this);
            Hesaplar.Add(hesap);
        } 
        public void para_Yatirma(){

            Console.WriteLine("*******************************  PARA YATİRMA ******************************");
            Console.Write("\tPara Yatırmak İstediğiniz Hesap Numarası :");
            string h_no = Console.ReadLine();
            Console.Write("\tYatırmak İstediğiniz Tutar :");
            double h_miktar = Convert.ToDouble(Console.ReadLine());
            Console.Write("\tYatırmak İstediğini Tarih Örnek='xx.xx.xxxx' :");
            DateTime trh = Convert.ToDateTime(Console.ReadLine());
            int hata = 0;
            foreach (var item in Hesaplar)
            {
                if(item.Hesap_No == h_no){
                    hata++;
                    Double ex_bakiye=item.Bakiye;
                    Double hacim = h_miktar; 
                    item.Bakiye = item.Bakiye +h_miktar;
                    Double new_bakiye = item.Bakiye;
                    string i_tur = "Para Yatırma";
                    Console.Write("\tİşlem Başarılı.");
                    islem_Kayitlari dekont = new islem_Kayitlari(Kayitlar.Count,h_no,i_tur,ex_bakiye,new_bakiye,hacim,trh);
                    this.Kayitlar.Add(dekont);
                    item.Kar(h_miktar,item.Kar_Orani,trh);
                    break;
                }
            }if(hata ==  0){
                Console.Write("\tHesap Numarası bulunamadı. Anamenüye dönmek için tıkayınız...");
                Console.ReadLine();
            }
        }
        public void para_Cekme(){
            Console.WriteLine("*******************************  PARA ÇEKME  ******************************");
            Console.Write("\tPara Çekmek İstediğiniz Hesap Numarası :");
            string h_no =Console.ReadLine();
            Console.Write("\tÇekmek İstediğiniz Tutar :");
            double h_miktar = Convert.ToDouble(Console.ReadLine());
            DateTime trh = DateTime.Now;
            int hata = 0;
            foreach (var item in Hesaplar)
            {
                if(item.Hesap_No == h_no){
                    hata++;
                    Double ex_bakiye=item.Bakiye;
                    Double hacim = h_miktar; 
                    item.Bakiye = item.Bakiye +h_miktar;
                    Double new_bakiye = item.Bakiye;
                    string i_tur = "Para Çekme";
                    Console.Write("\tİşlem Başarılı.");
                    islem_Kayitlari dekont = new islem_Kayitlari(Kayitlar.Count,h_no,i_tur,ex_bakiye,new_bakiye,hacim,trh);
                    this.Kayitlar.Add(dekont);
                    break;
                }
            }if(hata ==  0){
                Console.Write("\tHesap Numarası bulunamadı. Anamenüye dönmek için tıkayınız...");
                Console.ReadLine();
            }
            
        }
        public void hesap_listele(){
            Console.WriteLine("***************************  TÜM HESAP LİSTESİ  ***************************\n");
            int Adet= 0;
            foreach (var item in Hesaplar)
            {
                Adet++;
                Console.Write(" HESAP NO : {0} ",item.Hesap_No );
                Console.Write("\t HESAP TÜRÜ : {0} ", item.hesap_Tur);
                Console.Write("\t AD : {0} ", item.Ad);
                Console.Write("\t Soyad : {0} ", item.Soyad);
                Console.Write("\t HESAP BAKİYESİ : {0:#,##0.00} TL ", item.Bakiye);
                Console.Write("\t KAYİT TARİHİ : {0} \n", item.Kayit_Trh);
            }
            Console.WriteLine("\n Şuanda Kayıtlı {0} Adet hesap var \n",Adet);
            Console.Write("\tDevam Etmek İçin Tıklayınız...");
            Console.ReadLine();
        }
        public void hesap_Durum(){
            Console.WriteLine("*******************************  HESAP DURUMU  **************************");
            Console.Write("\tİşlem Yapmak İstediğiniz Hesap Numarası :");
            string h_no = Console.ReadLine();
            int hata = 0;
            foreach (var item in Hesaplar)
            {
                if(h_no == item.Hesap_No){
                    hata++;
                    Console.Write("\t HESAP NO : {0} \n", item.Hesap_No);
                    Console.Write("\t HESAP TÜRÜ : {0} \n", item.hesap_Tur);
                    Console.Write("\t AD : {0} \n", item.Ad);
                    Console.Write("\t SOYAD : {0} \n",item.Soyad );
                    Console.Write("\t HESAP BAKİYESİ : {0:#,##0.00} TL \n", item.Bakiye);
                    Console.Write("Devam Etmek İçin Tıklayınız...");
                    Console.ReadLine();
                }
            }
            if(hata ==  0){
                Console.Write("\tHesap Numarası bulunamadı. Anamenüye dönmek için tıkayınız...");
                Console.ReadLine();
            }       
        }
        public void Kar_hesapla(){
            Console.WriteLine("******************************* KAR TUTARI **********************************");
            Console.Write("\tİşlem Yapmak İstediğiniz Hesap Numarası :");
            string h_no = Console.ReadLine();
            int hata = 0 ;
            foreach (var item in Hesaplar)
            {
                if (h_no == item.Hesap_No)
                {
                    double Hacim = 0;
                    hata++;
                    foreach (var nesne in Kayitlar)
                    {
                        if(item.Hesap_No == nesne.h_No){
                            if(nesne.islem_Turu=="Yeni Hesap Açma"){Hacim+=nesne.islem_Hacmi;}
                            if(nesne.islem_Turu=="Para Yatırma"){Hacim+=nesne.islem_Hacmi;}
                        }
                    }
                    Console.Write("\t HESAP TÜRÜ : {0} \n", item.hesap_Tur);
                    Console.Write("\t TOPLAM YATIRILAN TUTAR : {0:#,##0.00} TL \n", Hacim);
                    Console.Write("\t HESAP AÇIK KALMA SÜRESİ : {0:#.00} GÜN\n",(DateTime.Now-item.Kayit_Trh).TotalDays);
                    Console.Write("\t KAR TUTARI : {0:#,##0.00} TL \n",item.Top_Kar_tutari);
                    Console.Write("\t ANA PARA + KAZANÇ : {0:#,##0.00} TL \n",(Hacim+item.Top_Kar_tutari));
                    Console.Write("\t GÜNCEL BAKİYENİZ : {0:#,##0.00} TL \n",item.Bakiye);
                    Console.Write("\t\t \n");
                }
            }if(hata ==  0){
                Console.Write("\tHesap Numarası bulunamadı. Anamenüye dönmek için tıkayınız...");
                Console.ReadLine();
            }
            Console.WriteLine("Devam etmek için tıklayınız...");
            Console.ReadLine();
        }
        public void islem_Kayit_listeleme(string h_no){
            Console.WriteLine("******************************  İŞLEM KAYITLARI  ************************");
            int Adet= 0;
            if (h_no == "Hepsi")
            {
                foreach (var item in Kayitlar)
                {
                    Adet++;
                    Console.Write(" İşlem No : {0} ",item.islem_No );
                    Console.Write("\t Hesap No : {0} ", item.h_No);
                    Console.Write("\t İşlem Türü: {0} ", item.islem_Turu);
                    Console.Write("\t Eski Bakiye : {0:#,##0.00} TL ", item.ex_bakiye);
                    Console.Write("\t Yeni Bakiye : {0:#,##0.00} TL ", item.new_bakiye);
                    Console.Write("\t İşlem Hacmi : {0:#,##0.00} TL", item.islem_Hacmi);
                    Console.Write("\t İşlem Tarihi : {0} \n", item.islem_trh);
                }
                Console.WriteLine("\n  {0} Adet işlem kaydı bulundu.\n",Adet);
                Console.Write("\tDevam Etmek İçin Tıklayınız...");
                Console.ReadLine();   
            }else{
                foreach (var item in Kayitlar)
                {
                    if (h_no == item.h_No)
                    {
                        Adet++;
                        Console.Write(" İşlem No : {0} ",item.islem_No );
                        Console.Write("\t Hesap No : {0} ", item.h_No);
                        Console.Write("\t İşlem Türü: {0} ", item.islem_Turu);
                        Console.Write("\t Eski Bakiye : {0:#,##0.00} TL ", item.ex_bakiye);
                        Console.Write("\t Yeni Bakiye : {0:#,##0.00} TL ", item.new_bakiye);
                        Console.Write("\t İşlem Hacmi : {0:#,##0.00} TL ", item.islem_Hacmi);
                        Console.Write("\t İşlem Tarihi : {0} \n", item.islem_trh);
                    }
                }
                Console.WriteLine("\n  {0} Adet işlem kaydı bulundu.\n",Adet);
                Console.Write("\tDevam Etmek İçin Tıklayınız...");
                Console.ReadLine();
            }
        }
        public void Cekilis(){
            Console.WriteLine("**********************************  ÇEKİLİŞ  ****************************");
            Console.WriteLine("\n Çekilişe Katılan Hesaplar.");
            Console.WriteLine("\n Tüm kullaınıcılara her 1000 TL'lik işlem için bir çekiliş hakkı verilmiştir.");
            List<string> cekilis_liste = new List<string>();
            foreach (var item in Kayitlar)
            {
                var hak =(item.islem_Hacmi/1000);
                if(hak>=1){
                    foreach (var item2 in Hesaplar)
                    {
                        if (item2.Hesap_No == item.h_No )
                        {
                            if (item2.hesap_Tur == Hesap.Hesap_Tur.Vadesiz_Cari_Hesap)
                            {
                                hak=0;
                            }
                        }
                    }
                    while(hak >= 1){
                        hak--;
                        cekilis_liste.Add(item.h_No);
                        //Console.WriteLine("\t " + item.h_No);
                    }
                }
            }
            Random rand = new Random();
            var kazanan = cekilis_liste[rand.Next(0,cekilis_liste.Count)];
            Console.WriteLine("\n \tKAZANAN : {0} Tebrikler.\n", kazanan);
            foreach (var item in Hesaplar)
            {
                if(item.Hesap_No == kazanan){
                    
                    islem_Kayitlari dekont = new islem_Kayitlari(Kayitlar.Count,item.Hesap_No,"ÇEKİLİŞ",item.Ad,item.Soyad,item.Bakiye,item.Bakiye+1000,1000,DateTime.Now);
                    Kayitlar.Add(dekont);
                    item.Bakiye = item.Bakiye+1000;
                }
            }
        }
    }
         //**********************************   HESAP SINIFI ÖZELLİKLERİ  *********************************
    class Hesap:Banka {
        // Hesap sınıfımızın propertyleri ...
            public enum Hesap_Tur{
                Kisa_Vadeli_Hesap,
                Uzun_Vadeli_Hesap,
                Vadesiz_Ozel_Hesap,
                Vadesiz_Cari_Hesap
            }
            public string Hesap_No { get; set; }
            public Hesap_Tur hesap_Tur { get; set; }
            public string Ad { get; set; }
            public string Soyad { get; set; }
            public Double Bakiye { get; set; }
            public DateTime Kayit_Trh { get; set; }
            //Kar için gerekli propertyler.
            public double Kar_Orani { get; set; }
            public double Kar_tutari { get; set; }
            public double Top_Kar_tutari { get; set; }
            //*********************
            private Banka banka ;
            public Hesap(string _id, int _tur,string _ad,string _soyad ,Double _bakiye,DateTime _trh, Banka _banka){
                // ****************Hesap no oluşturulması*******************
                // banka nesne si ataması
                banka = _banka;
                //**********************
                Hesap_No = _id;
                //// hwsap türü belirleme 
                if (_tur == 1) {hesap_Tur = Hesap_Tur.Kisa_Vadeli_Hesap; Kar_Orani = Convert.ToDouble(15);}//5000 min
                else if (_tur == 2) {hesap_Tur = Hesap_Tur.Uzun_Vadeli_Hesap;Kar_Orani = Convert.ToDouble(17);}//10000 min 
                else if (_tur == 3) {hesap_Tur = Hesap_Tur.Vadesiz_Ozel_Hesap;Kar_Orani = Convert.ToDouble(10);}
                else if (_tur == 4) {hesap_Tur = Hesap_Tur.Vadesiz_Cari_Hesap;Kar_Orani = Convert.ToDouble(0);}
                Ad = _ad;
                Soyad = _soyad;
                if (hesap_Tur == Hesap_Tur.Uzun_Vadeli_Hesap)
                    {
                    while(_bakiye < 10000){
                        Console.WriteLine("     Yetersiz Bakiye girdininiz. Min=10000 tl giriniz.");
                        Console.Write("     tekrardan bakiye giriniz :");
                        _bakiye= Convert.ToDouble(Console.ReadLine());
                    }
                }
                else if(hesap_Tur == Hesap_Tur.Kisa_Vadeli_Hesap){
                    while(_bakiye < 5000){
                        Console.WriteLine("     Yetersiz Bakiye girdininiz. Min=5000 tl giriniz.");
                        Console.Write("     tekrardan bakiye giriniz :");
                        _bakiye= Convert.ToDouble(Console.ReadLine());
                    }  
                }
                Bakiye =_bakiye;
                Kayit_Trh = _trh;
                string i_tur ="Yeni Hesap Açma";
                int eski_bakiye= 0;
                var _miktar = Bakiye;
                islem_Kayitlari dekont = new islem_Kayitlari(banka.Kayitlar.Count,Hesap_No,i_tur,eski_bakiye,Bakiye,_miktar,Kayit_Trh);
                banka.Kayitlar.Add(dekont);
                // otomatik kar hesaplatma 
                Kar(_bakiye,Kar_Orani,_trh);
                Alt_Menu();
            }
                // kayıt işlemleri tamamlanıyor
            public void Alt_Menu(){
                Console.WriteLine("********************** YENİ AÇILAN HESAP İŞLEMLERİ **********************");
                Console.WriteLine("\t 1-) Para Yatirma");
                Console.WriteLine("\t 2-) Para Çekme");
                Console.WriteLine("\t 3-) Hesap Durumu");
                Console.WriteLine("\t 4-) Kar Tutarı");
                Console.WriteLine("\t 5-) Hesap Özeti");
                Console.WriteLine("\t 6-) Ana Menüye Dön");
                Console.Write("\t İşlem Seçiniz : ");
                int Deger1=Convert.ToInt32(Console.ReadLine());
                if(Deger1 == 1){
                    Yatirma();
                }
                else if(Deger1 == 2){
                    Cekme();
                }
                else if(Deger1 == 3){
                    Durum();
                }
                else if(Deger1 == 4){
                    Kar_Tutar();
                }
                else if(Deger1 == 5){
                    hesap_Ozeti();
                }
                else if(Deger1 == 6){
                }
                else{
                    Console.WriteLine("Üzgünüm isteğinizi anlayamadım tekrar deneyiniz.");
                    Alt_Menu();
                }
            }
            private void Yatirma(){
                Console.WriteLine("*******************************  PARA YATİRMA ******************************");
                string i_tur="Para Yatırma";
                Console.WriteLine("Mevcut Bakiyeniz : {0:#,##0.00} TL", Bakiye);
                Double eski_bakiye= Bakiye;
                Console.Write("Yatırmak istediğiniz miktarı giriniz :");
                Double _miktar = Convert.ToDouble(Console.ReadLine());
                Console.Write("Yatırmak istediğiniz tarihi giriniz :");
                DateTime _trh = Convert.ToDateTime(Console.ReadLine());
                Bakiye = Bakiye + _miktar;
                islem_Kayitlari dekont = new islem_Kayitlari(banka.Kayitlar.Count,Hesap_No,i_tur,eski_bakiye,Bakiye,_miktar,_trh);
                banka.Kayitlar.Add(dekont);
                Kar(_miktar,Kar_Orani,_trh);
                Alt_Menu();
            }
            private void Cekme(){
                Console.WriteLine("*******************************  PARA ÇEKME *********************************");
                string i_tur = "Para Çekme";
                Console.WriteLine("\tMevcut Bakiyeniz : {0:#,##0.00} TL", Bakiye);
                Double eski_bakiye= Bakiye;
                Console.Write("\tÇekmek istediğiniz miktarı giriniz :");
                Double _miktar = Convert.ToDouble(Console.ReadLine());
                while (_miktar > Bakiye){
                    Console.WriteLine("\tHesabınızda yeterli miktar yok. Tekrar deneyiniz.");
                    Console.Write("\tÇekmek istediğiniz miktarı giriniz :");
                    _miktar = Convert.ToDouble(Console.ReadLine());
                }
                Bakiye = Bakiye - _miktar;
                Console.WriteLine("\tEski Bakiyeniz : {0:#,##0.00} TL", eski_bakiye);
                Console.WriteLine("\tYeni Bakiyeniz : {0:#,##0.00} TL", Bakiye);
                DateTime _trh = DateTime.Now;
                //H_Kayit(Hesap_No,i_tur,eski_bakiye,Bakiye,_miktar,_trh,banka):
                islem_Kayitlari dekont = new islem_Kayitlari(banka.Kayitlar.Count,Hesap_No,i_tur,eski_bakiye,Bakiye,_miktar,_trh);
                banka.Kayitlar.Add(dekont);
                Alt_Menu();
            }
            private void Durum(){
                Console.WriteLine("****************************** HESAP DURUMU *********************************");
                Console.Write("\t HESAP NO : {0} \n", Hesap_No);
                Console.Write("\t HESAP TÜRÜ : {0} \n", hesap_Tur);
                Console.Write("\t AD : {0} \n", Ad);
                Console.Write("\t SOYAD : {0} \n",Soyad );
                Console.Write("\t HESAP BAKİYESİ : {0:#,##0.00} TL \n", Bakiye);
                Console.Write("Devam Etmek İçin Tıklayınız...");
                Console.ReadLine();
                Alt_Menu();
            }
            public void Kar(Double islem_Hacmi ,Double Oran, DateTime _trh ){
                //Console.WriteLine("Yeni Hesap Açma");
                Double Kar_gün = Math.Floor((DateTime.Now-_trh).TotalDays);
                while(Kar_gün > 0){
                    if(Kar_gün > 365){
                        Kar_tutari=  Math.Floor(islem_Hacmi*Oran/100);
                        Top_Kar_tutari += Kar_tutari;
                        Kar_gün = Kar_gün-365 ;
                        islem_Hacmi +=Kar_tutari; 
                    }else
                    {
                        Kar_tutari = Math.Floor(islem_Hacmi*((Oran/365)*Kar_gün)/100);
                        Top_Kar_tutari += Kar_tutari;
                        Kar_gün = 0 ;
                    }
                }
                Bakiye +=Top_Kar_tutari; 
            }
            public void Kar_Tutar(){
                Console.WriteLine("******************************* KAR TUTARI **********************************");
                double Hacim = 0;
                foreach (var item in banka.Kayitlar)
                {
                    if(Hesap_No == item.h_No){
                        if(item.islem_Turu=="Yeni Hesap Açma"){Hacim+=item.islem_Hacmi;}
                        if(item.islem_Turu=="Para Yatırma"){Hacim+=item.islem_Hacmi;}
                    }
                }
                Console.Write("\t HESAP TÜRÜ : {0} \n", hesap_Tur);
                Console.Write("\t TOPLAM YATIRILAN TUTAR : {0:#,##0.00} TL \n", Hacim);
                Console.Write("\t HESAP AÇIK KALMA SÜRESİ : {0:#.00} GÜN\n",(DateTime.Now-Kayit_Trh).TotalDays);
                Console.Write("\t KAR TUTARI : {0:#,##0.00} TL \n",Top_Kar_tutari);
                Console.Write("\t ANA PARA + KAZANÇ : {0:#,##0.00} TL \n",Hacim+Top_Kar_tutari);
                Console.Write("\t GÜNCEL BAKİYENİZ : {0:#,##0.00} TL \n",Bakiye);
                Console.Write("\n");
                Console.Write("\tDevam Etmek İçin Tıklayınız...");
                Console.ReadLine();
                Alt_Menu();
            }
            private void hesap_Ozeti(){
                Console.WriteLine("******************************  İŞLEM KAYITLARI  ************************");
                int Adet= 0;
                    foreach (var item in banka.Kayitlar)
                    {
                        if(item.h_No == Hesap_No ){
                            Adet++;
                            Console.Write(" İşlem No : {0} ",item.islem_No );
                            Console.Write("\t Hesap No : {0} ", item.h_No);
                            Console.Write("\t İşlem Türü: {0} ", item.islem_Turu);
                            Console.Write("\t Eski Bakiye : {0:#,##0.00} TL", item.ex_bakiye);
                            Console.Write("\t Yeni Bakiye : {0:#,##0.00} TL ", item.new_bakiye);
                            Console.Write("\t İşlem Hacmi : {0:#,##0.00} TL", item.islem_Hacmi);
                            Console.Write("\t İşlem Tarihi : {0} \n", item.islem_trh);
                        }
                    }
                Console.WriteLine("\n  {0} Adet işlem kaydı bulundu.\n",Adet);
                Console.Write("\tDevam Etmek İçin Tıklayınız...");
                Console.ReadLine();
                Alt_Menu();
            }
        }
    class islem_Kayitlari
    {
        public int islem_No{ get; set; }
        public string h_No{ get; set; }
        public string islem_Turu{ get; set; }
        public Double ex_bakiye{ get; set; }
        public Double new_bakiye{ get; set; }
        public Double islem_Hacmi{ get; set; }
        public DateTime islem_trh{ get; set; }
        public islem_Kayitlari(int i_no,string h_no , string i_tur , Double ex_b, Double new_b ,Double _hacim, DateTime i_trh)
        {
            islem_No =i_no +1;
            h_No = h_no;
            islem_Turu = i_tur;
            ex_bakiye = ex_b;
            new_bakiye = new_b;
            islem_trh = i_trh;
            islem_Hacmi= _hacim;
            Console.WriteLine("******************************  İŞLEM DEKONTU  ************************");
            Console.WriteLine("\t İşlem Numarası : {0}" ,islem_No);
            Console.WriteLine("\t İşlem Yapılan Hesap No : {0}",h_No);
            Console.WriteLine("\t İşlem Türü : {0}",islem_Turu);
            Console.WriteLine("\t Önceki Bakiye : {0:#,##0.00} TL",ex_bakiye);
            Console.WriteLine("\t Yeni Bakiye : {0:#,##0.00} TL",new_bakiye);
            Console.WriteLine("\t İşlem Hacmi : {0:#,##0.00} TL",islem_Hacmi);
            Console.WriteLine("\t İşlem Tarihi : {0}",islem_trh);
            Console.WriteLine("Tekrar dekontunuzu görüntülemek için anamenü'den hesap no ile sorgulayabilirsiniz.");
            Console.WriteLine("Devam etmek için tıklayınız...");
            Console.ReadLine();
        }
        public islem_Kayitlari(int i_no,string h_no , string i_tur ,string _ad ,string _soyad, Double ex_b, Double new_b ,Double _hacim, DateTime i_trh){
            islem_No =i_no +1;
            h_No = h_no;
            islem_Turu = i_tur;
            ex_bakiye = ex_b;
            new_bakiye = new_b;
            islem_trh = i_trh;
            islem_Hacmi= _hacim;
            Console.WriteLine("******************************  ÇEKİLİŞ DEKONTU  ************************");
            Console.WriteLine("\t İşlem Numarası : {0}" ,islem_No);
            Console.WriteLine("\t Kazanan Hesap No : {0}",h_No);
            Console.WriteLine("\t Ad: {0}",_ad);
            Console.WriteLine("\t Soyad : {0}",_soyad);
            Console.WriteLine("\t İşlem Türü : {0}",islem_Turu);
            Console.WriteLine("\t Önceki Bakiye : {0:#,##0.00} TL",ex_bakiye);
            Console.WriteLine("\t Yeni Bakiye : {0:#,##0.00} TL",new_bakiye);
            Console.WriteLine("\t İşlem Hacmi : {0:#,##0.00} TL",islem_Hacmi);
            Console.WriteLine("\t İşlem Tarihi : {0}",islem_trh);
            Console.WriteLine("Tebrikler {0} {1} çekilişimizden 1000 tl ödül kazandınız. Ödülünüz bakiyenize eklenmiştir.",_ad,_soyad);
            Console.WriteLine("Tekrar dekontunuzu görüntülemek için anamenü'den hesap no ile sorgulayabilirsiniz.");
            Console.WriteLine("Devam etmek için tıklayınız...");
            Console.ReadLine();
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            Banka banka =new Banka();
            banka.Main();
        }
    }
}