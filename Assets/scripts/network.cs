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
    var remote = new IPEndPoint(IPAddress.Any, 5000) as EndPoint;
    
    var socket = new Socket(SocketType.Dgram, ProtocolType.Udp);
    socket.Bind(local);
    
    var e = new SocketAsyncEventArgs();
    e.RemoteEndPoint = remote;
    e.SetBuffer(0, 1024);
    e.Completed += this.OnReceiveCompleted;
    
    socket.ReceiveFromAsync(e);
}
 
private void OnReceiveCompleted(object sender, SocketAsyncEventArgs e)
{
    var data = Encoding.UTF8.GetString(e.Buffer);
    
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
    
    this.ListenMessage();
}
public void SendMessage()
{
    var local = new IPEndPoint(
        IPAddress.Parse(0),
        5000);
    
    var remote = new IPEndPoint(
        IPAddress.Parse("127.0.0.1"),
        5000);
    
    var e = new SocketAsyncEventArgs();
    e.RemoteEndPoint = remote;
    e.SetBuffer(message, 0, message.Length);
    
    var socket = new Socket(SocketType.Dgram, ProtocolType.Udp);
    socket.Bind(local);
    socket.SendToAsync(e);
    socket.Shutdown(SocketShutdown.Both);
    socket.Close();
}
