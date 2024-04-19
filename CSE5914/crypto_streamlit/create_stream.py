import datetime
import json
from urllib.request import urlopen

import streamlit as st
import yfinance as yf
from PIL import Image

#input data from json
crypto_names = []

with open('crypto_streamlit\crypto.json') as json_file:
    data = json.load(json_file)

    for key in data:
        temp_dict = {}
        temp_dict['name'] = key['name']
        temp_dict['symbol'] = key['symbol']

        crypto_names.append(temp_dict)

#titles and subtitles
st.title("Cryptocurrency Daily Prices")
st.header("Main Dashboard")



def stream(symbol, select_period): #select_period = "1d", "5d", "1mo", "3mo"
    #DEFINING TICKER VAR
    crypto_Var = symbol + '-USD'
    #ACCESS data from yahoo finance
    crypto_Data = yf.Ticker(crypto_Var)
    #fetch history data from yahoo
    crypto_Hist = crypto_Data.history(period = select_period)
    # fetch crypto data for the dataframe
    # crypto_Res = yf.download(crypto_Var, start=end_date, end=end_date)
    crypto_Res = yf.download(crypto_Var, period="1day")

    #plot tables
    st.write(crypto_Var)
    st.table(crypto_Res)
    st.bar_chart(crypto_Hist.Close)


# Main
print("Enter a selected crypto:")
cryp_name = input()
for item in crypto_names:
    if item['name'] == cryp_name:
        stream(item['symbol'], '1mo')
        break

# stream('ETH', '1mo')
