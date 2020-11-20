#include<stdio.h>
#include<math.h>
#include<stdlib.h>
#include<allegro5/allegro.h>
#include<allegro5/allegro_primitives.h>
#include<allegro5/allegro_font.h>
#include<allegro5/allegro_ttf.h>
int noktaSayisi = 0;

struct Nokta{ //Nokta structı
    float X;
    float Y;
};

struct Daire{ //Daire structı
    struct Nokta merkez;
    float yariCap;
};

struct Nokta enUzakNokta;
struct Daire anaDaire;

float OklidBul(struct Nokta a,struct Nokta b){ //Öklid uzaklığı bulmak
    return sqrt(pow(a.X - b.X,2) + pow(a.Y - b.Y,2));
}

int NoktaSayisiBul(){ //Nokta sayısını bulmak
    FILE *okunacakDosya = fopen("noktalar1.txt","r");
    if(okunacakDosya == NULL){
        printf("Dosya acilamadi");
        return -1;
    }
    char c[11];
    int satirSayisi=0;
    while((fgets(c,100,okunacakDosya)) != NULL)
        satirSayisi++;
    fclose(okunacakDosya);
    return satirSayisi;
}

float DeterminantBul(float matris[3][3]){ //Matrisin determinantını bulmak
    float det = 0.0;
    float ekSatir[2][3] = {{matris[0][0],matris[0][1],matris[0][2]},{matris[1][0],matris[1][1],matris[1][2]}};
    det = ((matris[0][0] * matris[1][1] * matris[2][2]) + (matris[1][0] * matris[2][1] * ekSatir[0][2]) + (matris[2][0] * ekSatir[0][1] * ekSatir[1][2])) - ((matris[0][2] * matris[1][1] * matris[2][0]) + (matris[1][2] * matris[2][1] * ekSatir[0][0]) + (matris[2][2] * ekSatir[0][1] * ekSatir[1][0]));
    return det;
}

struct Daire UcNoktaOrtaNoktaKontrol(struct Nokta i,struct Nokta j,struct Nokta k){ //3 noktanın orta noktasını dairenin merkezi yapmak
    float matris1[3][3] = {{i.X,i.Y,1},{j.X,j.Y,1},{k.X,k.Y,1}}; //O(1)
    float a = DeterminantBul(matris1); //O(5)
    float matris2[3][3] = {{pow(i.X,2)+pow(i.Y,2),i.Y,1},{pow(j.X,2)+pow(j.Y,2),j.Y,1},{pow(k.X,2)+pow(k.Y,2),k.Y,1}}; //O(1)
    float d = DeterminantBul(matris2); //O(5)
    float matris3[3][3] = {{pow(i.X,2)+pow(i.Y,2),i.X,1},{pow(j.X,2)+pow(j.Y,2),j.X,1},{pow(k.X,2)+pow(k.Y,2),k.X,1}}; //O(1)
    float e = DeterminantBul(matris3); //O(5)
    float matris4[3][3] = {{pow(i.X,2)+pow(i.Y,2),i.X,i.Y},{pow(j.X,2)+pow(j.Y,2),j.X,j.Y},{pow(k.X,2)+pow(k.Y,2),k.X,k.Y}}; //O(1)
    float f = DeterminantBul(matris4); //O(5)
    struct Daire tmpDaire; //O(1)
    tmpDaire.merkez.X = d/(2*a); //O(1)
    tmpDaire.merkez.Y = (-1.0*e)/(2*a); //O(1)
    tmpDaire.yariCap = sqrt(((pow(d,2)+pow(e,2))/(4*pow(a,2)))+(f/a)); //O(1)
    return tmpDaire; //O(1)
}

struct Daire IkiNoktaOrtaNoktaKontrol(struct Nokta a,struct Nokta b){ //2 noktanın orta noktasını dairenin merkezi yapmak
    struct Daire tempDaire;
    tempDaire.merkez.X = (a.X+b.X)/2;
    tempDaire.merkez.Y = (a.Y+b.Y)/2;
    tempDaire.yariCap = OklidBul(a,b)/2.0;
    return tempDaire;
}

int NoktalarIcerdeMi(struct Daire D,struct Nokta N[noktaSayisi]){ //Tüm noktalar dairenin içerisinde mi
    for(int i=0;i<noktaSayisi;i++)
        if(OklidBul(D.merkez,N[i]) > D.yariCap)
            return 0;
    return 1;
}

