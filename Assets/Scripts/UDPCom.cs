
using UnityEngine;
using System.Collections;

using System;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Threading;

public class UDPCom : MonoBehaviour {
	
	// receiving Thread
	Thread receiveThread;
	
	// udpclient object
	UdpClient client;
	
	// public
	public string IP = "192.1.159.22";
	public int port = 50;
	//IPEndPoint remoteEndPoint;
	
	// infos
	public string lastReceivedUDPPacket="";
	public string allReceivedUDPPackets=""; // clean up this from time to time!

	// init
	public void init(){
		// status
		print("UDPReceive Init");

		client = new UdpClient(port);

		// create thread for receive UDP messages
		receiveThread = new Thread(new ThreadStart(ReceiveData));
		receiveThread.IsBackground = true;
		receiveThread.Start();
		
	}

	// send String Data
	public void sendString(string message){
		try{
			if (message != ""){
				IPEndPoint remoteEndPoint = new IPEndPoint(IPAddress.Parse(IP), port);
				// Send message String
				byte[] data = Encoding.UTF8.GetBytes(message);
				client.Send(data, data.Length, remoteEndPoint);
				print("Sending : "+IP+":"+port+" : "+message);
			}
		}
		catch (Exception err){
			print(err.ToString());
		}
	}

	// send Byte Data
	public void sendByte(byte[] data){
		try{
			if ( data.Length > 0){
				IPEndPoint remoteEndPoint = new IPEndPoint(IPAddress.Parse(IP), port);
				// Send byte message
				client.Send(data, data.Length, remoteEndPoint);
				print("Sending : "+IP+":"+port+" : "+ data.Length +" ByteData");
			}
		}
		catch (Exception err){
			print(err.ToString());
		}
	}

	// receive thread
	private  void ReceiveData(){
		//client = new UdpClient(port);
		while (true){
			try{
				// receive bytes
				IPEndPoint anyIP = new IPEndPoint(IPAddress.Any,0);
				byte[] data = client.Receive(ref anyIP);
				
				// show received message.
				string text = Encoding.UTF8.GetString(data);
				print(">> " + text);
				
				// store new massage as latest message
				lastReceivedUDPPacket=text;
				
				// update received messages
				allReceivedUDPPackets=allReceivedUDPPackets+text;
				
			}
			catch (Exception err){
				print(err.ToString());
			}
		}
	}
	
	// getLatestUDPPacket cleans up the rest
	public string getLatestUDPPacket(){
		allReceivedUDPPackets="";
		return lastReceivedUDPPacket;
	}

	// Stop receive UDP messages
	public void stopThread(){
		if (receiveThread.IsAlive){
			receiveThread.Abort();
		}
		client.Close();
	}

	// Stop receive UDP messages with application Quit
	void OnApplicationQuit () {
		stopThread ();
	}


}