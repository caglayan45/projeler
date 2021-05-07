/**
* Node.cpp
* Asıl fonksiyonların bulunduğu, düğüm işlemlerinin yapıldığı dosyadır.
* Yaz okulu 
* Vize ödevi
* 11/08/2020
* Yavuz KAYA
*/
#include "Node.hpp"
#include "Hesap.hpp"

node *ilk=NULL,*son=NULL;
int enBuyukObeb=0;
Hesap h;

void Node::Ekle(int x){
    node *yeni = new node();
    yeni->data = x;
    if(ilk == NULL){
		enBuyukObeb = 0;
        ilk=yeni;
        son=yeni;
        son->next=ilk;
        ilk->prev=son;
        return;
    }
    node *dolas = ilk;
    while(dolas->next != ilk){
        int anlikObeb = h.obebBul(yeni->data,dolas->data);
        if(anlikObeb >= enBuyukObeb){
            enBuyukObeb=anlikObeb;
            int buyukSayi = h.HangisiBuyuk(dolas->data,yeni->data),mod;
            if(buyukSayi == dolas->data)
                mod = dolas->data % yeni->data;
            else
                mod = yeni->data % dolas->data;
            if(mod != 0){
                    for(int i=0;i<mod;i++){
						dolas=dolas->prev;
                        if(dolas->next == ilk){
                            yeni->next = ilk;
                            yeni->prev = son;
                            ilk->prev = yeni;
                            son->next = yeni;
                            ilk = yeni;
                            return;
                        }   
                    }
                    yeni->next = dolas->next;
                    yeni->prev = dolas;
                    dolas->next->prev = yeni;
                    dolas->next = yeni;
                    return;
                }else{
                    yeni->next = dolas->next;
                    yeni->prev = dolas;
                    dolas->next->prev = yeni;
                    dolas->next = yeni;
                    return;
                }
        }
        dolas = dolas->next;
    }

	int anlikObeb = h.obebBul(yeni->data,dolas->data);
        if(anlikObeb >= enBuyukObeb)
            enBuyukObeb=anlikObeb;
    int buyuk = h.HangisiBuyuk(yeni->data,dolas->data),mod;
    if(buyuk == yeni->data)
        mod = yeni->data%dolas->data;
    else
        mod = dolas->data%yeni->data;
    if(mod != 0){
        for(int i=0;i<mod;i++){
			dolas=dolas->prev;
            if(dolas->next == ilk){
                yeni->next = ilk;
                yeni->prev = son;
                ilk->prev = yeni;
                son->next = yeni;
                ilk = yeni;
                return;
            } 
        }
        yeni->next = dolas->next;
        yeni->prev = dolas;
        dolas->next->prev = yeni;
        dolas->next = yeni;
    }else{
        yeni->prev = son;
        yeni->next = ilk;
        son->next = yeni;
        ilk->prev = yeni;
        son=yeni;
    }
}

void Node::listeyiTemizle(){
    node *liste=NULL;
    while(ilk != son){
        liste=ilk;
        ilk = ilk->next;
        delete liste;
    }
	liste = ilk;
    delete liste;
    ilk=NULL;
    son=NULL;
}

void Node::SifreyiYazdir(){
    node *liste=NULL;
    liste=ilk;
	cout << "Sifre : ";
    while(liste->next != ilk){
        cout<<(char)liste->data;
        liste=liste->next;
    }
    cout<<(char)liste->data<<endl;
}