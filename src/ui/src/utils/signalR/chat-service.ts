import * as signalR from '@microsoft/signalr';

class ChatService {
    private connection: null | signalR.HubConnection = null;
    private isConnected: boolean = false;
    private observers: Array<(isConnected: boolean) => void> = [];

    constructor(url: string) {
        this.connection = new signalR.HubConnectionBuilder()
            .withUrl(url)
            .withAutomaticReconnect()
            .configureLogging(signalR.LogLevel.Information)
            .build();


        this.connection.onclose(error => {
            // console.log('onclose')
            this.isConnected = false;
            this.notify(this.isConnected)
            this.start()
        });

        this.connection.onreconnecting(error => {
            // console.log('onreconnecting')
            this.isConnected = false;
            this.notify(this.isConnected)
        });

        this.connection.onreconnected(connectionId => {
            // console.log('onreconnected')
            this.isConnected = true;
            this.notify(this.isConnected)
        });
    }

    private notify = (value: boolean) => {
        this.observers.forEach(observer => observer(value));
    };

    subscribeOnConnectionStateChange = (observer: (isConnected: boolean) => void) => {
        this.observers.push(observer);
    };

    unsubscribeOnConnectionStateChange = (observer: (isConnected: boolean) => void) => {
        this.observers = this.observers.filter(_observer => _observer !== observer);
    };

    register = (type: string, handler: (...args: any[]) => void) => {
        this.connection?.on(type, handler);
    }

    start = async () => {
        try {
            await this.connection?.start();
            this.isConnected = true;
            this.notify(this.isConnected)
        } catch {
            this.isConnected = false;
            this.notify(this.isConnected)
            setTimeout(() => {
                this.start()
            }, 5000);
        }
    }

    send = (name: string, ...args: Array<any>) => {
        this.connection?.send(name, ...args);        
    }

    stop = () => {
        this.connection?.stop();
    }
}

export default ChatService;