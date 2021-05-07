package AVL_BST_CSV;

import javax.xml.parsers.SAXParser;
import java.io.BufferedReader;
import java.io.File;
import java.io.FileReader;
import java.lang.reflect.Type;
import java.util.ArrayList;
import java.util.Scanner;

public class Main {

    public static ArrayList<String> storedNames = new ArrayList<String>();
    public static ArrayList<String> searchNames = new ArrayList<String>();
    public static ArrayList<HDTestData> tinyData = new ArrayList<HDTestData>();
    

    public static void ReadFromFile(){
        File readTxt = new File("E:\\IdeaProjects\\src\\AVL_BST_CSV\\starwars.txt");
        if (!readTxt.exists())
            System.out.println("Stored names file not exist");
        try {
            Scanner read = new Scanner(readTxt);
            while(read.hasNext()){
                String line = read.nextLine();
                storedNames.add(line);
            }
        }catch (Exception e){
            System.out.println(e.getMessage());
        }

        File readTxt2 = new File("E:\\IdeaProjects\\src\\AVL_BST_CSV\\starwars1.txt");
        if (!readTxt2.exists())
            System.out.println("Search names file not exist");
        try {
            Scanner read = new Scanner(readTxt2);
            while(read.hasNext()){
                String line = read.nextLine();
                searchNames.add(line);
            }
        }catch (Exception e){
            System.out.println(e.getMessage());
        }
    }

    public static void MatchTxt(){
        System.out.println("-----MATCHES-----");
        for (int i = 0; i < searchNames.size(); i++) {
            for (int j = 0; j < storedNames.size(); j++) {
                if(searchNames.get(i).equals(storedNames.get(j)))
                    System.out.println(searchNames.get(i));
            }
        }
        System.out.println("-----MATCHES-----\n");
    }

    public static void ReadFromCSV() {
        try {
        BufferedReader readCSV = new BufferedReader (new FileReader("E:\\IdeaProjects\\src\\AVL_BST_CSV\\data_tiny.csv"));
            String line = readCSV.readLine();
            while((line = readCSV.readLine()) != null){
                String[] data = line.split(",");
                tinyData.add(new HDTestData(data[0],data[1],data[2],Integer.parseInt(data[3])));
            }
        }catch (Exception e){
            System.out.println("Hata : " + e.getMessage());
        }
    }

    public static void main(String[] args) {

        ReadFromFile();
        MatchTxt();

        BST bstTree = new BST();

        long startTime = System.currentTimeMillis();
        for (int i = 0; i < storedNames.size(); i++) {
            bstTree.Insert(storedNames.get(i));
        }
        long endTime = System.currentTimeMillis();
        long elapsedTime = endTime - startTime;
        System.out.println("Insert time(ms) : " + elapsedTime);
        bstTree.InOrder();

        startTime = System.currentTimeMillis();
        System.out.println("\nMin : " + bstTree.GetMin().data);
        System.out.println("Max : " + bstTree.GetMax().data);
        endTime = System.currentTimeMillis();
        elapsedTime = endTime - startTime;
        System.out.println("Max-Min finding time(ms) : " + elapsedTime + "\n");

        bstTree.Search("SUN RIT");
        System.out.println();

        ReadFromCSV();
        AVL<String> AVLTreeSerialNumber = new AVL<String>();
        AVL<String> AVLTreeModel = new AVL<String>();
        AVL<String> AVLTreeCapacityBytes = new AVL<String>();
        AVL<Integer> AVLTreePowersOnHours = new AVL<Integer>();

        for (int i = 0; i < tinyData.size(); i++) {
            AVLTreeSerialNumber.Insert(tinyData.get(i).serialNumber);
            AVLTreeModel.Insert(tinyData.get(i).model);
            AVLTreeCapacityBytes.Insert(tinyData.get(i).capacityBytes);
            AVLTreePowersOnHours.Insert(tinyData.get(i).powerOnHours);
        }

        AVLTreeSerialNumber.BFO();
        AVLTreeModel.BFO();
        AVLTreeCapacityBytes.BFO();
        AVLTreePowersOnHours.BFO();

        System.out.println();

        AVLTreeSerialNumber.DFO();
        AVLTreeModel.DFO();
        AVLTreeCapacityBytes.DFO();
        AVLTreePowersOnHours.DFO();

        startTime = System.currentTimeMillis();
        AVLTreeSerialNumber.Search(tinyData.get(11).serialNumber);
        AVLTreeModel.Search(tinyData.get(11).model);
        AVLTreeCapacityBytes.Search(tinyData.get(11).capacityBytes);
        AVLTreePowersOnHours.Search(tinyData.get(11).powerOnHours);
        endTime = System.currentTimeMillis();
        elapsedTime = endTime - startTime;
        System.out.println("Search time(ms) : "+ elapsedTime + "\n");

        int size = tinyData.size();
        long time1 = 0, time2 = 0, time3 = 0, time4 = 0;

        for (int i = 0; i < tinyData.size(); i++) {
            startTime = System.currentTimeMillis();
            AVLTreeSerialNumber.Delete(tinyData.get(i).serialNumber);
            endTime = System.currentTimeMillis();
            elapsedTime = endTime - startTime;
            time1 += elapsedTime;
            System.out.println("Serial Number Delete Time(ms) : " + elapsedTime + "\n");

            startTime = System.currentTimeMillis();
            AVLTreeModel.Delete(tinyData.get(i).model);
            endTime = System.currentTimeMillis();
            elapsedTime = endTime - startTime;
            time2 += elapsedTime;
            System.out.println("Model Delete Time(ms) : " + elapsedTime + "\n");

            startTime = System.currentTimeMillis();
            AVLTreeCapacityBytes.Delete(tinyData.get(i).capacityBytes);
            endTime = System.currentTimeMillis();
            elapsedTime = endTime - startTime;
            time3 += elapsedTime;
            System.out.println("Capacity Bytes Delete Time(ms) : " + elapsedTime + "\n");

            startTime = System.currentTimeMillis();
            AVLTreePowersOnHours.Delete(tinyData.get(i).powerOnHours);
            endTime = System.currentTimeMillis();
            elapsedTime = endTime - startTime;
            time4 += elapsedTime;
            System.out.println("Power On Hours Delete Time(ms) : " + elapsedTime + "\n");
        }

        System.out.println("Avarage time(ms) Serial Number : " + time1/size);
        System.out.println("Avarage time(ms) Model : " + time2/size);
        System.out.println("Avarage time(ms) Capacity Bytes : " + time3/size);
        System.out.println("Avarage time(ms) Power On Hours : " + time4/size);

    }
}