float kordinatiPixeleCevirX(float x,int ekranBoyutuX){ //noktaları ekran formatına uyarlamak(X)
    return (x*ekranBoyutuX/64) + ekranBoyutuX / 2;
}

float kordinatiPixeleCevirY(float y, int ekranBoyutuY){ //noktaları ekran formatına uyarlamak(Y)
    return (-y*ekranBoyutuY/64) + ekranBoyutuY / 2;
}

void AnaDaireCiz(int ekranBoyutu){ //Ana daire çizmek
    al_draw_line(kordinatiPixeleCevirX(anaDaire.merkez.X,ekranBoyutu),kordinatiPixeleCevirY(anaDaire.merkez.Y,ekranBoyutu),kordinatiPixeleCevirX(enUzakNokta.X,ekranBoyutu),kordinatiPixeleCevirY(enUzakNokta.Y,ekranBoyutu),al_map_rgb(253, 121, 168),2);
    char xNoktasi[100],yNoktasi[100],yariCap[100];
    sprintf(xNoktasi,"X : %f",anaDaire.merkez.X);
    sprintf(yNoktasi,"Y : %f",anaDaire.merkez.Y);
    sprintf(yariCap,"Yaricap : %f",anaDaire.yariCap);
    al_draw_text(al_load_font("arial.ttf",20,1),al_map_rgb(255,255,255),48,48,0,xNoktasi);
    al_draw_text(al_load_font("arial.ttf",20,1),al_map_rgb(255,255,255),48,72,0,yNoktasi);
    al_draw_text(al_load_font("arial.ttf",20,1),al_map_rgb(255,255,255),50,96,0,yariCap);

    al_draw_filled_circle(kordinatiPixeleCevirX(anaDaire.merkez.X,ekranBoyutu),kordinatiPixeleCevirY(anaDaire.merkez.Y,ekranBoyutu),6,al_map_rgb(255, 242, 0));//merkez
    al_draw_circle(kordinatiPixeleCevirX(anaDaire.merkez.X,ekranBoyutu),kordinatiPixeleCevirY(anaDaire.merkez.Y,ekranBoyutu),anaDaire.yariCap*ekranBoyutu/64,al_map_rgb(52, 152, 219),2);//ana daire
    printf("Dairenin merkezi : (%f , %f)\nDairenin yaricapi : %f\n",anaDaire.merkez.X,anaDaire.merkez.Y,anaDaire.yariCap);
}

void KoordinatDuzlemi(int tuvalBoyutu, int koordinatSayisi){ //Koordinat düzlemi çizmek
    char sa[3];
	int xx = -32,yy = 32;
	for(int i=0;i<=tuvalBoyutu;i+=koordinatSayisi){
        al_draw_line(i,(tuvalBoyutu/2)-10,i,tuvalBoyutu/2+10,al_map_rgb(255,255,255),2);//X ekseni
        al_draw_line(i,0,i,tuvalBoyutu,al_map_rgb(75, 75, 75),1);//Y ekseni
        al_draw_line(0,i,tuvalBoyutu,i,al_map_rgb(75, 75, 75),1);//X ekseni
        al_draw_line((tuvalBoyutu/2)-10,i,tuvalBoyutu/2+10,i,al_map_rgb(255,255,255),2);//Y ekseni
        if(xx != 0){
            sprintf(sa,"%d",xx);
            al_draw_text(al_load_font("arial.ttf",12,1),al_map_rgb(255,255,255),i,(tuvalBoyutu/2)+13,1,sa);
            sprintf(sa,"%d",yy);
            al_draw_text(al_load_font("arial.ttf",12,1),al_map_rgb(255,255,255),(tuvalBoyutu/2)-20,i-8,1,sa);
        }
        xx++;
        yy--;
	}
	al_draw_line(tuvalBoyutu/2,0,tuvalBoyutu/2,tuvalBoyutu,al_map_rgb(255,255,255),2);
	al_draw_line(0,tuvalBoyutu/2,tuvalBoyutu,tuvalBoyutu/2,al_map_rgb(255,255,255),2);
}

