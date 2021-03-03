#include <stdio.h>
#include <stdlib.h>
#include <string.h>
#define size 5000000
char text[size];
char output[size];

struct String{
    char word[50];
};

int charAt(struct String *s, int index){
    if(index < strlen(s->word))
        return s->word[index];
    return -1;
}

struct String *concat(struct String *s1, struct String *s2){
    int length = strlen(s1->word)+strlen(s2->word), j = 0;
    for(int i=strlen(s1->word);i<=length;i++){
        if(i == length){
            s1->word[i] = '\0';
            break;
        }
        s1->word[i] = s2->word[j++];
    }

    return s1;
}

unsigned int strSearch(struct String *s1, struct String *s2){
    int j = 0;
    for(int i=0;i<strlen(s1->word);i++){

        while(s1->word[i] == s2->word[j]){
            j++;
            i++;
            if(j == strlen(s2->word)){
                int p = i-j, c = 0;
                while(s1->word[p] != ' ' && isalpha(s1->word[p])){
                    c++;
                    p++;
                }
                return c;
            }
        }
        if(j != 0){
            j = 0;
            i--;
        }
    }
    return 0;
}

unsigned int findIndex(struct String *s1, struct String *s2){
    int j = 0;
    for(int i=0;i<strlen(s1->word);i++){

        while(s1->word[i] == s2->word[j]){
            j++;
            i++;
            if(j == strlen(s2->word))
                return i-j;
        }
        if(j != 0){
            j = 0;
            i--;
        }
    }
    return 0;
}

unsigned int findScore(struct String *s1){
    int score = 0;
    for(int i=0;i<strlen(s1->word);i++){
        if(toupper(s1->word[i]) == 'A' || toupper(s1->word[i]) == 'E' || toupper(s1->word[i]) == 'I' || toupper(s1->word[i]) == 'O' || toupper(s1->word[i]) == 'U' || toupper(s1->word[i]) == 'L' || toupper(s1->word[i]) == 'N' || toupper(s1->word[i]) == 'R' || toupper(s1->word[i]) == 'S' || toupper(s1->word[i]) == 'T')
            score++;
        else if(toupper(s1->word[i]) == 'D' || toupper(s1->word[i]) == 'G')
            score += 2;
        else if(toupper(s1->word[i]) == 'B' || toupper(s1->word[i]) == 'C' || toupper(s1->word[i]) == 'M' || toupper(s1->word[i]) == 'P')
            score += 3;
        else if(toupper(s1->word[i]) == 'F' || toupper(s1->word[i]) == 'H' || toupper(s1->word[i]) == 'V' || toupper(s1->word[i]) == 'W' || toupper(s1->word[i]) == 'Y')
            score += 4;
        else if(toupper(s1->word[i]) == 'K')
            score += 5;
        else if(toupper(s1->word[i]) == 'J' || toupper(s1->word[i]) == 'X')
            score += 8;
        else if(toupper(s1->word[i]) == 'Q' || toupper(s1->word[i]) == 'Z')
            score += 10;
    }
    return score;
}

void stat(){
    int alphabeticCount = 0, wordCount = 0;
    for(int i=0;i<strlen(text);i++){
        if(isalpha(text[i]))
            alphabeticCount++;
    }

    char *word = strtok(text," ");
    while(word != NULL){
        wordCount++;
        word = strtok(NULL," ");
    }
    char num[3];
    sprintf(num, "%d", wordCount);
    concat(output,"The number of words : ");
    concat(output,num);
    concat(output,"\n");

    sprintf(num, "%d", alphabeticCount);
    concat(output,"The number of alphabetic letters : ");
    concat(output,num);
    concat(output,"\n");
}

void main()
{

    while(1){
        struct String string;
        printf("input : ");
        gets(string.word);

        if(strcmp(string.word,"quit") == 0 || strcmp(string.word,"exit") == 0){
            concat(output,"Program ends. Bye\n");
            printf("%s",output);
            FILE *file = fopen("output.txt","w");
            fprintf(file,"%s",output);
            fclose(file);
            break;
        }
        else if(strcmp(string.word,"stat") == 0){
            stat();
        }else if(findIndex(string.word,":") != 0){
            int number = 0,x = 0;
            struct String temp, pureWord;
            switch(string.word[findIndex(string.word,":")+1]){
            case '1':
                if(findIndex(string.word,":")+3 != strlen(string.word)){
                    for(int i=findIndex(string.word,":")+3;i<strlen(string.word);i++){
                    if(!isdigit(string.word[i])){
                        printf("Wrong value : %c\n",string.word[i]);
                        x++;
                        break;
                    }
                    number = number*10 + (string.word[i] - '0');
                    }
                    if(x == 0){
                        for(int i = 0;i<findIndex(string.word,":");i++)
                            pureWord.word[i] = string.word[i];
                        pureWord.word[findIndex(string.word,":")] = '\0';

                        if(charAt(pureWord.word,number) == -1){
                           printf("Too much bigger : %d\n",number);
                           break;
                        }
                        concat(text,pureWord.word);
                        concat(text," ");
                        char k[3];
                        k[0] = toupper(charAt(pureWord.word,number));
                        k[1] = '\n';
                        k[2] = '\0';
                        concat(output,k);
                    }
                }
                break;
            case '2':
                if(findIndex(string.word,":")+2 == strlen(string.word)){
                    printf("You must add a word.");
                    break;
                }else{
                    for(int i = 0;i<findIndex(string.word,":");i++)
                        pureWord.word[i] = string.word[i];
                    pureWord.word[findIndex(string.word,":")] = '\0';
                    for(int i=findIndex(string.word,":")+3;i<strlen(string.word);i++,x++)
                        temp.word[x] = string.word[i];
                    temp.word[x] = '\0';
                    concat(pureWord.word," ");
                    concat(pureWord.word,temp.word);
                    concat(text,pureWord.word);
                    concat(text," ");
                    concat(output,pureWord.word);
                    concat(output,"\n");
                }
                break;
            case '3':
                if(findIndex(string.word,":")+2 == strlen(string.word)){
                    printf("You must add a word.");
                    break;
                }else{
                    for(int i = 0;i<findIndex(string.word,":");i++)
                        pureWord.word[i] = string.word[i];
                    pureWord.word[findIndex(string.word,":")] = '\0';

                    for(int i=findIndex(string.word,":")+3;i<strlen(string.word);i++,x++)
                        temp.word[x] = string.word[i];
                    temp.word[x] = '\0';

                    char num[3];
                    sprintf(num, "%d", strSearch(pureWord.word,temp.word));
                    concat(text,pureWord.word);
                    concat(text," ");
                    concat(text,temp.word);
                    concat(text," ");
                    concat(output,num);
                    concat(output,"\n");
                }
                break;
            case '4':
                for(int i = 0;i<findIndex(string.word,":");i++)
                    pureWord.word[i] = string.word[i];
                pureWord.word[findIndex(string.word,":")] = '\0';
                concat(text,pureWord.word);
                concat(text," ");
                char num[3];
                sprintf(num, "%d", findScore(pureWord.word));
                concat(output,num);
                concat(output,"\n");
                break;
            default:
                printf("Wrong value : %c\n",string.word[findIndex(string.word,":")+1]);
            }
        }
    }


}
