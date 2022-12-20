#comments in a Makefile start with sharp

#target all means all targets currently defined in this file
all: lab6.zip lab6	

#this target is the .zip file that must be submitted to Carmen
lab6.zip: Makefile lab6main.c lab6.s
	zip lab6.zip Makefile lab6main.c lab6.s

# this target is to compile the lab6main.c
	gcc lab6.s lab6main.c -o lab6

# this target deletes all files produced from the Makefile
# so that a completely new compile of all items is required
clean:
	rm -rf *.o lab6 lab6.zip