void NoktalariCiz(struct Nokta noktalar[noktaSayisi], int tuvalBoyutu){ // Noktaları çizmek
    for(int i=0;i<noktaSayisi;i++){
        al_draw_filled_circle(kordinatiPixeleCevirX(noktalar[i].X,tuvalBoyutu),kordinatiPixeleCevirY(noktalar[i].Y,tuvalBoyutu),5,al_map_rgb(214, 48, 49));
        printf("%f , %f \n",noktalar[i].X,noktalar[i].Y);
    }
}

void NoktalariSirala(struct Nokta noktalar[noktaSayisi]){ //Noktaları sıralamak
    for(int i=0;i<noktaSayisi;i++){
        if(noktalar[i].X == enUzakNokta.X && noktalar[i].Y == enUzakNokta.Y){
            struct Nokta tmpNokta = noktalar[0];
            noktalar[0] = enUzakNokta;
            noktalar[i] = tmpNokta;
        }
    }

    float enKisaOklid = OklidBul(noktalar[0],noktalar[1]);
    int enKisaYer;

    for(int i=0;i<noktaSayisi-2;i++){//En ucak noktayı spline noktalarının ilk elemanı yapmak
    enKisaYer = i;
    enKisaOklid = OklidBul(noktalar[i],noktalar[i+1]);

        for(int j=i+2;j<noktaSayisi;j++){
            if(OklidBul(noktalar[i],noktalar[j]) < enKisaOklid){
                enKisaYer = j;
                enKisaOklid = OklidBul(noktalar[i],noktalar[j]);
            }
        }
    if(enKisaYer == i+1 || enKisaYer == i)
        continue;
    struct Nokta tmpNokta = noktalar[i+1];
    noktalar[i+1] = noktalar[enKisaYer];
    noktalar[enKisaYer] = tmpNokta;
    }
}

void IkiNoktaKontrol(struct Nokta noktalar[noktaSayisi]){ //İkili kombinasyon kontrolü
    for(int i=0;i<noktaSayisi;i++){
        for(int j=i+1;j<noktaSayisi;j++){
            struct Daire tempDaire = IkiNoktaOrtaNoktaKontrol(noktalar[i],noktalar[j]);
            if(tempDaire.yariCap < anaDaire.yariCap && NoktalarIcerdeMi(tempDaire,noktalar) == 1){
                anaDaire = tempDaire;
                enUzakNokta = noktalar[i];
            }
        }
    }
}

void UcNoktaKontrol(struct Nokta noktalar[noktaSayisi]){ //Üçlü kombinasyon kontrolü // O((n^3)*29*n*2) = O(n^4)
    for(int i=0;i<noktaSayisi;i++){ //O(n)
        for(int j=i+1;j<noktaSayisi;j++){ //O(n)
            for(int k=j+1;k<noktaSayisi;k++){ //O(n)
                struct Daire tempDaire = UcNoktaOrtaNoktaKontrol(noktalar[i],noktalar[j],noktalar[k]); //O(29)
                if(tempDaire.yariCap < anaDaire.yariCap && NoktalarIcerdeMi(tempDaire,noktalar) == 1){ //O(n)
                    anaDaire = tempDaire; //O(1)
                    enUzakNokta = noktalar[i]; //O(1)
                }
            }
        }
    }
}

int tI(int i,int k,int n){ //Zaman aralığını oluşturmak
    float s = 0;
    if(i < k)
        s = 0;
    else if(k <= i && i <= n)
        s = i-k+1;
    else if(i > n)
        s = n-k+2;
    return s;
}

float N_i_k(float u,int ti[],int i,int k){ // Spline formülü

    if(k == 1){
        if(ti[i]<=u && u<ti[i+1])
            return 1;
        else
            return 0;
    }
    float K1pay = (u-ti[i]) * N_i_k(u,ti,i,k-1);
    float K1payda = ti[i+k-1] - ti[i];
    float K2pay = (ti[i+k]-u) * N_i_k(u,ti,i+1,k-1);
    float K2payda = ti[i+k] - ti[i+1];

    float sonuc1 = K1pay/K1payda;
    float sonuc2 = K2pay/K2payda;

    if(K1payda == 0)
        sonuc1 = 0;
    if(K2payda == 0)
        sonuc2 = 0;

    return sonuc1 + sonuc2;
}

