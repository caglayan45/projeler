#include <stdio.h>
#include <stdlib.h>

struct Ogrenci{
    char adi[50];
    int kimlik,gecmeDurumu;
    float vizeNotu,finalNotu,dersNotu;
};

struct Ogrenci Ogrenciler[100];
int i=0;

void KayitEkle(char ad[50],int kimlik,float vizeNotu,float finalNotu){
    if(vizeNotu < 0 || vizeNotu > 100 || finalNotu < 0 || finalNotu > 100)
    {
        printf("\nKayit yapilmadi, gecerli bir not giriniz.\n");
        return;
    }
    for(int j=0;j<i;j++)
        if(Ogrenciler[j].kimlik == kimlik){
            printf("\nKayit yapilmadi, bu kimlikte bir kayit bulunuyor.\n");
            return;
        }
    strcpy(Ogrenciler[i].adi,ad);
    Ogrenciler[i].kimlik = kimlik;
    Ogrenciler[i].vizeNotu = vizeNotu;
    Ogrenciler[i].finalNotu = finalNotu;
    Ogrenciler[i].dersNotu = (vizeNotu * 0.4) + (finalNotu * 0.6);
    if(Ogrenciler[i].dersNotu >= 60)
        Ogrenciler[i].gecmeDurumu = 1;
    else
        Ogrenciler[i].gecmeDurumu = 0;
    i++;
}

void KayitBul(int kimlik){
    for(int j=0;j<i;j++){
        if(Ogrenciler[j].kimlik == kimlik){
            if(Ogrenciler[j].gecmeDurumu == 1)
                printf("--------------------\nAdi : %s\nKimligi : %d\nVize Notu : %f\nFinal Notu : %f\nDers Notu : %f\nGecme Durumu : Basarili\n--------------------\n",Ogrenciler[j].adi,Ogrenciler[j].kimlik,Ogrenciler[j].vizeNotu,Ogrenciler[j].finalNotu,Ogrenciler[j].dersNotu);
            else
                printf("--------------------\nAdi : %s\nKimligi : %d\nVize Notu : %f\nFinal Notu : %f\nDers Notu : %f\nGecme Durumu : Basarisiz\n--------------------\n",Ogrenciler[j].adi,Ogrenciler[j].kimlik,Ogrenciler[j].vizeNotu,Ogrenciler[j].finalNotu,Ogrenciler[j].dersNotu);
            return;
        }
    }
    printf("\nAranan kisi bulunamadi.\n");
}

void Istatistik(){
    int basariliKisiSayisi = 0, basarisizKisiSayisi = 0;
    float ortalama = 0.0;
    for(int j=0;j<i;j++){
        if(Ogrenciler[j].gecmeDurumu == 1)
            basariliKisiSayisi++;
        else
            basarisizKisiSayisi++;
        ortalama += Ogrenciler[j].dersNotu;
    }
    if(i == 0){
        printf("\nHenuz kayit eklenmedi.\n");
        return;
    }
    printf("********************\nBasarili kisi sayisi : %d\nBasarisiz kisi sayisi : %d\nOrtalama : %f\nKisi Sayisi : %d\n********************\n",basariliKisiSayisi,basarisizKisiSayisi,ortalama/i,i);
}

void main()
{
    char secim,adi[50],veri[50];
    int kimlik;
    float vizeNotu,finalNotu;
    while(1){
        printf("\nYeni Kayit Ekle : Y/y\nBir Kayit Bul : B/b\nTemel Istatistik : T/t\nCikis : C/c\n");
        secim = getchar();
        system("CLS");
        if(secim == 'C' || secim == 'c')
            return;
        if(secim == 'Y' || secim == 'y'){
            printf("Adi : ");
            getchar();
            scanf("%[^\n]",veri);
            strcpy(adi,veri);
            int kontrol = 0;

            printf("Kimligi : ");
            scanf("%s",&veri);
            for(int j=0;j<strlen(veri);j++)
                if(!isdigit(veri[j]))
                    kontrol = 1;

            if(kontrol == 1){
                do{
                kontrol=0;
                printf("Hata kimlik degeri numeric olmali : ");
                scanf("%s",&veri);
                for(int j=0;j<strlen(veri);j++)
                    if(!isdigit(veri[j]))
                        kontrol = 1;
                }while(kontrol);
            }
            kimlik = atoi(veri);

            printf("Vize Notu : ");
            scanf("%s",&veri);
            for(int j=0;j<strlen(veri);j++)
                if(!isdigit(veri[j]))
                    kontrol = 1;
            if(kontrol == 1){
                do{
                kontrol=0;
                printf("Hata Vize Notu degeri numeric olmali : ");
                scanf("%s",&veri);
                for(int j=0;j<strlen(veri);j++)
                    if(!isdigit(veri[j]))
                        kontrol = 1;
                }while(kontrol);
            }
            vizeNotu = atof(veri);

            printf("Final Notu : ");
            scanf("%s",&veri);
            for(int j=0;j<strlen(veri);j++)
                if(!isdigit(veri[j]))
                    kontrol = 1;

            if(kontrol == 1){
                do{
                kontrol=0;
                printf("Hata Final Notu degeri numeric olmali : ");
                scanf("%s",&veri);
                for(int j=0;j<strlen(veri);j++)
                    if(!isdigit(veri[j]))
                        kontrol = 1;
                }while(kontrol);
            }
            finalNotu = atof(veri);

            KayitEkle(adi,kimlik,vizeNotu,finalNotu);

        }else if(secim == 'B' || secim == 'b'){
            printf("Aranan kisinin kimligi : ");
            scanf("%s",&veri);
            kimlik = atoi(veri);

            KayitBul(kimlik);

        }else if(secim == 'T' || secim == 't'){
            Istatistik();
        }
        getchar();
    }
}
