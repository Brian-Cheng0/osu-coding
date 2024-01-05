import copy

def mult_3(li):
    newList = []
    for i in li:
        if i%3==0:
            newList.append(i)
    return newList

def make_list(n):
    newList = []
    for i in range(0,n):
        newList.append(2*i+2)
    return newList

def chessboard(n):
    m=copy.copy(n)
    array = [[0]*m]*n
    for i in range(0,n):
        for j in range(0,m):
            if m%2!=0&n%2==0:
                array[i][j] = 1
            if m%2!=0&n%2!=0:
                array[i][j] = 1
    print (array)

chessboard(4)
li = [4, 0, -6, 7, 9, 2]
newlist = make_list(5)
x = 8/3
#print(int(x))
#print(newlist)