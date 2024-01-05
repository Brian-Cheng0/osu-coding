#include <string.h>
#include <stdio.h>
#include <sys/socket.h>
#include <netinet/in.h>
#include <unistd.h>
#include <stdlib.h>
int main(int argc, char *argv[])
{
	int sd; /* socket descriptor */
	int connected_sd; /* socket descriptor */
	int rc; /* return code from recvfrom */
	struct sockaddr_in server_address;
	struct sockaddr_in from_address;
	char buffer[100];
	int flags = 0;
	socklen_t fromLength;
	int portNumber;
	if (argc < 2){
		printf ("Usage is: server <portNumber>\n");
		exit (1);
	}
	portNumber = atoi(argv[1]);
	sd = socket (AF_INET, SOCK_STREAM, 0);
	server_address.sin_family = AF_INET;
	server_address.sin_port = htons(portNumber);
	server_address.sin_addr.s_addr = INADDR_ANY;
	bind (sd, (struct sockaddr *)&server_address, sizeof(server_address));
	listen (sd, 5);
	connected_sd = accept (sd, (struct sockaddr *) &from_address,
	&fromLength);
	bzero (buffer, 100);
	rc = read(connected_sd, &buffer, 100);
	printf ("received the following %s\n", buffer);
	return 0;
}
