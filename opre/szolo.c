#include <stdio.h>
#include <stdlib.h>
#include <string.h>
#include <fcntl.h> //open,creat
#include <sys/types.h> //open
#include <sys/stat.h>
#include <errno.h> //perror, errno

struct worker{
    char name[20];
    char address[20];
    int workdays[7];
};
struct worker workers[10];
int WCount = 0;
int workersNeededPerDay[] = {3, 1, 1, 3, 4, 1, 2};


int whichDay(char * day){
    if(strcmp(day, "mon") == 0) return 0;
    if(strcmp(day, "tue") == 0) return 1;
    if(strcmp(day, "wed") == 0) return 2;
    if(strcmp(day, "thu") == 0) return 3;
    if(strcmp(day, "fri") == 0) return 4;
    if(strcmp(day, "sat") == 0) return 5;
    if(strcmp(day, "sun") == 0) return 6;
    perror("Not a valid day!\nValid: mon/tue/wed...\n");
    exit(1);
}

void hashtag(){
    for (int i = 0; i < 30; ++i){
        printf("#");
    }
    printf("\n");
}

void EditWorker(){
    printf("Which one?\n");
    for(int i=0;i<WCount;++i){
        printf("[%i] %s\n", i, workers[i].name);
    }
    int idx;
    scanf("%i", &idx);
    printf("Which data?\n[0] Name\n[1] Address\n[2] Workdays\n");
    int type;
    char newNameEdit[20];
    char newAddr[20];
    scanf("%i", &type);
    switch(type){
        case 0:
            printf("New name: ");
            scanf("%s", newNameEdit);
            strcpy(workers[idx].name,newNameEdit);
            break;
        case 1:
            printf("New address: ");
            scanf("%s", newAddr);
            strcpy(workers[idx].address,newAddr);
            break;
        case 2:
            printf("New workdays: ");
            int workdays[7];
            for(int i=0;i<7;++i){
                workdays[i] = 0;
            }
            char str[100];
            fgets(str, 10, stdin);
            fgets(str, 100, stdin);
            char * tmp;
            tmp = strtok(str, " \n");
            while(tmp != NULL){       
                workdays[whichDay(tmp)] = 1;
                tmp = strtok(NULL, " \n");
            }
            for(int i=0;i<7;++i){
                workers[idx].workdays[i] = workdays[i];
            }
        default:
            break;
    }

}

void DeleteWorker(){
    printf("Which one?\n");
    for(int i=0;i<WCount;++i){
        printf("[%i] %s\n", i, workers[i].name);
    }
    int idx;
    scanf("%i", &idx);
    workers[idx] = workers[--WCount];
}


void ShowWorker(int index){
    printf("Name: %s\nAddress: %s\nWorks on:\nmon\ttue\twed\tthu\tfri\tsat\tsun\n", workers[index].name, workers[index].address);
        for (int j = 0; j < 7; ++j) {
            printf("%s\t", (workers[index].workdays[j]==1?"yes":"no"));
        }
        printf("\n");
}

void DailyList(){
    printf("Which day?\n[0]mon\n[1]tue\n[2]wed\n[3]thu\n[4]fri\n[5]sat\n[6]sun\n");
    hashtag();
    int day;
    scanf("%i", &day);
    for(int i=0;i<WCount;++i){
        if(workers[i].workdays[day] == 1){
            ShowWorker(i);
        }
    }
}



void FullList(){
    for (int i = 0; i < WCount; ++i) {
        ShowWorker(i);
    }
}

int canWork(int day){
    int tmp[7];
    for(int i=0;i<7;++i){
        tmp[i]=workersNeededPerDay[i];
    }
    /*printf("\nCanWork\n");
    for(int i=0;i<7;++i){
        printf("%i ", tmp[i]);
    }
    printf("\n");*/
    for(int i=0;i<WCount;++i){
        for(int j=0;j<7;++j){
            tmp[j] -= workers[i].workdays[j];
        }
    }
    /*for(int i=0;i<7;++i){
        printf("%i ", tmp[i]);
    }
    printf("\nend\n");*/
    return (tmp[day]>0 ? 1 : 0);
}

void newWorker(){
    char name[20];
    char addr[20];
    printf("Name: ");
    scanf("%s", name);
    printf("Address: ");
    scanf("%s", addr);
    int workdays[7];
    for(int i=0;i<7;++i){
        workdays[i] = 0;
    }
    char str[100];
    fgets(str, 10, stdin);
    fgets(str, 100, stdin);
    char * tmp;
    tmp = strtok(str, " \n");
    while(tmp != NULL){       
        workdays[whichDay(tmp)] = 1;
        tmp = strtok(NULL, " \n");
    }
    /*for(int i=0;i<7;++i){
        printf("%i ", workdays[i]);
    }*/
    /*printf("name: %s, add: %s, workdays:\n", name, addr);
    for(int i=0;i<7;++i){
        printf("%i ", workdays[i]);
    }*/
    struct worker newMan;
    strcpy(newMan.name, name);
    strcpy(newMan.address, addr);
    
    for(int i=0;i<7;++i){
        if(canWork(i) && workdays[i]){
            newMan.workdays[i] = 1;
        }else if(workdays[i]){
            printf("You cannot work on day No.%i because it's full!\n", i);
            newMan.workdays[i] = 0;
        }else{
            newMan.workdays[i] = 0;
        }
    }
    /*for(int i=0;i<7;++i){
        printf("%i ", newMan.workdays[i]);
    }*/
    int workDayCount = 0;
    for(int i=0;i<7;++i){
        workDayCount += newMan.workdays[i];
    }
    if(workDayCount > 0){
        workers[WCount++] = newMan;
    }else{
        printf("\nYou cannot work here!\nSorry, but when you'd like to work, there is no place!\n");
    }
}
void writeToFile(){
    int g;
    g=open("workers.wo",O_WRONLY|O_CREAT|O_TRUNC,S_IRUSR|S_IWUSR);
    if (g<0){ perror("Error at opening the file\n");exit(1);}
    for(int i=0;i<WCount;++i){
        if (write(g,&workers[i],sizeof(workers[i]))!=sizeof(workers[i])) 
        {perror("There is a mistake in writing\n");exit(1);}
    }
    close(g);
}

void readFromFile(){
    int f;
    f = open("workers.wo", O_RDONLY);
    struct worker fileWorker;
    if(f>=0){
        while (read(f,&fileWorker,sizeof(fileWorker))){ 
            workers[WCount++] = fileWorker;
        }
    }
    close(f);
}

void bringMenu(){
    printf("[0] New worker\n");
    printf("[1] Edit workers\n");
    printf("[2] Delete worker\n");
    printf("[3] Daily list\n");
    printf("[4] Full list\n");
    printf("[5] Save workers\n");
    int input;
    scanf("%d", &input);
    switch (input){
        case 0:
            newWorker();
            break;
        case 1:
            EditWorker();
            break;
        case 2:
            DeleteWorker();
            break;
        case 3:
            DailyList();
            break;
        case 4:
            FullList();
            break;
        case 5:
            writeToFile();
            break;
        default:
            printf("Not a valid menupoint!");
    }
}




int main(int argc, char ** argv){
    printf("Hello there!\nThis program was made to help organizing the workers. Hope you'll enjoy using it!\n");    
    short end = 0;
    readFromFile();
    while(end == 0){
        hashtag();
        bringMenu();
    }
    
    return 0;
}
