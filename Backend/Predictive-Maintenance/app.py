from flask import Flask
from functions import predictProductions

app = Flask(__name__)


@app.route("/averageactions/<timestampBegin>/<timestampEnd>/<componentid>/<productionlineid>")
def hello(timestampBegin, timestampEnd, componentid, productionlineid):
    return str(predictProductions(timestampBegin, timestampEnd, componentid, productionlineid))


if __name__ == "__main__":
    app.run(port=5000)
