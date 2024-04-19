from datetime import datetime

import fuzzy
from elasticsearch import Elasticsearch

Bitcoin = ['bitcoin', 'Bitcoin', 'bitcino', 'btcoin']
Ethereum = ['ethereum', 'Ethereum', 'ETH', 'eth', 'Ether', 'ether', 'ethreum', 'ethrum']
diction = {'bitcoin': Bitcoin, 'ethereum': Ethereum}

# Connect to the Elasticsearch server
es = Elasticsearch('https://localhost:9200', ca_certs="http_ca.crt", basic_auth=("elastic", "lZCn_HfAGVWLb5PrBFcc"))
# s = input("Search bar: ")

# res = es.search(index="bitcoin", query={"match_all": {}})
# for hit in res['hits']['hits']:
#     print("%(name)s" % hit["_source"])
# print("Got %d Hits:" % res['hits']['total']['value'])
# print(res['hits'])
# print(res['hits']['hits'])
# print(hit["_source"].keys())
# coin, key = fuzzy.predict(s)

def search(s):
  coin, key = fuzzy.predict(s)
  search_res = []
  for n in range(len(coin)):
      res = es.search(index=coin[n], query={"match_all": {}})
      # for hit in res['hits']['hits']:
      #     print("%(name)s" % hit["_source"])
      # print("Got %d Hits:" % res['hits']['total']['value'])
      # print(res['hits'])
      # print(res['hits']['hits'])
      for hit in res['hits']['hits']:
          # print("%(name)s" % hit["_source"])
          # accurate = False
          # all = []
          hit_dict = {}
          for i in hit["_source"].keys():
              for j in range(5):
                  if i == key[j]:
                      # print(i, ": ",hit["_source"][key[j]])
                      hit_dict[i] = hit["_source"][key[j]]
          search_res.append(hit_dict)
                      # accurate = True
          #     all.append(hit["_source"][i])
          # if not accurate:
          #     for i in all:
          #         print(i)
      # print("##########################")
  return search_res

# print(search(input("search bar: ")))