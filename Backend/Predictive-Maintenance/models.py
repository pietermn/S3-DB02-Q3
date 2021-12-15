from datetime import datetime


class Hour:
    def __init__(self, hour, productions, averageshottime) -> None:
        self.hour = hour
        self.productions = productions
        self.ast = averageshottime

    def __str__(self) -> str:
        return "hour: {}, productions: {}, shottimes: {}".format(self.hour, self.productions, self.ast)

    def to_dict(self):
        return {
            'hour': self.hour,
            'productions': self.productions,
            'ast': self.ast
        }
