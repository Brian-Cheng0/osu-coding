def linear_search(li,n):
    index = 0
    for i in li:
        if n == i:
            return index
        index+=1

    return -1