import json

from elasticsearch import Elasticsearch
from elasticsearch.helpers import bulk

es = Elasticsearch('https://localhost:9200', ca_certs="http_ca.crt", basic_auth=("elastic", "lZCn_HfAGVWLb5PrBFcc"))

# Opening JSON file
f = open('D:\CSE5914new\CSE5914new\crypto_info\crypto.json')

# returns JSON object as
# a dictionary
data = json.load(f)
coin = []
# Iterating through the json
# # list
for i in data['data']:
    name = i['id']
    coin.append(name)
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
    es.index(index=name, id=1, document=doc)
f.close()

# for i in range(5, len(coin)):
#     es.indices.delete(index=coin[i])

# for i in data['data']:
#     print(i)


# Closing file
f.close()
# bulk(es, bulk_data)