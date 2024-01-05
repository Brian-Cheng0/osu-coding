from random import randint
def ten_flips():
    count = 0
    sum=0
    for _ in range(10):
        flip1 = randint(1, 10)
        if flip1>5:
            sum+=1
        if sum >=7:
            count+=1
    return count / 10

def ten_flips_advanced():
    count = 0
    sum=0
    for _ in range(10):
        flip1 = randint(1, 10)
        if flip1>=5:
            sum+=1
        if sum >=7:
            count+=1
    return count / 10
