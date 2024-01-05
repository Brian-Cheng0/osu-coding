def high_grades(di):
    a=0
    for i in di:
        for num in di[i]:
            if num>=90:
                a+=1
        di[i]=a
        a=0
                
                
    return di


di = {"Mike":[89,90,76,94],"Joe":[89,70,67,88],"Mary":[60,92,95,100]}
print (di.keys())
print(high_grades(di))