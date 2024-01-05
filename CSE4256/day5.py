def complete_m(n):
    array = [[1 for i in range(n)]for j in range(n)]
    for i in range(0,n):
            array[i][i] = 0
    return array


def is_complete_d(di):
      for keys, values in di:
            if(keys == values):
                  return False
            elif (keys(values) and not values(keys)):
                  return False
            else:
                  return True

