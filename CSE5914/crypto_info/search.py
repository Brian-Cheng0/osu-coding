# from datetime import datetime

# from elasticsearch import Elasticsearch

# Bitcoin = ['bitcoin', 'Bitcoin', 'Bitcoin', 'bitcino', 'btcoin']
# Ethereum = ['ethereum', 'Ethereum', 'ETH', 'eth', 'Ether', 'ether', 'ethreum', 'ethrum']
# diction = {'bitcoin': Bitcoin, 'ethereum': Ethereum}

# # Connect to the Elasticsearch server
# es = Elasticsearch('https://localhost:9200', ca_certs="http_ca.crt", basic_auth=("elastic", "J9bfBBqfzGnlpwXwloZc"))
# s = input("Search bar: ")
# s_list = s.split()
# coin = False
# for i in diction.keys():
#     for n in diction[i]:
#         if not coin:
#             for j in range(len(s_list)):
#                 if n == s_list[j]:
#                     coin = True
#                     search = i
#                     break
#         else:
#             break
# if coin:
#     res = es.search(index=search, query={"match_all": {}})
#     # print(res)
#     print("Got %d Hits:" % res['hits']['total']['value'])
#     # print(res['hits'])
#     # print(res['hits']['hits'])
#     for hit in res['hits']['hits']:
#         # print("%(name)s" % hit["_source"])
#         accurate = False
#         all = []
#         for i in hit["_source"].keys():
#             for j in range(len(s_list)):
#                 if i == s_list[j]:
#                     print(hit["_source"][s_list[j]])
#                     accurate = True
#             all.append(hit["_source"][i])
#         if not accurate:
#             for i in all:
#                 print(i)
# else:
#     print("Null not such information")

from datetime import datetime
from elasticsearch import Elasticsearch
import fuzzy
Bitcoin = ['bitcoin', 'Bitcoin', 'bitcino', 'btcoin']
Ethereum = ['ethereum', 'Ethereum', 'ETH', 'eth', 'Ether', 'ether', 'ethreum', 'ethrum']
diction = {'bitcoin': Bitcoin, 'ethereum': Ethereum}

# Connect to the Elasticsearch server
es = Elasticsearch('https://localhost:9200', ca_certs="http_ca.crt", basic_auth=("elastic", "J9bfBBqfzGnlpwXwloZc"))
s = input("Search bar: ")

# res = es.search(index="bitcoin", query={"match_all": {}})
# for hit in res['hits']['hits']:
#     print("%(name)s" % hit["_source"])
# print("Got %d Hits:" % res['hits']['total']['value'])
# print(res['hits'])
# print(res['hits']['hits'])
# print(hit["_source"].keys())
coin, key = fuzzy.predict(s)
# search_result = es.get(index="iotex",id=2)
# print(search_result)


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
        for i in hit["_source"].keys():
            for j in range(5):
                if i == key[j]:
                    print(i, ": ",hit["_source"][key[j]])
                    # accurate = True
        #     all.append(hit["_source"][i])
        # if not accurate:
        #     for i in all:
        #         print(i)
    print("##########################")