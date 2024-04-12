from random import choice, randint, seed
from copy import copy
from os import path
import warnings
warnings.filterwarnings("ignore")

from sklearn.linear_model import LogisticRegression
from sklearn.tree import DecisionTreeClassifier
from sklearn.neural_network import MLPClassifier
from sklearn.model_selection import cross_val_score

from price_crawler import update_token, build_features


# [<Prediction Interval>-<Feature Name>]
TARGET = ['1D-close', '3D-close', '7D-close']

# TA features to be used
TA_FEATURES = ['ROC', 'MOM', 'EMA', 'SMA', 'VAR', 'MACD', 'ADX', 'RSI']

ROLL_RANGE = [1, 3, 5]                          # How many previous periods to be considered
DELTA_RANGE = ['24H', '12H', '6H', '3H']        # Possible duration of each period
MODEL_RANGE = [DecisionTreeClassifier, LogisticRegression, MLPClassifier]     # Possible Models
NORMALIZE = ['MinMax', 'Normal', 'None']        # Possible data normalization method

CRITERION_RANGE = ['gini', 'entropy']   # Possible way to construct a decision tree
DEPTH_RANGE = [3, 4, 5, 6, 7, 8]        # Possible tree depth when using decision trees
HIDDEN_RANGE = [8, 16, 24, 32, 48]      # Possible hidden size when using MLP


def generate_config():
    seed()
    config = {}

    config['delta'] = choice(DELTA_RANGE)
    config['roll'] = choice(ROLL_RANGE)
    config['model'] = choice(MODEL_RANGE)
    config['norm'] = choice(NORMALIZE)
    config['criterion'] = 'NA'
    config['depth'] = -1
    config['hidden'] = -1
    
    target = copy(TARGET)

    if config['model'] == DecisionTreeClassifier:
        config['depth'] = choice(DEPTH_RANGE)
        config['criterion'] = choice(CRITERION_RANGE)
    if config['model'] == MLPClassifier:
        config['hidden'] = choice(HIDDEN_RANGE)

    features = ['open', 'close', 'high', 'low', 'volume'] + [s.lower() for s in TA_FEATURES]
    features += [c + '-r' + str(i + 1) for i in range(config['roll']) for c in features]

    return config, target, features


def to_csv_line(cfg, target, result):
    hyper_paramters = ['model', 'delta', 'roll', 'norm', 'hidden', 'depth', 'criterion']
    row = [target] + [str(cfg[p]) for p in hyper_paramters] + result
    line = ','.join(row) + '\n'
    return line


def train(address, cfg, target):

    '''
    Input(address <str>, configuration <dict>, target <dict>, log <file>) -> list
    This function gives a list of trained model for each time horizon using the configuration above
    '''

    X, y = build_features(address, freq=cfg['delta'], ta_list=TA_FEATURES, ys=target, roll=cfg['roll'])
    model = cfg['model']
    output_models = []
    results = []

    if cfg['norm'] == 'MinMax':
        X = (X - X.min()) / (X.max() - X.min())
    if cfg['norm'] == 'Normal':
        X = (X - X.mean()) / X.std()
    
    for label, t in y.iteritems():

        if model == DecisionTreeClassifier:
            depth = cfg['depth']
            criterion = cfg['criterion']
            m = model(max_depth=depth, criterion=criterion)
        elif model == MLPClassifier:
            hidden = cfg['hidden']
            m = model(hidden_layer_sizes=(hidden, 2), max_iter=500, solver='sgd')
        else:
            m = model(max_iter=500, solver='sag')

        scores = cross_val_score(m, X, t, cv=6)
        result = [str(scores.mean()), str(scores.std())]
        result = to_csv_line(cfg, label, result)
        output_models.append(m)
        results.append(result)
    
    return output_models, results


if __name__ == '__main__':
    # Number of trials
    TRIAL = 10

    address = '0x9f8f72aa9304c8b593d555f12ef6589cc3a579a2'
    update_token(address)
    csv_lines = []
    file_name = address + '_tuning.csv'
    if path.exists('tuning_logs/' + file_name):
        log = open('tuning_logs/' + file_name, 'a')
    else:
        log = open('tuning_logs/' + file_name, 'w')
        log.write('TARGET, MODEL, DELTA, ROLL, NORM, HIDDEN, DEPTH, CRITERION, SCORE, STD\n')
    try:
        for i in range(TRIAL):
            cfg, target, features = generate_config()
            models, lines = train(address, cfg, target)
            csv_lines += lines
            print(i + 1, 'trials completed')
        log.writelines(csv_lines)
        log.close()
    except KeyboardInterrupt:
        log.writelines(csv_lines)
        log.close()