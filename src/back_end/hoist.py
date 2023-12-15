import time


class Hoist:
    def __init__(self):
        self.acceleration = 1
        self.is_active = True
        self.hoist_y = 500
        self.move = False  # changes als mqtt is geconnect

    def positive_movement(self):
        while self.is_active:
            time.sleep(0.25)

            self.acceleration = self.acceleration * 1.1
            speed = 0.83 * self.acceleration

            if speed >= 2.5:
                speed = 2.5
            # print(speed)
            move_hoist_y = speed
            self.deceleration(speed)
            self.position(move_hoist_y)

    def negative_movement(self):
        while self.is_active:
            while self.move:
                time.sleep(0.25)

                self.acceleration = self.acceleration * 1.1
                speed = -0.83 * self.acceleration

                if speed <= -2.5:
                    speed = -2.5
                print(speed)
                move_hoist_y = speed
                self.deceleration(speed)
                self.position(move_hoist_y)

    def deceleration(self, speed):
        while not self.move:
            time.sleep(0.25)
            if -0.5 <= speed <= 0.5:
                speed = 0
            elif speed >= 0.5:
                speed -= 0.2
            else:
                speed += 0.2
            move_hoist_y = speed
            self.position(move_hoist_y)

    def position(self, move_hoist_y):
        self.hoist_y += move_hoist_y
        print(self.hoist_y)

    def container_connect(self):
        # placeholders until the container interaction is implemented
        is_connected = False
        can_connect = False
