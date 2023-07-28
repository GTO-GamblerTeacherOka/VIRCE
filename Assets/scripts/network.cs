using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class UDPreciever : MonoBehaviour
{
    // Start is called before the first frame update
    static void main()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
public void ListenMessage()
{
    var local = new IPEndPoint(IPAddress.Any, 5000);
    var remote = new IPEndPoint(IPAddress.Any, 5000);
    var client = new UdpClient(local);
 
    while (true)
    {
        var result = await client.ReceiveAsync();
        
        var data = Encoding.UTF8.GetString(result.Buffer);
        
        this.OnRecieve(data);
    }
}
 
private void OnRecieve(string data)
{
    public static void WriteBinaryToFile(string path, byte[] data)
    {
        var dir = Path.GetDirectoryName(path);
        if (!Directory.Exists(dir))
        {
            Directory.CreateDirectory(dir);
        }

        using (var fs = new FileStream(path, FileMode.Create))
        using (var sw = new BinaryWriter(fs))
        {
            sw.Write(data);
        }
    }
}

public async void SendMessage()
{
    var remote = new IPEndPoint(
        IPAddress.Parse("127.0.0.1"),
        5000);

    var client = new UdpClient(5000);
    client.Connect(remote);
    await client.SendAsync(message, message.Length);
    client.Close();
}
