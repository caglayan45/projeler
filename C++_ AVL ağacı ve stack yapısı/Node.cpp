/**
* Node.cpp
* Asıl fonksiyonların bulunduğu, düğüm işlemlerinin yapıldığı dosyadır.
* Yaz okulu 
* Final ödevi
* 23/08/2020
* Yavuz KAYA
*/
#include "Node.hpp"
#include "Yigin.hpp"
#include <math.h>
Node::Node(){}

int Node::Yukseklik(node *dugum){
	return (dugum == NULL)? 0 : dugum->yukseklik;
}

int Node::YukseklikDenge(node *dugum){
	return (dugum == NULL)? 0 : dugum->denge;
}

int Node::DengeFaktoru(node *dugum){
	return (dugum == NULL)? 0 : (Yukseklik(dugum->left) - Yukseklik(dugum->right));
}

int Node::Buyuk(int a,int b){
    return (a > b)? a : b;
}

node* Node::IlkKisi(string data,string isim,int yas,int kilo){
	node *temp = new node();
	
    temp->data = data;
    temp->isim = isim;
    temp->yas = yas;
    temp->kilo = kilo;
	temp->root = NULL;
	temp->root = Yigin::Push(temp->root,'O');
	
    temp->yukseklik = 1;
    temp->denge = 1;
    temp->left = NULL;
	temp->right = NULL;
    return temp;
}

node* Node::SolaDondur(node *dugum){
	node *dugumRight = dugum->right;
	node *dugumRightLeft = dugum->right->left;

    dugumRight->left = dugum;
    dugum->right = dugumRightLeft;

    dugum->yukseklik = Buyuk(Yukseklik(dugum->left),Yukseklik(dugum->right)) + 1;
    dugumRight->yukseklik = Buyuk(Yukseklik(dugumRight->left),Yukseklik(dugumRight->right)) + 1;
	
	dugum->denge = Buyuk(YukseklikDenge(dugum->left),YukseklikDenge(dugum->right)) + 1;
    dugumRight->denge = Buyuk(YukseklikDenge(dugumRight->left),YukseklikDenge(dugumRight->right)) + 1;

    return dugumRight;
}

node* Node::SagaDondur(node *dugum){
	node *dugumLeft = dugum->left;
	node *dugumLeftRight = dugum->left->right;

    dugumLeft->right = dugum;
    dugum->left = dugumLeftRight;

    dugum->yukseklik = Buyuk(Yukseklik(dugum->left),Yukseklik(dugum->right)) + 1;
    dugumLeft->yukseklik = Buyuk(Yukseklik(dugumLeft->left),Yukseklik(dugumLeft->right)) + 1;
	
	dugum->denge = Buyuk(YukseklikDenge(dugum->left),YukseklikDenge(dugum->right)) + 1;
    dugumLeft->denge = Buyuk(YukseklikDenge(dugumLeft->left),YukseklikDenge(dugumLeft->right)) + 1;

    return dugumLeft;
}

void Node::PostOrder(node *dugum){
	if(dugum == NULL)
        return;
    PostOrder(dugum->left);
    PostOrder(dugum->right);
    cout << dugum->isim << ", " <<  2020-(dugum->yas) << ", " << dugum->kilo; 
	Yigin::ShowStack(dugum->root);
}

int N = 0,i = 0,k = 0;
string* dugumler = NULL;
int* dugumDengeleri = NULL;

void Node::ElemanSayisi(int x){
	dugumler = new string[x];
	dugumDengeleri = new int[x];
	N = x;
}

int maxDugum = 0,oncekiMaxDugum = 0;

void Node::YiginaEkle(node *dugum){
	if(dugum == NULL)
		return;
	YiginaEkle(dugum->left);
	YiginaEkle(dugum->right);
	for(int j = 0;j < k;j++){
		if(dugum->data == dugumler[j]){
			if(maxDugum != oncekiMaxDugum){
				if(dugum->denge - 1 == dugumDengeleri[j]){
					dugum->root = Yigin::Push(dugum->root,'D');
					return;
				}else if(dugum->denge - 1 < dugumDengeleri[j]){
					dugum->root = Yigin::Push(dugum->root,'A');
					return;
				}else{
					dugum->root = Yigin::Push(dugum->root,'Y');
					return;
				}
			}else{
				if(dugum->denge == dugumDengeleri[j]){
					dugum->root = Yigin::Push(dugum->root,'D');
					return;
				}else if(dugum->denge < dugumDengeleri[j]){
					dugum->root = Yigin::Push(dugum->root,'A');
					return;
				}else{
					dugum->root = Yigin::Push(dugum->root,'Y');
					return;
				}
			}
		}
	}
}

void Node::DengeDuzenle(node *dugum){
	if(dugum == NULL)
        return;
	if(dugum->left != NULL)
		dugum->left->denge = dugum->denge - 1;
	if(dugum->right != NULL)
		dugum->right->denge = dugum->denge - 1;
	DengeDuzenle(dugum->left);
    DengeDuzenle(dugum->right);
}

void Node::PostOrderDengeler(node *dugum){
	if(dugum == NULL)
        return;
    PostOrderDengeler(dugum->left);
    PostOrderDengeler(dugum->right);
	dugumler[i] = dugum->data;
	dugumDengeleri[i++] = dugum->denge;
}

void Node::DugumSayisi(int x){
	maxDugum = pow(2,x)-1;
}

void Node::OncekiDugumSayisi(int x){
	oncekiMaxDugum = pow(2,x)-1;
}

void Node::Sifirla(){
	k=i;
	i=0;
}

void Node::yazdir(){
	for(int l=0;l<=k;l++)
		cout << dugumler[l] << " " << dugumDengeleri[l] << endl;
	cout << endl;
}

node* Node::Ekle(node *dugum,string data,string isim,int yas,int kilo){
    if(dugum == NULL)
        return IlkKisi(data,isim,yas,kilo);
    if(yas <= dugum->yas)
        dugum->left = Ekle(dugum->left,data,isim,yas,kilo);
    else
        dugum->right = Ekle(dugum->right,data,isim,yas,kilo);
	
    dugum->yukseklik = Buyuk(Yukseklik(dugum->left),Yukseklik(dugum->right)) + 1;
    dugum->denge = Buyuk(YukseklikDenge(dugum->left),YukseklikDenge(dugum->right)) + 1;
    int dengeFaktoru = DengeFaktoru(dugum);
	
    if(dengeFaktoru > 1 && yas < dugum->left->yas)
        return SagaDondur(dugum);

    if(dengeFaktoru < -1 && yas > dugum->right->yas)
        return SolaDondur(dugum);

    if(dengeFaktoru > 1 && yas > dugum->left->yas){
        dugum->left = SolaDondur(dugum->left);
        return SagaDondur(dugum);
    }

    if(dengeFaktoru < -1 && yas < dugum->right->yas){
        dugum->right = SagaDondur(dugum->right);
        return SolaDondur(dugum);
    }

    return dugum;
}
