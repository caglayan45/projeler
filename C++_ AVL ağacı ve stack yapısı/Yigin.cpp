/**
* Yigin.cpp
* Yığın fonksiyonlarının bulunduğu ve işlemlerinin yapıldığı dosya.
* Yaz okulu 
* Final ödevi
* 27/08/2020
* Yavuz KAYA
*/
#include "Yigin.hpp"
#include "Node.hpp"

yigin* Yigin::Push(yigin *dugum,char x){
    yigin *yeni = new yigin();
    yeni->data = x;
    if(dugum == NULL){
        dugum=yeni;
        dugum->next=NULL;
        return yeni;
    }
	yeni->next = dugum;
	dugum = yeni;
	return yeni;
}

void Yigin::Pop(yigin *dugum){
	yigin *sil=dugum;

	if(dugum == NULL){
		return;
	}else if(dugum->next != NULL){
		dugum = dugum->next;
		sil->next = NULL;
	}
	delete(sil);
}

void Yigin::ShowStack(yigin* dugum){
	yigin *liste = dugum, *temp = NULL;
	if(liste == NULL)
		return;
	cout << " Yigin : ";
	while(liste->next != NULL){
		cout << liste->data << " ";
		temp = liste;
		liste = liste->next;
		Pop(temp);
	}
	cout << liste->data << " " << endl;;
	Pop(liste);
}