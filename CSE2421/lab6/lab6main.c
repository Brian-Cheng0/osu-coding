#include <stdio.h>

long sum( long a [], int count);  

int main(int argc, char **argv)
{


long a [] = {1, 2, 3, 4, 5, 6, 7, 8, 9, 10};

printf("Main ..... call sum\n");
sum(a, 10);
printf("\ndone!\n");

  return 0;
}
