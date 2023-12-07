# Crane template

# here need to be all the data u need
this is fount under: process_data
varname = data.get("nameofdata")

for exemple
when u get this string:
{"isActive": true, "component": "Hoist", "acceleration": null, "speed":10}
and u want to read "isActive" 
the code shoul look like this:
iaActive = data.get("isActive")

# all the data u want to send
this is found under: send_data

all the data u want to send need to be in a json string
def send_data(in here u need to put the name of the variables u want to send)
    mqtt_send = '{all the data u want to send}'
    client.publish("the name of the topic", payload=mqtt_send, qos=1)


for example u have a var named speed:

//code that gives the speed//

def send_data(speed_value):
    speed = speed_value
    mqtt_send = '{"speed":'+str(speed)+'}'
    print(mqtt_send)
    client.publish("speed", payload=mqtt_send, qos=1)

'+str(speed)+'
' so the comment stops and the compiler sees the code
+ add the the string
str( to make sure u send a string, if you don't je json breaks
speed name of variable
) closes str
+ 
' makes it commend again

client.subscribe("topics u need information for", qos=1)

example
client.subscribe("dashboard/keys", qos=1)