void B_SplineCiz(struct Nokta noktalar[], int k ,int tuvalBoyutu){ // Spline çizmek

    int n = noktaSayisi-1;
    int *ti =(int*)malloc(sizeof(int)*(n+k));//zaman aralığı
    for(int i = 0; i < n+k; i++){
        ti[i] = tI(i,k,n);
    }//tüm zaman aralığını hesaplama

    int SPAdet = (n-k+2)*100;
    struct Nokta* SP = (struct Nokta*)malloc(sizeof(struct Nokta)*(SPAdet));
    int ss = 0;

    for(int i=0;i<SPAdet;i++){
        SP[i].X = 0.0;
        SP[i].Y = 0.0;
    }

    for(float u=0.0; u < (n-k+2); u+=0.01){
        for(int i = 0; i < noktaSayisi; i++){
            SP[ss].X += N_i_k(u,ti,i,k) * noktalar[i].X;
            SP[ss].Y += N_i_k(u,ti,i,k) * noktalar[i].Y;
            /*
            u : anlık zaman
            ti : zaman aralığı dizisi
            i : kaçıncı nokta olduğu
            k : derece
            */
        }
        //printf("SP[%d].X: %f , SP[%d].Y: %f\n",ss,SP[ss].X,ss,SP[ss].Y);
        ss++;
    }

    int index = 0;
    int r[30] = {255,255,255,255,255,255,205,155,103,50,0,0,0,0,0,0,0,0,0,0,0,50,100,150,200,255,255,255,255,255};
    int g[30] = {0,0,0,0,0,0,0,0,0,0,0,50,100,150,200,255,255,255,255,255,255,255,255,255,255,255,200,150,100,50};
    int b[30] = {0,50,100,150,200,255,255,255,255,255,255,255,255,255,255,255,200,150,100,50,0,0,0,0,0,0,0,0,0,0};
    al_draw_circle(kordinatiPixeleCevirX(SP[0].X,tuvalBoyutu),kordinatiPixeleCevirY(SP[0].Y,tuvalBoyutu),6,al_map_rgb(255, 255, 255),2);//Splineın başlangıcı
    for(int i=1; i < SPAdet; i++){
        if(i % (SPAdet/30) == 0 && index < 29)
            index++;
        al_draw_line(kordinatiPixeleCevirX(SP[i-1].X,tuvalBoyutu),kordinatiPixeleCevirY(SP[i-1].Y,tuvalBoyutu),kordinatiPixeleCevirX(SP[i].X,tuvalBoyutu),kordinatiPixeleCevirY(SP[i].Y,tuvalBoyutu),al_map_rgb(r[index],g[index],b[index]),2);
        al_flip_display();
    }
}

int main()
{
    int tuvalBoyutu = 1024;
    int koordinatSayisi = tuvalBoyutu/64;

    noktaSayisi = NoktaSayisiBul();

    FILE *dosya = fopen("noktalar1.txt","r");

    if(dosya == NULL){
        printf("Dosya acilamadi");
        return 0;
    }

    struct Nokta noktalar[noktaSayisi];

    char c[11];
    int k = 0;
    while((fgets(c,11,dosya)) != NULL){
        noktalar[k].X = atof(strtok(c,","));
        noktalar[k].Y = atof(strtok(NULL,","));
        k++;
    }

    al_init_primitives_addon();
    al_init_font_addon();
    al_init_ttf_addon();
	al_init();
	al_create_display(tuvalBoyutu, tuvalBoyutu);

    anaDaire.yariCap = (float)tuvalBoyutu;

    IkiNoktaKontrol(noktalar); //İkili kombinasyon kontrolü
    UcNoktaKontrol(noktalar); //Üçlü kombinasyon kontrolü

    //NoktalariSirala(noktalar);//Noktaları sıralamak

    KoordinatDuzlemi(tuvalBoyutu,koordinatSayisi); //Koordinat Düzlemi çizmek
    NoktalariCiz(noktalar,tuvalBoyutu); // Noktaları çizmek

    AnaDaireCiz(tuvalBoyutu); //Ana Daireyi ekranda göstermek

    B_SplineCiz(noktalar,3,tuvalBoyutu);//Spline Çizmek

    al_flip_display();
	system("pause");
	return 0;
}
