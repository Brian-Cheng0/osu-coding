#include <string.h>
#include <stdio.h>
#include <sys/socket.h>
#include <netinet/in.h>
#include <stdlib.h>
#include <arpa/inet.h>
#include <unistd.h>

int readfile(FILE *inputFile, int sd);

int main(int argc, char *argv[])
{
    FILE *inputFile;
    char inputFilename[20];

    int sd;
    struct sockaddr_in server_address;
    int portNumber;
    char serverIP[29];
    int rc = 0;

    if (argc < 3)
    {
        printf("usage is client <ipaddr> <port>\n");
        exit(1);
    }

    while (strcmp(inputFilename, "Done") != 0)
    {
        strncpy(inputFilename, argv[1], sizeof(inputFilename) - 1);
        inputFilename[sizeof(inputFilename) - 1] = '\0'; // Ensure null-terminated string
        printf("the sending filename is '%s'\n", inputFilename);

        if ((inputFile = fopen(inputFilename, "rb")) == NULL)
        {
            printf("Error opening the input file '%s'\n", inputFilename);
            exit(1);
        }

        sd = socket(AF_INET, SOCK_STREAM, 0);
        portNumber = strtol(argv[2], NULL, 10);
        strcpy(serverIP, argv[1]);

        server_address.sin_family = AF_INET;
        server_address.sin_port = htons(portNumber);
        server_address.sin_addr.s_addr = inet_addr(serverIP);

        if (connect(sd, (struct sockaddr *)&server_address, sizeof(struct sockaddr_in)) < 0)
        {
            close(sd);
            perror("error connecting stream socket");
            exit(1);
        }

        rc = write(sd, inputFilename, strlen(inputFilename));
        printf("sent %d bytes\n", rc);

        if (rc < 0)
            perror("sendto");

        rc = readfile(inputFile, sd);
        fclose(inputFile); // Close the input file after reading
    }

    return 0;
}

#define BUFFSIZE 1000

int readfile(FILE *inputFile, int sd)
{
    unsigned char buffer[BUFFSIZE];
    int numberOfBytes;
    int rc;
    int totalBytesRead = 0;

    numberOfBytes = fread(buffer, 1, BUFFSIZE, inputFile);

    while (numberOfBytes > 0)
    {
        totalBytesRead += numberOfBytes;
        rc = write(sd, buffer, numberOfBytes); // Write the buffer
        if (rc < 0)
        {
            perror("write");
            break;
        }
        numberOfBytes = fread(buffer, 1, BUFFSIZE, inputFile);
    }

    printf("sent %d bytes\n", totalBytesRead);
    return 0;
}