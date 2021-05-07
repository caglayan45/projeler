#include <stdio.h>
#include <stdlib.h>
#include <string.h>
#include <time.h>
#include <stdbool.h>

struct PGM_Imge{
    unsigned char *veri;
    int genislik, yukseklik, maxDeger;
};

unsigned char* readPGM(char *path, int *m/*geniþlik*/, int *n/*yükseklik*/, int *v/*max*/)
{
    FILE *fp = fopen(path, "r");
    char tip[3],aciklama[100];

    fscanf(fp,"%[^\n] ",tip);
    if(strcmp(tip,"P2") != 0)
        return "Geçersiz dosya tipi.";

    fscanf(fp,"%[^\n] ",aciklama);
    fscanf(fp,"%d %d %d ",m,n,v);

    unsigned char *data = (unsigned char*) malloc(((*m)*(*n)*sizeof(unsigned char)*4));

    int i=0;
    unsigned char c;
    do {
        fscanf(fp,"%c",&c);
        data[i++] = c;
    } while(!feof(fp));
    data[i-1] = '\0';
    fclose(fp);
    return data;
}

void writePGM(char *path, unsigned char *data, int m, int n, int v)
{
    FILE *fp = fopen(path, "w");
    fprintf(fp, "P2\n# aciklama\n");
    fprintf(fp, "%d %d\n", m, n);
    fprintf(fp, "%d\n", v);
    fprintf(fp, "%s", data);
    fclose(fp);
}

void createRandomPGM(char *path, int m, int n, int v)
{
    srand(time(NULL));
    FILE *fp = fopen(path, "w");
    fprintf(fp, "P2\n# aciklama\n");
    fprintf(fp, "%d %d\n", m, n);
    fprintf(fp, "%d\n", v);
    for (int i=1; i<=m*n; i++){
        fprintf(fp, "%d",rand()%(v+1));
        if(i % m == 0)
            fprintf(fp,"\n");
        else
            fprintf(fp," ");
    }
    fclose(fp);
}

void *ConvertToInt(unsigned char *data, int ncols, int nrows, int sayilar[]){
    unsigned char *temp = (unsigned char*) malloc((ncols*nrows*sizeof(unsigned char)*4));
    sprintf(temp,"%s",data);
    char *piksel = strtok(temp," \n");
    int j = 0;
    while(piksel != NULL){
        sayilar[j++] = atoi(piksel);
        piksel = strtok(NULL," \n");
    }
}

void threshold(unsigned char *data, int ncols, int nrows, unsigned int thresh, int max)
{
    int sayilar[ncols*nrows];
    ConvertToInt(data,ncols,nrows,sayilar);
    data[0] = '\0';
    int j = 0;
    for(int i=1; i<=ncols*nrows; i++,j++)
    {
        if (sayilar[j] >= thresh){
            if(i % ncols == 0)
                sprintf(data,"%s%d\n",data,max);
            else
                sprintf(data,"%s%d ",data,max);
        }
        else{
            if(i % ncols == 0)
                sprintf(data,"%s0\n",data);
            else{
                sprintf(data,"%s0 ",data);
            }
        }
    }
}

void scalePGM(unsigned char *data, int *m, int *n, int scale)
{
    int sayilar[(*m)*(*n)];
    ConvertToInt(data,(*m),(*n),sayilar);
    data[0] = '\0';
    for(int i=0; i<(*n); i++) {
        for(int j=0; j<(*m); j++){
            sayilar[i*(*m)+j] %= scale;

            if(j != (*m)-1)
                sprintf(data,"%s%d ",data,sayilar[i*(*m)+j]);
            else
                sprintf(data,"%s%d",data,sayilar[i*(*m)+j]);
        }
        sprintf(data,"%s\n",data);
    }
}

int* histogram1D(unsigned char *data, int m, int n, int nbins)
{
    int *histogram = (int *) calloc( nbins, sizeof (int));
    int sayilar[m*n];
    ConvertToInt(data,m,n,sayilar);
    for(int i=0; i<nbins; i++)
        histogram[i] = 0;
    for(int i=0; i<m*n; i++)
        histogram[sayilar[i]]++;
    return histogram;
}

