import pandas as pd
from dateutil.relativedelta import relativedelta
from datetime import datetime

credentials = "mysql+mysqlconnector://root:root@db:3306/db"


def getProductions(table, plid, start, end):
    return pd.read_sql("""
            SELECT
                unix_timestamp(Timestamp) / 3600 AS hour,
                COUNT(*) AS productions,
                ShotTime as ast
            FROM `{}`
            WHERE ProductionLineId = {}
            AND Timestamp > '{}'
            AND Timestamp < '{}'
            GROUP BY CAST(Timestamp as date),
                HOUR(Timestamp)
            """.format(table, plid, start, end), con=credentials)


def getAllProductionsFromProductionLine(table, plid):
    return pd.read_sql("""
            SELECT
                unix_timestamp(Timestamp) / 3600 AS hour,
                COUNT(*) AS productions,
                ShotTime as ast
            FROM `{}`
            WHERE ProductionLineId = {}
            GROUP BY CAST(Timestamp as date),
                HOUR(Timestamp)
            """.format(table, plid), con=credentials)


def GetAlternateProductions(productionLineId):
    productionsPerHour = pd.DataFrame()
    tableDate = datetime.strptime('2020-09', '%Y-%m').date()
    for i in range(0, 14):
        if tableDate > datetime.strptime('2020-09', '%Y-%m').date() and tableDate < datetime.strptime('2021-06', '%Y-%m').date():
            productionsPerHour = productionsPerHour.append(getAllProductionsFromProductionLine(
                "Productions-{}".format(str(tableDate)[:-3]), productionLineId), ignore_index=True)

        tableDate = tableDate + relativedelta(months=1)

    return productionsPerHour


def GetAllProductions(componentId, productionlineId) -> pd.DataFrame:
    history = pd.read_sql("""
        SELECT * 
        FROM `ProductionLinesHistory`
        WHERE ComponentId = {}
        AND ProductionLineId = {}
        ORDER BY StartDate
        """.format(componentId, productionlineId), con=credentials)

    productionsPerHour = pd.DataFrame()

    for index, row in history.iterrows():
        endDate = ""
        if row.EndDate.year == 1:
            endDate = datetime.now()
        else:
            endDate = row.EndDate
        differenceMonths = (endDate.year - row.StartDate.year) * \
            12 + (endDate.month - row.StartDate.month)
        tableDate = row.StartDate.date()

        for i in range(0, differenceMonths + 1):
            if tableDate > datetime.strptime('2020-09', '%Y-%m').date() and tableDate < datetime.strptime('2021-06', '%Y-%m').date():
                productionsPerHour = productionsPerHour.append(getProductions(
                    "Productions-{}".format(str(tableDate)[:-3]), row.ProductionLineId, row.StartDate, row.EndDate), ignore_index=True)

            tableDate = tableDate + relativedelta(months=1)

    return productionsPerHour
