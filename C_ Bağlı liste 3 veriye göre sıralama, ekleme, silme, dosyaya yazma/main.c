#include <stdio.h>
#include <stdlib.h>
#include <string.h>

//declare "struct List" structure
struct List{
    char *name;
    char *surname;
    int id;
    struct List *name_next;
    struct List *surname_next;
    struct List *id_next;
};

//declare first nodes
struct List *idFirst = NULL,*nameFirst = NULL,*surnameFirst = NULL;

//we are inserting node to the list
void InsertNode(char *name, char *surname, int id){
    struct List *newNode = (struct List*) malloc(sizeof(struct List));

    newNode->id = id;
    newNode->name = (char*)malloc(sizeof(char)*strlen(name));
    newNode->surname = (char*)malloc(sizeof(char)*strlen(surname));
    strcpy(newNode->name,name);
    strcpy(newNode->surname,surname);
    newNode->id_next = newNode->name_next = newNode->surname_next = NULL;

    if(idFirst == NULL)//if there is no any node in the list then inserting to the first
        idFirst = nameFirst = surnameFirst = newNode;
    else if(idFirst->id_next == NULL){)//if there is no any node the next of first then we comparison first and new node and inserting to the list
        if(idFirst->id <= newNode->id)
            idFirst->id_next = newNode;
        else{
            newNode->id_next = idFirst;
            idFirst = newNode;
        }

        if(strcmp(nameFirst->name,newNode->name) <= 0)
            nameFirst->name_next = newNode;
        else{
            newNode->name_next = nameFirst;
            nameFirst = newNode;
        }

        if(strcmp(nameFirst->surname,newNode->surname) <= 0)
            surnameFirst->surname_next = newNode;
        else{
            newNode->surname_next = surnameFirst;
            surnameFirst = newNode;
        }
    }else{//if there is 2 and more node in the list, then we comparison all nodes with new node and inserting to the list
        struct List *id_prev = idFirst,*id_searching = idFirst->id_next;
        struct List *name_prev = nameFirst,*name_searching = nameFirst->name_next;
        struct List *surname_prev = surnameFirst,*surname_searching = surnameFirst->surname_next;

        //id ordering
        if(idFirst->id >= newNode->id){
            newNode->id_next = idFirst;
            idFirst = newNode;
        }else{
            while(id_searching != NULL){
                if(id_searching->id >= newNode->id){
                    id_prev->id_next = newNode;
                    newNode->id_next = id_searching;
                    break;
                }
                id_prev = id_searching;
                id_searching = id_searching->id_next;
                if(id_searching == NULL){
                    id_prev->id_next = newNode;
                    break;
                }
            }
        }

        //name ordering
        if(strcmp(newNode->name,nameFirst->name) <= 0){
            newNode->name_next = nameFirst;
            nameFirst = newNode;
        }else{
            while(name_searching != NULL){
                if(strcmp(newNode->name,name_searching->name) <= 0){
                    name_prev->name_next = newNode;
                    newNode->name_next = name_searching;
                    break;
                }
                name_prev = name_searching;
                name_searching = name_searching->name_next;
                if(name_searching == NULL){
                    name_prev->name_next = newNode;
                    break;
                }
            }
        }

        //surname ordering
        if(strcmp(newNode->surname,surnameFirst->surname) <= 0){
            newNode->surname_next = surnameFirst;
            surnameFirst = newNode;
        }else{
            while(surname_searching != NULL){
                if(strcmp(newNode->surname,surname_searching->surname) <= 0){
                    surname_prev->surname_next = newNode;
                    newNode->surname_next = surname_searching;
                    break;
                }
                surname_prev = surname_searching;
                surname_searching = surname_searching->surname_next;
                if(surname_searching == NULL){
                    surname_prev->surname_next = newNode;
                    break;
                }
            }
        }
    }
}

//Searching id on the list, when we found it then we reconnect pointers
char *DeleteNode(int id){
    struct List *temp = NULL, *delNode = idFirst, *tempNext = nameFirst;
    //id ordering
    if(idFirst->id != id){
        while(delNode->id != id){
            temp = delNode;
            delNode = delNode->id_next;
            if(delNode->id == id){
                temp->id_next = delNode->id_next;
                break;
            }
        }
    }else{
        idFirst = delNode->id_next;
    }

    //name ordering
    temp = NULL;
    if(nameFirst->id != id){
        while(tempNext->id != id){
            temp = tempNext;
            tempNext = tempNext->name_next;
            if(tempNext->id == id){
                temp->name_next = tempNext->name_next;
                break;
            }
        }
    }else{
        nameFirst = tempNext->name_next;
    }

    //surname ordering
    tempNext = surnameFirst;
    temp = NULL;
    if(surnameFirst->id != id){
        while(tempNext->id != id){
            temp = tempNext;
            tempNext = tempNext->surname_next;
            if(tempNext->id == id){
                temp->surname_next = tempNext->surname_next;
                break;
            }
        }
    }else{
        surnameFirst = tempNext->surname_next;
    }

    //print to the screen and delete node
    if(id == delNode->id){
        char *c;
        sprintf(c,"%s %s     \t%d",delNode->name,delNode->surname,delNode->id);
        free(delNode);
        return c;
    }
    char c[] = "Could not found.";
    return c;
}

