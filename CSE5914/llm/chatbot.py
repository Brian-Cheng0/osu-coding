import getpass
import os

from langchain.cache import InMemoryCache
# <!-- ruff: noqa: F821 -->
from langchain.globals import set_llm_cache
from langchain_core.messages import HumanMessage, SystemMessage
from langchain_openai import ChatOpenAI

os.environ["OPENAI_API_KEY"] = 'API_KEY'
#invoek the chat modelAPI
chat = ChatOpenAI(model="gpt-3.5-turbo-0125", api_key="API_KEY")
set_llm_cache(InMemoryCache())
messages = [
    SystemMessage(content="You're a helpful assistant"),
    HumanMessage(content="tell me some about cryptos"),
]

chat.invoke(messages)

for chunk in chat.stream(messages):
    print(chunk.content, end="", flush=True)


# from langchain.cache import InMemoryCache

# set_llm_cache(InMemoryCache())
# llm = ChatOpenAI(model="gpt-3.5-turbo-0125")
# # The first time, it is not yet in cache, so it should take longer
# llm.predict("Tell me a joke")