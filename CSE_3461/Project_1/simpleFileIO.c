#include <stdio.h>
#include <strings.h>
#include <stdlib.h>
int readfile(FILE *inputFile, FILE *outputFile);

int main(int argc, char *argv[]){
	FILE *inputFile, *outputFile;
	char inputFilename[20], outputFilename[20];
	int rc;

	if(argc < 3){
		printf("usage is: simpleFileIO <inputfile> <outputfile>\n");
		exit(1);
	}
	memcpy (inputFilename, argv[1], strlen(argv[1]));
	memcpy (outputFilename, argv[2], strlen(argv[2]));

	printf("the input filename is '%s', the output filename is '%s'\n", inputFilename, outputFilename);

	if((inputFile = fopen(inputFilename,"rb")) == NULL){
		printf("Error opening the input file '%s\n', inputFilename");
		exit(1);
	}
	if((outputFile = fopen(outputFilename,"wb")) == NULL){
		printf("Error opening the output file '%s\n', outputFilename");
		exit(1);
	}
	rc = readfile( inputFile, outputFile);

	rc = fclose(inputFile);
	if(rc<0)
		perror("close");
	rc = fclose (outputFile);
	if(rc<0)
		perror("close");

	return 0;
}

#define BUFFSIZE 1000
int readfile(FILE *inputFile, FILE *outputFile){
	unsigned char buffer [BUFFSIZE];
	int numberOfBytes;
	int rc;
	int totalBytesRead = 0; int totalBytesWritten = 0;

	numberOfBytes = fread(buffer, 1, BUFFSIZE, inputFile);

	while(numberOfBytes > 0){
		totalBytesRead += numberOfBytes;
		rc = fwrite(buffer,1, numberOfBytes, outputFile);
		if (numberOfBytes != rc){
			perror ("writing to file");
			exit(1);
		}
		totalBytesWritten += rc;
		numberOfBytes = fread(buffer, 1, BUFFSIZE, inputFile);

	}
	printf ("read %d bytes, and wrote %d bytes\n", totalBytesRead, totalBytesWritten);
	return (0);
}