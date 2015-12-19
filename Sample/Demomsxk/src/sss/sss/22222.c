#include <stdio.h>
#include <stdlib.h>

void main()
{
int i = 0;
int sum = 0;

switch(i)
{
case 1:
case 3:
case 5:
case 7:
case 9:
default:
   sum +=2;

}
printf("%d\t",sum);
return;
}