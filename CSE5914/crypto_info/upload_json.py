# import json

# import requests
# from elasticsearch import Elasticsearch
# from elasticsearch.helpers import bulk

# es = Elasticsearch('https://localhost:9200', ca_certs="http_ca.crt", basic_auth=("elastic", "J9bfBBqfzGnlpwXwloZc"))

# url = 'http://api.coincap.io/v2/assets/'
# authToken = "lZCn_HfAGVWLb5PrBFcc"
# header = {
#     'Authorization': 'Bearer'+authToken,
#     'Content-Type': 'application/json'
# }

# response = requests.get(url,header)
# json_object = json.dumps(response.json(), indent=4)

# with open("crypto_info/crypto2.json","w") as outfile:
#     outfile.write(json_object)

# # Opening JSON file
# f = open('crypto_info\crypto2.json')

# # returns JSON object as
# # a dictionary
# data = json.load(f)
# bulk_data = []
# # Iterating through the json
# # # list
# for i in data['data']:
#     name = i['id']
#     doc = {
#         "id": i['id'],
#         "rank": i['rank'],
#         "symbol": i['symbol'],
#         "name": i["name"],
#         "supply": i["supply"],
#         "maxSupply": i["maxSupply"],
#         "marketCapUsd": i["marketCapUsd"],
#         "volumeUsd24Hr": i["volumeUsd24Hr"],
#         "priceUsd": i["priceUsd"],
#         "changePercent24Hr": i["changePercent24Hr"],
#         "vwap24Hr": i["vwap24Hr"],
#         "explorer": i["explorer"]
#     }
#     es.index(index=name, id=i, document=doc)
# # f.close()
# # es.indices.delete(index="crypto")
# # Closing file
# f.close()
# # bulk(es, bulk_data)

# #reconstruct the data structure in json
# # with open('crypto_info\crypto2.json') as json_file:
# #     data = json.load(json_file)

# #     print(data['data'])

from elasticsearch import Elasticsearch
from elasticsearch.helpers import bulk
import json

es = Elasticsearch('https://localhost:9200', ca_certs="http_ca.crt", basic_auth=("elastic", "J9bfBBqfzGnlpwXwloZc"))

# Opening JSON file
f1 = open('crypto_info/crypto.json')
f2 = open('crypto_info/coinLore.json')

# returns JSON object as
# # a dictionary
# def exclude_characters(input_string, characters_to_exclude):
#     return ''.join(char for char in input_string if char not in characters_to_exclude)
data1 = json.load(f1)
# data2 = json.load(f2)
coin = []
# Iterating through the json
# # list
# for i in data['data']:
#     name = i['id']
#     coin.append(name)
#     doc = {
#         "id": i['id'],
#         "rank": i['rank'],
#         "symbol": i['symbol'],
#         "name": i["name"],
#         "supply": i["supply"],
#         "maxSupply": i["maxSupply"],
#         "marketCapUsd": i["marketCapUsd"],
#         "volumeUsd24Hr": i["volumeUsd24Hr"],
#         "priceUsd": i["priceUsd"],
#         "changePercent24Hr": i["changePercent24Hr"],
#         "vwap24Hr": i["vwap24Hr"],
#         "explorer": i["explorer"]
#     }
#     es.index(index=name, id=1, document=doc)
# print(data)
for i in data1:
    name = i['id']
    coin.append(name)
    doc_1 = {
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
    es.index(index=name, id=i['id'], document=doc_1)
    # forbidden_characters = ['/', ',', '|', '>', '?', '*', '<', '"', ' ', '\\']
    # result = exclude_characters(name, forbidden_characters)
# for i in data2:
#     name = i["name"]
#     coin.append(name)
#     if((name.__contains__('/'))or(name.__contains__(','))or(name.__contains__('|'))or(name.__contains__('*'))or(name.__contains__('<'))or(name.__contains__('"'))or(name.__contains__(' '))or(name.__contains__('\\')))==False:
#         doc_2 = {
#             "id": i['id'],
#             "name": i["name"],
#             "name_id": i["name_id"],
#             "volume_usd": i["volume_usd"],
#             "active_pairs": i["active_pairs"],
#             "url": i["url"],
#             "country": i["country"]
#         }    
#         es.index(index=name.lower(), id=i['id'], document=doc_2)
f1.close()
# f2.close()
# for i in range(len(coin)):
#     es.indices.delete(index=coin[i])

# for i in data['data']:
#     print(i)


# Closing file
# bulk(es, bulk_data)