from urllib.request import urlopen

import streamlit as st
import yfinance as yf
from PIL import Image

#titles and subtitles
st.title("Cryptocurrency Daily Prices")
st.header("Main Dashboard")

#DEFINING TICKER VAR
Bitcoin = 'BTC-USD'
Ethereum = 'ETH-USD'
Ripple = 'XRP-USD'
BitcoinCash = 'BCH-USD'
Tether = 'USDT-USD'

#ACCESS data from yahoo finance
BTC_Data = yf.Ticker(Bitcoin)
ETH_Data = yf.Ticker(Ethereum)
XRP_Data = yf.Ticker(Ripple)
BCH_Data = yf.Ticker(BitcoinCash)
USDT_Data = yf.Ticker(Tether)


#fetch history data from yahoo
BTCHis = BTC_Data.history(period = "max")
ETHHis = ETH_Data.history(period = "max")
XRPHis = XRP_Data.history(period = "max")
BCHHis = BCH_Data.history(period = "max")
USDTHis = USDT_Data.history(period = "max")

# fetch crypto data for the dataframe
BTC = yf.download(Bitcoin, start="2024-1-1", end="2024-1-2")
ETH = yf.download(Ethereum, start="2024-1-1", end="2024-1-2")
XRP = yf.download(Ripple, start="2024-1-1", end="2024-1-2")
BCH = yf.download(BitcoinCash, start="2024-1-1", end="2024-1-2")
USDT = yf.download(Tether, start="2024-1-1", end="2024-1-2")

#Bitcoin
st.write("BITCOIN ($)")
#imageBTC = Image.open(urlopen()) ##image
#st.image(imageBTC)
#display dataframe
st.table(BTC)
#display a chart
st.bar_chart(BTCHis.Close)

#ETH
st.write("Etherum ($)")
#display dataframe
st.table(ETH)
#display a chart
st.bar_chart(ETHHis.Close)

#Ripple
st.write("Ripple ($)")
#display dataframe
st.table(XRP)
#display a chart
st.bar_chart(XRPHis.Close)

#BCH
st.write("BCH ($)")
#display dataframe
st.table(BCH)
#display a chart
st.bar_chart(BTCHis.Close)

#USDT
st.write("USDT ($)")
#display dataframe
st.table(USDT)
#display a chart
st.bar_chart(USDTHis.Close)
