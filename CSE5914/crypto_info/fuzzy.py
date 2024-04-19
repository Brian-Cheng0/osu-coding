from fuzzywuzzy import process


def predict(str):
    coin = ['bitcoin', 'ethereum', 'tether', 'binance-coin', 'solana', 'usd-coin', 'xrp', 'cardano', 'dogecoin',
            'avalanche', 'tron', 'polkadot', 'chainlink', 'polygon', 'wrapped-bitcoin', 'multi-collateral-dai',
            'internet-computer', 'shiba-inu', 'litecoin', 'bitcoin-cash', 'unus-sed-leo', 'uniswap', 'ethereum-classic',
            'stellar', 'okb', 'monero', 'near-protocol', 'lido-dao', 'filecoin', 'injective-protocol', 'bitcoin-bep2',
            'cosmos', 'stacks', 'crypto-com-coin', 'vechain', 'maker', 'trueusd', 'the-graph', 'render-token',
            'bitcoin-sv', 'thorchain', 'aave', 'algorand', 'quant', 'elrond-egld', 'flow', 'helium', 'mina',
            'hedera-hashgraph', 'axie-infinity', 'kucoin-token', 'theta', 'fantom', 'the-sandbox', 'tezos', 'ftx-token',
            'wemix', 'chiliz', 'decentraland', 'kava', 'frax-share', 'eos', 'neo', 'synthetix-network-token', 'iota',
            'klaytn', 'oasis-network', 'conflux-network', 'siacoin', 'wootrade', 'gala', 'pancakeswap', 'ecash',
            'akash-network', 'arweave', 'pendle', 'xinfin-network', 'gnosis-gno', 'fetch', 'dydx', 'curve-dao-token',
            'casper', 'gatetoken', 'trust-wallet-token', 'nexo', 'nem', '1inch', 'compound', 'fei-protocol',
            'skale-network', 'aelf', 'uma', 'huobi-token', 'iotex', 'enjin-coin', 'gas', 'paxos-standard',
            'zcash', 'zilliqa', 'celo']

    keyword = ["id", "rank", "symbol", "name", "supply", "maxSupply", "marketCapUsd", "volumeUsd24Hr", "priceUsd",
               "changePercent24Hr", "vwap24Hr", "explorer"]

    # User search input
    query = str
    query_k = str

    # Fuzzy search using Levenshtein Distance to find the best match
    best_match = process.extractBests(query, coin, limit=2)
    best_match2 = process.extract(query_k, keyword)
    # print(best_match)
    ls = []
    for name, rate in best_match:
        ls.append(name)
    ls2 = []
    for name, rate in best_match2:
        ls2.append(name)
    return ls, ls2

# print(predict("bit price"))