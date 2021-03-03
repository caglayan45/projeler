#include <stdio.h>
#include <stdlib.h>
#include <locale.h>
#include <string.h>

struct Liste{
    char *kelime;
    int adet;
    struct Liste *sonraki;
};

struct Liste *ilk=NULL,*son=NULL;

void Ekle(char *kelime, int adet){
    struct Liste *yeni = (struct Liste*) malloc(sizeof(struct Liste));
    yeni->adet = adet;
    yeni->kelime = (char*)malloc(sizeof(char)*strlen(kelime));
    strcpy(yeni->kelime,kelime);

    if(ilk == NULL || adet >= ilk->adet){
        BasaEkle(yeni);
    }else if(adet <= son->adet){
        SonaEkle(yeni);
    }else{//araya ekle
        struct Liste *onceki = ilk,*dolas = ilk->sonraki;
        if(dolas == NULL){
            SonaEkle(yeni);
            return 0;
        }

        while(dolas != NULL){
            if(dolas->adet <= adet){
                onceki->sonraki = yeni;
                yeni->sonraki = dolas;
                return 0;
            }
            onceki = dolas;
            dolas = dolas->sonraki;
        }
    }
}

void BasaEkle(struct Liste *temp){
    if(ilk == NULL){
        ilk = temp;
        ilk->sonraki = NULL;
        son = ilk;
    }else{
        temp->sonraki = ilk;
        ilk = temp;
    }
}

void SonaEkle(struct Liste *temp){
    son->sonraki = temp;
    son = temp;
    son->sonraki = NULL;
}

void Listele(){
    struct Liste *temp;
    temp = ilk;
    while(temp != NULL){
        printf("%3d  :  %s\n",temp->adet,temp->kelime);
        temp = temp->sonraki;
    }
}

int KelimeAra(char *kelime1){
    struct Liste *temp;
    temp = ilk;
    while(temp != NULL){
        if(strcmp(temp->kelime,kelime1) == 0)
            return 1;
        temp = temp->sonraki;
    }
    return 0;
}

void main()
{
    setlocale(LC_ALL, "Turkish");

    FILE *dosya;
    if((int)(dosya = fopen("metin.txt", "r")) == 0)
        printf("Dosya bulunamadi");
    else
    {
        char *tempKelime = (char*)malloc(sizeof(char));
        char *arananKelime;
        int uzunluk = 0,yer = 0;
        while(!feof(dosya))
        {
            char karakter = fgetc(dosya);
            yer++;
            if(karakter != ' ' && karakter != '\n'){
                tempKelime[uzunluk++] = karakter;
                tempKelime = realloc(tempKelime,uzunluk+1);
            }else{
                tempKelime[uzunluk] = '\0';
                arananKelime = (char*)malloc(sizeof(char)*(uzunluk+1));
                strcpy(arananKelime,tempKelime);
                if(KelimeAra(arananKelime) != 1)
                {
                    int sayac = 1;
                    free(tempKelime);
                    tempKelime = (char*)malloc(sizeof(char));
                    uzunluk = 0;
                    while(!feof(dosya)){
                        karakter = fgetc(dosya);
                        if(karakter != ' ' && karakter != '\n'){
                            tempKelime[uzunluk++] = karakter;
                            tempKelime = realloc(tempKelime,uzunluk+1);
                        }else{
                            tempKelime[uzunluk] = '\0';

                            if(strcmp(arananKelime,tempKelime) == 0){
                                sayac++;
                            }
                            free(tempKelime);
                            tempKelime = (char*)malloc(sizeof(char));
                            uzunluk = 0;
                        }
                    }
                    tempKelime[uzunluk-1] = '\0';
                    if(strcmp(arananKelime,tempKelime) == 0)
                        sayac++;

                    Ekle(arananKelime,sayac);
                    free(arananKelime);
                    fseek(dosya,yer,SEEK_SET);
                }
                free(tempKelime);
                tempKelime = (char*)malloc(sizeof(char));
                uzunluk = 0;
            }
        }
        tempKelime[uzunluk-1] = '\0';
        arananKelime = (char*)malloc(sizeof(char)*(uzunluk+1));
        strcpy(arananKelime,tempKelime);
        if(KelimeAra(arananKelime) != 1)
        {
            Ekle(arananKelime,1);
        }
    }
    fclose(dosya);
    Listele();
}
