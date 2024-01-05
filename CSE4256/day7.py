from math import sqrt

class Point:
    def __init__(self, x_coord, y_coord):
        self.x = x_coord
        self.y = y_coord

    def __str__(self):
        return "(" + str(self.x) + ", " + str(self.y) +")"

    def distance(self, point):
        return sqrt((self.x - point.x) ** 2 + (self.y - point.y) ** 2)

    def midpoint(self,point):
        xAxis=(self.x+point.x)/2
        yAxis=(self.y+point.y)/2
        midpoint = Point(xAxis, yAxis)
        return midpoint

point1 =Point(3,2)
point2 =Point(7,4)
print(point1.midpoint(point2))