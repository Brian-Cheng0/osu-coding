#include <string.h>
#include <stdio.h>
#include <sys/socket.h>
#include <netinet/in.h>
#include <stdlib.h>
#include <arpa/inet.h>
#include <unistd.h>
#include <time.h>
#define WAITTIME 0.5
#define WINDOWS 10


int main(int argc, char *argv[]){
        int sd;
        struct sockaddr_in server_address;
        int portNumber;
        char serverIP[29];
        int rc = 0;
        char str[500];
        FILE *input;
        int total;
        char start;
        int num = 0;
        char totalNum[500];
        int loop;
        int sendNum = 0;
        int x = 0;
        int seqNum = 0;
        char even[2];
	char odd[1];
        char bufferOut[20];
        time_t timeSent,currentTime;
        char bufferRead[100];
        if (argc < 3){
                printf ("usage is client <ipaddr> <port>\n");
                exit(1);
        }
        sd = socket(AF_INET, SOCK_DGRAM, 0);

        portNumber = strtol(argv[2], NULL, 10);
        strcpy(serverIP, argv[1]);

        server_address.sin_family = AF_INET;
        server_address.sin_port = htons(portNumber);
        server_address.sin_addr.s_addr = inet_addr(serverIP);
        printf("Enter a string:  ");
        fgets(str,sizeof(str),stdin);
        total = 0;
        start = str[0];
        while(start != '\n'){
                total++;
                start = str[total];
        }
        while(num<total){
                totalNum[num] = str[num];
                num++;
        }

        total = htonl(total);
        rc = sendto(sd,&total,sizeof(total),0,(struct sockaddr *)&server_address,sizeof(server_address));
        total = ntohl(total);
        loop = (total/2)+1;
        memset(bufferRead,0,100);
        int fromLength = sizeof(server_address);
        int ackNumber;
        while(sendNum < loop){
                //send last one byte at a time
                if(total%2 == 1 && sendNum == loop-1){
                        sprintf (bufferOut, "%11d%4ld%c", seqNum, sizeof(odd),totalNum[x]);
                        rc = sendto(sd,&bufferOut,17,0,(struct sockaddr *)&server_address,sizeof(server_address));
			timeSent = time(NULL); 
                //send two bytes each time
                }else{
                        sprintf (bufferOut, "%11d%4ld%c%c", seqNum, sizeof(even),totalNum[x],totalNum[x+1]);
                        rc = sendto(sd,&bufferOut,17,0,(struct sockaddr *)&server_address,sizeof(server_address));
			timeSent = time(NULL);
		}
		sleep(1); 
		memset (bufferRead, 0, 100);
    		rc = recvfrom(sd, &bufferRead, 100, MSG_DONTWAIT,(struct sockaddr *)&server_address, &fromLength);
                //this could happen in the timeout phase
		if (rc > 0){
			sscanf(bufferRead, "%11d", &ackNumber);
                        printf("Received:%s\n",bufferRead);
			seqNum+=2;
		}else{
			currentTime = time(NULL);
			if(currentTime-timeSent > WAITTIME){
				rc = sendto(sd,&bufferOut,17,0,(struct sockaddr *)&server_address,sizeof(server_address));
				timeSent = time(NULL);
				usleep(1000);
				memset (bufferRead, 0, 100);
    				rc = recvfrom(sd, &bufferRead, 100, MSG_DONTWAIT,(struct sockaddr *)&server_address, &fromLength);
				if (rc > 0){
					sscanf(bufferRead, "%11d", &ackNumber);
                                        printf("Timeout happened. Received:%s\n",bufferRead);
					seqNum+=2;
				}
			}
		}
                sendNum++;
		x+=2;
	}
        return 0;
}