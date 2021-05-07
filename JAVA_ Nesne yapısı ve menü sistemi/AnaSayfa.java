package personel;

import java.util.Scanner;

public class AnaSayfa {

    public static void main(String[] args) {
        Scanner sc = new Scanner(System.in);
        int departmanSayisi, secim;

        System.out.println("Departman sayısı giriniz : ");
        departmanSayisi = sc.nextInt();

        Departman[] departmanlar = new Departman[departmanSayisi];

        for (int i = 0; i < departmanSayisi; i++) {
            departmanlar[i] = new Departman();
            System.out.println("Departman No :");
            departmanlar[i].SetDepNo(sc.nextInt());

            System.out.println("Departman Adı :");
            sc.nextLine();
            departmanlar[i].SetDepAd(sc.nextLine());

            System.out.println("Personel Sayısı :");
            departmanlar[i].personeller = new Personel[sc.nextInt()];

            for (int j = 0; j < departmanlar[i].personeller.length; j++) {
                departmanlar[i].personeller[j] = new Personel();
                departmanlar[i].personeller[j].SetDepNo(departmanlar[i].GetDepNo());
                System.out.println(departmanlar[i].GetDepAd() + " departmanının " + (j+1) + ". Personel Adı : ");
                sc.nextLine();
                departmanlar[i].personeller[j].SetPerAd(sc.nextLine());

                System.out.println(departmanlar[i].GetDepAd() + " departmanının " + (j+1) + ". Personel Soyadı : ");
                departmanlar[i].personeller[j].SetPerSoyad(sc.nextLine());

                System.out.println(departmanlar[i].GetDepAd() + " departmanının " + (j+1) + ". Personel Yaşı : ");
                departmanlar[i].personeller[j].SetPerYas(sc.nextInt());

            }

            System.out.println("Servis Sayısı : ");
            departmanlar[i].servisler = new Servis[sc.nextInt()];

            for (int j = 0; j < departmanlar[i].servisler.length; j++) {
                departmanlar[i].servisler[j] = new Servis();
                departmanlar[i].servisler[j].SetDepNo(departmanlar[i].GetDepNo());
                System.out.println(departmanlar[i].GetDepAd() + " departmanının " + (j+1) + ". Servis Güzergahı : ");
                if(j == 0){
                    sc.nextLine();
                    departmanlar[i].servisler[j].SetServisGuzergah(sc.nextLine());
                }
                else
                    departmanlar[i].servisler[j].SetServisGuzergah(sc.nextLine());
            }
        }
        System.out.println("");
        do{
            System.out.println("1-Tüm Departman Bilgilerini Listele\n" +
                    "2-Departman Adına Göre Arama Yap\n" +
                    "3- Personel Adına Göre Arama Yap\n" +
                    "4- Servis Güzergâhına Göre Arama Yap\n" +
                    "5- Yaşı En Büyük ve En Küçük Personel/Personelleri Bul\n" +
                    "6- Çıkış");
            secim = sc.nextInt();
            switch (secim){
                case 1:
                    for (int i = 0; i < departmanlar.length; i++) {
                        System.out.println("Departman No : " + departmanlar[i].GetDepNo() + " Departman Adı : " + departmanlar[i].GetDepAd());
                        System.out.println("Personeller;");
                        for (int j = 0; j < departmanlar[i].personeller.length; j++)
                            System.out.println("\t" + (j+1) + ". Personel : " + departmanlar[i].personeller[j].GetPerID() + " " + departmanlar[i].personeller[j].GetPerAd() + " " + departmanlar[i].personeller[j].GetPerSoyad() + " " + departmanlar[i].personeller[j].GetPerYas());
                        for (int j = 0; j < departmanlar[i].servisler.length; j++)
                            System.out.println("\t" + (j+1) + ". Servis : " + departmanlar[i].servisler[j].GetServisID() + " " + departmanlar[i].servisler[j].GetServisGuzergah());
                    }
                    System.out.println("\n");
                    break;
                case 2:
                    System.out.println("Departman Adı : ");
                    sc.nextLine();
                    String bilgi = sc.nextLine();
                    int sayac = 0;
                    for (int i = 0; i < departmanlar.length; i++) {
                        if(departmanlar[i].GetDepAd().equals(bilgi)){
                            System.out.println("Departman No : " + departmanlar[i].GetDepNo() + " Departman Adı : " + departmanlar[i].GetDepAd());
                            System.out.println("Personeller;");
                            for (int j = 0; j < departmanlar[i].personeller.length; j++)
                                System.out.println("\t" + (j+1) + ". Personel : " + departmanlar[i].personeller[j].GetPerID() + " " + departmanlar[i].personeller[j].GetPerAd() + " " + departmanlar[i].personeller[j].GetPerSoyad() + " " + departmanlar[i].personeller[j].GetPerYas());
                            for (int j = 0; j < departmanlar[i].servisler.length; j++)
                                System.out.println("\t" + (j+1) + ". Servis : " + departmanlar[i].servisler[j].GetServisID() + " " + departmanlar[i].servisler[j].GetServisGuzergah() + "\n");
                            sayac++;
                            break;
                        }
                    }
                    if (sayac == 0)
                        System.out.println("Departman Bulunamadı.");
                    System.out.println("\n");
                    break;
                case 3:
                    System.out.println("Personel Adı : ");
                    sc.nextLine();
                    bilgi = sc.nextLine();
                    sayac = 0;
                    for (int i = 0; i < departmanlar.length; i++) {
                        for (int j = 0; j < departmanlar[i].personeller.length; j++) {
                            if(departmanlar[i].personeller[j].GetPerAd().equals(bilgi)){
                                System.out.println("Departman = " + departmanlar[i].GetDepAd() + ", " + departmanlar[i].personeller[j].GetPerID() + " " + departmanlar[i].personeller[j].GetPerAd() + " " + departmanlar[i].personeller[j].GetPerSoyad() + " " + departmanlar[i].personeller[j].GetPerYas());
                                sayac++;
                            }
                        }
                    }
                    if (sayac == 0)
                        System.out.println("Personel Bulunamadı.");
                    System.out.println("\n");
                    break;
                case 4:
                    System.out.println("Güzergah Adı : ");
                    sc.nextLine();
                    bilgi = sc.nextLine();
                    sayac = 0;
                    for (int i = 0; i < departmanlar.length; i++) {
                        for (int j = 0; j < departmanlar[i].servisler.length; j++) {
                            if(departmanlar[i].servisler[j].GetServisGuzergah().equals(bilgi)){
                                System.out.println("Departman = " + departmanlar[i].GetDepAd() + ", " + departmanlar[i].servisler[j].GetServisID() + " " + departmanlar[i].servisler[j].GetServisGuzergah());
                                sayac++;
                            }
                        }
                    }
                    if (sayac == 0)
                        System.out.println("Servis Bulunamadı.");
                    System.out.println("\n");
                    break;
                case 5:
                    int enK = departmanlar[0].personeller[0].GetPerYas(), enB = departmanlar[0].personeller[0].GetPerYas();
                    for (int i = 0; i < departmanlar.length; i++) {
                        for (int j = 0; j < departmanlar[i].personeller.length; j++) {
                            if(departmanlar[i].personeller[j].GetPerYas() < enK)
                                enK = departmanlar[i].personeller[j].GetPerYas();
                            if(departmanlar[i].personeller[j].GetPerYas() > enB)
                                enB = departmanlar[i].personeller[j].GetPerYas();
                        }
                    }
                    for (int i = 0; i < departmanlar.length; i++) {
                        for (int j = 0; j < departmanlar[i].personeller.length; j++) {
                            if(departmanlar[i].personeller[j].GetPerYas() == enK)
                                System.out.println("Departman = " + departmanlar[i].GetDepAd() + ", " + departmanlar[i].personeller[j].GetPerID() + " " + departmanlar[i].personeller[j].GetPerAd() + " " + departmanlar[i].personeller[j].GetPerSoyad() + " " + departmanlar[i].personeller[j].GetPerYas() + " (En küçük)");
                            if(departmanlar[i].personeller[j].GetPerYas() == enB)
                                System.out.println("Departman = " + departmanlar[i].GetDepAd() + ", " + departmanlar[i].personeller[j].GetPerID() + " " + departmanlar[i].personeller[j].GetPerAd() + " " + departmanlar[i].personeller[j].GetPerSoyad() + " " + departmanlar[i].personeller[j].GetPerYas() + " (En Büyük)");
                        }
                    }
                    System.out.println("\n");
                    break;
            }
        }while (secim != 6);



    }

}
