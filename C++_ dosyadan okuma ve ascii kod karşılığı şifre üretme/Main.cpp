/**
* Main.cpp
* Programın gerçeklemesi burada yapılmaktadır, düğüm(Node) nesnesi oluşturulup, dosya okuma yapılıp ekleme ve yazdırma işlemleri burada yapılmaktadır.
* Yaz okulu 
* Vize ödevi
* 11/08/2020
* Yavuz KAYA
*/
#include "Hesap.hpp"
#include "Node.hpp"
#include <iostream>
#include <fstream>
#include <string>
using namespace std;

int main(){
	
	Node *liste = new Node();
	
	ifstream dosyaOku("Sayilar.txt");
	string satir = "";
	
	if(dosyaOku.is_open()){
		while(getline(dosyaOku, satir)){
			int sayi;
			string stringSayi = "";
			
			if(isdigit(satir[satir.length()-1])){		
				for(int i=0;i<satir.length();i++){
					if(isdigit(satir[i])){
						stringSayi += satir[i];
					}else{
						sayi = stoi(stringSayi);
						stringSayi="";
						liste->Ekle(sayi);
					}
					if(i == satir.length()-1){
						sayi = stoi(stringSayi);
						liste->Ekle(sayi);
					}
				}
			}else{
				for(int i=0;i<satir.length();i++){
					if(isdigit(satir[i])){
						stringSayi += satir[i];
					}else{
						sayi = stoi(stringSayi);
						stringSayi="";
						liste->Ekle(sayi);
					}
				}
			}
			cout << endl;
			liste->SifreyiYazdir();
			liste->listeyiTemizle();
		}
	}
	
	dosyaOku.close();
	
	return 0;
}
