# Broker Client
This is a client that establishes a connection to a HiveMQ cluster.
As of now it can only subscribe and publish to one topic.

# Example implementation for the hoist micorservice
1.  Import the `broker` package 
    - From the `back_end` directory you can import the broker package with:
    ```python
    import broker.client
    ```
2. Make an instance of the `Client` class:
   - This can be done with:
   ```python
   import broker.client
   
   publisher = broker.client.Client(microservice="hoist", topic="crane/components/hoist/state", qos=0, subscribe=True) 
   ```
3. Now send the generated data to the broker
   - This can be done by envoking the publish method:
   ```python
   import broker.client
   
   def simulate_movement():
       data = {
           "meta":{
              "topic":"crane/components/hoist/state",
              "isActive":"bool",
                  "component":"hoist"
           },
           "msg":{
              "isConnected":"bool",
              "relativePosition":{
                 "y":"float"
           },
             "speed":{
                "activeAcceleration":{
                    "y":"bool"
                },
                 "acceleration":{
                     "y":"float"
                 },
               "speed":{
                   "y":"float"
                }
             }
         }
      }    
       # generate data
       return data
   
   data = simulate_movement()
   publisher = broker.client.Client(microservice="hoist", topic="crane/components/hoist/state", qos=0, subscribe=True) 
   
   publisher.publish(data)
   ```
