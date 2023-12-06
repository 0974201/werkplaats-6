using HiveMQtt.Client;
using HiveMQtt.Client.Events;
using HiveMQtt.Client.Options;

var options = new HiveMQClientOptions();
options.Host = "c0bbe3829ad14fe3b24e5c51247f57c1.s2.eu.hivemq.cloud";
options.Port = 8883;
options.UseTLS = true;

options.UserName = "cranemqtt";
options.Password = "7va@tWTv2.Jw2yk";

var client = new HiveMQClient(options);

var connectResult = await client.ConnectAsync().ConfigureAwait(false);
while (true)
{

    //publish
    await client.PublishAsync("", "").ConfigureAwait(false);

    //subscribe
    await client.SubscribeAsync("").ConfigureAwait(false);


    client.OnMessageReceived += Client_OnMessageReceived;
    void Client_OnMessageReceived(object sender, OnMessageReceivedEventArgs e)
    {
        Console.WriteLine($"Recieved message: {e.PublishMessage.PayloadAsString}");
        //if you have recieved a message you can find it here, and do stuff when OnMessageRecieved.
    }

    client.OnDisconnectReceived += Client_OnDisconnectRecieved;
    void Client_OnDisconnectRecieved(object sender, OnDisconnectReceivedEventArgs e)
    {
        //if you have recieved a message you can find it here, and do stuff when OnMessageRecieved.
    }

    client.BeforeConnect += Client_BeforeConnect;
    void Client_BeforeConnect(object sender, BeforeConnectEventArgs e)
    {
        //do stuff when OnMessageRecieved.
    }

    client.AfterConnect += Client_AfterConnect;
    void Client_AfterConnect(object sender, AfterConnectEventArgs e)
    {
        //if you have recieved a message you can find it here, and do stuff when OnMessageRecieved.
    }
}


