import pandas as pd
from models import Hour
from sklearn.model_selection import train_test_split
from sklearn.linear_model import LinearRegression
import numpy as np
from sql import GetAllProductions, GetAlternateProductions


def predictProductions(timestampBegin, timestampEnd, componentId, plId):
    data = GetAllProductions(componentId, plId)

    if data.empty:
        data = GetAlternateProductions(plId)

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

    lr = LinearRegression()
    lr.fit(x_train, y_train)
    lr.score(x_test, y_test)
    y_predBegin = lr.predict([[int(timestampBegin) / 3600]])
    y_predEnd = lr.predict([[int(timestampEnd) / 3600]])

    print(y_predBegin, y_predEnd)

    average = np.mean([y_predBegin[0][0], y_predEnd[0][0]])

    return (average)
