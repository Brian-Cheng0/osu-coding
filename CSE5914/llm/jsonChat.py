import json
import os

from langchain.agents import create_json_agent
from langchain.agents.agent_toolkits import JsonToolkit
from langchain.cache import InMemoryCache
from langchain.chat_models import ChatOpenAI
from langchain.globals import set_llm_cache
from langchain.tools.json.tool import JsonSpec

os.environ['OPENAI_API_KEY'] = 'API_KEY'

#load json fileAPI_KEY
file = open('crypto.json')
data = json.load(file)

spec=JsonSpec(dict_=data,max_value_length=4000)
toolkit=JsonToolkit(spec=spec)
#gpt-4 is too expensive...

#set up a cache
set_llm_cache(InMemoryCache())
#set up a llm
agent=create_json_agent(llm=ChatOpenAI(temperature=0,model="gpt-3.5-turbo-0125"),
                        toolkit=toolkit,
                        max_iterations=1000,
                        verbose=True,
                        )

user_input = input("Hello! Tell me what do you want to know about crypto?\n")
while user_input is not "":
    print(agent.run(user_input))
    print()
    print("What else do you want to know?")
    user_input = input()
