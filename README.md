Oda Rezervasyon Sistemi (C# WinForms)
Bu proje, kullanıcılardan oda tercihi, tarih ve saat bilgilerini alarak bir metin dosyası (input.txt) üzerinden rezervasyon yönetimini gerçekleştiren bir C# Windows Forms uygulamasıdır.

🚀 Özellikler
Oda Yönetimi: A, B ve C olmak üzere üç farklı oda tipi ve her odanın kendine özel müsaitlik saat aralıkları bulunmaktadır.

Akıllı Kontrol: Girilen saatin geçmiş bir tarihte olup olmadığını veya odanın müsaitlik dilimlerine uyup uymadığını denetler.

Dosya Tabanlı Kayıt: Tüm başarılı rezervasyonlar kalıcı olarak input.txt dosyasına kaydedilir.

Çakışma Önleme: Aynı oda için aynı tarih ve saatte mükerrer rezervasyon yapılmasını engeller.

Özet Görüntüleme: Mevcut tüm rezervasyonları uygulama içerisinden listeleyebilir.

🛠 Kullanılan Teknolojiler
Dil: C#

Framework: .NET (Windows Forms)

Dosya İşlemleri: System.IO (StreamReader & StreamWriter)

📁 Dosya Yapısı ve Çalışma Mantığı
1. Oda Sınıfı (Oda)
Odaların müsaitlik saatleri iki boyutlu diziler (int[,]) ile tanımlanmıştır:

Oda A: 00:00, 06:00, 12:00, 18:00 başlangıçlı 2'şer saatlik dilimler.

Oda B: 02:00, 08:00, 14:00, 20:00 başlangıçlı 2'şer saatlik dilimler.

Oda C: 04:00, 10:00, 16:00, 22:00 başlangıçlı 2'şer saatlik dilimler.

2. Rezervasyon Akışı
Kullanıcı adı, oda seçimi (A/B/C), tarih ve saat bilgilerini girer.

Doğrulama: Tarih formatı ve saatin geçerliliği kontrol edilir.

3.Müsaitlik Kontrolü: * Seçilen saat geçmişteyse sistem hata verir.

Seçilen saat oda dilimine uymuyorsa, sistem otomatik olarak bir sonraki uygun saati veya ertesi günü önerir.

4.Kayıt: Eğer çakışma yoksa input.txt dosyasına aşağıdaki formatta kayıt eklenir:
[Kişi Adı] - [Tarih Saat] - [Oda] - [Saat] - Rezervasyon Başarılı.
