import json

import requests

url = 'http://api.coincap.io/v2/assets/'
authToken = "lZCn_HfAGVWLb5PrBFcc"
header = {
    'Authorization': 'Bearer'+authToken,
    'Content-Type': 'application/json'
}
response = requests.get(url,header)
json_object = json.dumps(response.json(), indent=4)
# print(json_object)
with open("crypto_info/crypto.json","w") as outfile:
    outfile.write(json_object)
# J9bfBBqfzGnlpwXwloZc