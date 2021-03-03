#include <stdio.h>
#include <stdlib.h>
int fSatirSayisi = 0;

struct Point3D{
    float x,y,z;
};

int NoktaSayisi(){
    FILE *okunacakDosya = fopen("nesne.obj","r");
    if(okunacakDosya == NULL){
        printf("Dosya acilamadi");
        return -1;
    }
    char c[35];
    int satirSayisi=0;
    while((fgets(c,100,okunacakDosya)) != NULL){
        if(c[0] == 'v')
            satirSayisi++;
        if(c[0] == 'f')
            fSatirSayisi++;
    }
    fclose(okunacakDosya);
    return satirSayisi;
}

void NoktalariCevir(struct Point3D nokta[], int noktaSayisi, float x, float y, float z){
    for(int i=0;i<noktaSayisi;i++){
        nokta[i].x += x;
        nokta[i].y += y;
        nokta[i].z += z;
    }
}

void DosyayaYaz(struct Point3D nokta[], int noktaSayisi){
    char fVerileri[fSatirSayisi][35];

    FILE *okunacakDosya = fopen("nesne.obj","r");
    if(okunacakDosya == NULL){
        printf("Dosya acilamadi");
        return -1;
    }
    char c[35], part[10];
    int i=0;
    while((fgets(c,100,okunacakDosya)) != NULL){
        if(c[0] == 'f')
            strcpy(fVerileri[i++],c);
    }
    fclose(okunacakDosya);
    i = 0;
    FILE *yazilacakDosya = fopen("cevrilmis.obj","w");
    for(int j = 0;j < fSatirSayisi+noktaSayisi+1;j++){
        if(j < noktaSayisi)
            fprintf(yazilacakDosya,"v %f %f %f\n",nokta[j].x,nokta[j].y,nokta[j].z);
        else if(j == noktaSayisi)
            fprintf(yazilacakDosya,"\n");
        else
            fprintf(yazilacakDosya,"%s",fVerileri[i++]);
    }
    fclose(yazilacakDosya);
}

struct Point3D KutleMerkezi(struct Point3D nokta[], int noktaSayisi){
    struct Point3D tempNokta;
    float x=0,y=0,z=0;
    for(int i=0;i<noktaSayisi;i++){
        x += nokta[i].x;
        y += nokta[i].y;
        z += nokta[i].z;
    }
    tempNokta.x = x/noktaSayisi;
    tempNokta.y = y/noktaSayisi;
    tempNokta.z = z/noktaSayisi;
    return tempNokta;
}

void main()
{
    int noktaSayisi = NoktaSayisi();
    struct Point3D noktalar[noktaSayisi];

    FILE *okunacakDosya = fopen("nesne.obj","r");
    char c[35],part[10];
    for(int i=0;i<noktaSayisi;i++){
        fgets(c,100,okunacakDosya);
        strcpy(part,strtok(c," "));
        noktalar[i].x = atof(strtok(NULL," "));
        noktalar[i].y = atof(strtok(NULL," "));
        noktalar[i].z = atof(strtok(NULL," "));
    }
    fclose(okunacakDosya);

    struct Point3D ortNokta = KutleMerkezi(noktalar,noktaSayisi);
    printf("Kutle Merkezi X: %f , Y: %f , Z: %f\n\n",ortNokta.x,ortNokta.y,ortNokta.z);

    char x[10],y[10],z[10];
    float x1,y1,z1;
    printf("Cevirmek icin sirasiyla istenen degerleri girin\n");
    printf("X : ");
    scanf("%s",x);
    x1 = atof(x);

    printf("Y : ");
    scanf("%s",y);
    y1 = atof(y);

    printf("Z : ");
    scanf("%s",z);
    z1 = atof(z);

    NoktalariCevir(noktalar,noktaSayisi,x1,y1,z1);
    DosyayaYaz(noktalar,noktaSayisi);
}
