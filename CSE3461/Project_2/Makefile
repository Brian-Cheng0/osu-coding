#comments in a Makefile start with sharp

#target all means all targets cuurently defined in this file
all: project2.zip client client.c

#this target is the .zip file that must be submitted to Carmen
project2.zip: Makefile client.c client
	zip project2.zip Makefile client.c client

#this target is to compile the client.c
	gcc -o client client.c

#this target deletes all files produced from the Makefile
#so that a completely new compile of all items is required to clean:
clean:	
	rm -rf *.o client