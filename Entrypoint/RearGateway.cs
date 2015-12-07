using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using Brandon.Model;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Net;
using WebSocketSharp;

namespace Brandon.Gateway
{
    public class RearGateway
    {
        public readonly static RearGateway Self = new RearGateway();

        private static string AppKey { get { return "426e48ee-a41a-4981-b907-4afce4f53750"; } }
        private static string BaseAddress { get { return "ws://dev.push-sender.lgsmartplatform.com:8071/websocket"; } }
        public static String DeviceId { get { return "18620"; } }
        public static String DeviceAuth { get { return "9add9776224a89a316ce7243cb0c3a88aae4fc3b"; } }

        private WebSocket websocket;

        private RearGateway()
        {
        }

        public void Connect()
        {
            if(websocket != null || websocket.IsAlive) { return; }

            if(websocket != null)
            {
                websocket.Close();
                websocket = null;
            }

            using (websocket = new WebSocket(BaseAddress, new String[] { "lgpp-v1.0" }))
            {
                websocket.OnOpen += Websocket_OnOpen;
                websocket.OnMessage += Websocket_OnMessage;
                websocket.OnError += Websocket_OnError;
                websocket.OnClose += Websocket_OnClose;

                websocket.Connect();
            }
        }

        private void Websocket_OnClose(object sender, CloseEventArgs e)
        {
            websocket = null;
        }

        private void Websocket_OnOpen(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        private void Websocket_OnError(object sender, ErrorEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void Websocket_OnMessage(object sender, MessageEventArgs e)
        {
            throw new NotImplementedException();
        }

        public void Close()
        {
            if (websocket == null) { return; }

            websocket.Close();
        }
    }
}