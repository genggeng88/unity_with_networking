import java.io.*;
import java.net.*;
import java.util.*;

public class testServer {
    private ServerSocket serverSocket;
    public static final int portNumber = 3100;
    private static Map<String, Socket> clientList;
    
    public testServer() {}
    
    public void start() throws IOException {
        clientList = new HashMap<>();
        serverSocket = new ServerSocket(portNumber);
        System.out.println("Server started on port " + portNumber);
        
        while (true) {
            Socket clientSocket = serverSocket.accept();
            String clientId = UUID.randomUUID().toString();
            System.out.println("New client connected with clientId: " + clientId);
            // start a new thread to handle communication with the client
            clientList.put(clientId, clientSocket);
            Thread clientThread = new Thread(new ClientHandler(clientId, clientSocket));
            clientThread.start();
        }
    }
    
    private static class ClientHandler implements Runnable {
        private Socket clientSocket;
        private String clientId;
        
        public ClientHandler(String clientId, Socket clientSocket) {
            this.clientSocket = clientSocket;
            this.clientId = clientId;
        }
        
        public void run() {
            while (true){
                try {
                    InputStream inputStream = clientSocket.getInputStream();
                    /**
                    if (inputStream.available() <= 0){
                        System.out.println("inputStream is unavailable");
                        continue;
                    }
                    **/
                    BufferedReader in = new BufferedReader(new InputStreamReader(inputStream));
                    String inputLine = in.readLine();
                    System.out.println("Received message from " + clientId + ", with message: " + inputLine);
                    
                    for (Map.Entry<String, Socket> entry : clientList.entrySet()) {
                        //String id = entry.getKey();
                        Socket client = entry.getValue();
                        // do something with the key and value
                        PrintWriter out = new PrintWriter(client.getOutputStream(), true);
                        out.println("Echo: " + inputLine);
                    }
                    
                    System.out.println("Outside the read loop*************************");
                    // close the client socket
                    // clientSocket.close();
                } catch (IOException e) {
                    System.err.println("Error handling client: " + e);
                }
            }
        }
    }
    
    public static void main(String[] args) throws IOException {
        testServer server = new testServer();
        server.start();
    }
}
