import pandas as pd
from models import Hour
from sklearn.model_selection import train_test_split
from sklearn.linear_model import LinearRegression
import numpy as np
from sql import GetAllProductions, GetAlternateProductions


def aggreratePerHour(data):
    hours = []
    hour = data.iloc[0].Timestamp.timestamp() / 3600
    productions = 0
    shottimes = 0
    prevProduction = ""
    index = 0

    for index, row in data.iterrows():
        productions += 1
        shottimes += row.ShotTime

        if index == 0:
            prevProduction = row

        if row.Timestamp.hour != prevProduction.Timestamp.hour:
            hours.append(Hour(hour, productions, shottimes / productions))
            productions = 0
            shottimes = 0
            hour += 1

        prevProduction = row

    return hours


def predictProductions(timestampBegin, timestampEnd, componentId, plId):
    dfProductions = GetAllProductions(componentId, plId)

    if dfProductions.empty:
        dfProductions = GetAlternateProductions(plId)

    data = aggreratePerHour(dfProductions)

    df = pd.DataFrame.from_records([hour.to_dict() for hour in data])

    productionMax = df["productions"].max()

    currentMost = 0
    currentMostProductions = 0

    for i in range(0, 50):
        count = []

        for index, row in df.iterrows():
            if row.productions > productionMax / 50 * i * .95 and row.productions < productionMax / 50 * i * 1.05:
                count.append(row.productions)

        if (currentMost < len(count)):
            currentMost = len(count)
            currentMostProductions = np.mean(count)

    temp = df.copy()

    for index, row in temp.iterrows():
        if row.productions < currentMostProductions * .99 or row.productions > currentMostProductions * 1.01:
            df.drop(index, inplace=True)

    x = df.iloc[:, :1].values
    y = df.iloc[:, 1:2].values

    x_train, x_test, y_train, y_test = train_test_split(x, y, test_size=1/3)

    lr = LinearRegression()
    lr.fit(x_train, y_train)
    lr.score(x_test, y_test)
    y_predBegin = lr.predict([[int(timestampBegin) / 3600]])
    y_predEnd = lr.predict([[int(timestampEnd) / 3600]])

    average = np.mean([y_predBegin[0][0], y_predEnd[0][0]])

    return (average)