//print list to the screen with id ordering
void ShowListIdOrder(){
    struct List *temp;
    temp = idFirst;
    int i = 1;
    printf("The list in ID sorted order:\n");
    while(temp != NULL){
        printf("\t%d. %s %s     \t%d\n",i++,temp->name,temp->surname,temp->id);
        temp = temp->id_next;
    }
    printf("\n");
}

//print list to the screen with name ordering
void ShowListNameOrder(){
    struct List *temp;
    temp = nameFirst;
    int i = 1;
    printf("The list in name-alphabetical order:\n");
    while(temp != NULL){
        printf("\t%d. %s %s     \t%d\n",i++,temp->name,temp->surname,temp->id);
        temp = temp->name_next;
    }
    printf("\n");
}

//print list to the screen with surname ordering
void ShowListSurnameOrder(){
    struct List *temp;
    temp = surnameFirst;
    int i = 1;
    printf("The list in surname-alphabetical order:\n");
    while(temp != NULL){
        printf("\t%d. %s %s     \t%d\n",i++,temp->name,temp->surname,temp->id);
        temp = temp->surname_next;
    }
    printf("\n");
}

//print to the file
void PrintToFile(char fileName[]){
    FILE *file = fopen(fileName,"w");
    struct List *temp;

    //print id ordering
    temp = idFirst;
    fprintf(file,"The list in ID sorted order:\n");
    while(temp != NULL){
        fprintf(file,"%s %s    \t%d\n",temp->name,temp->surname,temp->id);
        temp = temp->id_next;
    }

    //print name ordering
    temp = nameFirst;
    fprintf(file,"\nThe list in name-alphabetical order:\n");
    while(temp != NULL){
        fprintf(file,"%s %s    \t%d\n",temp->name,temp->surname,temp->id);
        temp = temp->name_next;
    }

    //print surname ordering
    temp = surnameFirst;
    fprintf(file,"\nThe list in surname-alphabetical order:\n");
    while(temp != NULL){
        fprintf(file,"%s %s    \t%d\n",temp->name,temp->surname,temp->id);
        temp = temp->surname_next;
    }

    fclose(file);
}

int main()
{

    FILE *file;
    if((int)(file = fopen("students.txt", "r")) == 0)
        printf("File not exist.");
    else
    {
        char line[100];
        while((fgets(line,100,file)) != NULL)
        {//read line from file and split with "-" and insert to the list
            char name[40],surname[40];
            int id = 0;
            strcpy(name,strtok(line,"-"));
            strcpy(surname,strtok(NULL,"-"));
            id = atoi(strtok(NULL,"-"));
            //inserting
            InsertNode(name,surname,id);
        }
    }
    fclose(file);

    //print to the screen with 3 ordering
    ShowListNameOrder();
    ShowListSurnameOrder();
    ShowListIdOrder();

    while(1){
        //read a character from user
        char input;
        printf("\nEnter your choice:\n1 to insert a student into the list.\n2 to delete a student from the list.\n3 to print the students in the list.\n4 to print the students to an output file.\n5 to end.\n? ");
        scanf("%c",&input);
        char name[40],surname[40];
        int id = 0;
        switch(input){
        case '1':
            //if char is '1' then insert node with user's input
            printf("Enter a student name, surname, and ID:\n");
            scanf("%s %s %d",name,surname,&id);
            InsertNode(name,surname,id);
            break;
        case '2':
            //if char is '2' then delete from list user's input's id
            printf("Enter a student ID:\n");
            scanf("%d",&id);
            getchar();
            printf("The student \"%s\" is deleted from the list!\n",DeleteNode(id));
            break;
        case '3':
            //if char is '3' then print to the screen with 3 ordering
            ShowListNameOrder();
            ShowListSurnameOrder();
            ShowListIdOrder();
            break;
        case '4':
            //if char is '4' then print to the file with user's input's file name
            printf("Enter a file name\n");
            scanf("%s",name);
            PrintToFile(name);
            printf("Output is printed to the file %s\n",name);
            break;
        case '5':
            //if char is '5' then exit
            printf("--------------------------------------");
            return 0;
            break;
        default:
            //if char is different from 1,2,3,4,5 then print to the screen "Wrong character."
            printf("\nWrong character.\n");
        }
        getchar();
    }

    return 0;
}
