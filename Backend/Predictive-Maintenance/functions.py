import pandas as pd
from models import Hour
from sklearn.model_selection import train_test_split
from sklearn.linear_model import LinearRegression
import numpy as np
from sql import GetAllProductions, GetAlternateProductions
import joblib
from os import listdir
from os.path import isfile, join


def predictProductions(timestampBegin, timestampEnd, componentId, plId):

    ComponentFiles = [f for f in listdir(
        "SklearnComponentModels") if isfile(join("SklearnComponentModels", f))]

    ProductionLineFiles = [f for f in listdir(
        "SklearnProductionlineModels") if isfile(join("SklearnProductionlineModels", f))]

    lr = LinearRegression()
    hasLoadedModel = False
    hasCreatedAlternateModel = False

    # Check if it has a saved model for this component on this production line
    for file in ComponentFiles:
        if file == 'pl{}-cp{}'.format(plId, componentId):
            lr = joblib.load(
                'SklearnComponentModels/pl{}-cp{}'.format(plId, componentId))
            hasLoadedModel = True

    if hasLoadedModel is False:
        data = GetAllProductions(componentId, plId)

        if data.empty:
            # Check if it has a model for this productionline for all components
            for file in ProductionLineFiles:
                if file == 'pl{}'.format(plId, componentId):
                    lr = joblib.load(
                        'SklearnProductionlineModels/pl{}'.format(plId))
                    hasLoadedModel = True

            if (hasLoadedModel is False):
                hasCreatedAlternateModel = True
                data = GetAlternateProductions(plId)

        if hasLoadedModel is False:

            productionMax = data["productions"].max()

            currentMost = 0
            currentMostProductions = 0

            for i in range(0, 50):
                count = []

                for index, row in data.iterrows():
                    if row.productions > productionMax / 50 * i * .99 and row.productions < productionMax / 50 * i * 1.01:
                        count.append(row.productions)

                if (currentMost < len(count)):
                    currentMost = len(count)
                    currentMostProductions = np.mean(count)

            temp = data.copy()

            for index, row in temp.iterrows():
                if row.productions < currentMostProductions * .99 or row.productions > currentMostProductions * 1.01:
                    data.drop(index, inplace=True)

            x = data.iloc[:, :1].values
            y = data.iloc[:, 1:2].values

            try:
                x_train, x_test, y_train, y_test = train_test_split(
                    x, y, test_size=1/3)

            except:
                return 0

            lr.fit(x_train, y_train)

    # lr.score(x_test, y_test)
    y_predBegin = lr.predict([[int(timestampBegin) / 3600]])
    y_predEnd = lr.predict([[int(timestampEnd) / 3600]])

    if (hasLoadedModel is False):
        if (hasCreatedAlternateModel is True):
            joblib.dump(lr, 'SklearnProductionlineModels/pl{}'.format(plId))
        else:
            joblib.dump(
                lr, 'SklearnComponentModels/pl{}-cp{}'.format(plId, componentId))

    average = np.mean([y_predBegin[0][0], y_predEnd[0][0]])

    return (average)