double withinClassVariance(int *histogram, int nbins, int threshold)
{
    double w_b, mu_b, v_b;
    statistics(histogram, nbins, 0, threshold-1, &w_b, &mu_b, &v_b);
    double w_f, mu_f, v_f;
    statistics(histogram, nbins, threshold, nbins-1, &w_f, &mu_f,&v_f);
    double v_withinClass = w_b*v_b + w_f*v_f;
    return v_withinClass;
}

void statistics(int *histogram, int nbins, int binFirst, int binLast, double *w, double *mu, double *v)
{
    int histsum = 0;
    for(int i=0;i<nbins;i++)
        histsum += histogram[i];

    int totalweights = 0;
    for(int i=binFirst; i<=binLast; i++)
        totalweights += histogram[i];
    *w = (double)totalweights/histsum;

    int weightedsum =0;
    for(int i=binFirst; i<=binLast; i++)
        weightedsum += histogram[i]*i;

    if(totalweights) {
        *mu = weightedsum/totalweights;
        *v = 0;
        for(int i=binFirst; i<=binLast; i++)
            *v += histogram[i]*(i-(*mu))*(i-(*mu));
        *v = (*v)/(totalweights);
    }
}

void otsu(const int N, char *path_in, char *path_out)
{
    double withinClassVariances[N];
    int ncols, nrows, maxvalue;
    unsigned char* data0 = readPGM(path_in, &ncols, &nrows, &maxvalue);
    scalePGM(data0, &ncols, &nrows, 256/N);

    int *histogram = histogram1D(data0, ncols, nrows, N);

    for (int i=0; i<N; i++)
        withinClassVariances[i] = withinClassVariance(histogram, N,i);

    int min_index = 0;
    double min_val = withinClassVariances[0];

    for (int i=1; i<N; i++) {
        if (withinClassVariances[i] < min_val) {
            min_val = withinClassVariances[i];
            min_index = i;

        }
    }
    threshold(data0, ncols, nrows, min_index, N);
    writePGM(path_out, data0, ncols, nrows,MaxValue(data0,ncols,nrows));
    free(data0);
    free(histogram);
}

enum saltpepper {
SALT, PEPPER, BOTH
};

void add_saltpepper(double pnoise, unsigned char *data, int ncols, int nrows, int maxval, unsigned saltpepper)
{
    srand(time(NULL));
    int nnoise = (pnoise*ncols*nrows)/100;
    bool salt = (saltpepper == 0 || saltpepper == 2)? true : false;
    bool pepper = (saltpepper == 1 || saltpepper == 2)? true : false;
    int sayilar[nrows*ncols];
    ConvertToInt(data,ncols,nrows,sayilar);
    for (int i=0; i<nnoise; i++) {
        switch (saltpepper) {
            case SALT:
                sayilar[rand()%(nrows*ncols)] = 255;
                break;
            case PEPPER:
                sayilar[rand()%(nrows*ncols)] = 0;
                break;
            default:
                if(salt) {
                    sayilar[rand()%(nrows*ncols)] = 255;
                    salt = false;
                    pepper = true;
                }
                else {
                    sayilar[rand()%(nrows*ncols)] = 0;
                    salt = true;
                    pepper = false;
                }
        }
    }
    data[0] = '\0';
    for(int i=0;i<ncols*nrows;i++){
        if(i % ncols != 0 || i == 0)
            sprintf(data,"%s%d ",data,sayilar[i]);
        else
            sprintf(data,"%s%d\n",data,sayilar[i]);
    }
}

int MaxValue(unsigned char *data0,int ncols, int nrows){
    int sayilar[nrows*ncols],max_deger;
    ConvertToInt(data0,ncols,nrows,sayilar);
    max_deger = sayilar[0];
    for(int i=1;i<nrows*ncols;i++)
        if(sayilar[i] > max_deger)
            max_deger = sayilar[i];
    return max_deger;
}

