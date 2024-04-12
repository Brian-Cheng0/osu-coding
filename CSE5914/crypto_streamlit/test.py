import json

#input data from json
crypto_names = []

with open('crypto_streamlit\crypto.json') as json_file:
    data = json.load(json_file)

    for key in data:
        temp_dict = {}
        temp_dict['name'] = key['name']
        temp_dict['symbol'] = key['symbol']

        crypto_names.append(temp_dict)



print("Enter a selected crypto:")
cryp_name = input()
for item in crypto_names:
    # print(item['name'])
    # print(cryp_name)
    if item['name'] == cryp_name:

        print("succes")
        break
