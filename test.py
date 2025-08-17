import random

class MapObject:
    def __init__(self, objType, asciiSymbol):
        self.objType = objType
        self.asciiSymbol = asciiSymbol

    def draw(self):
        return self.asciiSymbol

class Monster

objects = [

]

def generate_room(width=10, height=6, monsters=3, treasures=2):
    # Створюємо кімнату з порожніх місць
    room = [["." for _ in range(width)] for _ in range(height)]

    # Додаємо стіни по краях
    for x in range(width):
        room[0][x] = "#"
        room[height - 1][x] = "#"
    for y in range(height):
        room[y][0] = "#"
        room[y][width - 1] = "#"

    # Ставимо гравця десь усередині
    px, py = random.randint(1, width - 2), random.randint(1, height - 2)
    room[py][px] = "P"

    # Ставимо монстрів
    for _ in range(monsters):
        while True:
            x, y = random.randint(1, width - 2), random.randint(1, height - 2)
            if room[y][x] == ".":
                room[y][x] = "M"
                break

    # Ставимо скарби
    for _ in range(treasures):
        while True:
            x, y = random.randint(1, width - 2), random.randint(1, height - 2)
            if room[y][x] == ".":
                room[y][x] = "T"
                break

    # Вивід
    for row in room:
        print("".join(row))


generate_room(100, 100, 20, 30)