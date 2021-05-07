/**
* Main.cpp
* Programın gerçeklemesi burada yapılmaktadır, dosya okuma yapılıp ekleme ve yazdırma işlemleri burada yapılmaktadır.
* Yaz okulu 
* Final ödevi
* 22/08/2020
* Yavuz KAYA
*/
#include "Node.hpp"
#include <iostream>
#include <fstream>
#include <string>
using namespace std;

int main(){
	
	ifstream dosyaOku("Kisiler.txt");
	ifstream dosyaOku1("Kisiler.txt");
	string satir = "",isim="", dogumYili = "",kilo = "";
	int veriDegis = 0,yas = 0,intKilo = 0,N=0;
	
	if(dosyaOku.is_open())
		while(getline(dosyaOku, satir))
			N++;
	string kisiYukseklikler[N][2];
	dosyaOku.close();
	Node::ElemanSayisi(N);
	
	node *root = NULL;
	cout << endl;
	if(dosyaOku1.is_open()){
		while(getline(dosyaOku1, satir)){
			string isim="", dogumYili = "",kilo = "";
			int veriDegis = 0,yas = 0,intKilo = 0;
			for(int i=0;i<satir.length();i++){
				if(satir[i] == '#'){
					veriDegis++;
					continue;
				}
				if(veriDegis < 1){
					isim += satir[i];
				}else if(veriDegis < 2){
					dogumYili += satir[i];
				}else{
					kilo += satir[i];
				}
			}
			yas = 2020 - stoi(dogumYili);
			intKilo  = stoi(kilo);
			
			Node::DengeDuzenle(root);
			Node::PostOrderDengeler(root);
			if(root != NULL)
				Node::OncekiDugumSayisi(root->denge);
			
			root = Node::Ekle(root,satir,isim,yas,intKilo);
			
			Node::DugumSayisi(root->denge);
			Node::Sifirla();
			Node::DengeDuzenle(root);
			Node::YiginaEkle(root);
			
			veriDegis = 0;
		}
	}
	
	dosyaOku1.close();
	
	
	cout << endl;
	Node::PostOrder(root);
	return 0;
}
