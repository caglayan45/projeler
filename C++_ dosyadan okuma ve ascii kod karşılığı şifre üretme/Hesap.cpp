/**
* Hesap.cpp
* Hesaplama fonksiyonlarının bulunduğu dosyadır
* Yaz okulu 
* Vize ödevi
* 11/08/2020
* Yavuz KAYA
*/
#include "Hesap.hpp"

int Hesap::obebBul(int s1,int s2){
    int kucukSayi=0,sonuc=0;
    if (s1 < s2)
        kucukSayi = s1;
    else
        kucukSayi = s2;
    for (int i = 2; i <= kucukSayi; i++) {
        if (s1 % i == 0 && s2 % i == 0)
            sonuc = i;
    }
    return sonuc;
}

int Hesap::HangisiBuyuk(int s1,int s2){
    if(s1 > s2)
        return s1;
    else
        return s2;
}