import json

import requests
from elasticsearch import Elasticsearch
from elasticsearch.helpers import bulk

es = Elasticsearch('https://localhost:9200', ca_certs="http_ca.crt", basic_auth=("elastic", "lZCn_HfAGVWLb5PrBFcc"))

url = 'http://api.coincap.io/v2/assets/'
authToken = "lZCn_HfAGVWLb5PrBFcc"
header = {
    'Authorization': 'Bearer'+authToken,
    'Content-Type': 'application/json'
}

response = requests.get(url,header)
json_object = json.dumps(response.json(), indent=4)

with open("D:\CSE5914new\CSE5914new\crypto_info\crypto2.json","w") as outfile:
    outfile.write(json_object)

# Opening JSON file
f = open('D:\CSE5914new\CSE5914new\crypto_info\crypto2.json')

# returns JSON object as
# a dictionary
data = json.load(f)
bulk_data = []
# Iterating through the json
# # list
for i in data['data']:
    name = i['id']
    doc = {
        "id": i['id'],
        "rank": i['rank'],
        "symbol": i['symbol'],
        "name": i["name"],
        "supply": i["supply"],
        "maxSupply": i["maxSupply"],
        "marketCapUsd": i["marketCapUsd"],
        "volumeUsd24Hr": i["volumeUsd24Hr"],
        "priceUsd": i["priceUsd"],
        "changePercent24Hr": i["changePercent24Hr"],
        "vwap24Hr": i["vwap24Hr"],
        "explorer": i["explorer"]
    }
    es.index(index=name, id=i, document=doc)
# f.close()
# es.indices.delete(index="crypto")

# Closing file
f.close()
# bulk(es, bulk_data)

#reconstruct the data structure in json
result=[]
with open('D:\CSE5914new\CSE5914new\crypto_info\crypto2.json') as json_file:
    data = json.load(json_file)
    for item in data['data']:
        result.append(item)

    json_obj = json.dumps(result, indent=4)

    with open("D:\CSE5914new\CSE5914new\crypto_info\crypto_new.json", "w") as outfile:
        outfile.write(json_obj)