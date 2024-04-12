# from elasticsearch import Elasticsearch
import os
import json

docs = list()
i = 10

for root, dirpath, files in os.walk('./20_newsgroups'):
    if i == 0:
        break
    for name in files:
        if i ==0:
            break
        path = os.path.join(root, name)
        print(path)
        with open(path, "r", errors='ignore') as file:
           doc = dict() 
           doc["_op_type"] = 'index'
           doc["_index"] = "newsgroup"
           doc["title"] = name
           doc["doc"] = file.read()
           docs.append(doc)
           i -= 1


with open("docs2.json", "w") as file:
    file.write(json.dumps(docs))

