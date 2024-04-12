from elasticsearch import Elasticsearch, helpers
import os
import json
import re

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

es = Elasticsearch('https://localhost:9200', ca_certs="http_ca.crt", basic_auth=("elastic", "lZCn_HfAGVWLb5PrBFcc"))


with open("docs.json") as file:
    docs = json.loads(file.read())
    # print(docs)
    helpers.bulk(es, docs )
    