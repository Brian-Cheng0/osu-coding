from elasticsearch import Elasticsearch, helpers
import os
import json
import re


### Example setup for Bonsai.io
#
# bonsai = os.environ['BONSAI_URL']
# auth = re.search('https\:\/\/(.*)\@', bonsai).group(1).split(':')
# host = bonsai.replace('https://%s:%s@' % (auth[0], auth[1]), '')

# # optional port
# match = re.search('(:\d+)', host)
# if match:
#   p = match.group(0)
#   host = host.replace(p, '')
#   port = int(p.split(':')[1])
# else:
#   port=443

# # Connect to cluster over SSL using auth for best security:
# es_header = [{
#  'host': host,
#  'port': port,
#  'use_ssl': True,
#  'http_auth': (auth[0],auth[1])
# }]

# # Instantiate the new Elasticsearch connection:
# es = Elasticsearch(es_header)

es = Elasticsearch('https://localhost:9200', ca_certs="http_ca.crt", basic_auth=("elastic", "J9bfBBqfzGnlpwXwloZc"))

# res = es.search (index="newsgroup", body={"query": {"match": {"doc": "Stanley"}}})
# print(len(res["hits"]["hits"]))
# for doc in res["hits"]["hits"]:
#   print(doc)

# res = es.search (index="newsgroup", body={"query": {"match": {"doc": "Phille"}}})
# print(len(res["hits"]["hits"]))

# res = es.search (index="newsgroup", body={"query": {"match": {"doc":{"query": "Phille", "fuzziness": "AUTO"}}}}, size =10000)
# print(len(res["hits"]["hits"]))

res = es.search(index = "newsgroup", body={"query": {"more_like_this": {"fields": ["doc"], "like": "The first ice resurfacer was invented by Frank Zamboni, who was originally in the refrigeration business. Zamboni created a plant for making ice blocks that could be used in refrigeration applications. As the demand for ice blocks waned with the spread of compressor-based refrigeration, he looked for another way to capitalize on his expertise with ice production"}}})
print(len(res["hits"]["hits"]))
for doc in res["hits"]["hits"]:
  print(doc)

