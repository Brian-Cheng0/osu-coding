def chessboard(n):
    m=n
    array = [[0 for i in range(m)]for j in range(n)]
    for i in range(m):
        for j in range(n):
            if i%2==0 and j%2==0:
                array[i][j]=1
            if i%2!=0 and j%2!=0:
                array[i][j]=1
    return array

print(chessboard(6))