void filter_median(unsigned char *data0, unsigned char *data1, int ncols, int nrows, int N)
{
    int sayilar[ncols*nrows], array[N*N], pikseller[nrows][ncols];
    ConvertToInt(data0,ncols,nrows,sayilar);
    int k = 0;
    for(int i=0;i<nrows;i++){
        for(int j=0;j<ncols;j++)
            pikseller[i][j] = sayilar[k++];
    }

    for(int y=N/2; y<nrows-1; y++)
        for(int x=N/2; x<ncols-1; x++)
        {
            int z=0;
            for(int j=-N/2; j<=N/2; j++) {
                for(int i=-N/2; i<=N/2; i++)
                {
                    array[z]= pikseller[y+j][x+i];
                    z++;
                }
            }

            for(int i=0;i<z-1;i++){
                for(int j=0;j<z-i-1;j++){
                    if(array[j] > array[j+1]){
                        int temp = array[j];
                        array[j] = array[j+1];
                        array[j+1] = temp;
                    }
                }
            }
            pikseller[y][x] = array[N*N/2];
        }
    data1[0] = '\0';
    for(int i=0;i<nrows;i++){
       for(int j=0;j<ncols;j++){
            sprintf(data1,"%s%d ",data1,pikseller[i][j]);
       }
       sprintf(data1,"%s\n",data1);
    }
}

void filter_mean(unsigned char *data0, unsigned char *data1, int ncols, int nrows, int N)
{
    int sayilar[ncols*nrows], array[N*N], pikseller[nrows][ncols];
    ConvertToInt(data0,ncols,nrows,sayilar);
    int k = 0;
    for(int i=0;i<nrows;i++){
        for(int j=0;j<ncols;j++)
            pikseller[i][j] = sayilar[k++];
    }

    for(int y=N/2; y<nrows-1; y++)
        for(int x=N/2; x<ncols-1; x++)
        {
            int toplam = 0;
            for(int j=-N/2; j<=N/2; j++) {
                for(int i=-N/2; i<=N/2; i++)
                    toplam += pikseller[y+j][x+i];
            }
            pikseller[y][x] = toplam/(N*N);
        }
    data1[0] = '\0';
    for(int i=0;i<nrows;i++){
       for(int j=0;j<ncols;j++){
            sprintf(data1,"%s%d ",data1,pikseller[i][j]);
       }
       sprintf(data1,"%s\n",data1);
    }
}

int main()
{
    struct PGM_Imge *deneme = (struct PGM_Imge*) malloc(sizeof(struct PGM_Imge));
    //1. ister
    deneme->veri = readPGM("deneme1.pgm",&deneme->genislik,&deneme->yukseklik,&deneme->maxDeger);
    //2. ister
    writePGM("writePGM_Sonuc.pgm",deneme->veri,deneme->genislik,deneme->yukseklik,deneme->maxDeger);

    //3. ister
    createRandomPGM("random.pgm",50,20,255);

    //4. ister
    threshold(deneme->veri,deneme->genislik,deneme->yukseklik,8,15);
    writePGM("writeThreshold_Sonuc.pgm",deneme->veri,deneme->genislik,deneme->yukseklik,MaxValue(deneme->veri,deneme->genislik,deneme->yukseklik));

    deneme->veri = readPGM("deneme1.pgm",&deneme->genislik,&deneme->yukseklik,&deneme->maxDeger);

    scalePGM(deneme->veri,&deneme->genislik,&deneme->yukseklik,8);
    writePGM("writeScale_Sonuc.pgm",deneme->veri,deneme->genislik,deneme->yukseklik,MaxValue(deneme->veri,deneme->genislik,deneme->yukseklik));

    //5. ister
    otsu(15,"deneme1.pgm","otsuSonuc.pgm");

    deneme->veri = readPGM("deneme1.pgm",&deneme->genislik,&deneme->yukseklik,&deneme->maxDeger);

    //6. ister
    add_saltpepper(10,deneme->veri,deneme->genislik,deneme->yukseklik,deneme->maxDeger,2);
    writePGM("writeSaltPepper_Sonuc.pgm",deneme->veri,deneme->genislik,deneme->yukseklik,deneme->maxDeger);

    deneme->veri = readPGM("deneme1.pgm",&deneme->genislik,&deneme->yukseklik,&deneme->maxDeger);

    //7. ister
    filter_median(deneme->veri,deneme->veri,deneme->genislik,deneme->yukseklik,3);
    writePGM("writeFilterMedian_Sonuc.pgm",deneme->veri,deneme->genislik,deneme->yukseklik,deneme->maxDeger);

    deneme->veri = readPGM("deneme1.pgm",&deneme->genislik,&deneme->yukseklik,&deneme->maxDeger);

    //8. ister
    filter_mean(deneme->veri,deneme->veri,deneme->genislik,deneme->yukseklik,3);
    writePGM("writeFilterMean_Sonuc.pgm",deneme->veri,deneme->genislik,deneme->yukseklik,deneme->maxDeger);

    return 0;
}
