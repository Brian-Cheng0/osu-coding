import os

os.environ["OPENAI_API_KEY"] = "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"

from langchain.agents import AgentType, initialize_agent
from langchain.cache import InMemoryCache
from langchain.globals import set_llm_cache
from langchain_community.tools.yahoo_finance_news import YahooFinanceNewsTool
from langchain_openai import ChatOpenAI

#set up a cache
set_llm_cache(InMemoryCache())

llm = ChatOpenAI(temperature=0.0)
tools = [YahooFinanceNewsTool()]
agent_chain = initialize_agent(
    tools,
    llm,
    agent=AgentType.ZERO_SHOT_REACT_DESCRIPTION,
    verbose=True,
)

agent_chain.run(
    "tell me information about a good crypto",
)