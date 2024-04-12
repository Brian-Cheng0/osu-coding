import json

#create a new dict to store
result = []

with open('UI\coinLore.json') as json_file:
    data = json.load(json_file)

    for key in data:
        temp_dict={}
        ##transfer data into new dict
        temp_dict['id'] = key['id']
        temp_dict['name'] = key['name']
        temp_dict['name_id'] = key['name_id']
        temp_dict['volume_usd'] = key['volume_usd']
        temp_dict['active_pairs'] = key['active_pairs']
        temp_dict['url'] = key['url']
        temp_dict['country'] = key['country']

        ##store new dict into result list
        result.append(temp_dict)



print(len(result))

# with open("UI\coinLore_new.json","w") as outfile:
#     outfile.write(json_object)

# json_obj = json.dumps(result, indent=4)

# with open("UI\coinLore_new.json", "w") as outfile:
#     outfile.write(json_obj)




