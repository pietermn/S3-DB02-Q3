from flask import Flask
from dotenv import load_dotenv
from functions import predictProductions
from pathlib import Path

app = Flask(__name__)
dotenv_path = Path("../../.env")
load_dotenv(dotenv_path=dotenv_path)


@app.route("/averageactions/<timestampBegin>/<timestampEnd>/<componentid>/<productionlineid>")
def hello(timestampBegin, timestampEnd, componentid, productionlineid):
    return str(predictProductions(timestampBegin, timestampEnd, componentid, productionlineid))


if __name__ == "__main__":
    app.run(port=5000)
