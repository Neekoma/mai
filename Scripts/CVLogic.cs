using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Linq;
using System.Drawing;
using System.Net.Sockets;
using System.Net;

public class CVLogic : MonoBehaviour
{
    [SerializeField] private RenderTexture _renderTexture;
    private Texture2D _texture;
    private string ip = "localhost";
    private int port = 1234;
    private static IPEndPoint iPEndPoint;
    private UdpClient client = new UdpClient();


    private void Start()
    {
        iPEndPoint = new IPEndPoint(IPAddress.Parse("127.0.0.1"), port);

    }

    private  void Update()
    {
        _texture = ToTexture2D(_renderTexture);
        var bitmap = _texture.EncodeToJPG();
        client.Send(bitmap, bitmap.Length, iPEndPoint);
    }

    



    private Texture2D ToTexture2D(RenderTexture rTex)
    {
        Texture2D tex = new Texture2D(512, 512, TextureFormat.RGB24, false);
        RenderTexture.active = rTex;
        tex.ReadPixels(new UnityEngine.Rect(0, 0, rTex.width, rTex.height), 0, 0);
        tex.Apply();
        return tex;
    }
}
